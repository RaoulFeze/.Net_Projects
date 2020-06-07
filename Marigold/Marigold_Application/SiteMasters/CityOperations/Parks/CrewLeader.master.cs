using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marigold_Application.SiteMasters.CityOperations.Parks
{
    public partial class CrewLeader : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CityOperations cityOperations = Page.Master as CityOperations;
            //cityOperations.SiteMapDataSource_Provider = "CrewLeaderSiteMapDataSourceProvider";
        }
    }
}