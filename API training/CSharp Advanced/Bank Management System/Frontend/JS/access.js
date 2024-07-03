const getCookies = () => {
  const cookie = document.cookie.split(";");

  let role, token;

  cookie.forEach(c => {
    if (c.trim().startsWith("role=")) {
      role = c.split("=")[1];
    }
    if (c.trim().startsWith("token=")) {
      token = c.split("=")[1];
    }
  });
  return {
    token,
    role
  };
};

const getAccess = ()=>{
  const role = getCookies().role;
  
  if (role == "User") {
    window.location.href = "./dashboard.html";
  }
}

const hideUser = ()=>{
    const role = getCookies().role; // Assuming getCookies() returns the correct role
  
    if (role !== 'User') {
      const usersLink = document.createElement('li');
      usersLink.classList.add('nav-item'); // Ensure correct CSS class
      usersLink.innerHTML = '<a class="nav-link" aria-current="page" href="./users.html">Users</a>';
  
      const usersLinkContainer = document.getElementById('usersLinkContainer');
      usersLinkContainer.after(usersLink); // Insert after profile link
    } 
    
}
