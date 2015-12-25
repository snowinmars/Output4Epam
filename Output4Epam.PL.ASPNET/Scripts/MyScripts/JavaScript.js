(function () {
	$(".delete").click(deleteLot);

	function deleteLot(e) {
		var target = $(e.target);
		$.ajax({
			url: "/Pages/AJAX/DeleteLot.cshtml",
			type: "post",
			data: target.attr("id"),
		}).success(function () {
			target.closest("li").remove();
		});
	};

	$(".more").click(more);
	$(".p").click(more);
	$(".title").click(more);

	function more(e) {
		var target = $(e.target);
		var guid = target.closest("li").children("p").children("img").attr("src").split("=")[1];

		location.href = "/Pages/StructureElements/ShowMore.cshtml" + "?id=" + guid;
	};

	$(".removeuser").click(removeUser);

	function removeUser(e) {
		var target = $(e.target);
		var login = target.closest("li").attr("data-login");

		$.ajax({
			url: "/Pages/AJAX/DeleteUser.cshtml",
			type: "post",
			data: login[1],
		}).success(function () {
			target.closest("li").remove();
		});
	}

	$(".banuser").click(toggleUserRole);
	$(".togglevipuser").click(toggleUserRole);
	$(".toggleadminuser").click(toggleUserRole);

	function toggleUserRole(e) {
		var target = $(e.target);
		var login = target.closest("li").attr("data-login");
		var action = target.attr("data-action");
		var datastr = {
			"Login": login,
			"Action": action,
		}

		$.ajax({
			url: "/Pages/AJAX/ToggleUserRole",
			type: "post",
			data: datastr,
		}).success(function (a) {
			a;
		})
	}

	$(".orderByName").click(writeGet);
	$(".orderByDate").click(writeGet);
	$(".orderByOwner").click(writeGet);

	function writeGet(e) {
		var target = $(e.target);
		var action = target.data("action");
		var thispage = location.href;

		$.ajax({
			url: thispage,
			type: "get",
			data: action,
		});
	}



	// TODO : this f to another file
	$(".add").click(add);
	var group = $(".checkbox");

	function add(e) {
		var form = $("form");
		var i = 0;
		form.append('<input type="text" name="Types" value="">');
		var type = form.children("[name=Types]")
		var count = 0;

		for (i = 0; i < group.length; ++i) {
			if (group[i].checked) {
				count += $(group[i]).data("enumvalue");
			}
		}
		type.val(count);
		form.submit();
		type.remove();
	}
})();