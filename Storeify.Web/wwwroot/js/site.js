﻿// Shared variables
var table;
var datatable;
var updatedRow;
var exportedCols = [];

function showSuccessMessage(message = 'Saved Successfully!') {
    //Use Sweet Alert
    Swal.fire({
        icon: 'success',
        title: 'Good Job',
        text: message,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}

function showErrorMessage(message = 'Something went wrong!') {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: message,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}
function disableSubmitButton(btn) {
    $(btn).attr('disabled', 'disabled').attr('data-kt-indicator', 'on');
}

function onModalBegin() {
    disableSubmitButton($('#Modal').find(':submit'));
}

function onModalSuccess(row) {
    showSuccessMessage();
    $('#Modal').modal('hide');

    if (typeof updatedRow !== 'undefined' && updatedRow !== null) {
        datatable.row(updatedRow).data($(row).children().map(function () {
            return $(this).html();
        }).get()).draw(false); // Update row without full table refresh
        updatedRow = undefined;
    } else {
        datatable.row.add($(row)).draw(false); // Add new row without refreshing
    }
    // Reinitialize KTMenu only if needed
    KTMenu.init();
    KTMenu.initHandlers()

}

function onModalComplete() {
    $('body :submit').prop('disabled', false).removeAttr("data-kt-indicator");

}

//Select2
function applySelect2() {
    $('.js-select2').select2();
    $('.js-select2').on('select2:select', function (e) {
        $('form').not('#SignOut').validate().element('#' + $(this).attr('id'));
    });
}

//Data Table
var headers = $('th');
$.each(headers, function (i) {
    var col = $(this);
    if (!col.hasClass('js-no-export'))
        exportedCols.push(i);


});

// Class definition
var KTDatatables = function () {

    // Private functions
    var initDatatable = function () {
        // Init datatable --- more info on datatables: https://datatables.net/manual/
        datatable = $(table).DataTable({
            "info": false,
            'order': [],
            'pageLength': 10,
        });
    }

    // Hook export buttons
    var exportButtons = () => {
        const documentTitle = $('.js-datatables').data('document-title');
        var buttons = new $.fn.dataTable.Buttons(table, {
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportedCols
                    }
                },
                {
                    extend: 'excelHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportedCols
                    }
                },
                {
                    extend: 'csvHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportedCols
                    }
                },
                {
                    extend: 'pdfHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exportedCols
                    }
                }
            ]
        }).container().appendTo($('#kt_datatable_buttons'));

        // Hook dropdown menu click event to datatable export buttons
        const exportButtons = document.querySelectorAll('#kt_datatable_export_menu [data-kt-export]');
        exportButtons.forEach(exportButton => {
            exportButton.addEventListener('click', e => {
                e.preventDefault();

                // Get clicked export value
                const exportValue = e.target.getAttribute('data-kt-export');
                const target = document.querySelector('.dt-buttons .buttons-' + exportValue);

                // Trigger click event on hidden datatable export buttons
                target.click();
            });
        });
    }

    // Search Datatable
    var handleSearchDatatable = () => {
        const filterSearch = document.querySelector('[data-kt-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    }

    // Public methods
    return {
        init: function () {
            table = document.querySelector('.js-datatables');

            if (!table) {
                return;
            }

            initDatatable();
            exportButtons();
            handleSearchDatatable();
        }
    };
}();

$(document).ready(function () {
    //Disable submit button
    //$('form').not('#SignOut').not('.js-excluded-validation').on('submit', function () {
    //    if ($('.js-tinymce').length > 0) {
    //        $('.js-tinymce').each(function () {
    //            var input = $(this);

    //            var content = tinyMCE.get(input.attr('id')).getContent();
    //            input.val(content);
    //        });
    //    }

    //    var isValid = $(this).valid();
    //    if (isValid) disableSubmitButton($(this).find(':submit'));
    //});

    //TinyMCE
    if ($('.js-tinymce').length > 0) {
        var options = {
            selector: ".js-tinymce",
            height: "240",
            //directionality: currentLanguage == 'ar' ? 'rtl' : 'ltr',
            //language: currentLanguage != 'en' ? currentLanguage : undefined,
        };

        if (KTThemeMode.getMode() === "dark") {
            options["skin"] = "oxide-dark";
            options["content_css"] = "dark";
        }

        tinymce.init(options);
    }



    //Select2
    //applySelect2();
    $('.js-select2').select2();


    //Datepicker
    $('.js-datepicker').daterangepicker({
        singleDatePicker: true,
        autoApply: true,
        drops: 'up',
        locale: {
            format: 'DD/MM/YYYY'
        },
        maxDate: new Date()
    });

    // Display SweetAlert if message exists
    var messageElement = $('#Message');
    if (messageElement.length && messageElement.text().trim() !== '') {
        showSuccessMessage(messageElement.text());
    }

    // Initialize DataTables
    KTUtil.onDOMContentLoaded(function () {
        KTDatatables.init();
    });

    // Handle Bootstrap Modal Rendering
    $('body').on('click', '.js-render-modal', function () {

        var btn = $(this);
        var modal = $('#Modal');
        modal.find('#ModalLabel').text(btn.data('title'));

        if (btn.data('update') !== undefined) {
            updatedRow = btn.closest('tr');
        }

        $.get({
            url: btn.data('url'),
            success: function (form) {
                modal.find('.modal-body').html(form);
                $.validator.unobtrusive.parse(modal);
                $('input[type="tel"]').mask('(000) 0000-0000');  


                // Reinitialize select2 & datepicker inside modal
                modal.on('shown.bs.modal', function () {
                    $('.js-select2').select2();
                    $('.js-datepicker').daterangepicker({
                        singleDatePicker: true,
                        drops: 'auto',
                    });
                });

            },
            error: function () {
                showErrorMessage();
            }
        });

        modal.modal('show');
    });

    // Handle Toggle Status
    $('body').on('click', '.js-toggle-status', function () {
        var btn = $(this);

        bootbox.confirm({
            message: "Are you sure that you need to Delete this item?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-primary'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-secondary'
                }
            },
            callback: function (result)
            {
                if (result) {
                    $.post({
                        url: btn.data('url'),
                        data: {
                            '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                        },
                        success: function (LastUpdatedOn) {
                            var row = btn.closest('tr');
                            var status = row.find('.js-status');
                            var newStatus = status.text().trim() === 'Deleted' ? 'Available' : 'Deleted';

                            // Update status text and styles
                            status.text(newStatus).toggleClass('badge-light-success badge-light-danger');
                            row.find('.js-updated-on').html(LastUpdatedOn);
                            row.addClass('animate__animated animate__flash');

                            showSuccessMessage("Edit Successfully");
                        },
                        error: function () {
                            showErrorMessage();
                        }
                    });
                }
            }
        });
    });
});
