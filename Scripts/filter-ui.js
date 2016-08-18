$(function () {
	location.search.slice(1).split('&').each(
		function (ix, value) {
			var key = value.split('=')[0]; // filter
			var id = value.split('=')[1]; // 1
		});
});