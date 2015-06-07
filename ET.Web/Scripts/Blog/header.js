
var p = 0, t = 81;
//	$(window).scroll(function(e){
//	p = $(this).scrollTop();
//	if(t < p){
//	   $('.head').removeClass('headscroll');
//		$('.head').css({position:"relative"});
//		$('.addhead').css({display:"none"});

//	}else{
//		$('.head').addClass('headscroll');
//		$('.head').css({position:"fixed",top:"0px"}).fadeIn(500);
//		$('.addhead').css({display:"block"});

//	}
//	setTimeout(function(){
//						t = p;
//						}, 0);
//});

$(document).ready(function () {
    $(".user-ed .lot").hover(function () {
        $(this).addClass("hover");

    }, function () {
        $(this).removeClass("hover");

    }
	);
    $(".nav-mo .nav-lis").hover(function () {
        $(this).addClass("hover");
    }, function () {
        $(this).removeClass("hover");
    }
	);
    $(".toolbar .tool-lis").hover(function () {
        $(this).addClass("hover");
    }, function () {
        $(this).removeClass("hover");
    }
	);



    $(".seatext").each(function () {
        var thisVal = $(this).val();

        if (thisVal != "") {
            $(this).siblings("em").hide();
        } else {
            $(this).siblings("em").show();
        }

        $(this).focus(function () {
            $(this).siblings("em").hide();
        }).blur(function () {
            var val = $(this).val();
            if (val != "") {
                $(this).siblings("em").hide();
            } else {
                $(this).siblings("em").show();
            }
        });
    })

    $(".seatext").each(function () {
        var thisVal = $(this).val();

        if (thisVal != "") {

            $('.seabutton').addClass("focus");
        } else {

            $('.seabutton').removeClass("focus");
        }
        $(this).keyup(function () {
            var val = $(this).val();

            $('.seabutton').addClass("focus");
        }).blur(function () {
            var val = $(this).val();
            if (val != "") {

                $('.seabutton').addClass("focus");
            } else {

                $('.seabutton').removeClass("focus");
            }
        })
    })

});


function QQLoginTrue() {
    var IsLogin = false;
    var openid; //登录成功获取openid,
    var accesstoken; //获取accessToken
    var qqsex;
    var qqnickname;
    var qqpic;
    //获取当前登录用户的Access Token以及OpenID
    if (QC.Login.check()) {//如果已登录
        QC.Login.getMe(function (openId, accessToken) {
            //从页面收集OpenAPI必要的参数。get_user_info不需要输入参数，因此paras中没有参数
            var paras = {};
            //用JS SDK调用OpenAPI
            QC.api("get_user_info", paras).success(function (s) {//指定接口访问成功的接收函数，s为成功返回Response对象
                //成功回调，通过s.data获取OpenAPI的返回数据
                alert("获取用户信息成功！当前用户昵称为：" + s.data.nickname + "当前用户头像地址：" + s.data.figureurl + "性别：" + s.data.gender + "");
            }).error(function (f) {//指定接口访问失败的接收函数，f为失败返回Response对象
                //失败回调
                TopNotify("获取用户信息失败", "error");
            }).complete(function (c) {//指定接口完成请求后的接收函数，c为完成请求返回Response对象
                //完成请求回调
                qqnickname = c.data.nickname;
                qqsex = c.data.gender;
                openid = openId;
                accesstoken = accessToken;
                qqpic = c.data.figureurl;
                $.ajax({
                    type: "POST",
                    url: "/blog/ajaxqqlogin",
                    data: { "u": openid, "p": accesstoken, "name": qqnickname, "sex": qqsex, "pic": escape(qqpic) },
                    error: function (data) { alert("" + data + "") },
                    success: function (data) {
                        if (data == "true") {
                            QC.Login.signOut();
                            CookieHelper.addCookie("qqloginstatus", 1);
                            location.reload();
                        } else {
                            TopNotify("QQ快捷登录失败！请刷新重试！错误:" + data, "error");
                            return;
                        }
                    },
                    async: true,
                    dataType: "text"
                });


            });

        });

    }
}
function InitQQLogin() {
    //调用QC.Login方法，指定btnId参数将按钮绑定在容器节点中

    QC.Login({
        //btnId：插入按钮的节点id，必选

        btnId: "ibtnQQLogin",
        //用户需要确认的scope授权项，可选，默认all
        scope: "all",
        //按钮尺寸，可用值[A_XL| A_L| A_M| A_S|  B_M| B_S| C_S]，可选，默认B_S
        size: "B_M"
    }, function (reqData, opts) {//登录成功
        QQLoginTrue();
    }, function (opts) {//注销成功
        CookieHelper.addCookie("qqloginstatus", 0);
        location.reload();
    }
);
}
if (CookieHelper.getCookie("qqloginstatus") != "1")
    InitQQLogin();

$(document).ready(function () {
    var options = { serviceUrl: '@PublicHelper.GetHostAddress()/blog/ajaxsearch' };
    searchobj = $('#txtSearch').autocomplete(options);

    $("#btnSearch").click(function () {
        location.href = "@PublicHelper.GetHostAddress()/blog/search?q=" + escape($('#txtSearch').val());
    });

    //回到顶部 Start
    //首先将#back-to-top隐藏
    $("#back-to-top").hide();
    //当滚动条的位置处于距顶部100像素以下时，跳转链接出现，否则消失
    $(function () {
        $(window).scroll(function () {
            if ($(window).scrollTop() > 100) {
                $("#back-to-top").fadeIn(500);
            }
            else {
                $("#back-to-top").fadeOut(500);
            }
        });
        //当点击跳转链接后，回到页面顶部位置
        $("#back-to-top").click(function () {
            $('body,html').animate({ scrollTop: 0 }, 100);
            return false;
        });
    });

    //回到顶部 End
});