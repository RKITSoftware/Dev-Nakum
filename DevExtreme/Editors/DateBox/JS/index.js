$(function () {
	// dateBoxInstance for basic datebox initialization
	let dateBoxInstance = $("#basicDateBox")
		.dxDateBox({
			acceptCustomValue: false,
			accessKey: "a",
			activeStateEnabled: true,
			adaptivityEnabled: false,
			applyButtonText: "Submit",
			applyValueMode: "instantly",
			dateOutOfRangeMessage: "Value is out of range",
			dateSerializationFormat: "yyyy-MM-ddTHH:mm:ss",

			// disabledDates: [
			// 	new Date("06/1/2024"),
			// 	new Date("06/2/2024"),
			// 	new Date("06/3/2024")
			// ],
			disabledDates: function (args) {
				const dayOfWeek = args.date.getDay();
				const isWeekend =
					args.view === "month" &&
					(dayOfWeek === 0 || dayOfWeek === 6);

				return isWeekend;
			},

			displayFormat: "shortdate",
			dropDownButtonTemplate() {
				return $("<img>", {
					alt: "Custom icon",
					src: "../images/dropdown.svg",
					class: "custom-icon",
				});
			},

			dropDownOptions: {
				maxHeight: 500, // Adjust the height of the dropdown as needed
				width: "auto", // Adjust the width of the dropdown as needed
			},
			focusStateEnabled: true,
			hint: "select a date",
			hoverStateEnabled: true,
			interval: 30,
			invalidDateMessage: "Value must be a date or time",
			max: new Date(),
			min: new Date(1900, 0, 1),
			name: "date",
			opened: false,
			openOnFieldClick: true,
			pickerType: "calender", //list //roller   apply only when type - date or datetime
			placeholder: "Select date and time",
			readOnly: false,
			rtlEnabled: false,
			showAnalogClock: false, // type - datetime && pickerType - calender
			showClearButton: true,
			showDropDownButton: true,
			stylingMode: "underlined", // outlined, filled
			tabIndex: 0,
			type: "datetime", // time , date
			visible: true,
			onValueChanged: function (e) {
				console.log(e.value);
			},
		})
		.dxDateBox("instance");

	// dateBoxInstance for picker type of rollers
	let dateBoxInstance2 = $("#basicDateBox2")
		.dxDateBox({
			max: new Date(),
			min: new Date(1900, 0, 1),
			opened: false,
			openOnFieldClick: false,
			pickerType: "rollers",
			placeholder: "Select date and time",
			showClearButton: true,
			showDropDownButton: true,
			stylingMode: "outlined",

			type: "datetime",
		})
		.dxDateBox("instance");

	// dateBoxInstacne for pickerType - list
	let dateBoxInstance3 = $("#basicDateBox3")
		.dxDateBox({
			max: new Date(),
			min: new Date(1900, 0, 1),
			opened: false,
			openOnFieldClick: false,
			pickerType: "list",
			placeholder: "Select date and time",
			showAnalogClock: true,
			showClearButton: true,
			showDropDownButton: true,
			stylingMode: "filled",
			type: "time",
			interval: 15, // pickerType - list & type - time
		})
		.dxDateBox("instance");
});
