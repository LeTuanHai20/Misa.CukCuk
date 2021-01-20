/**========================================================
 * Class chung để quản lý các sự kiện chung của chương trình
 * CreatedBy: LTHAI(12/11/2020)
 * */
class BaseJS {
    constructor() {
        this.host = "https://localhost:44339";
        this.apiRouter = null;
        this.setApiRouter();
        this.loadData();
        this.initEvents();
    }
    /**========================================================
     * Quy định dữ liệu được sử dụng để lấy dữ liệu
     * CreatedBy: LTHAI(12/11/2020)
     * */
    setApiRouter() {

    }

    /**========================================================
     * Khởi tạo các sự kiện được thực hiện trong trang
     * CreatedBy: LTHAI(1/12/2020)
     * */
    initEvents() {
        // Khởi tạo con trỏ this của Employee
        let base = this;
        //Hiển thị dialog khi nhấn 1 lần vào từng dòng trong bảng
        $('table tbody').on('click', 'tr', function () {
            base.EventsWhenClickTr(this);
        })

        //Load lại dữ liệu
        $('#btn-refresh').click(this.EventsWhenClickLoadData.bind(this))

        //Kiểm tra giá trị input bắt buộc nhập
        $('input[required]').blur(function () {
            base.EventsValidateRequiredWhenInputBlur(this);
        })

         //Chuyển đổi kiểu
        $('input[Money]').blur(function () {
            base.ConvertToMoney(this);
        })

        //Kiểm tra định dạng của email
        $("input[type = 'email']").blur(function () {
            base.EventsValidateEmailWhenInputBlur(this);
        })

        //Hiển thị thông báo 
        $('#myModal').click(ShowModal);

        // Đóng popup 
        $('.pop-up-cancel').click(ClosePopUp)
        
    }

    /**========================================================
     * Thực hiện load dữ liệu
     * CreatedBy: LTHAI(12/11/2020)
     **/
    loadData() {
        // Xóa thông tin trong table khi load lại nội dung
        $("#render-data").empty();
        $('table tbody').empty();
        // Loader 
        $(".loader").css('display', "block");
        // Lấy thông tin các cột dữ liệu
        let columns = $('table thead tr th');
        try {
            $.ajax({
                url: this.host + this.apiRouter,
                method: "GET"
            }).done(function (data) {
                // Tắt icon load dữ liệu
                $(".loader").css('display', "none");

                // - Hiển thị số bản ghi
                let length = data.length;
                $('#total').text(`${length}`)
                $.each(data, function (index, obj) {
                    let tr = $(`<tr></tr>`);
                    $(tr).data("recordId", obj.EmployeeId);

                    // Lấy thông tin dữ liệu sẽ map tương ứng với các cột
                    $.each(columns, function (index, item) {
                        let td = $(`<td><span></span></td>`);
                        let fieldName = $(item).attr("fieldName");
                        let value = obj[fieldName];

                        //Lấy thông tin các cột có thuộc tính formatType để format dữ liệu theo chuẩn
                        let formatType = $(item).attr("formatType");

                        // Duyệt qua từng trường hợp 
                        switch (formatType) {
                            case "ddmmyyyy": {
                                td.addClass("text-align-center");
                                value = formatDateOfBirth(value);
                                break;
                            }
                            case "Money": {
                                td.addClass("text-align-right");
                                value = formatMoney(value);
                                break;
                            }
                            default: {
                                break;
                            }
                        }

                        // Thêm nội dung vào thẻ tr trong tbody
                        td.find('span').append(value);
                        td.attr('title', value);
                        $(tr).append(td);
                    })
                    $("#render-data").append(tr);
                })
            }).fail(function (res) {
                $(".loader").css('display', "none");
                //Thông báo không thành công
                $('.modal-body').text("Không tìm thấy dữ liệu!");
                $('#myModal').trigger('click');
                // Đóng pop-up
                $('.pop-up-cancel').trigger('click');

            })
        } catch (e) {
            alert("Can't get data");
            console.log(e);
        }
    }

    /**========================================================
    * Load lại dữ liệu
    * CreatedBy: LTHAI(1/12/2020)
    * */
    EventsWhenClickLoadData() {
        $('#button-search-department').text('Tất cả phòng ban');
        $('#button-search-department').append('<i class="fas fa-caret-down"></i>');
        $('#button-search-position').text('Tất cả vị trí');
        $('#button-search-position').append('<i class="fas fa-caret-down"></i>');
        this.loadData();
    }

    /** ========================================================
    * Hiển thị dialog khi nhấn 1 lần vào từng dòng trong bảng
    * @param {any} self đại diện cho đối tượng tr
    *  CreatedBy: LTHAI(1/12/2020)
    * */
    EventsWhenClickTr(self){
        if ($(self).hasClass('active')) {
            $(self).removeClass('active');
            $('.icon-remove').find('button').css('display', 'none');
        } else {
            $('tr').removeClass('active');
            $(self).addClass('active');
            $('.icon-remove').find('button').css('display', 'block');
            $('.icon-remove').find('button').data('recordId', $(self).data('recordId'));
        }
    }

    /**========================================================
     * Kiểm tra trường bắt buộc nhập
     * @param {any} seft dại diện cho đối tượng input,select
     * CreatedBy: LTHAI(1/12/2020)
     */
    EventsValidateRequiredWhenInputBlur(seft) {
        let inScope = seft;
        let value = $(inScope).val();
        if (!value) {
            $(inScope).addClass("border-red");
            $(inScope).attr('title', `${$(inScope).attr('name')} không được để trống`)
            $(inScope).attr("validated", false);
        } else {
            $(inScope).removeClass("border-red");
            $(inScope).attr("validated", true);
        }
    }
    /**
    * Kiểm tra định dạng email
    * @param {any} self đại diện cho đối tượng input,select
    *  CreatedBy: LTHAI(1/12/2020)
    * */
    EventsValidateEmailWhenInputBlur(self) {
        let value = $(self).val();
        let regexEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
        if (!regexEmail.test(value)) {
            $(self).addClass('border-red');
            $(self).attr('title', 'Email không đúng định dạng.');
            $(self).attr("validated", false);
        } else {
            $(self).removeClass('border-red');
            $(self).attr("validated", true);
        }
    }  
    /**
     * Định dạng số tiền khi sự kiện blur
     * @param {any} seft đại diện cho đối tượng input
     * CreatedBy: LTHAI(2/12/2020)
     */
    ConvertToMoney(seft) {
        let value = $(seft).val();
        let valueInt = parseInt(value);
        if (Number.isInteger(valueInt)) {
            $(seft).val(formatMoney(valueInt));
        } else if (value != '') {
            $(seft).focus();
            alert("Số tiền không đúng định dạng !")
        }
    }
}
