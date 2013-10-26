/*--全局变量--*/
var dataGrid = "dg";//当前页面Grid对应的id
var currentForm = "dataForm";//当前页面对应的Form的id
var win = "alertWin";//新增和编辑的弹出框对应的id
var dataKeyValue = undefined; // 数据主键对应的值
var selectRow = undefined;//当前选中行数据对象
var rowIndex = undefined; //当前操作行索引

/*
    beforeAdd用于设置在新增之前自定义的校验
    beforeEdit用于设置在编辑之前自定义的校验
    beforeDel用于设置在删除之前自定义的校验
*/
//点击当前行事件
function onClickRow(index) {
    setRowData();
    $('#' + dataGrid).datagrid('getRowIndex', index);
    rowIndex = index;
}

//新增对应的方法，setDefaultValue用于设置新增之前的默认值
function addData() {
    if (typeof (beforeAdd) == "function") {
        if (!beforeAdd()) return;
    } 
    common.clearValidateInfo();
    $("#ID").remove();
    $("#" + currentForm).form('clear');
    if (typeof (setDefaultValue) == "function") setDefaultValue();
    $('#' + win).window('open');
}

//编辑对应的方法
function editData() {
    if (checkSelectedRow()) return;
    if (typeof (beforeEdit) == "function") {
        if (!beforeEdit()) return;
    } 
    common.clearValidateInfo();
    $("#ID").remove();
    $("#" + currentForm).append('<input type="hidden" value="' + dataKeyValue + '"id="ID" name="ID">');
    $("#" + currentForm).form('clear');
    selectRow.RowVersion = common.strToBase64(selectRow["RowVersion"]);
    $("#" + currentForm).form('load', selectRow);
    $('#' + win).window('open');
}
//删除数据对应的方法,beforeDel对应的是删除之前要进行校验的方法
function delData() {
    if (checkSelectedRow()) return;
    var delData = selectRow;
    if (delData == null) {
        common.alertError("删除对象不能为null值");
        return;
    }
    if (typeof (beforeDel) == "function") {
        if (!beforeDel()) return;
    }
    $.messager.confirm('提醒', '确定要删除此项吗?', function (r) {
        if (!r) return;
        common.delJson("Delete", delData, function (msg) {
            if (msg.Status == "success") {
                $('#' + dataGrid).datagrid('deleteRow', rowIndex);
                clearCacheData();
                common.alertSuccessMsg();
            } else {
                common.alertError(msg.Message);
            }
        });
    });
}

function checkSelectedRow() {
    setRowData();
    return common.checkError(dataKeyValue, '当前未选中行');
}

//设置当前选择行的数据
function setRowData(){
    selectRow = $('#' + dataGrid).datagrid('getSelected');
    if (selectRow != undefined) dataKeyValue = selectRow["ID"];
    else dataKeyValue = undefined;
}

function clearCacheData() {
    dataKeyValue = selectRow = rowIndex = undefined;
}
//Form提交操作成功的处理
function success(data) {
    if (data.Status == "success") {
        common.alertSuccessMsg();
        $('#' + win).window('close');
        $('#' + dataGrid).datagrid('reload');
        clearCacheData();
    } else {
        common.alertError(data.Message);
    }
}
//Form提交操作失败的处理
function failure(data) {
    common.alertError(data.Message);
}

function submitForm() {
    $("#" + currentForm).submit();
}

function clearForm() {
    $("#" + currentForm).form('clear');
}