import {getData,assignUser} from './ajax.js'; 

$(function(){

    const loadingContainer = document.querySelector('.loading-container');

    // showLoading - function - show loading icon
    function showLoading() {
        loadingContainer.style.display = 'block';
    }

    // hideLoading - function - hide loading icon
    function hideLoading() {
        loadingContainer.style.display = 'none';
    }


    // getUserData - function -- get the user data and display it
    const getUserData = async ()=>{
        showLoading();

        const userData = await getData();

        let userString = "";
        let ctr=1;
        $.map(userData,function(data){
            if(data.role !== "TRUE" && data.username!=null && data.email!=null &&data.phone!=null){
                userString += `<tr>
                    <th>${ctr++}</th>
                    <td>${data.username}</td>
                    <td>${data.email}</td>
                    <td>${data.phone}</td>
                    <td><button class="btn btn-primary p-1 assignUser" id="btn${data.id}" type="submit" data-id="${data.id}" >Assign</button></td>
                </tr>`;
            }
        })
        document.getElementById("userTableData").innerHTML = userString;
        hideLoading();
        
        $('.assignUser').on('click', async function() {
            // Access the value from the click event
            let userId = $(this).data('id');
            console.log('Clicked Image ID:', $(this).data('id'));
    
            await assignUser(userId); 
            alert("User is assigned successfully")
        });
    }

    getUserData();
})