/// <reference path="xheditor.js" />
/// <reference path="xheditor.js" />
/*
创建人：Simmer
创建日：2014-10-1
作用：富文本编辑器控件调用 $('#NEW_BODY').editor({skin:'nostyle',tools:'full'});$('#NEW_BODY').editor({skin:'nostyle',tools:'simple'});$('#NEW_BODY').editor({skin:'nostyle',tools:'none'});
*/
var ApplicationAjaxEditorUrl = "WebEditor";
$.fn.extend({

    editor: function (arraylist) {
        $(this).xheditor({
            tools: "full",
            clickCancelDialog:false,
            upLinkUrl: "/ajax/" + ApplicationAjaxEditorUrl + "/upload", upLinkExt: "zip,rar,txt",
            upImgUrl: "/ajax/" + ApplicationAjaxEditorUrl + "/upload", upImgExt: "jpg,jpeg,gif,png",
            upFlashUrl: "/ajax/" + ApplicationAjaxEditorUrl + "/upload", upFlashExt: "swf",
            upMediaUrl: "/ajax/" + ApplicationAjaxEditorUrl + "/upload", upMediaExt: "flv,mp4,mp3",
            localUrlTest: /^https?:\/\/[^\/]*?(etmanage\.com)\//i,
            remoteImgSaveUrl: "/ajax/" + ApplicationAjaxEditorUrl + "/saveremoteimg"
        });
    }
});
document.write('<script type="text/javascript" src="/Res/editor/xheditor.js"></script>');

//
//html5Upload:false,upMultiple:'1' 