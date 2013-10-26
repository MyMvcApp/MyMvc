var AdminUserID = common.getQueryString("AdminUserID");
var status = common.getQueryString("status");
$(document).ready(function () {
    if (AdminUserID == null || AdminUserID == "") {
        common.alertError("参数无效！");
        return;
    } 
    var columns =[[
           { field: 'ID', width: 80, sortable: true, title: 'ID', checkbox: false },
           { field: 'AdminModuleName', width: 80, sortable: true, title: '模块名称' },
           { field: 'AdminAuthorityName', width: 80, sortable: true, title: '权限名称' },
           { field: 'AdminAuthorityUrl', width: 100, sortable: true, title: '权限地址' },
           { field: 'AdminAuthorityOrder', width: 100, sortable: true, title: '权限顺序' }
    ]];
    if (status == "read") {
        $("#tb").hide();
        $('#dg').datagrid({
            url: 'GetAdminUserAuthorityList',
            queryParams: {
                AdminUserID: AdminUserID
            },
            columns: columns
        });
        
    }
    else if (status == "deliver") {
        $("#tb").show();
        columns[0][0].checkbox = true;
        $('#dg').datagrid({
            url: 'GetAllAuthority',
            columns: columns
        });
    }
    if (status != "deliver")
    $("#tb").hide();
});

function confirmDeliver() {
    if (AdminUserID == null || AdminUserID == "") {
        common.alertError("参数无效！");
        return;
    }
    var data = $('#dg').datagrid('getSelections');
    if (data.length == 0)
    {
        common.alertError("没有行数据选中！");
        return;
    }

    var adminUserAuthority = {ID: AdminUserID, AdminAuthoritys: data };
    common.updateData("DeliverAuthority", adminUserAuthority,
        function (msg) {
            common.closeNewWindow();
            parent.alertSuccessFromIframe(msg);
    });
}
