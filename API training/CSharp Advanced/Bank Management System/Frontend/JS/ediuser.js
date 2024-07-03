import { getCurrentUser, UpdateUserName, deleteUser } from "./ajax.js";
$(function() {
  const editUser = document.getElementById("editName");
  const btnDeleteUser = document.getElementById("btnDelete");

  editUser.addEventListener("click", async () => {
    const name = document.getElementById("nameChange");
    console.log(name.value);

    const UpdateData = await UpdateUserName(name.value);
    getUserInfo();
    closeModel();
  });

  const closeModel = () => {
    $("#btnClose").click();

    let alert = `<div class="alert alert-success d-flex align-items-center" role="alert">
                <div>
                    username has been successfully changed
                </div>
            </div>`;

    $("#showAlert").html(alert);        
    setTimeout(() => {$("#showAlert").html('')}, 2000);
  };

  const getUserInfo = async () => {
    const user = await getCurrentUser();
    // console.log(user);

    document.getElementById("userName").innerHTML = user.E01F02;
    document.getElementById("userEmail").innerHTML = user.E01F04;
    document.getElementById("userMoney").innerHTML = user.E01F05;
  };

  btnDeleteUser.addEventListener("click",async ()=>{
    await deleteUser();
    logoutFunction();
  })

  getUserInfo();
});
