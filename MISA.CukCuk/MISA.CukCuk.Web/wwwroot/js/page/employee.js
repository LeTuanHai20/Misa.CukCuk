$(document).ready(function () {
    // Khởi tạo đối tượng nhân viên
    new EmployeeJS();
    dialogDetail = $("#d-dialog").dialog({
        autoOpen: false,
        fluid: true,
        minWidth: 860,
        minHeight:863,
        resizable: true,
        position: ({ my: "center", at: "center", of: window }),
        modal: true,
    });
    datePickerDetail = $('.datepicker').datepicker({
        dateFormat: 'dd-mm-yy'
    });
    datePickerDetail.datepicker('enable');
})
/**================================================
 * Class quản lý sự kiện cho trang employee
 * CreatedBy: LTHAI(12/11/2020)
 * */
class EmployeeJS extends BaseJS {
    constructor() {
        super();
        this.initEventsOfEmployee();
        this.initValueDepartmentToolBar();
        this.initValuePositionToolBar();
        this.value = "";
        this.departmentId = "";
        this.positionId = "";
    }
    /**===================================================================
     * Quy định đường dẫn được sử dụng để lấy dữ liệu ở trang quản lý nhân viên
     * CreatedBy: LTHAI(12/11/2020)
     * */
    
    setApiRouter() {
        this.apiRouter = "/api/v1/employees";
    }
    /**
     * Khởi tạo các sự kiện của trang nhân viên
     * CreatedBy: LTHAI(2/12/2020)
     * */
    initEventsOfEmployee() {
        let thisInit = this;
        // Xóa bản ghi khi click nút xóa 
        $('.icon-remove button').click(thisInit.ShowConfirmDeleteEmployee.bind(this))

        //Hiển thị dialog khi nhấn nút thêm khách hàng
        $('#btn-add-employee').click(this.EventsWhenClickAddEmployee.bind(thisInit))

        //Thoát khỏi dialog bằng icon hủy hoặc nút hủy
        $('body').on('click', '.ui-dialog-titlebar-close, #d-btn-cancel', function () {
            RefreshDialog();
            dialogDetail.dialog('close');
        }) 

        //Hiển thị dialog khi nhấn 2 lần vào từng dòng trong bảng
        $('table tbody').on('dblclick', 'tr', function () {
            thisInit.EventsWhenDoubleClickTr(this);
        }) 
        //xóa bản ghi
        $('#btn-delete').click(thisInit.DeleteEmployeeRecord.bind(thisInit))

        //Thêm mới dữ liệu khi ấn nút lưu trong dialog
        $('#d-btn-save').click(this.SaveDataWhenClickButtonSave.bind(thisInit))

        // Lọc khi chọn chức vụ
        $('#data-position').on('click', 'a[filter="Position"]', function () {
            $('#button-search-position').text($(this).text());
            $('#button-search-position').append('<i class="fas fa-caret-down"></i>');
            thisInit.positionId = $(this).attr("data");
            thisInit.LoadDataByFilters();
        })
        // Lọc khi chọn chọn phòng ban
        $('#data-department').on('click', 'a[filter="Department"]', function () {
            $('#button-search-department').text($(this).text());
            $('#button-search-department').append('<i class="fas fa-caret-down"></i>');
            thisInit.departmentId = $(this).attr("data");
            thisInit.LoadDataByFilters();
        })
        // Lọc khi nhấn nút enter
        $('#filterDynamic').on('keydown', function (e) {
            thisInit.value = $(this).val();
            if (e.which == 13) {
                thisInit.LoadDataByFilters();
            }
        });
    }
    /**=====================================================
    * Sự kiện khi click vào nút xóa thông tin của nhân viên
    * CreatedBy: LTHAI(19/11/2020)
    */
    ShowConfirmDeleteEmployee() {
        // Lấy recordId từ data của button
        let recordId = $('.icon-remove button').data('recordId');

        // Đưa ra cảnh báo xác nhận 
        //+ Lấy thông tin của bản ghi được xóa
        let url = this.host + this.apiRouter + `/${recordId}`;
        let res = this.GetDataOfAEmployee(url);
        let title = "Xóa bản ghi";
        let body = `Bạn có chắc chắn muốn xóa nhân viên ${res.FullName} (Mã nhân viên ${res.EmployeeCode}) không?`;
        // Gán id bản ghi cho nút xác nhận xóa
        $('#btn-delete').data('recordId', recordId);
        //Hiển thị thông báo xác nhận xóa
        ShowPopUp(title, body);
    }

    /**=====================================================
    * Hiển thị dialog khi nhấn nút thêm nhân viên
    * CreatedBy: LTHAI(15/11/2020)
    * */
    EventsWhenClickAddEmployee() {
        this.FormMode = "Add";
        datePickerDetail.datepicker();
        dialogDetail.dialog('open');
        // Lấy dữ liệu của phòng ban và chức vụ và xây dựng select
        this.InitEmployeeCode();
        this.GetDepartments();
        this.GetPositions();
    }

