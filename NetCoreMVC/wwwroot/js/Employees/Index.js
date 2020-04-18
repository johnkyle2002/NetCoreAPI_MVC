var empID;
function showDeleteModal(id) {
    empID = id;
    $("#deleteModel").modal('show');
}

function deleteRecord() {
    $.post("/Employee/Delete", { id: empID }, function (result) {
        if (result.success) {
            $(".tr_" + empID).fadeOut('slow', function () {
                $(this).remove();
            });
            $("#deleteModel").modal('hide');
        }
    })
}
