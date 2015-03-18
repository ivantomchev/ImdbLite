function scrollTop() {

    if ($(window).scrollTop() != 0) {

        $('html, body').animate({ scrollTop: 0 }, 1000);
    }
    return false;
}