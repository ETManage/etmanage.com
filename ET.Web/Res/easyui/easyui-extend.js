$.extend($.fn.form.methods, {
    serialize: function (jq) {
        var arrayValue = $(jq[0]).serializeArray();
        var json = {};
        $.each(arrayValue, function () {
            var item = this;
            if (json[item["name"]]) {
                json[item["name"]] = json[item["name"]] + "," + item["value"];
            } else {
                json[item["name"]] = item["value"];
            }
        });
        return json;
    }
});
//[$('#flexigridData').datagrid('updateColumnTitle',{field:'name',title:"zhangbo"});]
$.extend($.fn.datagrid.methods, {
    updateColumnTitle: function (jq, param) {
        return jq.each(function () {
            var target = this;
            var cell = $(this).datagrid('getPanel').find('div.datagrid-header td[field="' + param.field + '"] div.datagrid-cell span:first-child');
            $(cell).text(param.title);
        });
    }
});
$.extend($.fn.combobox.defaults, {
    checkValue: function (jq) {
        var myvalue = $("input[name='" + jq[0].id + "']").val();
        var myoptions = $(jq).combobox('options');
        var mydata = $(jq).combobox('getData');
        if (mydata.length > 0) {
            for (var i = 0; i < mydata.length; i++) {
                if (myvalue == eval('mydata[' + i + '].' + myoptions.textField)) {
                    $("input[name='" + jq[0].id + "']").val(eval('mydata[' + i + '].' + myoptions.valueField));
                    return;
                }
                else if (myvalue == eval('mydata[' + i + '].' + myoptions.valueField)) {
                    return;
                }
            }
        }
        $(jq).combobox('setValue', '');
        $("input[name='" + jq[0].id + "']").val('');
    }
});

