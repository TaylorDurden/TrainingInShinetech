$(function(){

        $(".menulist > ul").children().click(function(){
            if (isNaN($(this).text())) {
            $(this).parent().find(".active").removeClass("active");
            $(this).addClass("active");
        }

        });
    });