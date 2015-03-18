function openFancy() {

    $(".gallery").fancybox({
        openEffect: 'elastic',
        closeEffect: 'elastic',
        padding:0
    }).trigger('click');
}