$(function () {

    //$.validator.setDefaults({ ignore: ":hidden:not(select)" });

    var participationType = {
        "Director": 0,
        "Producer": 1
    };

    $('.chzn-select').chosen();

    //$('.chosen-choices').addClass('form-control');

    $('#selectedCharacters').chosen().change(function () {

        var characterList = $('#character-list');
        var parameters = [];
        var selectedCelebrities = $(this).chosen().val();

        if (selectedCelebrities != null) {

            var len = selectedCelebrities.length;
            var movieId = $('#Id').val();

            for (var i = 0; i < len; i++) {

                var celebrityName = $(this).find('option[value="' + selectedCelebrities[i] + '"]').text();
                var celebrityId = selectedCelebrities[i];
                var id = $('#Characters_' + [i] + '__Id').val();
                var characterName = $('input[celebrity="' + celebrityName + '"]').val();

                var current = {
                    id: id,
                    celebrityName: celebrityName,
                    celebrityId: celebrityId,
                    characterName: characterName,
                    movieId: movieId,
                };

                console.log(current);
                parameters.push(current);
            };
        };

        sendResult('/Administration/Movies/AddCharacters', 'Post', parameters, characterList);
    });

    function sendResult(action, type, data, updateTarget) {

        $.ajax({
            url: action,
            type: type,
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                $(updateTarget).html(result);
            },
            error: function (request) {
                console.log('error');
            }
        });
    };
});