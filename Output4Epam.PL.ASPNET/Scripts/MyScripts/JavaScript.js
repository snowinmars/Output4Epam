(function () {
	$(".delete").click(deleteLot);

	function deleteLot(e) {
		var target = $(e.target);
		$.ajax({
			url: "/Pages/AJAX/DeleteLot.cshtml",
			type: "post",
			data: target.attr("id"),
		});
	}
})();