$(function () {

    var initImageSrc = $('#image-preview').attr('src');

    $("#upload-file").change(function () {

        var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
        if (regex.test($(this).val().toLowerCase())) {

            if (typeof (FileReader) != "undefined") {

                var reader = new FileReader();
                reader.onload = function (e) {

                    $('#image-preview').attr('src', e.target.result);
                }
                reader.readAsDataURL($(this)[0].files[0]);
            } else {
                alert("This browser does not support FileReader.");
            }

        } else {
            $('#image-preview').attr('src', initImageSrc);
        }
    });
});

//$(function () {

//    var initImageSrc = $('#image-preview').attr('src');

//    $("#upload-file").change(function () {

//        var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
//        if (regex.test($(this).val().toLowerCase())) {

//            if (typeof (FileReader) != "undefined") {

//                var reader = new FileReader();
//                reader.onload = function (e) {

//                    $('#no-photo-medium').removeClass('no-photo-medium');

//                    if ($('#image-preview')) {
//                        $('#image-preview').remove();
//                    }
//                    $('#image').prepend('<img id="image-preview" class="img-responsive" />');
//                    $('#image-preview').attr('src', e.target.result);
//                }
//                reader.readAsDataURL($(this)[0].files[0]);
//            } else {
//                alert("This browser does not support FileReader.");
//            }

//        } else {
//            $('#image').children('#image-preview').remove();
//            $('#no-photo-medium').addClass('no-photo-medium');
//        }
//    });
//});