using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mpps.models
{
    public enum TransactionTypes
    {
        Authorize,
        AuthorizeCatpure,
        Capture,
        Void,
        Credit
    }
}
