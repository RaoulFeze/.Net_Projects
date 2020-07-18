using Marigold.Security;
using MarigoldSystem.BLL;
using MarigoldSystem.Data.POCO_s;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marigold.App_Pages.City_Operations.Parks.CrewLeader
{
    public partial class Routes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string crewLeaderRole = ConfigurationManager.AppSettings["crewLeaderRole"];
            if (Request.IsAuthenticated)
            {
                if (!User.IsInRole(crewLeaderRole))
                {
                    Response.Redirect("~/Login.aspx");
                }

                InfoUserControl.TryRun(() =>
                {
                    SecurityController security = new SecurityController();
                    UserID.Text = (security.GetCurrentUserId(Context.User.Identity.GetUserName())).ToString();
                    EmployeeController employee = new EmployeeController();
                    YardID.Text = employee.GetYardID(int.Parse(UserID.Text)).ToString();
                });
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            RouteController routeManager = new RouteController();
            int siteTypeId = int.Parse(SiteTypeID.Text);
            int yardId = int.Parse(YardID.Text);
            int taskId = int.Parse(TaskID.Text);
            switch (siteTypeId)
            {
                //A Routes
                case 1:
                    InfoUserControl.TryRun(() =>
                    {
                        List<RouteStatus> routeStatuses = routeManager.Get_AB_Routes(yardId, siteTypeId, taskId);
                        RoutesGridView.DataSource = routeStatuses;
                        RoutesGridView.DataBind();
                    });

                    RoutesGridView.Columns[5].Visible = false;  //Count
                    RoutesGridView.Columns[7].Visible = true;  //Cycle1
                    RoutesGridView.Columns[4].Visible = true;  //Cycle2
                    RoutesGridView.Columns[9].Visible = true;  //Cycle3
                    RoutesGridView.Columns[10].Visible = true; //Cycle4
                    RoutesGridView.Columns[11].Visible = true; //Cycle5
                    RoutesGridView.Columns[12].Visible = true; //Pruning
                    RoutesGridView.Columns[13].Visible = true; //Mulching
                    RoutesGridView.Columns[14].Visible = false; //Planting
                    RoutesGridView.Columns[15].Visible = false; //Uprooting
                    RoutesGridView.Columns[16].Visible = false; //Trimming
                    RoutesGridView.Columns[17].Visible = false; //Watering
                    break;

                //B Routes
                case 2:

                    InfoUserControl.TryRun(() =>
                    {
                        List<RouteStatus> routeStatuses = routeManager.Get_AB_Routes(yardId, siteTypeId, taskId);
                        RoutesGridView.DataSource = routeStatuses;
                        RoutesGridView.DataBind();
                    });

                    RoutesGridView.Columns[5].Visible = false;  //Count
                    RoutesGridView.Columns[7].Visible = true;  //Cycle1
                    RoutesGridView.Columns[4].Visible = true;  //Cycle2
                    RoutesGridView.Columns[9].Visible = false;  //Cycle3
                    RoutesGridView.Columns[10].Visible = false; //Cycle4
                    RoutesGridView.Columns[11].Visible = false; //Cycle5
                    RoutesGridView.Columns[12].Visible = true; //Pruning
                    RoutesGridView.Columns[13].Visible = true; //Mulching
                    RoutesGridView.Columns[14].Visible = false; //Planting
                    RoutesGridView.Columns[15].Visible = false; //Uprooting
                    RoutesGridView.Columns[16].Visible = false; //Trimming
                    RoutesGridView.Columns[17].Visible = false; //Watering
                    break;

                //Grass Routes
                case 3:
                    InfoUserControl.TryRun(() =>
                    {
                        List<RouteStatus> GrassList = routeManager.GrassRouteList(yardId);
                        RoutesGridView.DataSource = GrassList;
                        RoutesGridView.DataBind();
                    });
                    RoutesGridView.Columns[5].Visible = true;  //Count
                    RoutesGridView.Columns[7].Visible = false;  //Cycle1
                    RoutesGridView.Columns[4].Visible = false;  //Cycle2
                    RoutesGridView.Columns[9].Visible = false;  //Cycle3
                    RoutesGridView.Columns[10].Visible = false; //Cycle4
                    RoutesGridView.Columns[11].Visible = false; //Cycle5
                    RoutesGridView.Columns[12].Visible = false; //Pruning
                    RoutesGridView.Columns[13].Visible = false; //Mulching
                    RoutesGridView.Columns[14].Visible = false; //Planting
                    RoutesGridView.Columns[15].Visible = false; //Uprooting
                    RoutesGridView.Columns[16].Visible = true; //Trimming
                    RoutesGridView.Columns[17].Visible = false; //Watering
                    break;

                //Watering Routes
                case 4:
                    InfoUserControl.TryRun(() =>
                    {
                        List<RouteStatus> wateringList = routeManager.WateringList(yardId);
                        RoutesGridView.DataSource = wateringList;
                        RoutesGridView.DataBind();
                    });

                    RoutesGridView.Columns[5].Visible = true;  //Count
                    RoutesGridView.Columns[7].Visible = true;  //Cycle1
                    RoutesGridView.Columns[4].Visible = true;  //Cycle2
                    RoutesGridView.Columns[9].Visible = false;  //Cycle3
                    RoutesGridView.Columns[10].Visible = false; //Cycle4
                    RoutesGridView.Columns[11].Visible = false; //Cycle5
                    RoutesGridView.Columns[12].Visible = false; //Pruning
                    RoutesGridView.Columns[13].Visible = false; //Mulching
                    RoutesGridView.Columns[14].Visible = false; //Planting
                    RoutesGridView.Columns[15].Visible = false; //Uprooting
                    RoutesGridView.Columns[16].Visible = false; //Trimming
                    RoutesGridView.Columns[17].Visible = false; //Watering
                    break;

                //Planting Routes
                case 5:
                    InfoUserControl.TryRun(() =>
                    {
                        List<RouteStatus> PlantingList = routeManager.PlantingList(yardId);
                        RoutesGridView.DataSource = PlantingList;
                        RoutesGridView.DataBind();
                    });

                    RoutesGridView.Columns[5].Visible = false;  //Count
                    RoutesGridView.Columns[7].Visible = false;  //Cycle1
                    RoutesGridView.Columns[4].Visible = false;  //Cycle2
                    RoutesGridView.Columns[9].Visible = false;  //Cycle3
                    RoutesGridView.Columns[10].Visible = false; //Cycle4
                    RoutesGridView.Columns[11].Visible = false; //Cycle5
                    RoutesGridView.Columns[12].Visible = false; //Pruning
                    RoutesGridView.Columns[13].Visible = false; //Mulching
                    RoutesGridView.Columns[14].Visible = true; //Planting
                    RoutesGridView.Columns[15].Visible = true; //Uprooting
                    RoutesGridView.Columns[16].Visible = false; //Trimming
                    RoutesGridView.Columns[17].Visible = false; //Watering
                    break;
            }
        }

        protected void RouteMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            RoutesGridView.PageIndex = 0;


            switch (index)
            {
                case 0:
                    SiteTypeID.Text = "1";
                    //Srub Bed Maintenance
                    TaskID.Text = "1";

                    break;
                case 1:
                    SiteTypeID.Text = "2";
                    //Srub Bed Maintenance
                    TaskID.Text = "1";

                    break;
                case 2:
                    SiteTypeID.Text = "3";
                    //Trimming
                    TaskID.Text = "7";

                    break;
                case 3:
                    SiteTypeID.Text = "4";

                    break;
                case 4:
                    SiteTypeID.Text = "5";
                    //Planting
                    TaskID.Text = "2";

                    break;
            }
        }

        protected void RoutesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView grid = sender as GridView;
            grid.PageIndex = e.NewPageIndex;
            grid.DataBind();
        }

        protected void RoutesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RoutesGridView.EditIndex = e.NewEditIndex;
        }

        protected void RoutesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void RoutesGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
    }
}