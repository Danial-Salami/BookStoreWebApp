var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { "url" :'/admin/company/getall' },
        "columns": [
            { data: 'name',"width": "10%" },
            { data: 'state', "width": "10%" },
            { data: 'city', "width": "10%" },
            { data: 'streetAddress', "width": "10%" },
            { data: 'postalCode', "width": "10%" },
            { data: 'phoneNumber', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group " role="group"><a href="/admin/company/upsert?id=${data}" class="btn btn-info mx-2 p-1" ><i class="bi bi-pencil-square"></i> Edit</a ><a onClick=Delete('/admin/company/delete/${data}') class="btn btn-danger mx-2 p-1"><i class="bi bi-trash-fill"></i> Delete</a></div>`;
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
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload()
                    toastr.success(data.message)
                }
            })
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });
}