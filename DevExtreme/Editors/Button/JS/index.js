$(function () {
	DevExpress.ui.dxButton.defaultOptions({
		device: { deviceType: "desktop" },
		options: {
			elementAttr: {
				class: "custom-class",
			},
		},
	});

	var buttonInstance = $("#basicButton")
		.dxButton({
			text: "Hello Button",
			stylingMode: "contained",
			hint: "Hello",
			type: "success",
			height: "50px",
			width: "auto",
			useSubmitBehavior: true,
			elementAttr: {
				style: "color: blue",
			},
			tabIndex: 1,
			icon: "check",
			onContentReady: function () {
				console.log("content is ready ");
			},

			onInitialized: function () {
				console.log("content is initialized");
			},
		})
		.dxButton("instance");
	buttonInstance.beginUpdate();
	buttonInstance.option("hoverStateEnabled", false);
	buttonInstance.option({
		activeStateEnabled: false,
		focusStateEnabled: false,
	});
	buttonInstance.endUpdate();

	let isButtton2Dispose = false;
	function initializeButton2Instance() {
		return $("#basicButton2")
			.dxButton({
				accessKey: "B",
				text: "Button2",
				type: "success",
				stylingMode: "outlined",
				tabIndex: 3,
				onDisposing: function () {
					console.log("content is disposing");
					isButtton2Dispose = true;
				},
				
			})
			
	}

	var buttonInstance2 = DevExpress.ui.dxButton.getInstance(initializeButton2Instance())

	buttonInstance2.on("contentReady", onCLickhander);
	buttonInstance2.on("click", onCLickhander);

	let disabledButton = $("#disabledButton")
		.dxButton({
			text: "Disabled Button",
			icon: "plus",
			tabIndex: 2,
			type: "success",
			rtlEnabled: true,
			elementAttr: {
				style: "color: black",
			},
			disabled: true,

			onOptionChanged: function (e) {
				console.log("option is changed");
				if (e.name == "disabled") {
					console.log("Changed the property of disabled");
				}
				if (e.name == "rtlEnabled") {
					console.log("Changed the property of rtlEnabled");
				}
			},
		})
		.dxButton("instance");
	disabledButton.on("click", function () {
		console.log("disabled button is clicked");
	});

	let visibleInstance = $("#visibleButton")
		.dxButton({
			text: "Invisible",
			stylingMode: "text",
			onClick: function () {
				let isVisible = buttonInstance.option("visible");
				buttonInstance.option("visible", !isVisible);
				visibleInstance.option(
					"text",
					!isVisible ? "Invisible" : "Visible",
				);
			},
		})
		.dxButton("instance");
	visibleInstance.registerKeyHandler("space", () => {
		alert("space key is pressed");
	});

	visibleInstance.resetOption();

	let disposeRepaintInstance = $("#disposeRepaintButton")
		.dxButton({
			text: "Dispose",
			onClick: function () {
				if (!isButtton2Dispose) {
					$("#basicButton2").dxButton("dispose");
					disposeRepaintInstance.option("text", "Repaint");
				} else {
					isButtton2Dispose = false;
					buttonInstance2 = DevExpress.ui.dxButton.getInstance(initializeButton2Instance())
					// buttonInstance2.repaint();
					disposeRepaintInstance.option("text", "Dispose");
				}
			},
			template: function (data, container) {
				// Custom template content
				const buttonContent = $("<div>")
					.append($("<span>").addClass("dx-icon dx-icon-refresh"))
					.append($("<span>").text(" " + data.text.toUpperCase()));
				container.append(buttonContent);
			},
		})
		.dxButton("instance");

	function onCLickhander() {
		// change the disabled button to enbled and vice versa
		let isButtonDisabled = disabledButton.option("disabled");

		let isRtl = disabledButton.option("rtlEnabled");

		if (isButtonDisabled) {
			disabledButton.off("click");
		}

		console.log(visibleInstance.element());
		visibleInstance.focus();
		disabledButton.option({
			disabled: !isButtonDisabled,
			rtlEnabled: !isRtl,
		});
	}
});
