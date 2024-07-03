$(function () {
	DevExpress.ui.dxTextArea.defaultOptions({
		device: { deviceType: "desktop" },
		options: {
			// Here go the TextBox properties
			// width: "auto",
		},
	});

	const dataItems = [
		{ text: "Low", color: "grey" },
		{ text: "Normal", color: "green" },
		{ text: "Urgent", color: "yellow" },
		{ text: "High", color: "red" },
	];

	let basicRadioGroupInstance = $("#basicRadioGroup")
		.dxRadioGroup({
			accessKey: "a",
			activeStateEnabled: true,
			dataSource: dataItems,
			disabled: false,
			displayExpr: "text",
			elementAttr: {
				class: "custom-class",
			},
			focusStateEnabled: true,
			hint: "Priority",
			itemTemplate: function (itemData, itemIndex, itemElement) {
				return itemData.text.toUpperCase()
			},
			hoverStateEnabled: true,
			layout: "horizontal",
			name: "Priority",
			onContentReady: function (e) {
				console.log("content is ready");
			},
			onDisposing: function (e) {
				console.log("content is disposing");
			},
			onInitialized: function (e) {
				console.log("content is initialized");
			},
			onOptionChanged: function (e) {
				// console.log("option is changed");
			},
			onValueChanged: function (e) {
				console.log("value is changed", e.value);
			},
			readOnly: false,
			rtlEnabled: false,
			value: dataItems[3],
			valueExpr: "text",
			visible: true,
		})
		.dxRadioGroup("instance");

	let button = $("#rtlButton")
		.dxButton({
			text: "Right to Left",
			onClick: function (e) {
				const rtl = basicRadioGroupInstance.option("rtlEnabled");
				basicRadioGroupInstance.option("rtlEnabled", !rtl);

				button.option("text", rtl ? "Right to Left" : "Left to Right");
			},
		})
		.dxButton("instance");
});
