import {addProjectDataAPI} from './ajax.js'; 

$(function(){
    // validate the project title
    $('#prtitle').blur(function(){
        let name = $('#prtitle').val();

        // validate the name for blank sapce
        if(name.trim()===''){
            $('#prTitleError').text('*Project title is required.');
            $('#prtitle').focus(); 
        }
        else{
            $('#prTitleError').text('');
        }
    })

    // validate the project title
    $('#prdesc').blur(function(){
        let name = $('#prdesc').val();

        // validate the name for blank sapce
        if(name.trim()===''){
            $('#prDescError').text('*Project Description is required.');
            $('#prdesc').focus(); 
        }
        else{
            $('#prDescError').text('');
        }
    })

    // call the function to add the data
    const addProjectData = async (data)=>{

        // add the data
        const userdata = await addProjectDataAPI(data);
        alert("Data Added Successfully")
    }

    // submit the form
    $('.addProject').submit(function(e){
        e.preventDefault();

        // get the value from input 
        let title = $('#prtitle').val();
        let desc = $('#prdesc').val();
                        
        if(title==='' || desc===''){
            alert("All fields are required");
            e.preventDefault();
        }
        
        // create the object of data
        const data = {
            "pname": title,
            "pdescription": desc,
        }
        
        // send the data to the api
        const addProject = async (data)=>{
            await addProjectData(data);
            
        }

        addProject(data);
    })
})