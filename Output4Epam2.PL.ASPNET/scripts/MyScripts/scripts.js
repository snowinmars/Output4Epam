(function () {
	menu = $("#bs-example-navbar-collapse-1");
	page = location.pathname,
	items = menu.find("li");
	items.removeClass("active");

	switch (page) {
		case "/Pages/StructureElements/ShowMyPage.cshtml":
			items.filter("[data-type=showmypage]").addClass("active");
			break;
		case "/Pages/StructureElements/AddLot.cshtml":
			items.filter("[data-type=addlot]").addClass("active");
			break;
		case "/Pages/StructureElements/ShowMyLots.cshtml":
			items.filter("[data-type=showmylots]").addClass("active");
			break;
		case "/Pages/StructureElements/ShowAllUsers.cshtml":
			items.filter("[data-type=showallusers]").addClass("active");
			break;
		case "/Pages/StructureElements/Statistic.cshtml":
			items.filter("[data-type=statistic]").addClass("active");
			break;
		default:
			break;
	}
})();

(function () {
	$('#login-form-link').click(function (e) {
		$("#login-form").delay(100).fadeIn(100);
		$("#register-form").fadeOut(100);
		$('#register-form-link').removeClass('active');
		$(this).addClass('active');
		e.preventDefault();
	});

	$('#register-form-link').click(function (e) {
		$("#register-form").delay(100).fadeIn(100);
		$("#login-form").fadeOut(100);
		$('#login-form-link').removeClass('active');
		$(this).addClass('active');
		e.preventDefault();
	});

	$(document).on('click', '.dropdown-menu', function (e) {
		if ($(this).hasClass('keep_open')) {
			e.stopPropagation();
		}
	});

	$(".delete").click(deleteLot);

	function deleteLot(e) {
		var target = $(e.target),
			datastr = {
				"Id": target.data("id"),
			};
		$.ajax({
			url: "/Pages/AJAX/DeleteLot.cshtml",
			type: "post",
			data: datastr,
		}).success(function () {
			target.closest("li").remove();
		});
	};

	/*________________________________________*/

	$("#add_money").click(addMoney);

	function addMoney(e) { // TODO to add success()
		var target = $(e.target),
			userLogin = target.data("userlogin");

		$.ajax({
			url: "/Pages/AJAX/AddMoney.cshtml",
			type: "post",
			data: {
				"Login": userLogin,
				"Summ": "1000",
			}
		})
	}

	/*________________________________________*/

	$("#sub_money").click(subMoney);

	function subMoney(e) {
		var target = $(e.target),
			userLogin = target.data("userlogin");

		$.ajax({
			url: "/Pages/AJAX/SubMoney.cshtml",
			type: "post",
			data: {
				"Login": userLogin,
				"Summ": "1000",
			}
		})
	}

	/*________________________________________*/

	$(".p").add(".title").click(more);

	function more(e) {
		var target = $(e.target),
			guid = target.closest("li").find("img").attr("src").split("=")[1];

		location.href = "/Pages/StructureElements/ShowMore.cshtml" + "?id=" + guid;
	};

	/*________________________________________*/

	$(".removeuser").click(removeUser);

	function removeUser(e) {
		var target = $(e.target),
			login = target.closest("li").data("login");

		$.ajax({
			url: "/Pages/AJAX/DeleteUser.cshtml",
			type: "post",
			data: {
				"Login": login,
			}
		}).success(function () {
			target.closest("li").remove();
		});
	}

	/*________________________________________*/

	$(".banuser").add(".togglevipuser").add(".toggleadminuser").click(toggleUserRole); // TODO to ask

	function toggleUserRole(e) {
		var i = 0,
			length = 0,
			target = $(e.target),
			roles = target.closest("li").children(".info")[0],
			login = target.closest("li").attr("data-login"),
			action = target.attr("data-action"),
			datastr = {
				"Login": login,
				"Action": action,
			},
			rutrnroleslist = $.ajax({
				async: false,
				url: "/Pages/AJAX/ToggleUserRole",
				type: "post",
				data: datastr,
			}).responseText;

		rutrnroleslist = rutrnroleslist.slice(0, rutrnroleslist.length - 1).split(",");

		length = roles.children.length;
		for (i = 0; i < length; ++i) {
			roles.children[0].remove();
		}

		$(roles).append("<span>" + rutrnroleslist[0].trim() + "</span>");
		for (i = 1; i < rutrnroleslist.length; ++i) {
			$(roles).append("<span>, " + rutrnroleslist[i].trim() + "</span>");
		}
	}

	/*________________________________________*/

	$(".orderByName").add(".orderByDate").add(".orderByOwner").click(writeGet);

	function writeGet(e) {
		var target = $(e.target),
			action = target.data("action"),
			thispage = location.href;

		$(".selected").toggleClass("selected");
		target.toggleClass("selected");

		//location.href = location.hostname + location.pathname + "?orderby=" + action;
		location.search = "orderby=" + action;
	}

	/*________________________________________*/

	$(".filter").click(filter);

	function filter(e) {
		var target = $(e.target),
			action = target.parent().data("action");

		location.search = "filterby=" + action;
	}

	/*________________________________________*/

	$("[name=colorsheme]").click(changetheme);

	function changetheme(e) {
		var target = $(e.target),
			theme = $("[name=stylesheet]");

		loadjscssfile("/Content/" + target.data("type") + ".css", "css");
	}

	// I'm afraid to touch this stuff due to I spent five hours to find this working stuff. Let it just be.
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

	$(".buy").click(buy);

	function buy(e) {
		var id,
			target = $(e.target),
			datastr = {
				"Id": target.data("id"),
				"Summ": target.data("summ"),
			};

		id = $.ajax({
			async: false,
			url: "/Pages/AJAX/BuyLot.cshtml",
			type: "post",
			data: datastr,
		}).responseText;

		target.attr("disabled", "disabled");
	}

	/*________________________________________*/

	// TODO : this stuff to another file - to ask: how?

	$(".costchoiseitem").click(calccost);

	function calccost(e) {
		var target = $(e.target),
			costitem = target.parent().parent(".costchoise").siblings(".cost").children(".cost"),
			type = target.parent().data("type"),
			datastr = {
				"Type": type,
			},
			ratestr,
			rate,
			oldcost,
			newcost;

		ratestr = $.ajax({
			async: false,
			url: "/Pages/AJAX/GetExchangeRates.cshtml",
			type: "post",
			data: datastr,
		}).responseText;
		rate = JSON.parse(ratestr);

		oldcost = costitem.data("cost"); // always in rubles

		switch (type) {
			case "dollar":
				newcost = (oldcost - 0) / (rate.USD - 0);
				costitem.text(newcost.toFixed(2) + " $");
				break;
			case "euro":
				newcost = (oldcost - 0) / (rate.EURO - 0);
				costitem.text(newcost.toFixed(2) + " €"); // euro
				break;
			case "ruble":
				newcost = oldcost;
				costitem.text(newcost + " руб."); // rubles
				break;
			default:
				break;
		}
	};
})();