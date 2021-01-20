/**
 * Định dạng ngày tháng theo chuẩn ngày/tháng/năm
 * @param {any} date dữ liệu thời gian 
 * CreatedBy: LTHAI (11/11/2020)
 * EditBy: LTHAI(12/11/2020) : Kiểm tra dữ liệu đầu vào 
 * nếu là string, NaN thì trả về giá trị rỗng
 */
function formatDateOfBirth(date) {
    date = new Date(date);
    if (Number.isNaN(date.getTime())) {
        return "";
    } else {
        let day = date.getDate()
        day = day < 10 ? ("0" + day) : day;
        let month = date.getMonth() + 1;
        month = month < 10 ? ("0" + month) : month;
        let year = date.getFullYear();
        return `${day}/${month}/${year}`;
    }
}

/**
 * Định dạng tiền (dạng 2500 => 2.500)
 * @param {any} money dữ liệu số tiền
 *  CreatedBy: LTHAI (11/11/2020)
 */
function formatMoney(money) {
    if (money == null || typeof (money) == "string" || Number.isNaN(money)) {
        return "";
    } else {
        return money.toFixed(0).replace(/(\d)(?=(\d{3})+\b)/g, '$1.');
    }
}

/**
 * Làm mới lại trang dialog
 * CreatedBy: LTHAI (15/11/2020)
 * EditedBy: LTHAI(20/11/2020)
 * Làm mới thêm trường hợp radio button
 * */
function RefreshDialog() {

    let inputs = $('#d-dialog input,#d-dialog select');
    $.each(inputs, function (index, input) {
        $(input).val('');
        $(input).removeAttr('validated');
        $(input).removeClass("border-red");
    })
    
}

/**
* Đưa ra cảnh báo cho những sự kiện cần xác nhận
* @param {any} title Thông tin tiêu đề
* @param {any} body Nội dung của cảnh báo
* CreatedBy: LTHAI(19/11/2020)
*/
function ShowPopUp(title, body) {
    $('.pop-up-title').text(title);
    $('.pop-up-inf').text(body);
    $('.pop-up-modal').css('display', 'block');
    $('.p-pop-up').css('display', 'block');
}

/**
* Tắt pop-up
* CreatedBy: LTHAI(19/11/2020)
* */
function ClosePopUp() {
    $('.p-pop-up').css('display', 'none');
    $('.pop-up-modal').css('display', 'none');
}

/**
* Hiển thị thông báo
* CreatedBy: LTHAI(18/11/2020)
* */
function ShowModal() {
    $("#staticBackdrop").modal({ backdrop: false });
    setTimeout(function () {
        $('#staticBackdrop').modal('hide');
    }, 1500);
}

/**
 * Tự tạo ra một mã mới
 * @param {any} code mã có giá trị lớn nhất trong dữ liệu
 * CreatedBy: LTHAI(2/12/2020)
 */
function InitCode(code) {
    let codes = code.split("");
    let length = code.length;
    let lastChar = codes[`${length - 1}`];
    let lastnumber = parseInt(lastChar);
    if (Number.isInteger(lastnumber)) {
        codes[`${length - 1}`] = lastnumber + 1;
    }
    else {
        codes[`${length - 1}`] = lastChar + 1;
    }
    code = codes.join('');
    return code
}

/**
* chuyển đổi định dạng tiền sang int
* @param {any} money số tiền 
*  CreatedBy: LTHAI(2/12/2020)
* */
function ConvertMoneyToInt(money) {
    let int = money.replaceAll('.', '')
    return int;
}
