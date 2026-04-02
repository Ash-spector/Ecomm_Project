var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": null,
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                        <div class="text-center">
                            <a class="btn btn-danger" onclick="lockUnlock('${data.id}')">
                            </a>
                        </div>`;
                    }
                    else {
                        return `
                        <div class="text-center">
                            <a class="btn btn-success" onclick="lockUnlock('${data.id}')">
                            </a>
                        </div>`;
                    }
                },
            }
        ]
    });
}
function lockUnlock(id) {
    alert(id);
}