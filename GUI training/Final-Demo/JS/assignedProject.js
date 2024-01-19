$(function(){
    const project = (id)=>{
        return $.ajax({
            url : `https://retoolapi.dev/5Jk3mO/project/${id}`,
            method : "get",
            success : function(result){
                // console.log("Data is successfully get");
                return result;
            },
            error: function(err){
                console.log("Something went wrong");
            }
        })
    }
    const getProjectOfUser = async ()=>{
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
                            <h5 class="card-title">${(result.pname).toUpperCase()}</h5>
                        </li>
                        <li class="list-group-item">
                            <p class="card-text">
                            ${result.pdescription}
                            </p>
                        </li>

                        <li class="list-group-item">Current User : ${result.currrent_user}</li>
                        <li class="list-group-item">Task : ${it.task}</li>
                        <!-- <li class="list-group-item">
                            Completed : No
                            <img src="./images/yes.jpg" width="25px" alt="" />
                            <img src="./images/no-10-16.png" width="25px" alt="" />
                        </li> -->
                        </ul>
                    </div>
                    </div>
                </div>
            `;
        }

        console.log(cardString);
        document.getElementById("assignProject").innerHTML = cardString != "" ? cardString : "<h3>Project is not assigned yet</h3>";

        
    }
    const getAssignedProjectData = async ()=>{
        const data = await getProjectOfUser(); 
    }
    getAssignedProjectData();
})