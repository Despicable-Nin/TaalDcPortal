﻿const portalSubURL = "";


function applyMask() {
    $("#AmountPaid").mask("#,##0.00", {reverse: true});
}

function processPaymentVerification(paymentVerificationType) {
    console.log(paymentVerificationType);

    $('.formLoader').show();

    var modal = document.getElementById("paymentVerifyModal");
    var endPointAction = "";
    if (paymentVerificationType === "accept") {
        endPointAction = "AcceptPayment";
    } else if (paymentVerificationType === "void") {
        console.log("Voided")
        modal = document.getElementById("paymentVoidModal");
        endPointAction = "VoidPayment";
    }

    var orderId = modal.getAttribute('data-order-id');
    var paymentId = modal.getAttribute('data-payment-id');
    var paymentTypeId = modal.getAttribute('data-payment-type-id');

    var confirmationNumber = document.getElementById('paymentConfirmationNumber').value;

    var uri = portalSubURL + `/portal/Sales/${endPointAction}?orderId=${orderId}&paymentId=${paymentId}`;

    if (paymentVerificationType === "accept") {
        uri = uri + `&paymentTypeId=${paymentTypeId}&confirmationNumber=${confirmationNumber}`;
    }

    console.log('sales uri', uri);

    const modalBackdrop = document.getElementsByClassName("modal-backdrop");

    $.ajax({
        type: 'POST',
        url: uri,
        success: function (data, status, xhr) {
            Toastify({
                text: "Payment successfully verified!",
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

            modal.classList.remove("show");
            modal.style.display = "none";
            document.body.classList.remove("modal-open");
            document.body.removeAttribute("style");

            while (modalBackdrop.length > 0) {
                modalBackdrop[0].parentNode.removeChild(modalBackdrop[0]);
            }

            window.location.reload();

        }, error: function (data) {
            const response = data.responseJSON;
            
            debugger;
            console.log(response);

            Toastify({
                text: response.isFormError ? "Please check your entry." : response.message,
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


const paymentForm = document.getElementById('paymentform');
paymentForm.addEventListener('submit', onPaymentSubmit);


const editPaymentForm = document.getElementById('editPaymentform');
editPaymentForm.addEventListener('submit', onPaymentSubmit);


function onPaymentSubmit(event) {
    event.preventDefault();
    $('.formLoader').show();

    const data = new FormData(event.target);

    console.log('form data', data);

    const value = Object.fromEntries(data.entries());

    console.log('form values', value);

    var isFormValid = event.target.checkValidity();

    event.target.classList.add("was-validated");

    var formAction = event.target.action

   
    var modal = document.getElementById("newPaymentModal");
        
    if (formAction.includes('EditPayment')) {
        modal = document.getElementById("editPaymentModal");
    }

    const modalBackdrop = document.getElementsByClassName("modal-backdrop");

    if (isFormValid) {
        $.ajax({
            type: 'POST',
            url: formAction,
            data: value,
            success: function (data, status, xhr) {
                console.log('payment post', data);
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

                modal.classList.remove("show");
                modal.style.display = "none";
                document.body.classList.remove("modal-open");
                document.body.removeAttribute("style");

                while (modalBackdrop.length > 0) {
                    modalBackdrop[0].parentNode.removeChild(modalBackdrop[0]);
                }

                window.location.reload();

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