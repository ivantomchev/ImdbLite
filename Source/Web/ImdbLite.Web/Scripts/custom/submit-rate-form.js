$(function () {
    $('#new-rating').on('rating.change', function (event) {

        event.preventDefault();
        $('form').submit();
        return false;
    });
})
