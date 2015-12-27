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

	$(".p").add(".title").click(more);

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

	$(".banuser").add(".togglevipuser").add(".toggleadminuser").click(toggleUserRole); // TODO to ask

	function toggleUserRole(e) {
		var i = 0;
		var length = 0;
		var target = $(e.target);
		var roles = target.closest("li").children(".info")[0];
		var login = target.closest("li").attr("data-login");
		var action = target.attr("data-action");
		var datastr = {
			"Login": login,
			"Action": action,
		}

		var rutrnroleslist = $.ajax({
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
		var target = $(e.target);
		var action = target.data("action");
		var thispage = location.href;
		$(".selected").toggleClass("selected");
		target.toggleClass("selected");

		//location.href = location.hostname + location.pathname + "?orderby=" + action;
		location.search = "orderby=" + action;
	}

	/*________________________________________*/

	$(".filter").click(filter);

	function filter(e) {
		var target = $(e.target);
		var action = target.data("action");
		var thispage = location.href;

		//location.href = location.hostname + location.pathname + "?orderby=" + action;
		location.search = "filterby=" + action;
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

	//	function getCurs($moneyCode){
	//		// создаем объект для работы с XML
	//		$xml = new DOMDocument();
	//		// ссылка на сайт банка
	//		$url = 'http://www.cbr.ru/scripts/XML_daily.asp?date_req=' . date('d.m.Y');
	//		// получаем xml с курсами всех валют
	//		if ($xml->load($url)){
	//			// массив для хранения курсов валют
	//			$result = array();
	//			// разбираем xml
	//			$root = $xml->documentElement;
	//			// берем все теги 'Valute' и их содержимое
	//			$items = $root->getElementsByTagName('Valute');
	//			// переберем теги 'Valute' по одному
	//			foreach ($items as $item){
	//			// получаем код валюты
	//			    $code = $item->getElementsByTagName('CharCode')->item(0)->nodeValue;
	//			// получаем значение курса валюты, относительно рубля
	//			$value = $item->getElementsByTagName('Value')->item(0)->nodeValue;
	//			// записываем в массив, предварительно заменив запятую на точку
	//			$result[$code] = str_replace(',', '.', $value);
	//		}
	//		// возвращаем значение курса, для запрошенной валюты
	//		return $result[$moneyCode];
	//	}else{
	//	// если не получили xml возвращаем false
	//        return false;
	//}
	//}

	/*________________________________________*/

	// TODO : this f to another file
	$(".add").click(add);

	function add(e) {
		var group = $(".checkbox");
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

	$("[name=settheme]").click(settheme);

	function settheme(e) {
		var target = $(e.target);
		var group = $(".radiobox");
		var i = 0;
		var datastr = "";

		for (i = 0; i < group.length; ++i) {
			if (group[i].checked) {
				break;
			}
		}

		datastr = {
			"Type": $(group[i]).data("shemename"),
			"Login": target.closest("ul").data("userlogin"),
		};

		$.ajax({
			url: "/Pages/AJAX/ChangeTheme.cshtml",
			type: "post",
			data: datastr,
		}).success(function (r) {
			loadjscssfile("/Content/" + $(group[i]).data("shemename") + ".css", "css");
		});
	};
})();