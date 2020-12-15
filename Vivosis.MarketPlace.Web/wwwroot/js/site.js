// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
function generateRandomPass(elementId) {
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var result = '';
    var charactersLength = characters.length;
    for (var i = 0; i < 15; i++)
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    document.getElementById(elementId).value = result;
}
function showPopup(url, title) {
    jQuery.ajax(
        {
            type: "GET",
            url: url,
            success: function (res) {
                jQuery("#form-modal .modal-body").html(res);
                jQuery("#form-modal .modal-title").html(title);
                jQuery("#form-modal").modal("show");
            }
        }).then(
            function () {
                jQuery("#option").children().first().children().addClass("active");
                jQuery("#option-content").children().first().addClass("active");
                jQuery("input[data-size='xs']").bootstrapToggle();
            });
}
$('#mytable').DataTable(
    {
        language: {
            url: '//cdn.datatables.net/plug-ins/1.10.22/i18n/Turkish.json'
        },
        columnDefs: [
            { orderable: false, targets: 0 }
        ],
        order: [[1, 'asc']]
    });