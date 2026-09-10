var dataTable;

$(document).ready(function () {
    loadDataTable()
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll",

        },
        "columns": [
            { "data": "name", "width": "12%" },
            { "data": "streetAddress", "width": "12%" },
            { "data": "city", "width": "12%" },
            { "data": "state", "width": "12%" },
            { "data": "postalCode", "width": "12%" },
            { "data": "phoneNumber", "width": "12%" },
            {
                "data": "isAuthorizedCompany",
                "width": "12%",
                "render": function (data) {
                    if (data) {
                        return `
                        <input type="checkbox" checked disable/>
                        `;
                    }
                    else {
                        return `
                        <input type="checkbox" checked/>
                        `;
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                    <a href="/Admin/Company/Upsert/${data} " class="btn btn-primary" >
                    <i class="fas fa-edit"></i>
                    </a>
                    <a class="btn btn-danger" onclick=Delete('/Admin/Company/Delete/${data}')>
                    <i class="fas fa-trash"></i>
                    </a>
                    </div>
                    `;
                }
            }
        ]
    })
}
function Delete(url) {
    alert(url);
}