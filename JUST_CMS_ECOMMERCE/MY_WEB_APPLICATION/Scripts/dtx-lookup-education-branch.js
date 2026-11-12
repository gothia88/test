$(document).ready(function () {
	$('input.education-branch')
		.each(function () {
			$(this).autocomplete({
				scroll: true,
				minLength: 0,
				autoFocus: false,
				source: function (request, response) {
					$.ajax({
						url: "/AutoComplete/GetEducationBranches",
						type: "GET",
						dataType: "json",
						data: { term: request.term },
						success: function (data) {
							response($.map(data, function (item) {
								return { label: item.FullName, value: item.Id };
							}))

						}
					})
				},
				focus: function (event, ui) {
					$(this).val(ui.item.label);
					$(this).next().val(ui.item.value);

					return (false);
				},
				select: function (event, ui) {
					$(this).val(ui.item.label);
					$(this).next().val(ui.item.value);

					return (false);
				},
				change: function (event, ui) {
					if (ui.item == null || ui.item == undefined) {
						$(this).val("");
						$(this).next().val("");
					}
				}
			});
		});
})
