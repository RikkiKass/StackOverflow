$(() => {

    const id = $("#like-question").data('question-id');
    console.log(id);
    $.get('/questions/getlikes', { id }, function (likes) {
        $("#likes-count").append(likes);

    });


    $("#like-question").on('click', function () {
   
        $("#like-question").attr('class', "oi oi-heart text-danger");

        $.post('/questions/updatelikes', { id }, function () {
            $.get('/questions/getlikes', { id }, function (likes) {
                $("#likes-count").text(`Likes: ${likes}`);
            });
        });
    });


});