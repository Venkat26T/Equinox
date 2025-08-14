jQuery.validator.addMethod("minimumage", function (value, element) {
    if (!value) return false;

    var dob = new Date(value);
    var today = new Date();

    var age = today.getFullYear() - dob.getFullYear();

    var birthdayThisYear = new Date(today.getFullYear(), dob.getMonth(), dob.getDate());
    if (today < birthdayThisYear) {
        age--;
    }

    return age >= 8 && age <= 80;
});

jQuery.validator.unobtrusive.adapters.addBool("minimumage");
