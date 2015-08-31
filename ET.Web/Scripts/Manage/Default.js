
var _menus;
$(function () {


    $.post("/System/AjaxQueryMenusList", {}, function (data, textStatus) {
        _menus = JSON.parse(data);
        // 导航菜单绑定初始化
        $("#wnav").accordion({
            animate: false
        });
        addNav(_menus.menus); //首次加载basic 左侧菜单
        InitLeftMenu();
        tabClose();
        tabCloseEven();

    }, 'html');

});


function IndexOut()
{
    $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {

        if (r) {
            $.get("/mAccount/AjaxLogout", {}, function (data, textStatus) { location.href = '/manage/login'; }, 'html');

        }

    });
}
















/*
创建人：West
时间：2015/3/2
作用：页面的左侧栏目和选项卡进行操作
*/
$(function () {
    //    InitLeftMenu();
    //    tabClose();
    //    tabCloseEven();
})



function Clearnav() {
    var pp = $('#wnav').accordion('panels');

    $.each(pp, function (i, n) {
        if (n) {
            var t = n.panel('options').title;
            $('#wnav').accordion('remove', t);
        }
    });

    pp = $('#wnav').accordion('getSelected');
    if (pp) {
        var title = pp.panel('options').title;
        $('#wnav').accordion('remove', title);
    }
}

function GetMenuList(data, menulist) {
    if (data.menus == null)
        return menulist;
    else {
        menulist += '<ul>';
        $.each(data.menus, function (i, sm) {
            if (sm.menus.length == 0 && sm.url != null && sm.url != "") {
                menulist += '<li ><a ref="' + sm.menuid + '" href="#" rel="'
					+ sm.url + '" ><div class="nav">' + sm.menuname
					+ '</div></a>'
            }
            else {
                menulist += '<li state="closed"><span class="nav">' + sm.menuname + '</span>'
            }
            menulist = GetMenuList(sm, menulist);
        })
        menulist += '</ul>';
    }
    return menulist;
}

function SelectTextExpand(obj, node) {
    if (node.state == 'closed') $(obj).tree("expand", node.target); else $(obj).tree("collapse", node.target);
}
//左侧导航加载
function addNav(data) {

    $.each(data, function (i, sm) {
        var menulist1 = "";
        //sm 常用菜单  邮件 列表
        menulist1 = GetMenuList(sm, menulist1);
        menulist1 = "<ul  class='easyui-tree ' animate='false' dnd='false' data-options='onSelect:function(node){  SelectTextExpand(this,node)  }'>" + menulist1.substring(4);
        $('#wnav').accordion('add', {
            title: sm.menuname,
            content: menulist1,
            iconCls: 'icon ' + sm.icon
        });

    });

    var pp = $('#wnav').accordion('panels');
    var t = pp[0].panel('options').title;
    $('#wnav').accordion('select', t);

}

// 初始化左侧
function InitLeftMenu() {

    hoverMenuItem();

    $('#wnav li a').click(function () {//当单击菜单某个选项时，在右边出现对用的内容
        var tabTitle = $(this).children('.nav').text(); //获取超链里span中的内容作为新打开tab的标题
        var url = $(this).attr("rel");
        var menuid = $(this).attr("ref"); //获取超链接属性中ref中的内容
        var icon = getIcon(menuid, icon);

        addTab(tabTitle, url, icon); //增加tab
        $('#wnav li div').removeClass("selected");
        $(this).parent().addClass("selected");
    }).hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });



}

/**
* 菜单项鼠标Hover
*/
function hoverMenuItem() {
    $(".easyui-accordion").find('a').hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });
}
//获取左侧导航的图标
function getIcon(menuid) {
    var icon = 'icon ';
    $.each(_menus.menus, function (i, n) {
        $.each(n.menus, function (j, o) {
            if (o.menuid == menuid) {
                icon += o.icon;
            }
        })
    })

    return icon;
}

function addTab(subtitle, url, icon) {
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            icon: icon
        });
    } else {
        $('#tabs').tabs('select', subtitle);
        $('#menu-tabupdate').click();
    }
    tabClose();
}

function createFrame(url) {
    var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
    return s;
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    })
    /*为选项卡绑定右键*/

    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#TabMenuStrip').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children(".tabs-closable").text();

        $('#TabMenuStrip').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
    $(".icon-tabs").click(function (e) {
        $('#TabMenuStrip').menu('show', {
            left: e.pageX,
            top: e.pageY
        });
    });
}
//绑定右键菜单事件
function tabCloseEven() {
    //刷新
    $('#menu-tabupdate').click(function () {
        var currTab = $('#tabs').tabs('getSelected');
        var url = $(currTab.panel('options').content).attr('src');
        $('#tabs').tabs('update', {
            tab: currTab,
            options: {
                content: createFrame(url)
            }
        })
    })
    //关闭当前
    $('#menu-tabclose').click(function () {
        var currtab_title = $('#TabMenuStrip').data("currtab");
        $('#tabs').tabs('close', currtab_title);
    })
    //全部关闭
    $('#menu-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            $('#tabs').tabs('close', t);
        });
    });
    //关闭除当前之外的TAB
    $('#menu-tabcloseother').click(function () {
        $('#menu-tabcloseright').click();
        $('#menu-tabcloseleft').click();
    });
    //关闭当前右侧的TAB
    $('#menu-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            //msgShow('系统提示','后边没有啦~~','error');
            alert('后边没有啦~~');
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#menu-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            alert('到头了，前边没有啦~~');
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });

    //退出
    $("#menu-exit").click(function () {
        $('#TabMenuStrip').menu('hide');
    })
}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
    $.messager.alert(title, msgString, msgType);
}
