$(function () {

    $.ajaxSetup({ cache: false });

    $("a[data-modal]").on("click", function (e) {

        // hide dropdown if any
        $(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');


        $('#myModalContent').load(this.href, function () {


            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');

            bindForm(this);
        });

        return false;
    });


});

function bindForm(dialog) {

    $('form', dialog).submit(function () {

        if ($(this).attr("enctype") != "multipart/form-data") {

            $.post(this.action, $(this).serialize(), function () {

                $('#myModal').modal('hide');

            }).then(function (data) {

                if ($(data.dropdownToUpdate).length > 0) {

                    var option = $('<option>').val(data.value).html(data.text);
                    $(data.dropdownToUpdate).append(option);

                    var options = $(data.dropdownToUpdate).children().sort(function (a, b) {
                        return a.text.toUpperCase() == b.text.toUpperCase() ? 0 : a.text.toUpperCase() < b.text.toUpperCase() ? -1 : 1
                    });

                    $(data.dropdownToUpdate).html(options);
                    $(data.dropdownToUpdate).trigger("chosen:updated");
                }
                else {

                    $(data.listToUpdate).load(data.url);

                }
                

            });
        }
        else {

            var formData = new FormData($(this)[0]);

            $.ajax({
                url: this.action,
                type: this.method,
                data: formData,
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    $('#myModal').modal('hide');

                    if ($(data.dropdownToUpdate).length > 0) {

                        var option = $('<option>').val(data.value).html(data.text);
                        $(data.dropdownToUpdate).append(option);

                        var options = $(data.dropdownToUpdate).children().sort(function (a, b) {
                            return a.text == b.text ? 0 : a.text < b.text ? -1 : 1
                        });

                        $(data.dropdownToUpdate).html(options);
                        $(data.dropdownToUpdate).trigger("chosen:updated");
                    }
                    else {

                        $(data.listToUpdate).load(data.url);

                    }

                }
            });

        }
        return false;
    });
}