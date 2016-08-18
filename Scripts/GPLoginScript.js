function signinCallback(authResult) {
    if (authResult['status']['signed_in'] && authResult['status']['method'] == 'PROMPT') {
        disconnectUser(authResult['access_token']);
        document.getElementById('signinButton').setAttribute('style', 'display: none');
        // User clicked on the sign in button. Do your staff here.
    } else if (authResult['status']['signed_in']) {
        // This is called when user is signed in to Google but hasn't clicked on the button.
    } else {
        // Update the app to reflect a signed out user
        // Possible error values:
        //   "user_signed_out" - User is signed-out
        //   "access_denied" - User denied access to your app
        //   "immediate_failed" - Could not automatically log in the user
        console.log('Sign-in state: ' + authResult['error']);
    }
//    if (authResult['access_token']) {
//        //alert(authResult['access_token']);
//        disconnectUser(authResult['access_token']);
//        // Успешная авторизация
//        // Скрыть кнопку входа после авторизации пользователя, например:
//        document.getElementById('signinButton').setAttribute('style', 'display: none');
//    } else if (authResult['error']) {
//        // Возможные коды ошибок:
//        //   "access_denied" – пользователь отказался предоставить приложению доступ к данным
//        //   "immediate_failed" – не удалось выполнить автоматический вход пользователя
//        // console.log('There was an error: ' + authResult['error']);
//    }
//    else {
//    }
}



function disconnectUser(access_token) {
    //alert(access_token);
    var revokeUrl = 'https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=' + access_token;

    // Выполните асинхронный запрос GET.
    $.ajax({
        type: 'GET',
        url: revokeUrl,
        async: false,
        contentType: "application/json",
        dataType: 'jsonp',
        success: function (data) {
            //alert(121);
            checkAndAddToDbFromG(data.given_name, data.family_name, data.email);
            //alert(nullResponse.name);
            //  alert(nullResponse.given_name);
            //alert(nullResponse.email_verified);
            //alert(nullResponse.id);
            //alert(nullResponse.email);
            //alert(nullResponse.family_name);
            //alert(nullResponse.link);
            //alert(nullResponse.picture);
            //alert(nullResponse.gender);
            //                                        var email = nullResponse['emails'].filter(function (v) {
            //                                            return v.type === 'account'; 
            //                                        })[0].value;
            //                                        alert(email);
            //getEmail();
            // Выполните любое действие после отключения пользователя
            // Ответ всегда неопределенный.
        },
        error: function (e) {
            //alert(33);
            // Обработайте ошибку
            // console.log(e);
            // Вы можете предложить пользователю отключится вручную
            // https://plus.google.com/apps
        }
    });
}




function checkAndAddToDbFromG(fname, lname, email) {
    var from = 'g';
   
    $.ajax({
        url: '../Account/LoginWithGPlus',
        type: 'POST',
        data: { fname: fname, lname: lname, email: email, from: from },
        beforeSend: function () {
        },
        success: function (data) {
            //alert(data);
            if (data == 'success' || data == true || data == "True" || data == "true") {
                location.reload();
            }
        },
        error: function (request, status, error) {

        }
    });
}









//        function getEmail() {
//            //alert("getEmail");
//            // Загружает библиотеки oauth2 для активации методов userinfo.
//            gapi.client.load('oauth2', 'v2', function () {
//                var request = gapi.client.oauth2.userinfo.get();
//                request.execute(getEmailCallback);
//            });
//        }

//        function getEmailCallback(obj) {
//            //alert("getEmailCallback");
//            var el = document.getElementById('email');
//            var email = '';
//            //alert(obj.toString());
//            //alert(obj['email']);
//            if (obj['email']) {
//                email = 'Email: ' + obj['email'];
//                alert(email);
//            }

//            //console.log(obj);   // Отменяет преобразование для проверки всего объекта.

////            el.innerHTML = email;
////            toggleElement('email');
//        }

////        function toggleElement(id) {
////            var el = document.getElementById(id);
////            if (el.getAttribute('class') == 'hide') {
////                el.setAttribute('class', 'show');
////            } else {
////                el.setAttribute('class', 'hide');
////            }
////        }
