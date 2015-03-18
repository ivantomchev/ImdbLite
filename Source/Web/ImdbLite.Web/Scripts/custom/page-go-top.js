$(function () {
    $(window).scroll(function () {
        var possition = $(window).scrollTop();
        //console.log(possition)
        if (possition > 100) {
            $('#goTop').fadeIn(500);

        }
        else {
            $('#goTop').fadeOut(500);
        }

    });

    $('#goTop').click(function () {

        if ($(window).scrollTop() != 0) {

            $('html, body').animate({ scrollTop: 0 }, 1000);
        }
        return false;
    });

})