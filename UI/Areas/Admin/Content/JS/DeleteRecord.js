var URL = "";
var ID = "";

function AskQuestion(url, id) {
    URL = url; // Store the URL
    ID = id; // Store the ID
    $('#modalmessage').modal('show'); // Show the modal
}

function Delete() {
    $.ajax({
        url: URL + ID,
        type: "POST",
        success: function (result) {
            $("#a_" + ID).fadeOut(); // Fade out the table row
            $('#modalmessage').modal('hide'); // Hide the modal
        },
        error: function () {
            alert('Error occurred while deleting meta.');
        }
    });
}
