function fbclick() {
    var cb = function (response) {
        console.log('FB.login callback', response);
        if (response.status === 'connected') {
            console.log('User logged in');
            return;
        } else {
            console.log('User is logged out');
        }
    };
    testAPI();
    FB.login(cb, { scope: 'email' });
}

function statusChangeCallback(response) {
    if (response.status === 'connected') {
        // Logged into your app and Facebook.
//        testAPI();
    } else if (response.status === 'not_authorized') {
        // The person is logged into Facebook, but not your app.
        document.getElementById('status').innerHTML = 'Please log ' +
        'into this app.';
    } else {
        // The person is not logged into Facebook, so we're not sure if
        // they are logged into this app or not.
        document.getElementById('status').innerHTML = 'Please log ' +
        'into Facebook.';
    }
}

//function checkLoginState() {
//    FB.getLoginStatus(function (response) {
//        statusChangeCallback(response);
//    });
//}

//function getStatusAfterLogin() {
//    FB.getLoginStatus(function (response) {
//        statusChangeCallback(response);
//        return response.status;
//    });
//}

window.fbAsyncInit = function () {
    FB.init({
        appId: '932059190199198',
        xfbml: true,
        version: 'v2.4'
    });

    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });

    FB.Event.subscribe('auth.statusChange', function (response) {
        if (response.status == 'connected') {
//            testAPI();
        }
    });
};

function testAPI() {
    FB.api('/me?fields=gender,first_name,last_name,email', function (response) {
        checkAndAddToDbFromFb(response.gender, response.first_name, response.last_name, response.email);
    });
}

function checkAndAddToDbFromFb(title, fname, lname, email) {
    var from = 'f';
    $.ajax({
        url: '../Account/LoginWithFB',
        type: 'POST',
        data: { gender: title.toString(), fname: fname.toString(), lname: lname.toString(), email: email.toString(), from: from.toString() },
        beforeSend: function () {
        },
        success: function (data) {
            if (data == true || data == "True" || data == "true") {
                location.reload();
            }
        },
        error: function (request, status, error) {
            alert("Error");
        }
    });
}

function logoutfromFb() {
    FB.logout(function (response) {
    });
}






















////$(document).ready(function () {
////    if (document.getElementById('fb-script') != undefined) {
////        var e = document.createElement('script');
////        e.type = 'text/javascript';
////        e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
////        e.async = true;
////        document.getElementById('fb-root').appendChild(e);
////    }
////});

////$(document).ready(function () {
////    if (document.getElementById('fb-script') != undefined) {
////        var e = document.createElement('script');
////        e.type = 'text/javascript';
////        e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
////        e.async = true;
////        document.getElementById('fb-root').appendChild(e);
////    }
////});



//function fbclick() {

//    var cb = function (response) {
//        console.log('FB.login callback', response);
//        if (response.status === 'connected') {
//            console.log('User logged in');
//            return;
//        } else {
//            console.log('User is logged out');
//        }
//    };
//    FB.login(cb, { scope: 'email' });
//    location.reload();
//}



//// This is called with the results from from FB.getLoginStatus().
//function statusChangeCallback(response) {
//    console.log('statusChangeCallback');
//    console.log(response);
//    // The response object is returned with a status field that lets the
//    // app know the current login status of the person.
//    // Full docs on the response object can be found in the documentation
//    // for FB.getLoginStatus().
//    if (response.status === 'connected') {
//        // Logged into your app and Facebook.
//        testAPI();
//    } else if (response.status === 'not_authorized') {
//        // The person is logged into Facebook, but not your app.
//        document.getElementById('status').innerHTML = 'Please log ' +
//        'into this app.';
//    } else {
//        // The person is not logged into Facebook, so we're not sure if
//        // they are logged into this app or not.
//        document.getElementById('status').innerHTML = 'Please log ' +
//        'into Facebook.';
//    }
//}

//// This function is called when someone finishes with the Login
//// Button.  See the onlogin handler attached to it in the sample
//// code below.
//function checkLoginState() {
//    FB.getLoginStatus(function (response) {
//        statusChangeCallback(response);
//    });
//}


