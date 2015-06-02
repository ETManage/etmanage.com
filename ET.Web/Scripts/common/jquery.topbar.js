function TopNotify(msg, msgtype) {
    var c = $("<div style='display: none;'>" + msg + "</div>"); switch (msgtype) {
        case "correct": $(c).showTopbarMessage({ background: "#90EE90", close: 2e3 }); break;
        case "error": $(c).showTopbarMessage({ background: "#CD0000", close: 1e4, color: "#DDDDDD", close: "click" }); break;
        case "info": $(c).showTopbarMessage({ background: "#0191E0", close: 2e3 }); break;
        default: $(c).showTopbarMessage({ background: "#90EE90", close: 2e3 })
    }
}
!function (a) { a.fn.showTopbarMessage = function (b) { var d, c = { background: "#3A8CF9", borderColor: "#ccc", color: "#051F2D", height: "45px", fontSize: "12px", close: "click" }; return b = a.extend(c, b), d = '<div class="topbarBox" style="width: 100%; position: fixed; z-index: 9999; height: ' + b.height + ";top: 0px; left: 0px; line-height: " + b.height + ";font-size: " + b.fontSize + ";color:" + b.color + ";background-color: " + b.background + "; border-bottom: solid 3px " + b.borderColor + ';text-align: center; display: block;">' + a(this).html() + "</div>", this.each(function () { a(".topbarBox").length > 0 && (a(".topbarBox").hide(), a(".topbarBox").slideUp(200, function () { a(".topbarBox").remove() })), "click" == b.close ? a("body").prepend(a(d).click(function () { a(this).slideUp(200, function () { a(this).remove() }) }).slideDown(200)) : setTimeout(function () { a("body").prepend(a(d).slideDown(200).delay(b.close).slideUp(1000, function () { a(this).remove() })) }, 1) }) } }(jQuery);