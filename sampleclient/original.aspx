<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="original.aspx.cs" Inherits="sampleclient.original" %>

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
    <div class="sectionContent">
         <%--<h1 class="section-title">Contact address</h1>--%>
        <div>
             <form id="contact_form"  method="post">
            <label class="fieldName">First Name:</label><input type="text" name="FirstName" value="jane" size="25" /><br/>
            <label class="fieldName">Last Name:</label><input type="text" name="LastName" value="doe" size="25" /><br/>
            <label class="fieldName">City:</label><input type="text" name="City" value="Glendora" size="25" /><br/>
            <label class="fieldName">country:</label><input type="text" name="Country" value="us" size="25" /><br/>
            <label class="fieldName">Address Line 1:</label><input type="text" name="StreetLine1" value="2100 route 66" size="25" /><br/>
            <label class="fieldName">Address Line 2:</label><input type="text" name="StreetLine2" size="25" /><br/>
            <label class="fieldName">State:</label><input type="text" name="State" value="CA" size="25" /><br/>
            <label class="fieldName">Postal Code:</label><input type="text" name="PostalCode" value="91789" size="25" /><br/>
            <label class="fieldName">Email:</label><input type="text" name="EmailAddress" value="vip@loopnet.com" size="25" /><br/>         
        </form>
        </div>
            <h1 class="section-title">Purchase summary</h1>
        <div >
            <label class="fieldName" id="Label1">Premium Searcher</label><input type="text" style="float:right" id="productPrice" />
        </div>
           <button id="completePurchase">Complete Purchase</button>
       </div>
       </div>
    </li>    
    </ul>
</div>
</body>
</html>
<script>
    var responseUrl = "<%=Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + ResolveUrl("~/confirm.aspx") %>";
    var cancelUrl = "<%=Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + ResolveUrl("~/default.aspx") %>";
</script>
<script src="/Scripts/original.js" type="text/javascript">
</script>
