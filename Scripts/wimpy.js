// Global variables
var currentFile   = { src : '', html: '' };
var hidden_player = null;
var hidden_list   = null;
var i_fa          = null;
var audioTimer    = null;
//-------------------------------------

function playAudio(that) {
    // Check for audio element support.
    if (window.HTMLAudioElement && hidden_player) {
        try {
        	// Skip loading if no files in the list
        	if(hidden_list.value == '') return false;
            
          // Skip loading if current file hasn't changed.
          if (hidden_list.value !== currentFile.src) {
            hidden_player.src = hidden_list.value;
            setCurrentFile( {src: hidden_list.value} );

            hidden_player.play();

            setPlayerTitle('play');
            setAudioLength();    
            switchPlayPauseIcon('fa-pause');
            
            audioTimer = setInterval(setAudioLength,1000);
          } else {
            if (hidden_player.paused) {
              switchPlayPauseIcon('fa-pause');
              hidden_player.play();
              audioTimer = setInterval(setAudioLength,1000);
            } else {
              clearInterval(audioTimer);
              hidden_player.pause();
              switchPlayPauseIcon('fa-play');
            }
          }
        } catch (e) {
            if (alert("Error:" + e));
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
      $("#tmp-timing-slider").slider('option', 'value', 0);
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
  if(obj && (obj.src !== undefined) && (obj.src != '')) {
  	currentFile.html = $('#hidden-mylist>option').eq(hidden_list.options.selectedIndex);
  	currentFile.src = obj.src;
  } else {
  	alert("Can't load the audio");
  }
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
  }
}

function switchPlayPauseIcon(state){
	var player_btn = $('#tmp-play');
    switch(state){
      case 'fa-play':
        player_btn.removeClass('fa-pause').addClass('fa-play').attr('title','Play Audio');
        i_fa.removeClass('fa-pause').addClass('fa-play').attr('title','Play');
        if($(i_fa).next('label').length){
          $(i_fa).next('label').text('Play');
        }
        break;
      case 'fa-pause':
        player_btn.removeClass('fa-play').addClass('fa-pause').attr('title','Pause Audio');
        $('.fpb-play>i.fa-pause').removeClass('fa-pause').addClass('fa-play');
        i_fa.removeClass('fa-play').addClass('fa-pause').attr('title','Pause');
        if($(i_fa).next('label').length){
          $(i_fa).next('label').text('Pause');
        }
        break;
    }
}

function setAudioLength(){ 
	hidden_player = document.getElementById('hidden-player');
  	var length = hidden_player.duration;

  if(length && hidden_player.currentTime > 0)
  {
    var clock = {
    	  hours: Math.floor((length - hidden_player.currentTime)/360),
      minutes: Math.floor((length - hidden_player.currentTime)/60),
      seconds: Math.round((length - hidden_player.currentTime)%60)
    };

    if(clock.seconds == 60){
        clock.seconds = 59;
    }

    var result  = (clock.hours<10) ? '0'+clock.hours : clock.hours;
        result += ':'+((clock.minutes<10) ? '0'+clock.minutes : clock.minutes);
        result += ':'+((clock.seconds<10) ? '0'+clock.seconds : clock.seconds);
    $('.tmp-audio-length').text(result);

    var currlengthsec = length-(clock.hours*360+clock.minutes*60+clock.seconds);
    var timingwidth   = Math.floor(currlengthsec/length*100);
    $("#tmp-timing-slider").slider('option', 'value', timingwidth);

    if(result == '00:00:00') stopAudio();
  } 
}

function loadAudio(that)
{ 
  var value       = that.attr('value');
  var figcaption  = that.closest('.card-wrap').next('figcaption');
  var library     = that.closest('.card-wrap').next('figcaption').children('.library').text();
  var author      = that.closest('.card-wrap').next('figcaption').find('.author').text();
  var product_img = that.closest('.card-wrap').children('.card.front').children('.product-img').attr('src');

  $(hidden_list).html('').append(
  	'<option value="'+value+'" '+
    '        data-library="'+library+'" '+
    '        data-author="'+author+'" '+
    '        data-img="'+product_img+'">'+
    '</option>'
  );
}

function slidePlayer()
{
  if($('#tmp-slide').hasClass('fa-angle-up'))
  {
    $('.main-container, .menu-panel-left').animate({paddingTop:24}, 700);
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
    $('.main-container, .menu-panel-left').animate({paddingTop:height}, 700);
    $('.top-music-player-wrap,#top-music-player').animate({height:height}, 700);
  }
}

$(function() {
    hidden_player = document.getElementById('hidden-player');
    hidden_list   = document.getElementById('hidden-mylist');

    $("#tmp-timing-slider").slider({
      min: 0,
      max: 100,
      orientation: "horizontal",
      range: "min",
      animate: true,
      value: 0,
      slide: function( event, ui ) {
      	var _hp = document.getElementById('hidden-player');
	  	if( _hp && _hp.duration ){
	  		var timingwidth = _hp.duration*ui.value/$("#tmp-timing-slider").slider('option','max');
	      	$("#tmp-timing-slider").slider('option', 'value', timingwidth);
	        _hp.currentTime = parseFloat(timingwidth);
	  	}
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

    document.getElementById('hidden-player').volume = parseFloat('0.'+$("#tmp-volume-slider").slider("value"));
    document.getElementById('hidden-player').currentTime = parseFloat($("#tmp-timing-slider").slider("value"));

    $('.fpb-play').click(function(){
    	i_fa = $(this).children('i'); 
      loadAudio($(this));
      playAudio($(this));
    });
  });