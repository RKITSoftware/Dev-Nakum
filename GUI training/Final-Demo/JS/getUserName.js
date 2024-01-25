$(function() {
  // getUserName - function -- get the username from the session storage
  const getUserName = () => {
    const userdata = sessionStorage.getItem("userData");
    const storedUserData = JSON.parse(userdata);
    const username = storedUserData.username;

    return username;
  };
  console.log("username " + getUserName());
  console.log(document.getElementById("user").innerHTML);
  // display the username into frontend
  document.getElementById("user").innerHTML = `Welcome, ${getUserName()}`;
});
