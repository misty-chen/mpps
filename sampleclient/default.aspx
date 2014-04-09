<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="/Content/payment.css" rel="stylesheet" />
    <link href="Content/themes/base/jquery.ui.all.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.0.min.js"></script>
    <script src="Scripts/jquery-ui-1.10.4.min.js"></script>
    <title></title>
</head>
<body>
    <div class="content" >
    <div>
        <a class="logo"></a>
        <a style="color:#929292;">#1 in Commercial Real Estate Online</a>
    </div>
    <ul id="panelBar" class="t-accordion">
    <li id="selectProduct">
        <div class="accordion-header">
        <span class="k-icon k-plus"><span class="circle">1</span><span>Select your product and subscription terms</span></span>
        </div>
        <div class="accordion-content" style="display:none">
        <div class="sectionContent">
         <%--<h1 class="section-title">Product</h1>--%>
            <div><input type="radio" name="Product" id="premiumSearcher" checked="checked" value="99"/>Premium Searcher - starting at $64.95/month</div>
            <div><input type="radio" name="Product" id="platinumSearcher" value="99"/>Platinum Searcher - starting at $179.95/month</div>
         <h1 class="section-title">Subscription terms</h1>
             <div><input type="radio" name="Terms" class="terms" checked="checked" value="64.95"/>Annual Subscription - $64.95/month</div>
            <div><input type="radio" name="Terms" class="terms"" value="79.95"/>Quarterly Subscription - $79.95/month</div>
            <div><input type="radio" name="Terms" class="terms" value="89.95"/>Monthly Subscription - $89.95/month</div>
        
       <%-- <input type="button" value="Continue" id="continue" />--%>
        </div>
        </div>
    </li>        
        <li id="purchase">
        <div class="accordion-header">
        <span class="k-icon k-plus"><span class="circle">2</span><span>Enter your contact information</span></span>
        </div>
    <div class="accordion-content" style="display:none">
    <form id="contact_form" action="http://localhost:51400/api/CheckOut/" method="post" -->
    <!--form id="contact_form" action="http://devvmecom02.us.costar.local/api/CheckOut/" method="post" class="payment_form" -->
    <%--<input type="hidden" name="ProfileID" value="1">
    <input type="hidden" name="TranactionType" value="0">
    <input type="hidden" id="ReferenceNumber" name="PaymentDetail.ReferenceNumber" value="0">
    <input type="hidden" id="ResponseUrl" name="ResponseUrl"  value="<%=Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + ResolveUrl("~/confirm.aspx") %>">    
    <input type="hidden" id="CancelUrl" name="CancelUrl"  value="<%=Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + ResolveUrl("~/default.aspx") %>">    --%>
        <div class="sectionContent">
         <%--<h1 class="section-title">Contact address</h1>--%>
        <div>
            <label class="fieldName">First Name:</label><input type="text" name="FirstName" value="jane" size="25" /><br/>
            <label class="fieldName">Last Name:</label><input type="text" name="LastName" value="doe" size="25" /><br/>
            <label class="fieldName">City:</label><input type="text" name="City" value="Glendora" size="25" /><br/>
            <label class="fieldName">country:</label><input type="text" name="Country" value="us" size="25" /><br/>
            <label class="fieldName">Address Line 1:</label><input type="text" name="StreetLine1" value="2100 route 66" size="25" /><br/>
            <label class="fieldName">Address Line 2:</label><input type="text" name="StreetLine2" size="25" /><br/>
            <label class="fieldName">State:</label><input type="text" name="State" value="CA" size="25" /><br/>
            <label class="fieldName">Postal Code:</label><input type="text" name="PostalCode" value="91789" size="25" /><br/>
            <label class="fieldName">Email:</label><input type="text" name="EmailAddress" value="vip@loopnet.com" size="25" /><br/>         
        </div>
        <%--<h1 class="section-title">Purchase summary</h1>
        <div >
            <label class="fieldName">Tax amount:</label><input type="text" name="PaymentDetail.TaxAmount" value="7.50" id="TaxAmount" readonly="readonly" size="25" /><br/>            
            <label class="fieldName">Total amount:</label><input type="text" name="PaymentDetail.TotalAmount" id="TotalAmount" value="100.00" readonly="readonly" size="25" /><br/>
            <input type="hidden" name="PaymentDetail.Currency" value="USD" /><br/>
            <label class="fieldName">Payment Token:</label><input id="PaymentTokenID" name="PaymentDetail.PaymentTokenID" value="" /><br/>
        </div>--%>
      <%-- <div>
           <input type="submit" id="submit" name="submit" value="Complete Purchase"/>
       </div>--%>
        </div>
    </!--form>
    </div>
    </li>
    <div class="accordion-content" style="display:none">
    </div>
    <li id="Checkout">
        <div class="accordion-header">
        <span class="k-icon k-plus"><span class="circle">3</span><span>Complete your payment</span></span>
        </div>
        <div class="accordion-content" style="display:none">
            <h1 class="section-title">Purchase summary</h1>
        <div >
            <label class="fieldName" id="productName">Premium Searcher</label><input type="text" style="float:right" id="productPrice" />
        </div>
            <h1 class="section-title">Payment Details</h1>
            <div id="payment_page" >
            </div>
        </div>
    </li> 
    </ul>
</div>
</body>
</html>
<script>
    var responseUrl = "<%=Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + ResolveUrl("~/response.aspx") %>";  
    var cancelUrl = "<%=Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + ResolveUrl("~/default.aspx") %>";
</script>
<script src="/Scripts/default.js" type="text/javascript">
</script>
<script src="http://devvmecom02.us.costar.local/scripts/mppsclient.js" type="text/javascript">
</script>
