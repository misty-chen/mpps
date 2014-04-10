using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using mpps.models;

namespace mpps.domain
{
    public class CyberSourceCheckOutDomain : ICheckOutDomain
    {
        public string Authorize(Profile profile, PaymentDetail paymentDetail, string responseUrl, string cancelUrl)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("access_key", profile.Provider.ProviderPassword);
            parameters.Add("profile_id", profile.Provider.ProviderLogin);
            parameters.Add("transaction_uuid", getUUID());
            parameters.Add("signed_field_names", "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,tax_amount,currency,bill_to_forename,bill_to_surname,bill_to_address_city,bill_to_address_country,bill_to_address_line1,bill_to_address_line2,bill_to_address_state,bill_to_address_postal_code,bill_to_email,override_custom_receipt_page,override_custom_cancel_page,payment_token");
            parameters.Add("unsigned_field_names", "");
            parameters.Add("signed_date_time", getUTCDateTime());
            parameters.Add("locale", "en");

            if (string.IsNullOrWhiteSpace(paymentDetail.PaymentTokenID))
            {
                parameters.Add("transaction_type", "authorization,create_payment_token");
            }
            else
            {
                parameters.Add("transaction_type", "authorization");
            }
            parameters.Add("reference_number", paymentDetail.ReferenceNumber);
            parameters.Add("amount", paymentDetail.TotalAmount.ToString("0.##"));
            parameters.Add("tax_amount", paymentDetail.TaxAmount.ToString("0.##"));
            parameters.Add("currency", paymentDetail.Currency);

            parameters.Add("bill_to_forename", paymentDetail.BillingAddress.FirstName);
            parameters.Add("bill_to_surname", paymentDetail.BillingAddress.LastName);
            parameters.Add("bill_to_address_city", paymentDetail.BillingAddress.City);
            parameters.Add("bill_to_address_country", paymentDetail.BillingAddress.Country);
            parameters.Add("bill_to_address_line1", paymentDetail.BillingAddress.StreetLine1);
            parameters.Add("bill_to_address_line2", paymentDetail.BillingAddress.StreetLine2);
            parameters.Add("bill_to_address_state", paymentDetail.BillingAddress.State);
            parameters.Add("bill_to_address_postal_code", paymentDetail.BillingAddress.PostalCode);
            parameters.Add("bill_to_email", paymentDetail.BillingAddress.EmailAddress);
            parameters.Add("override_custom_receipt_page", responseUrl);
            parameters.Add("override_custom_cancel_page", cancelUrl);
            parameters.Add("payment_token", paymentDetail.PaymentTokenID);

            StringBuilder sb = new StringBuilder();
            sb.Append("<form id=\"custom\" action=\"" + profile.Provider.ServiceUrl + "\" method=\"post\"/>");

            foreach (var p in parameters)
            {
                sb.Append("<input type=\"hidden\" id=\"" + p.Key + "\" name=\"" + p.Key + "\" value=\"" + p.Value + "\"/>\n");
            }
            sb.Append("<input type=\"hidden\" id=\"signature\" name=\"signature\" value=\"" + Sign(parameters, profile.Provider.ProviderSecret) + "\"/>\n");
            sb.Append("</form>");

            string htmlContent = @"
<html>
<head>
  <title></title>
  <script src='/scripts/jquery-2.1.0.min.js' type='text/javascript'></script>  
</head>
<body>" + sb.ToString() +
@"<script type='text/javascript'>
    $(function () {
        $('#custom').submit();
    });
</script>
</body>
</html>
";
            return htmlContent;
        }

        public string ProcessResponse(int profileID, string responseUrl, NameValueCollection values)
        {
            if (String.IsNullOrWhiteSpace(responseUrl))
            {
                return ProcessResponse(profileID, values);
            }

            var item = ProcessResponse(values);
            var properties = item.GetType().GetProperties();

            StringBuilder sb = new StringBuilder();
            sb.Append("<form id=\"custom\" action=\"" + responseUrl + "\" method=\"post\"/>");

            foreach (var p in properties)
            {
                sb.Append("<input type=\"hidden\" id=\"" + p.Name + "\" name=\"" + p.Name + "\" value=\"" + p.GetValue(item) + "\"/>\n");
            }
            sb.Append("</form>");

            string htmlContent = @"
<html>
<head>
  <title></title>
  <script src='/scripts/jquery-2.1.0.min.js' type='text/javascript'></script>  
</head>
<body>" + sb.ToString() +
@"<script type='text/javascript'>
    $(function () {
        $('#custom').submit();
    });
</script>
</body>
</html>
";
            return htmlContent;

        }

        public string ProcessCancel(int profileID, string cancelUrl, NameValueCollection values)
        {
            if (String.IsNullOrWhiteSpace(cancelUrl))
            {
                return ProcessCancel(profileID, values);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<form id=\"custom\" action=\"" + cancelUrl + "\" method=\"post\"/>");


            sb.Append("</form>");

            string htmlContent = @"
<html>
<head>
  <title></title>
  <script src='/scripts/jquery-2.1.0.min.js' type='text/javascript'></script>  
</head>
<body>" + sb.ToString() +
@"<script type='text/javascript'>
    $(function () {
        $('#custom').submit();
    });
</script>
</body>
</html>
";
            return htmlContent;

        }

        private string ProcessResponse(int profileID, NameValueCollection values)
        {
            var item = ProcessResponse(values);
            
            StringBuilder sb = new StringBuilder();
            string htmlContent = @"
<html>
<head>
    <title></title>
    <script src='/scripts/jquery-2.1.0.min.js' type='text/javascript'></script>  
</head>
<body>" + sb.ToString() +
@"<script type='text/javascript'>
    $(function () {
         window.parent.window.postMessage({'type':'response', 'response':" + item.ToJSON() +  @"},'*');
    });
</script>
</body>
</html>
";
            return htmlContent;

        }
        private string ProcessCancel(int profileID,  NameValueCollection values)
        {
            StringBuilder sb = new StringBuilder();
            string htmlContent = @"
<html>
<head>
    <title></title>
    <script src='/scripts/jquery-2.1.0.min.js' type='text/javascript'></script>  
</head>
<body>" + sb.ToString() +
@"<script type='text/javascript'>
    $(function () {
         window.parent.window.postMessage({'type':'cancel'},'*');
    });
</script>
</body>
</html>
";
            return htmlContent;

        }


        private TransactionResponse ProcessResponse(NameValueCollection values)
        {
            var item = new TransactionResponse();
            item.Decision = values["decision"];
            item.AuthorizedAmount = decimal.Parse( values["auth_amount"]);
            item.PaymentTokenID = values["payment_token"];
            item.TransactionID = values["transaction_id"];
            item.ReferenceNumber = values["req_reference_number"];
            item.ReasonCode = values["reason_code"];
            item.Message = values["message"];            
            return item;
        }

        private static String Sign(IDictionary<string, string> paramsArray, String secretKey)
        {
            return Sign(BuildDataToSign(paramsArray), secretKey);
        }

        private static String Sign(String data, String secretKey)
        {
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        private static String BuildDataToSign(IDictionary<string, string> paramsArray)
        {
            String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

            foreach (String signedFieldName in signedFieldNames)
            {
                dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
            }

            return CommaSeparate(dataToSign);
        }

        private static String CommaSeparate(IList<string> dataToSign)
        {
            return String.Join(",", dataToSign);
        }
        private static String getUUID()
        {
            return System.Guid.NewGuid().ToString();
        }

        private static String getUTCDateTime()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            return time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }
    }
}
