(function () {
	var jVal = {
		'inputTitle': function () {
			var $inputElement = $("#inputTitle"),
				$inputElementAfterCorrect = $(".inputafterTitle").filter(".correct"),
				$inputElementAfterWrong = $(".inputafterTitle").filter(".wrong");

			if (($inputElement.val().length < 3) ||
				($inputElement.val().length > 50)) {
				$inputElementAfterWrong.removeClass("hidden");
				$inputElementAfterCorrect.addClass("hidden");
			}
			else {
				$inputElementAfterCorrect.removeClass("hidden");
				$inputElementAfterWrong.addClass("hidden");
			}
		},
		'inputSity': function () {
			var $inputElement = $("#inputSity"),
				$inputElementAfterCorrect = $(".inputafterSity").filter(".correct"),
				$inputElementAfterWrong = $(".inputafterSity").filter(".wrong");

			if (($inputElement.val().length < 2) ||
				($inputElement.val().length > 100)) {
				$inputElementAfterWrong.removeClass("hidden");
				$inputElementAfterCorrect.addClass("hidden");
			}
			else {
				$inputElementAfterCorrect.removeClass("hidden");
				$inputElementAfterWrong.addClass("hidden");
			}
		},
		'inputCost': function () {
			var $inputElement = $("#inputCost"),
				$inputElementAfterCorrect = $(".inputafterCost").filter(".correct"),
				$inputElementAfterWrong = $(".inputafterCost").filter(".wrong"),
				value = parseInt($inputElement.val());

			if (isNaN(value)) {
				$inputElementAfterWrong.removeClass("hidden");
				$inputElementAfterCorrect.addClass("hidden");
			}
			else {
				if ((+$inputElement.val() <= 0) ||
					(+$inputElement.val() > 2147483647)) { // Int32 C# MaxValue
					$inputElementAfterWrong.removeClass("hidden");
					$inputElementAfterCorrect.addClass("hidden");
				}
				else {
					$inputElementAfterCorrect.removeClass("hidden");
					$inputElementAfterWrong.addClass("hidden");
				}
			}
		},
		'inputInfo': function () {
			var $inputElement = $("#inputInfo"),
				$inputElementAfterCorrect = $(".inputafterInfo").filter(".correct"),
				$inputElementAfterWrong = $(".inputafterInfo").filter(".wrong");

			if (($inputElement.val().length < 0) ||
				($inputElement.val().length > 500)) {
				$inputElementAfterWrong.removeClass("hidden");
				$inputElementAfterCorrect.addClass("hidden");
			}
			else {
				$inputElementAfterCorrect.removeClass("hidden");
				$inputElementAfterWrong.addClass("hidden");
			}
		},
	};

	$("#inputTitle").change(jVal.inputTitle);
	$("#inputSity").change(jVal.inputSity);
	$("#inputCost").change(jVal.inputCost);
	$("#inputInfo").change(jVal.inputInfo);

	$(".add").click(add);

	function add(e) {
		var group = $(".checkbox").find(":input"),
			form = $("form");
		form.append('<input type="text" name="Types" value="">'); // TODO fix this hack later
		var i = 0,
			type = form.children("[name=Types]"),
			count = 0;

		for (i = 0; i < group.length; ++i) {
			if (group[i].checked) {
				count += $(group[i]).data("enumvalue");
			}
		}

		type.val(count);

		if (!$(".inputafter").filter(".correct").hasClass("hidden"))
		{
			form.submit();
		}

		type.remove();
	}
})();