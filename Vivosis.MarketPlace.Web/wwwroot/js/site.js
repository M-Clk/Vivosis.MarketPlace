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
function showPopup(url, title, afterOpenFunc) {
    jQuery.ajax(
        {
            type: "GET",
            url: url,
            success: function (res) {
                jQuery("#form-modal .modal-body").html(res);
                jQuery("#form-modal .modal-title").html(title);
                jQuery("#form-modal").modal("show");
            }
        }).then(afterOpenFunc);
}

function afterOpenCategoryEdit() {

}

function loadCategoryOptions(categoryCode) {
    var url = jQuery("#category-options-area").attr('url') + "&categoryCode=" + categoryCode;
    jQuery.ajax(
        {
            type: "GET",
            url: url,
            success: function (res) {
                if (res.isEmpty) {
                    jQuery("#category-options-area").empty();
                    jQuery("#category-options-area").hide();
                    return;
                }
                jQuery("#category-options-area").empty().append(res.html);
                jQuery("#category-options-area").show();
                jQuery('select').selectpicker();
            }
        });
}

function loadCategories(url) {
    jQuery.ajax(
        {
            type: "GET",
            url: url,
            success: function (res) {
                if (res.isEmpty)
                    return;
                jQuery("#categories-row").append(res.html);
                jQuery('select').selectpicker();
                jQuery('#category-name').attr('disabled', 'disabled');
                jQuery('#category-code').attr('disabled', 'disabled');
                jQuery('#currency-option').on('changed.bs.select', function () {
                    jQuery('#currency').val(jQuery(this).val());
                });
                if (jQuery('#currency').length > 0) {
                    jQuery("#currency-option option[value='" + jQuery('#currency').val() + "']").attr('selected', 'selected');
                    jQuery("#currency-option").trigger('change');
                }
            }
        }).then(() => loadSelectOptionEvents(1));
}

function loadSelectOptionEvents(id) {
    jQuery('select#' + id).on("changed.bs.select", function () {
        jQuery(this).parent().parent().nextAll().remove();
        var indexId = parseInt(jQuery(this).attr("id"));
        var lastId = indexId + 1;
        var categoryText = '';
        for (i = 1; i <= indexId; i++) {
            categoryText += jQuery('select#' + i).children("option:selected").val() + ' > ';
        }
        var url = jQuery('option:selected', this).attr("url");
        var code = jQuery('option:selected', this).attr("id");
        categoryText = categoryText.substr(0, categoryText.length - 3);
        jQuery('#category-name').prop('value', categoryText);
        jQuery('#category-code').prop('value', code);
        if (code.length > 0)
            loadCategoryOptions(jQuery('#category-code').prop('value'));
        jQuery.ajax(
            {
                type: "GET",
                url: url,
                success: function (res) {
                    if (res.isEmpty)
                        return;
                    res.html = res.html.replace(/(select id=[ ]?\"[ ]?)([0-9]+)(\")/, '$1' + lastId + '$3')
                    jQuery("#categories-row").append(res.html);
                    jQuery('select#' + lastId).selectpicker();
                    loadSelectOptionEvents(lastId);
                }
            });
    });
}

function submitStoreCategory(form, infoId) {
    try {
        jQuery('#category-name').removeAttr('disabled');
        jQuery('#category-code').removeAttr('disabled');
        jQuery.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    jQuery('#form-modal .modal-body').html('');
                    jQuery('#form-modal .modal-title').html('');
                    jQuery('#form-modal').modal('hide');
                    jQuery('#' + infoId).removeClass('text-warning').addClass('text-success');
                    jQuery('#' + infoId).html('Eşleştirildi');
                }
                else
                    jQuery('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

submitStore = form => {
    try {
        jQuery.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    jQuery('#list-user-stores').html(res.html)
                    jQuery('#form-modal .modal-body').html('');
                    jQuery('#form-modal .modal-title').html('');
                    jQuery('#form-modal').modal('hide');
                    jQuery("input[data-toggle='toggle']").bootstrapToggle();
                    jQuery('#mytable').DataTable(
                        {
                            language: {
                                url: '//cdn.datatables.net/plug-ins/1.10.22/i18n/Turkish.json'
                            },
                            columnDefs: [
                                { orderable: false, targets: 0 }
                            ],
                            order: [[1, 'asc']]
                        });
                }
                else
                    jQuery('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

function optionsAfterOpen() {
    jQuery("#option").children().first().children().addClass("active");
    jQuery("#option-content").children().first().addClass("active");
    jQuery("input[data-toggle='toggle']").bootstrapToggle();
    jQuery("input[name='all-checkbox-weight']").on("change", function () {
        var optionId = jQuery(this).prop('id');
        jQuery("input[name='checkbox-" + optionId + "']").prop('checked', jQuery(this).prop('checked')).change();
    });
    jQuery("input[name='all-checkbox-price']").on("change", function () {
        var optionId = jQuery(this).prop('id');
        jQuery("input[name='checkbox-" + optionId + "']").prop('checked', jQuery(this).prop('checked')).change();
    });
    jQuery("input[name='all-checkbox-substract']").on("change", function () {
        var optionId = jQuery(this).prop('id');
        jQuery("input[name='checkbox-" + optionId + "']").prop('checked', jQuery(this).prop('checked')).change();
    });
}

function editStoreProductAfterOpen() {
    jQuery("input[data-toggle='toggle']").bootstrapToggle();
    jQuery('select').selectpicker();
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

jQuery(function () {
    jQuery('input[name ="storeStatusCheckbox"]').change(function () {
        var url = jQuery(this).prop("value");
        jQuery.ajax(
            {
                type: "GET",
                url: url
            });
    })
})
