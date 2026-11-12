tinymce.init({
	selector: "textarea",
	theme: "modern",
	relative_urls: false,
	directionality: 'rtl',
	//language: 'fa',
	fontsize_formats: "8pt 10pt 12pt 14pt 18pt 24pt 36pt",
	font_formats: "Tahoma=tahoma;Verdana=verdana;Courier New=courier new",
	plugins: [
		"advlist autolink lists link image charmap print preview hr anchor pagebreak",
		"searchreplace wordcount visualblocks visualchars code fullscreen",
		"insertdatetime media nonbreaking save table contextmenu directionality",
		"emoticons template paste textcolor sh4tinymce"
	],
	toolbar1: "undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
	toolbar2: "print preview media | forecolor backcolor | fontselect fontsizeselect | rtl ltr | sh4tinymce",
	image_advtab: true,
	templates: [
		{ title: 'Test template 1', content: 'Test 1' },
		{ title: 'Test template 2', content: 'Test 2' }
	]
});