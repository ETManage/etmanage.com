$(document).ready(function () {
    $("#btnlogin").click(function () { return CheckForm(); });
    $(document).keydown(function () { if (event.keyCode == 13) $("#btnlogin").click(); });
    $("#txtUserName").focus();
    //$("#txt_UserName,#txt_Password").focus(function () { $(".logintoolkit").html(""); });
});

function reset() {
    $("#txtUserName").val("");
    $("#txtPassword").val("");
}
//校验页面
function CheckForm() {
    setButtonStyle(1);
    var str = "";
    if ($("#txtUserName").val() == "") str += "“用户名”不能为空！<BR>";
    if ($("#txtPassword").val() == "") str += "“密码”不能为空！<BR>";

    if (str != "") {
        $("#ilogintoolkit").removeClass("hidden");
        $("#ilogintoolkittxt").removeClass("hidden");
        $("#ilogintoolkittxt").html(str);
        return false;
    }
    else
        login($("#txtUserName").val(), $("#txtPassword").val(), $("#ckRemember").is(':checked'))

}

function login(Cuser, Cpwd, isremember) {

    $.ajax({
        type: "POST", dataType: "html",
        url: "/maccount/ajaxlogin",
        data: { strUserName: Cuser, strUserPass: Cpwd, remember: isremember },
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
                    parent.location.href = '/manage/';

            }
            else if (data == 'false') {
                $("#ilogintoolkit").removeClass("hidden");
                $("#ilogintoolkittxt").removeClass("hidden");
                $("#ilogintoolkittxt").html("用户名或密码错误");
                setButtonStyle(2);
                return false;
            }
            else {
                $("#ilogintoolkit").removeClass("hidden");
                $("#ilogintoolkittxt").removeClass("hidden");
                $("#ilogintoolkittxt").html(data);
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
        $('#btnlogin').unbind("click");
        $('#btnlogin').text('登录中...');
    }
    else if (type == 2) {
        $('#btnlogin').bind("click", function () { CheckForm(); });
        $('#btnlogin').val('登录');
    }
}