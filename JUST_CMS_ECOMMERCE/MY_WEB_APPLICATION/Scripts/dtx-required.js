$(document).ready(function () {
	$("*[data-val-required]:not(input[type=checkbox])").each(function () {
		var varControlName = $(this).attr("id");
		//alert(varControlName);

		$("label[for='" + varControlName + "']").append("&nbsp;*");
	})
})
