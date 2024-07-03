$(async function () {
	var store = new DevExpress.data.CustomStore({
		key: "id",
		loadMode: "row",
		cacheRawData: true,

		byKey: async function (key) {
			return $.ajax({
				type: "get",
				url: `https://6673ada075872d0e0a933549.mockapi.io/api/user/${key}`,
				success: function (response) {
					console.log(response);
					return response;
				},
			});
		},

		load: async function () {
			console.log("inside load");
			return $.ajax({
				type: "get",
				url: "https://6673ada075872d0e0a933549.mockapi.io/api/user",
				success: function (response) {
					return response;
				},
			});
		},
		insert: function (values) {
			console.log("insert the data");
			return $.ajax({
				type: "post",
				url: "https://6673ada075872d0e0a933549.mockapi.io/api/user",
				data: values,
			});
		},

		remove: function (key) {
			return $.ajax({
				type: "delete",
				url: `https://6673ada075872d0e0a933549.mockapi.io/api/user/${key}`,
			});
		},
		update: function (key, values) {
			return $.ajax({
				type: "put",
				url: `https://6673ada075872d0e0a933549.mockapi.io/api/user/${key}`,
				data: values,
			});
		},

		onInserted: function (values, key) {
			basicSelectBoxInstance.option("dataSource", store);
			console.log("after insert the data");
			store.load();
		},
		onInserting: function (values) {
			console.log("before insert the data");
		},
		errorHandler: function (error) {
			console.log(error.message);
		},
		onLoaded: function (result) {
			console.log(`after loading the data`);
		},
		onLoading: function (loadOptions) {
			console.log(`before laoding the data`);
		},
		onModified: function () {
			console.log("after modified the data");
		},
		onModifying: function () {
			console.log("before modify the data");
		},
		onPush: function (changes) {
			console.log("Before pushing changes:", changes);
		},
		onRemoved: function (key) {
			console.log("Item removed:", key);
			store.load();
		},
		onRemoving: function (key) {
			console.log("Before removing:", key);
		},
		onUpdated: function (key, values) {
			console.log("Item updated:", key, values);
			store.load();
		},
		onUpdating: function (key, values) {
			console.log("Before updating:", key, values);
		},
	});

	var basicSelectBoxInstance = $("#basicSelectBox")
		.dxSelectBox({
			// dataSource: store,
			dataSource: new DevExpress.data.DataSource({
				store: store,
			}),
			placeholder: "Select a value",
			displayExpr: "name",
			valueExpr: "id",
			deferRendering: true,
			// onOptionChanged: function(e){
			// 	if(e.name =="dataSource"){
			// 		store.load()
			// 	}
			// }
		})
		.dxSelectBox("instance");

	function refreshDataSource() {
		store.load().done(function (data) {
			basicSelectBoxInstance.option("dataSource", data);
		});
	}

	// Button for insert the data into dataStore
	var BtnInsert = $("#btnInsert")
		.dxButton({
			text: "Insert",
			type: "default",
			onClick: function (e) {
				store.load().done(function (data) {
					console.log(data);
					const id = Number(data[data.length - 1].id) + 1;
					let user = {
						name: "Test User" + id,
						email: "test" + id + "@gmail.com",
						gender: "male",
						id: id,
					};
					store
						.insert(user)
						.done(function () {
							console.log("insert data successfully");
							refreshDataSource();
						})
						.fail(function () {
							console.log("insert fail");
						});
				});
			},
		})
		.dxButton("instance");

	// // Button for update the data into dataStore
	var btnUpdate = $("#btnUpdate")
		.dxButton({
			text: "Update",
			type: "success",
			onClick: function () {
				store.load().done(function (data) {
					const id = data[data.length - 1].id;
					const gender = data[data.length - 1].gender;

					store
						.update(id, {
							gender: gender == "male" ? "female" : "male",
						})
						.done(() => {
							console.log("user is successfully updated");
							refreshDataSource();
						});
				});
			},
		})
		.dxButton("instance");

	// Button for remove the data into dataStore
	var btnRemoved = $("#btnRemoved")
		.dxButton({
			text: "Remove",
			type: "danger",
			onClick: function () {
				store.load().done(function (data) {
					var id = data[data.length - 1].id;
					console.log(id);
					store
						.remove(id)
						.done(() => {
							console.log("successfully removed last user");
							refreshDataSource();
						})
						.fail((error) => {
							console.log("remove fail");
						});
				});
			},
		})
		.dxButton("instance");

	// Button for get the data from dataSouce
	var btnStore = $("#btnStore")
		.dxButton({
			text: "Store Data",
			type: "normal",
			onClick: function () {
				store.load().done(function (data) {
					console.log(data);
				});
			},
		})
		.dxButton("instance");
});
