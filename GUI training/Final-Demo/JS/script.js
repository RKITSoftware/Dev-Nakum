import {getDataAllProject,updateCompleted} from './ajax.js'; 

// document ready event
$(function(){

    // logout - function -- session destroy
    $('#logout').click(function(){
        console.log("clicked");
        sessionStorage.removeItem("userData");
        window.location.href = "/Final-Demo/index.html";        // redirect into register page
    })

    // capitalizeFirstLetter - function -- convert the string into capitalize latter
    function capitalizeFirstLetter(str) {
        if(str==null)
            return;
        return str.charAt(0).toUpperCase() + str.slice(1);
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

    const noIcon = (data)=>{
        const str = capitalizeFirstLetter(data.completed) === "No" ? 
        `<img class="yesCompleted" data-id="${data.id}" id="yesCompleted${data.id}" src="./images/yes.jpg" width="25px" alt="" />` : ""; 
        const str2 = `</li>
        <li class="list-group-item before-yes"></li>
    </ul>
    <a href="./assignUser.html?id=${data.id}" class="text-decoration-none">
        <button type="button" class="btn btn-primary ms-3 mb-2">
        Assign User
        </button>
    </a>
    
    <a href="./assignTask.html?id=${data.id}">
        <button type="button" class="btn btn-primary ms-3 mb-2">
        Assign Task
        </button>
    </a>`
        return str + str2;
    }

    // if logged user is admin - assign the user and assign task 
    

    // displayProjects - function -- display the project data into card
    const displayProjects = async (projectData)=>{
        let cardString = ""; 
        
        
    
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
                                    ${isAdmin()=="TRUE" ? noIcon(data) :""}
                    </div>

                </div>
            </div>`
        })    
       
        
        document.getElementById("card-data").innerHTML = cardString;
        
        // when click on right icon in card
        $('.yesCompleted').on('click', async function() {
            // Access the value from the click event
            let projectId = $(this).data('id');

            await updateCompleted(projectId);
            
            // after completed diplay the projectdata
            getProjectData();
            
        });
    }

    // getProjectData - function -- get project data and send to the diplay project
    const getProjectData = async () => {
        try {
            const projectData = await getDataAllProject();
            displayProjects(projectData);
        } catch (error) {
            console.log("Something went wrong:", error);
        }
    };
    
    getProjectData();

    $("#searchForm").on("submit", function(event) {
        // Prevent the default form submission behavior
        event.preventDefault();
    });

    $("#search").on("keyup", async function(event) {
        // Check if Enter key is pressed (key code 13)
        if (event.keyCode === 13) {
            // Prevent the default form submission behavior
            event.preventDefault();
        }
    
        let search = $(this).val();
        try {
            const projectData = await getDataAllProject();
    
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