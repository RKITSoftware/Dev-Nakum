$(function () {
	DevExpress.ui.dxTextBox.defaultOptions({
		device: { deviceType: "desktop" },
		options: {
			// Here go the TextBox properties
			width: "60%",
		},
	});
	let basicTextBoxInstance = $("#basicTextBox")
		.dxTextBox({
			placeholder: "Enter a text",
			maxLength: "20",
			stylingMode: "filled",
			mode: "text",
			onEnterKey: function (e) {
				basicTextBoxInstance2.focus();
			},
		})
		.dxTextBox("instance");

	let basicTextBoxInstance2 = $("#basicTextBox2")
		.dxTextBox({
			mask: "+A1-00000-00000", //A digit, a space, "+" or "-" sign.
			maskChar: "#",
			stylingMode: "underlined",
			hint: "mobile number",
			useMaskedValue: true, // Ensure the value includes the mask characters
			value: "+91", // Ensure the prefix is set as the initial value
			placeholder: "Enter mobile number",
			maskRules: {
				A: (char) => char == "9",
			},
			maskInvalidMessage: "contant number is not from india",
			mode: "tel",
			showMaskMode: "onFocus",

			onValueChanged: function (e) {
				const maskValue = basicTextBoxInstance2.option("value");
				console.log(maskValue);
			},
		})
		.dxTextBox("instance");

	window.onload = function () {
		basicTextBoxInstance.focus();
	};
});
