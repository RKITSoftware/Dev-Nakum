import {project} from './ajax.js'; 

$(function() {

  $('#logout').click(function(){
    console.log("clicked");
    sessionStorage.removeItem("userData");
    window.location.href = "/Final-Demo/index.html";        // redirect into register page
  })

  // getUserName - function -- get the username from the session storage
  const getUserName = () => {
    const userdata = sessionStorage.getItem("userData");
    const storedUserData = JSON.parse(userdata);
    const username = storedUserData.username;

    return username;
  };
  console.log("username " + getUserName());
 

  // display the username into frontend
  document.getElementById("user").innerHTML = `Welcome, ${getUserName()}`;

  // get the all project which is assigned to that user
  const getProjectOfUser = async () => {
    //get the data from session storage
    const storedUserDataJSON = sessionStorage.getItem("userData");

    // Convert the JSON string back to an object
    const storedUserData = JSON.parse(storedUserDataJSON);
    const username = storedUserData.username;

    // Retrieve data from local storage
    const storedData = localStorage.getItem("formData");
    console.log(storedData);
    // Parse the JSON string into an array
    const data = JSON.parse(storedData);

    const filteredObjects = data.filter(item => item.name === username);

    let cardString = "";
    for (const it of filteredObjects) {
      // Call the project function and await the result
      const result = await project(it.prId);

      cardString += `
                <div class="col-12 col-lg-3 col-xxl-3">
                    <div class="card-box rounded-2 p-1 shadow">
                    <div class="card-body">
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">
                                <h5 class="card-title">${result.pname.toUpperCase()}</h5>
                            </li>
                            <li class="list-group-item">
                                <p class="card-text">
                                ${result.pdescription}
                                </p>
                            </li>

                            <li class="list-group-item">Current User : ${result.currrent_user}</li>
                            <li class="list-group-item">Task : ${it.task}</li>
                        </ul>
                    </div>
                    </div>
                </div>
            `;
    }

    // console.log(cardString);
    document.getElementById("card-data").innerHTML =
      cardString != "" ? cardString : "<h3>Project is not assigned yet</h3>";
  };

  // call the function to the get the assigned project to the loged user
  const getAssignedProjectData = async () => {
    const data = await getProjectOfUser();
  };
  getAssignedProjectData();
});
