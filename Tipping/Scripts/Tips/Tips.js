function settOppLagreTips() {
    $(".tips input[type=number]").change(function () {
        var container = $(this).closest(".tips");
        var kampid = parseInt($(container).data("kampid"),10);
        var maalHjemmelag = parseInt($(container).find(".hjemmelag").val(), 10);
        var maalBortelag = parseInt($(container).find(".bortelag").val(), 10);
        if (isNaN(maalHjemmelag) || isNaN(maalBortelag)) {
            alert(maalHjemmelag + " - " + maalBortelag);
            return;
        } else {
            $.ajax(getUrl("LagreTips_Tips"), {
                type: "POST",
                data: {kampID: kampid, målHjemmelag: maalHjemmelag, målBortelag: maalBortelag},
                success: function (returdata) {
                    //alert("Great success " + returdata);
                }
            });
        }
    });
}