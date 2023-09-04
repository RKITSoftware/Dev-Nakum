const form = document.getElementById("myForm");
const nameInput = document.getElementById('name');
const emailInput = document.getElementById('email');
const phoneInput = document.getElementById('phone');
const passwordInput = document.getElementById('pass');
const cpasswwordInput = document.getElementById('cpass');

const nameError = document.getElementById('nameError');
const emailError = document.getElementById('emailError');
const phoneError = document.getElementById('phoneError');
const passwordError = document.getElementById('passError');
const cpasswwordError = document.getElementById('cpassError');


// function - set the error at tha positon and focus at input fields
const setErrors = (id,input,error) => {
    id.innerHTML = error;
    input.focus();
    return;
}

// function - eventlistener - submit - validate the input
form.addEventListener('submit',(e) => {
    e.preventDefault();

    nameError.innerHTML = "";
    emailError.innerHTML = "";
    phoneError.innerHTML = "";
    passwordError.innerHTML = "";
    cpasswwordError.innerHTML = "";

    let flag = true;
    // validate the input field
    if (nameInput.value.trim() === '') {
        setErrors(nameError,nameInput,'*Name is required.');
        flag = false;
    }

    const emailPattern =  /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if(!emailPattern.test(emailInput.value)){
        setErrors(emailError,emailInput,'*Invalid email address');
        flag = false;
    }
    
    if(phoneInput.value.length!=10){
        setErrors(phoneError,phoneInput,"*Phone number must be 10 digits")
        flag = false;
    }

    const passwordPatttern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/;
    if(passwordInput.value.length<8){
        setErrors(passwordError,passwordInput,"*Password should be 8 digits long")
        flag = false;
    }
    else if(!passwordPatttern.test(passwordInput.value)){
        setErrors(passwordError,passwordInput,'*Password must be strong')
        flag = false;
    }

    if(passwordInput.value !== cpasswwordInput.value){
        setErrors(cpasswwordError,cpasswwordInput,"*Confirm password must be same with password")
        flag = false;
    }

    if(flag == true){
        window.alert("Data added successfully");
    }
})