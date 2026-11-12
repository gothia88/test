$(document).ready(function () {

	$("input#[INPUT_TEXT_NAME]").kendoComboBox({

		dataValueField: [DATA_VALUE_FIELD],
		dataTextField: [DATA_TEXT_FIELD],

		text: '', // Default: [""]
		value: '', // Default: [""]

		enable: [ENABLE], // Default: [true]
		suggest: false, // Default: [false]
		autoBind: false, // Default: [true]
		valuePrimitive: true, // Default: [false]

		ignoreCase: true, // Default: [true]
		filter: 'contains', // Default: ["none"]

		// دستور ذيل مهم است
		delay: 0, // Defautl: [200]

		// دستور ذيل مهم است
		highlightFirst: false, // Default: [true]

		index: -1, // Default: [-1]
		height: 200, // Default: [200]
		minLength: 1, // Default: [1]
		placeholder: [PLACE_HOLDER], // Default: [""]

		animation: {
			open: {
				duration: 400,
				effects: 'fadeIn slideIn:down',
			},
			close: {
				duration: 200,
				effects: 'fadeOut slideIn:up',
			},
		},

		template: '',
		headerTemplate: '',

		dataSource: {

			serverFiltering: true,

			transport: {
				read: {
					type: 'POST',
					dataType: 'json',
					url: [URL],
				},

			},

		},

		select: function (e) {

			if (this.selectedIndex == -1) {

				this.value("");

			}

			$('input#[INPUT_HIDDEN_NAME]').val(this.value());

		},

		change: function (e) {

			if (this.selectedIndex == -1) {

				this.value("");

			}

			$('input#[INPUT_HIDDEN_NAME]').val(this.value());

		},

		filtering: function (e) {

			if ((e.filter == null) || (e.filter.value == null) || (e.filter.value == "")) {

				this.value("");

			}

			$('input#[INPUT_HIDDEN_NAME]').val(this.value());

		},

	});

	var varComboBox =
		$("input#[INPUT_TEXT_NAME]").data("kendoComboBox");

	varComboBox.value([VALUE]);

	$('input#[INPUT_HIDDEN_NAME]').val([VALUE]);

});
