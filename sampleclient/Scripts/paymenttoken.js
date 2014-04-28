$(document).ready(function () {

    $("#createPaymentToken").click(function () {
        doCheckout();
    });


});

function doCheckout() {
    var mmpsUrl = 'http://devvmecom02.us.costar.local/api/PaymentToken/';
    //var mmpsUrl = 'http://localhost:51400/api/PaymentToken/';

    var contactInfo = {};
    $.each($('#contact_form').serializeArray(), function (_, kv) {
        contactInfo[kv.name] = kv.value;
    });

    checkout(1, 0, mmpsUrl, responseUrl, cancelUrl, contactInfo);
}

function checkout(profileId, transactionType, mppsUrl, ResponseUrl, CancelUrl, contactInfo) {

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
 //   $('#csPaymentForm').append('<input type="hidden" id="ReferenceNumber" name="PaymentDetail.ReferenceNumber" value="0"> ');
    $('#csPaymentForm').append('<input type="hidden" id="ResponseUrl" name="ResponseUrl"  value="' + ResponseUrl + '">');
    $('#csPaymentForm').append('<input type="hidden" id="CancelUrl" name="CancelUrl"  value="' + CancelUrl + '">');
//    $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.TaxAmount" value="' + cart.tax + '" id="TaxAmount" size="25" >');
//    $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.TotalAmount" id="TotalAmount" value="' + cart.amount + '"  size="25" >');
//    $('#csPaymentForm').append('<input type="hidden" name="PaymentDetail.Currency" value="USD" >');
    $('#csPaymentForm').append('<input type="hidden" name="ProfileID" value="' + profileId + '" >');

    $('#csPaymentForm').submit();

};