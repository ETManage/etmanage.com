/*
创建人：刘宗威
创建日：2015-03-13
文件名：common.js
说明文：通用方法JS库
*/

/*字符串处理方法|West|2015-05-05 Start */
var StringHelper = {

    trim: function (str) {
        return (str + '').replace(/(\s+)$/g, '').replace(/^\s+/g, '');
    },
    strlen: function (str) {
        return (BROWSER.ie && str.indexOf('\n') != -1) ? str.replace(/\r?\n/g, '_').length : str.length;
    },
    mb_strlen: function (str) {
        var len = 0;
        for (var i = 0; i < str.length; i++) {
            len += str.charCodeAt(i) < 0 || str.charCodeAt(i) > 255 ? (charset == 'utf-8' ? 3 : 2) : 1;
        }
        return len;
    },
    mb_cutstr: function (str, maxlen, dot) {
        var len = 0;
        var ret = '';
        var dot = !dot ? '...' : dot;
        maxlen = maxlen - dot.length;
        for (var i = 0; i < str.length; i++) {
            len += str.charCodeAt(i) < 0 || str.charCodeAt(i) > 255 ? (charset == 'utf-8' ? 3 : 2) : 1;
            if (len > maxlen) {
                ret += dot;
                break;
            }
            ret += str.substr(i, 1);
        }
        return ret;
    },
    preg_replace: function (search, replace, str, regswitch) {
        var regswitch = !regswitch ? 'ig' : regswitch;
        var len = search.length;
        for (var i = 0; i < len; i++) {
            re = new RegExp(search[i], regswitch);
            str = str.replace(re, typeof replace == 'string' ? replace : (replace[i] ? replace[i] : replace[0]));
        }
        return str;
    },
    htmlspecialchars: function (str) {
        return StringHelper.preg_replace(['&', '<', '>', '"'], ['&amp;', '&lt;', '&gt;', '&quot;'], str);
    },
    hash: function (string, length) {
        var length = length ? length : 32;
        var start = 0;
        var i = 0;
        var result = '';
        filllen = length - string.length % length;
        for (i = 0; i < filllen; i++) {
            string += "0";
        }
        while (start < string.length) {
            result = StringHelper.stringxor(result, string.substr(start, length));
            start += length;
        }
        return result;
    },
    stringxor: function (s1, s2) {
        var s = '';
        var hash = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
        var max = Math.max(s1.length, s2.length);
        for (var i = 0; i < max; i++) {
            var k = s1.charCodeAt(i) ^ s2.charCodeAt(i);
            s += hash.charAt(k % 52);
        }
        return s;
    },
    updatestring: function (str1, str2, clear) {
        str2 = '_' + str2 + '_';
        return clear ? str1.replace(str2, '') : (str1.indexOf(str2) == -1 ? str1 + str2 : str1);
    }
}
/*字符串处理方法|West|2015-05-05 End */

/*页面元素处理方法|West|2015-05-05 Start */
var HtmlHelper = {
    display: function (id) {
        var obj = $(id);
        if (obj.style.visibility) {
            obj.style.visibility = obj.style.visibility == 'visible' ? 'hidden' : 'visible';
        } else {
            obj.style.display = obj.style.display == '' ? 'none' : '';
        }
    },
    checkall: function (form, prefix, checkall) {
        var checkall = checkall ? checkall : 'chkall';
        count = 0;
        for (var i = 0; i < form.elements.length; i++) {
            var e = form.elements[i];
            if (e.name && e.name != checkall && !e.disabled && (!prefix || (prefix && e.name.match(prefix)))) {
                e.checked = form.elements[checkall].checked;
                if (e.checked) {
                    count++;
                }
            }
        }
        return count;
    },
    showPreview: function (val, id) {
        var showObj = $(id);
        if (showObj) {
            showObj.innerHTML = val.replace(/\n/ig, "<bupdateseccoder />");
        }
    },
    hasClass: function (elem, className) {
        return elem.className && (" " + elem.className + " ").indexOf(" " + className + " ") != -1;
    },
    //将网页中的图片全部转化为背景显示if(window.attachEvent)window.attachEvent("onload", fixPng); 
    correctPNG: function () {
        var arVersion = navigator.appVersion.split("MSIE")
        var version = parseFloat(arVersion[1])
        if ((version >= 5.5) && (document.body.filters)) {
            for (var j = 0; j < document.images.length; j++) {
                var img = document.images[j]
                var imgName = img.src.toUpperCase()
                if (imgName.substring(imgName.length - 3, imgName.length) == "PNG") {
                    var imgID = (img.id) ? "id='" + img.id + "' " : ""
                    var imgClass = (img.className) ? "class='" + img.className + "' " : ""
                    var imgTitle = (img.title) ? "title='" + img.title + "' " : "title='" + img.alt + "' "
                    var imgStyle = "display:inline-block;" + img.style.cssText
                    if (img.align == "left") imgStyle = "float:left;" + imgStyle
                    if (img.align == "right") imgStyle = "float:right;" + imgStyle
                    if (img.parentElement.href) imgStyle = "cursor:hand;" + imgStyle
                    var strNewHTML = "<span " + imgID + imgClass + imgTitle
                 + " style=\"" + "width:" + img.width + "px; height:" + img.height + "px;" + imgStyle + ";"
                 + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"
                 + "(src=\'" + img.src + "\', sizingMethod='scale');\"></span>"
                    img.outerHTML = strNewHTML
                    j = j - 1
                }
            }
        }
    }
}
/*页面元素处理方法|West|2015-05-05 End */

