$(function () {
    //InitMenu();
    tabClose();
    tabCloseEven();
    InitLeftMenu();
});

function InitMenu() {
    var adminUser = common.getCookie("LoginAdminUser");
    if (adminUser == null) {
        common.alertError("请重新登录！");
        return;
    }
    var adminUserID = JSON.parse(adminUser).ID;
    var url = common.getDomainUrl+"AdminAuthority/GetLoginAuthorityList";
    common.getData(url, { ID: parseInt(adminUserID)}, function (data) {
        var accordion = new Array();
        var oldData = data.rows;
        $.each(oldData, function (index, obj) {
            if ($.inArray(obj.AdminModuleID, accordion) == -1) {
                accordion.push(obj.AdminModuleID);
            }
        });
        $.each(accordion, function (index, obj) {
            var menuHtml = "<ul>";
            var AdminModuleName = "";
            for (var d in oldData) {
                if (oldData[d].AdminModuleID == obj) {
                    menuHtml += "<li><div><a href='javascript:void(0);' rel=" + oldData[d].AdminAuthorityUrl + " ref='12'>";
                    menuHtml += "<span class='icon icon-user'>&nbsp;</span>";
                    menuHtml += "<span class='nav'>" + oldData[d].AdminAuthorityName + "</span></a></div></li>";
                    AdminModuleName = oldData[d].AdminModuleName;
                }
            }
            menuHtml += "</ul>";
            $('#nav').accordion('add', {
                title: AdminModuleName,
                iconCls:'icon-sys icon',
                content: menuHtml
            });
        });
        //因为accordion菜单栏绘到界面上会有一点时间的延迟，所以这里加了延迟注册事件处理
        //setTimeout(function () { InitLeftMenu();}, 1000);
    });
   
}

    //初始化左侧
    function InitLeftMenu() {
        $("#nav").accordion({ animate: false });
        $('.easyui-accordion div ul li div a').click(function () {
            var tabTitle = $(this).children('.nav').text();

            var url = $(this).attr("rel");
            var menuid = $(this).attr("ref");
            var icon = getIcon(menuid, icon);

            addTab(tabTitle, url, icon);
            $('.easyui-accordion li div').removeClass("selected");
            $(this).parent().addClass("selected");
        }).hover(function () {
            $(this).parent().addClass("hover");
        }, function () {
            $(this).parent().removeClass("hover");
        });

        //选中第一个
        var panels = $('#nav').accordion('panels');
        var t = panels[0].panel('options').title;
        $('#nav').accordion('select', t);
    }
    //获取左侧导航的图标
    function getIcon(menuid) {
        return 'icon icon-user';
    }

    function addTab(subtitle, url, icon) {
        if (!$('#tabs').tabs('exists', subtitle)) {
            var content = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
            $('#tabs').tabs('add', {
                title: subtitle,
                content:content,
                closable: true,
                icon: icon
            });
        } else {
            $('#tabs').tabs('select', subtitle);
            $('#mm-tabupdate').click();
        }
        tabClose();
    }

    function tabClose() {
        /*双击关闭TAB选项卡*/
        $(".tabs-inner").dblclick(function () {
            var subtitle = $(this).children(".tabs-closable").text();
            $('#tabs').tabs('close', subtitle);
        })
        /*为选项卡绑定右键*/
        $(".tabs-inner").bind('contextmenu', function (e) {
            $('#mm').menu('show', {
                left: e.pageX,
                top: e.pageY
            });

            var subtitle = $(this).children(".tabs-closable").text();

            $('#mm').data("currtab", subtitle);
            $('#tabs').tabs('select', subtitle);
            return false;
        });
    }
    //绑定右键菜单事件
    function tabCloseEven() {
        //刷新
        $('#mm-tabupdate').click(function () {
            var currTab = $('#tabs').tabs('getSelected');
            var url = currTab.panel('options').href;
            $('#tabs').tabs('update', {
                tab: currTab,
                options: {
                    href: url
                }
            })
        })
        //关闭当前
        $('#mm-tabclose').click(function () {
            var currtab_title = $('#mm').data("currtab");
            $('#tabs').tabs('close', currtab_title);
        })
        //全部关闭
        $('#mm-tabcloseall').click(function () {
            $('.tabs-inner span').each(function (i, n) {
                var t = $(n).text();
                $('#tabs').tabs('close', t);
            });
        });
        //关闭除当前之外的TAB
        $('#mm-tabcloseother').click(function () {
            $('#mm-tabcloseright').click();
            $('#mm-tabcloseleft').click();
        });
        //关闭当前右侧的TAB
        $('#mm-tabcloseright').click(function () {
            var nextall = $('.tabs-selected').nextAll();
            if (nextall.length == 0) {
                //$.messager.alert('系统提示','后边没有啦~~','error');
                return false;
            }
            nextall.each(function (i, n) {
                var t = $('a:eq(0) span', $(n)).text();
                $('#tabs').tabs('close', t);
            });
            return false;
        });
        //关闭当前左侧的TAB
        $('#mm-tabcloseleft').click(function () {
            var prevall = $('.tabs-selected').prevAll();
            if (prevall.length == 0) {
                return false;
            }
            prevall.each(function (i, n) {
                var t = $('a:eq(0) span', $(n)).text();
                $('#tabs').tabs('close', t);
            });
            return false;
        });

        //退出
        $("#mm-exit").click(function () {
            $('#mm').menu('hide');
        })
    }