using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mpps.models;

namespace mpps.domain
{
    public interface IPaymentTokenDomain
    {
        string Create(Profile profile, Contact billingAddress, string responseUrl, string cancelUrl);
        string Update(Profile profile, string token, Contact billingAddress, string responseUrl, string cancelUrl);
    }
}
