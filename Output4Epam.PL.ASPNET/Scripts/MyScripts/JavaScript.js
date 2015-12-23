(function () {
	$(".delete").click(deleteLot);

	function deleteLot(e) {
		var target = $(e.target);
		$.ajax({
			url: "/Pages/AJAX/DeleteLot.cshtml",
			type: "post",
			data: target.attr("id"),
		});
	};

	$(".more").click(more);
	$(".p").click(more);
	$(".title").click(more);

	function more(e) {
		var target = $(e.target);
		var guid = $(e.target).closest("li").children("p").children("img").attr("src").split("=")[1];

		location.href = "/Pages/StructureElements/ShowMore.cshtml" + "?id=" + guid;
	};
})();