    /**=====================================================
    * Hàm lấy dữ liệu để truyền vào combobox phòng ban
    * @param {any} url đường dẫn được truyền
    * CreatedBy: LTHAI(1/12/2020)
    */
    GetDepartments() {
        let select = $("select#Department");
        select.empty();
        try {
            $.ajax({
                url: this.host + "/api/v1/departments",
                method: "GET",
                async: false
            }).done(function (res) {
                $.each(res, function (index, obj) {
                    select.append(`<option value ='${obj.DepartmentId}'>${obj.DepartmentName}</option>`)
                })
            }).fail(function (res) {
                debugger
            })
        } catch (e) {

        }
    }

    /**=====================================================
    * Hàm lấy dữ liệu để truyền vào combobox chức vụ
    * @param {any} url đường dẫn được truyền
    * CreatedBy: LTHAI(1/12/2020)
    */
    GetPositions() {
        let select = $("select#Position");
        select.empty();
        try {
            $.ajax({
                url: this.host + "/api/v1/positions",
                method: "GET",
                async: false
            }).done(function (res) {
                $.each(res, function (index, obj) {
                    select.append(`<option value ='${obj.PositionId}'>${obj.PositionName}</option>`)
                })
            }).fail(function (res) {
                debugger
            })
        } catch (e) {

        }
    }

    /**=====================================================
    * Hàm lấy dữ liệu để khởi tạo mã nhân viên
    * CreatedBy: LTHAI(1/12/2020)
    */
    InitEmployeeCode(url) {
        try {
            $.ajax({
                url: this.host + "/api/v1/employees/employeecodemax",
                method: "GET",
                async: false
            }).done(function (res) {
                $('#txtEmployeeCode').val(InitCode(res.EmployeeCode));
            }).fail(function (res) {
                debugger
            })
        } catch (e) {

        }
    }

    /**=====================================================
    * Hiển thị dialog khi nhấn 2 lần vào từng dòng trong bảng
    * @param {any} self đại diện cho đối tượng tr
    * CreatedBy: LTHAI(1/12/2020)
    */
    EventsWhenDoubleClickTr(self) {
        this.FormMode = "Edit";

        // Lấy khóa chính của bản ghi
        this.recordId = $(self).data('recordId');

        // Lấy dữ liệu của phòng ban và chức vụ và xây dựng select
        this.GetDepartments();
        this.GetPositions();

        // Gọi service lấy đối tượng khách hàng theo CustomerId
        let url = this.host + this.apiRouter + `/${this.recordId}`;
        let res = this.GetDataOfAEmployee(url);

        // Xây dựng form dựa trên thông tin sẵn có
        let inputs = $('input[bind-data], select[bind-data]');
        $.each(inputs, function (index, input) {
            let property = $(input).attr("bind-data");
            if ($(input).attr('dbType') == "date") {
                $(input).datepicker('setDate', new Date(res[property]));;
            } else if (property == "Salary") {
                $(input).val(formatMoney(res[property]));
            }
            else {
                $(input).val(res[property]);
            }

        })
        // Hiển thị dialog
        dialogDetail.dialog('open');
    }

