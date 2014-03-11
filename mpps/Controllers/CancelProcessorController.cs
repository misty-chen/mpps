using System;
using System.Collections.Generic;
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
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CancelProcessorController : ApiController
    {
     private readonly ICheckOutDomain _checkOutDomain;
     public CancelProcessorController(ICheckOutDomain checkOutDomain)
        {
            _checkOutDomain = checkOutDomain;
        }

        
        public HttpResponseMessage Post(int profileID, string cancelUrl, [FromBody]FormDataCollection formValues)
        {
            try
            {
                var content = _checkOutDomain.ProcessCancel(profileID, cancelUrl, formValues.ReadAsNameValueCollection());
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