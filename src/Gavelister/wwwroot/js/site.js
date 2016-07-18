jQuery(document).ready(function () {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        $(".show-if-desktop").remove();
    } else $(".show-if-mobile").remove();

    $('.buy-button').on("click", function (e) {
        e.preventDefault();
        var giftId = $(this).attr('gift-id');
        var fieldId = $(this).attr('field');
        var amountReserved = parseInt($('#' + fieldId).val());
        $.post("gifts/buy/?id=" + giftId + "&amountReserved=" + amountReserved, function (data) {
            window.location.reload();
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
