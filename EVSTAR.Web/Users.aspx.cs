using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Configuration;

namespace EVSTAR.Web
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "User Maintenance";
        }

        protected void btnMyClaims_Click(object sender, EventArgs e)
        {
            Response.Redirect("Claims.aspx");
        }
    }
}