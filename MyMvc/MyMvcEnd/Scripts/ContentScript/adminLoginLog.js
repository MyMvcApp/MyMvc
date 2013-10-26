$(document).ready(function () {
    var AdminUserID = common.getQueryString("AdminUserID");
    if (AdminUserID == null) AdminUserID = "";
    $('#dg').datagrid({
        url: 'GetDataListByID',
        queryParams: {
            id: AdminUserID
        }
    });
});

function formatter(value, row, index) {
    if (value != null) {
        return common.formatDate(value);
    }
    return "";
}