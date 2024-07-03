$(function () {
	var userData = [
		{
			avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1017.jpg",
			userName: "Laura Bogisich",
			gender: "female",
			id: 1,
		},
		{
			avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/509.jpg",
			userName: "Karl Kling",
			gender: "male",
			id: 2,
		},
		{
			avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/581.jpg",
			userName: "Charlie Welch",
			gender: "female",
			id: 3,
		},
		{
			avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/904.jpg",
			userName: "Nicolas Friesen",
			gender: "male",
			id: 4,
		},
		{
			avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/93.jpg",
			userName: "Karen Parker",
			gender: "female",
			id: 5,
		},
		{
			avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/198.jpg",
			userName: "Neal Krajcik",
			gender: "female",
			id: 6,
		},
		{
			avatar: "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/273.jpg",
			userName: "Elsa Price",
			gender: "male",
			id: 7,
		},
	];

	const nums = [80, 85, 95, 79, 89];
	const step = (total, items) => {
		return total + items;
	};

	const finalize = (total) => {
		return total / 100;
	};

	DevExpress.data
		.query(nums)
		.aggregate(0, step, finalize)
		.done((result) => {
			console.log(result + "%");
		});

	DevExpress.data
		.query(nums)
		.aggregate(step)
		.done((result) => {
			console.log(result);
		});

	DevExpress.data
		.query(nums)
		.sum()
		.done((result) => {
			console.log(result);
		});

	DevExpress.data
		.query(userData)
		.sum("id")
		.done((result) => {
			console.log(result);
		});

	DevExpress.data
		.query(nums)
		.avg()
		.done((result) => {
			console.log(result);
		});

	DevExpress.data
		.query(userData)
		.count()
		.done((result) => {
			console.log(result);
		});

	var filterData = DevExpress.data
		.query(userData)
		.filter(["id", ">", 2])
		.toArray();
	console.log(filterData);

	var filterData2 = DevExpress.data
		.query(nums)
		.filter((item) => {
			return item > 80;
		})
		.toArray();
	console.log(filterData2);

	var groupData = DevExpress.data.query(userData).select(["gender","id"]).groupBy("gender").toArray();
	console.log(groupData);

	DevExpress.data
		.query(userData)
		.select("userName", "id")
		.max("id")
		.done(function (result) {
			console.log(`${result}`);
		});

	DevExpress.data
		.query(nums)
		.min()
		.done(function (result) {
			console.log(`${result}`);
		});

	var subset = DevExpress.data.query(nums).slice(1, 2).toArray();
	console.log(subset);

	var sortedData = DevExpress.data
		.query(userData)
		.sortBy("userName")
		
		.toArray("id");

	console.log(sortedData);
});
