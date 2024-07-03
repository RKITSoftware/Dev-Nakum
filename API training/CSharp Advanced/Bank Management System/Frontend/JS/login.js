import { login } from "./ajax.js";

$(function() {
  const form = document.getElementById("login");
  form.addEventListener("submit", async e => {
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    console.log([email, password]);

    e.preventDefault();
    const data = JSON.stringify({
      E01F03: password,
      E01F04: email
    });

    const user = await login(data);

    const token = user.jwt;
    const role = user.role;

    document.cookie = `token=${token}`;
    document.cookie = `role=${role}`;
    // console.log(document.cookie);
    window.location.href = "./dashboard.html";
  });
});
