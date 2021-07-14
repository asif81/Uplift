﻿var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("approved")) {
        loadDataTable("GetAllApprovedOrders");
    }
    else if (url.includes("all")) {
        loadDataTable("GetAllOrders");
    }
    else {
        loadDataTable("GetAllPendingOrders");
    }
});

function loadDataTable(url) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/order/"+url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "phone", "width": "15%" },
            { "data": "email", "width": "20%" },
            { "data": "serviceCount", "width": "15%" },
            { "data": "status", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="/admin/order/details/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;"> 
                             Details
                            </a>
                            </div>`
                }, "width": "15%"
            }
        ],
        language: {
            "emptyTable": "No records found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the content!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: "DELETE",
            url: url,
            success: function (data) {
                if (data.success) {
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.error);
                }
            }
        });
    }
    )
}

function ShowMessage(msg) {
    toastr.success(msg);
}