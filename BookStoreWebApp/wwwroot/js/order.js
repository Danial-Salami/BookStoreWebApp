var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { "url" :'/admin/product/getall' },
        "columns": [
            { data: 'id',"width": "10%" },
            { data: 'name', "width": "10%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'applicationuser.email', "width": "15%" },
            { data: 'orderstatus', "width": "10%" },
            { data: 'ordertotal', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group " role="group"><a href="/admin/product/upsert?id=${data}" class="btn btn-info mx-2 p-1" ><i class="bi bi-pencil-square"></i> Edit</a ></div>`;
                },
                "width": "25%"
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
