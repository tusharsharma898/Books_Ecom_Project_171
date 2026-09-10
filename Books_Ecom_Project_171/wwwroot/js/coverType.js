var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tbldataa').DataTable({
        "ajax": {
         "url": "/Admin/CoverType/GetAll"
        },

        "lengthMenu":[2,4,6,8,10],
        "columns": [
            {
                "data": "id","width":"15%",
                "render": function (data) {
                    return `
                   
                    <a href="/Admin/CoverType/Upsert/${data}" class="btn btn-primary" >
                    <i class="fas fa-edit"></i>
                    </a>
                    
                    `;
                }
            },
           
            { "data": "name", "width": "70%","className":"text-center" },
            {
                "data": "id","width":"15%",
                "render": function (data) {
                    return `
                   
                    <a class="btn btn-danger" onclick=Delete('/Admin/CoverType/Delete/${data}')>
                    <i class="fas fa-trash"></i>
                    </a>
                    
                    `;
                }
            }
        ]

    })
}
function Delete(url) {
    // alert(url)
    swal({
        title: "Want To Delete Data",
        text: "sure tu delete?",
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
