
function ChangeDoType(type) {
    var strSelect;
    $("input[name='ck']:checked").each(function () {
        if ($(this).data("id")) {
            strSelect += $(this).data("id") + ",";
            $(this).parent().remove();
        }
    })
    $.ajax({
        cache: true,
        type: "POST",
        url: '/onegtd/ajaxchangedotype',
        data: { ids: strSelect, t: type },
        async: false,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            TopNotify(errorThrown, 'error');
        },
        success: function (data) {
            if (data == "true") {
                switch (type) {
                    case "today":
                        TopNotify("选择项已转为今日待办", 'info');
                        break;
                    case "tomorrow":
                        TopNotify("选择项已转为明日待办", 'info');
                        break;
                }

                $("input[name='ck']:checked").each(function () {
                    strSelect += $(this).data("id") + ",";
                    $(this).parent().remove();
                })
            }
        }
    });
}

function deleteInBox() {
    if (confirm('确认删除？')) {
        var strSelect;
        $("input[name='ck']:checked").each(function () {
            if ($(this).data("id")) {
                strSelect += $(this).data("id") + ",";
                $(this).parent().remove();
            }
        })
        $.ajax({
            cache: true,
            type: "POST",
            url: '/onegtd/ajaxdeleteinbox',
            data: { ids: strSelect, t: type },
            async: false,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                TopNotify(errorThrown, 'error');
            },
            success: function (data) {
                if (data == "true") {
                    TopNotify("选择项已删除", 'info');


                    $("input[name='ck']:checked").each(function () {
                        strSelect += $(this).data("id") + ",";
                        $(this).parent().remove();
                    })
                }
            }
        });
    }
}
