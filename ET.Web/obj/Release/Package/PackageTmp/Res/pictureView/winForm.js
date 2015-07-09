
document.oncontextmenu = function () { event.returnValue = false; } //屏蔽鼠标右键   
document.onselectstart = function () { event.returnValue = false; }; //取消选择
document.oncopy = function () { event.returnValue = false; };
document.oncut = function () { event.returnValue = false; };
document.onpaste = function () { event.returnValue = false; }; //粘贴
window.onhelp = function () { return false } //屏蔽F1帮助

function LockWindow(e) {
    if ((window.event.altKey) &&
       ((window.event.keyCode == 37) ||   //屏蔽 Alt+ 方向键 ←   
        (window.event.keyCode == 39)))   //屏蔽 Alt+ 方向键 →   
    {
        event.returnValue = false;
    }
//    if ((window.event.ctrlKey) && ((window.event.altKey) || event.keyCode == 46)) { alert('强行结束该考试，试卷将不能完整提交！'); event.returnValue = false; }
    if ((event.keyCode == 116) ||                 //屏蔽 F5 刷新键   
       (event.ctrlKey && event.keyCode == 82)) { //Ctrl + R   
        event.keyCode = 0;
        event.returnValue = false;
    }
    if ((window.event.altKey) && (event.keyCode == 9)) { event.keyCode = 0; event.returnValue = false; } //alt+tab
    if (event.keyCode == 122) { event.keyCode = 0; event.returnValue = false; }  //屏蔽F11   
    //if (event.keyCode==8){event.keyCode=0;event.returnValue=false;}  //屏蔽backspace   
    if (event.keyCode == 113) { event.keyCode = 0; event.returnValue = false; }  //屏蔽F2   
    if (event.keyCode == 114) { event.keyCode = 0; event.returnValue = false; }  //屏蔽F3 
    if (event.keyCode == 123) { event.keyCode = 0; event.returnValue = false; }  //屏蔽F12 
    if (event.keyCode == 42) { event.keyCode = 0; event.returnValue = false; }  //屏蔽打印   
    if (event.ctrlKey && event.keyCode == 78) event.returnValue = false;   //屏蔽 Ctrl+n   
    if (event.shiftKey && event.keyCode == 121) event.returnValue = false;  //屏蔽 shift+F10  
    
    
    
     
    if (window.event.srcElement.tagName == "A" && window.event.shiftKey)
        window.event.returnValue = false;             //屏蔽 shift 加鼠标左键新开一网页   
    if ((window.event.altKey) && (window.event.keyCode == 115))             //屏蔽Alt+F4   
    {
        window.showModelessDialog("about:blank", "", "dialogWidth:1px;dialogheight:1px");
        return false;
    }


    var ev = e || window.event; //获取event对象
    var obj = ev.target || ev.srcElement; //获取事件源
    var t = obj.type || obj.getAttribute('type'); //获取事件源类型
    //获取作为判断条件的事件类型
    var vReadOnly = obj.readOnly;
    var vDisabled = obj.disabled;
    //处理undefined值情况
    vReadOnly = (vReadOnly == undefined) ? false : vReadOnly;
    vDisabled = (vDisabled == undefined) ? true : vDisabled;
    //当敲Backspace键时，事件源类型为密码或单行、多行文本的，
    //并且readOnly属性为true或disabled属性为true的，则退格键失效
    var flag1 = ev.keyCode == 8 && (t == "password" || t == "text" || t == "textarea") && (vReadOnly == true || vDisabled == true);
    //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效
    var flag2 = ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea";
    //判断
    if (flag2 || flag1) return false;
}
//禁止退格键 作用于Firefox、Opera
document.onkeypress = LockWindow;
//禁止退格键 作用于IE、Chrome
document.onkeydown = LockWindow;