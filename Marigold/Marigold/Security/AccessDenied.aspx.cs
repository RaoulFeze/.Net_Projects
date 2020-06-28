using Marigold.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marigold_Application.Security
{
    public partial class AccessDenied : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SecurityController securityManager = new SecurityController();
            string role = securityManager.GetCurrentUserRole(Context.User.Identity.Name);
            switch (role)
            {
                case "Staff":
                    break;
                case "Crew Leader":
                    Response.Redirect("~/App_Pages/City_Operations/Parks/CrewLeader/Crews.aspx");
                    break;
                case "Team Leader":
                    Response.Redirect("~/App_Pages/City_Operations/Parks/TeamLeader/DefaultTL.aspx");
                    break;
                default:
                    Response.Redirect("~/Security/AccessDenied.aspx");
                    break;
            }
        }

        protected void GoTo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}