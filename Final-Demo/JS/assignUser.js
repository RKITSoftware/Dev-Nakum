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

    // getData - function --  API CALL to get the user data 
    const getData = async ()=>{
        return $.ajax({
            url : `https://retoolapi.dev/uXBmwj/data`,
            method: "get",
            success : function(result){
                console.log("Successfully get the data");
                return result;
            },
            error: function(error){
                console.log("Something went wrong");
            }
        });
    }

    // getUserData - function -- get the user data and display it
    const getUserData = async ()=>{
        showLoading();

        const userData = await getData();

        let userString = "";
        let ctr=1;
        $.map(userData,function(data){
            if(data.role !== "TRUE" && data.username!=null && data.email!=null &&data.phone!=null){
                userString += `<tr>
                    <th>${ctr++}</th>
                    <td>${data.username}</td>
                    <td>${data.email}</td>
                    <td>${data.phone}</td>
                    <td><button class="btn btn-primary p-1 assignUser" id="btn${data.id}" type="submit" data-id="${data.id}" >Assign</button></td>
                </tr>`;
            }
        })
        document.getElementById("userTableData").innerHTML = userString;
        hideLoading();
        
        $('.assignUser').on('click', async function() {
            // Access the value from the click event
            userId = $(this).data('id');
            console.log('Clicked Image ID:', $(this).data('id'));
    
            await assignUser(userId); 
            alert("User is assigned successfully")
        });
    }

    const projects = async (id)=>{
        return $.ajax({
            url : `https://retoolapi.dev/5Jk3mO/project/${id}`,
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

    // assignUser - function -- API CALL to assgin user  
    const assignUser = async(userId)=>{
        var urlSearchParams = new URLSearchParams(window.location.search);
        var taskId = urlSearchParams.get('id');

        let userIds = await projects(taskId); 
        
        console.log(userIds);

        if(userIds["userid[]"]){
            userIds["userid[]"].push(userId.toString());
        }
        else {
            userIds.userid.push(userId.toString())
        }

        

        console.log(userIds["userid[]"]);

        // update the current user 
        userIds.currrent_user = userIds["userid[]"] ? userIds["userid[]"].length : userIds.userid.length;

        return $.ajax({
            url : `https://retoolapi.dev/5Jk3mO/project/${taskId}`,
            method : "put",
            data : {
                "pname": userIds.pname,
                "userid": userIds["userid[]"] ? userIds["userid[]"] : userIds.userid,
                "completed": userIds.completed,
                "pdescription": userIds.pdescription,
                "currrent_user": userIds.currrent_user,
            },
            success : function(result){
                console.log("Data is successfully updated for projects");
                return result;
            },
            error: function(err){
                console.log("Something went wrong");
            }
        })
    }

    

    // Assuming you have an object with the updated data
    // var updatedData = {
    //     id: 19,
    //     pname: "AJAX",
    //     userid: ["1"], // Change the userId property to an object
    //     completed: "no",
    //     pdescription: "Asynchronous JavaScript and XML",
    //     currrent_user: "1"
    // };

    // // Assuming you have the API endpoint
    // var apiEndpoint = 'https://retoolapi.dev/5Jk3mO/project/19'; // Update with your actual endpoint

    // // Perform the PUT request to update the data
    // fetch(apiEndpoint, {
    //     method: 'PUT',
    //     headers: {
    //         'Content-Type': 'application/json',
    //         // Add any other headers as needed
    //     },
    //     body: JSON.stringify(updatedData),
    // })
    // .then(response => response.json())
    // .then(data => {
    //     console.log('Updated data:', data);
    // })
    // .catch(error => {
    //     console.error('Error updating data:', error);
    // });

    getUserData();
})