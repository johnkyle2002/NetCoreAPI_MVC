var trID;
function showDeleteModal(id) {
    trID = id;
    $("#deleteModel").modal('show');
}

function deleteRecord() {
    $.post("/TimeRecord/Delete", { id: trID }, function (result) {
        if (result.success) {
            $(".tr_" + trID).fadeOut('slow', function () {
                $(this).remove();
            });
            $("#deleteModel").modal('hide');
        }
    })
} 
$(document).ready(function () {
    $("table").DataTable();
});
