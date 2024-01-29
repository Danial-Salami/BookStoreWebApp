var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    } else if (url.includes("pending")) {
        loadDataTable("pending");
    } else if (url.includes("completed")) {
        loadDataTable("completed");
    } else if (url.includes("approved")) {
        loadDataTable("approved");
    } else {
        loadDataTable();
           }
    
});
function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { "url": '/admin/order/getall?status=' + status },
        "columns": [
            { data: 'id',"width": "15%" },
            { data: 'name', "width": "15%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'applicationUser.email', "width": "15%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group " role="group"><a href="/Admin/Order/Details?orderId=${data}" class="btn btn-info mx-2 p-1" ><i class="bi bi-pencil-square"></i> Details</a ></div>`;
                },
                "width": "15%"
            },

        ],
        "initComplete": function (settings, json) {
            console.log("DataTables has been initialized with the following data:", json);
        },
        "error": function (xhr, error, thrown) {
            console.error("Error loading DataTable:", error);
        }
    });               
}