$.extend($.fn.tabs.defaults, {
    onSelect: function (title) {
        var td = $(this).tabs('getTab', title)
        var itemfn = td.attr("onselectitemfn");
        if (null != itemfn && undefined != itemfn)
            eval(itemfn);
        if (!td.attr("firstloaded")) {
            var fun = td.attr("onfirstloaded");
            var ifsrc = td.attr("iframesrc");
            if (null != fun && undefined != fun)
                eval(fun);
            if (null != ifsrc && undefined != ifsrc) {
                var s = '<iframe scrolling="auto" frameborder="0"  src="' + ifsrc + '" style="width:100%;height:' + td.attr("height") + 'px;overflow-y: auto;"></iframe>';
                $(this).tabs('update', { tab: td, options: { content: s} });
            }
            td.attr("firstloaded", true);
        }
    }
});
$.extend($.fn.datagrid.defaults.editors, {
    TimeSpinner: {
        init: function (container, options) {
            var input = $('<input type="text">').appendTo(container);
            return input.TimeSpinner(options);
        },
        destroy: function (target) {
            $(target).timespinner('destroy');
        },
        getValue: function (target) {
            return $(target).timespinner('getValue');
        },
        getHours: function (target) {
            return $(target).timespinner('getHours');
        },
        getMinutes: function (target) {
            return $(target).timespinner('getMinutes');
        },
        getSeconds: function (target, value) {
            return $(target).timespinner('getSeconds');
        },
        resize: function (target, width) {
            $(target).timespinner('resize', width);
        }
    }
});
$.extend($.fn.datagrid.defaults, {
    onLoadSuccess: function (data) {
        var opt = $(this).datagrid('options').columns;
        var rows = $(this).datagrid("getRows");
        if (typeof ($(this).datagrid('options').onFootLoadSuccess) === "function")
            $(this).datagrid('options').onFootLoadSuccess.call(this, data);
        var footer = new Array();
        footer['sum'] = "";
        footer['sumInt'] = "";
        footer['avg'] = "";
        footer['max'] = "";
        footer['min'] = "";
        for (var i = 0; i < opt[0].length; i++) {
            if (opt[0][i].sum) {
                footer['sum'] = footer['sum'] + sum(opt[0][i].field) + ',';
            }
            if (opt[0][i].sumInt) {
                footer['sumInt'] = footer['sumInt'] + sumInt(opt[0][i].field) + ',';
            }
            if (opt[0][i].avg) {
                footer['avg'] = footer['avg'] + avg(opt[0][i].field) + ',';
            }
            if (opt[0][i].max) {
                footer['max'] = footer['max'] + max(opt[0][i].field) + ',';
            }
            if (opt[0][i].min) {
                footer['min'] = footer['min'] + min(opt[0][i].field) + ',';
            }
            if (opt[0][i].sumdistinct) {
                footer['sum'] = footer['sum'] + sumdistinct(opt[0][i].field, opt[0][i].sumdistinct) + ',';
            }
            if (opt[0][i].kb) {
                var tr = $(this).datagrid('getPanel').find('div.datagrid-body tr');
                tr.each(function () {
                    var cell = $(this).children('td[field="' + opt[0][i].field + '"]').children('div');
                    var cellvalue = $(cell[0]).html();
                    if (cellvalue && !isNaN(cellvalue)) {
                        $(cell[0]).html(cellvalue.toString().formatMoney());
                    }
                });
            }
        }
        var footerObj = new Array();
        if (footer['sum'] != "") {
            var tmp = '{' + footer['sum'].substring(0, footer['sum'].length - 1) + "}";
            var obj = eval('(' + tmp + ')');
            if (obj[opt[0][0].field] == undefined) {
                footer['sum'] += '"' + opt[0][0].field + '":"<b>当页合计:</b>"';
                obj = eval('({' + footer['sum'] + '})');
            } else {
                obj[opt[0][0].field] = "<b>当页合计:</b>" + obj[opt[0][0].field];
            }
            footerObj.push(obj);
        }
        if (footer['sumInt'] != "") {
            var tmp = '{' + footer['sumInt'].substring(0, footer['sumInt'].length - 1) + "}";
            var obj = eval('(' + tmp + ')');
            if (obj[opt[0][0].field] == undefined) {
                footer['sumInt'] += '"' + opt[0][0].field + '":"<b>当页合计:</b>"';
                obj = eval('({' + footer['sumInt'] + '})');
            } else {
                obj[opt[0][0].field] = "<b>当页合计:</b>" + obj[opt[0][0].field];
            }
            footerObj.push(obj);
        }
        if (footer['avg'] != "") {
            var tmp = '{' + footer['avg'].substring(0, footer['avg'].length - 1) + "}";
            var obj = eval('(' + tmp + ')');
            if (obj[opt[0][0].field] == undefined) {
                footer['avg'] += '"' + opt[0][0].field + '":"<b>当页均值:</b>"';
                obj = eval('({' + footer['avg'] + '})');
            } else {
                obj[opt[0][0].field] = "<b>当页均值:</b>" + obj[opt[0][0].field];
            }
            footerObj.push(obj);
        }
        if (footer['max'] != "") {
            var tmp = '{' + footer['max'].substring(0, footer['max'].length - 1) + "}";
            var obj = eval('(' + tmp + ')');

            if (obj[opt[0][0].field] == undefined) {
                footer['max'] += '"' + opt[0][0].field + '":"<b>当页最大值:</b>"';
                obj = eval('({' + footer['max'] + '})');
            } else {
                obj[opt[0][0].field] = "<b>当页最大值:</b>" + obj[opt[0][0].field];
            }
            footerObj.push(obj);
        }
        if (footer['min'] != "") {
            var tmp = '{' + footer['min'].substring(0, footer['min'].length - 1) + "}";
            var obj = eval('(' + tmp + ')');

            if (obj[opt[0][0].field] == undefined) {
                footer['min'] += '"' + opt[0][0].field + '":"<b>当页最小值:</b>"';
                obj = eval('({' + footer['min'] + '})');
            } else {
                obj[opt[0][0].field] = "<b>当页最小值:</b>" + obj[opt[0][0].field];
            }
            footerObj.push(obj);
        }
        if (footerObj.length > 0) {
            $(this).datagrid('reloadFooter', footerObj);
        }
        function sum(field) {
            var sumNum = 0;
            for (var i = 0; i < rows.length; i++) {
                sumNum += Number(rows[i][field]);
            }
            return '"' + field + '":"' + sumNum.toFixed(2).formatMoney() + '"';
        };
        function sumInt(field) {
            var sumNum = 0;
            for (var i = 0; i < rows.length; i++) {
                sumNum += Number(rows[i][field]);
            }
            return '"' + field + '":"' + parseInt(sumNum) + '"';
        };
        function sumdistinct(field, sumdistinct, count) {
            if (null != window._ && undefined != window._) {
                var groupBytemp = _.groupBy(rows, sumdistinct);
                var sumfee = _.reduce(groupBytemp, function (memo, num) { return memo + parseFloat(num[0][field]); }, 0);
                var count = _.reduce(groupBytemp, function (memo, num) { if (num.length > 1) { return memo + 1 } else { return memo }; }, 0);

                var sumNum = Number(sumfee);
                if (count > 0)
                    return '"' + field + '":"' + sumNum.toFixed(2).toString().formatMoney() + '重复' + count + '"';
                else
                    return '"' + field + '":"' + sumNum.toFixed(2).toString().formatMoney() + '"';
            } else
                throw "未引用 underscore.js"


        };
        function avg(field) {
            var sumNum = 0;
            for (var i = 0; i < rows.length; i++) {
                sumNum += Number(rows[i][field]);
            }
            return '"' + field + '":"' + (sumNum / rows.length).toFixed(2) + '"';
        }
        function max(field) {
            var max = 0;
            for (var i = 0; i < rows.length; i++) {
                if (i == 0) {
                    max = Number(rows[i][field]);
                } else {
                    max = Math.max(max, Number(rows[i][field]));
                }
            }
            return '"' + field + '":"' + max + '"';
        }
        function min(field) {
            var min = 0;
            for (var i = 0; i < rows.length; i++) {
                if (i == 0) {
                    min = Number(rows[i][field]);
                } else {
                    min = Math.min(min, Number(rows[i][field]));
                }
            }
            return '"' + field + '":"' + min + '"';
        }
    }
});
$.extend($.fn.combobox.defaults, {
    filter: function (q, row) {
        var opts = $(this).combobox('options');
        return row[opts.textField].toLocaleLowerCase().indexOf(q.toLocaleLowerCase()) >= 0;
    }
});
//日期验证
$.extend($.fn.validatebox.defaults.rules, {
    datetime: {
        validator: function (value, param) {
            return !CheckDate(value);
        },
        message: '日期格式不正确.'
    },
    comboselectonly: {
        validator: function (value, param) {
            var rules = $.fn.validatebox.defaults.rules;
            var data = $(param[0]).combobox("getData");
            var opts = $(param[0]).combobox("options");
            if (data != undefined && data.length > 0) {
                for (var key in data) {
                    if (data[key][opts.textField] == value || param[1] == value) {
                        return true;
                    }
                }
            }
            rules.comboselectonly.message = '必须是选择项或等同项！';
            return false;
        },
        comboselectonly: '必须是选择项或等同项！'
    }
});
//选择项验证
$.extend($.fn.validatebox.defaults.rules, {
    combovalue: {
        validator: function (value, param) {
            var rules = $.fn.validatebox.defaults.rules;
            if (param[0].length > 0 & $(param[0]).combobox("getValue").length > 0) {
                rules.combovalue.message = '必须是选择项或等同项！';
                return true;
            } else {
                rules.combovalue.message = '必须是选择项或等同项！';
                return false;
            }
        },
        combovalue: '必须是选择项或等同项！'
    }
});
//电话验证
$.extend($.fn.validatebox.defaults.rules, {
    tel: {
        validator: function (value, param) {
            return (/^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$/.test(value)) || (/^(?:1\d{2})-?\d{5}(\d{3}|\*{3})$/.test(value));
        },
        message: '电话格式不正确.'
    }
});
$.fn.tree.defaults.loadFilter = function (data, parent) {
    var opt = $(this).data().tree.options;
    var idFiled, textFiled, parentField;
    if (opt.parentField) {
        idFiled = opt.idFiled || 'id';
        textFiled = opt.textFiled || 'text';
        parentField = opt.parentField;
        var i, l, treeData = [], tmpMap = [];
        for (i = 0, l = data.length; i < l; i++) {
            tmpMap[data[i][idFiled]] = data[i];
        }
        for (i = 0, l = data.length; i < l; i++) {
            if (tmpMap[data[i][parentField]] && data[i][idFiled] != data[i][parentField]) {
                if (!tmpMap[data[i][parentField]]['children'])
                    tmpMap[data[i][parentField]]['children'] = [];
                data[i]['text'] = data[i][textFiled];
                tmpMap[data[i][parentField]]['children'].push(data[i]);
            } else {
                data[i]['text'] = data[i][textFiled];
                treeData.push(data[i]);
            }
        }
        return treeData;
    }
    return data;
};
function windowConfirm() {
    var myopenwindow = $('div.myopenwindow').last();
    var oFrm = myopenwindow.find("iframe")[0];
    if (typeof (oFrm.contentWindow.BtnSaveOrUpdate) == 'function') {
        oFrm.contentWindow.BtnSaveOrUpdate()
    }
};
function hideWindowBtn(index) {
    var myopenwindow = $('div.myopenwindow').last().find("a").eq(index).hide();
};
function showWindowBtn(index) {
    var myopenwindow = $('div.myopenwindow').last().find("a").eq(index).show();
};
function openWindow(title, href, options) {
    this.options =
    {
        width: 860,
        height: 520,
        buttons: [{ text: '确定(Alt+S)', iconCls: 'icon-ok', handler: windowConfirm }, { text: '关闭', iconCls: 'icon-cancel', handler: windowClose }]
    };
    $.extend(this.options, options);
    var mydialog = $("<div class='myopenwindow' style='height: 0px;'></div>");
    mydialog.insertAfter("body");
    var oFrm = $('<iframe id="iframepage" width="100%" height="100%" style="border: none; overflow: hidden;" frameborder="0" src="about:blank"></iframe>');
    oFrm.appendTo(mydialog);
    $(mydialog).dialog({
        title: title,
        width: this.options.width,
        height: this.options.height,
        modal: true,
        shadow: false,
        onClose: function () { windowClose(); },
        buttons: this.options.buttons
    });
    $(oFrm).attr("src", href);
    $(mydialog).dialog("open");
}
function openSimpleWindow(title, href, options) {
    this.options =
    {
        width: 860,
        height: 520,
        buttons: [{ text: '关闭', iconCls: 'icon-cancel', handler: windowClose}]
    };
    $.extend(this.options, options);
    openWindow(title, href, this.options);
};
$.fn.combobox.defaults.loadFilter = function (data) {
    var opt = $(this).data().combobox.options;
    var valueFiled, textFiled, bindValueField;
    if (opt.bindValueField) {
        valueFiled = opt.valueFiled || 'value';
        textFiled = opt.textFiled || 'text';
        bindValueField = opt.bindValueField;
        var i, l;
        for (i = 0, l = data.length; i < l; i++) {
            eval("data[i]." + valueFiled + '=' + "data[i]." + bindValueField)
        }
    }
    return data;
};
$.extend($.fn.form.methods, {
    myLoad: function (jq, param) {
        return jq.each(function () {
            load(this, param);
        });
        function load(target, param) {
            if (!$.data(target, "form")) {
                $.data(target, "form", {
                    options: $.extend({}, $.fn.form.defaults)
                });
            }
            var options = $.data(target, "form").options;
            if (typeof param == "string") {
                var params = {};
                if (options.onBeforeLoad.call(target, params) == false) {
                    return;
                }
                $.ajax({
                    url: param,
                    data: params,
                    dataType: "json",
                    success: function (rsp) {
                        loadData(rsp);
                    },
                    error: function () {
                        options.onLoadError.apply(target, arguments);
                    }
                });
            } else {
                loadData(param);
            }
            function loadData(dd) {
                var form = $(target);
                var formFields = form.find("input[name],select[name],textarea[name]");
                formFields.each(function () {
                    var name = this.name;
                    var value = jQuery.proxy(function () { try { return eval('this.' + name); } catch (e) { return ""; } }, dd)();
                    var rr = setNormalVal(name, value);
                    if (!rr.length) {
                        var f = form.find("input[numberboxName=\"" + name + "\"]");
                        if (f.length) {
                            f.numberbox("setValue", value);
                        } else {
                            $("input[name=\"" + name + "\"]", form).val(value);
                            $("textarea[name=\"" + name + "\"]", form).val(value);
                            $("select[name=\"" + name + "\"]", form).val(value);
                        }
                    }
                    setPlugsVal(name, value);
                });
                options.onLoadSuccess.call(target, dd);
                $(target).form("validate");
            };
            function setNormalVal(key, val) {
                var rr = $(target).find("input[name=\"" + key + "\"][type=radio], input[name=\"" + key + "\"][type=checkbox]");
                rr._propAttr("checked", false);
                rr.each(function () {
                    var f = $(this);
                    if (f.val() == String(val) || $.inArray(f.val(), val) >= 0) {
                        f._propAttr("checked", true);
                    }
                });
                return rr;
            };
            function setPlugsVal(key, val) {
                var form = $(target);
                var cc = ["combobox", "combotree", "combogrid", "datetimebox", "datebox", "combo"];
                var c = form.find("[comboName=\"" + key + "\"]");
                if (c.length) {
                    for (var i = 0; i < cc.length; i++) {
                        var combo = cc[i];
                        if (c.hasClass(combo + "-f")) {
                            if (c[combo]("options").multiple) {
                                c[combo]("setValues", val);
                            } else {
                                c[combo]("setValue", val);
                            }
                            return;
                        }
                    }
                }
            };
        };
    }
});
$.extend($.fn.validatebox.defaults.rules, {
    equal: {
        validator: function (value, param) {
            var rules = $.fn.validatebox.defaults.rules;
            if (param.value != value) {
                rules.equal.message = param.col.title + ",必须为" + value;
                return false;
            } else {
                return true;
            }
        },
        message: '不能为0'
    },
    unequal: {
        validator: function (value, param) {
            var rules = $.fn.validatebox.defaults.rules;
            if (param.value == value) {
                rules.unequal.message = param.col.title + ",不能为" + value;
                return false;
            } else {
                return true;
            }
        },
        message: '不能为0'
    },
    lessequal: {
        validator: function (value, param) {
            var rules = $.fn.validatebox.defaults.rules;
            var val1, val2;
            if (!isNaN(param.value)) {
                val1 = parseFloat(param.value);
                if (isNaN(value)) {
                    val2 = parseFloat(param.row[value]);
                } else {
                    val2 = value;
                }
                if (!isNaN(val2) && !isNaN(val1) && val2 < 0) {
                    if (val1 >= val2) {
                        return true;
                    } else {
                        rules.lessequal.message = param.col.title + ",必须大于等于" + val2;
                        return false;
                    }

                } else {
                    if (val1 <= val2) {
                        return true;
                    } else {
                        rules.lessequal.message = param.col.title + ",必须小于等于" + val2;
                        return false;
                    }
                }

            } else {
                return false;
            }

        },
        message: '数值不在合理范围。'
    }
});
$.extend($.fn.datagrid.methods, {
    validateData: function (jq, param) {
        var result = true;
        var opt = $(jq).datagrid('options').columns;
        var rows = $(jq).datagrid("getRows");
        for (var i = 0; i < opt[0].length; i++) {
            if (opt[0][i].valid) {
                var valids = opt[0][i].valid;
                var rules = $.fn.validatebox.defaults.rules;
                var scrollToindex;
                for (var j = 0; j < rows.length; j++) {
                    var argument = { value: rows[j][opt[0][i].field], col: opt[0][i], row: rows[j], target: $(jq) };
                    $.each(valids, function (n, v) {
                        //[解析参数类似unequal['0.00']]
                        var ns = /([a-zA-Z_]+)(.*)/.exec(v);
                        //[ns[2]:集合['0.00']]
                        var v1 = eval(ns[2]);
                        //[ns[1]:解析参数类似unequal]
                        if (rules[ns[1]].validator.call(this, v1, argument)) {
                            //[销毁之前提醒]
                            var current = $(jq).datagrid('getPanel').find("div.datagrid-body tr[datagrid-row-index='" + j + "']");
                            $(current).tooltip("destroy");
                        } else {
                            //[提醒]
                            var current = $(jq).datagrid('getPanel').find("div.datagrid-body tr[datagrid-row-index='" + j + "']");
                            current.css({ color: "#000", borderColor: "#ffa8a8", backgroundColor: "#FFCCCC" });
                            $(current).tooltip({ position: 'top', content: rules[ns[1]].message, onShow: function () {
                                $(this).tooltip('tip').css({ backgroundColor: '#FFFFCC', borderColor: '#CC9933' });
                            }
                            });
                            if (!scrollToindex)
                                scrollToindex = j;
                            result = false;
                            return false
                        }
                    });
                }
                $(jq).datagrid('scrollTo', scrollToindex);
                //[如果验证不通过先返回]
                if (!result) {
                    return false;
                }
            }
        }
        return result;
    }
});
function windowClose() {
    var myopenwindow = $('div.myopenwindow').last().parent();
    myopenwindow.next("div").remove();
    myopenwindow.remove();
    $("#InfoGridData").datagrid('load', getQueryParam());
}
$(function () {
    document.onkeydown = function (e) {
        var ev = document.all ? window.event : e;
        if (ev.keyCode == 13) {
            if (typeof (queryClick) == 'function') {
                queryClick();
            }
        };
    };
});
