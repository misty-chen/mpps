using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using mpps.domain;

namespace mpps.Controllers
{
    [ApiExplorerSettings(IgnoreApi=true)]
    public class ResponseProcessorController : ApiController
    {
        private readonly ICheckOutDomain _checkOutDomain;
        public ResponseProcessorController(ICheckOutDomain checkOutDomain)
        {
            _checkOutDomain = checkOutDomain;
        }

        
        public HttpResponseMessage Post(int profileID, string responseUrl, [FromBody]FormDataCollection formValues)
        {
            try
            {
                var content = _checkOutDomain.ProcessResponse(profileID, responseUrl, formValues.ReadAsNameValueCollection());
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
