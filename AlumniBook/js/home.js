window.onload = function () {
    var bodyH = $(window).height();
    console.log(bodyH);
    $("body").css("height", bodyH + "px");
    $(".header").css("height", (bodyH * 0.2) + "px");
    $(".body").css("height", (bodyH * 0.8) + "px");
}