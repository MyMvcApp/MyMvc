$(document).ready(function () {
    win = "AdminAuthorityWin";
    currentForm = "AdminAuthorityForm";
    var AdminModuleID = common.getQueryString("AdminModuleID");
    if (AdminModuleID == null) AdminModuleID = "";
    $('#dg').datagrid({
        url: 'GetDataListByID',
        queryParams: {
            id: AdminModuleID
        }
    });

    $("#" + currentForm).validate({
        rules: {
            AdminAuthorityName: "required",
            AdminAuthorityUrl: "required"
        },
        messages: {
            AdminAuthorityName: "请输入姓名!",
            AdminAuthorityUrl:"请输入权限地址!"
        }
    });
    if (AdminModuleID != "") {
        $("#tb").hide();
    }
});
