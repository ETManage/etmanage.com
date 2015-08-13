
(function (jQuery) {
    if (jQuery.browser) return;
    jQuery.browser = {};
    jQuery.browser.mozilla = false;
    jQuery.browser.webkit = false;
    jQuery.browser.opera = false;
    jQuery.browser.msie = false;
    var nAgt = navigator.userAgent;
    jQuery.browser.name = navigator.appName;
    jQuery.browser.fullVersion = '' + parseFloat(navigator.appVersion);
    jQuery.browser.majorVersion = parseInt(navigator.appVersion, 10);
    var nameOffset, verOffset, ix;
    // In Opera, the true version is after "Opera" or after "Version"
    if ((verOffset = nAgt.indexOf("Opera")) != -1) {
        jQuery.browser.opera = true;
        jQuery.browser.name = "Opera";
        jQuery.browser.fullVersion = nAgt.substring(verOffset + 6);
        if ((verOffset = nAgt.indexOf("Version")) != -1)
            jQuery.browser.fullVersion = nAgt.substring(verOffset + 8);
    }
        // In MSIE, the true version is after "MSIE" in userAgent
    else if ((verOffset = nAgt.indexOf("MSIE")) != -1) {
        jQuery.browser.msie = true;
        jQuery.browser.name = "Microsoft Internet Explorer";
        jQuery.browser.fullVersion = nAgt.substring(verOffset + 5);
    }
        // In Chrome, the true version is after "Chrome"
    else if ((verOffset = nAgt.indexOf("Chrome")) != -1) {
        jQuery.browser.webkit = true;
        jQuery.browser.name = "Chrome";
        jQuery.browser.fullVersion = nAgt.substring(verOffset + 7);
    }
        // In Safari, the true version is after "Safari" or after "Version"
    else if ((verOffset = nAgt.indexOf("Safari")) != -1) {
        jQuery.browser.webkit = true;
        jQuery.browser.name = "Safari";
        jQuery.browser.fullVersion = nAgt.substring(verOffset + 7);
        if ((verOffset = nAgt.indexOf("Version")) != -1)
            jQuery.browser.fullVersion = nAgt.substring(verOffset + 8);
    }
        // In Firefox, the true version is after "Firefox"
    else if ((verOffset = nAgt.indexOf("Firefox")) != -1) {
        jQuery.browser.mozilla = true;
        jQuery.browser.name = "Firefox";
        jQuery.browser.fullVersion = nAgt.substring(verOffset + 8);
    }
        // In most other browsers, "name/version" is at the end of userAgent
    else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) <
    (verOffset = nAgt.lastIndexOf('/'))) {
        jQuery.browser.name = nAgt.substring(nameOffset, verOffset);
        jQuery.browser.fullVersion = nAgt.substring(verOffset + 1);
        if (jQuery.browser.name.toLowerCase() == jQuery.browser.name.toUpperCase()) {
            jQuery.browser.name = navigator.appName;
        }
    }
    // trim the fullVersion string at semicolon/space if present
    if ((ix = jQuery.browser.fullVersion.indexOf(";")) != -1)
        jQuery.browser.fullVersion = jQuery.browser.fullVersion.substring(0, ix);
    if ((ix = jQuery.browser.fullVersion.indexOf(" ")) != -1)
        jQuery.browser.fullVersion = jQuery.browser.fullVersion.substring(0, ix);
    jQuery.browser.majorVersion = parseInt('' + jQuery.browser.fullVersion, 10);
    if (isNaN(jQuery.browser.majorVersion)) {
        jQuery.browser.fullVersion = '' + parseFloat(navigator.appVersion);
        jQuery.browser.majorVersion = parseInt(navigator.appVersion, 10);
    }
    jQuery.browser.version = jQuery.browser.majorVersion;
})(jQuery);


