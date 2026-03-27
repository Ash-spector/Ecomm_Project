var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "20%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "10%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "isAuthorized",
                "width": "10%",
                "render": function (data) {
                    if (data) {
                        return `<input type="checkbox" checked disabled />`;
                    } else {
                        return `<input type="checkbox" disabled />`;
                    }
                }
            },
            {
                "data": "id",
                "width": "15%",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="/Admin/Company/Upsert/${data}" class="btn btn-info">
                            <i class="fas fa-edit"></i>
                        </a>
                        <a onclick="Delete('/Admin/Company/Delete/${data}')" class="btn btn-danger">
                            <i class="fas fa-trash"></i>
                        </a>
                    </div>`;
                }
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Want to Delete Data ?",
        text: "Delete this Covertype?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: url,
                    type: "DELETE",
                    success: function (data) {
                        if (data.success) {
                            toastr.success(data.message);
                            datatable.ajax.reload();
                        } else {
                            toastr.error(data.message);
                        }
                    }
                });
            }
        });
}
