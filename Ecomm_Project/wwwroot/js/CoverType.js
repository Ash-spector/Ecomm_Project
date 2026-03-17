var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    // Destroy existing DataTable instance if it exists (prevents re-init errors)
    if ($.fn.DataTable.isDataTable('#tblData')) {
        $('#tblData').DataTable().destroy();
    }

    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/CoverType/GetAll",
            "type": "GET",
            "dataType": "json",
            "dataSrc": "data"   // <-- THIS was missing: maps the "data" key from your JSON response
        },
        "columns": [
            { "data": "name", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="/Admin/CoverType/Upsert/${data}" class="btn btn-info">
                            <i class="fas fa-edit"></i>
                        </a>
                        <a class="btn btn-danger" onclick="Delete('/Admin/CoverType/Delete/${data}')">
                            <i class="fas fa-trash-alt"></i>
                        </a>
                    </div>
                    `;
                },
                "width": "30%"
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
