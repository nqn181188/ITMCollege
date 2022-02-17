function printErr(elementID, hintMess) {
    document.getElementById(elementID).innerHTML = hintMess;
}
function ValidateAdmissionForm() {
    
    var FullName = document.getElementById('fullname');
    var FatherName = document.getElementById('fathername');
    var MotherName = document.getElementById('mothername');
    var Dob = document.getElementById('dob');
    var Email = document.getElementById('email');
    var ResAdd = document.getElementById('resadd');
    var PerAdd = document.getElementById('peradd');
    var FullNameErr = FatherNameErr = MotherNameErr = DobErr = EmailErr = ResAddErr = PerAddErr = true;
    
    //validate FullName
    if (FullName.value == "") {
        printErr("FullNameErr", "Full Name cannot be blank.");
        FullName.classList.add("input-err");
    } else {
        printErr("FullNameErr", "");
        FullName.classList.remove("input-err");
        FullNameErr = false;
    }
    //validate FatherName
    if (FatherName.value == "") {
        printErr("FatherNameErr", "Father Name cannot be blank.");
        FatherName.classList.add("input-err");
    } else {
        printErr("FatherNameErr", "");
        FatherName.classList.remove("input-err");
        FatherNameErr = false;
    }
    //validate MotheName
    if (MotherName.value == "") {
        printErr("MotherNameErr", "Mother Name cannot be blank.");
        MotherName.classList.add("input-err");
    } else {
        printErr("MotherNameErr", "");
        MotherName.classList.remove("input-err");
        MotherNameErr = false;
    }
    //validate DOB
    if (Dob.value == "") {
        printErr("DobErr", "Date Of Birth cannot be blank.");
        Dob.classList.add("input-err");
    } else {
        printErr("DobErr", "");
        Dob.classList.remove("input-err");
        DobErr = false;
    }
    //validate Email
    if (Email.value == "") {
        printErr("EmailErr", "Email cannot be blank.");
        Email.classList.add("input-err");
    } else {
        printErr("EmailErr", "");
        Email.classList.remove("input-err");
        EmailErr = false;
    }
    //validate Residential Address
    if (ResAdd.value == "") {
        printErr("ResAddErr", "Residential Address cannot be blank.");
        ResAdd.classList.add("input-err");
    } else {
        printErr("ResAddErr", "");
        ResAdd.classList.remove("input-err");
        ResAddErr = false;
    }
    //Validate Permanent Address
    if (PerAdd.value == "") {
        printErr("PerAddErr", "Permanent Address cannot be blank.");
        PerAdd.classList.add("input-err");
    } else {
        printErr("PerAddErr", "");
        PerAdd.classList.remove("input-err");
        PerAddErr = false;
    }
    if (FullNameErr == true || FatherNameErr == true || MotherNameErr == true || DobErr == true || EmailErr == true || ResAddErr == true || PerAddErr == true) {
        return false;
    }
    else {
        return;
    }
}
