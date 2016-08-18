// Global variables
var currentFile   = { src : '', html: '' };
var hidden_player = document.getElementById('hidden-player');
var hidden_list   = document.getElementById('hidden-mylist');
var i_fa          = null;
var audioTimer    = null;
//-------------------------------------

function playAudio() {
    // Check for audio element support.
    if (window.HTMLAudioElement && hidden_player) {
        try {
        	// Skip loading if no files in the list
        	if(hidden_list.value == '') return false;
            // Tests the paused attribute and set state.
            if (hidden_player.paused) {
            	// Skip loading if current file hasn't changed.
	            if (hidden_list.value !== currentFile.src) {
	                hidden_player.src = hidden_list.value;
	                setCurrentFile({src:hidden_list.value});
	                setPlayerTitle('play');
	                setAudioLength();
	            }
                
                switchPlayPauseIcon('fa-pause');
                hidden_player.play();
                audioTimer = setInterval(setAudioLength,1000);
            } else {
                clearInterval(audioTimer);
                hidden_player.pause();
                switchPlayPauseIcon('fa-play');
            }
        } catch (e) {
            if (window.console && console.error("Error:" + e));
        }
    }
}

function stopAudio() {
  // Check for audio element support.
  if (window.HTMLAudioElement && hidden_player) 
  {
    try {
      hidden_player.pause();
      hidden_player.currentTime = 0;
      setPlayerTitle('stop');
      switchPlayPauseIcon('fa-play');
      clearInterval(audioTimer);
    } catch (e) {
        if (window.console && console.error("Error:" + e));
    }
  }
}

//function to select previous song
function prevAudio(){
  // Check for audio element support.
  if (window.HTMLAudioElement && hidden_player) 
  {
    try {
      // Skip loading if no files in the list
	  if(hidden_list.value == '') return false;
      //Skip loading if current file hasn't changed.
      if(hidden_list.value !== currentFile.src)
      {
        currentFile.src = hidden_list.value;
      }
  
      for(i=0; i<=hidden_list.options.length; i++)  
      {
        if(hidden_list.options[i] != undefined && currentFile.src == hidden_list.options[i].value)
        {
          if(i!=0)
          {
            j=i-1;
            hidden_list.options[j].selected=true;
            hidden_player.src=hidden_list.options[j].value;
            setCurrentFile({src:hidden_list.options[j].value});
            setPlayerTitle('play');
            switchPlayPauseIcon('fa-pause');
            hidden_player.play();
            break;
          }
        } 
        else hidden_player.currentTime = 0;
      }
    } catch (e) {
      if (window.console && console.error("Error:" + e));
    }
  }
}

// Function to select next song
function nextAudio()
{
  // Check for audio element support.
  if (window.HTMLAudioElement && hidden_player) 
  {
    try {
      // Skip loading if no files in the list
      if(hidden_list.value == '') return false;
      var count=hidden_list.options.length;
      if(currentFile.src != '' && currentFile.src != hidden_list.options[count-1].value)
      {
        for(i=0; i<hidden_list.options.length; i++)
        {
          if(hidden_list.options[i] != undefined && currentFile.src==hidden_list.options[i].value)
          {
            j=i+1;
            hidden_list.options[j].selected=true;
            hidden_player.src=hidden_list.options[j].value;
            setCurrentFile({src:hidden_list.options[j].value});
            setPlayerTitle('play');
            switchPlayPauseIcon('fa-pause');
            hidden_player.play();
            break;
          }
        }
      }   
    } catch (e) {
      if (window.console && console.error("Error:" + e));
    }
  } 
}

function repeatAudio()
{
  // Check for audio element support.
  if (window.HTMLAudioElement && hidden_player) 
  {
    var el = $('#tmp-repeat');
    if(el.hasClass('fa-repeat-active'))
    {
      $('#tmp-repeat').removeClass('fa-repeat-active')
        .addClass('fa-repeat')
        .attr('title','Repeat Audio ON');
      $(hidden_player).removeAttr('loop');
    }
    else
    {
      $('#tmp-repeat').removeClass('fa-repeat')
        .addClass('fa-repeat-active')
        .attr('title','Repeat Audio OFF');
      $(hidden_player).attr('loop', '');
    }
  }
}

function setCurrentFile(obj)
{
  currentFile.html = $('#hidden-mylist>option').eq(hidden_list.options.selectedIndex);
  var arr = currentFile.html.data('maxlength').split(':');
  currentFile.html.attr('data-maxlengthsec',parseInt(arr[0]*360+arr[1]*60+arr[2]));
  if(obj.src !== undefined || obj.src != '') 
    currentFile.src = obj.src;
}

function setPlayerTitle(state){
  switch(state)
  {
    case 'play':
      var author  = currentFile.html.data('author');
      $('.tmp-audio-author').text(author);
      var library = currentFile.html.data('library');
      $('.tmp-audio-library').text(' - '+library);
      $('.tmp-img').attr('src', currentFile.html.data('img'));
      break;
    case 'stop':
      break;
  }
}

