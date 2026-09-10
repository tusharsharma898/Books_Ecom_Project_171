var dataTable;

$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },
        "lengthMenu": [[2,4,6,8,10],["Two","Four","Six","Eight","Ten"]],
        "columns": [
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                    <a href="/Admin/Category/Upsert/${data}" class="btn btn-primary">
                    <i class="fas fa-edit"></i>
                    </a>
                    <a class="btn btn-danger" onclick=Delete('/Admin/Category/Delete/${data}')>
                    <i class="fas fa-trash"></i>
                    </a>
                    </div>
                    `;
                }
            },
            { "data": "name", "width": "70%" ,"className":"text-center"}

        ]
    })
}
function Delete(url) {
    // alert(url)
    swal({
        title: "Want To Delete Data",
        text: "sure to delete?",
        icon: "warning",
        buttons: true,
        dangerModel: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }

            })
        }
    });
}