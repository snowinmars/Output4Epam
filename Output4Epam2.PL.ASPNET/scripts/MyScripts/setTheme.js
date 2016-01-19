(function () {
$("[name=settheme]").click(settheme);

function settheme(e) {
	var target = $(e.target),
		group = $(".radiobox"),
		i = 0,
		datastr = "",
		headimg;

	for (i = 0; i < group.length; ++i) {
		if (group[i].checked) {
			break;
		}
	}

	datastr = {
		"Type": $(group[i]).data("shemename"),
		"Login": target.siblings("ul").data("userlogin"),
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
