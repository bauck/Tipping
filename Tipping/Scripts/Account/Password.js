$(function() {
    $("#SendPassword").click(function (e) {
        e.preventDefault();
        var eMail = $("#Email").val();
        $.ajax(getUrl("SendPassword_Account"), {
            type: "POST",
            data: { eMail: eMail },
            success: function (returdata) {
                var success = eval(returdata);
                if (success) {
                    $("#status").text("Mail med passord er sendt.");
                } else {
                    $("#status").text("Kunne ikke finne bruker.");
                }
            },
            error: function () {
                $("#status").text("Noe gikk feil. Last siden på nytt og prøv igjen");
            }
        });
    });
});

