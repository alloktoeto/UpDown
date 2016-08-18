function checkSubmit() {
    $('#captchaSpn').css('display', 'none');
    $('#errors').html('');
    var flag = true;
    var message = "";
    if ($('input[name=gender]').is(':checked')) {
        //$('#genderS').css('display', 'none');
        $('#rdo #rdoSpn').removeAttr('style');
    }
    else {
        //$('#genderS').css('display', 'block');
        flag = false;
        $('input[name=gender]').css('background', '#CD0000');
        //$('#rdo:not(span)').css('color', 'red');
        $('#rdo #rdoSpn').css('color', 'red');

        message += "<strong>*נא לסמן מין</strong><br/>"
    }

    var flagTxt = true;
    var allLbl = $('.login-details input[type=text]');
    for (var i = 0; i < allLbl.length; i++) {
        //alert($(allLbl[i]).val());
        if ($(allLbl[i]).val() == "") {
            flag = false;
            flagTxt = false;
            $(allLbl[i]).css('border', '1px solid #CD0000');
            
        }
        else {
            $(allLbl[i]).css('border', '1px solid #d8d8d8');
        }
    }

    

    var allPas = $('.login-details input[type=password]');
    for (var i = 0; i < allPas.length; i++) {
        //alert($(allLbl[i]).val());
        if ($(allPas[i]).val() == "") {
            flag = false;
            flagTxt = false;
            $(allPas[i]).css('border', '1px solid #CD0000');
        }
        else {
            $(allPas[i]).css('border', '1px solid #d8d8d8');
        }
    }


    if (flagTxt == false) {
        message += "<strong>*נא למלא כל השדות</strong><br/>"
    }


    if ($('#ConfirmPassword').val() != $('#Password').val()) {
        message += "<strong>*אימות סיסמה לא זהה</strong><br/>"
        flag = false;
    }


    if (!isValidEmailAddress($('#Email').val())) {
        message += "<strong>*מייל לא תקין</strong><br/>"
        flag = false;
    }


    var regulChk = $("#regulChk").is(":checked") ? 1 : 0;
    if (regulChk == 0) {
        message += "<strong>*נא לאשר תקנון</strong><br/>"
        flag = false;
        $("#regulChk").css('outline', '1px solid Red');
    }
    else {
        $("#regulChk").removeAttr('style');
    }


    if (flag == false) {
        $('#lblErrors').css('display', 'block');
        $('#errors').html(message);
    }


    return flag;
}



function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
}