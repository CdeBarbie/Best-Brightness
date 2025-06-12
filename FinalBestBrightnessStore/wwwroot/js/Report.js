var dataTable;

$(document).ready(function () {
    loadDataTable();

    $('#btnGeneratePDF').click(function () {
        generatePDF();
    });
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Reports/GetSalesData', dataSrc: '' },
        "columns": [
            { data: 'salePersonId', "width": "15%" },
            { data: 'salesPersonName', "width": "20%" },
            { data: 'numberOfProductsSold', "width": "15%" },
            { data: 'dateOfOrder', "width": "20%" },
            { data: 'totalAmountMade', "width": "15%" }
        ]
    });
}

function generatePDF() {
    const { jsPDF } = window.jspdf;
    var doc = new jsPDF();

    var userData = dataTable.rows({ search: 'applied' }).data().toArray();

    var col = ["Salesperson ID", "Name", "Number of Products Sold", "Date", "Total Amount Made"];
    var rows = [];

    for (var i = 0; i < userData.length; i++) {
        var user = userData[i];
        var temp = [
            user.salePersonId,
            user.salesPersonName,
            user.numberOfProductsSold,
            user.dateOfOrder,
            user.totalAmountMade
        ];
        rows.push(temp);
    }

    doc.autoTable(col, rows);

    doc.save('SalesReport.pdf');
}
