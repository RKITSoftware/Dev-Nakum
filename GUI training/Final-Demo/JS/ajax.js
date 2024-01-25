// add projects
// addProjectDataAPI - function - API CALL - add the project data into api
export const addProjectDataAPI = async addData => {
    let userids = ["1"]; //default user id

    // data which is send into the api
    let data = JSON.stringify({
        pname: addData.pname,
        pdescription: addData.pdescription,
        userid: userids,
        completed: "no",
        currrent_user: "1"
    });

    return $.ajax({
        url: `https://retoolapi.dev/5Jk3mO/project/`,
        method: "post",
        data: data,
        headers: {
            "Content-Type": "Application/json"
        },
        success: function(result) {
        console.log("Successfully add the data");
            return result;
        },
        error: function(error) {
            console.log("Something went wrong");
        }
    });
};


// assigned projects

// assign user also
//  get the project details based on project id
export const project = async (id) => {
    return $.ajax({
        url: `https://retoolapi.dev/5Jk3mO/project/${id}`,
        method: "get",
        success: function(result) {
            // console.log("Data is successfully get");
            return result;
        },
        error: function(err) {
            console.log("Something went wrong");
        }
    });
};

//assign task
// getData - function -- API CALL to get the project data based on project id from url
export const getDataProject = async ()=>{ 
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
export const getUsername = async (id)=>{
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

//assign user
// getData - function --  API CALL to get the user data 
export const getData = async ()=>{
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

 // assignUser - function -- API CALL to assgin user  
 export const assignUser = async(userId)=>{
    var urlSearchParams = new URLSearchParams(window.location.search);
    var taskId = urlSearchParams.get('id');

    let userIds = await project(taskId); 
    
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

// login
// getUserData - function -- API CALL to get the userdata from the username
export const getUserData = async (data)=>{
    return $.ajax({
        url : `https://retoolapi.dev/uXBmwj/data?username=${data.username}`,
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

//register
// register - function -- API CALL to store the userdata
export const register = (data)=>{
    return $.ajax({
        url:"https://retoolapi.dev/uXBmwj/data",
        method:"post",
        data:{
            "email": data.email,
            "password": data.password,
            "username": data.username,
            "phone": data.phone,
            "role": "FALSE",
        },
        success:function(result){
            console.log("Data added successfully");
            window.location.href = "./login.html"
        },
        error:function(error){
            console.log("Sometthing went wrong");
            console.log(error);
        }
    })
}


// getDataAllProject - function -- API CALL - get all the project data
export const getDataAllProject = ()=>{
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

export const updateCompleted = (id)=>{
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