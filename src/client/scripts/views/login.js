$(function() {
    // process the form
    $('form').submit(function(event) {
        // get the form data
        // there are many ways to get this data using jQuery (you can use the class or id also)
        var formData = {
            'email'             : $('input[name=inputEmail]').val(),
            'password'    : $('input[name=inputPassword]').val()
        };

        // process the form
        $.ajax({
            type        : 'GET', // define the type of HTTP verb we want to use (POST for our form)
            url         : 'http://localhost:8001/api/users/' + formData.email, // the url where we want to POST
            //data        : formData, // our data object
            dataType    : 'json', // what type of data do we expect back from the server
            encode      : true
        })
        // using the done promise callback
        .done(function(data) {
                // log data to the console so we can see
                console.log(data); 
                // here we will handle errors and validation messages
                if($.isEmptyObject(data)){
                    console.log("Empty result");
                    $('button').addClass('btn-danger');
                } else {
                    $('button').removeClass('btn-danger');
                    $('button').addClass('btn-success');
                }
            });

        // stop the form from submitting the normal way and refreshing the page
        event.preventDefault();
    });
});