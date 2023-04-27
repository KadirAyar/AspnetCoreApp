// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$('#btnAddForm').click(function () {
    $('#FormModal').modal('show');
})

$('#btnClose').click(function () {
    $('#FormModal').modal('hide');
})
$('#btnClosee').click(function () {
    $('#FormModal').modal('hide');
})

function AddForm() {
    debugger
    var objdata = {
        Name: $('#Name').val(),
        Description: $('#Description').val(),
        CreatedAt: $('#CreatedAt').val()
    }
    $.ajax({
        url: '/Forms/AddForm',
        type: 'Post',
        data: objdata,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8',
        dataType: 'json',
        success: function () {
            alert('Kayıt Başarılı');
        },
        error: function () {
            alert('Kayıt Edilemedi!');
        }
    });
}