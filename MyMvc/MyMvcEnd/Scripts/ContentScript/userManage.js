$(document).ready(function () {
    win = "userWin";
    currentForm = "userForm";
    dataGrid = "dg";
    $("#" + currentForm).validate({
        rules: {
            Name: "required",
            Telephone: {
                required: true,
                checkPhone:true
            }
        },
        messages: {
            Name: "请输入姓名",
            Telephone: {
                required: "手机不能为空",
                checkPhone: "手机号码无效"
            }
        }
    });
});

$.validator.addMethod("checkPhone", function (value, element) {
    var tel = /^(1)\d{10}$/;
    return this.optional(element) || (tel.test(value));
});

function formatter(value, row, index) {
    if (value == "0") {
        return "超级管理员";
    } else {
        return "普通管理员";
    }
}

function openAdminLog() {
    if (checkSelectedRow()) return;
    common.openNewWindow(common.getDomainUrl+"AdminLoginLog/Index?AdminUserID=" + dataKeyValue, "管理员登录日志",500,500);
}

function deliverAuthority()
{
    var currentUser = common.getCookie("LoginAdminUser");
    if (currentUser == null) {
        common.alertError("请重新登录！");
        return;
    }
    var AdminType = JSON.parse(currentUser).AdminType;
    if (AdminType == 1) {
        common.alertError("您没有足够的权限进行权限的分配！");
        return;
    }
    if (checkSelectedRow()) return;
    common.openNewWindow(common.getDomainUrl + "AdminAuthority/AdminUserAuthority?AdminUserID=" + dataKeyValue + "&status=deliver", "分配权限", 500, 500);
}

function readAuthority() {
    if (checkSelectedRow()) return;
    common.openNewWindow(common.getDomainUrl + "AdminAuthority/AdminUserAuthority?AdminUserID=" + dataKeyValue + "&status=read", "查看权限", 500, 500);
}


function alertSuccessFromIframe(msg) {
    common.alertCompleteMessage(msg);
};