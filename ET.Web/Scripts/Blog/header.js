
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
    var openid; //��¼�ɹ���ȡopenid,
    var accesstoken; //��ȡaccessToken
    var qqsex;
    var qqnickname;
    var qqpic;
    //��ȡ��ǰ��¼�û���Access Token�Լ�OpenID
    if (QC.Login.check()) {//����ѵ�¼
        QC.Login.getMe(function (openId, accessToken) {
            //��ҳ���ռ�OpenAPI��Ҫ�Ĳ�����get_user_info����Ҫ������������paras��û�в���
            var paras = {};
            //��JS SDK����OpenAPI
            QC.api("get_user_info", paras).success(function (s) {//ָ���ӿڷ��ʳɹ��Ľ��պ�����sΪ�ɹ�����Response����
                //�ɹ��ص���ͨ��s.data��ȡOpenAPI�ķ�������
                alert("��ȡ�û���Ϣ�ɹ�����ǰ�û��ǳ�Ϊ��" + s.data.nickname + "��ǰ�û�ͷ���ַ��" + s.data.figureurl + "�Ա�" + s.data.gender + "");
            }).error(function (f) {//ָ���ӿڷ���ʧ�ܵĽ��պ�����fΪʧ�ܷ���Response����
                //ʧ�ܻص�
                TopNotify("��ȡ�û���Ϣʧ��", "error");
            }).complete(function (c) {//ָ���ӿ���������Ľ��պ�����cΪ������󷵻�Response����
                //�������ص�
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
                            TopNotify("QQ��ݵ�¼ʧ�ܣ���ˢ�����ԣ�����:" + data, "error");
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
    //����QC.Login������ָ��btnId��������ť���������ڵ���

    QC.Login({
        //btnId�����밴ť�Ľڵ�id����ѡ

        btnId: "ibtnQQLogin",
        //�û���Ҫȷ�ϵ�scope��Ȩ���ѡ��Ĭ��all
        scope: "all",
        //��ť�ߴ磬����ֵ[A_XL| A_L| A_M| A_S|  B_M| B_S| C_S]����ѡ��Ĭ��B_S
        size: "B_M"
    }, function (reqData, opts) {//��¼�ɹ�
        QQLoginTrue();
    }, function (opts) {//ע���ɹ�
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

    //�ص����� Start
    //���Ƚ�#back-to-top����
    $("#back-to-top").hide();
    //����������λ�ô��ھඥ��100��������ʱ����ת���ӳ��֣�������ʧ
    $(function () {
        $(window).scroll(function () {
            if ($(window).scrollTop() > 100) {
                $("#back-to-top").fadeIn(500);
            }
            else {
                $("#back-to-top").fadeOut(500);
            }
        });
        //�������ת���Ӻ󣬻ص�ҳ�涥��λ��
        $("#back-to-top").click(function () {
            $('body,html').animate({ scrollTop: 0 }, 100);
            return false;
        });
    });

    //�ص����� End
});