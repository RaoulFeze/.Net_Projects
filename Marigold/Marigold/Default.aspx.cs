using Marigold.Security;
using Marigold_Application.Security;
using MarigoldSystem.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marigold
{
    public partial class _Default : Page
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
    }
}