$(function() {
    $("#btnRecordPaging").dxButton({
        text: "Record Paging",
        onClick: (e)=>{
            window.location = "./recordPaging.html";
        }
    })
    $("#btnVirtualScrolling").dxButton({
        text: "Virtual Scrolling",
        onClick: (e)=>{
            window.location = "./virtualScrolling.html";
        }
    })
    $("#btnInfiniteScrolling").dxButton({
        text: "Infinite Scrolling",
        onClick: (e)=>{
            window.location = "./infiniteScrolling.html";
        }
    })
});