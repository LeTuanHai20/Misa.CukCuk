$(document).ready(function () {
    // Khởi tạo đối tượng khách hàng
    new CustomerJS();
    
})
/**
 * Class quản lý sự kiện cho trang quản lý khách hàng
 * CreatedBy: LTHAI(12/11/2020)
 * */
class CustomerJS extends BaseJS {
    constructor() {
        super();
        initEventsCustomer();
    }
    /**
     * Hàm khởi tạo customer
     * */
    initEventsCustomer() {
        let me = this;
        //Hiển thị dialog khi nhấn 2 lần vào từng dòng trong bảng
        $('table tbody').on('dblclick', 'tr', function () {
            EventsWhenDoubleClickTr(this);
        }) 
        //Thêm mới dữ liệu khi ấn nút lưu trong dialog
        $('#d-btn-save').click(this.SaveDataWhenClickButtonSave.bind(me))

        // Xóa bản ghi khi click nút xóa 
        $('.icon button').click(me.ShowConfirmDeleteCustomer.bind(me))
        //Xác nhận xóa bản ghi
        $('#btn-delete').click(me.DeleteCustomerRecord.bind(me))
        //Hiển thị dialog khi nhấn nút thêm khách hàng
        $('#btn-add-customer').click(this.EventsWhenClickAddCustomer.bind(me))

    }
    /**
    * Quy định đường dẫn được sử dụng để lấy dữ liệu ở trang quản lý khách hàng
    * CreatedBy: LTHAI(12/11/2020)
    * */
    setApiRouter() {
        this.apiRouter = "/api/customers";
    }
/**
* Hiển thị dialog khi nhấn nút thêm khách hàng
* CreatedBy: LTHAI(15/11/2020)
* */
    EventsWhenClickAddCustomer() {
        this.FormMode = "Add";
        $('#d-dialog').css("display", "block");

        // Lấy dữ liệu của CustomerGroup và xây dựng combobox
        let urlGetData = this.host + "/api/customergroups";
        GetDataOfCustomerGroup(urlGetData)

        // focus vào mã khách hàng khi mở dialog
        $('#txtCustomerCode').focus();
    }
    /**
 * Hàm lấy dữ liệu để truyền vào combobox nhóm người dùng
 * @param {any} url đường dẫn được truyền 
 * CreatedBy: LTHAI(19/11/2020)
 */

    GetDataOfCustomerGroup(url) {
        let select = $("select#CustomerGroup");
        select.empty();
        try {
            $.ajax({
                url: url,
                method: "GET",
                async: false
            }).done(function (res) {
                $.each(res, function (index, obj) {
                    select.append(`<option value ='${obj.CustomerGroupId}'>${obj.CustomerGroupName}</option>`)
                })
            }).fail(function (res) {
                debugger
            })
        } catch (e) {

        }
    }
    /**
* Sự kiện khi click vào nút xóa thông tin của khách hàng
* CreatedBy: LTHAI(19/11/2020)
*/
    ShowConfirmDeleteCustomer() {
        // Lấy recordId từ data của button
        let recordId = $('.icon-remove button').data('recordId');

        // Đưa ra cảnh báo xác nhận 
        //+ Lấy thông tin của bản ghi được xóa
        let url = this.host + this.apiRouter + `/${recordId}`;
        let res = GetDataOfACustomer(url);
        let title = "Xóa bản ghi";
        let body = `Bạn có chắc chắn muốn xóa khách hàng ${res.FullName} (Mã khách hàng ${res.CustomerCode}) không?`;
        // Gán id bản ghi cho nút xác nhận xóa
        $('#btn-delete').data('recordId', recordId);
        //Hiển thị thông báo xác nhận xóa
        this.ShowPopUp(title, body);
    }
    /**
     * Lưu dữ liệu 
     * CreatedBy: LTHAI(18/11/2020)
     * */
    SaveDataWhenClickButtonSave() {
        let thisHere = this;
        // Validate dữ liệu
        let inputValidates = $('input[required], input[type = "email"]')
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
            // Nếu loại là radio và được checked thì set giá trị cho nó là 0 <Nữ> hoặc 1<Nam>
            if ($(this).attr('type') == "radio") {
                if (this.checked) {
                    if ($(input).attr('id') == "Male") {
                        entity[property] = 1;
                    } else {
                        entity[property] = 0;
                    }
                }
            } else {
                entity[property] = $(input).val();
            }

        })
        // Kiểm tra xem nút lưu dùng để thêm hay cập nhật và tùy chỉnh phương thức của Resful 
        let method = "POST";
        if (thisHere.FormMode == "Edit") {
            method = "PUT";
            entity.CustomerId = thisHere.recordId;
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
                // + Load lại dữ liệu 
                thisHere.loadData();

            }).fail(function (res) {
                debugger
            })
        } catch (e) {
            console.log(e);
        }
    }
    /**
* Sau khi xác nhận xóa bản ghi thực hiện gọi service xóa
* CreatedBy: LTHAI(19/11/2020)
* */
 DeleteCustomerRecord() {
    let thisHere = this;
    let recordId = $('.icon button').data('recordId');
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
        })
    } catch (e) {

    }
}
}
/**
* Hiển thị dialog khi nhấn 2 lần vào từng dòng trong bảng
* @param {any} self đại diện cho đối tượng tr
* CreatedBy: LTHAI(15/11/2020)
*/
function EventsWhenDoubleClickTr(self) {
    this.FormMode = "Edit";

    // Lấy khóa chính của bản ghi
    this.recordId = $(self).data('recordId');

    // Xây dựng nội dung của customerGroups
    let urlGetDataForCustomerGroup = this.host + "/api/customergroups";
    GetDataOfCustomerGroup(urlGetDataForCustomerGroup);

    // Gọi service lấy đối tượng khách hàng theo CustomerId
    let url = this.host + this.apiRouter + `/${this.recordId}`;
    let res = GetDataOfACustomer(url);

    // Xây dựng form dựa trên thông tin sẵn có
    let inputs = $('input[bind-data], select[bind-data]');
    $.each(inputs, function (index, input) {
        let property = $(input).attr("bind-data");

        if ($(input).attr('type') == "radio") {
            if (res[property] == 1) {
                $('#Male').prop("checked", true);
            } else if (res[property] == 0) {
                $('#feMale').prop("checked", true);
            }
        } else if ($(input).attr('type') == "date") {
            let yyyyMMdd = formatDateOfBirthyyyyMMdd(res[property]);
            $(input).val(yyyyMMdd);
        }
        else {
            $(input).val(res[property]);
        }

    })

    // Hiển thị dialog
    $('#d-dialog').css("display", "block");

    // focus vào mã khách hàng khi mở dialog
    $('#txtCustomerCode').focus();

}


/**
 * Hàm lấy ra nội dung của một khách hàng thông qua id
 * @param {any} url đường dẫn để lấy data từ service
 * CreatedBy: LTHAI(19/11/2020)
 */
function GetDataOfACustomer(url) {
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




