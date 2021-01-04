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

function loadCategoryOption(categoryOptionFromStoreId, optionId, callback) {
    jQuery('#institution-options-' + categoryOptionFromStoreId).selectpicker('val', optionId);
    callback();
}

function openCategoryOptionsValues(url, id, categoryOptionId) {
    var selectedOptionId = jQuery("#institution-options-" + id + " option:selected").attr('value');
    url = url + "&optionId=" + selectedOptionId + "&categoryOptionId=" + categoryOptionId;
    jQuery.ajax(
        {
            type: "GET",
            url: url,
            success: function (res) {
                if (res.isEmpty) {
                    jQuery("#option-values-area-" + id).empty();
                    return;
                }
                jQuery("#option-values-area-" + id).empty().append(res.html);
                jQuery('select').selectpicker();
            }
        });
}

function saveStoreCategory(url, infoId) {
    try {
        var storeCategory = {};
        storeCategory.store_category_id = Number(jQuery('#store-category-id').val());
        storeCategory.store_id = Number(jQuery('#store-id').val());
        storeCategory.category_id = Number(jQuery('#category-id').val());
        storeCategory.is_matched = true;
        storeCategory.matched_category_name = jQuery('#category-name').val();
        storeCategory.matched_category_code = jQuery('#category-code').val();
        storeCategory.commission = Number(jQuery('#commission').val());
        storeCategory.currency = jQuery('#currency').val();
        storeCategory.shipping_fee = Number(jQuery('#shipping-fee').val());
        var finishTheProcess = false;
        var categoryOptions = new Array();
        jQuery("#option-content div [name='category-options']").each(function () {
            var optionRow = jQuery('select', this);
            var isRequired = optionRow.attr("is-required") == 'true';
            if (isRequired && jQuery('option:selected', optionRow).val().length == 0) {
                alert("Lütfen zorunlu seçenekleri boş geçmeyin.");
                finishTheProcess = true;
                return false;
            }
            if (jQuery('option:selected', optionRow).val().length == 0)
                return;
            var categoryOption = {};
            categoryOption.category_option_id = Number(optionRow.attr("category-option-id"));
            categoryOption.store_category_id = storeCategory.store_category_id;
            categoryOption.option_id = Number(jQuery('option:selected', optionRow).val());
            categoryOption.is_required = isRequired;
            categoryOption.matched_store_option_id = optionRow.attr("option-id");
            categoryOption.matched_store_option_name = optionRow.attr("option-name");
            var categoryOptionValues = new Array();
            jQuery("#option-values-table-" + categoryOption.matched_store_option_id + " tbody tr").each(function () {
                var row = jQuery(this);
                var categoryOptionValue = {};
                categoryOptionValue.category_option_value_id = Number(row.attr('category-option-value-id'));
                categoryOptionValue.category_option_id = Number(row.attr('category-option-id'));
                categoryOptionValue.option_value_id = Number(row.attr('option-value-id'));
                var selectedOption = jQuery('#store-attribute-value-options-' + categoryOption.matched_store_option_id + '-' + categoryOptionValue.option_value_id + ' option:selected', row);
                categoryOptionValue.store_category_value_id = Number(selectedOption.attr('value'));
                categoryOptionValue.store_category_value_name = selectedOption.attr('attribute-name');
                categoryOptionValues.push(categoryOptionValue);
            });
            categoryOption.CategoryOptionValues = categoryOptionValues;
            categoryOptions.push(categoryOption);
        });
        if (finishTheProcess)
            return;
        storeCategory.CategoryOptions = categoryOptions;
        var result = JSON.stringify(storeCategory);
        jQuery('#category-name').removeAttr('disabled');
        jQuery('#category-code').removeAttr('disabled');
        jQuery.ajax({
            type: "POST",
            url: url,
            data: result,
            processData: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
                jQuery('.tab-pane .selectpicker').on('changed.bs.select', function () {
                    id = jQuery(this).attr('option-id');
                    jQuery('#get-option-values-' + id).removeAttr('disabled');
                });
            }
        });
}

function loadCategories(url, loadSubCategoriesUrl) {
    fillCategories(url, fillCategoriesCallback, 1).done(() => {
        if (loadSubCategoriesUrl.length > 0) {
            jQuery.ajax(
                {
                    type: "GET",
                    url: loadSubCategoriesUrl,
                    success: async function (res) {
                        if (res.isEmpty)
                            return;
                        var idList = JSON.parse(res.idList);
                        for (var i = 1; i <= idList.length; i++) {
                            await new Promise(resolve => setTimeout(resolve, 100));
                            selectCategory('select#' + i, idList[i - 1]);
                        }
                    }
                });
        }
    });
}
function selectCategory(selectId, value) {
    jQuery(selectId).selectpicker('val', value);
}

function fillCategories(url, callback, elementOrderId) {
    return jQuery.ajax(
        {
            type: "GET",
            url: url,
            success: function (res) {
                if (res.isEmpty)
                    return;
                res.html = res.html.replace(/(select id=[ ]?\"[ ]?)([0-9]+)(\")/, '$1' + elementOrderId + '$3')
                jQuery("#categories-row").append(res.html);
                jQuery('select#' + elementOrderId).selectpicker();
                callback();
                loadSelectOptionEvents(elementOrderId);
            }
        });
}
function fillCategoriesCallback() {
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

function loadSelectOptionEvents(id) {
    jQuery('select#' + id).on("changed.bs.select", function () {
        jQuery(this).parent().parent().nextAll().remove();
        var indexId = parseInt(jQuery(this).attr("id"));
        var lastId = indexId + 1;
        var categoryText = '';
        for (i = 1; i <= indexId; i++) {
            categoryText += jQuery('select#' + i).children("option:selected").val() + ' > ';
        }
        var url = jQuery('#categories-row').attr("url");
        var code = jQuery('option:selected', this).attr("id");
        categoryText = categoryText.substr(0, categoryText.length - 3);
        jQuery('#category-name').prop('value', categoryText);
        jQuery('#category-code').prop('value', code);
        if (code.length > 0)
            loadCategoryOptions(jQuery('#category-code').prop('value'));
        jQuery.ajax(
            {
                type: "GET",
                url: url + "?parentId=" + code,
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
function submitStoreProduct(form) {
    try {
        if (jQuery('#description').val().length > 150)
        {
            alert("Aciklama max 150 karakter uzunlugunda olmalidir.");
            return false;
        }
        var attributes = jQuery("select[name='product-attribute']", form);
        var query = '';
        attributes.each((index, select) => {
            if (select.value.length > 0)
                query += jQuery(select).attr('attribute-name') + '=' + select.value + '&';
        });
        query = query.slice(0, query.length-1);
        var data = new FormData(form);
        data.set("AttributesQuery", query);
        jQuery.ajax({
            type: 'POST',
            url: form.action,
            data: data,
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isSucced) {
                    jQuery('#form-modal .modal-body').html('');
                    jQuery('#form-modal .modal-title').html('');
                    jQuery('#form-modal').modal('hide');
                }
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

$('table#table-categories').DataTable(
    {
        language: {
            url: '//cdn.datatables.net/plug-ins/1.10.22/i18n/Turkish.json'
        },
        columnDefs: [
            { orderable: false, targets: 0 }
        ],
        order: [[1, 'asc']]
    });
$('table#table-products').DataTable(
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