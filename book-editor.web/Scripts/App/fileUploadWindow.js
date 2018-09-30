
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

function openCover(bookId, IsWithCover, callback,name)
{

    var cover;
    if (IsWithCover)
    {
        cover = [{ name: name, extension: "jpg", size: "0" }];
    }


       

    function SetCover(srs)
    {
        $("#bigCover").attr("src", srs);
        callback(bookId, srs);
    }
    function addPreview(file)
    {
        var raw = file.rawFile;
        var reader = new FileReader();

        if (raw)
        {
            reader.onloadend = function ()
            {
                SetCover(this.result);
            };
            reader.readAsDataURL(raw);
        }
    }

    $("#coverContainer").html("");
    var fileSelect;
    $('<input type="file" id="cover" name="cover" />').appendTo("#coverContainer").kendoUpload({
        async: {
            saveUrl: saveCoverUrl,
            removeUrl: removeCoverUrl + "/" + bookId,
            autoUpload: true
        },
        validation: {
            allowedExtensions: ["jpeg", "jpg", "png"],
        },
        upload: function (e) {
            e.data = { bookId: bookId };

        },
        success: function (e)
        {
           
            if (e.operation != "remove")
            {
                setTimeout(function () {
                    addPreview(fileSelect);
                });
            }
            else
            {
                SetCover(defaultCover);
            }

        },
        complete: function (e)
        {
            
        },
        select: function (e)
        {
            var fileInfo = e.files[0];
            fileSelect = fileInfo;
            
        },
        files: cover,
        dropZone: "#fileWindow",
        multiple: false,
    });

    if (IsWithCover)
    {
        $("#bigCover").attr("src", getCoverUrl + "/" + bookId)
    } else
    {
        $("#bigCover").attr("src", defaultCover)

    }

    fileWindow.center();
    fileWindow.open();
}
