$(async function () {
	var dataStore = new DevExpress.data.DataSource({
		store: new DevExpress.data.ArrayStore({
			key: "id",
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
		}),

		// map: (data) => {
		// 	return { Name: `${data.id} ${data.userName} ${data.gender}` };
		// },
		onChanged: function () {
			console.log("onChanged");
		},

		onLoadingChanged: function () {
			console.log("isLoading", dataStore.isLoading());
			console.log("onLoadingChanged - data loading status is changed");
			console.log("isLoaded", dataStore.isLoaded());
		},

		// filter: ["id", ">", 2],
		// group: { selector: "gender", desc: true },
		// filter: ["gender", "=", "female"],
		pageSize: 10,
		paginate: true,
		requireTotalCount: true,
		reshapeOnPush: true,
		searchExpr: ["userName", "gender"],
		select: ["userName", "gender", "id"],
		sort: "userName",
		postProcess: function (data) {
			console.log("postProcess :", data);
			return data;
		},

		onLoadError: function (error) {
			console.log(error.message);
		},
	});

	var basicSelectBoxInstance = $("#basicSelectBox")
		.dxSelectBox({
			dataSource: dataStore,
			placeholder: "Select a value",
			displayExpr: "userName",
			valueExpr: "id",
			grouped: false,
			deferRendering: true,
			searchEnabled: true,
			onInput: function (e) {
				var value = e.component.option("text");
				// e.component.option("text",value);
				dataStore.searchExpr(["userName", "gender"]);
				dataStore.searchOperation("contains");
				dataStore.searchValue(value);
				refreshDataSource();
			},
		})
		.dxSelectBox("instance");

	function refreshDataSource() {
		dataStore.load().done(function (data) {
			basicSelectBoxInstance.option("dataSource", data);
		});
	}

	// Button for grouping the data
	var BtnGroup = $("#btnGroup")
		.dxButton({
			text: "Group",
			type: "default",
			onClick: function (e) {
				dataStore.group({ selector: "gender", desc: true });
				dataStore.sort({ selector: "userName", desc: false });
				dataStore.load().done(function () {
					basicSelectBoxInstance.option({
						grouped: true,
						dataSource: dataStore.items(),
					});
				});
			},
		})
		.dxButton("instance");

	var BtnFilter = $("#btnFilter")
		.dxButton({
			text: "Filter",
			type: "default",
			onClick: function (e) {
				dataStore.filter(["id", "<", 5]);
				dataStore.sort({ selector: "userName", desc: false });
				dataStore.load().done(function () {
					basicSelectBoxInstance.option({
						dataSource: dataStore.items(),
					});

					console.log(dataStore);
				});

				console.log(dataStore.isLastPage());
			},
		})
		.dxButton("instance");
});
