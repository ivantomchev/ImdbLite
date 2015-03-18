$(function () {

    $('.trailer').click(function () {

        var href = $(this).attr('href') + "&autoplay=1";

        $.fancybox({
            padding: 0,
            href: href.replace(new RegExp("watch\\?v=", "i"), 'v/'),
            type: 'swf',
            swf: {
                wmode: 'transparent',
                allowfullscreen: 'true'
            },
            title: this.title,
            autoSize: true,
            fitToView: true,
            transitionIn: 'elastic',
            transitionOut: 'elastic',
            onStart: function () {
                $.fancybox.resize();
            }
        });

        return false;
    });

});