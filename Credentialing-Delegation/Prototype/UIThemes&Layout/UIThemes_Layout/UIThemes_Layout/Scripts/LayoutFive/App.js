$(document).ready(function () {
    TweenLite.to($("#caption"), 2, { css: { top: 0 }, delay: 1, ease: Power2.easeOut });
    TweenLite.to($("#btn1"), 2, { css: { left: 10 }, delay: 2, ease: Power2.easeOut });
    TweenLite.to($("#btn2"), 2, { css: { left: 20 }, delay: 2.5, ease: Power2.easeOut });
    TweenLite.to($("#btn3"), 2, { css: { left: 40 }, delay: 3, ease: Power2.easeOut });
    TweenLite.to($("#btn4"), 2, { css: { left: 60 }, delay: 3.5, ease: Power2.easeOut });
    TweenLite.to($("#btn5"), 2, { css: { left: 10 }, delay: 4, ease: Power2.easeOut });
    TweenLite.to($("#btn6"), 2, { css: { left: 20 }, delay: 4.5, ease: Power2.easeOut });

});


$("#btn1").click(function () {
    TweenLite.to($("#content"), 0.5, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#backbutton"), 1, { css: { left: 1000, rotation: -180 }, ease: Power2.easeOut });
    TweenLite.to($("#pagecaption"), 1, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#caption"), 1, { css: { top: 90 }, ease: Power2.easeOut });
    $("#pagecaption").text("About Us");

});

$("#btn2").click(function () {
    TweenLite.to($("#content"), 0.5, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#backbutton"), 1, { css: { left: 650, rotation: -180 }, ease: Power2.easeOut });
    TweenLite.to($("#pagecaption"), 1, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#caption"), 1, { css: { top: 90 }, ease: Power2.easeOut });
    $("#pagecaption").text("Our Products");

});

$("#btn3").click(function () {
    TweenLite.to($("#content"), 0.5, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#backbutton"), 1, { css: { left: 650, rotation: -180 }, ease: Power2.easeOut });
    TweenLite.to($("#pagecaption"), 1, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#caption"), 1, { css: { top: 90 }, ease: Power2.easeOut });
    $("#pagecaption").text("Who We Are?");

});

$("#btn4").click(function () {
    TweenLite.to($("#content"), 0.5, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#backbutton"), 1, { css: { left: 650, rotation: -180 }, ease: Power2.easeOut });
    TweenLite.to($("#pagecaption"), 1, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#caption"), 1, { css: { top: 90 }, ease: Power2.easeOut });
    $("#pagecaption").text("We on Map");

});

$("#btn5").click(function () {
    TweenLite.to($("#content"), 0.5, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#backbutton"), 1, { css: { left: 650, rotation: -180 }, ease: Power2.easeOut });
    TweenLite.to($("#pagecaption"), 1, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#caption"), 1, { css: { top: 90 }, ease: Power2.easeOut });
    $("#pagecaption").text("Contact Us");

});

$("#btn6").click(function () {
    TweenLite.to($("#content"), 0.5, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#backbutton"), 1, { css: { left: 650, rotation: -180 }, ease: Power2.easeOut });
    TweenLite.to($("#pagecaption"), 1, { css: { top: 0 }, ease: Power2.easeOut });
    TweenLite.to($("#caption"), 1, { css: { top: 90 }, ease: Power2.easeOut });
    $("#pagecaption").text("Feedback");

});


$("#backbutton").click(function () {
    TweenLite.to($("#content"), 1, { css: { top: 500 }, ease: Power2.easeIn });
    TweenLite.to($("#backbutton"), 1, { css: { left: 1500, rotation: 0 }, ease: Power2.easeIn });
    TweenLite.to($("#pagecaption"), 1, { css: { top: -60 }, ease: Power2.easeOut });
    TweenLite.to($("#caption"), 1, { css: { top: 0 }, ease: Power2.easeOut });
});