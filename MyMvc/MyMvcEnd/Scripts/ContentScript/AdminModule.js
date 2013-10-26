$(document).ready(function () {
    win = "AdminModuleWin";
    currentForm = "AdminModuleForm";

    $("#" + currentForm).validate({
        rules: {
            AdminModuleName: "required"
        },
        messages: {
            AdminModuleName: "请输入姓名!"
        }
    });
});

function openAdminAuthority() {
    if (checkSelectedRow()) return;
    common.openNewWindow(common.getDomainUrl+"AdminAuthority/Index?AdminModuleID=" + dataKeyValue, "模块对应的权限", 500, 480);
}
