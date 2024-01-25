import {getDataProject,getUsername} from './ajax.js'; 

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

    // getProjectData - function -- call the fetch username and after that push into local storage
    const getProjectData = async () => {

        showLoading();

        const projectData = await getDataProject();
        const prId = projectData.id;
        const userIdArray = projectData["userid[]"];
        fetchUsernames(userIdArray);
        

        $('.taskAssignForm').submit(function (e) {
            e.preventDefault();
    
            const name = $('#username').val();
            const task = $('#task').val();
    
            let existingData = JSON.parse(localStorage.getItem("formData")) || [];
    
            if (Array.isArray(existingData)) {
                existingData.push({
                    name: name,
                    prId: prId,
                    task: task,
                });
            } else {
                existingData = [{
                    name: name,
                    prId: prId,
                    task: task,
                }];
            }
    
            const formDataJSON = JSON.stringify(existingData);
            localStorage.setItem("formData", formDataJSON);
    
            alert("Form submitted and data stored in local storage!");
        });
    };

    getProjectData();
})