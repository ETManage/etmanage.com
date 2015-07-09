

(function () {
    var parent = window;
    $G = function (id) { return document.getElementById(id) };
    //focus元素
    $focus = function (node) {
        setTimeout(function () {
            if (browser.ie) {
                var r = node.createTextRange();
                r.collapse(false);
                r.select();
            } else {
                node.focus()
            }
        }, 0)
    }

})();

