import { getAllusersData } from "./ajax.js";

$(function() {

  const getAllusers = async () => {
    const userList = await getAllusersData();
    console.log(userList);
    let users = ``;
    userList.map(user => {
      users += `<tr>
            <th scope="row">${user.E01F01}</th>
            <td>${user.E01F02}</td>
            <td>${user.E01F04}</td>
            <td>${user.E01F05}</td>
            <td>${user.E01F06}</td>
        </tr>`;
    });

    document.getElementById("tableUsers").innerHTML = users
  };
  getAllusers();
});
