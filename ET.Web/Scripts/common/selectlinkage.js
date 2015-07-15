/**
 * N级联动v2.0
 *
 * @说明: 使用json数据格式，生成N级下拉框联运，
 *        在初始化联动前，可灵活设置json数据中相对应的id\text\value\parent以及topParent
 *
 * @author: tips
 * @date: 2009-09-07
 * @lastupdate: 2010-04-14
 * @调用: SelectN(数组, '下拉框id1', '下拉框id2', ...'第N个..ID');
 * @注意: 级数N<=254
 *       setProperties只能在其它初始化级联效果之前运行，否则将有可能出现异常
 *
 *       数据格式为: [{id:'cn', text:'中国', parent:'-'}, ...]
 *
 *       若需要设定其它键为id\text\parent，则使用SelectN.setProperties(json);
 *       例：SelectN.setProperties({idKey: 'ucode', textKey:'uname', textKey:'uname', parentKey:'fatherCode'});
 *           此时解析的数据格式将发生变化：[{ucode: 'aaa', uname: 'bbb', fatherCode:'xxx'}]
 *
 */
; (function () {

    var myData;
    var topParent = '-';
    var idKey = 'id', textKey = 'text', valueKey = idKey, parentKey = 'parent';

    var SelectN = window.SelectN = function (data) {
        myData = data;
        var ids = [].slice.call(arguments, 1);//下拉框级数的ID,按级数顺序存入数组selectN

        //length-1 => 最后一个下拉框不添加onchange
        for (var i = 0, len = ids.length - 1; i < len; i++) {
            var o = $(ids[i]);
            addEventHandler(o, 'change', (function () {
                var self = o, index = i + 1;
                return function () {
                    showChild(self, ids, index);
                }
            })());
        }

        showChild(null, ids, 0);

        return SelectN;
    }
    /**
     * 指定data中的哪一项代表相应的id、text、value、parent等
     *
     * 设置属性，该方法必须在SelectN所有其它初始化方法之前调用！
     * @param Object opt 配置参数
     *        topParent - 代表顶级节点的值，默认为"-"
     *        idKey - 代表id的键名，默认为"id"
     *        textKey - 代表下拉框text的键名，默认为"text"
     *        valueKey - 代表下拉框value的键名，默认使用idKey
     *        parentKey - 代表parent的键名，默认为"parent"
     */
    SelectN.setProperties = function (opt) {
        topParent = opt.topParent || topParent;
        idKey = opt.idKey || idKey;
        textKey = opt.textKey || textKey;
        valueKey = opt.valueKey || idKey;
        parentKey = opt.parentKey || parentKey;

        return SelectN;
    }

    /**
     * 显示子级下拉框
     * @param Object(Select) oSelect 当前change的下拉框DOM
     * @param Array ids 联动的下拉框ID数组
     * @param int index 当前下拉框的下一级索引
     */
    function showChild(oSelect, ids, index) {
        var p = null == oSelect ? topParent : oSelect.options[oSelect.selectedIndex].xid;
        var ds = getChilds(p);
        clearSelect(ids, index);
        var child = $(ids[index]);
        for (var i = 0, len = ds.length; i < len; i++) {
            var currentObj = ds[i];
            //child.options[child.length] = new Option(ds[i].text, ds[i].text);
            var currentOpt = child.options[child.length] = new Option(currentObj[textKey], currentObj[valueKey]);
            //child.options[child.length-1].xid=ds[i].id;
            currentOpt.xid = currentObj[idKey];
            currentOpt.ownerObj = currentObj;
        }
    }

    /**
     * 获得子级数据
     * @param String p 父级代号
     * @return Array childs
     */
    function getChilds(p) {
        var childs = [];
        for (var i = 0, len = myData.length; i < len; i++) {
            //if(p == myData[i].parent)
            if (p == myData[i][parentKey]) {
                childs.push(myData[i]);
            }
        }
        return childs;
    }
    /**
     * 清除所有子级下拉框
     * @param Array ids 联动的下拉框ID数组
     * @param int index 当前下拉框的下一级索引
     */
    function clearSelect(ids, index) {
        for (var i = index, len = ids.length; i < len; i++) {
            $(ids[i]).length = 1;
        }
    }
    function $(sid) {
        return document.getElementById(sid);
    }
    //为对象obj追加事件 -- 兼容IE与FF
    function addEventHandler(oTarget, sEventType, fnHandler) {
        if (oTarget.addEventListener) {
            oTarget.addEventListener(sEventType, fnHandler, false);
        } else if (oTarget.attachEvent) {
            oTarget.attachEvent("on" + sEventType, fnHandler);
        } else {
            oTarget["on" + sEventType] = fnHandler;
        }
    };
})();
