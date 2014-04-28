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
    public class PaymentTokenController : ApiController
    {
        private readonly IPaymentTokenDomain _paymentTokenDomain;
        private readonly IProfileDomain _profileDomain;
        public PaymentTokenController(IPaymentTokenDomain paymentTokenDomain, IProfileDomain profileDomain)
        {
            _paymentTokenDomain = paymentTokenDomain;
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

                var content = _paymentTokenDomain.Create(profile, request.PaymentDetail.BillingAddress, responseUrl, cancelUrl);
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

    }
}
