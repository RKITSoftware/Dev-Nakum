$(async function () {
	const getData = async () => {
		return $.ajax({
			type: "get",
			url: "https://6666d488a2f8516ff7a5223a.mockapi.io/userData",
			dataType: "json",
			success: function (response) {
				// console.log(response);
				return response;
			},
		});
	};

	DevExpress.ui.dxDropDownBox.defaultOptions({
		device: { deviceType: "desktop" },
		options: {
			// Here go the SelectBox properties
			width: "60%",
			dataSource: await getData(),
		},
	});

	let basicDropDownInstance = $("#basicDropDown")
		.dxDropDownBox({
			dataSource: await getData(),
			contentTemplate(e) {
				const $list = $("<div>").dxList({
					dataSource: e.component.option("dataSource"),
					displayExpr: "userName",
					selectionMode: "single",
					onSelectionChanged: function (arg) {
						// console.log(arg);
						e.component.option("value", arg.addedItems[0]);
						e.component.close();
					},
				});
				return $list;
			},
			displayValueFormatter: function (value) {
				if (Array.isArray(value) && value.length > 0) {
					return value[0].id + ".  " + value[0].userName;
				}
				return value;
			},

			inputAttr: {
				"aria-label": "users",
			},
			dropDownButtonTemplate() {
				return $("<img>", {
					alt: "Custom icon",
					src: "../images/dropdown.svg",
					class: "custom-icon",
				});
			},

			acceptCustomValue: true,
			deferRendering: true, // render the drop-down field's content when it is displayed. If false, the content is rendered immediately
			// displayExpr: "userName",
			valueExpr: "id",
			placeholder: "Select a User",
			showDropDownButton: true,

			onValueChanged: function (e) {
				console.log("value is changed", e.value.userName);
				// $("#content").html(basicSelectBoxInstance.field())
			},
			onClosed: function (e) {
				console.log("selection is closed");
			},
			onOpened: function (e) {
				console.log("selection is opened");
			},
			onSelectionChanged: function (e) {
				console.log("selection is changed");
			},
			dropDownOptions: {
				maxHeight: 300, // Adjust the height of the dropdown as needed
				width: "auto" // Adjust the width of the dropdown as needed
			}
		})
		.dxDropDownBox("instance");

	// var selectedIds = 0;
	// let multipleSelectionDropDown = $("#multipleSelectionDropDown")
	// 	.dxDropDownBox({
	// 		dataSource: await getData(),
	// 		contentTemplate(e) {
	// 			const $list = $("<div>").dxList({
	// 				dataSource: e.component.option("dataSource"),
	// 				displayExpr: "userName",
	// 				selectionMode: "multiple",
	// 				onSelectionChanged: function (arg) {
	// 					selectedIds = arg.addedItems.map(item=>item.id) ;
	// 					e.component.option("value",selectedIds)
	// 				},
	// 				selectedItems: selectedIds,
	// 			});
	// 			return $list;
	// 		},
	// 		displayExpr : "userName",
	// 		valueExpr: "id",
	// 		// value: [],
	// 		// valueExpr : []
	// 	})
	// 	.dxDropDownBox("instance");
});
