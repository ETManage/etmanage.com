
var _menus ;
$(function () {
    $('#loginOut').click(function () {
        $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {

            if (r) {
                $.get("/Account/AjaxLogout", {}, function (data, textStatus) { location.href = '/Account/Login'; }, 'html');

            }

        });
    })

    $.get("/System/AjaxQueryMenusList", {}, function (data, textStatus) {
        _menus = JSON.parse(data);
        InitLeftMenu();
        tabClose();
        tabCloseEven();
    }, 'html');
 
});







/*
创建人：West
时间：2015/3/2
作用：云朵浮动效果
*/
var $main = $cloud = mainwidth = null;
var offset1 = 450;
var offset2 = 0;

var offsetbg = 0;

function startFloatBG() {
    $main = $("#mainBody");
    $body = $("body");
    $cloud1 = $("#cloud1");
    $cloud2 = $("#cloud2");
    mainwidth = $main.outerWidth();
    /// 飘动
    setInterval(function flutter() {
        if (offset1 >= mainwidth) {
            offset1 = -580;
        }

        if (offset2 >= mainwidth) {
            offset2 = -580;
        }

        offset1 += 1.1;
        offset2 += 1;
        $cloud1.css("background-position", offset1 + "px -100px")

        $cloud2.css("background-position", offset2 + "px 0px")
    }, 70);


    setInterval(function bg() {
        if (offsetbg >= mainwidth) {
            offsetbg = -580;
        }

        offsetbg += 0.9;
        $body.css("background-position", -offsetbg + "px 0")
    }, 90);
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

//初始化左侧
function InitLeftMenu() {
    $("#nav").accordion({ animate: false }); //为id为nav的div增加手风琴效果，并去除动态滑动效果
    $.each(_menus.menus, function (i, n) {//$.each 遍历_menu中的元素
        var menulist = '';
        menulist += '<ul style="padding:3px;">';
        $.each(n.menus, function (j, o) {
            menulist += '<li><div><a ref="' + o.menuid + '" href="#" rel="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span><span class="nav">' + o.menuname + '</span></a></div></li> ';
        })
        menulist += '</ul>';

        $('#nav').accordion('add', {
            title: n.menuname,
            content: menulist,
            iconCls: 'icon ' + n.icon
        });

    });

    $('.easyui-accordion li a').click(function () {//当单击菜单某个选项时，在右边出现对用的内容
        var tabTitle = $(this).children('.nav').text(); //获取超链里span中的内容作为新打开tab的标题

        var url = $(this).attr("rel");
        var menuid = $(this).attr("ref"); //获取超链接属性中ref中的内容
        var icon = getIcon(menuid, icon);

        addTab(tabTitle, url, icon); //增加tab
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
