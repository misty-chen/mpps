$(document).ready(function () {

    $('#panelBar .accordion-header .k-icon').each(function () {
        var accordionHeader = $(this).parent().parent();
        $(this).click(function () {
            toggleAccordion(accordionHeader);
        });
    });
    $(".terms").click(function () {
        computeAmount();
    });
    $("#continue").click (function() {
        hideAccordion($("#selectProduct"));
        showAccordion($("#purchase"));
    });
    
    //$("#oneClick").click(function (e) {
    //    $("#paymentTokenDialog").dialog("open");
    //   // e.preventDefault();
    //});

    //$("#paymentTokenDialog").dialog({
    //    autoOpen: false,
    //    height: 300,
    //    width: 350,
    //    modal: true,
    //    buttons: {
    //        Ok: function () {
    //            if ($("#PaymentTokenID").val().length <= 0) {
    //                alert("PaymentTokenID is required");
    //            }
    //            else {
    //                $(this).dialog("close");
    //                $("#payment_form").submit();
    //            }
    //        },
    //        Cancel: function () {
    //            $(this).dialog("close");
    //        }
    //    }        
    //});

    showAccordion($("#selectProduct"));
    computeAmount();
});
function showAccordion(accordion) {
    if (isAccordionHidden(accordion)) {
        toggleAccordion(accordion);
    }
};

function isAccordionHidden(accordion) {
    return $(accordion).children('.accordion-content').is(":hidden");
};

function toggleAccordion(accordion) {
    if ($(accordion).children('.accordion-content').is(":visible")) {
        $(accordion).children('span').removeClass('k-minus');
        $(accordion).children('span').addClass('k-plus');
    }
    else {
        $(accordion).children('span').removeClass('k-plus');
        $(accordion).children('span').addClass('k-minus');
    }
    $(accordion).children('.accordion-content').slideToggle('fast');
};

function showAccordion(accordion) {
    $(accordion).children('.accordion-content').slideDown('fast');
};
function hideAccordion(accordion) {
    $(accordion).children('.accordion-content').slideUp('fast');
};

function computeAmount() {
    var amount = 0;
    $(".terms").each(function (idx, el) {
        if (el.checked) {
            amount = new  Number( el.value);
        }
    });

    var tax = Math.round((amount * 7 / 100) * 100) / 100;
    var total = amount + tax;

    $("#TaxAmount").val(tax);
    $("#TotalAmount").val(total);
}