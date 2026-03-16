var datatable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {

    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/CoverType/GetAll"
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
    alert(url);
}