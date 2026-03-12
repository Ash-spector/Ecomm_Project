var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    datatable = $('#tblData').DataTable({

        "ajax": {
            "url": "/Admin/Category/GetAll"
        },

        "columns": [
            { "data": "name", "width": "70%" },

            {
                "data": "id",
                "render": function (data) {

                    return `
                    <div class="text-center">

                        <a href="/Admin/Category/Upsert/${data}" class="btn btn-info">
                            <i class="fas fa-edit"></i>
                        </a>

                        <a class="btn btn-danger" onclick="Delete('/Admin/Category/Delete/${data}')">
                            <i class="fas fa-trash-alt"></i>
                        </a>

                    </div>
                    `;
                },
            }
        ]
    });

}