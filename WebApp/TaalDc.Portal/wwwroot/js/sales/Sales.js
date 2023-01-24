applyMask();

function applyMask() {
	$("#SellingPrice").mask("#,##0.00", { reverse: true });
	$("#Reservation").mask("#,##0.00", { reverse: true });
	$("#DownPayment").mask("#,##0.00", { reverse: true });
}

const unitsForm = document.getElementById('units-form');
unitsForm.addEventListener('submit', onUnitFormSubmit);

function onUnitFormSubmit(event) {
	event.preventDefault();

	getAvailableUnits(1, 10);
}

function getAvailableUnits(pageNumber = 1, pageSize = 10) {
	console.log(document.getElementsByName("unitFilter"));
	var filter = document.getElementsByName("unitFilter")[0].value;

	fetch(`/Properties/GetUnits?pageNumber=${pageNumber}&statusId=1&filter=${filter}`)
		.then((response) => response.json())
		.then((data) => {
			//Process data into table
			var unitTableBody = document.getElementById("unit-body");
			var unitModalBody = document.getElementById("unit-modal-body");
			var unitPagination = document.getElementById("unit-pagination");
			unitPagination.innerHTML = "";
			var rows = "";

			data.data.map((u, i) => {
				var unit = {
					unit: u.identifier,
					id: u.unit_id,
					tower: u.tower_name,
					floor: u.floor_name,
					view: u.scenic_view,
					unitType: u.unit_type
				};

				var unitName = `${u.tower_name} | ${u.unit_type} ${u.scenic_view} | Unit ${u.identifier}`;

				let newRow = `<tr>
								<td>${unitName}</td>
								<td>
									<button data-unit="${unitName}" data-unitId="${u.unit_id}" data-sellingPrice="${u.price}" onclick="onUnitSelect()" type="button" class="btn btn-primary">Select</button>
								</td>
							</tr>`

				rows = rows + newRow;
			});

			unitTableBody.innerHTML = rows;


			//add a pagination
			var numberOfPagesToDisplay = 5;
			var totalPageGroups = data.total_pages / numberOfPagesToDisplay;
			var pageGroups = [];

			for (var i = 1; i <= totalPageGroups; i++) {
				pageGroups.push(i * numberOfPagesToDisplay);
			}


			var pageGroupIndex = pageGroups.find(i => data.page_number <= i);
			var lastPageIndex = pageGroups[pageGroups.length - 1];

			console.log('last index', lastPageIndex);

			var pageGroup = pageGroups.length > 0 && pageGroupIndex > 0 ? pageGroupIndex : data.total_pages;
			var pageGroupMinPage = data.total_pages <= 5 ? 0 : pageGroupIndex > 0 ? pageGroup - numberOfPagesToDisplay : lastPageIndex;

			////5 -> 0
			////10 -> 5

			var nextPage = data.page_number < data.total_pages ? data.page_number + 1 : 0;
			var prevPage = data.page_number > 1 ? data.page_number - 1 : 0;


			var ul = document.createElement("ul");


			ul.classList.add("pagination");

			if (data.page_number > 1) {
				var li = document.createElement("li");
				li.classList.add("page-item");
				li.innerHTML = `<a onclick="getAvailableUnits(${prevPage}, 10)" class="page-link alink noselect">Prev</a>`;
				ul.appendChild(li);
			}


			if (prevPage == 0) {
				var li = document.createElement("li");
				li.classList.add("page-item");
				li.classList.add("disabled");
				li.innerHTML = `<a class="page-link alink noselect">Prev</a>`;
				ul.appendChild(li);
			}

			if (pageGroup == 0 || pageGroup == pageGroupMinPage) {
				var li = document.createElement("li");
				li.classList.add("page-item");
				li.innerHTML = `<a onclick="getAvailableUnits(${data.page_number}, 10)" class="page-link alink noselect">${data.page_number}</a>`;
				ul.appendChild(li);
			}

			for (var i = pageGroupMinPage; i < pageGroup; i++) {
				var isActive = data.page_number == i + 1;

				var li = document.createElement("li");
				li.classList.add("page-item");
				li.innerHTML = `<a onclick="getAvailableUnits(${i + 1}, 10)" class="noselect page-link alink ${isActive ? "active" : ""}">${i + 1}</a>`;
				ul.appendChild(li);
			}

			if (data.page_number < data.total_pages) {
				var li = document.createElement("li");
				li.classList.add("page-item");
				li.innerHTML = `<a onclick="getAvailableUnits(${nextPage}, 10)" class="page-link alink noselect">Next</a>`;
				ul.appendChild(li);
			}

			if (nextPage == 0) {
				var li = document.createElement("li");
				li.classList.add("page-item");
				li.classList.add("disabled");
				li.innerHTML = `<a class="page-link alink noselect">Next</a>`;
				ul.appendChild(li);
			}

			unitPagination.appendChild(ul)

			return data;
		});
}


function onUnitSelect() {
	document.getElementsByName("unitName")[0].value = event.target.getAttribute('data-unit');
	document.getElementsByName("UnitId")[0].value = event.target.getAttribute('data-unitId');

	var sellingPrice = parseFloat(event.target.getAttribute('data-sellingPrice')).toFixed(2);

	document.getElementsByName("SellingPrice")[0].value = parseFloat(sellingPrice).toLocaleString();

	const unitsModal = document.getElementById("available-units-modal");
	const modalBackdrop = document.getElementsByClassName("modal-backdrop");

	applyMask();

	unitsModal.classList.remove("show");
	unitsModal.style.display = "none";
	document.body.classList.remove("modal-open");
	document.body.removeAttribute("style");

	while (modalBackdrop.length > 0) {
		modalBackdrop[0].parentNode.removeChild(modalBackdrop[0]);
	}
}

function openAvailableUnitsModal() {
	const unitsModal = new bootstrap.Modal(document.getElementById('available-units-modal'), {});

	//fetch available units
	getAvailableUnits();

	unitsModal.show();
}

var withReservation = document.getElementById("withReservation");

//withReservation.addEventListener('change', function () {
//	var reservationFields = document.getElementsByClassName('reservationfields');
//	if (this.checked) {
//		for(var i=0; i<reservationFields.length; i++){
//			reservationFields[i].style.display = "block";
//		}

//		console.log("Checkbox is checked..");
//	} else {
//		for (var i = 0; i < reservationFields.length; i++) {
//			reservationFields[i].style.display = "none";
//		}
//	}
//});



var withDP = document.getElementById("withDownPayment");

withDP.addEventListener('change', function () {
	var dpFields = document.getElementsByClassName('dpfields');
	if (this.checked) {
		for (var i = 0; i < dpFields.length; i++) {
			dpFields[i].style.display = "block";
		}

		console.log("Checkbox is checked..");
	} else {
		for (var i = 0; i < dpFields.length; i++) {
			dpFields[i].style.display = "none";
		}
	}
});




const salesForm = document.getElementById('salesForm');
salesForm.addEventListener('submit', onSalesFormSubmit);


function onSalesFormSubmit(event) {
	event.preventDefault();

	const data = new FormData(event.target);

	console.log('form data', data);

	const value = Object.fromEntries(data.entries());

	console.log('form values', value);

	var isFormValid = event.target.checkValidity();

	event.target.classList.add("was-validated");

	if (isFormValid) {
		var formAction = event.target.action

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
					onClick: function () { } // Callback after click
				}).showToast();

				window.location.replace("/Sales/Reserved");

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