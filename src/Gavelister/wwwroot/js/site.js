jQuery(document).ready(function () {
    $('.buy-button').on("click", function () {
        var amountReserved = $("#amountReserved").val();
        $.post("gifts/buy/?id=3&amountReserved=" + amountReserved, function (data) {
            $(".result").html(data);
        });
    });
    
    $('.qtyplus').click(function (e) {
        e.preventDefault();
        var fieldId = $(this).attr('field');
        var maxNumber = $(this).attr('max-number');

        var currentVal = parseInt($('#' + fieldId).val());
        if (currentVal >= maxNumber)
            return;
        if (!isNaN(currentVal)) {
            $('#' + fieldId).val(currentVal + 1);
        } else {
            $('#' + fieldId).val(0);
        }
    });
    $(".qtyminus").click(function (e) {
        e.preventDefault();
        var fieldId = $(this).attr('field');
        var currentVal = parseInt($('#' + fieldId).val());
        if (!isNaN(currentVal) && currentVal > 0) {
            $('#' + fieldId).val(currentVal - 1);
        } else {
            $('#' + fieldId).val(0);
        }
    });
});
