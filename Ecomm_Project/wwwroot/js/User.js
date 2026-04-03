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
                            <a class="btn btn-danger text-white" style="cursor:pointer"
                               onclick="lockUnlock('${data.id}')">
                               <i class="fas fa-lock"></i> Lock
                            </a>
                        </div>`;
                    }
                    else {
                        return `
                        <div class="text-center">
                            <a class="btn btn-success text-white" style="cursor:pointer"
                               onclick="lockUnlock('${data.id}')">
                               <i class="fas fa-unlock"></i> Unlock
                            </a>
                        </div>`;
                    }
                },
            }
        ]
    });
}

function lockUnlock(id) {
    $.ajax({
        url: "/Admin/User/LockUnlock",
        type: "POST",
        data: JSON.stringify(id),  
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}