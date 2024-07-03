export const getData = ()=>{
	return $.ajax({
		type: "get",
		url: "https://6673ada075872d0e0a933549.mockapi.io/api/employee",
		success: function (response) {
			return response;   
		}
	});
}