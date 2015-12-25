/// <reference path="C:\prg\Output4Epam\Output4Epam.PL.ASPNET\Content/SetStyle.cshtml" />
/// <reference path="C:\prg\Output4Epam\Output4Epam.PL.ASPNET\Content/SetStyle.cshtml" />
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

/*________________________________________*/

	$(".more").click(more);
	$(".p").click(more);
	$(".title").click(more);

	function more(e) {
		var target = $(e.target);
		var guid = target.closest("li").children("p").children("img").attr("src").split("=")[1];

		location.href = "/Pages/StructureElements/ShowMore.cshtml" + "?id=" + guid;
	};

/*________________________________________*/

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

/*________________________________________*/

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
		}).success(function (a) { // TODO
			a;
		})
	}

/*________________________________________*/

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

/*________________________________________*/

	$("[name=colorsheme]").click(changetheme);
	
	function changetheme(e) {
		var target = $(e.target);

		var theme = $("[name=stylesheet]");
		loadjscssfile("/Content/" + target.data("type") + ".css", "css");
	}

	// I'm afraid to touch this stuff due to I spent five hours to find this working stuff. Due to my code on jQuery didn't rerender page.
	function loadjscssfile(filename, filetype) {
		if (filetype == "js") { //if filename is a external JavaScript file
			var fileref = document.createElement('script');
			fileref.setAttribute("type", "text/javascript");
			fileref.setAttribute("src", filename);
		}
		else if (filetype == "css") { //if filename is an external CSS file
			var fileref = document.createElement("link");
			fileref.setAttribute("rel", "stylesheet");
			fileref.setAttribute("type", "text/css");
			fileref.setAttribute("href", filename);
			fileref.setAttribute("id", "stylesheet");
		}
		$("#stylesheet").remove();

		if (typeof fileref != "undefined")
			document.getElementsByTagName("head")[0].appendChild(fileref);
	}
/*________________________________________*/


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