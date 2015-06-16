$(document).ready(function () {
    jQuery("span.timeago").timeago();
    $("#Loadding").click(LoadData);
});

//异步加载数据 Start
var totalGroups = 2;
var trackLoad = {};
trackLoad.groupNumber = 2;
trackLoad.url = "/blog/ajaxgetarticlelist?scope=&id=";
trackLoad.loading = false;
function LoadData() {
    var Oldscroll = $(window).scrollTop();
    if (trackLoad.groupNumber <= totalGroups && !trackLoad.loading) {
        trackLoad.loading = true;
        $('#Loadding').show().css("display", "block");
        $.ajax({
            url: trackLoad.url,
            type: "get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { "groupNumber": trackLoad.groupNumber },
            success: function (data, textStatus, xhr) {
                if (data.total && !isNaN(data.total))
                    totalGroups = Math.ceil(parseInt(data.total) / 10);
                if (data.rows && data.rows.length > 0) {
                    $.each(data.rows, function () {
                        var vlabel = this.ArticleLabel;
                        $(".content").append('<article class="excerpt"><header><a class="label label-important" href="@PublicHelper.GetHostAddress()/blog/articletag/' + vlabel + '/">' + vlabel + '<i class="label-arrow"></i></a>                        <h2><a target="_blank" href="' + this.ArticleUrl + '" title="' + this.ArticleTitle + '">' + this.ArticleTitle + '</a></h2>                    </header>                    <div class="focus">                        <a target="_blank" href="' + this.ArticleUrl + '"><img class="thumb" src="' + this.ArticleCover + '" alt="' + this.ArticleTitle + '" width="200" height="125"></a>                    </div> <span class="note">                       ' + this.ArticleDescription + '                            </span><p class="auth-span">                                <span class="muted">                                    <i class="fa fa-clock-o"></i>                                    <span class="time timeago" title="' + this.CreateTime + '">                                    </span>                                </span> <span class="muted"><i class="fa fa-eye"></i> ' + this.AccessCount + '℃</span> <span class="muted"><i class="fa fa-comments-o"></i> <a target="_blank" href="' + this.ArticleUrl + '#comments">' + (this.ArticleSource ? this.ArticleSource : 0).toString() + '评论</a></span><span class="muted"> <a href="javascript:;" data-action="ding" data-id="' + this.ArticleID + '" id="Addlike" class="action"><i class="fa fa-heart-o"></i><span class="count">' + this.LoveCount + '</span>喜欢</a></span>                            </p>                        </article>');
                    });
                }
                $('#Loadding').hide();
                trackLoad.groupNumber++;
                trackLoad.loading = false;
                $(window).scrollTop() = Oldscroll;
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                trackLoad.loading = false;
                console.log(errorThrown.toString());
            }
        });

    }
}
//异步加载数据 End

//图片滚动 Start
$(function () {

    var sWidth = $("#focus").width(); //获取焦点图的宽度（显示面积）
    var len = $("#focus ul li").length; //获取焦点图个数
    var index = 0;
    var picTimer;

    //以下代码添加数字按钮和按钮后的半透明条，还有上一页、下一页两个按钮
    var btn = "<div class='btnBg'></div><div class='btn'>";
    for (var i = 0; i < len; i++) {
        btn += "<span></span>";
    }
    btn += "</div><div class='preNext pre'></div><div class='preNext next'></div>";
    $("#focus").append(btn);
    $("#focus .btnBg").css("opacity", 0.5);
    //为小按钮添加鼠标滑入事件，以显示相应的内容
    $("#focus .btn span").css("opacity", 0.4).mouseover(function () {
        index = $("#focus .btn span").index(this);
        showPics(index);
    }).eq(0).trigger("mouseover");
    //上一页、下一页按钮透明度处理
    $("#focus .preNext").css("opacity", 0.2).hover(function () {
        $(this).stop(true, false).animate({ "opacity": "0.5" }, 300);
    }, function () {
        $(this).stop(true, false).animate({ "opacity": "0.2" }, 300);
    });
    //上一页按钮
    $("#focus .pre").click(function () {
        index -= 1;
        if (index == -1) { index = len - 1; }
        showPics(index);
    });
    //下一页按钮
    $("#focus .next").click(function () {
        index += 1;
        if (index == len) { index = 0; }
        showPics(index);
    });
    //本例为左右滚动，即所有li元素都是在同一排向左浮动，所以这里需要计算出外围ul元素的宽度
    $("#focus ul").css("width", sWidth * (len));

    //鼠标滑上焦点图时停止自动播放，滑出时开始自动播放
    $("#focus").hover(function () {
        clearInterval(picTimer);
        $(".preNext").show();
    }, function () {
        $(".preNext").hide();
        picTimer = setInterval(function () {
            showPics(index);
            index++;
            if (index == len) { index = 0; }
        }, 4000); //此4000代表自动播放的间隔，单位：毫秒
    }).trigger("mouseleave");

    //显示图片函数，根据接收的index值显示相应的内容
    function showPics(index) { //普通切换
        var nowLeft = -index * sWidth; //根据index值计算ul元素的left值
        $("#focus ul").stop(true, false).animate({ "left": nowLeft }, 300); //通过animate()调整ul元素滚动到计算出的position
        $(".btnBg").html("<a target='_blank' href='" + $("#focus ul li a:eq(" + index + ")").attr("href") + "'>" + $("#focus ul li img:eq(" + index + ")").attr("alt") + "</a>")
        $("#focus .btn span").stop(true, false).animate({ "opacity": "0.4" }, 300).eq(index).stop(true, false).animate({ "opacity": "1" }, 300); //为当前的按钮切换到选中的效果
    }
});
//图片滚动 End