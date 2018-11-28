"use strict";

function uploadFile(inputId, pathInput) {
	var input = $(inputId);
	var file = input.files;
	var formData = new FormData();

	formData.append("file", file[0]);

	$.ajax({
		url: "",
		data: formData,
		processData: false,
		contentType: false,
		type: "POST",
		success: function success(data) {
			$(pathInput).val(data);
			alert("Files Uploaded!");
		},
		error: function error(data) {}
	});
}

