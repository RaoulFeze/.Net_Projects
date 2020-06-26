using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marigold_Application.App_Pages.City_Operations.Parks.TeamLeader
{
    public partial class DefaultTL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string teamLeaderRole = ConfigurationManager.AppSettings["teamLeaderRole"];
            if (Request.IsAuthenticated)
            {
                if (!User.IsInRole(teamLeaderRole))
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}