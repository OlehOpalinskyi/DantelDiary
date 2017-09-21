$( document ).ready(function() {
	$('#myCarousel').carousel({
	  interval: 5000
	});

	$('#carousel-text').html($('#slide-content-0').html());

	// When the carousel slides, auto update the text
	$('#myCarousel').on('slid.bs.carousel', function (e) {
	  var id = $('.item.active').data('slide-number');
	  $('#carousel-text').html($('#slide-content-'+id).html());
	});

	$("#mytable #checkall").click(function () {
			if ($("#mytable #checkall").is(':checked')) {
				$("#mytable input[type=checkbox]").each(function () {
					$(this).prop("checked", true);
				});

			} else {
				$("#mytable input[type=checkbox]").each(function () {
					$(this).prop("checked", false);
				});
			}
		});
		
	 $("[data-toggle=tooltip]").tooltip();
    
 
	 
 });