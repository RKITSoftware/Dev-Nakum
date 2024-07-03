$(function () {
    $("#gridContainer").dxDataGrid({
        dataSource: customers,
        keyExpr: "Id",
        columns: ['CompanyName', 'City', 'State', 'Phone', 'Fax'],
        showBorders: true
    })
});
