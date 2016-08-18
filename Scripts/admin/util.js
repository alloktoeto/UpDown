var UDS_Admin_Util = (function(){

	return {

		registerDropDown: function(){
			$('.dropdown-menu > li > a').click(function(){
			    var self_text = $(this).text();
			    $(this)
			        .closest('.dropdown-menu')
			        .prev('.btn')
			        .html(self_text + ' <span class="caret"></span>');
			});
		},

		registerSideMenuEvent: function(){
			if($(".menu-nav ul ul").is(":visible")){
				$(".menu-nav>ul>li.active .fa-angle-").addClass('fa-angle-down');
			} 
			$(".menu-nav>ul>li .fa-angle-")
				.removeClass('fa-angle-')
				.not('.fa-angle-down')
				.addClass('fa-angle-right');

			$(".menu-nav h3").click(function(){
				//slide up all the link lists
				$(".menu-nav ul ul").slideUp();
				$(".menu-nav>ul>li.active .fa-angle-down").removeClass('fa-angle-down').addClass('fa-angle-right');
				//slide down the link list below the h3 clicked - only if its closed
				if(!$(this).next().is(":visible")){
					$(this).next().slideDown();
					$(this).find('.fa-angle-right').removeClass('fa-angle-right').addClass('fa-angle-down');

					$(".menu-nav>ul>li.active").removeClass('active');
					$(this).parent().addClass('active');
				} else {
					$(this).parent().removeClass('active');
				}
			});
		},

		registerEvents: function(){
			this.registerDropDown();
			this.registerSideMenuEvent();
		}
		
	}

})();

$(function() {

	UDS_Admin_Util.registerEvents();

	$('.table').DataTable();
});