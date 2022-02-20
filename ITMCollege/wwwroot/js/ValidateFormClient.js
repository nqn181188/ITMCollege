
//Validate Admission Form
$("#btnAd").click(function () {
    $("span").empty();
    $("input").removeClass("input-err");
    if ($("#FullName").val() == "") {
        $("#FullName").addClass("input-err");
        $("#FullNameErr").append("Full Name cannot be blank.");
    }
    if ($("#FatherName").val() == "") {
        $("#FatherName").addClass("input-err");
        $("#FatherNameErr").append("Father Name cannot be blank.");
    }
    if ($("#MotherName").val() == "") {
        $("#MotherName").addClass("input-err");
        $("#MotherNameErr").append("Mother Name cannot be blank.");
    }
    if ($("#Dob").val() == "") {
        $("#Dob").addClass("input-err");
        $("#DobErr").append("Date Of Birth cannot be blank.");
    }
    if ($("#Email").val() == "") {
        $("#Email").addClass("input-err");
        $("#EmailErr").append("Email cannot be blank.");
    } else {
        var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        if (regex.test($("#Email").val()) == false) {
            $("#Email").addClass("input-err");
            $("#EmailErr").append("Email is invalid. Please choose another one.");
        }
    }
    if ($("#ResAdd").val() == "") {
        $("#ResAdd").addClass("input-err");
        $("#ResAddErr").append("Residential Address cannot be blank.");
    }
    if ($("#PerAdd").val() == "") {
        $("#PerAdd").addClass("input-err");
        $("#PerAddErr").append("Permanent Address cannot be blank.");
    }
});
//Validate Registration Form
$("#btnReg").click(function (e) {
    $("input").removeClass("input-err");
    $("span").empty();
    $("select").removeClass("input-err");
    if ($("#image").val() == "") {
        $("#ImageErr").append("Image is required.");
        $("#image").addClass("input-err");
    } else {
        var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        if ($.inArray($("#image").val().split('.').pop().toLowerCase(), fileExtension) == -1) {
            $("#ImageErr").append("Only formats are allowed : " + fileExtension.join(', '));
            $("#image").addClass("input-err");
            e.preventDefault();
        }
    }
    if ($("#SpeSubject").val() == "") {
        $("#SpeSubjectErr").append("Specialzed Subject is required");
        $("#SpeSubject").addClass("input-err");
    }
    if ($("#EmerName").val() == "") {
        $("#EmerNameErr").append("Emergency Name cannot be blank.");
        $("#EmerName").addClass("input-err");
    }
    if ($("#EmerPhone").val() == "") {
        $("#EmerPhoneErr").append("Emergency Phone cannot be blank.");
        $("#EmerPhone").addClass("input-err");
    } else {
        var regex = /^\d{8,12}$/;
        if (regex.test($("#EmerPhone").val()) == false) {
            $("#EmerPhoneErr").append("Emergency Phone is invalid");
            $("#EmerPhone").addClass("input-err");
        }
    }
    if ($("#EmerAddress").val() == "") {
        $("#EmerAddressErr").append("Emergency Address cannot be blank.");
        $("#EmerAddress").addClass("input-err");
    }
});
$("#image").change(function (e) {
    var FileName = e.target.files[0].name;
    $("#imageLabel").html(FileName);
});