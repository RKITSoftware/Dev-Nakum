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


    // validate the input field
    if (nameInput.value.trim() === '') {
        setErrors(nameError,nameInput,'*Name is required.');
    }

    const emailPattern =  /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if(!emailPattern.test(emailInput.value)){
        setErrors(emailError,emailInput,'*Invalid email address');
    }
    
    if(phoneInput.value.length!=10){
        setErrors(phoneError,phoneInput,"*Phone number must be 10 digits")
    }

    const passwordPatttern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/;
    if(passwordInput.value.length<8){
        setErrors(passwordError,passwordInput,"*Password should be 8 digits long")
    }
    else if(!passwordPatttern.test(passwordInput.value)){
        setErrors(passwordError,passwordInput,'*Password must be strong')
    }

    if(passwordInput.value !== cpasswwordInput.value){
        setErrors(cpasswwordError,cpasswwordInput,"*Confirm password must be same with password")
    }

    else{
        window.alert("form submited successfully");
    }
})