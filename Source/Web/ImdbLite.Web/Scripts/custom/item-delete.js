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

        var formData = new FormData($(this)[0]);

        $.ajax({
            url: this.action,
            type: this.method,
            data: formData,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            error: function (data) {

                $('#myModalContent').html(data);
                bindForm();
            },
            success: function (data) {

                $('#myModal').modal('hide');
                $('#update-target').load(data.url);
            }
        });

        return false;
    });
}