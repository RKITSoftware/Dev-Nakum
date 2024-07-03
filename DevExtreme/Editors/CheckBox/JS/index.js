$(function () {
	// default options
	// Set default options for all CheckBox instances
	DevExpress.ui.dxCheckBox.defaultOptions({
        device: { deviceType: "desktop" },
		options: {
			// add default class 
			class: "custom-class",
		},
	});

	// function which can change the focus while value is changed
    function valueChangedEvent() {
        console.log("onvalue changed");
		// focus to accessKey Instance
        accessKey.focus();
    };


	// Basic CheckBox Initialization
	var checkbBox = $("#basicCheckBox")
		.dxCheckBox({
			text: "I agree to the Terms and Conditions",
			value: false,
			name: "tnc",
			elementAttr: {
				class: "custom-class",
			},
			onValueChanged: valueChangedEvent
		})
		.dxCheckBox("instance");

	checkbBox.beginUpdate();
	checkbBox.option("activeStateEnabled", true);
	checkbBox.option("focusStateEnabled", true);
	checkbBox.option("hoverStateEnabled", true);
	checkbBox.endUpdate();

	// CheckBox with AccessKey
	let accessKey = $("#accessKeyCheckBox")
		.dxCheckBox({
			text: "Press 'A' to check/uncheck",
			value: false,
			accessKey: "a",
            onValueChanged: valueChangedEvent
		})
		.dxCheckBox("instance");

	// Read-Only CheckBox
	$("#readOnlyCheckBox").dxCheckBox({
		text: "Read-Only CheckBox",
		value: false,
		readOnly: true,
		hint: "read-only",
	});

	// CheckBox with Events
	let eventCheckBox = $("#eventCheckBox")
		.dxCheckBox({
			text: "Event Handling CheckBox",
			value: false,
			onValueChanged: function (e) {
				console.log(`Checkbox value changed to ${e.previousValue} to ${e.value} `);
			},
			onContentReady: function (e) {
				console.log("Checkbox content is ready"); // 2nd
			},
			onDisposing: function (e) {
				console.log("Checkbox is being disposed");
			},
			onInitialized: function (e) {
				console.log("Checkbox has been initialized"); //1st
			},
			onOptionChanged: function (e) {
				// console.log("Checkbox option changed");
			},
		})
		.dxCheckBox("instance");

	// $("#eventCheckBox").dxCheckBox("dispose")
	//$("#basicCheckBox").dxCheckBox("dispose")
	// $("#basicCheckBox").remove();

	// Dynamic CheckBox for Methods and Dynamic Changes
	var btnVisibleInstance = $("#visible").dxButton({
		onClick: function (e) {
			var isVisible = dynamicCheckBox.option("visible");
			dynamicCheckBox.option("visible", !isVisible);
		},
	});
	var dynamicCheckBox = $("#dynamicCheckBox")
		.dxCheckBox({
			text: "Dynamic CheckBox",
			value: false,
			disabled: true,
			visible: true,
		})
		.dxCheckBox("instance");

	// RTL Enabled CheckBox
	var rtlCheckBox = $("#rtlEnabledCheckBox")
		.dxCheckBox({
			text: "Left to Right",
			value: true,
			onValueChanged: function (e) {
				rtlCheckBox.option("rtlEnabled", e.value);
				rtlCheckBox.option(
					"text",
					e.value ? "Left to Right" : "Right to Left",
				);
			},
		})
		.dxCheckBox("instance");

	// TabIndex
	for (let i = 0; i < 4; i++) {
		let id = `tab${i + 1}`;
		let textValue = `checkBox${i + 1}`;
		$(`#${id}`).dxCheckBox({
			text: textValue,
			tabIndex: i + 1,
		});
	}

	// Handle button events 
	$("#btnEvent").dxButton({
        text: "Handler",
		onClick: function () {
            eventCheckBox.on("valueChanged",function(){
                console.log("valuechanged event");
            })
            eventCheckBox.on("disposing",function(){
                console.log("disposing event");
            })
            eventCheckBox.on("initialized",function(){
                console.log("initialized event");
            })
            eventCheckBox.on("optionChanged",function(){
                // console.log("optionChanged event");
            })
            eventCheckBox.on("contentReady",function(){
                console.log("contentReady event");
            })
        },
	});
	
	// register handle key event on accesskeyInstance
    accessKey.registerKeyHandler("enter",function(e){
        console.log("enter key is pressed");
        let previousValue = accessKey.option("value");
        accessKey.option("value",!previousValue);
    })
    
});
