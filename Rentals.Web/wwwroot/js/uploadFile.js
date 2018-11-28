function uploadFile(inputId, onSucces, onFailure) {
	var input = document.getElementById(inputId);
	var file = input.files;
	var formData = new FormData();

	formData.append("file", file[0]);

	$.ajax(
		{
			url: "/Admin/Uploads/Upload",
			data: formData,
			processData: false,
			contentType: false,
			type: "POST",
			success: function (data) {
				onSucces(data);
			},
			error: function (data) {
				onFailure(data);
			}
		}
	);
}