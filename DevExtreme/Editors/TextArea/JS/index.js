$(function () {
	DevExpress.ui.dxTextArea.defaultOptions({
		device: { deviceType: "desktop" },
		options: {
			// Here go the TextBox properties
			width: "60%",
		},
	});

	let basicTextAreaInstance = $("#basicTextArea")
		.dxTextArea({
			placeholder: "Enter the text",
			hint: "text",
			accessKey : "a",
			maxHeight: 160,
			maxLength: 10000,
			minHeight: 20,
			autoResizeEnabled: true,
			
		})
		.dxTextArea("instance");

	let basicTextAreaInstance2 = $("#basicTextArea2").dxTextArea({
		accessKey:'t',
		activeStateEnabled:true,
		autoResizeEnabled:true,
		disabled:false,
		
		focusStateEnabled:true,
		hint:"text area",
		hoverStateEnabled:true,
		
		maxHeight:160,
		maxLength:1000,
		minHeight:20,
		name:"text area",
		onChange:function (e) {
			console.log("onChange text :",e.component._changedValue);
		  },
		onContentReady:function(e){
			console.log("content is ready");
		},
		onCopy:function(e){
			console.log("content is copied");
		},
		onCut:function(e){
			console.log("content cut");
		},
		onDisposing:function(e){
			console.log("content disposing");
		},
		onEnterKey:function(e){
			console.log("enter key is pressed");
		},
		onFocusIn:function(e){
			console.log("focus In");
		},
		onFocusOut:function(e){
			console.log("foucs Out");
		},
		onInitialized:function(e){
			console.log("content initialized");
		},
		
		onKeyDown:function(e){
			console.log("key down");
		},
		onKeyUp:function(e){
			console.log("key up");
		},
		onOptionChanged:function(e){
			console.log("option changed");
		},
		onPaste:function(e){
			console.log("content paste");
		},
		onValueChanged:function(e){
			console.log("onValueChanged value :",e.value);
		},
		placeholder:"Text area",
		readOnly:false,
		rtlEnabled:false,
		spellcheck:true,
		stylingMode:"outlined",
		tabIndex:0,
		visible:true,
		}).dxTextArea("instance")
});
