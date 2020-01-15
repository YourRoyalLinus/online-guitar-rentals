//Electric Box
$(document).ready(function () {
    $("#electric").hover(function () {
        $("#electric-cell .card-text").css({ "color": "rgba(255, 255, 255, 1)", "font-weight": "400" })
    }, function () {
        $("#electric-cell .card-text").css({ "color": "rgba(255, 255, 255, .5)", "font-weight": "300" });
    });
});

//Bass Box
$(document).ready(function () {
    $("#bass").hover(function () {
        $("#bass-cell .card-text").css({ "color": "rgba(255, 255, 255, 1)", "font-weight": "400" })
    }, function () {
        $("#bass-cell .card-text").css({ "color": "rgba(255, 255, 255, .5)", "font-weight": "300" });
    });
});

//Acoustic Box
$(document).ready(function () {
    $("#acoustic").hover(function () {
        $("#acoustic-cell .card-text").css({ "color": "rgba(255, 255, 255, 1)", "font-weight": "400" })
    }, function () {
        $("#acoustic-cell .card-text").css({ "color": "rgba(255, 255, 255, .5)", "font-weight": "300" });
    });
});