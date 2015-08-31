$(function () {
    loadMenu();

    $('#menu-search-input').autocomplete({
        serviceUrl: '/system/AjaxSearchModule', width: 165, onSelect: function (event, item) {
            addTablePage($('#imanagemenu').find("[data-id='" + item.FuncID + "']"));
        }
    });
    $("#iuserlogout").click(function () {
        bootbox.confirm("确定要退出?", function (result) {
            if (result) {
                $.get("/mAccount/AjaxLogout", {}, function (data, textStatus) { location.href = '/manage/login'; }, 'html');
            }
        });
    });
});
/*
创建人：West
时间：2015/3/2
作用：页面的左侧栏目
*/
var menulist = "";
function loadMenu() {
    $.post("/System/AjaxQueryMenusList", {}, function (data, textStatus) {
        var _menus = JSON.parse(data);
        GetMenuList1(_menus.menus); //首次加载basic 左侧菜单
        $(".main-tab.nav-tabs").on("click", ".close-tab", function () {
            closeTab($(this).data("id"));
        });
        $(".main-tab.nav-tabs").on("click", ".refresh-tab", function () {
            var oFrm = document.getElementById('ifm' + $(this).data("id"));
            oFrm.src = oFrm.src
        });
    }, 'html');
}

function GetMenuList3(data) {
    menulist += '<ul class="submenu">';
    $.each(data.menus, function (i, sm) {
        if (sm.menus && sm.menus.length > 0) {
            menulist += '<li><a href="#"  class="dropdown-toggle" addtabs="mail" > <i class="' + sm.icon + '"></i>' + sm.menuname + '<b class="arrow icon-angle-down"></b></a>';
            GetMenuList3(sm);
        }
        else {
            menulist += '<li><a href="javascript:;" data-url="' + sm.url + '"  data-id="' + sm.menuid + '" onclick="addTablePage(this)"> <i class="icon-double-angle-right"></i>' + sm.menuname + '</a>';
        }
        menulist += '</li>';
    })
    menulist += '</ul>';
}
function GetMenuList2(data) {
    menulist += '<ul class="submenu">';
    $.each(data.menus, function (i, sm) {
        if (sm.menus && sm.menus.length > 0) {
            menulist += '<li><a href="#"  class="dropdown-toggle"> <i class="icon-double-angle-right"></i>' + sm.menuname + '<b class="arrow icon-angle-down"></b></a>';
            GetMenuList3(sm);
        }
        else {
            menulist += '<li><a href="javascript:;" data-url="' + sm.url + '"  data-id="' + sm.menuid + '"  onclick="addTablePage(this)"> <i class="icon-double-angle-right"></i>' + sm.menuname + '</a>';
        }
        menulist += '</li>';
    })
    menulist += '</ul>';
}
function GetMenuList1(data) {
    menulist += '<li class="active"><a href="/manage/"> <i class="icon-dashboard"></i><span class="menu-text"> 控制台 </span></a></li>';

    $.each(data, function (i, sm) {
        if (sm.menus && sm.menus.length > 0) {
            menulist += '<li><a href="#"  class="dropdown-toggle"> <i class="' + sm.icon + '"></i><span class="menu-text"> ' + sm.menuname + ' </span><b class="arrow icon-angle-down"></b></a>';
            GetMenuList2(sm);
        }
        else {
            menulist += '<li><a href="' + sm.url + '"> <i class="icon-dashboard"></i><span class="menu-text"> ' + sm.menuname + ' </span></a>';
        }
        menulist += '</li>';

    });
    $('#imanagemenu').html(menulist);

}

//选项卡
var addTabs = function (obj) {
    tabid = "tab_" + obj.id;

    $(".main-tab.nav-tabs .active").removeClass("active");
    $(".main-tab.tab-content .active").removeClass("active");
    //如果TAB不存在，创建一个新的TAB
    if (!$("#" + tabid)[0]) {
        //固定TAB中IFRAME高度,根据需要自己修改
        mainHeight = $(document).height() - 160;
        startInit('ifm' + obj.id, $(document).height() - 160);
        //创建新TAB的title
        title = '<li role="presentation" id="tab_' + tabid + '"><a href="#' + tabid + '" aria-controls="' + tabid + '" role="tab" data-toggle="tab" style="padding-right:30px">' + obj.title + '</a>';
        //是否允许关闭
        if (obj.close) {
            title += ' <i class="close-tab glyphicon glyphicon-remove"  data-id="' + tabid + '"></i><i class="refresh-tab glyphicon glyphicon-refresh" data-id="' + obj.id + '"></i>';
        }
        title += '</li>';
        //是否指定TAB内容
        if (obj.content) {
            content = '<div role="tabpanel" class="tab-pane" id="' + tabid + '">' + obj.content + '</div>';
        } else {//没有内容，使用IFRAME打开链接
            content = '<div role="tabpanel" class="tab-pane" id="' + tabid + '"><iframe src="' + obj.url + '" width="100%" height="' + mainHeight +
                    '" frameborder="no" border="0"  id="ifm' + obj.id + '" marginwidth="0" marginheight="0" scrolling="yes" allowtransparency="yes"></iframe></div>';
        }
        //加入TABS
        $(".main-tab.nav-tabs").append(title);
        $(".main-tab.tab-content").append(content);
    }

    //激活TAB
    $("#tab_" + tabid).addClass('active');
    $("#" + tabid).addClass("active");


};

var closeTab = function (id) {
    //如果关闭的是当前激活的TAB，激活他的前一个TAB
    if ($(".main-tab.nav-tabs  li.active").attr('id') == "tab_" + id) {
        $("#tab_" + id).prev().addClass('active');
        $("#" + id).prev().addClass('active');
    }
    //关闭TAB
    $("#tab_" + id).remove();
    $("#" + id).remove();
};

function addTablePage(obj) {
    $("#imanagemenu .active").removeClass("active").removeClass("open");
    $(obj).parent().parent().parent().addClass('active').addClass('open');
    $(obj).parent().parent().addClass('active').addClass('open');
    $(obj).parent().addClass('active');
    addTabs({ id: $(obj).data("id"), title: $(obj).text(), url: $(obj).data('url'), close: true });
}