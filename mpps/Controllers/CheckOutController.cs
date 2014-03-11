using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using mpps.domain;
using mpps.models;

namespace mpps.Controllers
{
    public class CheckOutController : ApiController
    {
        private readonly ICheckOutDomain _checkOutDomain;
        private readonly IProfileDomain _profileDomain;
        public CheckOutController(ICheckOutDomain checkOutDomain, IProfileDomain profileDomain)
        {
            _checkOutDomain = checkOutDomain;
            _profileDomain = profileDomain;
        }
        
        /// <summary>
        /// This method processes Authorize and AuthorizeCapture transactions.  These type of transactions require entry of credit info and implemented as a hosted solution.
        /// As response, the method returns a web page with the credit payment form.  Result of the transaction is returned to the caller via the response url.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>html mark with hidden form or an error message</returns>
         [ResponseType(typeof(string))]
        public HttpResponseMessage Post([FromBody]PaymentRequest request)
        {
            try
            {
                var profile = _profileDomain.Get(request.ProfileID);
                var responseUrl = Url.Link("DefaultApi", new { controller = "ResponseProcessor" });
                responseUrl = responseUrl + "?ProfileID=" + request.ProfileID.ToString() + "&responseUrl=" + HttpUtility.UrlEncode(request.ResponseUrl);

                var cancelUrl = Url.Link("DefaultApi", new { controller = "CancelProcessor" });
                cancelUrl = cancelUrl + "?ProfileID=" + request.ProfileID.ToString() + "&cancelUrl=" + HttpUtility.UrlEncode(request.CancelUrl);

                var content = _checkOutDomain.Authorize(profile, request.PaymentDetail, responseUrl, cancelUrl);
               return new HttpResponseMessage
               {
                   Content = new StringContent(
                       content,
                       Encoding.UTF8,
                       "text/html")
               };
            }
            catch (Exception exp)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exp);
            }
        }
       
        /// <summary>
        /// This method processes capture, void or credit transactions.  These operations are synchronous.
        /// </summary>
        /// <param name="transactionType">capture, void or credit</param>
        /// <param name="transactionID">original transaction id</param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [ResponseType(typeof(TransactionResponse))]
        public HttpResponseMessage Get(TransactionTypes transactionType, int transactionID, decimal? amount)
        {
            string htmlContent = @"
<html>
<head>
  <title></title>
  <script src='/scripts/jquery-2.1.0.min.js' type='text/javascript'></script>  
</head>
<body>
<script type='text/javascript'>
    $(function () {
        window.location.href = 'http://www.yahoo.com';
    });
</script>

</body>
";
            return new HttpResponseMessage
            {
                Content = new  StringContent(
                    htmlContent,
                    Encoding.UTF8,
                    "text/html")
            };
        }
    }
}
