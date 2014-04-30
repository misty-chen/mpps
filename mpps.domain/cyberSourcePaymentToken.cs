using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mpps.models;

namespace mpps.domain
{
    public class CyberSourcePaymentToken : IPaymentTokenDomain
    {
        public string Create(Profile profile, Contact billingAddress, string responseUrl, string cancelUrl)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("access_key", profile.Provider.ProviderSettings.First(p => p.SettingName == "password").SettingValue);
            parameters.Add("profile_id", profile.Provider.ProviderSettings.First(p => p.SettingName == "login").SettingValue);
            parameters.Add("transaction_uuid", CyberSourceCheckOutDomain.getUUID());
            parameters.Add("signed_field_names", "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,bill_to_forename,bill_to_surname,bill_to_address_city,bill_to_address_country,bill_to_address_line1,bill_to_address_line2,bill_to_address_state,bill_to_address_postal_code,bill_to_email,override_custom_receipt_page,override_custom_cancel_page,reference_number,currency");
            parameters.Add("unsigned_field_names", "");
            parameters.Add("signed_date_time", CyberSourceCheckOutDomain.getUTCDateTime());
            parameters.Add("locale", "en");

            parameters.Add("transaction_type", "create_payment_token");

            parameters.Add("reference_number", DateTime.Now.Ticks.ToString());
            //parameters.Add("amount", paymentDetail.TotalAmount.ToString("0.##"));
            //parameters.Add("tax_amount", paymentDetail.TaxAmount.ToString("0.##"));
            parameters.Add("currency", "USD");

            parameters.Add("bill_to_forename", billingAddress.FirstName);
            parameters.Add("bill_to_surname", billingAddress.LastName);
            parameters.Add("bill_to_address_city", billingAddress.City);
            parameters.Add("bill_to_address_country", billingAddress.Country);
            parameters.Add("bill_to_address_line1", billingAddress.StreetLine1);
            parameters.Add("bill_to_address_line2", billingAddress.StreetLine2);
            parameters.Add("bill_to_address_state", billingAddress.State);
            parameters.Add("bill_to_address_postal_code", billingAddress.PostalCode);
            parameters.Add("bill_to_email", billingAddress.EmailAddress);
            parameters.Add("override_custom_receipt_page", responseUrl);
            parameters.Add("override_custom_cancel_page", cancelUrl);
            // parameters.Add("payment_token", PaymentTokenID);

            StringBuilder sb = new StringBuilder();
            sb.Append("<form id=\"custom\" action=\"" + profile.Provider.ProviderSettings.First(p => p.SettingName == "tokencreateurl").SettingValue + "\" method=\"post\"/>");

            foreach (var p in parameters)
            {
                sb.Append("<input type=\"hidden\" id=\"" + p.Key + "\" name=\"" + p.Key + "\" value=\"" + p.Value + "\"/>\n");
            }
            sb.Append("<input type=\"hidden\" id=\"signature\" name=\"signature\" value=\"" + CyberSourceCheckOutDomain.Sign(parameters, profile.Provider.ProviderSettings.First(p => p.SettingName == "secret").SettingValue) + "\"/>\n");
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

        public string Update(Profile profile, string token, Contact billingAddress, string responseUrl, string cancelUrl)
        {
             IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("access_key", profile.Provider.ProviderSettings.First(p => p.SettingName == "password").SettingValue);
            parameters.Add("profile_id", profile.Provider.ProviderSettings.First(p => p.SettingName == "login").SettingValue);
            parameters.Add("transaction_uuid", CyberSourceCheckOutDomain.getUUID());
            parameters.Add("signed_field_names", "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,override_custom_receipt_page,override_custom_cancel_page,reference_number,currency,payment_token,allow_payment_token_update,bill_to_forename,bill_to_surname,bill_to_address_city,bill_to_address_country,bill_to_address_line1,bill_to_address_line2,bill_to_address_state,bill_to_address_postal_code,bill_to_email");
            parameters.Add("unsigned_field_names", "");
            parameters.Add("signed_date_time", CyberSourceCheckOutDomain.getUTCDateTime());
            parameters.Add("locale", "en");

            parameters.Add("transaction_type", "update_payment_token");

            parameters.Add("reference_number", DateTime.Now.Ticks.ToString());
      //      parameters.Add("amount", "1.00");
            parameters.Add("allow_payment_token_update", "true");
            parameters.Add("currency", "USD");

           
            parameters.Add("override_custom_receipt_page", responseUrl);
            parameters.Add("override_custom_cancel_page", cancelUrl);
            parameters.Add("payment_token", token);

            parameters.Add("bill_to_forename", billingAddress.FirstName);
            parameters.Add("bill_to_surname", billingAddress.LastName);
            parameters.Add("bill_to_address_city", billingAddress.City);
            parameters.Add("bill_to_address_country", billingAddress.Country);
            parameters.Add("bill_to_address_line1", billingAddress.StreetLine1);
            parameters.Add("bill_to_address_line2", billingAddress.StreetLine2);
            parameters.Add("bill_to_address_state", billingAddress.State);
            parameters.Add("bill_to_address_postal_code", billingAddress.PostalCode);
            parameters.Add("bill_to_email", billingAddress.EmailAddress);


            StringBuilder sb = new StringBuilder();
            sb.Append("<form id=\"custom\" action=\"" + profile.Provider.ProviderSettings.First(p => p.SettingName == "tokenupdateurl").SettingValue + "\" method=\"post\"/>");

            foreach (var p in parameters)
            {
                sb.Append("<input type=\"hidden\" id=\"" + p.Key + "\" name=\"" + p.Key + "\" value=\"" + p.Value + "\"/>\n");
            }
            sb.Append("<input type=\"hidden\" id=\"signature\" name=\"signature\" value=\"" + CyberSourceCheckOutDomain.Sign(parameters, profile.Provider.ProviderSettings.First(p => p.SettingName == "secret").SettingValue) + "\"/>\n");
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
    }

}
