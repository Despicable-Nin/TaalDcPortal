var isCorporate = document.getElementById("IsCorporate");

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
