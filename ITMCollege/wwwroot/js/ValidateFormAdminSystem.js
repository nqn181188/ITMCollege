function printErr(elementID, hintMess) {
    document.getElementById(elementID).innerHTML = hintMess;
}
//Validate Insert Optional Subject Form
function ValidateOpSubjectForm() {

    var OpSubjectName = document.getElementById('OpSubjectName');
    var OpSubjectNameErr = true;

    //validate FullName
    if (OpSubjectName.value == "") {
        printErr("OpSubjectNameErr", "Optional Subject Name cannot be blank.");
        OpSubjectName.classList.add("input-err");
    } else {
        printErr("OpSubjectNameErr", "");
        OpSubjectName.classList.remove("input-err");
        OpSubjectNameErr = false;
        if (OpSubjectNameErr == true) {
            return false;
        }
        else {
            return;
        }
    }
}
//Validate Insert Specialized Subject Form
function ValidateSpeSubjectForm() {

    var SpeSubjectName = document.getElementById('SpeSubjectName');
    var SpeSubjectNameErr = true;

    //validate FullName
    if (SpeSubjectName.value == "") {
        printErr("SpeSubjectNameErr", "Specialized Subject Name cannot be blank.");
        SpeSubjectName.classList.add("input-err");
    } else {
        printErr("SpeSubjectNameErr", "");
        SpeSubjectName.classList.remove("input-err");
        SpeSubjectNameErr = false;
        if (SpeSubjectNameErr == true) {
            return false;
        }
        else {
            return;
        }
    }
}
