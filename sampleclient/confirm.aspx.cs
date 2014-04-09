using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sampleclient
{
    public partial class confirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StringBuilder sb = new StringBuilder();

                if (Request.Form["decision"] == "ACCEPT")
                {
                    sb.Append("<h3>Thank you for your order</h3>");
                    sb.Append("<ul>");
                    sb.Append(string.Format("<li>Authorized Amount: {0:c}</li>", decimal.Parse(Request.Form["AuthorizedAmount"])));
                    sb.Append(string.Format("<li>Transaction ID: {0:c}</li>", Request.Form["TransactionID"]));
                    sb.Append(string.Format("<li>Reference Number: {0}</li>", Request.Form["ReferenceNumber"]));
                    sb.Append(string.Format("<li>PaymentTokenID {0}</li>", Request.Form["PaymentTokenID"]));
                    sb.Append("</ul>");
                }
                else
                {
                    sb.Append("<h1>Sorry,  it failed to process your payment<.h>");
                }

                PlaceHolder.Text = sb.ToString();
            }
        }
    }
}