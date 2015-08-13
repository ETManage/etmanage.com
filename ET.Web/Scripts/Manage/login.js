$(document).ready(function () {
    $("#btlogin").click(function () { return CheckForm(); });
    $(document).keydown(function () { if (event.keyCode == 13) $("#btlogin").click(); });
    $("#txt_UserName").focus();

    $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
    $(window).resize(function () {
        $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
    })
});

function reset() {
    $("#txt_UserName").val("");
    $("#txt_Password").val("");
}
//校验页面
function CheckForm() {
    var str = "";
    if ($("#txt_UserName").val() == "") str += "“用户名”不能为空！<BR>";
    if ($("#txt_Password").val() == "") str += "“密码”不能为空！<BR>";

    if (str != "") { $.messager.alert('提示', str); return false; }
    else
        login($("#txt_UserName").val(), $("#txt_Password").val())

}

function login(Cuser, Cpwd) {

    $.ajax({
        type: "POST", dataType: "html",
        url: "/maccount/ajaxlogin",
        data: { strUserName: Cuser, strUserPass: Cpwd, strRememberMe: '1' },
        success: function (data, textStatus) {
            if (data == 'true') {
                if (getRequest("oldurl") != "") {
                    if (window.top == window.self) {
                        location.href = unescape(getRequest("oldurl"));
                    }
                    else {
                        if (window.navigate) {
                            window.navigate(unescape(getRequest("oldurl")));
                        }
                        else
                            location.href = unescape(getRequest("oldurl"));
                    }
                }
                else
                    parent.location.href = '/manage/default';

            }
            else if (data == 'false') {
                $.messager.alert('提示', "用户名或密码错误");
                setButtonStyle(2);
                return false;
            }
            else {
                $.messager.alert('提示', data);
                setButtonStyle(2);
                return false;
            }
        }
    });
}

/*获得参数的方法*/
function getRequest(val) {
    var uri = window.location.search;
    var re = new RegExp("" + val + "=([^&?]*)", "ig");
    return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : "");
}



function setButtonStyle(type) {
    if (type == 1) {
        $('#hbtnLogin').unbind("click");
        $('#hbtnLogin').val('登录中...');
    }
    else if (type == 2) {
        $('#hbtnLogin').bind("click", function () { CheckForm(); });
        $('#hbtnLogin').val('用户登录');
    }
}


// 云朵浮动...
var $main = $cloud = mainwidth = null;
var offset1 = 450;
var offset2 = 0;

var offsetbg = 0;

$(document).ready(
        function () {
            $main = $("#mainBody");
            $body = $("body");
            $cloud1 = $("#cloud1");
            $cloud2 = $("#cloud2");

            mainwidth = $main.outerWidth();

        }
    );

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
    $cloud1.css("background-position", offset1 + "px 100px")

    $cloud2.css("background-position", offset2 + "px 460px")
}, 70);


setInterval(function bg() {
    if (offsetbg >= mainwidth) {
        offsetbg = -580;
    }

    offsetbg += 0.9;
    $body.css("background-position", -offsetbg + "px 0")
}, 90);
	