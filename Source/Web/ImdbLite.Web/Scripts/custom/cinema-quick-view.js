$(function () {

    $('.cinemaItem').click(function (e) {

        e.preventDefault();

        var url = $(this).attr('href');

        $('.panel').removeClass('panel-primary');
        $('.panel').addClass('panel-info');

        $(this).children().first().removeClass('panel-info');
        $(this).children().first().addClass('panel-primary');

        $.ajax({
            url: url,
            type: "Get",
            async: false,
            cache: true,
            contentType: false,
            processData: false,
            error: function (data) {

                $('#update-target').html(data);
            },
            success: function (data) {
                $('#update-target').html(data);
            }
        });

        if ($(window).scrollTop() != 0) {

            $('html, body').animate({ scrollTop: 0 }, 1000);
        }

        return false;
    })

})