function isDef(v) {
    return v !== undefined && v !== null
}

function traverce(source, dist) {
    for (var prop in source) {
        if (source[prop] instanceof Object)
            traverce(source[prop], dist[prop]);
        else if (isDef(dist[prop])) {
            source[prop] = dist[prop];
        }
    }
    return source;
}

kendo.ui.Grid.prototype.addColumnsSelector = function (settings) {
    var settingsDefault = {
        toolbar: {
            headerFloat: "right",
            showText: true,
            PageSizeVisibility: true,
        }
    };

    if (settings == null) {
        settings = settingsDefault;
    }
    else {
        var settings = traverce(settingsDefault, settings);
    }


    if (this.KendoGridColumnsSelectorPlaginLocalizations == null) {
        this.KendoGridColumnsSelectorPlaginLocalizations =
            {
                MainLabel: "Параметры таблицы"
            }
    }


    var localization = this.KendoGridColumnsSelectorPlaginLocalizations;

    var StreamsGrid_ForSelector_WithWindowSelector = this;
    var href = location.href.replace("#", "");
    var localStorageIndex = location.href + "_" + this.element[0].id + 'grid-filters-column-selector';
    var Id = this.element[0].id;
    var id = Id;
    this.Window = $("<div/>",
        {
            Id: Id + "_settingWindow"
        });

    this.ColumnsSelector = $("<div/>",
        {
            Id: Id + "_columnsSelector"
        });

    if (!$('#' + Id + '_toolbar_button_setting').length)
        $("#" + id).find(".k-grid-toolbar").prepend('<a style="padding: 2px 5px;float:' + settings.toolbar.headerFloat + '" class="k-button k-button-icontext" id="' + Id + '_toolbar_button_setting"><i class="fa fa-cogs" aria-hidden="true"></i>' + (settings.toolbar.showText ? localization.MainLabel : "") + '</a>');



    this.Window.append(this.ColumnsSelector);

    columnSelectorWindow = this.Window.kendoWindow(
        {
            modal: true,
            title: "Активные столбцы таблицы",
            width: '500px',
            maxHeight: 700,
            visible: false,
        }).data("kendoWindow");
    this.columnSelectorWindow = columnSelectorWindow;
    $('#' + Id + '_toolbar_button_setting').click(function () {
        columnSelectorWindow.center();
        columnSelectorWindow.open();
    });


    this.StreamsGridColumns = this.ColumnsSelector.kendoGrid(
        {
            toolbar: [{ template: "<a id='" + id + "_columnsSelector_selectAll' class='k-button k-button-icontext'> Выделить все</a><a id='" + id + "_columnsSelector_clearAll' class='k-button k-button-icontext'> Снять все</a>" }],

            columns:
            [

                { field: "text", title: "Столбец" },
                {
                    field: "hidden", title: "Отображать", template: function (e) {
                        var state = "";
                        if (!e.hidden) {
                            state = "checked";
                        }

                        return "<input class='" + id + "_columnsSelector_isHidden' data-name='" + e.name + "' type='checkbox' " + state + " />"


                    }
                },

            ],
            saveChanges: function (e) {

                console.log(this.dataSource.view());

            },
        }).data("kendoGrid");
    function SelectClear(checkbox) {
        if (checkbox.prop("checked")) {
            StreamsGrid_ForSelector_WithWindowSelector.showColumn(checkbox.data("name"))
        }
        else {
            StreamsGrid_ForSelector_WithWindowSelector.hideColumn(checkbox.data("name"))
        }

        StreamsGrid_ForSelector_WithWindowSelector.StreamsGridColumns.setDataSource(CreateColumnDataSource())
        StreamsGrid_ForSelector_WithWindowSelector.StreamsGridColumns.dataSource.view()
        localStorage.setItem(localStorageIndex, JSON.stringify(StreamsGrid_ForSelector_WithWindowSelector.StreamsGridColumns.dataSource.view()));
        StreamsGrid_ForSelector_WithWindowSelector.refresh();

    }


    function ReloadFilters() {

        var Columns = localStorage.getItem(localStorageIndex);
        if (Columns == null) {
            return;
        }
        var ColumnsObj = JSON.parse(Columns);
        $.map(ColumnsObj, function (item) {

            if (item.hidden) {
                StreamsGrid_ForSelector_WithWindowSelector.hideColumn(item.name);

            }
            else {
                StreamsGrid_ForSelector_WithWindowSelector.showColumn(item.name)

            }
            //StreamsGrid_ForSelector_WithWindowSelector.autoFitColumn(item.name);

        });

    };
    ReloadFilters();

    function CreateColumnDataSource()
    {

        var Model = $.map(StreamsGrid_ForSelector_WithWindowSelector.columns, function (item)
        {

            console.log(item.columns);

            if (item.columns == null) {
                if (isDef(item.title)) {
                    var obj =
                        {
                            name: item.field,
                            text: item.title,
                            hidden: item.hidden == undefined ? false : item.hidden,

                        };
                    return obj;
                }

            } else
            {
                var GlobalItem = item;
              return  $.map(item.columns, function (item)
                {
                  if (isDef(item.title) && isDef(GlobalItem.title))
                    {
                        var obj =
                            {
                                name: item.field,
                                text: GlobalItem.title+"."+ item.title,
                                hidden: item.hidden == undefined ? false : item.hidden,

                            };
                        return obj;
                    }

                })
            }

        });

        var dataSource = new kendo.data.DataSource(
            {
                data: Model
            });
        return dataSource;
    }


    $("#" + id + "_columnsSelector_selectAll").click(function () {

        $("." + id + "_columnsSelector_isHidden").prop("checked", true);

        $.each($("." + id + "_columnsSelector_isHidden"), function (index, item) {

            SelectClear($(item));
        })
        // $(".isHidden").trigger("change");
    });

    $("#" + id + "_columnsSelector_clearAll").click(function () {
        $("." + id + "_columnsSelector_isHidden").prop("checked", false);

        $.each($("." + id + "_columnsSelector_isHidden"), function (index, item) {
            SelectClear($(item));
        })
    });

    $("body").on("change", "." + id + "_columnsSelector_isHidden", function (e) {
        SelectClear($(this));

    });


    this.StreamsGridColumns.setDataSource(CreateColumnDataSource())
    return this;
};