//function getStatusAfterLogin() {
//    FB.getLoginStatus(function (response) {
//        statusChangeCallback(response);
//        //alert(response.status);
//        return response.status;
//    });
//}


////window.fbAsyncInit = function () {
////    FB.init({
////        appId: '932148283523622',
////        xfbml: true,
////        version: 'v2.4'
////    });
////};


//window.fbAsyncInit = function () {
//    FB.init({
//        appId: '932059190199198',
//        xfbml: true,
//        version: 'v2.4'
//    });

////    FB.init({
////        appId: '{932148283523622}', // 932148283523622 '{932059190199198}',
////        xfbml: true,
////        version: 'v2.0'
////    });


//    // Now that we've initialized the JavaScript SDK, we call 
//    // FB.getLoginStatus().  This function gets the state of the
//    // person visiting this page and can return one of three states to
//    // the callback you provide.  They can be:
//    //
//    // 1. Logged into your app ('connected')
//    // 2. Logged into Facebook, but not your app ('not_authorized')
//    // 3. Not logged into Facebook and can't tell if they are logged into
//    //    your app or not.
//    //
//    // These three cases are handled in the callback function.

//    FB.getLoginStatus(function (response) {
//        statusChangeCallback(response);
//    });

//    FB.Event.subscribe('auth.statusChange', function (response) {
//        if (response.status == 'connected') {
//            testAPI();
//        }
//    });
//};



////(function (d, s, id) {
////alert(1234);
////    var js, fjs = d.getElementsByTagName(s)[0];
////    if (d.getElementById(id)) return;
////    js = d.createElement(s); js.id = id;
////    js.src = "//connect.facebook.net/en_US/sdk.js";
////    fjs.parentNode.insertBefore(js, fjs);
////} (document, 'script', 'facebook-jssdk'));


////(function (d, s, id) {
////    var js, fjs = d.getElementsByTagName(s)[0];
////    if (d.getElementById(id)) return;
////    js = d.createElement(s);
////    js.id = id;
////    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&appId=446312808846047&version=v2.0";
////    fjs.parentNode.insertBefore(js, fjs);

////}
////    (document, 'script', 'facebook-jssdk'));



//function testAPI() {
//    alert(1);
//    console.log('Welcome!  Fetching your information.... ');
//    FB.api('/me?fields=gender,first_name,last_name,email', function (response) {
//        console.log(JSON.stringify(response));
//        console.log('Successful login for: ' + response.name);
//        //        document.getElementById('status').innerHTML =
//        //        'Thanks for logging in, ' + response.name + '!';
//        checkAndAddToDbFromFb(response.gender, response.first_name, response.last_name, response.email);
//        //        if (response.email != '' && response.email != null) {
//        //            //alert("email1");
//        //        }
//    });
//}







//function checkAndAddToDbFromFb(title, fname, lname, email) {
//    var from = 'f';
////    if (email == "") {
////        jAlert('<strong>פרופיל שלך לא מאפשר לראות אל המייל שלך</strong><br/><strong>תשנה הגדרות או תירשם בדרך הרגילה</strong>', 'חשוב!', function () {
////            return;
////        });
////    }
//    $.ajax({
//        url: '../Account/LoginWithFB',
//        type: 'POST',
//        data: { gender: title.toString(), fname: fname.toString(), lname: lname.toString(), email: email.toString(), from: from.toString() },
//        beforeSend: function () {
//        },
//        success: function (data) {
//            if (data == true || data=="True" || data=="true") {
//                alert("Success");
//                //                window.location.href = '../Account/Success';
//            }
//        },
//        error: function (request, status, error) {
//            alert("Error");
//        }
//    });
//}




//function afterConnect() {
//    //alert(8);
//    FB.getLoginStatus(function (response) {
//        if (response.session) {
//            //alert(43);
//            //window.location = "../Account/FbLogin?token=" + response.session.access_token;
//        } else {
//            //alert(33);
//        }
//    });
//};



//function logoutfromFb() {
//    FB.logout(function (response) {
//        //alert(' Person is now logged out');
//    });
//}