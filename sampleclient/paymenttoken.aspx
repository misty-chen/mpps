<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paymenttoken.aspx.cs" Inherits="sampleclient.paymenttoken" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/Content/payment.css" rel="stylesheet" />
    <link href="Content/themes/base/jquery.ui.all.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.0.min.js"></script>
    <script src="Scripts/jquery-ui-1.10.4.min.js"></script>
    <title></title>
</head>
<body>
    <div class="sectionContent">
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
        <button id="createPaymentToken">Create Payment Token</button>
       </div>
        <br />
        <br />
         <br />
        <br />
        <form id="token_form"  method="post">
            <label class="fieldName">Payment Token:</label><input type="text" id="PaymentToken" value="" />         
        </form>    
        <button id="updatePaymentToken">Update Payment Token</button>
</body>
<script>
        var responseUrl = "<%=Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + ResolveUrl("~/confirm.aspx") %>";
        var cancelUrl = "<%=Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + ResolveUrl("~/default.aspx") %>";
</script>
<script src="/Scripts/paymenttoken.js" type="text/javascript">
</script>
</html>
