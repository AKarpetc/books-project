
kendo.ui.Grid.prototype.PagerExtention = function (toolbarTemplateId = null) {
    var href = location.href.replace("#", "");

    var localStorageIndex = href + "_" + this.element[0].id + '_grid-pager-dropdownlist-saver';
    var countPages = localStorage.getItem(localStorageIndex);

    var opt = this.getOptions();
    opt.pageable = { pageSizes: [5, 10, 12, 15, 18, 20, 25] };

    if (toolbarTemplateId) {
        opt.toolbar = [
            { template: $('#' + toolbarTemplateId).html() }
        ]
    }

    this.setOptions(opt);

    this.dataSource.pageSize((countPages == null ? 10 : countPages));
    var pageSizeDropDownList = this.wrapper.children(".k-grid-pager").find("select").data("kendoDropDownList");
    pageSizeDropDownList.bind("change", function (e) {
        var pageSize = e.sender.value();
        localStorage.setItem(localStorageIndex, pageSize);
        console.log(pageSize);
    });

    return this;
}