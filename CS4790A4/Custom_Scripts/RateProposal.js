$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    blinking($(".blink"));
});
function loadAjaxEdit(propToLoad, propId) {
    var pth = '/ProposalScores/AjaxEdit/' + propId;
    $.ajax({
        url: pth,
        success: function (result) {
            document.getElementById("mainTable").style.display = 'none';
            $("#editDiv").html(result);
            $("input[type=number]").first().focus();
        }
    });
}


function blinking(elm) {
    timer = setInterval(blink, 10);
    function blink() {
        elm.fadeOut(750, function () {
            elm.fadeIn(750);
        });
    }
}