    /**=====================================================
    * Hàm lấy ra nội dung của một nhân viên thông qua id
    * @param {any} url đường dẫn để lấy data từ service
    * CreatedBy: LTHAI(1/12/2020)
    */
    GetDataOfAEmployee(url) {
        let result;
        try {
            $.ajax({
                url: url,
                method: "GET",
                async: false
            }).done(function (res) {
                result = res;
            }).fail(function (res) {
                debugger;
                // Lấy dữ liệu không thành công
                $('.modal-body').text("Lấy dữ liệu không thành công !");
                $('#myModal').trigger('click');
            })
        } catch (e) {

        }
        return result;
    }
    /**
     * Lưu dữ liệu =============================================
     * CreatedBy: LTHAI(1/12/2020)
     * */
    SaveDataWhenClickButtonSave() {
        let thisHere = this;
        this.setApiRouter();
        // Validate dữ liệu
        let inputValidates = $('input[required], input[type ="email"]')
        $.each(inputValidates, function (index, input) {
            $(input).trigger('blur');
        })
        // Lấy các thẻ input có thuộc tính validated = false
        let inputNotValids = $('input[validated = "false"]');
        if (inputNotValids && inputNotValids.length > 0) {
            alert("Dữ liệu không hợp lệ vui lòng kiểm tra lại.");
            inputNotValids[0].focus();
            return;
        }
        // Xây dựng dữ liệu được lấy thành object
        // + Lấy các thẻ input,select trong dialog
        let inputs = $('input[bind-data], select[bind-data]');
        let entity = {};
        $.each(inputs, function (index, input) {
            // Lấy nội dung của thộc tính bind-data của từng thẻ input
            let property = $(input).attr("bind-data");
            if ($(input).attr("dbType") == "date") {
                entity[property] = $(input).datepicker('getDate');
            } else if ($(input).attr("bind-data") == "Salary") {
                entity[property] = ConvertMoneyToInt($(input).val());
            }
            else {
                entity[property] = $(input).val();
            }
        })
        // Kiểm tra xem nút lưu dùng để thêm hay cập nhật và tùy chỉnh phương thức của Resful 
        let method = "POST";
        if (thisHere.FormMode == "Edit") {
            method = "PUT";
            thisHere.apiRouter = `${thisHere.apiRouter}/${thisHere.recordId}`;
        }

        // Gọi service thực hiện lưu dữ liệu
        try {
            $.ajax({
                url: thisHere.host + thisHere.apiRouter,
                method: method,
                data: JSON.stringify(entity),
                contentType: 'application/json'
            }).done(function (res) {
                // Sau khi load dữ liệu thành công
                // + Hiện thị thông báo thêm thành công
                if (thisHere.FormMode == "Edit") {
                    $('.modal-body').text("Bạn đã cập nhật thành công !");
                } else {
                    $('.modal-body').text("Bạn đã thêm khách hàng thành công !");
                }

                $('#myModal').trigger('click');

                // + Đóng dialog
                RefreshDialog();
                dialogDetail.dialog('close');
                thisHere.setApiRouter();
                // + Load lại dữ liệu 
                thisHere.loadData();

            }).fail(function (res) {
                // + Hiện thị thông báo không thành công
                $('.modal-body').text(res.responseJSON.Data.Msg);
                $('.modal-content').css('background-color', '#FF4747');
                $('#myModal').trigger('click');
                let input = $(`input[bind-data = ${res.responseJSON.Data.fieldName}]`);
                $(input).focus();
               
               
            })
        } catch (e) {
            console.log(e);
        }
    }
    /**=================================================
    * Sau khi xác nhận xóa bản ghi thực hiện gọi service xóa
    * CreatedBy: LTHAI(19/11/2020)
    * */
    DeleteEmployeeRecord() {
        let thisHere = this;
        let recordId = $('.icon-remove button').data('recordId');
        // Gọi tới service để xóa bản ghi
        try {
            $.ajax({
                url: thisHere.host + thisHere.apiRouter + `/${recordId}`,
                method: "DELETE"
            }).done(function () {

                //Thông báo thành công
                $('.modal-body').text("Bạn đã xóa thành công !");
                $('#myModal').trigger('click');

                // Đóng pop-up
                $('.pop-up-cancel').trigger('click');

                // + Load lại dữ liệu 
                thisHere.loadData();
            }).fail(function () {

                //Thông báo không thành công
                $('.modal-body').text("Bạn đã xóa thất bại !");
                $('#myModal').trigger('click');
                // Đóng pop-up
                $('.pop-up-cancel').trigger('click');
            })
        } catch (e) {

        }
    }

    /**
     *  Khởi tạo giá trị cho các combobox phòng ban trên toolbar
     *  CreatedBy: LTHAI(2/12/2020)
     * */
    initValueDepartmentToolBar() {
        let buttonSelect = $('#data-department')
        buttonSelect.empty();
        try {
            $.ajax({
                url: this.host + "/api/v1/departments",
                method: "GET",
                async: false
            }).done(function (res) {
                $.each(res, function (index, obj) {
                    buttonSelect.append(`<a class="dropdown-item" filter="Department" data="${obj.DepartmentId}">${obj.DepartmentName}</a>`)
                })
            }).fail(function (res) {
                debugger
            })
        } catch (e) {

        }

    } 

     /**
     *  Khởi tạo giá trị cho các combobox chức vụ trên toolbar
     *  CreatedBy: LTHAI(2/12/2020)
     * */
    initValuePositionToolBar() {
        let buttonSelect = $('#data-position')
        buttonSelect.empty();
        try {
            $.ajax({
                url: this.host + "/api/v1/positions",
                method: "GET",
                async: false
            }).done(function (res) {
                $.each(res, function (index, obj) {
                    buttonSelect.append(`<a class="dropdown-item" filter="Position" data="${obj.PositionId}">${obj.PositionName}</a>`)
                })
            }).fail(function (res) {
                debugger
            })
        } catch (e) {

        }

    }
    /**===========================================================
    * Lọc theo giá trị bất kì
    * @param {any} self đối tượng button
    * CreatedBy: LTHAI(2/12/2020)
    */
    LoadDataByFilters() {
        this.apiRouter = `/api/v1/employees/filter?value=${this.value}&departmentid=${this.departmentId}&positionId=${this.positionId}`;
        this.loadData();
        this.setApiRouter();
    }
}




