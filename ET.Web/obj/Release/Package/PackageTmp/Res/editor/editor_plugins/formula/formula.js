(function () {

    var editor = null;

    UM.registerWidget('formula', {

        tpl: "<link type=\"text/css\" rel=\"stylesheet\" href=\"<%=formula_url%>formula.css\">" +
            "<div class=\"edui-formula-wrapper\">" +
            "<ul class=\"edui-tab-nav\"></ul>" +
            "<div class=\"edui-tab-content\"></div>" +
            "</div>",

        sourceData: {
            formula: {
                'common': [
                    "\\frac{ }{ }", "^{ }/_{ }", "x^{ }", "x_{ }", "x^{ }_{ }", "\\bar{ }", "\\sqrt{ }", "\\nthroot{ }{ }",
                    "\\sum^{ }_{n=}", "\\sum", "\\log_{ }", "\\ln", "\\int_{ }^{ }", "\\oint_{ }^{ }", "\\vec{ } ", "\\circ","\\quad", "{}^{}_{}{}",//, "\\underline{^\\underline{\\quad楂樻俯\\quad}}{}", "\\underset{0}{\\overset{a}{\\rightleftarrows}}B"
                ],
                'symbol': [
                    "+", "-", "\\pm", "\\times", "\\ast", "\\div", "/", "\\bigtriangleup",
                    "=", "\\ne", "\\approx", ">", "<", "\\ge", "\\le", "\\infty",
                    "\\cap", "\\cup", "\\because", "\\therefore", "\\subset", "\\supset", "\\subseteq", "\\supseteq",
                    "\\nsubseteq", "\\nsupseteq", "\\in", "\\ni", "\\notin", "\\mapsto", "\\leftarrow", "\\rightarrow",
                    "\\Leftarrow", "\\Rightarrow", "\\leftrightarrow", "\\Leftrightarrow"
                ],
                'letter': [
                    "\\alpha", "\\beta", "\\gamma", "\\delta", "\\varepsilon", "\\varphi", "\\lambda", "\\mu",
                    "\\rho", "\\sigma", "\\omega", "\\Gamma", "\\Delta", "\\Theta", "\\Lambda", "\\Xi",
                    "\\Pi", "\\Sigma", "\\Upsilon", "\\Phi", "\\Psi", "\\Omega"
       //         ], 'maths': [
//                    "{/}alpha", "{/}beta", "{/}gamma", "{/}delta", "{/}varepsilon", "{/}varphi", "{/}lambda", "{/}mu",
//                    "{/}rho", "{/}sigma", "{/}omega", "{/}Gamma", "{/}Delta", "{/}Theta", "{/}Lambda", "{/}Xi"
//                ], 'physics': [
//                    "{/}alpha", "{/}beta", "{/}gamma", "{/}delta", "{/}varepsilon", "{/}varphi", "{/}lambda", "{/}mu",
//                    "{/}rho", "{/}sigma", "{/}omega", "{/}Gamma", "{/}Delta", "{/}Theta", "{/}Lambda", "{/}Xi"
                ], 'chymist': [
                     "bgceq_(1).png", "bgceq_(2).png", "bgceq_(3).png", "bgceq_(4).png", "bgceq_(5).png", "bgceq_(6).png"
                ]
            }
        },
        initContent: function (_editor, $widget) {

            var me = this,
                formula = me.sourceData.formula,
                lang = _editor.getLang('formula').static,
                formulaUrl = UMEDITOR_CONFIG.UMEDITOR_HOME_URL + 'dialogs/formula/',
                options = $.extend({}, lang, { 'formula_url': formulaUrl }),
                $root = me.root();

            if (me.inited) {
                me.preventDefault();
                return;
            }
            me.inited = true;

            editor = _editor;
            me.$widget = $widget;

            $root.html($.parseTmpl(me.tpl, options));
            me.tabs = $.eduitab({ selector: "#edui-formula-tab-Jpanel" });

            /* 鍒濆鍖杙opup鐨勫唴瀹?*/
            var headHtml = [], xMax = 0, yMax = 0,
            $tabContent = me.root().find('.edui-tab-content');
            $.each(formula, function (k, v) {

                var contentHtml = [];
                $.each(v, function (i, f) {

                if (f.indexOf("bgceq_") >= 0)
                    contentHtml.push('<li class="edui-formula-latex-item" data-latex="' + f + '" style="background-image: url(\'../dialogs/formula/formulaImg/' + f + '\'); background-repeat: no-repeat;width: 36px; height: 24px;background-position: center center;" >            </li>');
               
                 else
                    contentHtml.push('<li class="edui-formula-latex-item" data-latex="' + f + '" style="background-position:-' + (xMax * 30) + 'px -' + (yMax * 30) + 'px"></li>');
                   
                  
                   
                    if (++xMax >= 8) {
                        ++yMax; xMax = 0;
                    }


                });
                if (xMax != 0)
                    yMax++;
                //yMax++; xMax = 0;
                xMax = 0;
                $tabContent.append('<div class="edui-tab-pane"><ul>' + contentHtml.join('') + '</ul>');
                headHtml.push('<li class="edui-tab-item"><a href="javascript:void(0);" class="edui-tab-text">' + lang['lang_tab_' + k] + '</a></li>');
            });
            headHtml.push('<li class="edui-formula-clearboth"></li>');

//            if (headHtml.indexOf("bgceq_") >= 0) {
//                $root.find('.edui-tab-nav').html(headHtml.join('') + '<div class="edui-tab-pane">        <ul>            <li class="edui-formula-latex-item" style="background-image: url(\'../dialogs/formula/formulaImg/bgceq_(1).png\');            background-repeat: no-repeat;width: 36px; height: 24px;" data-latex="bgceq_(1).png">            </li> </ul> </div>');
//            }
//            else
                $root.find('.edui-tab-nav').html(headHtml.join(''));
            $root.find('.edui-tab-content').append('<div class="edui-formula-clearboth"></div>');

            /* 閫変腑绗竴涓猼ab */
            me.switchTab(0);
        },
        initEvent: function () {
            var me = this;

            //闃叉鐐瑰嚮杩囧悗鍏抽棴popup
            me.root().on('click', function (e) {
                return false;
            });

            //鐐瑰嚮tab鍒囨崲鑿滃崟
            me.root().find('.edui-tab-nav').delegate('.edui-tab-item', 'click', function (evt) {
                me.switchTab(this);
                return false;
            });

            //鐐瑰嚮閫変腑鍏紡
            me.root().find('.edui-tab-pane').delegate('.edui-formula-latex-item', 'click', function (evt) {
                var $item = $(this),
                    latex = $item.attr('data-latex') || '';

                if (latex) {
                    //me.insertLatex(latex.replace("{/}", "\\"));
                    // \\frac{ }{ }
                    if (latex.indexOf("bgceq_") >= 0) {
                        //me.insertLatex(latex);


                        me.insertLatex('{ }\\quad</span><span style="background-image: url(\'../dialogs/formula/formulaImg/' + latex + '\'); background-repeat: no-repeat; background-position: left 12px; display:inline-block; width: 36px; height: 24px; font-size: 10px; text-align: center;"  class="mathquill-embedded-latex">^{^{}}_{_{}}\\quad{}</span><span class="mathquill-embedded-latex">\\quad{}');
                    }
                    else
                        me.insertLatex(latex);
                }
                me.$widget.edui().hide();
                return false;
            });
        },
        switchTab: function (index) {
            var me = this,
                $root = me.root(),
                index = $.isNumeric(index) ? index : $.inArray(index, $root.find('.edui-tab-nav .edui-tab-item'));

            $root.find('.edui-tab-nav .edui-tab-item').removeClass('edui-active').eq(index).addClass('edui-active');
            $root.find('.edui-tab-content .edui-tab-pane').removeClass('edui-active').eq(index).addClass('edui-active');

            /* 鑷姩闀块珮 */
            me.autoHeight(0);
        },
        autoHeight: function () {
            this.$widget.height(this.root() + 2);
        },
        insertLatex: function (latex) {
            editor.execCommand('formula', latex);
        },
        width: 350,
        height: 400
    });

})();

