$(function(){
    const loadingContainer = document.querySelector('.loading-container');

    // showLoading - function - show loading icon
    function showLoading() {
        loadingContainer.style.display = 'block';
    }

    // hideLoading - function - hide loading icon
    function hideLoading() {
        loadingContainer.style.display = 'none';
    }


    // getData - function -- API CALL to get the project data based on project id from url
    const getData = async ()=>{ 
        var urlSearchParams = new URLSearchParams(window.location.search);
        var prId = urlSearchParams.get('id');

        return $.ajax({
            url : `https://retoolapi.dev/5Jk3mO/project/${prId}`,
            method : "get",
            success : function(result){
                console.log("Data is successfully get projects");
                return result;
            },
            error: function(err){
                console.log("Something went wrong");
            }
        })
    }

    // getUsername - function -- API CALL to get the username 
    const getUsername = async (id)=>{
        return $.ajax({
            url : `https://retoolapi.dev/uXBmwj/data/${id}`,
            method: "get",
            success : function(result){
                return result;
            },
            error: function(error){
                console.log("Something went wrong");
            }
        });
    }

    // fetchUsernames - function -- fetch the username from userid which is stored in userIdArray and display it 
    const fetchUsernames = async (userIdArray) => {
        let usernameString = "";
        for (const userId of userIdArray) {
            const userData = await getUsername(userId);
            const username = userData.username;
            console.log(username);

            usernameString += `
                <option value="${username}">${username}</option>
            `;
        }
        
        // display the username string into frontend
        document.getElementById("username").innerHTML = usernameString;     

        hideLoading();
    };
    

    // const getProjectData = async ()=>{
    //     const projectData = await getData(); 
    //     const prId = projectData.id;   
    //     const userIdArray = projectData["userid[]"];
    //     fetchUsernames(userIdArray);

    //     $('.taskAssignForm').submit(function(e){
    //         e.preventDefault();
            
    //         const name = $('#username').val();
    //         const task = $('#task').val();
            
    //         let existingData = JSON.parse(localStorage.getItem("formData")) || [];
    //         console.log(existingData);
            
    //         // if(existingData){
    //         //     existingData.push({
    //         //         name : name,
    //         //         prId : prId,
    //         //         task : task,
    //         //     })
    //         // } 
    //         // else{
    //         //     existingData = {
    //         //         name : name,
    //         //         prId : prId,
    //         //         task : task,
    //         //     }
    //         // }

    //         existingData.push([{
    //             name : name,
    //             prId : prId,
    //             task : task,
    //         }])
            
    //         // const formData = {
    //         //     name : name,
    //         //     prId : prId,
    //         //     task : task,
    //         // };

    //         // Convert the form data object to a JSON string
    //         const formDataJSON = JSON.stringify(existingData);

    //         localStorage.setItem("formData", formDataJSON);
    //         alert("Form submitted and data stored in local storage!");
    //     })
    // }


    // getProjectData - function -- call the fetch username and after that push into local storage
    const getProjectData = async () => {

        showLoading();

        const projectData = await getData();
        const prId = projectData.id;
        const userIdArray = projectData["userid[]"];
        fetchUsernames(userIdArray);
        

        $('.taskAssignForm').submit(function (e) {
            e.preventDefault();
    
            const name = $('#username').val();
            const task = $('#task').val();
    
            let existingData = JSON.parse(localStorage.getItem("formData")) || [];
    
            if (Array.isArray(existingData)) {
                existingData.push({
                    name: name,
                    prId: prId,
                    task: task,
                });
            } else {
                existingData = [{
                    name: name,
                    prId: prId,
                    task: task,
                }];
            }
    
            const formDataJSON = JSON.stringify(existingData);
            localStorage.setItem("formData", formDataJSON);
    
            alert("Form submitted and data stored in local storage!");
        });
    };


    // const storedData = localStorage.getItem("formData");
    // console.log(storedData);
    // // Parse the JSON string into an array
    // const data = JSON.parse(storedData);
    // const filteredData = data.filter(item => item.prId !== 1);

    // console.log(filteredData);
    // localStorage.setItem("formData",JSON.stringify(filteredData));
    getProjectData();
})