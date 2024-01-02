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

    const addProjectDataAPI = async (addData)=>{
        let userids =  ["1",""];
        let data = {
            pname : addData.pname,
            pdescription : addData.pdescription,
            userid: userids, 
            completed: "no",
            currrent_user: "1"
        };

        return $.ajax({
            url : `https://retoolapi.dev/5Jk3mO/project/`,
            method: "post",
            data: data,
            success : function(result){
                console.log("Successfully add the data");
                return result;
            },
            error: function(error){
                console.log("Enter the valid username or password");
            }
        });
    }

    const addProjectData = async (data)=>{

        // get data from the api
        const userdata = await addProjectDataAPI(data);
        alert("Data Added Successfully")
        
    }

    // submit the form
    $('.addProject').submit(function(e){
        e.preventDefault();
        let title = $('#prtitle').val();
        let desc = $('#prdesc').val();
                        
        if(title==='' || desc===''){
            alert("All fields are required");
            e.preventDefault();
        }
        
        const data = {
            "pname": title,
            "pdescription": desc,
        }

        const addProject = async (data)=>{
            await addProjectData(data);
            
        }

        addProject(data);
    })
})