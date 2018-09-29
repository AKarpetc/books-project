
var fileWindow = $("#fileWindow").kendoWindow({
    actions: ['Close'],
    height: 600,
    pinned: true,
    draggable: false,
    width: 568,
    visible: false,
    resizable: false,
    modal: true,
    title: "Обложка",
    close: function (e) {
    },

}).data("kendoWindow");
function openCover()
{

// var upload = $("#cover").kendoUpload({
//    async: {
//        saveUrl: saveCoverUrl,
//        removeUrl: removeCoverUrl,
//        autoUpload: true
//    },
//    validation: {
//        allowedExtensions: ["jpeg", "jpg", "png"],
//    },
//    multiple: false,
//    upload: function (e)
//    {
       
//    },
//    remove: function (e) {
//    },
//    success: function (e) {
       
//    },
//    complete: function (e) {
//    },
//    // template: function (e)
//    // {
//    //    var id = e.files[0].id;
//    //    var defaultImage = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs%3D"; //super small image
//    //    var urlDownload = '@Url.Action("GetFile", "CarWeightingClose", new { area= "CarOrders" })?fileId=' + id;
//    //    var template = '<div class="product"><button class="btn remove-photo-report btn-danger" type="button " data-id="' + id + '">' +
//    //        '<i class="fa fa-remove"></i></button><a  href="' + urlDownload + '"><img class="image-preview" src="' + (isDef(id) ? urlDownload : defaultImage) + '"></img></a></div>';
//    //    return template;
//    //}
//}).data('kendoUpload');

    $("#cover").kendoUpload({
        async: {
            saveUrl: saveCoverUrl,
            removeUrl: removeCoverUrl,
            autoUpload: true
        },
        dropZone: "#fileWindow",
        multiple: false,
    });
    fileWindow.center();
    fileWindow.open();
}
openCover();