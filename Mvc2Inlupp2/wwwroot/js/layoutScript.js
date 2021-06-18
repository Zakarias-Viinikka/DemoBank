$(document).ready(function () {
    getLoggedInAs();
    function getLoggedInAs() {
        console.log("got to function");
        $.ajax({
            dataType: "html",
            type: "GET",
            url: "/Partial/_LoggedInAsPartial",
            success: function (result) {
                $("#loggedInAsDiv").html(result);
                console.log("result:", result);
            }
        });
    }
});