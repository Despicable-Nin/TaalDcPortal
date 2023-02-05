﻿const form = document.getElementById('propertyForm');
form.addEventListener('submit', openFormConfirmation);


function openFormConfirmation(event) {
    event.preventDefault();

    var isFormValid = event.target.checkValidity();

    event.target.classList.add("was-validated");

    if (isFormValid) {
        var confirmationModal = document.getElementById('confirmationModal');
        var modal = new bootstrap.Modal(confirmationModal, {});
        modal.show();
    }
    return false;
}

function closeModal() {
    var confirmationModal = document.getElementById("confirmationModal");
    const modalBackdrop = document.getElementsByClassName("modal-backdrop");

    confirmationModal.classList.remove("show");
    confirmationModal.style.display = "none";
    document.body.classList.remove("modal-open");
    document.body.removeAttribute("style");

    while (modalBackdrop.length > 0) {
        modalBackdrop[0].parentNode.removeChild(modalBackdrop[0]);
    }
}

function onPropertyFormSubmit(modalBtn, redirectTo) {
    var btn = document.getElementById("property-form-submit");

    btn.disabled = true;
    modalBtn.disabled = true;

    const data = new FormData(form);

    const value = Object.fromEntries(data.entries());

    console.log('form values', value, value.IsActive);

    var isFormValid = form.checkValidity();

    if (isFormValid) {
        var formAction = form.action

        $.ajax({
            type: 'POST',
            url: formAction,
            data: value,
            success: function (data, status, xhr) {
                console.log('property post', data);
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
                    onClick: function () { } // Callback after click
                }).showToast();

                //TODO: Toast immediately closes 
                setTimeout(() => window.location.replace(redirectTo), 500);
            }, error: function (data) {
                const response = data.responseJSON;

                Toastify({
                    text: response && response.message ? response.message : "Something went wrong. Please try again later.",
                    duration: 3000,
                    close: true,
                    gravity: "top", // `top` or `bottom`
                    position: "center", // `left`, `center` or `right`
                    stopOnFocus: true, // Prevents dismissing of toast on hover
                    style: {
                        background: "#D01818",
                    },
                    onClick: function () { } // Callback after click
                }).showToast();

                btn.disabled = false;
                modalBtn.disabled = false;

                closeModal();
            }
        });
    } else {
        btn.disabled = false;
        modalBtn.disabled = false;
    }
    return false;
}