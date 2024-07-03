$(function () {
	var btnInstance = $("#btnContainer")
		.dxButton({
			text: "Hello",
			onClick: function () {
				console.log("hello world");
			},
		})
		.dxButton("instance");

	var btnInstance2 = $("#btnContainer2").dxButton().dxButton("instance");

	// var btnInstance = $('#btnContainer').dxButton('instance')
	// var btnInstance2 = $('#btnContainer2').dxButton('instance')

	// get the text property from ButtonInstance1
	function getProperty() {
		// get the text from Button Instance 1
		var text = btnInstance.option("text");

		// get all the options
		var getALLOptions = btnInstance.option();
		return text;
	}

	// set the property of ButtonInstance2
	function setProperty() {
		btnInstance2.option("text", getProperty);
		btnInstance2.option("onClick", function () {
			console.log("hello");
		});
	}

	setProperty();

	// add focus method on ButtonInstance2
	$("#btnContainer2")
		.dxButton()
		.focus(function () {
			console.log("In focused");
		});

	// handle the events
	btnInstance2.on("click", function () {
		// first time execute
		console.log("Button clicked again!");
	});

	// remove the events
	function removeProperty(btn) {
		btn.off("click");
		console.log("event off");
	}

	// removeProperty(btnInstance2);


	// remove the component
	function removeComponent() {
		$("#btnContainer2").remove();
		// $("#btnContainer2").dxButton("dispose")
	}

	// removeComponent();
});
