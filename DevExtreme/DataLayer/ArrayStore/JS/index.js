$(function () {
	var store = new DevExpress.data.ArrayStore({
		data: [
			{
				avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1017.jpg",
				userName: "Charlie Welch",
				gender: "female",
				id: "1",
			},
			{
				avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/509.jpg",
				userName: "Elsa Price",
				gender: "male",
				id: "2",
			},
			{
				avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/581.jpg",
				userName: "Nicolas Friesen",
				gender: "female",
				id: "3",
			},
			{
				avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/904.jpg",
				userName: "Laura Bogisich",
				gender: "male",
				id: "4",
			},
			{
				avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/93.jpg",
				userName: "Karen Parker",
				gender: "female",
				id: "5",
			},
			{
				avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/198.jpg",
				userName: "Neal Krajcik",
				gender: "female",
				id: "6",
			},
			{
				avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/273.jpg",
				userName: "Karl Kling",
				gender: "male",
				id: "7",
			},
		],
		key: "id",
		errorHandler: function (error) {
			console.log(error);
			console.log(error.message);
		},
		onInserted: function (values, key) {
			console.log("Item inserted:", values, key);
		},
		onInserting: function (values) {
			console.log("Before inserting:", values);
		},
		onLoaded: function (result) {
			console.log("Data loaded:", result);
		},
		onLoading: function (loadOptions) {
			console.log("Before loading:", loadOptions);
		},
		onModified: function () {
			console.log("Data modified");
		},
		onModifying: function () {
			console.log("Before modifying data");
		},
		onPush: function (changes) {
			console.log("Before pushing changes:", changes);
		},
		onRemoved: function (key) {
			console.log("Item removed:", key);
		},
		onRemoving: function (key) {
			console.log("Before removing:", key);
		},
		onUpdated: function (key, values) {
			console.log("Item updated:", key, values);
		},
		onUpdating: function (key, values) {
			console.log("Before updating:", key, values);
		},
	});

	var basicSelectBoxInstance = $("#basicSelectBox")
		.dxSelectBox({
			dataSource: store,
			
			placeholder: "Select a value",
			displayExpr: "userName",
			valueExpr: "id",
			deferRendering: true,
		})
		.dxSelectBox("instance");

	// Button for insert the data into dataStore
	var BtnInsert = $("#btnInsert")
		.dxButton({
			text: "Insert",
			type: "default",
			onClick: function (e) {
				let user = {
					avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1017.jpg",
					userName: `Test User ${store._array.length + 1}`,
					gender: "male",
					id: store._array.length + 1,
				};
				store
					.insert(user)
					.done(function () {
						console.log("insert data successfully");
						user["id"] = store._array.length + 1;
						user["userName"] = `Test User ${
							store._array.length + 1
						}`;

						basicSelectBoxInstance.option("dataSource", store);
					})
					.fail(function () {
						console.log("insert fail");
					});
			},
		})
		.dxButton("instance");

	// Button for update the data into dataStore
	var btnUpdate = $("#btnUpdate")
		.dxButton({
			text: "Update",
			type: "success",
			onClick: function () {
				const id = store._array[store._array.length - 1].id;
				const gender = store._array[store._array.length - 1].gender;

				store
					.update(id, {
						gender: gender == "male" ? "female" : "male",
					})
					.done(() => {
						console.log("user is successfully updated");
						basicSelectBoxInstance.option("dataSource", store);
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
				store.remove(store._array.length).done(() => {
					console.log("successfully removed last user");
					basicSelectBoxInstance.option("dataSource", store);
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
				// console.log(basicSelectBoxInstance.option("dataSource"));

				console.log(store);
				console.log(store._array);
			},
		})
		.dxButton("instance");

	store.push([
		{
			type: "insert",
			data: {
				avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1017.jpg",
				userName: `Test User ${store._array.length + 1}`,
				gender: "male",
				id: store._array.length + 1,
			},
			index: store._array.length,
		},
	]);

	store.byKey(1).done((dataItem) => {
		console.log(dataItem);
	});

	// Button for clear the dataStore
	var btnStore = $("#btnClear")
		.dxButton({
			text: "Clear Store",
			type: "danger",
			onClick: function () {
				store.clear();
				basicSelectBoxInstance.option("dataSource", store);
			},
		})
		.dxButton("instance");

	console.log("Key Property is",store.key());
	console.log("Key of",store.keyOf({
		avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1017.jpg",
		userName: "Charlie Welch",
		gender: "female",
		id: "4",
	}));


	store.totalCount().done((count)=>{
		console.log("total count are",count);
	})
});
