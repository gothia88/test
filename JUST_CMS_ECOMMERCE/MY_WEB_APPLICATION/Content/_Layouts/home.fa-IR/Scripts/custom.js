$(document).ready(function () {

	$("div.product").mouseenter(function (e) {

		$(this).css("border", "1px solid black");

	});

	$("div.product").mouseleave(function (e) {

		$(this).css("border", "1px solid lightgray");

	});

	doSomeEffects();

});

function doSomeEffects() {

	SyntaxHighlighter.all();
	$("[data-toggle='tooltip']").tooltip();

}