function switchPlayPauseIcon(state){
	var player_btn = $('#tmp-play');
    switch(state){
      case 'fa-play':
        player_btn.removeClass('fa-pause').addClass('fa-play').attr('title','Play Audio');
        i_fa.removeClass('fa-pause').addClass('fa-play').attr('title','Play');
        break;
      case 'fa-pause':
        player_btn.removeClass('fa-play').addClass('fa-pause').attr('title','Pause Audio');
        i_fa.removeClass('fa-play').addClass('fa-pause').attr('title','Pause');
        break;
    }
}

function setAudioLength(){
  var length = currentFile.html.data('maxlength');

  if(length !== undefined)
  {
    var arr   = length.split(':');
    var clock = {hours: parseInt(arr[0]),
                 minutes: parseInt(arr[1]),
                 seconds: parseInt(arr[2])};
    clock.seconds -= Math.floor(hidden_player.currentTime);
    if(clock.seconds < 0)
    {
      clock.seconds = 0;
      if(--clock.minutes < 0)
      {
        if(clock.hours == 0)
          clock.minutes = 0;
        else{
          --clock.hours;
          clock.minutes = 59;
        }
      }
    }
    var result  = (clock.hours<10) ? '0'+clock.hours : clock.hours;
        result += ':'+((clock.minutes<10) ? '0'+clock.hours : clock.hours);
        result += ':'+((clock.seconds<10) ? '0'+clock.seconds : clock.seconds);
    $('.tmp-audio-length').text(result);

    var maxlengthsec  = parseInt(currentFile.html.data('maxlengthsec'));
    var currlengthsec = maxlengthsec-parseInt(clock.hours*360+clock.minutes*60+clock.seconds);
    var timingwidth   = currlengthsec/maxlengthsec*100;
    $("#tmp-timing-slider").slider('option', 'value', timingwidth);

    if(result == '00:00:00') stopAudio();
  }
}

function loadAudio(that)
{ 
  var value       = that.attr('value');
  var maxlength   = that.attr('data-maxlength');
  var prod_name   = $('#name').text();
  var artist      = $('#artist').text();
  var product_img = $('#image').attr('src');

  $(hidden_list).html('').append('<option value="'+value+'" data-maxlength="'+maxlength+'"'+
      'data-library="'+prod_name+'" data-author="'+artist+'" data-img="'+product_img+'"></option>');
}

function slidePlayer()
{
  if($('#tmp-slide').hasClass('fa-angle-up'))
  {
    $('.main-container').animate({paddingTop:24}, 700);
    $('.tmp-controls-wrap1,.tmp-controls-wrap2,.tmp-controls-wrap3').animate({opacity:0}, 700);
    $('.top-music-player-wrap,#top-music-player').animate({height:24}, 
      700, function(){
        $('#tmp-slide')
          .removeClass('fa-angle-up')
          .addClass('fa-angle-down')
          .attr('title','Slide Down Player');
        var marginTop = ($(window).width() < 992) ? '-31px' : '-19px';
        $('.tmp-controls-wrap3')
          .css({marginTop:marginTop})
          .animate({opacity:1}, 700);
      });
  }
  else {
    $('.tmp-controls-wrap1,.tmp-controls-wrap2').animate({opacity:1}, 700);
    $('.tmp-controls-wrap3').animate({opacity:0},
      700, function(){
        $('#tmp-slide')
          .removeClass('fa-angle-down')
          .addClass('fa-angle-up')
          .attr('title','Slide Up Player');
        $('.tmp-controls-wrap3')
          .css({marginTop:'0px'})
          .animate({opacity:1}, 700);
    });
    var height = ($(window).width() < 992) ? '82px' : '60px';
    $('.main-container').animate({paddingTop:height}, 700);
    $('.top-music-player-wrap,#top-music-player').animate({height:height}, 700);
  }
}

$(function() {
    $("#tmp-timing-slider").slider({
      min: 0,
      max: 100,
      orientation: "horizontal",
      range: "min",
      animate: true,
      value: 0,
      slide: function( event, ui ) {
        var maxlengthsec = currentFile.html.data('maxlengthsec');
        var timingwidth = maxlengthsec*ui.value/$("#tmp-timing-slider").slider('option','max');
      	document.getElementById('hidden-player').currentTime = parseFloat(timingwidth);
        $("#tmp-timing-slider").slider('option', 'value', timingwidth);
      }
    });

    $("#tmp-volume-slider").slider({
      min: 0,
      max: 10,
      orientation: "horizontal",
      range: "min",
      animate: true,
      value: 7,
      slide: function( event, ui ) {
      	document.getElementById('hidden-player').volume = (ui.value < 10)
          ? parseFloat('0.'+ui.value) : 1;
      }
    });

    // document.getElementById('hidden-player').volume = parseFloat('0.'+$("#tmp-volume-slider").slider("value"));
    // document.getElementById('hidden-player').currentTime = parseFloat($("#tmp-timing-slider").slider("value"));

    $('.fpb-play').click(function(){
    	i_fa = $(this).children('i');
      loadAudio($(this));
      playAudio();
    });
  });