$(() => {
	const fileUploader = $('#file-uploader')
		.dxFileUploader({
			multiple: false,
			accept: '*',
			accessKey: 'a',
			value: [],
			showFileList: true,
			uploadMode: 'instantly',
			allowCanceling: true,
			uploadUrl:
				'https://js.devexpress.com/Demos/NetCore/FileUploader/Upload',
			onValueChanged(e) {
				const files = e.value
				console.log(files)
				if (files.length > 0) {
					$('#selected-files .selected-item').remove()
					$.each(files, (i, file) => {
						const $selectedItem =
							$('<div />').addClass('selected-item')
						$selectedItem.append(
							$('<span />').html(`Name: ${file.name}<br/>`),
							$('<span />').html(`Size ${file.size} bytes<br/>`),
							$('<span />').html(`Type ${file.type}<br/>`),
							$('<span />').html(
								`Last Modified Date: ${file.lastModifiedDate}`,
							),
						)
						$selectedItem.appendTo($('#selected-files'))
					})
					$('#selected-files').show()
				} else {
					$('#selected-files').hide()
				}
			},
		})
		.dxFileUploader('instance')

	let fileTbeRemoved
	const fileExtension = $('#fileExtension')
		.dxFileUploader({
			allowedFileExtensions: ['.jpg', '.jpeg', '.gif', '.png'],
			disabled: false,
			hint: 'Upload the file',
			hoverStateEnabled: true,
			dialogTrigger: '#dropzone-external',
			uploadUrl:
				'https://js.devexpress.com/Demos/NetCore/FileUploader/Upload',
			invalidFileExtensionMessage:
				'Invalid file type use only from [.jpg, .jpeg, .gif, .png]',
			invalidMaxFileSizeMessage:
				'file size is not allowed more than 10MB',
			invalidMinFileSizeMessage:
				'file size is not allowed less than 10KB',
			maxFileSize: 10000000,
			minFileSize: 10000,
			uploadMode: 'instantly',
			uploadAbortedMessage: 'upload is cancelled',
			selectButtonText: 'select a file here',
			uploadMethod: 'POST',
			onBeforeSend: function (e) {
				console.log('onBeforeSend File Name:', e.file.name)
			},
			onContentReady: function (e) {
				console.log('content is ready')
			},
			onDisposing: function (e) {
				console.log('content is disposing')
			},
			onDropZoneEnter: function (e) {
				console.log('entered into dropzone')
				toggleDropZoneActive(e.dropZoneElement, true)
			},
			onDropZoneLeave: function (e) {
				console.log('leave from dropzone')
				toggleDropZoneActive(e.dropZoneElement, false)
			},
			onFilesUploaded: function (e) {
				console.log('onFilesUploaded')
				removeFileButton.option('disabled', false)
			},
			onProgress: function (e) {
				console.log('file upload in process')
				const $chunkItem = $('<div />').addClass('chunk-item')
				$chunkItem.append(
					$('<span />').html(`segmentSize: ${e.segmentSize}<br/>`),
					$('<span />').html(
						`bytesLoaded ${e.bytesLoaded} bytes<br/>`,
					),
					$('<span />').html(`bytesTotal ${e.bytesTotal}<br/>`),
				)
				$chunkItem.appendTo($('#chunkInfo'))
			},
			onUploadAborted: function (e) {
				console.log('file upload is cancelled')
			},
			onUploadError: function (e) {
				console.log('error while uploading the file :', e.error)
			},
			onUploadStarted: function (e) {
				console.log('file upload is just started')
				abortButton.option('disabled', false)

				fileTbeRemoved = e.file
			},
			onValueChanged: function (e) {
				console.log(
					'file is changed previous file is :',
					e.previousValue,
				)
			},
			readyToUploadMessage: 'file is ready to upload',
		})
		.dxFileUploader('instance')

	function toggleDropZoneActive(dropZone, isActive) {
		if (isActive) {
			console.log('class is added')
			dropZone.classList.add('dx-theme-accent-as-border-color')
		} else {
			console.log('class is removed')
			dropZone.classList.remove('dx-theme-accent-as-border-color')
		}
	}

	$('#accept-option').dxSelectBox({
		inputAttr: { 'aria-label': 'Accept Option' },
		dataSource: [
			{ name: 'All types', value: '*' },
			{ name: 'Images', value: 'image/*' },
			{ name: 'Videos', value: 'video/*' },
		],
		valueExpr: 'value',
		displayExpr: 'name',
		value: '*',
		onValueChanged(e) {
			fileUploader.option('accept', e.value)
		},
	})

	$('#upload-option').dxSelectBox({
		items: ['instantly', 'useButtons'],
		value: 'instantly',
		inputAttr: { 'aria-label': 'Upload Option' },
		onValueChanged(e) {
			fileUploader.option('uploadMode', e.value)
		},
	})

	$('#multiple-option').dxCheckBox({
		value: false,
		text: 'Allow multiple files selection',
		onValueChanged(e) {
			console.log(e.value)
			fileUploader.option('multiple', e.value)
		},
	})

	const abortButton = $('#abort-upload')
		.dxButton({
			text: 'Abort',
			disabled: true,

			onClick: function () {
				fileExtension.abortUpload()
				abortButton.option('disabled', true)
				console.log('abort successfully')
			},
		})
		.dxButton('instance')

	const removeFileButton = $('#remove-file')
		.dxButton({
			text: 'Remove File',
			disabled: true,
			onClick: function () {
				console.log(fileTbeRemoved)
				fileExtension.removeFile(fileTbeRemoved)
				removeFileButton.option('disabled', true)
				console.log('file removed')
			},
		})
		.dxButton('instance')
})
