

$(document).on('click', '.tr-remove', function () {
    // alert("You clicked the element with and ID of 'test-element'");
    $('#ContentPlaceHolder1_ContentPlaceHolderBody_hdnDociId').val($(this).parent().parent('tr').find('td:first').html());
    $('#tr-remove').modal('show');
    return false;
});