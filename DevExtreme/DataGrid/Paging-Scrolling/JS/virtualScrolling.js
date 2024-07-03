import {getData} from "./data.js"

$(async function() {
    $("#virtualScrolling").dxDataGrid({
        dataSource: await getData(),
        showBorders: true,
        keyExpr: "ID",
        columns: ["ID","Name", "Email", "gender","Country", "City"],
        scrolling: {
            mode: "virtual"
        }
    })
});