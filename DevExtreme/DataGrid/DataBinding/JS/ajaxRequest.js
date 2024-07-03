$(function () {
	var store = new DevExpress.data.CustomStore({
		key: "id",
		load: function () {
			return $.ajax({
				type: "get",
				url: "https://6673ada075872d0e0a933549.mockapi.io/api/user",
				success: function (response) {
					return response;
				},
			});
		},
	});
	$("#gridContainerAJAX").dxDataGrid({
		dataSource: store,
		keyExpr: "id",
		columns: ["name", "email", "gender"],
		showBorders: true,
	});
});
