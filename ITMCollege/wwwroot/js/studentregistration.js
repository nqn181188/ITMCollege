$(document).ready(function () {
    $("#registrationForm").hide();
});
$("#btnSubmit").click(function (e) {
    e.preventDefault();
    $("#regNumErr").empty();
    $("#regnum").removeClass("input-err");
    var regNum = $("#regnum").val();
    if (regNum == "") {
        $("#registrationForm").hide();
        $("#regnum").addClass("input-err");
        $("#regNumErr").append("Registration Number cannot be blank.");
    } else {
        var regex = /^ST\d{8,8}*$/;
        if (!regex.test(regNum)) {
            $("#registrationForm").hide();
            $("#regnum").addClass("input-err");
            $("#regNumErr").append("Registration Number must follow the pattern STxxxxxxxx. Example : ST12345678");
        } else {
            $.ajax({
                type: "POST",
                url: "/Client/Registrations/GetAdmissionInfor",
                data: { 'regNum': regNum },


            });
        }
    }
});