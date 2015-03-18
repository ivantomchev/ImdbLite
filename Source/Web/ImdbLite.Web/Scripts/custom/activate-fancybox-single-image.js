$(function () {
    $(".fancybox").fancybox({
        openEffect: 'elastic',
        closeEffect: 'elastic',
        padding:0,

        helpers: {
            title: {
                type: 'inside'
            }
        }
    });
})