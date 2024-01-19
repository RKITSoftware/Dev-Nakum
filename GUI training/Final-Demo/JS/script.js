// document ready event
$(function(){
    
    // getUserName - function -- get the username from the session storage 
    const getUserName = () => {
        const userdata = sessionStorage.getItem("userData");
        const storedUserData = JSON.parse(userdata);
        const username = storedUserData.username;

        return username;
    }
    
    // display the username into frontend
    document.getElementById("user").innerHTML = `Welcome, ${getUserName()}`
    $("#user").text('')
    // logout - function -- session destroy
    $('#logout').click(function(){
        sessionStorage.removeItem("userData");
        window.location.href = "/Final-Demo/index.html";        // redirect into register page
    })

    // capitalizeFirstLetter - function -- convert the string into capitalize latter
    function capitalizeFirstLetter(str) {
        if(str==null)
            return;
        return str.charAt(0).toUpperCase() + str.slice(1);
    }

    // getData - function -- API CALL - get all the project data
    const getData = ()=>{
        return $.ajax({
            url : "https://retoolapi.dev/5Jk3mO/project",
            method : "get",
            success : function(result){
                console.log("Data is successfully get");
                return result;
            },
            error: function(err){
                console.log("Something went wrong");
            }
        })
    }

    // isAdmin - function -- to check if login user is admin or not ?
    const isAdmin = ()=>{
        const storedUserDataJSON = sessionStorage.getItem("userData");
        const storedUserData = JSON.parse(storedUserDataJSON);      // Convert the JSON string back to an object
        return storedUserData.role;
    }

    // filterProjects - function -- filter the project based on search from navbar
    const filterProjects = (projects, searchTerm) => {
        return projects.filter(project => 
            (project.pname ?? '').toLowerCase().includes(searchTerm.toLowerCase()) || 
            (project.pdescription ?? '').toLowerCase().includes(searchTerm.toLowerCase())
        );
    };

    // displayProjects - function -- display the project data into card
    const displayProjects = async (projectData)=>{
        let cardString = ""; 
        
        // display for admin
        if(isAdmin()=="TRUE"){
            $.map(projectData,function(data){
                
                cardString += `
                <div class="col-12 col-lg-3 col-xxl-3">
                    <div class="card-box rounded-2 p-1 shadow">
                        
                        <div class="card-body">
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item">
                                <h5 class="card-title">${(data.pname).toUpperCase()}</h5>
                                </li>
                                <li class="list-group-item">
                                <p class="card-text">
                                    ${data.pdescription}
                                </p>
                                </li>

                                <li class="list-group-item">Current User : ${data.currrent_user}</li>
                                <li class="list-group-item yes-no-img" id="completedImageContainer${data.id}">
                                    Completed : ${capitalizeFirstLetter(data.completed)}
                                     ${capitalizeFirstLetter(data.completed) === "No" ? 
                                    `<img class="yesCompleted" data-id="${data.id}" id="yesCompleted${data.id}" src="./images/yes.jpg" width="25px" alt="" />` : ""}
                                </li>
                                <li class="list-group-item before-yes"></li>
                            </ul>
                            <a href="./assignUser.html?id=${data.id}">
                                <button type="button" class="btn btn-primary ms-3 mb-2">
                                Assign User
                                </button>
                            </a>
                            
                            <a href="./assignTask.html?id=${data.id}">
                                <button type="button" class="btn btn-primary ms-3 mb-2">
                                Assign Task
                                </button>
                            </a>
                        </div>

                    </div>
                </div>`
            })    
        }
        else{                   // display for user
            $.map(projectData,function(data){
                cardString += `
                <div class="col-12 col-lg-3 col-xxl-3">
                    <div class="card-box rounded-2 p-1 shadow">
                        <div class="card-body">
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">
                            <h5 class="card-title">${(data.pname).toUpperCase()}</h5>
                            </li>
                            <li class="list-group-item">
                            <p class="card-text">
                                ${data.pdescription}
                            </p>
                            </li>
    
                            <li class="list-group-item">Current User : ${data.currrent_user}</li>
                            <li class="list-group-item">Completed : ${capitalizeFirstLetter(data.completed)}</li>
                            <li class="list-group-item"></li>
                        </ul>
                        </div>
                    </div>
                </div>`
            })
        }
        
        document.getElementById("card-data").innerHTML = cardString;
        
        // updateCompleted - funciton -- API CALL - updated to the project completed or not
        const updateCompleted = (id)=>{
            return $.ajax({
                url : `https://retoolapi.dev/5Jk3mO/project/${id}`,
                method : "patch",
                data: {
                    completed : "Yes",
                },
                success: function(result){
                    console.log("successfully updated status"); 
                    return result;
                },
                error: function(err){
                    console.log("Something went wrong");
                }
            })
        }

        // when click on right icon in card
        $('.yesCompleted').on('click', async function() {
            // Access the value from the click event
            projectId = $(this).data('id');

            await updateCompleted(projectId);
            
            // after completed diplay the projectdata
            getProjectData();
            
        });
    }

    // getProjectData - function -- get project data and send to the diplay project
    const getProjectData = async () => {
        try {
            const projectData = await getData();
            displayProjects(projectData);
        } catch (error) {
            console.log("Something went wrong:", error);
        }
    };
    
    getProjectData();

    // something type in search input 
    $("#search").on("input", async function() {
        let search = $(this).val();
        try {
            const projectData = await getData();
    
            if (search.trim() === "") {
                // If the search input is empty, display all projects
                displayProjects(projectData);
            } else {
                // If there's a search term, filter projects based on the search term
                const filteredProjects = filterProjects(projectData, search);
                displayProjects(filteredProjects);
            }
        } catch (error) {
            console.log("Something went wrong:", error);
        }
    });
})