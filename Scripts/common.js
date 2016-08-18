$(document).ready(function () {
    //make buttons clickable
    $('.drop-menu-item').click(clickAsideDropMenu);
    $('.aside-menu .item').click(clickAsideMenu);
    $('.dialog-times, .buttonpanel .btn-cancel').click(click_buttonpanelCancel);
    $('.buttonpanel .btn-signup').click(click_buttonpanelSignUp);
    $('.buttonpanel .btn-signin').click(click_buttonpanelSignIn);
    $('.dropdown-wrap').click(clickDropdownEvent);
    $('.dropdown > li').click(selectDropdownEvent);
    $('.currency-dropdown > li').click(clickSetCurrency);
    //$('.featured-prod .card.back').click(function (e) {
    //    if(!$(e.target).parent().hasClass('featured-prods-btn')){
    //        location.pathname = $(this).closest('li').data('href');
    //    }
    //});
    // set elements to be accordion
    $( ".accordion" ).accordion({
        collapsible: true, 
        active: false
    });
    //tooltip of all ellements
    $(document).tooltip({ position: { my: "center top+25", at: "center left" } });
    //main picture's slider
    $("#owl-slider").owlCarousel({
        autoPlay:true,
        slideSpeed : 300,
        paginationSpeed : 400,
        rewindSpeed: 100,
        singleItem: true
    });

    $('.mobile-menu .btn-menu').on('click',function(e){
        e.preventDefault();
        var animate_speed = 10;

        // open menu
        if($('.main-container').css('left') == '0px'){
            $('.main-container').css(
                {
                    'position':'fixed',
                    'left': '80%'
                });
            $('.menu-container').css({left: "0"}); 
            $('.menu-container, .menu-panel-left').removeClass('hide');
            // toggle button menu icon
            $('.mobile-menu>.btn-menu>i.fa').toggleClass('fa-bars hidden');
        }
        // close menu
        else{
            $('.main-container').css(
                {
                    'position':'relative',
                    'left': '0px'
                });
            $('.menu-container').css({'left': '-100%'});
            $('.menu-container, .menu-panel-left').addClass('hide');
            // toggle button menu icon
            $('.mobile-menu>.btn-menu>i.fa').toggleClass('fa-bars hidden');
        }
    });

    $('.tmp-controls-wrap2').click(function(){
        if($(this).css('opacity') == '0'){
            $('.mobile-menu .btn-menu').trigger('click');
        }
    });

    $(window).on('resize', function windowResize(){
        $(".menu aside").css("width", $(".container").width()-30);
        $(".mt-right .menu aside").css("right", $(".container").width()-105);
    });

    /*set blur/focus text on input elemnts*/
    $(".placeholder")
        .on('focus', function(){ 
            var placeholder = $(this).attr('data-placeholder');
            var value = $(this).val();
            // if empty
            if(!value.length || value == placeholder) { $(this).val(""); }
        })
        .on('blur', function(){ 
            var placeholder = $(this).attr('data-placeholder');
            var value = $(this).val();
            if(!value.length) { $(this).val(placeholder); }
        });

    /*set key press on aside left menu*/
    $(document).keyup( function (e) {
        var display = $('.aside-lvl2').css('display');
        //close menu window if pressed Esc and it's opened
        if( e.keyCode == 27  && display == 'block' ) {
            clickAsideDropMenu();
        }
    });

});

function loader(onOff) {
    if (onOff) {
        $('#load_ajax').css('display', 'block');
    }
    else {
        $('#load_ajax').css('display', 'none');
    }
}

/* HOME */

function clickDropdownEvent() {
    var dropdown = $(this).children('.dropdown');
    if(dropdown.is(':visible')){
        dropdown
            .hide()
            .prev()
            .attr("class", "fa fa-caret-down");
    } else {
        dropdown
            .show()
            .prev()
            .attr("class", "fa fa-caret-up");
    }
}

function selectDropdownEvent(){
    $(this)
        .closest('.dropdown-wrap')
        .children('.value')
        .text( $(this).text() );
}

/*set selected currency string*/
function clickSetCurrency() {
    $('.currency>.fa')
        .first()
        .attr( 'class', $(this).children("i").attr("class") );
}

/*aside left drop menu*/
function clickAsideDropMenu() {
    var thisMenu = $(this).find('.aside-lvl2');
    $('.aside-menu li').removeClass('active');
    
    if(thisMenu.css('display') == 'block') {
        thisMenu.css('display','none');
    }
    else{
        $('.aside-lvl2').css('display','none');
        thisMenu.css('display','block');
        $(this).addClass('active');
    }  
}

/*aside left menu*/
function clickAsideMenu() {
    $('.aside-menu>li').css('background-color','#363636');
    $('.aside-menu li section').css('border-bottom','1px solid #434343');
    $(this).css('background-color','rgb(89, 88, 88)');
    $(this).css('border-bottom','none');
    //set text of title
    var newTitle = $(this).find('span.aside-menu-title').text();
    var currTitle = $('.central-home .title-block');
    if(newTitle != currTitle.text() ){
        currTitle.text(newTitle);
    }
}
    
/*close dialog on "X" or "Cancel" button */
function click_buttonpanelCancel(){ 
    $("#dialog-signin, #dialog-signup, #dialog-prod-info").dialog("close");
}

function click_buttonpanelSignUp(){ }

function click_buttonpanelSignIn(){ }

/*set selected currency string*/
function clickSetCurrency() {
    $('.currency>.fa')
        .first()
        .attr( 'class', $(this).children("i").attr("class") );
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}