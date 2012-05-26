function settOppLagreBonusTips() {
    $(".bonustips input.answer").change(function () {
        var container = $(this).closest(".bonustips");
        var bonusid = parseInt($(container).data("bonusid"), 10);
        var svar = $(this).val();
        if (svar === "") {
            return;
        } else {
            $(container).find(".status").show().text("Lagrer...");
            $.ajax(getUrl("LagreBonusTips_Tips"), {
                type: "POST",
                data: { bonusID: bonusid, svar: svar },
                success: function (returdata) {
                    $(container).find(".status").text("Lagret!").fadeOut(3000);
                },
                error: function () {
                    $(container).find(".status").text("Noe gikk feil under lagring").css("color","red");
                }
            });
        }
    });
}
function settOppLagreTips() {
    $(".tips input[type=number]").change(function () {
        var container = $(this).closest(".tips");
        var kampid = parseInt($(container).data("kampid"), 10);
        var maalHjemmelag = parseInt($(container).find(".hjemmelag").val(), 10);
        var maalBortelag = parseInt($(container).find(".bortelag").val(), 10);
        if (isNaN(maalHjemmelag) || isNaN(maalBortelag)) {
            return;
        } else {
            $(container).find(".status").show().text("Lagrer...");
            $.ajax(getUrl("LagreTips_Tips"), {
                type: "POST",
                data: { kampID: kampid, målHjemmelag: maalHjemmelag, målBortelag: maalBortelag },
                success: function (returdata) {
                    $(container).find(".status").text("Lagret!").fadeOut(3000);
                },
                error: function () {
                    $(container).find(".status").text("Noe gikk feil under lagring").css("color", "red");
                }
            });
        }
    });
}