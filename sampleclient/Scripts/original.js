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
    $("#continue").click(function () {
        hideAccordion($("#selectProduct"));
        showAccordion($("#purchase"));
    });

  
    $("#completePurchase").click(function () {
        doCheckout();
    });
   

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
            amount = new Number(el.value);
        }
    });

    var tax = Math.round((amount * 7 / 100) * 100) / 100;
    var total = amount + tax;

    $("#productPrice").val(amount);
}

function doCheckout() {
    var mmpsUrl = 'http://devvmecom02.us.costar.local/api/CheckOut/';
    //var mmpsUrl = 'http://localhost:51400/api/CheckOut/';
    var amount = new Number($("#productPrice").val());
    var tax = Math.round((amount * 7 / 100) * 100) / 100;

    var contactInfo = {};
    $.each($('#contact_form').serializeArray(), function (_, kv) {
        contactInfo[kv.name] = kv.value;
    });
    var referenceNumber = new Date().getMilliseconds();

    checkout(1, 0, referenceNumber, mmpsUrl, responseUrl, cancelUrl, contactInfo, { amount: amount + tax, tax: tax });
}

function checkout(profileId, transactionType, referenceId, mppsUrl, ResponseUrl, CancelUrl, contactInfo, cart) {

    if (!$('#csPaymentForm').length) {
        $('body').append('<form id="csPaymentForm" method="post" action="' + mppsUrl + '">');
    } else {
        $('#csPaymentForm').empty();
    }

     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.BillingAddress.FirstName" value="' + contactInfo.FirstName + '" size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.BillingAddress.LastName" value="' + contactInfo.LastName + '" size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.BillingAddress.City" value="' + contactInfo.City + '" size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.BillingAddress.Country" value="' + contactInfo.Country + '" size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.BillingAddress.StreetLine1" value="' + contactInfo.StreetLine1 + '" size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.BillingAddress.StreetLine2" value="' + contactInfo.StreetLine2 + '" size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.BillingAddress.State" value="' + contactInfo.State + '" size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.BillingAddress.PostalCode" value="' + contactInfo.PostalCode + '" size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.BillingAddress.EmailAddress" value="' + contactInfo.EmailAddress + '" size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="TranactionType" value="0">');
     $('#csPaymentForm').append('<input type="hidden" id="ReferenceNumber" name="PaymentDetail.ReferenceNumber" value="0"> ');
     $('#csPaymentForm').append('<input type="hidden" id="ResponseUrl" name="ResponseUrl"  value="' + ResponseUrl + '">');
     $('#csPaymentForm').append('<input type="hidden" id="CancelUrl" name="CancelUrl"  value="' + CancelUrl + '">');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.TaxAmount" value="' + cart.tax + '" id="TaxAmount" size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.TotalAmount" id="TotalAmount" value="' + cart.amount + '"  size="25" >');
     $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.Currency" value="USD" >');
     $('#csPaymentForm').append('<input type="hidden" name="ProfileID" value="' + profileId + '" >');

     $('#csPaymentForm').submit();

};