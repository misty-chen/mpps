using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mpps.models;

namespace mpps.domain
{
    public interface ICheckOutDomain
    {
        string Authorize(Profile profile, PaymentDetail paymentDetail, string responseUrl, string cancelUrl);
        string ProcessResponse(int profileID, string reponseUrl, NameValueCollection values);
        string ProcessCancel(int profileID, string reponseUrl, NameValueCollection values);
    }
}
