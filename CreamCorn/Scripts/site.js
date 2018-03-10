// Modal support

$(function () {
    $.ajaxSetup({ cache: false });
    $("a[data-modal]").on("click", function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                keyboard: true
            }, 'show');

            bindForm(this);
        });
        return false;
    });


});

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $('#progress').show();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#progress').hide();
                    location.reload();
                } else {
                    $('#progress').hide();
                    $('#myModalContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}

// Modal support


// Delete functions

function deleteCompany(id, name) {

	if (!confirm('Are you sure you wish to delete "' + name + '"?'))
        return;

    // find the data div, remove its row.
	var divData = $("#c-" + id);
	divData.closest("tr").remove();

    var form = $('#__AjaxAntiForgeryForm');
	var token = $('input[name="__RequestVerificationToken"]', form).val();

    var options = {};
    options.url = '/company/delete';
    options.type = "POST";
    options.data = { __RequestVerificationToken: token, Id: id, Name: name };
    options.success = function (data) {
        $('#actionMessage').css('visibility', 'visible');
        $('#actionMessageText').text(data.message);
    };
    options.error = function (XMLHttpRequest, textStatus, errorThrown) {
		$('#actionMessage').css('visibility', 'visible');
		$('#actionMessageText').text("There was a problem deleting this company: " + errorThrown);
    };
    $.ajax(options);

}


// Delete functions
