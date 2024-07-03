const logout = document.getElementById("logout");

const logoutFunction = ()=>{
  // console.log("inside");
  var cookies = document.cookie.split(";");
  for (var i = 0; i < cookies.length; i++) {
    var cookie = cookies[i].trim().split("=");
    var name = cookie[0];
    var value = cookie[1];

    // Expire the cookie immediately
    document.cookie = name + `=; expires=${new Date(Date.now())}; path=/`;
    window.location.href = "./index.html"
  }
}
logout.addEventListener("click",()=>{
  console.log(logout);
  logoutFunction();
})