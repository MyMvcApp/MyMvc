var common = {};

//网站域名用于弹出框页面路径的设置
common.getDomainUrl = "http://localhost:17399/";

common.callAjax = function (url, type, data, succFunc,errFunc) {
    var ajaxObj = {
        url: url,
        type: type,
        dataType:"json",
        contentType: "application/json; charset=utf-8",
        data: type=="GET"?data:JSON.stringify(data),
        success: function (msg) {
            succFunc(msg);
        },
        error: function (msg) {
            if (typeof (errFunc) == "function") errFunc();
            else {
                if (msg.Message != undefined) {
                    common.alertError(msg.Message);
                }
                else {
                    common.alertError("");
                }       
            }
        }
    };
    $.ajax(ajaxObj);
};

common.getJson= function (url, data, succFunc, errFunc) {
    common.callAjax(url, "GET", data, succFunc, errFunc);
};
common.operateJson = function (url, data, succFunc, errFunc) {
    common.callAjax(url, "POST", data, succFunc, errFunc);
};
common.delJson = function (url, data, succFunc, errFunc) {
    common.operateJson(url, data, succFunc, errFunc);
};
common.createJson = function (url, data, succFunc, errFunc) {
    common.operateJson(url, data, succFunc, errFunc);
};
common.updateData = function (url, data, succFunc, errFunc) {
    common.operateJson(url, data, succFunc, errFunc);
};
common.regexErrorMsg = function (str) {
    var reg = /<title>(.+)<\/title>/g;
    var msg = reg.exec(str)[1];
    if (msg != undefined) return ":" + msg;
    else return "";
};
common.clearValidateInfo = function () {
    $('.errorvalidator').each(function () {
        $(this).empty();
    });
};
common.checkError = function (operateVal, str) {
    if (operateVal == undefined) {
        common.alertError(str);
        return true;
    }
    return false;
};

common.alertError = function (str) {
    $.messager.alert('错误', "操作失败:" + str, 'error');
};

common.alertSuccess = function (str) {
    $.messager.alert('提示', str, 'success');
};

common.alertSuccessMsg = function () {
    $.messager.alert('提示', '操作成功', 'success');
};

common.alertErrorMsg = function () {
    common.alertError("");
};

common.alertCompleteMessage = function (msg) {
    if (msg.Status == "success") {
        common.alertSuccessMsg();
    } else {
        common.alertErrorMsg();
    }
};

//获取地址栏参数
common.getQueryString = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
};

//获取制定位数的随机数
common.RandNum = function(n){
    var rnd="";
    for(var i=0;i<n;i++)
        rnd+=Math.floor(Math.random()*10);
    return rnd;
};

//获取不重复的随机数
common.getRandNum = function(n){
    var time = new Date().getTime(); //当前毫秒
    var temp = time + common.RandNum(n); //毫秒+随机数
    return temp;
}

//以弹出框的形式打开一个新窗口
common.openNewWindow = function (url, title, width, height) {
    $("#openANewWindow").parent().next().next().remove();
    $("#openANewWindow").parent().next().remove();
    $("#openANewWindow").parent().remove();
    $("body").append("<div id='openANewWindow'><iframe scrolling='no' frameborder='0'  src='" + url + "' style='width:100%;height:100%;'></iframe></div>");
    $('#openANewWindow').window({
        width: width,
        height: height,
        closed:true,
        modal: true,
        title: title
    });
    $('#openANewWindow').window('open');
};

common.closeNewWindow = function () {
    $(window.parent.document).find("#openANewWindow").parent().hide();
    $(window.parent.document).find("#openANewWindow").parent().next().hide();
    $(window.parent.document).find("#openANewWindow").parent().next().next().hide();
};

//将/Date/格式的Date类型数据转成<yyyy-mm-dd>格式
common.formatDate = function (val) {
    if (val != null) {
        var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
        //月份为0-11，所以+1，月份小于10时补个0
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return date.getFullYear() + "-" + month + "-" + currentDate;
    }
    return "";
};

common.setCookie=function (key, value, expireDate) {
    if (expireDate == undefined) {
        expireDate = {};
    }
    if (expireDate.path == undefined)
        expireDate.path = "/";
    $.cookie(key, value, expireDate);
};
common.getCookie=function (key) {
    return $.cookie(key);
},
common.deleteCookie=function (key) {
    $.cookie(key, null, { path: "/" });
}

common.strToBase64 = function (arrStr) {
    var m = "";
    for (var i = 0; i < arrStr.length; i++) {//byte转字符串
        m += String.fromCharCode(arrStr[i]);
    }
    var mm = window.btoa(m);//转成base64位字符串
    return mm;
}