/*Cookie处理|West|2015-05-05 Start */
var CookieHelper = {
    setCookie: function (cookieName, cookieValue, seconds, path, domain, secure) {
        var expires = new Date();
        if (cookieValue == '' || seconds < 0) {
            cookieValue = '';
            seconds = -2592000;
        }
        expires.setTime(expires.getTime() + seconds * 1000);
        domain = !domain ? cookiedomain : domain;
        path = !path ? cookiepath : path;
        document.cookie = escape(cookiepre + cookieName) + '=' + escape(cookieValue)
            + (expires ? '; expires=' + expires.toGMTString() : '')
            + (path ? '; path=' + path : '/')
            + (domain ? '; domain=' + domain : '')
            + (secure ? '; secure' : '');
    },
    getCookie: function (name, nounescape) {
        name = cookiepre + name;
        var cookie_start = document.cookie.indexOf(name);
        var cookie_end = document.cookie.indexOf(";", cookie_start);
        if (cookie_start == -1) {
            return '';
        } else {
            var v = document.cookie.substring(cookie_start + name.length + 1, (cookie_end > cookie_start ? cookie_end : document.cookie.length));
            return !nounescape ? unescape(v) : v;
        }
    },
    //取cookies函数
    getCookie: function (name) {
        var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
        if (arr != null) return unescape(arr[2]); return null;
    },
    delCookie: function (name) {
        var exp = new Date();
        exp.setTime(exp.getTime() - 1);
        var cval = CookieHelper.getCookie(name);
        if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
    },
    //添加cookie,objHours为空则浏览器关闭时cookie自动消失  
    addCookie: function (objName, objValue, objHours) {
        var str = objName + "=" + escape(objValue);
        if (objHours&&objHours > 0) {
            var date = new Date();
            var ms = objHours * 3600 * 1000;
            date.setTime(date.getTime() + ms);
            str += "; expires=" + date.toGMTString();
        }
        document.cookie = str;
    }
}
/*Cookie处理|West|2015-05-05 End */








/*系统API方法|West|2015-05-05 Start */
var SystemHelper = {
    getClipboardData: function (value) {
        window.document.clipboardswf.SetVariable('str', value);
    },
    addFavorite: function (url, title) {
        try {
            window.external.addFavorite(url, title);
        } catch (e) {
            try {
                window.sidebar.addPanel(title, url, '');
            } catch (e) {
                showDialog("请按 Ctrl+D 键添加到收藏夹", 'notice');
            }
        }
    },
    setHomepage: function (sURL) {
        if (BROWSER.ie) {
            document.body.style.behavior = 'url(#default#homepage)';
            document.body.setHomePage(sURL);
        } else {
            showDialog("非 IE 浏览器请手动将本站设为首页", 'notice');
            doane();
        }
    }
}

/*系统API方法|West|2015-05-05 End */