(function (e, $) {
    if (window.xheditor) return !1; var I = navigator.userAgent.toLowerCase(), Ba = -1 !== I.indexOf("mobile"), J = e.browser, pa = parseFloat(J.version), h = J.msie, qa = J.mozilla, R = J.safari, Ca = J.opera, eb = -1 < I.indexOf(" adobeair/"), Da = /OS 5(_\d)+ like Mac OS X/i.test(I); e.fn.xheditor = function (h) {
        if (Ba && !Da) return !1; var o = []; this.each(function () {
            if (e.nodeName(this, "TEXTAREA")) if (!1 === h) { if (this.xheditor) this.xheditor.remove(), this.xheditor = null } else if (this.xheditor) o.push(this.xheditor); else {
                var q = /({.*})/.exec(e(this).attr("class"));
                if (q) { try { q = eval("(" + q[1] + ")") } catch (t) { } h = e.extend({}, q, h) } q = new ra(this, h); if (q.init()) this.xheditor = q, o.push(q)
            }
        }); 0 === o.length && (o = !1); 1 === o.length && (o = o[0]); return o
    }; var aa = 0, S = !1, sa = !0, ta = !1, Sa = !1, t, ba, ca, da, K, Ea, ea, Fa, Ga, Ha, A; e("script[src*=xheditor]").each(function () { var e = this.src; if (e.match(/xheditor[^\/]*\.js/i)) return A = e.replace(/[\?#].*$/, "").replace(/(^|[\/\\])[^\/]*$/, "$1"), !1 }); if (h) {
        try { document.execCommand("BackgroundImageCache", !1, !0) } catch (qb) { } (I = e.fn.jquery) && I.match(/^1\.[67]/) &&
(e.attrHooks.width = e.attrHooks.height = null)
    } var fb = { 27: "esc", 9: "tab", 32: "space", 13: "enter", 8: "backspace", 145: "scroll", 20: "capslock", 144: "numlock", 19: "pause", 45: "insert", 36: "home", 46: "del", 35: "end", 33: "pageup", 34: "pagedown", 37: "left", 38: "up", 39: "right", 40: "down", 112: "f1", 113: "f2", 114: "f3", 115: "f4", 116: "f5", 117: "f6", 118: "f7", 119: "f8", 120: "f9", 121: "f10", 122: "f11", 123: "f12" }, Ta = "#FFFFFF,#CCCCCC,#C0C0C0,#999999,#666666,#333333,#000000,#FFCCCC,#FF6666,#FF0000,#CC0000,#990000,#660000,#330000,#FFCC99,#FF9966,#FF9900,#FF6600,#CC6600,#993300,#663300,#FFFF99,#FFFF66,#FFCC66,#FFCC33,#CC9933,#996633,#663333,#FFFFCC,#FFFF33,#FFFF00,#FFCC00,#999900,#666600,#333300,#99FF99,#66FF99,#33FF33,#33CC00,#009900,#006600,#003300,#99FFFF,#33FFFF,#66CCCC,#00CCCC,#339999,#336666,#003333,#CCFFFF,#66FFFF,#33CCFF,#3366FF,#3333FF,#000099,#000066,#CCCCFF,#9999FF,#6666CC,#6633FF,#6600CC,#333399,#330099,#FFCCFF,#FF99FF,#CC66CC,#CC33CC,#993399,#663366,#330033".split(","),
        //2015/4/2 West 
        LH = [ { n: "1", s: "1" }, { n: "1.15", s: "1.15" }, { n: "1.5", s: "1.5" }, { n: "2", s: "2" }, { n: "3", s: "3" }],
gb = [{ n: "p", t: "\u666e\u901a\u6bb5\u843d" }, { n: "h1", t: "\u6807\u98981" }, { n: "h2", t: "\u6807\u98982" }, { n: "h3", t: "\u6807\u98983" }, { n: "h4", t: "\u6807\u98984" }, { n: "h5", t: "\u6807\u98985" }, { n: "h6", t: "\u6807\u98986" }, { n: "pre", t: "\u5df2\u7f16\u6392\u683c\u5f0f" }, { n: "address", t: "\u5730\u5740"}], hb = [{ n: "\u5b8b\u4f53", c: "SimSun" }, { n: "\u4eff\u5b8b\u4f53", c: "FangSong_GB2312" }, { n: "\u9ed1\u4f53", c: "SimHei" }, { n: "\u6977\u4f53", c: "KaiTi_GB2312" }, { n: "\u5fae\u8f6f\u96c5\u9ed1", c: "Microsoft YaHei" }, { n: "Arial" }, { n: "Arial Black" },
{ n: "Comic Sans MS" }, { n: "Courier New" }, { n: "System" }, { n: "Times New Roman" }, { n: "Tahoma" }, { n: "Verdana"}], T = [{ n: "x-small", s: "10px", t: "\u6781\u5c0f" }, { n: "small", s: "12px", t: "\u7279\u5c0f" }, { n: "medium", s: "16px", t: "\u5c0f" }, { n: "large", s: "18px", t: "\u4e2d" }, { n: "x-large", s: "24px", t: "\u5927" }, { n: "xx-large", s: "32px", t: "\u7279\u5927" }, { n: "-webkit-xxx-large", s: "48px", t: "\u6781\u5927"}], ib = [{ s: "\u5de6\u5bf9\u9f50", v: "justifyleft" }, { s: "\u5c45\u4e2d", v: "justifycenter" }, { s: "\u53f3\u5bf9\u9f50", v: "justifyright" }, { s: "\u4e24\u7aef\u5bf9\u9f50",
    v: "justifyfull"
}], jb = [{ s: "\u6570\u5b57\u5217\u8868", v: "insertOrderedList" }, { s: "\u7b26\u53f7\u5217\u8868", v: "insertUnorderedList"}], kb = { "default": { name: "\u9ed8\u8ba4", width: 24, height: 24, line: 7, list: { smile: "\u5fae\u7b11", tongue: "\u5410\u820c\u5934", titter: "\u5077\u7b11", laugh: "\u5927\u7b11", sad: "\u96be\u8fc7", wronged: "\u59d4\u5c48", fastcry: "\u5feb\u54ed\u4e86", cry: "\u54ed", wail: "\u5927\u54ed", mad: "\u751f\u6c14", knock: "\u6572\u6253", curse: "\u9a82\u4eba", crazy: "\u6293\u72c2", angry: "\u53d1\u706b", ohmy: "\u60ca\u8bb6",
    awkward: "\u5c34\u5c2c", panic: "\u60ca\u6050", shy: "\u5bb3\u7f9e", cute: "\u53ef\u601c", envy: "\u7fa1\u6155", proud: "\u5f97\u610f", struggle: "\u594b\u6597", quiet: "\u5b89\u9759", shutup: "\u95ed\u5634", doubt: "\u7591\u95ee", despise: "\u9119\u89c6", sleep: "\u7761\u89c9", bye: "\u518d\u89c1"
}
}
}, ka = { Cut: { t: "\u526a\u5207 (Ctrl+X)" }, Copy: { t: "\u590d\u5236 (Ctrl+C)" }, Paste: { t: "\u7c98\u8d34 (Ctrl+V)" }, Pastetext: { t: "\u7c98\u8d34\u6587\u672c", h: h ? 0 : 1 }, Blocktag: { t: "\u6bb5\u843d\u6807\u7b7e", h: 1 }, Fontface: { t: "\u5b57\u4f53",
    h: 1
}, FontSize: { t: "\u5b57\u4f53\u5927\u5c0f", h: 1 }, Bold: { t: "\u52a0\u7c97 (Ctrl+B)", s: "Ctrl+B" }, Italic: { t: "\u659c\u4f53 (Ctrl+I)", s: "Ctrl+I" }, Underline: { t: "\u4e0b\u5212\u7ebf (Ctrl+U)", s: "Ctrl+U" },

    //2015/4/2 West 
LineHeight: { t: "Line-Height", h: 1 },

Strikethrough: { t: "\u5220\u9664\u7ebf" }, FontColor: { t: "\u5b57\u4f53\u989c\u8272", h: 1 },


BackColor: { t: "\u80cc\u666f\u989c\u8272", h: 1 }, SelectAll: { t: "\u5168\u9009 (Ctrl+A)" }, Removeformat: { t: "\u5220\u9664\u6587\u5b57\u683c\u5f0f" }, Align: { t: "\u5bf9\u9f50", h: 1 }, List: { t: "\u5217\u8868", h: 1 }, Outdent: { t: "\u51cf\u5c11\u7f29\u8fdb" },
    Indent: { t: "\u589e\u52a0\u7f29\u8fdb" }, Link: { t: "\u8d85\u94fe\u63a5 (Ctrl+L)", s: "Ctrl+L", h: 1 }, Unlink: { t: "\u53d6\u6d88\u8d85\u94fe\u63a5" }, Anchor: { t: "\u951a\u70b9", h: 1 }, Img: { t: "\u56fe\u7247", h: 1 }, Flash: { t: "Flash\u52a8\u753b", h: 1 }, Media: { t: "\u591a\u5a92\u4f53\u6587\u4ef6", h: 1 }, Hr: { t: "\u63d2\u5165\u6c34\u5e73\u7ebf" }, Emot: { t: "\u8868\u60c5", s: "ctrl+e", h: 1 }, Table: { t: "\u8868\u683c", h: 1 }, Source: { t: "\u6e90\u4ee3\u7801" }, Preview: { t: "\u9884\u89c8" }, Print: { t: "\u6253\u5370 (Ctrl+P)", s: "Ctrl+P" }, Fullscreen: { t: "\u5168\u5c4f\u7f16\u8f91 (Esc)",
        s: "Esc"
    }
    //, About: { t: "\u5173\u4e8e xhEditor" }//版权Simmer
}, Ia = { mini: "Bold,Italic,Underline,Strikethrough,|,Align,List,|,Link,Img", simple: "Blocktag,Fontface,FontSize,Bold,Italic,Underline,LineHeight,Strikethrough,FontColor,BackColor,|,Align,List,Outdent,Indent,|,Link,Img,Emot", full: "Cut,Copy,Paste,Pastetext,|,Blocktag,Fontface,FontSize,Bold,Italic,Underline,LineHeight,Strikethrough,FontColor,BackColor,SelectAll,Removeformat,|,Align,List,Outdent,Indent,|,Link,Unlink,Anchor,Img,Flash,Media,Hr,Emot,Table,|,Source,Preview,Print,Fullscreen" }; //simmer 按钮控制 2015/4/2 West
    Ia.mfull = Ia.full.replace(/\|(,Align)/i, "/$1"); var lb = { a: "Link", img: "Img", embed: "Embed" }, mb = { "<": "&lt;", ">": "&gt;", '"': "&quot;", "\u00ae": "&reg;", "\u00a9": "&copy;" }, nb = /[<>"\u00ae\u00a9]/g, ra = function (z, o) {
        function q(a) { var a = a.target, b = lb[a.tagName.toLowerCase()]; b && ("Embed" === b && (b = { "application/x-shockwave-flash": "Flash", "application/x-mplayer2": "Media"}[a.type.toLowerCase()]), d.exec(b)) } function I(a) { if (27 === a.which) return ta ? d.removeModal() : S && d.hidePanel(), !1 } function J() {
            setTimeout(d.setSource,
10)
        } function U() { d.getSource() } function Ua(a) {
            var b, c, f; if (a && (b = a.originalEvent.clipboardData) && (c = b.items) && (f = c[0]) && "file" == f.kind && f.type.match(/^image\//i)) return a = f.getAsFile(), b = new FileReader, b.onload = function () { var a = '<img src="' + event.target.result + '">', a = Va(a); d.pasteHTML(a) }, b.readAsDataURL(a), !1; var i = g.cleanPaste; if (0 === i || x || Ja) return !0; Ja = !0; d.saveBookmark(); var a = h ? "pre" : "div", m = e("<" + a + ' class="xhe-paste">\ufeff\ufeff</' + a + ">", l).appendTo(l.body), a = m[0]; b = d.getSel(); c = d.getRng(!0);
            m.css("top", fa.scrollTop()); h ? (c.moveToElementText(a), c.select()) : (c.selectNodeContents(a), b.removeAllRanges(), b.addRange(c)); setTimeout(function () {
                var a = 3 === i, b; if (a) b = m.text(); else { var c = []; e(".xhe-paste", l.body).each(function (a, b) { 0 == e(b).find(".xhe-paste").length && c.push(b.innerHTML) }); b = c.join("<br />") } m.remove(); d.loadBookmark(); if (b = b.replace(/^[\s\uFEFF]+|[\s\uFEFF]+$/g, "")) if (a) d.pasteText(b); else if (b = d.cleanHTML(b), b = d.cleanWord(b), b = d.formatXHTML(b), !g.onPaste || g.onPaste && !1 !== (b = g.onPaste(b))) b =
Va(b), d.pasteHTML(b); Ja = !1
            }, 0)
        } function Va(a) {
            var b = g.localUrlTest, c = g.remoteImgSaveUrl; if (b && c) {

                var f = [], i = 0, a = a.replace(/(<img)((?:\s+[^>]*?)?(?:\s+src="\s*([^"]+)\s*")(?: [^>]*)?)(\/?>)/ig, function (a, c, d, e, s) { /^(https?|data:image)/i.test(e) && !/_xhe_temp/.test(d) && !b.test(e) && (f[i] = e, d = d.replace(/\s+(width|height)="[^"]*"/ig, "").replace(/\s+src="[^"]*"/ig, ' src="' + ua + 'img/waiting.gif" remoteimg="' + i++ + '"')); return c + d + s }); 0 < f.length && e.post(c, { urls: f.join("|") }, function (a) {
                    a = a.split("|"); e("img[remoteimg]",
d.doc).each(function () { var b = e(this); L(b, "src", a[b.attr("remoteimg")]); b.removeAttr("remoteimg") })
                })
            } return a
        } function Ka(a) { try { d._exec("styleWithCSS", a, !0) } catch (b) { try { d._exec("useCSS", !a, !0) } catch (c) { } } } function La() { if (Ma && !x) { Ka(!1); try { d._exec("enableObjectResizing", !0, !0) } catch (a) { } if (h) try { d._exec("BackgroundImageCache", !0, !0) } catch (b) { } } } function Ba(a) {
            if (x || 13 !== a.which || a.shiftKey || a.ctrlKey || a.altKey) return !0; a = d.getParent("p,h1,h2,h3,h4,h5,h6,pre,address,div,li"); if (a.is("li")) return !0;
            if (g.forcePtag) 0 === a.length && d._exec("formatblock", "<p>"); else return d.pasteHTML("<br />"), h && 0 < a.length && 2 === d.getRng().parentElement().childNodes.length && d.pasteHTML("<br />"), !1
        } function Na() { !qa && !R && (la && B.height("100%").css("height", B.outerHeight() - n.outerHeight()), h && n.hide().show()) } function Da(a) { a = a.target; if (a.tagName.match(/(img|embed)/i)) { var b = d.getSel(), c = d.getRng(!0); c.selectNode(a); b.removeAllRanges(); b.addRange(c) } } function L(a, b, c) {
            if (!b) return !1; var d = "_xhe_" + b; c && (va && (c = V(c,
va, F)), a.attr(b, F ? V(c, "abs", F) : c).removeAttr(d).attr(d, c)); return a.attr(d) || a.attr(b)
        } function Oa() { sa && d.hidePanel() } function ob(a) { if (x) return !0; var b = a.which, c = fb[b], b = c ? c : String.fromCharCode(b).toLowerCase(); sKey = ""; sKey += a.ctrlKey ? "ctrl+" : ""; sKey += a.altKey ? "alt+" : ""; sKey += a.shiftKey ? "shift+" : ""; sKey += b; var a = ma[sKey], f; for (f in a) if (f = a[f], e.isFunction(f)) { if (!1 === f.call(d)) return !1 } else return d.exec(f), !1 } function M(a, b) {
            var c = typeof a; return !b ? "undefined" != c : "array" === b && a.hasOwnProperty &&
a instanceof Array ? !0 : c === b
        } function V(a, b, c) {
            if (a.match(/^(\w+):\/\//i) && !a.match(/^https?:/i) || /^#/i.test(a) || /^data:/i.test(a)) return a; var d = c ? e('<a href="' + c + '" />')[0] : location, c = d.protocol, i = d.host, m = d.hostname, j = d.port, d = d.pathname.replace(/\\/g, "/").replace(/[^\/]+$/i, ""); "" === j && (j = "80"); "" === d ? d = "/" : "/" !== d.charAt(0) && (d = "/" + d); a = e.trim(a); "abs" !== b && (a = a.replace(RegExp(c + "\\/\\/" + m.replace(/\./g, "\\.") + "(?::" + j + ")" + ("80" === j ? "?" : "") + "(/|$)", "i"), "/")); "rel" === b && (a = a.replace(RegExp("^" +
d.replace(/([\/\.\+\[\]\(\)])/g, "\\$1"), "i"), "")); if ("rel" !== b && (a.match(/^(https?:\/\/|\/)/i) || (a = d + a), "/" === a.charAt(0))) { for (var m = [], a = a.split("/"), p = a.length, d = 0; d < p; d++) j = a[d], ".." === j ? m.pop() : "" !== j && "." !== j && m.push(j); "" === a[p - 1] && m.push(""); a = "/" + m.join("/") } "abs" === b && !a.match(/^https?:\/\//i) && (a = c + "//" + i + a); return a = a.replace(/(https?:\/\/[^:\/?#]+):80(\/|$)/i, "$1$2")
        } function Wa(a, b) {
            if ("*" === b || a.match(RegExp(".(" + b.replace(/,/g, "|") + ")$", "i"))) return !0; alert("\u4e0a\u4f20\u6587\u4ef6\u6269\u5c55\u540d\u5fc5\u9700\u4e3a: " +
b); return !1
        } function Xa(a) { var b = Math.floor(Math.log(a) / Math.log(1024)); return (a / Math.pow(1024, Math.floor(b))).toFixed(2) + "Byte,KB,MB,GB,TB,PB".split(",")[b] } function N() { return !1 } var d = this, G = e(z), Ya = G.closest("form"), n, B, W, fa, l, ga, ha, Ma = !1, x = !1, la = !1, Ja = !1, Pa, na = !1, Za = "", v = null, wa, oa = !1, Qa = !1, ia = null, X = null, O = 0, g = d.settings = e.extend({}, ra.settings, o), xa = g.plugins, ya = []; xa && (ka = e.extend({}, ka, xa), e.each(xa, function (a) { ya.push(a) }), ya = ya.join(",")); if (g.tools.match(/^\s*(m?full|simple|mini)\s*$/i)) {
            var $a =
Ia[e.trim(g.tools)]; g.tools = g.tools.match(/m?full/i) && xa ? $a.replace("Table", "Table," + ya) : $a
        } g.tools.match(/(^|,)\s*About\s*(,|$)/i) || (g.tools += ",About"); g.tools = g.tools.split(","); if (g.editorRoot) A = g.editorRoot; !1 === eb && (A = V(A, "abs")); if (g.urlBase) g.urlBase = V(g.urlBase, "abs"); var ab = "xheCSS_" + g.skin, ja = "xhe" + aa + "_container", bb = "xhe" + aa + "_Tool", cb = "xhe" + aa + "_iframearea", db = "xhe" + aa + "_iframe", za = "xhe" + aa + "_fixffcursor", P = "", Q = "", ua = A + "editor_skin/" + g.skin + "/", Aa = kb, va = g.urlType, F = g.urlBase, Y = g.emotPath,
Y = Y ? Y : A + "editor_emot/", Ra = "", Aa = e.extend({}, Aa, g.emots), Y = V(Y, "rel", F ? F : null); (na = g.showBlocktag) && (Q += " showBlocktag"); var ma = []; this.init = function () {
    0 === e("#" + ab).length && e("head").append('<link id="' + ab + '" rel="stylesheet" type="text/css" href="' + ua + 'ui.css" />'); var a = G.outerWidth(), b = G.outerHeight(), a = g.width || z.style.width || (10 < a ? a : 0); O = g.height || z.style.height || (10 < b ? b : 150); M(a, "number") && (a += "px"); M(O, "string") && (O = O.replace(/[^\d]+/g, "")); var b = g.background || z.style.background, c = ['<span class="xheGStart"/>'],
f, i, m = /\||\//i; e.each(g.tools, function (a, b) {
    b.match(m) && c.push('<span class="xheGEnd"/>'); if ("|" === b) c.push('<span class="xheSeparator"/>'); else if ("/" === b) c.push("<br />"); else { f = ka[b]; if (!f) return; i = f.c ? f.c : "xheIcon xheBtn" + b; c.push('<span><a href="#" title="' + f.t + '" cmd="' + b + '" class="xheButton xheEnabled" tabindex="-1" role="button"><span class="' + i + '" unselectable="on" style="font-size:0;color:transparent;text-indent:-999px;">' + f.t + "</span></a></span>"); f.s && d.addShortcuts(f.s, b) } b.match(m) &&
c.push('<span class="xheGStart"/>')
});

    c.push('<span class="xheGEnd"/><br />'); G.after(e('<input type="text" id="' + za + '" style="position:absolute;display:none;" /><span id="' + ja + '" class="xhe_' + g.skin + '" style="display:none"><table cellspacing="0" cellpadding="0" class="xheLayout" style="' + ("0px" != a ? "width:" + a + ";" : "") + "height:" + O + 'px;" role="presentation"><tr><td id="' + bb + '" class="xheTool" unselectable="on" style="height:1px;" role="presentation"></td></tr><tr><td id="' + cb + '" class="xheIframeArea" role="presentation"><iframe frameborder="0" id="' +
db + '" src="javascript:;" style="width:100%;"></iframe></td></tr></table></span>')); n = e("#" + bb); B = e("#" + cb); P = '<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><link rel="stylesheet" href="' + ua + 'iframe.css"/>'; if (a = g.loadCSS) if (M(a, "array")) for (var j in a) P += '<link rel="stylesheet" href="' + a[j] + '"/>'; else P = a.match(/\s*<style(\s+[^>]*?)?>[\s\S]+?<\/style>\s*/i) ? P + a : P + ('<link rel="stylesheet" href="' + a + '"/>'); j = "<html><head>" + P + "<title>\u53ef\u89c6\u5316\u7f16\u8f91\u5668,alt+1\u52309\u952e,\u5207\u6362\u5230\u5de5\u5177\u533a,tab\u952e,\u9009\u62e9\u6309\u94ae,esc\u952e,\u8fd4\u56de\u7f16\u8f91 " +
(g.readTip ? g.readTip : "") + "</title>"; b && (j += "<style>html{background:" + b + ";}</style>"); j += '</head><body spellcheck="0" class="editMode' + Q + '"></body></html>'; d.win = W = e("#" + db)[0].contentWindow; fa = e(W); try { this.doc = l = W.document, ga = e(l), l.open(), l.write(j), l.close(), h ? l.body.contentEditable = "true" : l.designMode = "On" } catch (p) { } setTimeout(La, 300); d.setSource(); W.setInterval = null; n.append(c.join("")).bind("mousedown contextmenu", N).click(function (a) {
    var b = e(a.target).closest("a"); b.is(".xheEnabled") && (clearTimeout(wa),
n.find("a").attr("tabindex", "-1"), v = a, d.exec(b.attr("cmd"))); return !1
}); n.find(".xheButton").hover(function (a) { var b = e(this), c = g.hoverExecDelay, k = X; X = null; if (-1 === c || oa || !b.is(".xheEnabled")) return !1; if (k && 10 < k) return oa = !0, setTimeout(function () { oa = !1 }, 100), !1; var f = b.attr("cmd"); if (1 !== ka[f].h) return d.hidePanel(), !1; Qa && (c = 0); 0 <= c && (wa = setTimeout(function () { v = a; ia = { x: v.clientX, y: v.clientY }; d.exec(f) }, c)) }, function () { ia = null; wa && clearTimeout(wa) }).mousemove(function (a) {
    if (ia) {
        var b = a.clientX - ia.x,
c = a.clientY - ia.y; if (1 < Math.abs(b) || 1 < Math.abs(c)) 0 < b && 0 < c ? (b = Math.round(Math.atan(c / b) / 0.017453293), X = X ? (X + b) / 2 : b) : X = null, ia = { x: a.clientX, y: a.clientY }
    }
}); t = e("#xhePanel"); ba = e("#xheShadow"); ca = e("#xheCntLine"); 0 === t.length && (t = e('<div id="xhePanel"></div>').mousedown(function (a) { a.stopPropagation() }), ba = e('<div id="xheShadow"></div>'), ca = e('<div id="xheCntLine"></div>'), setTimeout(function () { e(document.body).append(t).append(ba).append(ca) }, 10)); e("#" + ja).show(); G.hide(); B.css("height", O - n.outerHeight());
    h & 8 > pa && setTimeout(function () { B.css("height", O - n.outerHeight()) }, 1); G.focus(d.focus); Ya.submit(U).bind("reset", J); g.submitID && e("#" + g.submitID).mousedown(U); e(window).bind("unload beforeunload", U).bind("resize", Na); e(document).mousedown(Oa); Sa || (e(document).keydown(I), Sa = !0); fa.focus(function () { g.focus && g.focus() }).blur(function () { g.blur && g.blur() }); R && fa.click(Da); ga.mousedown(Oa).keydown(ob).keypress(Ba).dblclick(q).bind("mousedown click", function (a) { G.trigger(a.type) }); if (h) {
        ga.keydown(function (a) {
            var b =
d.getRng(); if (8 === a.which && b.item) return e(b.item(0)).remove(), !1
        }); var w = function (a) { var a = e(a.target), b; (b = a.css("width")) && a.css("width", "").attr("width", b.replace(/[^0-9%]+/g, "")); (b = a.css("height")) && a.css("height", "").attr("height", b.replace(/[^0-9%]+/g, "")) }; ga.bind("controlselect", function (a) { a = a.target; e.nodeName(a, "IMG") && e(a).unbind("resizeend", w).bind("resizeend", w) })
    } ga.keydown(function (a) {
        var b = a.which; if (a.altKey && 49 <= b && 57 >= b) return n.find("a").attr("tabindex", "0"), n.find(".xheGStart").eq(b -
49).next().find("a").focus(), l.title = "\ufeff\ufeff", !1
    }).click(function () { n.find("a").attr("tabindex", "-1") }); n.keydown(function (a) { var b = a.which; if (27 == b) n.find("a").attr("tabindex", "-1"), d.focus(); else if (a.altKey && 49 <= b && 57 >= b) return n.find(".xheGStart").eq(b - 49).next().find("a").focus(), !1 }); j = e(l.documentElement); Ca ? j.bind("keydown", function (a) { a.ctrlKey && 86 === a.which && Ua() }) : j.bind(h ? "beforepaste" : "paste", Ua); g.disableContextmenu && j.bind("contextmenu", N); g.html5Upload && j.bind("dragenter dragover",
function (a) { var b; if ((b = a.originalEvent.dataTransfer.types) && -1 !== e.inArray("Files", b)) return !1 }).bind("drop", function (a) {
    var a = a.originalEvent.dataTransfer, b; if (a && (b = a.files) && 0 < b.length) {
        var c, k, a = ["Link", "Img", "Flash", "Media"], f = [], j; for (c in a) k = a[c], g["up" + k + "Url"] && g["up" + k + "Url"].match(/^[^!].*/i) && f.push(k + ":," + g["up" + k + "Ext"]); if (0 === f.length) return !1; j = f.join(","); k = function (a) {
            var b, d; for (c = 0; c < a.length; c++) if (b = a[c].name.replace(/.+\./, ""), b = j.match(RegExp("(\\w+):[^:]*," + b + "(?:,|$)",
"i"))) if (d) { if (d !== b[1]) return 2 } else d = b[1]; else return 1; return d
        } (b); 1 === k ? alert("\u4e0a\u4f20\u6587\u4ef6\u7684\u6269\u5c55\u540d\u5fc5\u9700\u4e3a\uff1a" + j.replace(/\w+:,/g, "")) : 2 === k ? alert("\u6bcf\u6b21\u53ea\u80fd\u62d6\u653e\u4e0a\u4f20\u540c\u4e00\u7c7b\u578b\u6587\u4ef6") : k && d.startUpload(b, g["up" + k + "Url"], "*", function (a) {
            var b = [], c; (c = g.onUpload) && c(a); for (var f = 0, j = a.length; f < j; f++) c = a[f], url = M(c, "string") ? c : c.url, "!" === url.substr(0, 1) && (url = url.substr(1)), b.push(url); d.exec(k); e("#xhe" +
k + "Url").val(b.join(" ")); e("#xheSave").click()
        }); return !1
    }
}); (j = g.shortcuts) && e.each(j, function (a, b) { d.addShortcuts(a, b) }); aa++; Ma = !0; g.fullscreen ? d.toggleFullscreen() : g.sourceMode && setTimeout(d.toggleSource, 20); return !0
}; this.remove = function () {
    d.hidePanel(); U(); G.unbind("focus", d.focus); Ya.unbind("submit", U).unbind("reset", J); g.submitID && e("#" + g.submitID).unbind("mousedown", U); e(window).unbind("unload beforeunload", U).unbind("resize", Na); e(document).unbind("mousedown", Oa); e("#" + ja).remove(); e("#" +
za).remove(); G.show(); Ma = !1
}; this.saveBookmark = function () { if (!x) { d.focus(); var a = d.getRng(), a = a.cloneRange ? a.cloneRange() : a; ha = { top: fa.scrollTop(), rng: a} } }; this.loadBookmark = function () { if (!x && ha) { d.focus(); var a = ha.rng; if (h) a.select(); else { var b = d.getSel(); b.removeAllRanges(); b.addRange(a) } fa.scrollTop(ha.top); ha = null } }; this.focus = function () { x ? e("#sourceCode", l).focus() : W.focus(); if (h) { var a = d.getRng(); a.parentElement && a.parentElement().ownerDocument !== l && d.setTextCursor() } return !1 }; this.setTextCursor =
function (a) { var b = d.getRng(!0), c = l.body; if (h) b.moveToElementText(c); else { for (var e = a ? "lastChild" : "firstChild"; 3 != c.nodeType && c[e]; ) c = c[e]; b.selectNode(c) } b.collapse(a ? !1 : !0); h ? b.select() : (a = d.getSel(), a.removeAllRanges(), a.addRange(b)) }; this.getSel = function () { return l.selection ? l.selection : W.getSelection() }; this.getRng = function (a) { var b, c; try { a || (b = d.getSel(), c = b.createRange ? b.createRange() : 0 < b.rangeCount ? b.getRangeAt(0) : null), c || (c = l.body.createTextRange ? l.body.createTextRange() : l.createRange()) } catch (e) { } return c };
        this.getParent = function (a) { var b = d.getRng(), c; h ? c = b.item ? b.item(0) : b.parentElement() : (c = b.commonAncestorContainer, b.collapsed || b.startContainer === b.endContainer && 2 > b.startOffset - b.endOffset && b.startContainer.hasChildNodes() && (c = b.startContainer.childNodes[b.startOffset])); a = a ? a : "*"; c = e(c); c.is(a) || (c = e(c).closest(a)); return c }; this.getSelect = function (a) {
            var b = d.getSel(), c = d.getRng(), f = !0, f = !c || c.item ? !1 : !b || 0 === c.boundingWidth || c.collapsed; if ("text" === a) return f ? "" : c.text || (b.toString ? b.toString() :
""); c.cloneContents ? (a = e("<div></div>"), (c = c.cloneContents()) && a.append(c), c = a.html()) : c = M(c.item) ? c.item(0).outerHTML : M(c.htmlText) ? c.htmlText : c.toString(); f && (c = ""); c = d.processHTML(c, "read"); c = d.cleanHTML(c); return c = d.formatXHTML(c)
        }; this.pasteHTML = function (a, b) {
            if (x) return !1; d.focus(); var a = d.processHTML(a, "write"), c = d.getSel(), f = d.getRng(); if (b !== $) { if (f.item) { var i = f.item(0), f = d.getRng(!0); f.moveToElementText(i); f.select() } f.collapse(b) } a += "<" + (h ? "img" : "span") + ' id="_xhe_temp" width="0" height="0" />';
            if (f.insertNode) { if (0 < e(f.startContainer).closest("style,script").length) return !1; f.deleteContents(); f.insertNode(f.createContextualFragment(a)) } else "control" === c.type.toLowerCase() && (c.clear(), f = d.getRng()), f.pasteHTML(a); var i = e("#_xhe_temp", l), m = i[0]; h ? (f.moveToElementText(m), f.select()) : (f.selectNode(m), c.removeAllRanges(), c.addRange(f)); i.remove()
        }; this.pasteText = function (a, b) { a || (a = ""); a = d.domEncode(a); a = a.replace(/\r?\n/g, "<br />"); d.pasteHTML(a, b) }; this.appendHTML = function (a) {
            if (x) return !1;
            d.focus(); a = d.processHTML(a, "write"); e(l.body).append(a); d.setTextCursor(!0)
        }; this.domEncode = function (a) { return a.replace(nb, function (a) { return mb[a] }) }; this.setSource = function (a) { ha = null; if ("string" !== typeof a && "" !== a) a = z.value; x ? e("#sourceCode", l).val(a) : (g.beforeSetSource && (a = g.beforeSetSource(a)), a = d.cleanHTML(a), a = d.formatXHTML(a), a = d.processHTML(a, "write"), h ? (l.body.innerHTML = '<img id="_xhe_temp" width="0" height="0" />' + a, e("#_xhe_temp", l).remove()) : l.body.innerHTML = a) }; this.processHTML = function (a,
b) {

            if ("write" === b) {
              
                a = a.replace(/(<(\/?)(\w+))((?:\s+[\w\-:]+\s*=\s*(?:"[^"]*"|'[^']*'|[^>\s]+))*)\s*((\/?)>)/g, function (a, b, c, d, e, f, i) {
                    d = d.toLowerCase(); qa ? "strong" === d ? d = "b" : "em" === d && (d = "i") : R && ("strong" === d ? (d = "span", c || (e += ' class="Apple-style-span" style="font-weight: bold;"')) : "em" === d ? (d = "span", c || (e += ' class="Apple-style-span" style="font-style: italic;"')) : "u" === d ? (d = "span", c || (e += ' class="Apple-style-span" style="text-decoration: underline;"')) : "strike" === d && (d = "span", c || (e += ' class="Apple-style-span" style="text-decoration: line-through;"')));
                    var k, y = ""; if ("del" === d) d = "strike"; else if ("img" === d) {
                       





                        e = e.replace(/\s+emot\s*=\s*("[^"]*"|'[^']*'|[^>\s]+)/i, function (a, b) { k = b.match(/^(["']?)(.*)\1/)[2]; k = k.split(","); k[1] || (k[1] = k[0], k[0] = ""); "default" === k[0] && (k[0] = ""); return g.emotMark ? a : "" });
                    }



                    else if ("a" === d) !e.match(/ href=[^ ]/i) && e.match(/ name=[^ ]/i) && (y += " xhe-anchor"), i && (f = "></a>"); else if ("table" === d && !c && (a = e.match(/\s+border\s*=\s*("[^"]*"|'[^']*'|[^>\s]+)/i), !a || a[1].match(/^(["']?)\s*0\s*\1$/))) y += " xhe-border"; var Z, e = e.replace(/\s+([\w\-:]+)\s*=\s*("[^"]*"|'[^']*'|[^>\s]+)/g,
function (a, b, c) {
    b = b.toLowerCase(); c = c.match(/^(["']?)(.*)\1/)[2]; aft = ""; if (h && b.match(/^(disabled|checked|readonly|selected)$/) && c.match(/^(false|0)$/i) || "img" === d && k && "src" === b) return "";

    if (b == "src" && c.toLowerCase().indexOf('file://') == 0) {

        //        //实现能够复制WORD中的图片
        try {
            xmlhttp = new XMLHttpRequest();
        } catch (e) {
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }

        xmlhttp.onreadystatechange = function () {
            if (4 == xmlhttp.readyState) {
                if (200 == xmlhttp.status) {
                    var Bodys = xmlhttp.responseText;
                    c = Bodys;

                }
            }
        }

        xmlhttp.open("post", g.remoteImgSaveUrl + "?urls=" + c, false);
        xmlhttp.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
        xmlhttp.send("");



        /*         alert(b);  simmer 此处用户复制本地图片触发事件  */
    }

    b.match(/^(src|href)$/) && (aft = " _xhe_" + b + '="' + c + '"', F && (c = V(c, "abs", F))); y && "class" === b && (c += " " + y, y = ""); R && "style" === b && "span" === d && c.match(/(^|;)\s*(font-family|font-size|line-height|color|background-color)\s*:\s*[^;]+\s*(;|$)/i) && (Z = !0);
   
    return " " + b + '="' + c + '"' + aft
}); k && (a = Y + (k[0] ? k[0] : "default") + "/" + k[1] + ".gif", e += ' src="' +
a + '" _xhe_src="' + a + '"');


                    Z && (e += ' class="Apple-style-span"'); y && (e += ' class="' + y + '"'); return "<" + c + d + e + f
                }); h && (a = a.replace(/&apos;/ig, "&#39;")); if (!R) var c = function (a, b, c, d, e, f) {
                    
                    var b = "", i, k; (i = d.match(/font-family\s*:\s*([^;"]+)/i)) && (b += ' face="' + i[1] + '"'); if (i = d.match(/font-size\s*:\s*([^;"]+)/i)) { i = i[1].toLowerCase(); for (var y = 0; y < T.length; y++) if (i === T[y].n || i === T[y].s) { k = y + 1; break } k && (b += ' size="' + k + '"', d = d.replace(/(^|;)(\s*font-size\s*:\s*[^;"]+;?)+/ig, "$1")) } 
                    //2015/4/2 West
                    if (i = d.match(/line-height\s*:\s*([^;"]+)/i)) { i = i[1].toLowerCase(); for (var y = 0; y < LH.length; y++) if (i === LH[y].n || i === LH[y].s) { k = y + 1; break } k && (b += ' lineheight="' + k + '"', d = d.replace(/(^|;)(\s*line-height\s*:\s*[^;"]+;?)+/ig, "$1")) }

                    if (k = d.match(/(?:^|[\s;])color\s*:\s*([^;"]+)/i)) {
                        if (i =
k[1].match(/\s*rgb\s*\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*\)/i)) { k[1] = "#"; for (y = 1; 3 >= y; y++) k[1] += ("0" + (i[y] - 0).toString(16)).slice(-2) } k[1] = k[1].replace(/^#([0-9a-f])([0-9a-f])([0-9a-f])$/i, "#$1$1$2$2$3$3"); b += ' color="' + k[1] + '"'
                    }
                   

                    d = d.replace(/(^|;)(\s*(font-family|color)\s*:\s*[^;"]+;?)+/ig, "$1"); return "" !== b ? (d && (b += ' style="' + d + '"'), "<font" + (c ? c : "") + b + (e ? e : "") + ">" + f + "</font>") : a
                }, a = a.replace(/<(span)(\s+[^>]*?)?\s+style\s*=\s*"((?:[^"]*?;)?\s*(?:font-family|font-size|line-height|color)\s*:[^"]*)"( [^>]*)?>(((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S])*?<\/\1>)*?<\/\1>)*?)<\/\1>/ig,
c), a = a.replace(/<(span)(\s+[^>]*?)?\s+style\s*=\s*"((?:[^"]*?;)?\s*(?:font-family|font-size|line-height|color)\s*:[^"]*)"( [^>]*)?>(((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S])*?<\/\1>)*?)<\/\1>/ig, c), a = a.replace(/<(span)(\s+[^>]*?)?\s+style\s*=\s*"((?:[^"]*?;)?\s*(?:font-family|font-size|line-height|color)\s*:[^"]*)"( [^>]*)?>(((?!<\1(\s+[^>]*?)?>)[\s\S])*?)<\/\1>/ig, c); a = a.replace(/<(td|th)(\s+[^>]*?)?>(\s|&nbsp;)*<\/\1>/ig, "<$1$2>" + (h ? "" : "<br />") + "</$1>")
            } else {
                
                if (R) for (var d = [{ r: /font-weight\s*:\s*bold;?/ig,
                    t: "strong"
                }, { r: /font-style\s*:\s*italic;?/ig, t: "em" }, { r: /text-decoration\s*:\s*underline;?/ig, t: "u" }, { r: /text-decoration\s*:\s*line-through;?/ig, t: "strike"}], c = function (a, b, c, e, i) { for (var a = (c ? c : "") + (e ? e : ""), g = [], D = [], k, c = 0; c < d.length; c++) b = d[c].r, k = d[c].t, a = a.replace(b, function () { g.push("<" + k + ">"); D.push("</" + k + ">"); return "" }); a = a.replace(/\s+style\s*=\s*"\s*"/i, ""); return (a ? "<span" + a + ">" : "") + g.join("") + i + D.join("") + (a ? "</span>" : "") }, e = 0; 2 > e; e++) a = a.replace(/<(span)(\s+[^>]*?)?\s+class\s*=\s*"Apple-style-span"(\s+[^>]*?)?>(((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S])*?<\/\1>)*?<\/\1>)*?)<\/\1>/ig,
c), a = a.replace(/<(span)(\s+[^>]*?)?\s+class\s*=\s*"Apple-style-span"(\s+[^>]*?)?>(((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S])*?<\/\1>)*?)<\/\1>/ig, c), a = a.replace(/<(span)(\s+[^>]*?)?\s+class\s*=\s*"Apple-style-span"(\s+[^>]*?)?>(((?!<\1(\s+[^>]*?)?>)[\s\S])*?)<\/\1>/ig, c); a = a.replace(/(<(\w+))((?:\s+[\w\-:]+\s*=\s*(?:"[^"]*"|'[^']*'|[^>\s]+))*)\s*(\/?>)/g, function (a, b, c, d, e) {
    var c = c.toLowerCase(), f, d = d.replace(/\s+_xhe_(?:src|href)\s*=\s*("[^"]*"|'[^']*'|[^>\s]+)/i, function (a,
b) { f = b.match(/^(["']?)(.*)\1/)[2]; return "" }); f && va && (f = V(f, va, F)); d = d.replace(/\s+([\w\-:]+)\s*=\s*("[^"]*"|'[^']*'|[^>\s]+)/g, function (a, b, c) {
    b = b.toLowerCase(); c = c.match(/^(["']?)(.*)\1/)[2].replace(/"/g, "'"); if ("class" === b) { if (c.match(/^["']?(apple|webkit)/i)) return ""; c = c.replace(/\s?xhe-[a-z]+/ig, ""); if ("" === c) return "" } else {
       
        if (b.match(/^((_xhe_|_moz_|_webkit_)|jquery\d+)/i)) return ""; if (f && b.match(/^(src|href)$/i)) return " " + b + '="' + f + '"'; "style" === b && (c = c.replace(/(^|;)\s*(font-size)\s*:\s*([a-z-]+)\s*(;|$)/i,
function (a, b, c, d, e) { for (var f, i = 0; i < T.length; i++) if (a = T[i], d === a.n) { f = a.s; break } return b + c + ":" + f + e }));

        //2015/4/2 West
        "style" === b && (c = c.replace(/(^|;)\s*(line-height)\s*:\s*([a-z-]+)\s*(;|$)/i,
function (a, b, c, d, e) { for (var f, i = 0; i < LH.length; i++) if (a = LH[i], d === a.n) { f = a.s; break } return b + c + ":" + f + e }))
    } return " " + b + '="' + c + '"';
    
}); 
    "img" === c && !d.match(/\s+alt\s*=\s*("[^"]*"|'[^']*'|[^>\s]+)/i) && (d += ' alt=""'); return b + d + e
}); a = a.replace(/(<(td|th)(?:\s+[^>]*?)?>)\s*([\s\S]*?)(<br(\s*\/)?>)?\s*<\/\2>/ig, function (a, b, c, d) { return b + (d ? d : "&nbsp;") + "</" + c + ">" }); a = a.replace(/^\s*(?:<(p|div)(?:\s+[^>]*?)?>)?\s*(<span(?:\s+[^>]*?)?>\s*<\/span>|<br(?:\s+[^>]*?)?>|&nbsp;)*\s*(?:<\/\1>)?\s*$/i, "")
            } return a = a.replace(/(<pre(?:\s+[^>]*?)?>)([\s\S]+?)(<\/pre>)/gi,
function (a, b, c, d) { return b + c.replace(/<br\s*\/?>/ig, "\r\n") + d })
        }; this.getSource = function (a) { var b, c = g.beforeGetSource; x ? (b = e("#sourceCode", l).val(), c || (b = d.formatXHTML(b, !1))) : (b = d.processHTML(l.body.innerHTML, "read"), b = d.cleanHTML(b), b = d.formatXHTML(b, a), c && (b = c(b))); return z.value = b }; this.cleanWord = function (a) {
           
            var b = g.cleanPaste; if (0 < b && 3 > b && /mso(-|normal)|WordDocument|<table\s+[^>]*?x:str|\s+class\s*=\s*"?xl[67]\d"/i.test(a)) {
                a = a.replace(/<\!--[\s\S]*?--\>|<!(--)?\[[\s\S]+?\](--)?>|<style(\s+[^>]*?)?>[\s\S]*?<\/style>/ig,
""); a = a.replace(/\r?\n/ig, ""); h ? (a = a.replace(/<v:shapetype(\s+[^>]*)?>[\s\S]*<\/v:shapetype>/ig, ""), a = a.replace(/<v:shape(\s+[^>]+)?>[\s\S]*?<v:imagedata(\s+[^>]+)?>\s*<\/v:imagedata>[\s\S]*?<\/v:shape>/ig, function (a, b, c) {

    if (a = c.match(/\s+src\s*=\s*("[^"]+"|'[^']+'|[^>\s]+)/i)) {


        a[1].match(/^(["']?)(.*)\1/); c = '<img src="' + A + f + '" _xhe_temp="true" class="wordImage"'; if (a = b.match(/\s+style\s*=\s*("[^"]+"|'[^']+'|[^>\s]+)/i)) a = a[1].match(/^(["']?)(.*)\1/)[2], c += ' style="' + a + '"'; return c +
" />"
    } return ""
})) : a = a.replace(/<img( [^<>]*(v:shapes|msohtmlclip)[^<>]*)\/?>/ig, function (a, b) {





    var c, d = '<img src="' + A + '" _xhe_temp="true" class="wordImage"'; (c = b.match(/ width\s*=\s*"([^"]+)"/i)) && (d += ' width="' + c[1] + '"'); (c = b.match(/ height\s*=\s*"([^"]+)"/i)) && (d += ' height="' + c[1] + '"'); return d + " />"
}); for (var a = a.replace(/(<(\/?)([\w\-:]+))((?:\s+[\w\-:]+(?:\s*=\s*(?:"[^"]*"|'[^']*'|[^>\s]+))?)*)\s*(\/?>)/g, function (a, c, d, e, f, g) {
    e = e.toLowerCase(); if (e.match(/^(link)$/) && f.match(/file:\/\//i) ||
e.match(/:/) || "span" === e && 2 === b) return ""; d || (f = f.replace(/\s([\w\-:]+)(?:\s*=\s*("[^"]*"|'[^']*'|[^>\s]+))?/ig, function (a, c, d) {
    c = c.toLowerCase(); if (/:/.test(c)) return ""; d = d.match(/^(["']?)(.*)\1/)[2]; if (1 === b) switch (e) {
        case "p": if ("style" === c) return (d = d.replace(/"|&quot;/ig, "'").replace(/\s*([^:]+)\s*:\s*(.*?)(;|$)/ig, function (a, b, c) { return /^(text-align)$/i.test(b) ? b + ":" + c + ";" : "" }).replace(/^\s+|\s+$/g, "")) ? " " + c + '="' + d + '"' : ""; break; case "span": if ("style" === c) return (d = d.replace(/"|&quot;/ig, "'").replace(/\s*([^:]+)\s*:\s*(.*?)(;|$)/ig,
function (a, b, c) { return /^(color|background|font-size|line-height|font-family)$/i.test(b) ? b + ":" + c + ";" : "" }).replace(/^\s+|\s+$/g, "")) ? " " + c + '="' + d + '"' : ""; break; case "table": if (c.match(/^(cellspacing|cellpadding|border|width)$/i)) return a.replace('border="0"', 'border="1"'); break; case "td": if (c.match(/^(rowspan|colspan)$/i)) return a; if ("style" === c) return (d = d.replace(/"|&quot;/ig, "'").replace(/\s*([^:]+)\s*:\s*(.*?)(;|$)/ig, function (a, b, c) { return /^(width|height)$/i.test(b) ? b + ":" + c + ";" : "" }).replace(/^\s+|\s+$/g, "")) ? " " + c + '="' + d + '"' : ""; break; case "a": if (c.match(/^(href)$/i)) return a;
            break; case "font": case "img": return a
    } else if (2 === b) switch (e) { case "td": if (c.match(/^(rowspan|colspan)$/i)) return a; break; case "img": return a } return ""
})); return c + f + g
}), c = 0; 3 > c; c++) a = a.replace(/<([^\s>]+)(\s+[^>]*)?>\s*<\/\1>/g, ""); for (var d = function (a, b, c) { return c }, c = 0; 3 > c; c++) a = a.replace(/<(span|a)>(((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S])*?<\/\1>)*?<\/\1>)*?)<\/\1>/ig, d); for (c = 0; 3 > c; c++) a = a.replace(/<(span|a)>(((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S])*?<\/\1>)*?)<\/\1>/ig,
d); for (c = 0; 3 > c; c++) a = a.replace(/<(span|a)>(((?!<\1(\s+[^>]*?)?>)[\s\S])*?)<\/\1>/ig, d); for (c = 0; 3 > c; c++) a = a.replace(/<font(\s+[^>]+)><font(\s+[^>]+)>/ig, function (a, b, c) { return "<font" + b + c + ">" }); a = a.replace(/(<(\/?)(tr|td)(?:\s+[^>]+)?>)[^<>]+/ig, function (a, b, c, d) { return !c && /^td$/i.test(d) ? a : b })
            } return a
        }; this.cleanHTML = function (a) {
            
            var a = a.replace(/<!?\/?(DOCTYPE|html|body|meta)(\s+[^>]*?)?>/ig, ""), b, a = a.replace(/<head(?:\s+[^>]*?)?>([\s\S]*?)<\/head>/i, function (a, d) {
                b = d.match(/<(script|style)(\s+[^>]*?)?>[\s\S]*?<\/\1>/ig);
                return ""
            }); b && (a = b.join("") + a); a = a.replace(/<\??xml(:\w+)?(\s+[^>]*?)?>([\s\S]*?<\/xml>)?/ig, ""); g.internalScript || (a = a.replace(/<script(\s+[^>]*?)?>[\s\S]*?<\/script>/ig, "")); g.internalStyle || (a = a.replace(/<style(\s+[^>]*?)?>[\s\S]*?<\/style>/ig, "")); if (!g.linkTag || !g.inlineScript || !g.inlineStyle) a = a.replace(/(<(\w+))((?:\s+[\w-]+\s*=\s*(?:"[^"]*"|'[^']*'|[^>\s]+))*)\s*(\/?>)/ig, function (a, b, d, e, j) {
                if (!g.linkTag && "link" === d.toLowerCase()) return ""; g.inlineScript || (e = e.replace(/\s+on(?:click|dblclick|mouse(down|up|move|over|out|enter|leave|wheel)|key(down|press|up)|change|select|submit|reset|blur|focus|load|unload)\s*=\s*("[^"]*"|'[^']*'|[^>\s]+)/ig,
"")); g.inlineStyle || (e = e.replace(/\s+(style|class)\s*=\s*("[^"]*"|'[^']*'|[^>\s]+)/ig, "")); return b + e + j
            }); return a = a.replace(/<\/(strong|b|u|strike|em|i)>((?:\s|<br\/?>|&nbsp;)*?)<\1(\s+[^>]*?)?>/ig, "$2")
        }; this.formatXHTML = function (a, b) {
           
            function c(a) { for (var b = {}, a = a.split(","), c = 0; c < a.length; c++) b[a[c]] = !0; return b } function e(a) { var a = a.toLowerCase(), b = o[a]; return b ? b : a } function i(a, b, c) {
                if (k[a]) for (; E.last() && y[E.last()]; ) g(E.last()); Z[a] && E.last() === a && g(a); (c = D[a] || !!c) || E.push(a); var d = []; d.push("<" +
a); b.replace(x, function (a, b, c, e, f) { b = b.toLowerCase(); d.push(" " + b + '="' + (c ? c : e ? e : f ? f : l[b] ? b : "").replace(/"/g, "'") + '"') }); d.push((c ? " /" : "") + ">"); w(d.join(""), a, !0); "pre" === a && (B = !0)
            } function g(a) { if (a) for (b = E.length - 1; 0 <= b && !(E[b] === a); b--); else var b = 0; if (0 <= b) { for (var c = E.length - 1; c >= b; c--) w("</" + E[c] + ">", E[c]); E.length = b } "pre" === a && (B = !1, v--) } function j(a) { w(d.domEncode(a)) } function p(a) { H.push(a.replace(/^[\s\r\n]+|[\s\r\n]+$/g, "")) } function w(a, c, d) {
                B || (a = a.replace(/(\t*\r?\n\t*)+/g, "")); if (!B &&
!0 === b) if (a.match(/^\s*$/)) H.push(a); else { var e = k[c]; e ? (d && v++, "" === A && v--) : A && v++; ((e ? c : "") !== A || e) && s(); H.push(a); "br" === c && s(); e && (D[c] || !d) && v--; A = e ? c : "" } else H.push(a)
            } function s() { H.push("\r\n"); if (0 < v) for (var a = v; a--; ) H.push("\t") } function r(a, b, c, d) {
                if (!c) return d; var e = "", c = c.replace(/ face\s*=\s*"\s*([^"]*)\s*"/i, function (a, b) { b && (e += "font-family:" + b + ";"); return "" }), c = c.replace(/ size\s*=\s*"\s*(\d+)\s*"/i, function (a, b) {
                    e += "font-size:" + T[(7 < b ? 7 : 1 > b ? 1 : b) - 1].s + ";";
                     return "" })

                    //2015/4/2 West
                    , c = c.replace(/ lineheight\s*=\s*"\s*(\d+)\s*"/i, function (a, b) { e += "line-height:" + LH[(7 < b ? 7 : 1 > b ? 1 : b) - 1].s + ";"; return "" })

                    , c = c.replace(/ color\s*=\s*"\s*([^"]*)\s*"/i,
function (a, b) { b && (e += "color:" + b + ";"); return "" }), c = c.replace(/ style\s*=\s*"\s*([^"]*)\s*"/i, function (a, b) { b && (e += b); return "" }); return (c += ' style="' + e + '"') ? "<span" + c + ">" + d + "</span>" : d
            } var D = c("area,base,basefont,br,col,frame,hr,img,input,isindex,link,meta,param,embed"), k = c("address,applet,blockquote,button,center,dd,dir,div,dl,dt,fieldset,form,frameset,h1,h2,h3,h4,h5,h6,hr,iframe,ins,isindex,li,map,menu,noframes,noscript,ol,p,pre,table,tbody,td,tfoot,th,thead,tr,ul,script"), y = c("a,abbr,acronym,applet,b,basefont,bdo,big,br,button,cite,code,del,dfn,em,font,i,iframe,img,input,ins,kbd,label,map,q,s,samp,script,select,small,span,strike,strong,sub,sup,textarea,tt,u,var"),
Z = c("colgroup,dd,dt,li,options,p,td,tfoot,th,thead,tr"), l = c("checked,compact,declare,defer,disabled,ismap,multiple,nohref,noresize,noshade,nowrap,readonly,selected"), h = c("script,style"), o = { b: "strong", i: "em", s: "del", strike: "del" }, n = /<(?:\/([^\s>]+)|!([^>]*?)|([\w\-:]+)((?:"[^"]*"|'[^']*'|[^"'<>])*)\s*(\/?))>/g, x = /\s*([\w\-:]+)(?:\s*=\s*(?:"([^"]*)"|'([^']*)'|([^\s]+)))?/g, H = [], E = []; E.last = function () { return this[this.length - 1] }; for (var u, C, q = 0, t, z, v = -1, A = "body", B = !1; u = n.exec(a); ) {
                C = u.index; C > q && (q =
a.substring(q, C), t ? z.push(q) : j(q)); q = n.lastIndex; if (C = u[1]) if (C = e(C), t && C === t && (p(z.join("")), z = t = null), !t) { g(C); continue } t ? z.push(u[0]) : (C = u[3]) ? (C = e(C), i(C, u[4], u[5]), h[C] && (t = C, z = [])) : u[2] && H.push(u[0])
            } a.length > q && j(a.substring(q, a.length)); g(); a = H.join(""); H = null; a = a.replace(/<(font)(\s+[^>]*?)?>(((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S])*?<\/\1>)*?<\/\1>)*?)<\/\1>/ig, r); a = a.replace(/<(font)(\s+[^>]*?)?>(((?!<\1(\s+[^>]*?)?>)[\s\S]|<\1(\s+[^>]*?)?>((?!<\1(\s+[^>]*?)?>)[\s\S])*?<\/\1>)*?)<\/\1>/ig,
r); a = a.replace(/<(font)(\s+[^>]*?)?>(((?!<\1(\s+[^>]*?)?>)[\s\S])*?)<\/\1>/ig, r); return a = a.replace(/^(\s*\r?\n)+|(\s*\r?\n)+$/g, "")
        }; this.toggleShowBlocktag = function (a) { na !== a && (na = !na, a = e(l.body), na ? (Q += " showBlocktag", a.addClass("showBlocktag")) : (Q = Q.replace(" showBlocktag", ""), a.removeClass("showBlocktag"))) }; this.toggleSource = function (a) {
            
            if (x !== a) {
                n.find("[cmd=Source]").toggleClass("xheEnabled").toggleClass("xheActive"); var b = l.body, c = e(b), f, i, a = 0, g = ""; if (x) f = d.getSource(), c.html("").removeAttr("scroll").attr("class",
"editMode" + Q), h ? b.contentEditable = "true" : l.designMode = "On", qa && (d._exec("inserthtml", "-"), e("#" + za).show().focus().hide()), g = "\u6e90\u4ee3\u7801"; else {
                    d.pasteHTML('<span id="_xhe_cursor"></span>', !0); f = d.getSource(!0); a = f.indexOf('<span id="_xhe_cursor"></span>'); if (!Ca) a = f.substring(0, a).replace(/\r/g, "").length; f = f.replace(/(\r?\n\s*|)<span id="_xhe_cursor"><\/span>(\s*\r?\n|)/, function (a, b, c) { return b && c ? "\r\n" : b + c }); h ? b.contentEditable = "false" : l.designMode = "Off"; c.attr("scroll", "no").attr("class",
"sourceMode").html('<textarea id="sourceCode" wrap="soft" spellcheck="false" style="width:100%;height:100%" />'); i = e("#sourceCode", c).blur(d.getSource)[0]; g = "\u53ef\u89c6\u5316\u7f16\u8f91"
                } x = !x; d.setSource(f); d.focus(); x ? i.setSelectionRange ? i.setSelectionRange(a, a) : (i = i.createTextRange(), i.move("character", a), i.select()) : d.setTextCursor(); n.find("[cmd=Source]").attr("title", g).find("span").text(g); n.find("[cmd=Source],[cmd=Preview]").toggleClass("xheEnabled"); n.find(".xheButton").not("[cmd=Source],[cmd=Fullscreen],[cmd=About]").toggleClass("xheEnabled").attr("aria-disabled",
x ? !0 : !1); setTimeout(La, 300)
            }
        }; this.showPreview = function () { var a = g.beforeSetSource, b = d.getSource(); a && (b = a(b)); var a = "<html><head>" + P + "<title>\u9884\u89c8</title>" + (F ? '<base href="' + F + '"/>' : "") + "</head><body>" + b + "</body></html>", b = window.screen, b = window.open("", "xhePreview", "toolbar=yes,location=no,status=yes,menubar=yes,scrollbars=yes,resizable=yes,width=" + Math.round(0.9 * b.width) + ",height=" + Math.round(0.8 * b.height) + ",left=" + Math.round(0.05 * b.width)), c = b.document; c.open(); c.write(a); c.close(); b.focus() };
        this.toggleFullscreen = function (a) {
            if (la !== a) {
                var a = e("#" + ja).find(".xheLayout"), b = e("#" + ja), c = jQuery.browser.version, c = h && (6 == c || 7 == c); la ? (c && G.after(b), a.attr("style", Za), B.height(O - n.outerHeight()), e(window).scrollTop(Pa), setTimeout(function () { e(window).scrollTop(Pa) }, 10)) : (c && e("body").append(b), Pa = e(window).scrollTop(), Za = a.attr("style"), a.removeAttr("style"), B.height("100%"), setTimeout(Na, 100)); qa ? (e("#" + za).show().focus().hide(), setTimeout(d.focus, 1)) : c && d.setTextCursor(); la = !la; b.toggleClass("xhe_Fullscreen");
                e("html").toggleClass("xhe_Fullfix"); n.find("[cmd=Fullscreen]").toggleClass("xheActive"); setTimeout(La, 300)
            }
        }; this.showMenu = function (a, b) {
            var c = e('<div class="xheMenu"></div>'), f = a.length, i = []; e.each(a, function (a, b) { "-" === b.s ? i.push('<div class="xheMenuSeparator"></div>') : i.push("<a href=\"javascript:void('" + b.v + '\')" title="' + (b.t ? b.t : b.s) + '" v="' + b.v + '" role="option" aria-setsize="' + f + '" aria-posinset="' + (a + 1) + '" tabindex="0">' + b.s + "</a>") }); c.append(i.join("")); c.click(function (a) {
                a = a.target; if (!e.nodeName(a,
"DIV")) return d.loadBookmark(), b(e(a).closest("a").attr("v")), d.hidePanel(), !1
            }).mousedown(N); d.saveBookmark(); d.showPanel(c)
        }; this.showColor = function (a) {
            var b = e('<div class="xheColor"></div>'), c = [], f = Ta.length, i = 0; e.each(Ta, function (a, b) { 0 === i % 7 && c.push((0 < i ? "</div>" : "") + "<div>"); c.push("<a href=\"javascript:void('" + b + '\')" xhev="' + b + '" title="' + b + '" style="background:' + b + '" role="option" aria-setsize="' + f + '" aria-posinset="' + (i + 1) + '"></a>'); i++ }); c.push("</div>"); b.append(c.join("")); b.click(function (b) {
                b =
b.target; if (e.nodeName(b, "A")) return d.loadBookmark(), a(e(b).attr("xhev")), d.hidePanel(), !1
            }).mousedown(N); d.saveBookmark(); d.showPanel(b)
        }; this.showPastetext = function () {
            var a = e('<div><label for="xhePastetextValue">\u4f7f\u7528\u952e\u76d8\u5feb\u6377\u952e(Ctrl+V)\u628a\u5185\u5bb9\u7c98\u8d34\u5230\u65b9\u6846\u91cc\uff0c\u6309 \u786e\u5b9a</label></div><div><textarea id="xhePastetextValue" wrap="soft" spellcheck="false" style="width:300px;height:100px;" /></div><div style="text-align:right;"><input type="button" id="xheSave" value="\u786e\u5b9a" /></div>'),
b = e("#xhePastetextValue", a); e("#xheSave", a).click(function () { d.loadBookmark(); var a = b.val(); a && d.pasteText(a); d.hidePanel(); return !1 }); d.saveBookmark(); d.showDialog(a)
        }; this.showLink = function () {
            var a = '<div><label for="xheLinkUrl">\u94fe\u63a5\u5730\u5740: </label><input type="text" id="xheLinkUrl" value="http://" class="xheText" /></div><div><label for="xheLinkTarget">\u6253\u5f00\u65b9\u5f0f: </label><select id="xheLinkTarget"><option selected="selected" value="">\u9ed8\u8ba4</option><option value="_blank">\u65b0\u7a97\u53e3</option><option value="_self">\u5f53\u524d\u7a97\u53e3</option><option value="_parent">\u7236\u7a97\u53e3</option></select></div><div style="display:none"><label for="xheLinkText">\u94fe\u63a5\u6587\u5b57: </label><input type="text" id="xheLinkText" value="" class="xheText" /></div><div style="text-align:right;"><input type="button" id="xheSave" value="\u786e\u5b9a" /></div>',
b = ga.find("a[name]").not("[href]"), c = 0 < b.length; if (c) { var f = []; b.each(function () { var a = e(this).attr("name"); f.push('<option value="#' + a + '">' + a + "</option>") }); a = a.replace(/(<div><label for="xheLinkTarget)/, '<div><label for="xheLinkAnchor">\u9875\u5185\u951a\u70b9: </label><select id="xheLinkAnchor"><option value="">\u672a\u9009\u62e9</option>' + f.join("") + "</select></div>$1") } var a = e(a), i = d.getParent("a"), m = e("#xheLinkText", a), j = e("#xheLinkUrl", a), p = e("#xheLinkTarget", a), b = e("#xheSave", a), w = d.getSelect();
            c && a.find("#xheLinkAnchor").change(function () { var a = e(this).val(); "" != a && j.val(a) }); if (1 === i.length) { if (!i.attr("href")) return v = null, d.exec("Anchor"); j.val(L(i, "href")); p.attr("value", i.attr("target")) } else "" === w && m.val(g.defLinkText).closest("div").show(); g.upLinkUrl && d.uploadInit(j, g.upLinkUrl, g.upLinkExt); b.click(function () {
                d.loadBookmark(); var a = j.val(); ("" === a || 0 === i.length) && d._exec("unlink"); if ("" !== a && "http://" !== a) {
                    var b = a.split(" "), c = p.val(), f = m.val(); if (1 < b.length) {
                        d._exec("unlink");
                        w = d.getSelect(); var g = '<a href="xhe_tmpurl"', Z = []; "" !== c && (g += ' target="' + c + '"'); for (var g = g + ">xhe_tmptext</a>", f = "" !== w ? w : f ? f : a, h = 0, pb = b.length; h < pb; h++) a = b[h], "" !== a && (a = a.split("||"), c = g, c = c.replace("xhe_tmpurl", a[0]), c = c.replace("xhe_tmptext", a[1] ? a[1] : f), Z.push(c)); d.pasteHTML(Z.join("&nbsp;"))
                    } else a = b[0].split("||"), f || (f = a[0]), f = a[1] ? a[1] : "" !== w ? "" : f ? f : a[0], 0 === i.length ? (f ? d.pasteHTML('<a href="#xhe_tmpurl">' + f + "</a>") : d._exec("createlink", "#xhe_tmpurl"), i = e('a[href$="#xhe_tmpurl"]', l)) : f &&
!R && i.text(f), L(i, "href", a[0]), "" !== c ? i.attr("target", c) : i.removeAttr("target")
                } d.hidePanel(); return !1
            }); d.saveBookmark(); d.showDialog(a)
        }; this.showAnchor = function () {
            var a = e('<div><label for="xheAnchorName">\u951a\u70b9\u540d\u79f0: </label><input type="text" id="xheAnchorName" value="" class="xheText" /></div><div style="text-align:right;"><input type="button" id="xheSave" value="\u786e\u5b9a" /></div>'), b = d.getParent("a"), c = e("#xheAnchorName", a), f = e("#xheSave", a); if (1 === b.length) {
                if (b.attr("href")) return v =
null, d.exec("Link"); c.val(b.attr("name"))
            } f.click(function () { d.loadBookmark(); var a = c.val(); a ? 0 === b.length ? d.pasteHTML('<a name="' + a + '"></a>') : b.attr("name", a) : 1 === b.length && b.remove(); d.hidePanel(); return !1 }); d.saveBookmark(); d.showDialog(a)
        }; this.showImg = function () {
            var a = e('<div><label for="xheImgUrl">\u56fe\u7247\u6587\u4ef6: </label><input type="text" id="xheImgUrl" value="http://" class="xheText" /></div><div><div><label for="xheImgAlt">\u66ff\u6362\u6587\u672c: </label><input type="text" id="xheImgAlt" /></div><div><label for="xheImgAlign">\u5bf9\u9f50\u65b9\u5f0f: </label><select id="xheImgAlign"><option selected="selected" value="">\u9ed8\u8ba4</option><option value="left">\u5de6\u5bf9\u9f50</option><option value="right">\u53f3\u5bf9\u9f50</option><option value="top">\u9876\u7aef</option><option value="middle">\u5c45\u4e2d</option><option value="baseline">\u57fa\u7ebf</option><option value="bottom">\u5e95\u8fb9</option></select></div><div><label for="xheImgWidth">\u5bbd\u3000\u3000\u5ea6: </label><input type="text" id="xheImgWidth" style="width:40px;" /> <label for="xheImgHeight">\u9ad8\u3000\u3000\u5ea6: </label><input type="text" id="xheImgHeight" style="width:40px;" /></div><div><label for="xheImgBorder">\u8fb9\u6846\u5927\u5c0f: </label><input type="text" id="xheImgBorder" style="width:40px;" /></div><div><label for="xheImgHspace">\u6c34\u5e73\u95f4\u8ddd: </label><input type="text" id="xheImgHspace" style="width:40px;" /> <label for="xheImgVspace">\u5782\u76f4\u95f4\u8ddd: </label><input type="text" id="xheImgVspace" style="width:40px;" /></div><div style="text-align:right;"><input type="button" id="xheSave" value="\u786e\u5b9a" /></div>'),
b = d.getParent("img"), c = e("#xheImgUrl", a), f = e("#xheImgAlt", a), i = e("#xheImgAlign", a), m = e("#xheImgWidth", a), j = e("#xheImgHeight", a), p = e("#xheImgBorder", a), w = e("#xheImgVspace", a), s = e("#xheImgHspace", a), r = e("#xheSave", a); if (1 === b.length) { c.val(L(b, "src")); f.val(b.attr("alt")); i.val(b.attr("align")); m.val(b.attr("width")); j.val(b.attr("height")); p.val(b.attr("border")); var D = b.attr("vspace"), k = b.attr("hspace"); w.val(0 >= D ? "" : D); s.val(0 >= k ? "" : k) } g.upImgUrl && d.uploadInit(c, g.upImgUrl, g.upImgExt); r.click(function () {
    d.loadBookmark();
    var a = c.val(); if ("" !== a && "http://" !== a) {

        /*alert("插入图片的时候触发"); simmer  */
        var g = a.split(" "), k = f.val(), D = i.val(), r = m.val(), h = j.val(), o = p.val(), q = w.val(), n = s.val(); if (1 < g.length) {
            var u = '<img src="xhe_tmpurl"', t = []; "" !== k && (u += ' alt="' + k + '"'); "" !== D && (u += ' align="' + D + '"'); "" !== r && (u += ' width="' + r + '"'); "" !== h && (u += ' height="' + h + '"'); "" !== o && (u += ' border="' + o + '"'); "" !== q && (u += ' vspace="' + q + '"'); "" !== n && (u += ' hspace="' + n + '"'); var u = u + " />", v; for (v in g) a = g[v], "" !== a && (a = a.split("||"), k = u, k = k.replace("xhe_tmpurl", a[0]), a[1] && (k = '<a href="' +
a[1] + '" target="_blank">' + k + "</a>"), t.push(k)); d.pasteHTML(t.join("&nbsp;"))
        } else 1 === g.length && (a = g[0], "" !== a && (a = a.split("||"), 0 === b.length && (d.pasteHTML('<img src="' + a[0] + '#xhe_tmpurl" />'), b = e('img[src$="#xhe_tmpurl"]', l)), L(b, "src", a[0]), "" !== k && b.attr("alt", k), "" !== D ? b.attr("align", D) : b.removeAttr("align"), "" !== r ? b.attr("width", r) : b.removeAttr("width"), "" !== h ? b.attr("height", h) : b.removeAttr("height"), "" !== o ? b.attr("border", o) : b.removeAttr("border"), "" !== q ? b.attr("vspace", q) : b.removeAttr("vspace"),
"" !== n ? b.attr("hspace", n) : b.removeAttr("hspace"), a[1] && (g = b.parent("a"), 0 === g.length && (b.wrap("<a></a>"), g = b.parent("a")), L(g, "href", a[1]), g.attr("target", "_blank"))))
    } else 1 === b.length && b.remove(); d.hidePanel(); return !1
}); d.saveBookmark(); d.showDialog(a)
        }; this.showEmbed = function (a, b, c, f, g, m, j) {
            var b = e(b), p = d.getParent('object[type="' + c + '"],object[classid="' + f + '"]'), w = e("#xhe" + a + "Url", b), s = e("#xhe" + a + "Width", b), r = e("#xhe" + a + "Height", b), a = e("#xheSave", b); m && d.uploadInit(w, m, j); 1 === p.length && (w.val(L(p,
"src")), s.val(p.attr("width")), r.val(p.attr("height")));
            a.click(function () {
                d.loadBookmark(); var a = w.val(); if ("" !== a && "http://" !== a) {
                    var b = s.val(), j = r.val(), m = /^\d+%?$/; m.test(b) || (b = 412); m.test(j) || (j = 300);

                    var h = "";
                    var m = a.split(" ");
                    var pageDir = '/editor/'; //设置编辑器的相对路径  simmer 调用播放器配置
                    if (1 < m.length) {

                        h = '<object  classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"  width="xhe_width" height="xhe_height"></param><param name="movie" value="' + pageDir + 'mediaplayer/player.swf" /><param name="allowfullscreen" value="true" /><param name="allowscriptaccess" value="always" />        <param name="fullscreen" value="true" /><param name="flashvars" value="file=xhe_tmpurl&image=' + pageDir + 'mediaplayer/flv.gif&autostart=false" />		<object  data="' + pageDir + 'mediaplayer/player.swf"   type="application/x-shockwave-flash"   width="xhe_width" height="xhe_height">			<param name="movie" value="mediaplayer/player.swf" />			<param name="allowfullscreen" value="true" />			<param name="allowscriptaccess" value="always" />			<param name="flashvars" value="file=xhe_tmpurl&image=' + pageDir + 'mediaplayer/flv.gif&autostart=false" />		</object>	</object>';

                        var o, q = [], n; for (n in m) a = m[n].split("||"), o = h, o = o.replace("xhe_tmpurl", a[0]).replace("xhe_tmpurl", a[0]), o = o.replace("xhe_width", a[1] ? a[1] : b).replace("xhe_width", a[1] ? a[1] : b), o = o.replace("xhe_height", a[2] ? a[2] : j).replace("xhe_height", a[2] ? a[2] : j), "" !==
a && q.push(o); d.pasteHTML(q.join("&nbsp;"))
                    }
                    else {

                        h = '<object  classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"  width="xhe_width" height="xhe_height"><param name="movie" value="' + pageDir + 'mediaplayer/player.swf" /><param name="allowfullscreen" value="true" /><param name="allowscriptaccess" value="always" /><param name="fullscreen" value="true" /><param name="flashvars" value="file=xhe_tmpurl&image=' + pageDir + 'mediaplayer/flv.gif&autostart=false" /><object  data="' + pageDir + 'mediaplayer/player.swf"   type="application/x-shockwave-flash"   width="xhe_width" height="xhe_height"><param name="movie" value="mediaplayer/player.swf" /><param name="allowfullscreen" value="true" /><param name="allowscriptaccess" value="always" /><param name="flashvars" value="file=xhe_tmpurl&image=' + pageDir + 'mediaplayer/flv.gif&autostart=false" /></object></object>';

                        1 === m.length && (a = m[0].split("||"), 0 === p.length && (d.pasteHTML(h.replace("xhe_tmpurl", a[0]).replace("xhe_width", a[1] ? a[1] : b).replace("xhe_height", a[2] ? a[2] : j).replace("xhe_tmpurl", a[0]).replace("xhe_width", a[1] ? a[1] : b).replace("xhe_height", a[2] ? a[2] : j))))


                    }

                } else 1 === p.length && p.remove(); d.hidePanel(); return !1
            });
            d.saveBookmark(); d.showDialog(b)
        }; this.showEmot = function (a) {
            var b = e('<div class="xheEmot"></div>'), a = a ? a : Ra ? Ra : "default", c = Aa[a], f = Y + a + "/", g = 0, m = [], j = "", j = c.width, p = c.height, w = c.line,
s = c.count, c = c.list; if (s) for (c = 1; c <= s; c++) g++, m.push("<a href=\"javascript:void('" + c + '\')" style="background-image:url(' + f + c + '.gif);" emot="' + a + "," + c + '" xhev="" title="' + c + '" role="option">&nbsp;</a>'), 0 === g % w && m.push("<br />"); else e.each(c, function (b, c) { g++; m.push("<a href=\"javascript:void('" + c + '\')" style="background-image:url(' + f + b + '.gif);" emot="' + a + "," + b + '" title="' + c + '" xhev="' + c + '" role="option">&nbsp;</a>'); 0 === g % w && m.push("<br />") }); var s = w * (j + 12), c = Math.ceil(g / w) * (p + 12), r = 0.75 * s; c <= r &&
(r = ""); j = e("<style>" + (r ? ".xheEmot div{width:" + (s + 20) + "px;height:" + r + "px;}" : "") + ".xheEmot div a{width:" + j + "px;height:" + p + "px;}</style><div>" + m.join("") + "</div>").click(function (a) { var a = a.target, b = e(a); if (e.nodeName(a, "A")) return d.loadBookmark(), d.pasteHTML('<img emot="' + b.attr("emot") + '" alt="' + b.attr("xhev") + '">'), d.hidePanel(), !1 }).mousedown(N); b.append(j); var h = 0, k = ['<ul role="tablist">']; e.each(Aa, function (b, c) {
    h++; k.push("<li" + (a === b ? ' class="cur"' : "") + ' role="presentation"><a href="javascript:void(\'' +
c.name + '\')" group="' + b + '" role="tab" tabindex="0">' + c.name + "</a></li>")
}); 1 < h && (k.push('</ul><br style="clear:both;" />'), j = e(k.join("")).click(function (a) { Ra = e(a.target).attr("group"); d.exec("Emot"); return !1 }).mousedown(N), b.append(j)); d.saveBookmark(); d.showPanel(b)
        }; this.showTable = function () {
            var a = e('<div><label for="xheTableRows">\u884c\u3000\u3000\u6570: </label><input type="text" id="xheTableRows" style="width:40px;" value="3" /> <label for="xheTableColumns">\u5217\u3000\u3000\u6570: </label><input type="text" id="xheTableColumns" style="width:40px;" value="2" /></div><div><label for="xheTableHeaders">\u6807\u9898\u5355\u5143: </label><select id="xheTableHeaders"><option selected="selected" value="">\u65e0</option><option value="row">\u7b2c\u4e00\u884c</option><option value="col">\u7b2c\u4e00\u5217</option><option value="both">\u7b2c\u4e00\u884c\u548c\u7b2c\u4e00\u5217</option></select></div><div><label for="xheTableWidth">\u5bbd\u3000\u3000\u5ea6: </label><input type="text" id="xheTableWidth" style="width:40px;" value="200" /> <label for="xheTableHeight">\u9ad8\u3000\u3000\u5ea6: </label><input type="text" id="xheTableHeight" style="width:40px;" value="" /></div><div><label for="xheTableBorder">\u8fb9\u6846\u5927\u5c0f: </label><input type="text" id="xheTableBorder" style="width:40px;" value="1" /></div><div><label for="xheTableCellSpacing">\u8868\u683c\u95f4\u8ddd: </label><input type="text" id="xheTableCellSpacing" style="width:40px;" value="1" /> <label for="xheTableCellPadding">\u8868\u683c\u586b\u5145: </label><input type="text" id="xheTableCellPadding" style="width:40px;" value="1" /></div><div><label for="xheTableAlign">\u5bf9\u9f50\u65b9\u5f0f: </label><select id="xheTableAlign"><option selected="selected" value="">\u9ed8\u8ba4</option><option value="left">\u5de6\u5bf9\u9f50</option><option value="center">\u5c45\u4e2d</option><option value="right">\u53f3\u5bf9\u9f50</option></select></div><div><label for="xheTableCaption">\u8868\u683c\u6807\u9898: </label><input type="text" id="xheTableCaption" /></div><div style="text-align:right;"><input type="button" id="xheSave" value="\u786e\u5b9a" /></div>'),
b = e("#xheTableRows", a), c = e("#xheTableColumns", a), f = e("#xheTableHeaders", a), g = e("#xheTableWidth", a), m = e("#xheTableHeight", a), j = e("#xheTableBorder", a), p = e("#xheTableCellSpacing", a), h = e("#xheTableCellPadding", a), s = e("#xheTableAlign", a), r = e("#xheTableCaption", a); e("#xheSave", a).click(function () {
    d.loadBookmark(); var a = r.val(), e = j.val(), l = b.val(), o = c.val(), q = f.val(), n = g.val(), t = m.val(), v = p.val(), z = h.val(), x = s.val(), e = "<table" + ("" !== e ? ' border="' + e + '"' : "") + ("" !== n ? ' width="' + n + '"' : "") + ("" !== t ? ' height="' +
t + '"' : "") + ("" !== v ? ' cellspacing="' + v + '"' : "") + ("" !== z ? ' cellpadding="' + z + '"' : "") + ("" !== x ? ' align="' + x + '"' : "") + ">"; "" !== a && (e += "<caption>" + a + "</caption>"); if ("row" === q || "both" === q) { e += "<tr>"; for (a = 0; a < o; a++) e += '<th scope="col"></th>'; e += "</tr>"; l-- } e += "<tbody>"; for (a = 0; a < l; a++) { e += "<tr>"; for (n = 0; n < o; n++) e = 0 === n && ("col" === q || "both" === q) ? e + '<th scope="row"></th>' : e + "<td></td>"; e += "</tr>" } d.pasteHTML(e + "</tbody></table>"); d.hidePanel(); return !1
}); d.saveBookmark(); d.showDialog(a)
        }; this.showAbout = function () {
            var a =
e('<div style="font:12px Arial;width:245px;word-wrap:break-word;word-break:break-all;outline:none;" role="dialog" tabindex="-1"><p><span style="font-size:20px;color:#1997DF;">ET</span><br />v1.0.0 (build 140922)</p><p>Copyright &copy; <a href="http://www.etmanage.com/" target="_blank">http://www.etmanage.com/</a>. All rights reserved.</p></div>');
            a.find("p").attr("role", "presentation"); d.showDialog(a, !0); setTimeout(function () { a.focus() }, 100)
        }; this.addShortcuts = function (a, b) { a = a.toLowerCase(); ma[a] === $ && (ma[a] = []); ma[a].push(b) }; this.delShortcuts = function (a) { delete ma[a] }; this.uploadInit = function (a, b, c) {
            function f(b) { M(b, "string") && (b = [b]); var c = !1, d, e = b.length, f, i = []; (d = g.onUpload) && d(b); for (d = 0; d < e; d++) f = b[d], f = M(f, "string") ? f : f.url, "!" === f.substr(0, 1) && (c = !0, f = f.substr(1)), i.push(f); a.val(i.join(" ")); c && a.closest(".xheDialog").find("#xheSave").click() }
            var i = e('<span class="xheUpload"><input type="text" style="visibility:hidden;" tabindex="-1" /><input type="button" value="' + g.upBtnText + '" class="xheBtn" tabindex="-1" /></span>'), m = e(".xheBtn", i), j = g.html5Upload, p = j ? g.upMultiple : 1; a.after(i); m.before(a); b = b.replace(/{editorRoot}/ig, A); if ("!" === b.substr(0, 1)) m.click(function () { d.showIframeModal("\u4e0a\u4f20\u6587\u4ef6", b.substr(1), f, null, null) }); else {
                i.append('<input type="file"' + (1 < p ? ' multiple=""' : "") + ' class="xheFile" size="13" name="filedata" tabindex="-1" />');
                var h = e(".xheFile", i); h.change(function () { d.startUpload(h[0], b, c, f) }); setTimeout(function () { a.closest(".xheDialog").bind("dragenter dragover", N).bind("drop", function (a) { var a = a.originalEvent.dataTransfer, e; j && a && (e = a.files) && 0 < e.length && d.startUpload(e, b, c, f); return !1 }) }, 10)
            }
        }; this.startUpload = function (a, b, c, f) {
            function i(a, c) {
                var e = Object, g = !1; try { e = eval("(" + a + ")") } catch (i) { } e.err === $ || e.msg === $ ? alert(b + " \u4e0a\u4f20\u63a5\u53e3\u53d1\u751f\u9519\u8bef\uff01\r\n\r\n\u8fd4\u56de\u7684\u9519\u8bef\u5185\u5bb9\u4e3a: \r\n\r\n" +
a) : e.err ? alert(e.err) : (m.push(e.msg), g = !0); (!g || c) && d.removeModal(); c && g && f(m); return g
            } var m = [], j = g.html5Upload, p = j ? g.upMultiple : 1, h, s = e('<div style="padding:22px 0;text-align:center;line-height:30px;">\u6587\u4ef6\u4e0a\u4f20\u4e2d\uff0c\u8bf7\u7a0d\u5019\u2026\u2026<br /></div>'), r = '<img src="' + ua + 'img/loading.gif">'; if (Ca || !j || a.nodeType && (!(h = a.files) || !h[0].name)) { if (!Wa(a.value, c)) return; s.append(r); c = new d.html4Upload(a, b, i) } else {
                h || (h = a); a = h.length; if (a > p) {
                    alert("\u8bf7\u4e0d\u8981\u4e00\u6b21\u4e0a\u4f20\u8d85\u8fc7" +
p + "\u4e2a\u6587\u4ef6"); return
                } for (p = 0; p < a; p++) if (!Wa(h[p].name, c)) return; var l = e('<div class="xheProgress"><div><span>0%</span></div></div>'); s.append(l); c = new d.html5Upload("filedata", h, b, i, function (a) { if (0 <= a.loaded) { var b = Math.round(100 * a.loaded / a.total) + "%"; e("div", l).css("width", b); e("span", l).text(b + " ( " + Xa(a.loaded) + " / " + Xa(a.total) + " )") } else l.replaceWith(r) })
            } d.showModal("\u6587\u4ef6\u4e0a\u4f20\u4e2d(Esc\u53d6\u6d88\u4e0a\u4f20)", s, 320, 150); c.start()
        }; this.html4Upload = function (a, b,
c) {
            var d = "jUploadFrame" + (new Date).getTime(), g = this, h = e('<iframe name="' + d + '" class="xheHideArea" />').appendTo("body"), j = e('<form action="' + b + '" target="' + d + '" method="post" enctype="multipart/form-data" class="xheHideArea"></form>').appendTo("body"), p = e(a), l = p.clone().attr("disabled", "true"); p.before(l).appendTo(j); this.remove = function () { null !== g && (l.before(p).remove(), h.remove(), j.remove(), g = null) }; this.onLoad = function () {
                var a = h[0].contentWindow.document, b = e(a.body).text(); a.write(""); g.remove();
                c(b, !0)
            }; this.start = function () { j.submit(); h.load(g.onLoad) }; return this
        }; this.html5Upload = function (a, b, c, d, e) {
            function g(b, c, d, e) {
                h = new XMLHttpRequest; upload = h.upload; h.onreadystatechange = function () { 4 === h.readyState && d(h.responseText) }; upload ? upload.onprogress = function (a) { e(a.loaded) } : e(-1); h.open("POST", c); h.setRequestHeader("Content-Type", "application/octet-stream"); h.setRequestHeader("Content-Disposition", 'attachment; name="' + encodeURIComponent(a) + '"; filename="' + encodeURIComponent(b.name) + '"');
                h.sendAsBinary && b.getAsBinary ? h.sendAsBinary(b.getAsBinary()) : h.send(b)
            } function j(a) { e && e({ loaded: r + a, total: o }) } for (var h, l = 0, s = b.length, r = 0, o = 0, k = this, n = 0; n < s; n++) o += b[n].size; this.remove = function () { h && (h.abort(), h = null) }; this.uploadNext = function (a) { a && (r += b[l - 1].size, j(0)); (!a || a && !0 === d(a, l === s)) && l < s && g(b[l++], c, k.uploadNext, function (a) { j(a) }) }; this.start = function () { k.uploadNext() }
        }; this.showIframeModal = function (a, b, c, f, g, h) {
            function j() {
                try {
                    r.callback = l, r.unloadme = d.removeModal, e(r.document).keydown(I),
n = r.name
                } catch (a) { }
            } function l(a) { r.document.write(""); d.removeModal(); null != a && c(a) } var b = e('<iframe frameborder="0" src="' + b.replace(/{editorRoot}/ig, A) + (/\?/.test(b) ? "&" : "?") + "parenthost=" + location.host + '" style="width:100%;height:100%;display:none;" /><div class="xheModalIfmWait"></div>'), o = b.eq(0), s = b.eq(1); d.showModal(a, b, f, g, h); var r = o[0].contentWindow, n; j(); o.load(function () {
                j(); if (n) { var a = !0; try { n = eval("(" + unescape(n) + ")") } catch (b) { a = !1 } if (a) return l(n) } s.is(":visible") && (o.show().focus(),
s.remove())
            })
        }; this.showModal = function (a, b, c, f, i) {
            if (ta) return !1; d.panelState = S; S = !1; ea = g.layerShadow; c = c ? c : g.modalWidth; f = f ? f : g.modalHeight; K = e('<div class="xheModal" style="width:' + (c - 1) + "px;height:" + f + "px;margin-left:-" + Math.ceil(c / 2) + "px;" + (h && 7 > pa ? "" : "margin-top:-" + Math.ceil(f / 2) + "px") + '">' + (g.modalTitle ? '<div class="xheModalTitle"><span class="xheModalClose" title="\u5173\u95ed (Esc)" tabindex="0" role="button"></span>' + a + "</div>" : "") + '<div class="xheModalContent"></div></div>').appendTo("body");
            Fa = e('<div class="xheModalOverlay"></div>').appendTo("body"); 0 < ea && (Ea = e('<div class="xheModalShadow" style="width:' + K.outerWidth() + "px;height:" + K.outerHeight() + "px;margin-left:-" + (Math.ceil(c / 2) - ea - 2) + "px;" + (h && 7 > pa ? "" : "margin-top:-" + (Math.ceil(f / 2) - ea - 2) + "px") + '"></div>').appendTo("body")); e(".xheModalContent", K).css("height", f - (g.modalTitle ? e(".xheModalTitle").outerHeight() : 0)).html(b); h && 6 === pa && (Ga = e("select:visible").css("visibility", "hidden")); e(".xheModalClose", K).click(d.removeModal); Fa.show();
            0 < ea && Ea.show(); K.show(); setTimeout(function () { K.find("a,input[type=text],textarea").filter(":visible").filter(function () { return "hidden" !== e(this).css("visibility") }).eq(0).focus() }, 10); ta = !0; Ha = i
        }; this.removeModal = function () { Ga && Ga.css("visibility", "visible"); K.html("").remove(); 0 < ea && Ea.remove(); Fa.remove(); Ha && Ha(); ta = !1; S = d.panelState }; this.showDialog = function (a, b) {
            var c = e('<div class="xheDialog"></div>'), f = e(a), i = e("#xheSave", f); if (1 === i.length) {
                f.find("input[type=text],select").keypress(function (a) {
                    if (13 ===
a.which) return i.click(), !1
                }); f.find("textarea").keydown(function (a) { if (a.ctrlKey && 13 === a.which) return i.click(), !1 }); i.after(' <input type="button" id="xheCancel" value="\u53d6\u6d88" />'); e("#xheCancel", f).click(d.hidePanel); if (!g.clickCancelDialog) { sa = !1; var h = e('<div class="xheFixCancel"></div>').appendTo("body").mousedown(N), j = B.offset(); h.css({ left: j.left, top: j.top, width: B.outerWidth(), height: B.outerHeight() }) } c.mousedown(function () { oa = !0 })
            } c.append(f); d.showPanel(c, b)
        }; this.showPanel = function (a,
b) {
           
            if (!v.target) return !1; t.html("").append(a).css("left", -999).css("top", -999); da = e(v.target).closest("a").addClass("xheActive"); var c = da.offset(), d = c.left, c = c.top, c = c + (da.outerHeight() - 1); ca.css({ left: d + 1, top: c, width: da.width() }).show(); var i = document.documentElement, h = document.body; if (d + t.outerWidth() > (window.pageXOffset || i.scrollLeft || h.scrollLeft) + (i.clientWidth || h.clientWidth)) d -= t.outerWidth() - da.outerWidth(); i = g.layerShadow; 0 < i && ba.css({ left: d + i, top: c + i, width: t.outerWidth(), height: t.outerHeight() }).show();
            if ((i = e("#" + ja).offsetParent().css("zIndex")) && !isNaN(i)) ba.css("zIndex", parseInt(i, 10) + 1), t.css("zIndex", parseInt(i, 10) + 2), ca.css("zIndex", parseInt(i, 10) + 3); t.css({ left: d, top: c }).show(); b || setTimeout(function () { t.find("a,input[type=text],textarea").filter(":visible").filter(function () { return "hidden" !== e(this).css("visibility") }).eq(0).focus() }, 10); Qa = S = !0
        }; this.hidePanel = function () {
            S && (da.removeClass("xheActive"), ba.hide(), ca.hide(), t.hide(), S = !1, sa || (e(".xheFixCancel").remove(), sa = !0), Qa = oa = !1,
X = null, d.focus(), d.loadBookmark())
        }; this.exec = function (a) {
           
            d.hidePanel(); var b = ka[a]; if (!b) return !1; if (null === v) { v = {}; var c = n.find(".xheButton[cmd=" + a + "]"); if (1 === c.length) v.target = c } if (b.e) b.e.call(d); else {
                switch (a = a.toLowerCase(), a) {


                    case "cut": try { if (l.execCommand(a), !l.queryCommandSupported(a)) throw "Error"; } catch (f) { alert("\u60a8\u7684\u6d4f\u89c8\u5668\u5b89\u5168\u8bbe\u7f6e\u4e0d\u5141\u8bb8\u4f7f\u7528\u526a\u5207\u64cd\u4f5c\uff0c\u8bf7\u4f7f\u7528\u952e\u76d8\u5feb\u6377\u952e(Ctrl + X)\u6765\u5b8c\u6210") } break;
                    case "copy": try { if (l.execCommand(a), !l.queryCommandSupported(a)) throw "Error"; } catch (i) { alert("\u60a8\u7684\u6d4f\u89c8\u5668\u5b89\u5168\u8bbe\u7f6e\u4e0d\u5141\u8bb8\u4f7f\u7528\u590d\u5236\u64cd\u4f5c\uff0c\u8bf7\u4f7f\u7528\u952e\u76d8\u5feb\u6377\u952e(Ctrl + C)\u6765\u5b8c\u6210") } break; case "paste": try { if (l.execCommand(a), !l.queryCommandSupported(a)) throw "Error"; } catch (m) { alert("\u60a8\u7684\u6d4f\u89c8\u5668\u5b89\u5168\u8bbe\u7f6e\u4e0d\u5141\u8bb8\u4f7f\u7528\u7c98\u8d34\u64cd\u4f5c\uff0c\u8bf7\u4f7f\u7528\u952e\u76d8\u5feb\u6377\u952e(Ctrl + V)\u6765\u5b8c\u6210") } break;
                    case "pastetext": window.clipboardData ? d.pasteText(window.clipboardData.getData("Text", !0)) : d.showPastetext(); break;
                    case "blocktag": var j = []; e.each(gb, function (a, b) { j.push({ s: "<" + b.n + ">" + b.t + "</" + b.n + ">", v: "<" + b.n + ">", t: b.t }) }); d.showMenu(j, function (a) { d._exec("formatblock", a) }); break;
                    case "fontface": var o = []; e.each(hb, function (a, b) { b.c = b.c ? b.c : b.n; o.push({ s: '<span style="font-family:' + b.c + '">' + b.n + "</span>", v: b.c, t: b.n }) }); d.showMenu(o, function (a) { d._exec("fontname", a) }); break;



                    case "fontsize": var q =
    []; e.each(T, function (a, b) { q.push({ s: '<span style="font-size:' + b.s + ';">' + b.t + "(" + b.s + ")</span>", v: a + 1, t: b.t }) });
                        d.showMenu(q, function (a) { d._exec("fontsize", a) }); break;
                        //2015/4/2 West 修改

                    case "lineheight": var lll = []; e.each(LH, function (a, b) { b.c = b.c ? b.c : b.n; lll.push({ s: '<span style="line-height:' + b.s + '" >' + b.n + "</span>", v: b.s, t: b.n }) }); d.showMenu(lll, function (a) {
                        
                       //增加行距
                        d.pasteHTML("<label style='line-height:" + a + ";'>" + d.getSelect(a) + "</label>");
                    });
                         break;

                    case "fontcolor": d.showColor(function (a) { d._exec("forecolor", a) }); break;
                    case "backcolor": d.showColor(function (a) { h ? d._exec("backcolor", a) : (Ka(!0), d._exec("hilitecolor", a), Ka(!1)) }); break; case "align": d.showMenu(ib, function (a) { d._exec(a) }); break; case "list": d.showMenu(jb, function (a) { d._exec(a) }); break; case "link": d.showLink(); break; case "anchor": d.showAnchor();
                        break; case "img": d.showImg(); break; case "flash":
                            d.showEmbed("Flash", '<div><label for="xheFlashUrl">\u52a8\u753b\u6587\u4ef6: </label><input type="text" id="xheFlashUrl" value="http://" class="xheText" /></div><div><label for="xheFlashWidth">\u5bbd\u3000\u3000\u5ea6: </label><input type="text" id="xheFlashWidth" style="width:40px;" value="480" /> <label for="xheFlashHeight">\u9ad8\u3000\u3000\u5ea6: </label><input type="text" id="xheFlashHeight" style="width:40px;" value="400" /></div><div style="text-align:right;"><input type="button" id="xheSave" value="\u786e\u5b9a" /></div>',
        "application/x-shockwave-flash", "clsid:d27cdb6e-ae6d-11cf-96b8-4445535400000", ' wmode="opaque" quality="high" menu="false" play="true" loop="true" allowfullscreen="true"', g.upFlashUrl, g.upFlashExt);

                            break;
                    case "media":
                        d.showEmbed("Media", '<div><label for="xheMediaUrl">\u5a92\u4f53\u6587\u4ef6: </label><input type="text" id="xheMediaUrl" value="http://" class="xheText" /></div><div><label for="xheMediaWidth">\u5bbd\u3000\u3000\u5ea6: </label><input type="text" id="xheMediaWidth" style="width:40px;" value="480" /> <label for="xheMediaHeight">\u9ad8\u3000\u3000\u5ea6: </label><input type="text" id="xheMediaHeight" style="width:40px;" value="400" /></div><div style="text-align:right;"><input type="button" id="xheSave" value="\u786e\u5b9a" /></div>',
    "application/x-mplayer2", "clsid:6bf52a52-394a-11d3-b153-00c04f79faa6", '', g.upMediaUrl, g.upMediaExt); break;
                    case "hr": d.pasteHTML("<hr />"); break; case "emot": d.showEmot(); break; case "table": d.showTable(); break; case "source": d.toggleSource(); break; case "preview": d.showPreview(); break; case "print": W.print(); break; case "fullscreen": d.toggleFullscreen(); break; case "about": d.showAbout(); break; default: d._exec(a)
                }
            } v = null
        }; this._exec = function (a, b, c) {
          
            c || d.focus(); return b !==
$ ? l.execCommand(a, !1, b) : l.execCommand(a, !1, null)
        }
    }; ra.settings = { skin: "nostyle", tools: "full", clickCancelDialog: !0, linkTag: !1, internalScript: !1, inlineScript: !1, internalStyle: !0, inlineStyle: !0, showBlocktag: !1, forcePtag: !0, upLinkExt: "zip,rar,txt", upImgExt: "jpg,jpeg,gif,png", upFlashExt: "swf", upMediaExt: "flv,mp4,mp3,mid", modalWidth: 350, modalHeight: 220, modalTitle: !0, defLinkText: "\u70b9\u51fb\u6253\u5f00\u94fe\u63a5", layerShadow: 3, emotMark: !1, upBtnText: "\u4e0a\u4f20", cleanPaste: 1, hoverExecDelay: 100, html5Upload: !0,
        upMultiple: 99
    }; window.xheditor = ra; e(function () { e.fn.oldVal = e.fn.val; e.fn.val = function (e) { var h = this, q; return e === $ ? h[0] && (q = h[0].xheditor) ? q.getSource() : h.oldVal() : h.each(function () { (q = this.xheditor) ? q.setSource(e) : h.oldVal(e) }) }; e("textarea").each(function () { var h = e(this), o = h.attr("class"); if (o && (o = o.match(/(?:^|\s)xheditor(?:\-(m?full|simple|mini))?(?:\s|$)/i))) h.xheditor(o[1] ? { tools: o[1]} : null) }) })
})(jQuery);


