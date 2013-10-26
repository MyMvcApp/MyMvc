$(document).ready(function () {
    win = "userWin";
    currentForm = "PagedPeoPleForm";
    $("#" + currentForm).validate({
        rules: {
            Name: "required",
        },
        messages: {
            Name: "请输入姓名",
        }
    });
});

function formatter(value, row, index) {
    if (value == false) {
        return "女";
    } else {
        return "男";
    }
}

function setDefaultValue() {
    $("#age").val(10);
    $("#sex1").attr("checked", "checked");
}





