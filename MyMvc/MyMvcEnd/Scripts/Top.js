$(document).ready(function () {
    $('#updatePwdForm').validate();
});
function updatepwd() {
    $('#updatePwdForm').form('clear');
    $('#updatePwdWin').window('open');
}
function exitsys() {
    $.messager.confirm('提醒', '确定要退出系统吗?', function (r) {
            if (!r) return;
            common.callAjax('Account/Logout', "POST", null,
                function (msg) {
                    if (msg.Status == "success") {
                        window.location.href = "Account/Login";
                    } else {
                        $.messager.alert('错误', '退出失败:' + msg.Message, 'error');
                    }
                },
                function (msg) {
                    $.messager.alert('错误', '退出失败' + RegexErrorMsg(msg.responseText), 'error');
                });
    });
}
function submitForm() {
    $('#updatePwdForm').submit();
}
function clearForm() {
    $('#updatePwdForm').form('clear');
}
function success(data) {
    if (data.Status == "success") {
        $.messager.alert('提示', '操作成功', 'success');
        $('#updatePwdWin').window('close');
    } else {
        $.messager.alert('提示', '操作失败:' + data.Message, 'error');
    }
}
function failure(msg) {
    $.messager.alert('提示', '操作失败' + common.regexErrorMsg(msg.responseText), 'error');
}

