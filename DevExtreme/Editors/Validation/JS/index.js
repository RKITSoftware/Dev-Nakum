$(function () {
	// for mange the promise of validationCallBack()
	const sendRequest = function (value) {
		return new Promise((resolve) => {
			setTimeout(() => {
				resolve()
			}, 1000)
		})
	}

	const eyeButton = (id) => {
		const htmlId = `#${id}`
		const result = [
			{
				name: 'password',
				location: 'after',
				options: {
					icon: 'user',
					stylingMode: 'text',
					onClick: function () {
						const editor = $(htmlId).dxTextBox('instance')

						const isPassword = editor.option('mode') == 'password'
						editor.option('mode', isPassword ? 'text' : 'password')
					},
				},
			},
		]

		return result
	}
	$('#summary').dxValidationSummary({})
	let email = $('#email-validation')
		.dxTextBox({
			placeholder: 'Enter an email',

			stylingMode: 'underlined',
		})
		.dxValidator({
			validationRules: [
				{
					type: 'required',
					message: 'Email is required',
				},
				{
					type: 'email',
					message: 'Email must be validated',
				},
				{
					type: 'async',
					message: 'Email is already registered',
					validationCallback(params) {
						console.log(params)
						return sendRequest().then(() => {
							password.focus()
						})
					},
				},
			],
		})
		.dxTextBox('instance')

	let password = $('#password-validation')
		.dxTextBox({
			placeholder: 'Enter a password',

			stylingMode: 'outlined',
			mode: 'password',
			onValueChanged: function (e) {
				// if there is a value in confirm password and changed the value of password there should be validate
				if (cfPassword.option('value')) {
					$('#confirm-password-validation').dxValidator('validate')
				}
			},
			onEnterKey: function () {
				cfPassword.focus()
			},
			buttons: eyeButton('password-validation'),
		})
		.dxValidator({
			validationRules: [
				{
					type: 'required',
					message: 'Password is required',
				},
			],
		})
		.dxTextBox('instance')

	let cfPassword = $('#confirm-password-validation')
		.dxTextBox({
			placeholder: 'Enter a confirm password',

			stylingMode: 'outlined',
			mode: 'password',
			buttons: eyeButton('confirm-password-validation'),
		})
		.dxValidator({
			validationRules: [
				{
					type: 'compare',
					comparisonTarget: function (e) {
						if (password) {
							return password.option('value')
						}
						return null
					},
					message: 'password and confim password does not match',
				},
				{
					type: 'required',
					message: 'Confirm password is required',
				},
			],
		})
		.dxTextBox('instance')

	let name = $('#name-validation')
		.dxTextBox({
			placeholder: 'Enter your name',
			stylingMode: 'filled',
		})
		.dxValidator({
			validationRules: [
				{
					type: 'required',
					message: 'Name is required',
				},
				{
					type: 'pattern',
					pattern: /^[a-zA-Z]+$/,
					message: 'Digits does not allowed name',
				},
				{
					type: 'stringLength',
					min: 3,
					message: 'Name must have at least 3 character',
				},
			],
		})
		.dxTextBox('instance')

	let pincode = $('#pincode-validation')
		.dxNumberBox({
			placeholder: 'Enter your area pincode',
			showSpinButtons: true,
			max: 999999,
			showClearButton: true,
			onEnterKey: function () {
				phone.focus()
			},
		})
		.dxValidator({
			validationRules: [
				{
					type: 'stringLength',
					min: 6,
					max: 6,
					message: 'Length of pincode is must be 6 digits',
				},
				{
					type: 'required',
					message: 'Pincode is required',
				},
			],
		})
		.dxNumberBox('instance')

	const maxDate = new Date()
	maxDate.setFullYear(maxDate.getFullYear() - 18)
	let dob = $('#date-validation')
		.dxDateBox({
			placeholder: 'Enter date of birth',
			invaliddateMessage:
				'The date must have the following format: MM/dd/yyyy',
		})
		.dxValidator({
			validationRules: [
				{
					type: 'required',
					message: 'Date of Birth is required',
				},
				{
					type: 'range',
					max: new Date().setFullYear(new Date().getFullYear() - 18),
					message: 'You must be at least 18 years old',
				},
			],
		})
		.dxDateBox('instance')

	let gender = $('#gender-validation')
		.dxRadioGroup({
			dataSource: ['Male', 'Female'],
			layout: 'horizontal',
		})
		.dxValidator({
			type: 'required',
			message: 'Gender is required',
		})
		.dxRadioGroup('instance')

	let country = $('#country-validation')
		.dxSelectBox({
			dataSource: countries,
			searchEnabled: true,
			onSelectionChanged: function () {
				country.close()
				city.focus()
			},
		})
		.dxValidator({
			type: 'required',
			message: 'Country is required',
		})
		.dxSelectBox('instance')

	let city = $('#city-validation')
		.dxTextBox({
			inputAttr: { 'aria-label': 'City' },

			onEnterKey: function () {
				address.focus()
			},
		})
		.dxValidator({
			validationRules: [
				{
					type: 'required',
					message: 'City is required',
				},
				{
					type: 'pattern',
					pattern: '^[^0-9]+$',
					message: 'Do not use digits in the City name.',
				},
				{
					type: 'stringLength',
					min: 2,
					message: 'City must have at least 2 symbols',
				},
			],
		})
		.dxTextBox('instance')

	let address = $('#address-validation')
		.dxTextArea({
			validationMessagePosition: 'left',
			inputAttr: { 'aria-label': 'Address' },

			autoResizeEnabled: true,
		})
		.dxValidator({
			validationRules: [
				{
					type: 'required',
					message: 'Address is required',
				},
			],
		})
		.dxTextArea('instance')

	let phone = $('#phone-validation')
		.dxTextBox({
			mask: '+\\91 00000-00000',
			maskChar: '#',
			inputAttr: { 'aria-label': 'Phone' },
			maskInvalidMessage:
				'The phone must have a correct Indian phone format',
			validationMessagePosition: 'left',
		})
		.dxValidator({
			validationRules: [
				{
					type: 'pattern',
					pattern: /^[06-9]\d{9}$/,
					message:
						'The phone must have a correct Indian phone format',
				},
				{
					type: 'async',
					message: 'Phone number is already registered',
					validationCallback(params) {
						return sendRequest().then(() => {
							return true // Replace this with actual phone number existence check
						})
					},
				},
				{
					type: 'required',
					message: 'Phone number is required',
				},
			],
		})
		.dxTextBox('instance')

	$('#check')
		.dxCheckBox({
			value: false,
			text: 'I agree to the Terms and Conditions',
			validationMessagePosition: 'right',
		})
		.dxValidator({
			validationRules: [
				{
					type: 'compare',
					comparisonTarget() {
						return true
					},
					message: 'You must agree to the Terms and Conditions',
				},
			],
		})

	let profile = $('#profile-validation')
		.dxFileUploader({
			accept: 'image/*',
			multiple: false,
			allowedFileExtensions: ['.jpg', '.jpeg', '.gif', '.png'],
			uploadUrl:
				'https://js.devexpress.com/Demos/NetCore/FileUploader/Upload',
			hint: 'Upload the profile picture',
			invalidFileExtensionMessage:
				'Invalid file type use only from [.jpg, .jpeg, .gif, .png]',
			invalidMaxFileSizeMessage: 'file size is not allowed more than 4MB',
			maxFileSize: 4000000,
		})
		.dxValidator({
			validationRules: [
				{
					type: 'required',
					message: 'Porfile Picture is required',
				},
			],
		})
		.dxFileUploader('instance')

	$('#form').on('submit', (e) => {
		DevExpress.ui.notify(
			{
				message: 'You have submitted the form',
				position: {
					my: 'center top',
					at: 'center top',
				},
			},
			'success',
			3000,
		)

		e.preventDefault()
	})

	$('#button').dxButton({
		width: '120px',
		text: 'Register',
		type: 'default',
		useSubmitBehavior: true,
	})
})
