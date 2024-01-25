import {getUserData} from './ajax.js'; 

$(function(){
    // validate the username
    $('#username').blur(function(){
        let name = $('#username').val();

        // validate the name for blank sapce
        if(name.trim()===''){
            $('#nameError').text('*Name is required.');
            $('#username').focus(); 
        }
        else{
            $('#nameError').text('');
        }
    })

    // validate the password
    $("#password").blur(function () {
        const passwordPatttern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/;
        
        // check the lenght of the password
        if ($(this).val().length < 8) {
            $("#passError").text("*Password should be 8 digits long");
            $("#password").focus();
        }
        
        // match with pattern
        else if (!passwordPatttern.test($(this).val())) {
            $("#passError").text("*Password must be strong");
            $("#password").focus();
        } else {
            $("#passError").text("");
        }
    });

    const login = async (data)=>{

        // get data from the api
        const userdata = await getUserData(data);

        if(data.password === userdata[0].password){
            const dataTOStoreSession = {
                "username" : userdata[0].username,
                "email" : userdata[0].email,
                "phone" : userdata[0].phone,
                "role" : userdata[0].role,
            }

            // convert user data object to a JSON string
            const userDataJSON = JSON.stringify(dataTOStoreSession);
            
            // // Store the user data in the session storage
            sessionStorage.setItem("userData", userDataJSON);

            if(userdata[0].role === "FALSE"){
                window.location.href = "/Final-Demo/projects.html";
            }
            else{
                window.location.href = "/Final-Demo/dashboard.html";
            }

        }
        else{
            alert("Enter the valid username or password");
        }
    }

    // submit the form
    $('.login').submit(function(e){
        e.preventDefault();
        let name = $('#username').val();
        let pass = $('#password').val();
                        
        if(name==='' || pass===''){
            alert("All fields are required");
            e.preventDefault();
        }
        
        console.log(name);
        const data = {
            "password": pass,
            "username": name,
        }

        const loginUser = async (data)=>{
            await login(data);
            //alert("Data Added Successfully")
        }

        loginUser(data);
    })
})