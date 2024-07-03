import {getData} from "./data.js"
$(async function() {
    $("#recordPaging").dxDataGrid({
        dataSource: await getData(),
        showBorders: true,
        keyExpr: "ID",
        columns: ["ID","Name", "Email", "gender","Country", "City"],
        paging: {
            pageSize: 10
        },
        customizeColumns: (columns)=>{
            columns[0].width = "auto"
            columns[1].width = "auto"
            columns[3].width = "auto"
        },
        pager: {
            visible: true,
            allowedPageSizes:[5,10,15,20] ,
            displayMode : "full",
            showInfo: true,
            showNavigationButtons: true,
            showPageSizeSelector: true,
        },
    })
});