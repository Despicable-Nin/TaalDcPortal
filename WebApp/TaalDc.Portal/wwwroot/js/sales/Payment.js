function applyMask() {
	$("#AmountPaid").mask("#,##0.00", { reverse: true });
}

function acceptPayment() {
	var modal = document.getElementById("paymentVerifyModal");

	var orderId = modal.getAttribute('data-order-id');
	var paymentId = modal.getAttribute('data-payment-id');

	var uri = `/Sales/AcceptPayment?orderId=${orderId}&paymentId=${paymentId}`;

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

			Toastify({
				text: response.isFormError? "Please check your entry.": response.message,
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
		}
	});
}

const paymentForm = document.getElementById('paymentform');
paymentForm.addEventListener('submit', onPaymentSubmit);


function onPaymentSubmit(event) {
	event.preventDefault();

	const data = new FormData(event.target);

	console.log('form data', data);

	const value = Object.fromEntries(data.entries());

	console.log('form values', value);

	var isFormValid = event.target.checkValidity();

	event.target.classList.add("was-validated");

	var modal = document.getElementById("newPaymentModal");
	const modalBackdrop = document.getElementsByClassName("modal-backdrop");

	if (isFormValid) {
		var formAction = event.target.action

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
					onClick: function () { } // Callback after click
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
					onClick: function () { } // Callback after click
				}).showToast();
			}
		});
	}

	return false;
}