$(function() {
    $("#btnGridContainer").dxButton({
        text: "Simple Array",
        onClick: (e)=>{
            window.location = "./simpleArray.html";
        }
    })
    $("#btnGridContainerAJAX").dxButton({
        text: "AJAX Request",
        onClick: (e)=>{
            window.location = "./ajaxRequest.html";
        }
    })
});