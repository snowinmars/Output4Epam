$(".add").click(add);

function add(e) {
	var group = $(".checkbox").find(":input"),
		form = $("form");
	form.append('<input type="text" name="Types" value="">');
	var i = 0,
		type = form.children("[name=Types]"),
		count = 0;

	for (i = 0; i < group.length; ++i) {
		if (group[i].checked) {
			count += $(group[i]).data("enumvalue");
		}
	}

	type.val(count);
	form.submit();
	type.remove();
}