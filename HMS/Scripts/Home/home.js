$(document).ready(function () {
    $('#loginButton').click(function () {
        var email = $('#email').val();
        var password = $('#password').val();
        var isValid = true;

        //Hide Error by default
        $('#emailError').hide();
        $('#passwordError').hide();

        // Validate email format
        if (!isValidEmail(email)) {
            $('#emailError').show();
            isValid = false;
        } else {
            $('#emailError').hide();
        }

        // Validate password length
        if (password.length < 6) {
            $('#passwordError').show();
            isValid = false;
        } else {
            $('#passwordError').hide();
        }

        if (isValid) {
            $.ajax({
                url: "/Home/PatientLogin", // URL to the API endpoint
                type: "POST",                         // HTTP method
                contentType: "application/json",      // Content type
                data: JSON.stringify({ email: $('#email').val(), password: $('#password').val() }),    // Data to be sent
                success: function (response) {
                    if (response.success) {
                        window.location.href = "/patientMain";
                    }
                    else
                    {
                        window.location.href = "/";
                        toastr.error("Please check the login credentials and try again", "Error");
                    }
                },
                error: function (xhr, status, error) {
                    console.log("Error:", error);
                }
            });

        }
    });

    function loadHospitalDetailsPage(hospitalId)
    {
        $.ajax({
            url: "/PatientMain/PatientLogin", // URL to the API endpoint
            type: "POST",                         // HTTP method
            contentType: "application/json",      // Content type
            data: JSON.stringify({ email: $('#email').val(), password: $('#password').val() }),    // Data to be sent
            success: function (response) {
                if (response.success) {
                    window.location.href = "/patientMain";
                }
                else {
                    window.location.href = "/";
                    toastr.error("Please check the login credentials and try again", "Error");
                }
            },
            error: function (xhr, status, error) {
                console.log("Error:", error);
            }
        });
    }

    function isValidEmail(email) {
        // Basic email format validation
        var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailPattern.test(email);
    }


    $('#signupButton').click(function () {
        var isValid = true;

        // Validate First Name
        var firstName = $('#firstName').val();
        if (firstName.trim() === '') {
            $('#firstNameError').show();
            isValid = false;
        } else {
            $('#firstNameError').hide();
        }

        // Validate Last Name
        var lastName = $('#lastName').val();
        if (lastName.trim() === '') {
            $('#lastNameError').show();
            isValid = false;
        } else {
            $('#lastNameError').hide();
        }

        // Validate Phone
        var phone = $('#phone').val();
        if (phone.trim() === '') {
            $('#phoneError').show();
            isValid = false;
        } else {
            $('#phoneError').hide();
        }

        // Validate Email
        var email = $('#email').val();
        var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailPattern.test(email)) {
            $('#emailError').show();
            isValid = false;
        } else {
            $('#emailError').hide();
        }

        // Validate Age
        var age = $('#age').val();
        if (age.trim() === '') {
            $('#ageError').show();
            isValid = false;
        } else {
            $('#ageError').hide();
        }

        // Validate Gender
        var gender = $('input[name=gender]:checked').val();
        if (!gender) {
            $('#genderError').show();
            isValid = false;
        } else {
            $('#genderError').hide();
        }

        // Validate Address
        var address = $('#address').val();
        if (address.trim() === '') {
            $('#addressError').show();
            isValid = false;
        } else {
            $('#addressError').hide();
        }

        // Validate Pincode
        var pincode = $('#pincode').val();
        if (pincode.trim() === '') {
            $('#pincodeError').show();
            isValid = false;
        } else {
            $('#pincodeError').hide();
        }

        // Validate State
        var state = $('#state').val();
        if (state.trim() === '') {
            $('#stateError').show();
            isValid = false;
        } else {
            $('#stateError').hide();
        }

        if (isValid) {
            // Perform registration action here
        }
    });
});