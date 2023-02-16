var isCorporate = document.getElementById("IsCorporate");

if (isCorporate) { 
    isCorporate.addEventListener('change', function () {
        var corpFields = document.getElementsByClassName('corpFields');

        var companyName = document.getElementById("CompanyName");
        var companyAddress = document.getElementById("CompanyAddress");
        var companyIndustry = document.getElementById("CompanyIndustry");

        if (this.checked) {
            for (var i = 0; i < corpFields.length; i++) {
                corpFields[i].style.display = "block";
                companyName.required = true;
                companyAddress.required = true;
                companyIndustry.required = true;
            }

        } else {
            for (var i = 0; i < corpFields.length; i++) {
                corpFields[i].style.display = "none";

                companyName.required = false;
                companyAddress.required = false;
                companyIndustry.required = false;
            }
        }
    });
}




const generalInfoForm = document.getElementById("generalinfo");

if (generalInfoForm) {
    generalInfoForm.addEventListener("submit", saveInfo);
}

const contactInfoForm = document.getElementById('contactInfo');


if (contactInfoForm) {
    contactInfoForm.addEventListener("submit", saveInfo);
}


const addressForm = document.getElementById('addressForm');


if (addressForm) {
    addressForm.addEventListener("submit", saveInfo);
}


const idInformationForm = document.getElementById('idForm');


if (idInformationForm) {
    idInformationForm.addEventListener("submit", saveInfo);
}

const companyInformationForm = document.getElementById('companyForm');


if (companyInformationForm) {
    companyInformationForm.addEventListener("submit", saveInfo);
}


const spouseForm = document.getElementById('spouseForm');


if (spouseForm) {
    spouseForm.addEventListener("submit", saveInfo);
}


function saveInfo(event) {
    $('.formLoader').show();
    event.preventDefault();
    const form = event.target;
    const data = new FormData(form);
    const value = Object.fromEntries(data.entries());

    var isFormValid = event.target.checkValidity();

    event.target.classList.add('was-validated');

    if (isFormValid) {
        var formAction = form.action;

        $.ajax({
            type: 'POST',
            url: formAction,
            data: value,
            success: function (data, status, xhr) {
                console.log('sales post', data);
                Toastify({
                    text: "Submitted successfully!",
                    duration: 3000,
                    close: true,
                    gravity: "top", // `top` or `bottom`
                    position: "right", // `left`, `center` or `right`
                    stopOnFocus: true, // Prevents dismissing of toast on hover
                    style: {
                        background: "#6AB0F8",
                    },
                    onClick: function () {
                    } // Callback after click
                }).showToast();

                event.target.classList.remove('was-validated');

                $('.formLoader').hide();
            }, error: function (data) {
                const response = data.responseJSON;

                if (response.isFormError) {
                    var errors = response.modelState.value;
                    console.log("Error", response);

                    // Iterate through the errors and render them next to the appropriate form fields
                    for (var key in errors) {
                        if (errors.hasOwnProperty(key)) {
                            var inputElement = $('input[name="' + key + '"]');

                            if (errors[key].errors.length > 0) {
                                errors[key].errors.forEach(function (error) {
                                    var errorElement = $('<div class="invalid-feedback" style="display: block;">' + error.errorMessage + '</div>');
                                    inputElement.after(errorElement);
                                });
                            }
                        }
                    }
                }

                Toastify({
                    text: response.message ? response.message : "Something went wrong. Please try again later.",
                    duration: 3000,
                    close: true,
                    gravity: "top", // `top` or `bottom`
                    position: "center", // `left`, `center` or `right`
                    stopOnFocus: true, // Prevents dismissing of toast on hover
                    style: {
                        background: "#D01818",
                    },
                    onClick: function () {
                    } // Callback after click
                }).showToast();

                $('.formLoader').hide();
            }
        });
    }

    return false;
}
