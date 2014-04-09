using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mpps.models
{
    public class TransactionResponse
    {
        public string Decision { get; set; }
        public string TransactionID { get; set; }
        public decimal AuthorizedAmount { get; set; }
        public string PaymentTokenID { get; set; }
        public string ReferenceNumber { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
