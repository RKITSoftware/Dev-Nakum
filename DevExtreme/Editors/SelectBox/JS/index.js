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

	DevExpress.ui.dxSelectBox.defaultOptions({
		device: { deviceType: "desktop" },
		options: {
			// Here go the SelectBox properties
			width: "60%",
			dataSource: await getData(),
		},
	});

	let allUserData = await getData();
	let basicSelectBoxInstance = $("#basicSelectBox")
		.dxSelectBox({
			dataSource: new DevExpress.data.DataSource({
				store: allUserData,
				key: "id",
				group: "gender",
			}),
			deferRendering: false, // render the drop-down field's content when it is displayed. If false, the content is rendered immediately
			displayExpr: "userName",
			// valueExpr: "id",
			placeholder: "Select a User",
			searchEnabled: true,
			grouped: true,
			searchExpr: ["userName", "id"],
			searchMode: "contains",
			showDataBeforeSearch: true,
			showDropDownButton: false,
			showSelectionControls: true,
			spellcheck: true,
			wrapItemText: true,
			searchTimeout: 250,
			useItemTextAsTitle: true,
			groupTemplate(data) {
				return $(
					`<div class='custom-icon'><span class='dx-icon-user icon'></span> ${data.key.toUpperCase()}</div>`,
				);
			},
			onValueChanged: function (e) {
				//console.log("value is changed", e.value.userName);
				basicSelectBoxInstance.blur();
				// console.log();
				basicSelectBoxInstance.close();
				basicSelectBoxInstance2.focus();
				basicSelectBoxInstance2.open();

				$("#content").html(basicSelectBoxInstance.content().html());
				// $("#content").html(basicSelectBoxInstance.field())
			},
			onContentReady: function (e) {
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
		})
		.dxSelectBox("instance");
	
	let basicSelectBoxInstance2 = $("#basicSelectBox2")
		.dxSelectBox({
			displayExpr: "userName",
			items: await getData(),
			itemTemplate(data) {
				return `<div style='display: flex; flex-direction :row; justify-content : space-around;'><img width="50px" alt='user name' src='${data.avatar}' /><span class='dx-field-label'>${data.userName}	</span></div>`;
			},
			valueExpr: "userName",
			value: allUserData[3].userName,
			fieldTemplate(data, container) {
				const result = $(
					`<div style='display: flex; flex-direction :row; justify-content : space-around;'><img width="50px" alt='user name' src='${
						data ? data.avatar : ""
					}' /><div class='user-name'></div></div>`,
				);
				result.find(".user-name").dxTextBox({
					value: data && data.userName,
					readOnly: true,
					inputAttr: { "aria-label": "Name" },
				});
				container.append(result);
			},

			name: "username",
			noDataText: "Not found any data",
			acceptCustomValue: true,
			showClearButton: true,
			openOnFieldClick: false,

			onCustomItemCreating: function (e) {
				const newItem = {
					id: allUserData.length + 1,
					userName: e.text,
					avatar: "avatar",
					gender: "male",
				};
				allUserData.push(newItem);
				e.component.option("dataSource", allUserData);
				e.customItem = newItem;
			},
			onItemClick: function (e) {
				const displayValue =
					// basicSelectBoxInstance2.option("displayValue");
					basicSelectBoxInstance2.option("value");
				console.log("clicked on", displayValue.userName);
			},
			
		})
		.dxSelectBox("instance");

	// console.log(basicSelectBoxInstance.getDataSource()._items);
	let getSelectedValueInstance = $("#getSelectedValue")
		.dxButton({
			text: "Get Data",
			onClick: function (e) {
				const displayValue =
					basicSelectBoxInstance.option("selectedItem");
				alert("Hello " + JSON.stringify(displayValue.userName));
			},
		})
		.dxButton("instance");
});
