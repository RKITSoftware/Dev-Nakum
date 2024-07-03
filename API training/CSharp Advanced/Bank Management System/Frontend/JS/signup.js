import { signUp } from "./ajax.js";

$(function() {
  const form = document.getElementById("signup");


  form.addEventListener("submit", async e => {
    const fname = document.getElementById("name").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    console.log([fname, email, password]);

    e.preventDefault();
    const data = JSON.stringify({
      E01F02: fname,
      E01F03: password,
      E01F04: email
    });

    const res = await signUp(data);
  });
});
