
$(document).ready(function () {
    $("#registrationForm").hide();
});
$("#btnSubmit").click(function (e) {
    e.preventDefault();
    $("#regNumErr").empty();
    $("#regnum").removeClass("input-err");
    var regNum = $("#regnum").val();
    var regex = /^ST\d{8,8}$/;
    if (regNum == "") {
        $("#registrationForm").hide();
        $("#regnum").addClass("input-err");
        $("#regNumErr").append("Registration Number cannot be blank.");
    } else {
        if (!regex.test(regNum)) {
            $("#registrationForm").hide();
            $("#regnum").addClass("input-err");
            $("#regNumErr").append("Registration Number must follow the pattern STxxxxxxxx. Example : ST12345678");
        } else {
            $.ajax({
                type: "POST",
                url: "/Client/Registrations/GetAdmissionInfor",
                data: { 'regNum': regNum },
                success: function (res) {
                    if (!res) {
                        $("#registrationForm").hide();
                        $("#regnum").addClass("input-err");
                        $("#regNumErr").append("Registration Number is not exits.");
                    } else {
                        if (res.status == "0") {
                            $("#registrationForm").hide();
                            $("#regnum").addClass("input-err");
                            $("#regNumErr").append("Admission Status is WAITING. Please wait until the status is ACCEPTED .");
                        }
                        if (res.status == "2") {
                            $("#registrationForm").hide();
                            $("#regnum").addClass("input-err");
                            $("#regNumErr").append("Admission Status is REJECTED. You cannot registration in ITM College.");
                        }
                        if (res.status == "1") {
                            $("#registrationForm").show();
                            $("#fullname").val(res.fullName);
                            $("#Dob").val(res.dateOfBirth);
                            if (res.gender == "true") {
                                $("#male").prop("checked", true);
                            } else {
                                $("#female").prop("checked", true);
                            }
                            $("#resadd").val(res.resAdd);
                            $("#peradd").val(res.perAdd);
                            $("#stream").val(res.stream);
                            $("#field").val(res.field);
                            $("#email").val(res.email);
                            $("#RegNum").val(res.regNum);
                        }
                        $.ajax({
                            type: "POST",
                            url: "/Client/Registrations/GetSpeSubjectList",
                            data: { 'FieldId': parseInt(res.fieldId) },
                            success: function (SpeSubjectList) {
                                $.each(SpeSubjectList, function (index,value) {
                                    $("#SpeSubject").append('<option value="' + value.subjectId + '">' + value.subjectName + '</option>');
                                });
                            },
                        });
                    }
                },
                error: function () {
                    alert("Error. Please try again later.")
                },

            });
        }
    }
});