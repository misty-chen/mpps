$(document).ready(function () {

    $('#panelBar .accordion-header .k-icon').each(function () {
        var accordionHeader = $(this).parent().parent();
        $(this).click(function () {
            toggleAccordion(accordionHeader);            
        });
    });
    $(".terms").click(function () {
        computeAmount();
        doCheckout();
    });
    $("#continue").click (function() {
        hideAccordion($("#selectProduct"));
        showAccordion($("#purchase"));
    });
    
    $("#contact_form  input").change(function () {
        doCheckout();
    });

    $("#productPrice").change(function () {
        doCheckout();
    });
    if (window.addEventListener) {
        window.addEventListener("message", receive, false);
    }
    else {
        if (window.attachEvent) {
            window.attachEvent("onmessage", receive, false);
        }
    }

    function receive(event) {
        var data = event.data;
        if (data.type === 'cancel') {
            window.location = '/default.aspx';
        } else if (data.type === 'response') {
            if (!$('#responseForm').length) {
                $('body').append('<form id="responseForm" method="post" action="/confirm.aspx">');
            } else {
                $('#responseForm').empty();
            }
            $('#responseForm').append('<input type="hidden" name="AuthorizedAmount" value="' + data.response.AuthorizedAmount + '">');
            $('#responseForm').append('<input type="hidden" name="TransactionID" value="' + data.response.TransactionID + '">');
            $('#responseForm').append('<input type="hidden" name="ReferenceNumber" value="' + data.response.ReferenceNumber + '">');
            $('#responseForm').append('<input type="hidden" name="PaymentTokenID" value="' + data.response.PaymentTokenID + '">');
            $('#responseForm').append('<input type="hidden" name="decision" value="' + data.response.Decision + '">');
            $('#responseForm').append('<input type="hidden" name="ReasonCode" value="' + data.response.ReasonCode + '">');
            $('#responseForm').append('<input type="hidden" name="Message" value="' + data.response.Message + '">');
            $('#responseForm').submit();
        }
    }

    showAccordion($("#selectProduct"));
    computeAmount();
    doCheckout();
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
    $("#productPrice").val(amount);
    $("#TotalAmount").val("3000.00 ");
}

function doCheckout() {
    //var mmpsUrl = 'http://devvmecom02.us.costar.local/api/CheckOut/';
    var mmpsUrl = 'http://localhost:51400/api/CheckOut/';
    var amount = new Number( $("#productPrice").val());
    var tax = Math.round((amount * 7 / 100) * 100) / 100;

    var contactInfo = {};
    $.each($('#contact_form').serializeArray(), function (_, kv) {
        contactInfo[kv.name] = kv.value;
    });
    var referenceNumber = new Date().getMilliseconds();

    mpps.checkout(1, 0, referenceNumber, mmpsUrl, contactInfo, { amount: amount+tax, tax: tax });
}