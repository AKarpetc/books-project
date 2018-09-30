$(document).ready(function ()
{
    var dataSource, gridBooks, dateFrom, dateTo, authorGrid;
    var href = location.href.replace("#", "");
    var localStorageIndex = href + '_gridBooks_grid-sorts-parameter';

    dateFrom = $("#dateFrom").kendoDatePicker(
        {
            culture: "ru-RU",
            value: addMonths(new Date(), -1)

        });

    dateTo = $("#dateTo").kendoDatePicker(
        {
            culture: "ru-RU",
            value: new Date(),
        });

    dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: bookUrl + "/kendods",
                contentType: "application/json",
                type: "POST"
            },
            update: {
                url: bookUrl,
                dataType: "json",
                type: "PUT"
            },
            destroy:
            {
                url: bookUrl,
                dataType: "json",
                type: "DELETE"
            },
            create: {
                url: bookUrl,
                dataType: "json",
                type: "POST",

            },

            parameterMap: function (options, operation) {
                if (operation == "read") {
                    return kendo.stringify(options);
                } else {
                    options.AuditDateTime = null;
                    return options;
                }
            }
        },
        pageSize: 20,
        schema: {
            model: {
                id: "Id",
                fields: {
                    Id: { type: "number" },
                    Header: {
                        type: "string", validation:
                        {
                            required: true,
                            maxlength: function (input) {
                                if ((input.is("[name='Header']") || input.is("[name='PublishingOffice']")) && input.val().length > 30) {
                                    input.attr("data-maxlength-msg", "Максимальная длина 30");
                                    return false;
                                };
                                if (input.is("[data-validmask-msg]") && input.val() != "") {
                                    var maskedtextbox = input.data("kendoMaskedTextBox");
                                    if (maskedtextbox.value().indexOf(maskedtextbox.options.promptChar) != -1) {
                                        input.attr("data-maxlength-msg", "Не коректный ISBN");
                                        return false;
                                    }
                                }
                                if (input.is("[name='authorsGridMsg']") && authorGrid.dataSource.view().length == 0) {
                                    $("#authorsGridMsg").attr("data-maxlength-msg", "Книга должна содержать хотя бы одного автора");
                                    return false;
                                }
                                return true;
                            }
                        }
                    },
                    ISBN: { type: "string" },
                    AuctorsShort: { type: "string" },
                    AuditDateTime: { type: "date" },
                    IsWithCover: {type:"boolean"},
                    PageCount: { type: "number", validation: { required: true } },
                    PublishingOffice:
                    {
                        type: "string", validation: { required: true }
                    }
                },
           
            },

            data: "Data",
            total: "Total"
        },
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true,
        sort: getLocalStorage(localStorageIndex)
    });

    gridBooks = $("#gridBooks").kendoGrid({
        dataSource: dataSource,
        height: '85vh',
        filterable: { mode: "row" },
        sortable: true,
        sort: function (e) {
            setLocalStorage(localStorageIndex, e.sort);
        },
        pageable: true,
        selectable: true,
        toolbar: ["create", "edit", "destroy"],
        save: function (e) {
           // if (e.model.Id == 0)
            {
                e.model["Auctors"] = $.map(authorGrid.dataSource.view(), function (authorItem) {
                    return { Name: authorItem.Name, Surname: authorItem.Surname }
                })
            }
        },
        columns: [
            {
                template: kendo.template($("#book-editor-cover").html()),
                width: 100,
                title: "Обложка",
            },
            {
                field: "Header",
                title: "Заголовок",
                filterable: {

                    cell: { operator: "contains" }
                }
            },
            {
                field: "AuctorsShort",
                title: "Авторы",
                sortable: false,
                template: function (e) {
                    if (e.AuctorsShort == null || e.AuctorsShort.length == 0) {
                        return "";

                    } else {
                        return e.AuctorsShort;
                    }
                },
                filterable: {

                    cell: { operator: "contains" }
                }
            },
            {
                field: "PublishYear",
                title: "Год"
            },
            {
                field: "PageCount",
                title: "Количество страниц",
                sortable: false
            },
            {
                field: "PublishingOffice",
                title: "Издательство",
                sortable: false,
                filterable: {

                    cell: { operator: "contains" }
                }
            },

            {
                title: "ISBN",
                field: "ISBN",
                sortable: false,
                filterable: {

                    cell: { operator: "contains" }
                }
            },
            {
                sortable: false,
                filterable: false,
                field: "AuditDateTime",
                title: "Создана",
                template: "#:kendo.toString(AuditDateTime,'dd.MM.yy')#"
            },
        ],
        editable:
        {
            mode: "popup",
            template: kendo.template($("#book-editor-form").html()),
            window:
            {

                title: "Книга",
                width: 550,
                open: function () {
                    var item = gridBooks.dataItem(gridBooks.select());
                    authorGrid = new aurhorsGrid(item == null ? 0 : item.Id);
                }
            }
        }
    }).data("kendoGrid");

    gridBooks = gridBooks.PagerExtention();
    gridBooks = gridBooks.addColumnsSelector();

    $("#gridBooks .k-grid-edit").click(function () {
        gridBooks.editRow(gridBooks.select());

    });

    $("#gridBooks .k-grid-delete").click(function () {
        gridBooks.removeRow(gridBooks.select());

    });

    function ColBackFileSet(id,ref)
    {
        $(".cover-for-book-" + id).attr("src", ref)
        gridBooks.dataSource.get(id).IsWithCover = true;
    }

    $("body").on("click", ".cover-ref", function ()
    {
        var bookId = $(this).data("id");
        var IsWithCover = Boolean($(this).data("is-with-cover")); 
        var Item = gridBooks.dataSource.get(bookId)
        openCover(bookId, IsWithCover, ColBackFileSet, Item.Header);
    });


});