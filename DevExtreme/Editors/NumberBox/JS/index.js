$(function () {
	DevExpress.ui.dxNumberBox.defaultOptions({
		device: { deviceType: "desktop" },
		options: {
			elementAttr: {
				class: "custom-class",
			},
		},
	});

	let basicNumberBoxInstance = $("#basicNumberBox")
		.dxNumberBox({
			width: "50%",
			rtlEnabled: true,
			min: 10,
			tabIndex: 1,
			value: 15,
			max: 20,
			readOnly: true,
			name: "basic-button",
			focusStateEnabled: false,
		})
		.dxNumberBox("instance");

	let basicNumberBoxInstance2 = $("#basicNumberBox2")
		.dxNumberBox({
			width: "50%",
			tabIndex: 2,
			placeholder: "password",
			hint: "password",
			hoverStateEnabled: false,
			mode: "password",
			inputAttr: {
				"aria-label": "Password",
			},

			value: "123123",
			buttons: [
				{
					name: "password",
					location: "after",
					options: {
						icon: "user",
						stylingMode: "text",
						onClick() {
							basicNumberBoxInstance2.option(
								"mode",
								basicNumberBoxInstance2.option("mode") ===
									"text"
									? "password"
									: "text",
							);
						},
					},
				},
			],
		})
		.dxNumberBox("instance");

	function numberValidate(num) {
		return num % 3 == 0;
	}

	let basicNumberBoxInstance3 = $("#basicNumberBox3")
		.dxNumberBox({
			width: "50%",
			tabIndex: 5,
			placeholder: "number is in modulo of 3",
			onValueChanged: function (e) {
				const isValidNumber = numberValidate(e.value);

				if (isValidNumber) {
					basicNumberBoxInstance3.option({
						validationError: null,
						validationErrors: null,
						validationStatus: "valid",
					});
				} else {
					basicNumberBoxInstance3.option({
						validationError: { message: "Invalid number" },
						validationErrors: [
							{ message: "number should be modulo of 3" },
						],
						validationStatus: "invalid",
					});
				}
			},
			validationMessageMode: "always",
		})
		.dxNumberBox("instance");

	let disabledInstance = $("#disabled")
		.dxNumberBox({
			width: "50%",
			tabIndex: 3,
			value: 0,
			stylingMode: "filled",
			disabled: true,
			height: "auto",
		})
		.dxNumberBox("instance");

	function getCurrentValue(numberInstance) {
		return numberInstance.option("value");
	}
	let eventInstance = $("#event")
		.dxNumberBox({
			width: "50%",
			format: "#,##,##0.##",
			type: "decimal",
			step: 1, //number of counter changed when spin the mouse wheel
			stylingMode: "underlined",
			tabIndex: 4,
			valueChangeEvent: "change",
			showClearButton: true,
			showSpinButtons: true,
			useLargeSpinButtons: true,
			value: 0,

			onChange: function () {
				let updatedValue = getCurrentValue(eventInstance);
				disabledInstance.option("value", updatedValue);
				console.log("changed the value", updatedValue);
			},
			onContentReady: function () {
				console.log("content is ready");
			},
			onCopy: function (e) {
				console.log("successfully copied !!");
			},
			onCut: function () {
				console.log("sussessfully cut the value !!");
			},
			onDisposing: function () {
				console.log("content is disposing");
			},
			onEnterKey: function () {
				basicNumberBoxInstance.focus();
			},
			onFocusIn: function () {
				console.log("focus in");
			},
			onFocusOut: function () {
				console.log("focus out");
			},
			onInitialized: function () {
				console.log("content is initialized");
			},
			onInput: function () {
				console.log("current value is", getCurrentValue(eventInstance));
			},
			onKeyDown: function () {
				console.log("Key Down");
			},
			onKeyUp: function () {
				console.log("Key Up");
			},
			onPaste: function () {
				console.log("value paste successfully");
			},
			onValueChanged: function (e) {
				console.log("Value changed:", e.value);
			},
		})
		.dxNumberBox("instance");

	let visibleInstance = $("#visible")
		.dxButton({
			text: "invisible",
			onClick: function () {
				let isNumberBoxVisible = eventInstance.option("visible");
				eventInstance.option("visible", !isNumberBoxVisible);
				visibleInstance.option(
					"text",
					isNumberBoxVisible ? "invisible" : "visible",
				);
			},
		})
		.dxButton("instance");
});
