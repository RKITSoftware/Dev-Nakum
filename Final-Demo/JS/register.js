$(function () {
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
    
    // validate the email
    $("#email").blur(function () {
        const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        if (!emailPattern.test($(this).val())) {
            $("#emailError").text("*Enter valid email.");
            $("#email").focus();
        }
        else {
            $("#emailError").text("");
        }
    });
  
    // validate the phone number
    $("#phone").blur(function () {
        if ($(this).val().length != 10) {
            $("#phoneError").text("*Length of phone number must be 10 digits.");
            $("#phone").focus();
        }
        else {
            $("#phoneError").text("");
        }
    });
  
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


    // register - function -- API CALL to store the userdata
    const register = (data)=>{
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
    // submit the form
    $('.register').submit(function(e){
        e.preventDefault();
        let name = $('#username').val();
        let email = $('#email').val();
        let phone = $('#phone').val();
        let pass = $('#password').val();
                        
        if(name==='' || email==='' || phone==='' || pass===''){
            alert("All fields are required");
            e.preventDefault();
        }
  
        const data = {
            "email": email,
            "password": pass,
            "username": name,
            "phone": phone,
        }

        const registerUser = async (data)=>{
            await register(data);
            alert("Data Added Successfully")
        }

        registerUser(data);
    })
});
