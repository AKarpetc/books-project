
function GetAuthorDS(Id) {

    var model = {
        id: "Id",
        fields:
        {
            Id: { type: "number" },
            Name: { type: "text", validation: { required: true } },
            Surname: { type: "text", validation: { required: true } },
            BookId: { type: "number",defaultValue: Id },
        }
    };
    var dataSource
    if (Id == 0) {
        dataSource = new kendo.data.DataSource({
            data: [],
            schema: {
                model: model,
            },
        });
    } else {
        dataSource = new kendo.data.DataSource({
            transport: {
                read:
                {
                    url: authorUrl + "/" + Id,
                    contentType: "application/json",
                    type: "GET"
                },
                create:
                {
                    url: authorUrl,
                    dataType: "json",
                    type: "POST"
                },
                destroy:
                {
                    url: authorUrl,
                    dataType: "json",
                    type: "DELETE"
                },
                update: {
                    url: authorUrl,
                    dataType: "json",
                    type: "PUT",

                },
            },
            schema: {
                model: model,
            },
        });
    }

    return dataSource;
}
function aurhorsGrid(Id) {

    var columns =
        [
            { field: "Name", title: "Имя" },
            { field: "Surname", title: "Фамилия" },
      
        ];
    if (Id != 0)
    {
        columns.push({
            command: [{
                name: "edit",
                text: {
                    edit: "",
                    update: "",
                    cancel: ""
                },
                iconClass: "k-icon k-i-edit",
            },
            {
                name: "destroy",
                text: "",
                iconClass: "k-icon k-i-close"
            },


            ]
        })

    }

    var authorsGrid = $("#authorsGrid").kendoGrid(
        {
            dataSource: GetAuthorDS(Id),
            toolbar: ["create", "destroy"],
            editable: (Id == 0 ? true : "inline"),
            selectable: true,
            change: function (e) {
                $("#authorsGridMsg").trigger("change");

            },
            columns: columns
        }).data("kendoGrid");



    $("#authorsGrid .k-grid-delete").click(function () {
        authorsGrid.removeRow(authorsGrid.select());

    });
    this.grid = authorsGrid;
    this.dataSource = authorsGrid.dataSource;
}

