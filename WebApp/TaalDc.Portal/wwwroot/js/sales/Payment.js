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