/*扩展系统类属性|West|2015-05-05 Start */
String.prototype.replaceAll = function (reallyDo, replaceWith, ignoreCase) {
    if (!RegExp.prototype.isPrototypeOf(reallyDo)) {
        return this.replace(new RegExp(reallyDo, (ignoreCase ? "gi" : "g")), replaceWith);
    } else {
        return this.replace(reallyDo, replaceWith);
    }
}
Date.prototype.pattern = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份		
        "d+": this.getDate(), //日		
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时		
        "H+": this.getHours(), //小时		
        "m+": this.getMinutes(), //分		
        "s+": this.getSeconds(), //秒		
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度		
        "S": this.getMilliseconds() //毫秒		
    };
    var week = {
        "0": "/u65e5",
        "1": "/u4e00",
        "2": "/u4e8c",
        "3": "/u4e09",
        "4": "/u56db",
        "5": "/u4e94",
        "6": "/u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}
/*扩展系统类属性|West|2015-05-05 End */

/*操作时间的方法|West|2015-05-05 Start */
var DateHelper = {
    //Using By EasyUI-DataGrid
    TimeFormatter: function (value, rec, index) {
        if (value == undefined) {
            return "";
        }
        value = value.substr(1, value.length - 2);
        var obj = eval('(' + "{Date: new " + value + "}" + ')');
        var dateValue = obj["Date"];
        if (dateValue.getFullYear() < 1900) {
            return "";
        }
        var val = dateValue.pattern("yyyy-MM-dd HH:mm");
        return val;
    },
    DateTimeFormatter: function (value, rec, index) {
        if (value == null || value == '') {
            return '';
        }
        var dt;
        if (value instanceof Date) {
            dt = value;
        }
        else {
            dt = new Date(value);
            if (isNaN(dt)) {
                value = value.replace(/\/Date\((-?\d+)\)\//, '$1');
                dt = new Date();
                dt.setTime(value);
            }
        }
        return dt.pattern("yyyy-MM-dd HH:mm");
    },
    DateFormatter: function (value, rec, index) {
        if (value == undefined) {
            return "";
        }
        value = value.substr(1, value.length - 2);
        var obj = eval('(' + "{Date: new " + value + "}" + ')');
        var dateValue = obj["Date"];
        if (dateValue.getFullYear() < 1900) {
            return "";
        }
        return dateValue.pattern("yyyy-MM-dd");
    },
    DateTimeFormat: function (value, format) {
        if (value == null || value == '') {
            return '';
        }
        var dt;
        if (value instanceof Date) {
            dt = value;
        }
        else {
            dt = new Date(value);
            if (isNaN(dt)) {
                value = value.replace(/\/Date\((-?\d+)\)\//, '$1');
                dt = new Date();
                dt.setTime(value);
            }
        }
        return dt.pattern(format);
    },
    getCountDays: function (curDate) {
        var curMonth = curDate.getMonth();
        curDate.setMonth(curMonth + 1);
        curDate.setDate(0);
        return curDate.getDate();
    },
    //获取当月天数
    getNowFormatDate: function (day) {
        var Year = 0;
        var Month = 0;
        var Day = 0;
        var CurrentDate = "";
        //初始化时间
        //Year       = day.getYear();//有火狐下2008年显示108的bug
        Year = day.getFullYear(); //ie火狐下都可以
        Month = day.getMonth() + 1;
        Day = day.getDate();

        CurrentDate += Year + "-";

        if (Month >= 10) {
            CurrentDate += Month + "-";
        }
        else {
            CurrentDate += "0" + Month + "-";
        }
        if (Day >= 10) {
            CurrentDate += Day;
        }
        else {
            CurrentDate += "0" + Day;
        }

        return CurrentDate;
    }
};

/*操作时间的方法|West|2015-05-05 End */




/*对JSON格式的数组按指定建排序|West|2015-05-05 Start */
var CollectionHelper = {
    jsonSortByKey: function (array, key) {
        return array.sort(function (a, b) {
            var x = a[key]; var y = b[key];
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }
}
/*对JSON格式的数组按指定建排序|West|2015-05-05 End */


/*JQ扩展方法声明|West|2015-05-05 Start */
/*扩展快捷键|West|2015-05-05 Start */
$.fn.extend({
    ApplyQuickKey: function (key, Func) {
        $(this).bind("keydown", function (e) {
            var e = e || event;
            var currKey = e.keyCode || e.which || e.charCode;
            if (e.altKey && e.which == key) {
                if (typeof (Func) == 'function') {
                    Func();
                }
            }
        });
    }
});
/*扩展快捷键|West|2015-05-05 End */


/*JQ扩展方法声明|West|2015-05-05 End */


