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

	$(".removeuser").click(removeUser);

	function removeUser(e) {
		var target = $(e.target);
		var login = $(e.target).closest("li").children(".title").contents()[0].data.split(" ");

		$.ajax({
			url: "/Pages/AJAX/DeleteUser.cshtml",
			type: "post",
			data: login[1],
		});
	}

	$(".banuser").click(toggleUserRole);
	$(".togglevipuser").click(toggleUserRole);
	$(".toggleadminuser").click(toggleUserRole);

	function toggleUserRole(e) {
		var target = $(e.target);
		var login = $(e.target).closest("li").attr("data-login");
		var action = $(e.target).attr("data-action");
		var datastr = {
			"Login": login,
			"Action": action,
		}

		$.ajax({
			url: "/Pages/AJAX/ToggleUserRole",
			type: "post",
			data: datastr,
		});
	}





	// TODO : this f to another file
	$(".add").click(add);
	var group = $(".checkbox");

	function add(e) {
		var target = $(e.target);
		var i = 0;

		for (i = 0; i < group.length; ++i)
			if (group[i].checked) {
				alert('чекбокс включён');
			} else {
				alert('чекбокс отключён');
			}
	}
})();