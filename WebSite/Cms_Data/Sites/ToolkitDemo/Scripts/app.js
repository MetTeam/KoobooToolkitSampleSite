$(function () {
    //Fancybox
    $('#login-link').fancybox({
        showCloseButton: false,
        overlayColor: '#000',
        opacity: 0.3,
        autoDimensions: false,
        height: 260
    });

    $('.ajaxForm').each(function () {
        var form = $(this);
        form.ajaxForm({
            sync: true,
            beforeSerialize: function ($form, options) {
            },
            beforeSend: function () {
                var form = $(this);
                form.find("[type=submit]").addClass("disabled").attr("disabled", true);
            },
            beforeSubmit: function (arr, $form, options) {
            },
            success: function (responseData, statusText, xhr, $form) {
                form.find("[type=submit]").removeClass("disabled").removeAttr("disabled");
                if (!responseData.Success) {
                    var validator = form.validate();
                    //                            var errors = [];
                    for (var i = 0; i < responseData.FieldErrors.length; i++) {
                        var obj = {};
                        var fieldName = responseData.FieldErrors[i].FieldName;
                        if (fieldName == "") {
                            alert(responseData.FieldErrors[i].ErrorMessage);
                        }
                        obj[fieldName] = responseData.FieldErrors[i].ErrorMessage;
                        validator.showErrors(obj);
                    }
                } else {
                    if (responseData.RedirectUrl != null) {
                        location.href = responseData.RedirectUrl;
                    } else {
                        location.reload();
                    }

                }
            },
            error: function () {
                var form = $(this);
                form.find("[type=submit]").removeClass('disabled').removeAttr('disabled');
            }
        });
    });
});
//#region Comment Page
function AddComment(response) {
    var responseData = $.parseJSON(response.responseText);
    if (responseData.Success) {
        var $model = $(responseData.Model);
        if ($(".item:last").length) {
            //append added comment to comment list
            $model.insertAfter(".item:last");
        } else {
            $model.insertAfter("#comments h6.title:first");
        }
        document.getElementById("comment-form").reset();
        //InlineEditor
        if (window.yardi) {
            window.yardi.initInlineEditing();
        }
    } else {
        var errorMsg = responseData.Messages.join("<br/>");
        alert(errorMsg);
    }
}

function DeleteComment(response) {
    var json = $.parseJSON(response.responseText);
    var msg = json.Messages.join("<br/>");
    if (json.Success) {
        //remove item from comment list
        $(".item a.delete[href$=" + json.Model + "]").parent(".item").remove();
    } else {
        alert(msg);
    }
}
//#endregion
//#region Contact Page
function SubmitContact(response) {
    var json = $.parseJSON(response.responseText);
    if (!!json.Messages) {
        var msg = json.Messages.join("<br/>");
        alert(msg);
    }
    if (json.ReloadPage) {
        window.location.reload();
    } else {
        document.getElementsByTagName("form")[0].reset();
    }
}
//#endregion