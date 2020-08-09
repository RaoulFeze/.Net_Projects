using Marigold.Security;
using MarigoldSystem.BLL;
using MarigoldSystem.Data.Entities;
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
    public delegate void ProcessSearch();
    public partial class Routes : System.Web.UI.Page
    {

        //public List<RouteStatus> routes = new List<RouteStatus>();

        #region Search validation string constantes
        protected const string CRITERION_IS_MISSING = "Select a search criterion";
        protected const string PIN_IS_NOT_A_NUMBER = "Enter a Pin. A Pin should be a whole number";
        protected const string TASK_NOT_SELECTED = "Select a specific task";
        protected const string SEASON_NOT_SELECTED = "Select a season";
        protected const string COMMUNITY_IS_MISSING = "Enter a Community name";
        protected const string DESCRIPTION_IS_MISSING = "Enter a description";
        protected const string ADDRESS_IS_MISSING = "Enter an address";
        protected const string SITETYPEID_NOT_DEFINED = "1000000";
        #endregion

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

                if (!Page.IsPostBack)
                {
                    SearchCategory.Items.Insert(0, "Search Criteria...");
                    SearchCategory.Items.Insert(1, "Pin");
                    SearchCategory.Items.Insert(2, "Community");
                    SearchCategory.Items.Insert(3, "Description");
                    SearchCategory.Items.Insert(4, "Address");

                    SeasonDDL.Items.Insert(0, "Seasons");
                    int currentSeason = DateTime.Now.Year;
                    for (int season = currentSeason; season >= currentSeason - 10; season--)
                    {
                        ListItem list = new ListItem(season.ToString());
                        SeasonDDL.Items.Add(list);
                    }
                }


            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        /// <summary>
        /// This event fires when the pre-render stage of the page life cycle is complete
        ///     It loads the RoutesGridView 
        ///     It turns On/Off the columns inthe GridView that are necessary/unnecessary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            List<RouteStatus> routes = new List<RouteStatus>();
            RouteController routeManager = new RouteController();
            int siteTypeId = int.Parse(SiteTypeID.Text);
            int yardId = int.Parse(YardID.Text);
            int taskId = int.Parse(TaskID.Text);
            int searchFlag = int.Parse(SearchFlag.Text);
            switch (siteTypeId)
            {
                case 0:
                    if (SearchFlag.Text == "1")
                    {

                        ValidateSearch(() =>
                        {
                            PopulateSearch();

                        }, "");
                    }

                    //RoutesManager(routes);
                    if (taskId == 1)
                    {
                        A_Routes();
                        MenuItem item = RouteMenu.Items[0] as MenuItem;
                        item.Selected = true;
                    }
                    else if (taskId == 7)
                    {
                        G_Routes();
                        MenuItem item = RouteMenu.Items[2] as MenuItem;
                        item.Selected = true;
                    }
                    else if (taskId == 6)
                    {
                        W_Routes();
                        MenuItem item = RouteMenu.Items[3] as MenuItem;
                        item.Selected = true;
                    }
                    else if (taskId == 2)
                    {
                        P_Routes();
                        MenuItem item = RouteMenu.Items[4] as MenuItem;
                        item.Selected = true;
                    }
                    break;
                //A Routes
                case 1:
                    InfoUserControl.TryRun(() =>
                    {
                        if (searchFlag == 1)
                        {
                            PopulateSearch();
                            MenuItem item = RouteMenu.Items[0] as MenuItem;
                            item.Selected = true;
                        }
                        else
                        {
                            routes = routeManager.Get_AB_Routes(yardId, siteTypeId, taskId);
                            RoutesManager(routes);
                        }

                    });

                    A_Routes();
                    break;

                //B Routes
                case 2:

                    InfoUserControl.TryRun(() =>
                    {
                        routes = routeManager.Get_AB_Routes(yardId, siteTypeId, taskId);
                    });

                    RoutesManager(routes);
                    B_Routes();
                    break;

                //Grass Routes
                case 3:
                    InfoUserControl.TryRun(() =>
                    {
                        if (searchFlag == 1)
                        {
                            PopulateSearch();
                            MenuItem item = RouteMenu.Items[2] as MenuItem;
                            item.Selected = true;
                        }
                        else
                        {
                            routes = routeManager.GrassRouteList(yardId);
                            RoutesManager(routes);
                        }
                    });
                    G_Routes();
                    break;

                //Watering Routes
                case 4:
                    InfoUserControl.TryRun(() =>
                    {
                        if (searchFlag == 1)
                        {
                            PopulateSearch();
                            MenuItem item = RouteMenu.Items[3] as MenuItem;
                            item.Selected = true;
                        }
                        else
                        {
                            routes = routeManager.WateringList(yardId);
                            RoutesManager(routes);
                        }
                    });
                    W_Routes();
                    break;

                //Planting Routes
                case 5:
                    InfoUserControl.TryRun(() =>
                    {
                        if (searchFlag == 1)
                        {
                            PopulateSearch();
                            MenuItem item = RouteMenu.Items[4] as MenuItem;
                            item.Selected = true;
                        }
                        else
                        {
                            routes = routeManager.PlantingList(yardId);
                            RoutesManager(routes);
                        }
                    });

                    P_Routes();
                    break;
            }

        }

        /// <summary>
        /// This method populates the Routes on Gridview
        /// </summary>
        protected void PopulateSearch()
        {
            RouteController routeManager = new RouteController();
            List<RouteStatus> route = new List<RouteStatus>();
            int yardId = int.Parse(YardID.Text);
            int taskId = int.Parse(TaskID.Text);
            int season = int.Parse(SeasonDDL.SelectedValue);
            string searchCriterion = SearchCriteria.Text;
            switch (SearchCategory.SelectedIndex)
            {
                case 0:
                    InfoUserControl.ShowWarning(CRITERION_IS_MISSING);
                    SiteTypeID.Text = SITETYPEID_NOT_DEFINED;
                    break;
                case 1:
                    route = routeManager.Get_RouteByPin(int.Parse(SearchCriteria.Text), yardId, taskId, season);
                    break;
                case 2:
                    //Community
                    route = routeManager.GetRoutesByCommunity(SearchCriteria.Text, yardId, taskId, season);
                    break;
                case 3:
                    //Description
                    route = routeManager.GetRoutesByDescription(searchCriterion, yardId, taskId, season);
                    break;
                case 4:
                    //Address
                    route = routeManager.GetRoutesByAddress(searchCriterion, yardId, taskId, season);
                    break;
            }
            if (route == null || route.Count == 0)
            {
                InfoUserControl.ShowInfo("There is no data retuned with the given search criteria");
                RoutesManager(route);
            }
            else
            {
                RoutesManager(route);
            }
        }

        protected void RoutesManager(List<RouteStatus> routes)
        {
            RoutesGridView.DataSource = routes;
            RoutesGridView.DataBind();
        }
        /// <summary>
        /// This event fires when the user selects a route type
        ///     It sets the SiteTypeID and the TaskID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    SearchFlag.Text = "0";

                    break;
                case 1:
                    SiteTypeID.Text = "2";
                    //Srub Bed Maintenance
                    TaskID.Text = "1";
                    SearchFlag.Text = "0";

                    break;
                case 2:
                    SiteTypeID.Text = "3";
                    //Trimming
                    TaskID.Text = "7";
                    SearchFlag.Text = "0";

                    break;
                case 3:
                    SiteTypeID.Text = "4";
                    SearchFlag.Text = "0";

                    break;
                case 4:
                    SiteTypeID.Text = "5";
                    //Planting
                    TaskID.Text = "2";
                    SearchFlag.Text = "0";

                    break;
            }
        }

        /// <summary>
        /// This event fires when the user pages the GidView
        /// It moves the index page to the next one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RoutesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RoutesGridView.PageIndex = e.NewPageIndex;
            RoutesGridView.DataBind();
        }

        /// <summary>
        /// This event fires when the user click on the edit button
        ///     It allows the user to update the selected row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RoutesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RoutesGridView.EditIndex = e.NewEditIndex;
        }

        /// <summary>
        /// This event fires when the user click on the update button
        ///     it calls the update method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RoutesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            RouteController routeManager = new RouteController();
            GridViewRow row = sender as GridViewRow;
            row = RoutesGridView.Rows[e.RowIndex];
            int siteTypeId = int.Parse(SiteTypeID.Text);

            int siteId = int.Parse((row.FindControl("SiteID") as Label).Text);
            int pin = int.Parse((row.FindControl("Pin") as Label).Text);
            string description = (row.FindControl("Description") as TextBox).Text;
            string address = (row.FindControl("Address") as TextBox).Text;
            int area = int.Parse((row.FindControl("Area") as TextBox).Text);

            if (siteTypeId == 1 || siteTypeId == 2 || siteTypeId == 4 || siteTypeId == 5)
            {

            }
            else if(siteTypeId == 3)
            {

            }


            InfoUserControl.TryRun(() =>
            {
                int communityId = routeManager.GetCommunityId(siteId);

                Site site = new Site();
                site.SiteID = siteId;
                site.Pin = pin;
                site.CommunityID = communityId;
                site.Description = description;
                site.StreetAddress = address;
                site.Area = area;

                    //routeManager.UpdateSite(site);
                    CloseEditMode();
            });

        }

        /// <summary>
        /// This event fires when the user click on the cancel button
        ///     It disables the editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RoutesGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CloseEditMode();
        }

        protected void RoutesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void CloseEditMode()
        {
            RoutesGridView.EditIndex = -1;
            RoutesGridView.DataBind();
        }

        /// <summary>
        /// This method fires when the user click on the search button
        ///     It calls the ValidateSearch method that raises notifications
        ///         to the user in case there is an issue with the search criteria
        ///     It sets the TaskID, and SiteTypeID needed to do the search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            RouteController routeManager = new RouteController();
            SearchFlag.Text = "1";
            if (!string.IsNullOrEmpty(TaskOption.SelectedValue))
            {
                if (TaskOption.SelectedValue == "1")
                {
                    TaskID.Text = "1";
                    SiteTypeID.Text = "1";
                }
                else if (TaskOption.SelectedValue == "2")
                {
                    TaskID.Text = "7";
                    SiteTypeID.Text = "3";
                }
                else if (TaskOption.SelectedValue == "3")
                {
                    TaskID.Text = "6";
                    SiteTypeID.Text = "4";
                }
                else if (TaskOption.SelectedValue == "4")
                {
                    TaskID.Text = "2";
                    SiteTypeID.Text = "5";
                }
            }

            switch (SearchCategory.SelectedIndex)
            {
                case 0:
                    InfoUserControl.ShowWarning(CRITERION_IS_MISSING);
                    SiteTypeID.Text = SITETYPEID_NOT_DEFINED;
                    break;
                case 1:
                    int pin;
                    if (int.TryParse(SearchCriteria.Text, out pin))
                    {
                        SiteTypeID.Text = "0";
                    }
                    else
                    {
                        InfoUserControl.ShowWarning(PIN_IS_NOT_A_NUMBER);
                        SiteTypeID.Text = SITETYPEID_NOT_DEFINED;
                    }
                    break;
                case 2:
                    ValidateSearch(() =>
                    {

                    }, COMMUNITY_IS_MISSING);
                    break;
                case 3:
                    ValidateSearch(() =>
                    {

                    }, DESCRIPTION_IS_MISSING);
                    break;
                case 4:
                    ValidateSearch(() =>
                    {

                    }, ADDRESS_IS_MISSING);
                    break;
            }

        }

        /// <summary>
        /// This method validates the search filters
        /// </summary>
        /// <param name="Search"></param>
        /// <param name="missingCriteria"></param>
        protected void ValidateSearch(ProcessSearch Search, string missingCriteria)
        {
            string criteria = SearchCriteria.Text;
            if (string.IsNullOrEmpty(criteria))
            {
                InfoUserControl.ShowWarning(missingCriteria);
                SiteTypeID.Text = SITETYPEID_NOT_DEFINED;
            }
            else
            {
                if (string.IsNullOrEmpty(TaskOption.SelectedValue))
                {
                    InfoUserControl.ShowWarning(TASK_NOT_SELECTED);
                    SiteTypeID.Text = SITETYPEID_NOT_DEFINED;
                }
                else
                {
                    if (SeasonDDL.SelectedIndex == 0)
                    {
                        InfoUserControl.ShowWarning(SEASON_NOT_SELECTED);
                        SiteTypeID.Text = SITETYPEID_NOT_DEFINED;
                    }
                    else
                    {
                        InfoUserControl.TryRun(() =>
                        {
                            Search();
                        });
                    }
                }
            }

        }

        /// <summary>
        /// This method clear all search filters 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearSearch_Click(object sender, EventArgs e)
        {
            SearchCategory.SelectedIndex = 0;
            SearchCriteria.Text = "";
            TaskOption.SelectedIndex = -1;
            SeasonDDL.SelectedIndex = 0;
            SearchFlag.Text = "0";
        }

        /// <summary>
        /// This method fromats the Gridview for A Routes
        /// </summary>
        protected void A_Routes()
        {
            RoutesGridView.Columns[6].Visible = true; //Area
            RoutesGridView.Columns[7].Visible = false; //Count
            RoutesGridView.Columns[8].Visible = true;  //Notes
            RoutesGridView.Columns[9].Visible = true;  //Cycle1
            RoutesGridView.Columns[10].Visible = true;  //Cycle2
            RoutesGridView.Columns[11].Visible = true; //Cycle3
            RoutesGridView.Columns[12].Visible = true; //Cycle4
            RoutesGridView.Columns[13].Visible = true; //Cycle5
            RoutesGridView.Columns[14].Visible = true; //Pruning
            RoutesGridView.Columns[15].Visible = true; //Mulching
            RoutesGridView.Columns[16].Visible = false; //Planting
            RoutesGridView.Columns[17].Visible = false; //Uprooting
            RoutesGridView.Columns[18].Visible = false; //Trimming
            RoutesGridView.Columns[19].Visible = false; //Watering
        }

        /// <summary>
        /// This method fromats the Gridview for B Routes
        /// </summary>
        protected void B_Routes()
        {
            RoutesGridView.Columns[6].Visible = true; // Area
            RoutesGridView.Columns[7].Visible = false;  //Count
            RoutesGridView.Columns[8].Visible = true;  //Notes
            RoutesGridView.Columns[9].Visible = true;  //Cycle1
            RoutesGridView.Columns[10].Visible = true;  //Cycle2
            RoutesGridView.Columns[11].Visible = false; //Cycle3
            RoutesGridView.Columns[12].Visible = false; //Cycle4
            RoutesGridView.Columns[13].Visible = false; //Cycle5
            RoutesGridView.Columns[14].Visible = true; //Pruning
            RoutesGridView.Columns[15].Visible = true; //Mulching
            RoutesGridView.Columns[16].Visible = false; //Planting
            RoutesGridView.Columns[17].Visible = false; //Uprooting
            RoutesGridView.Columns[18].Visible = false; //Trimming
            RoutesGridView.Columns[19].Visible = false; //Watering
        }

        /// <summary>
        /// This method fromats the Gridview for Grass Routes
        /// </summary>
        protected void G_Routes()
        {
            RoutesGridView.Columns[6].Visible = false; // Area
            RoutesGridView.Columns[7].Visible = true;  //Count
            RoutesGridView.Columns[8].Visible = true;  //Notes
            RoutesGridView.Columns[9].Visible = true;  //Cycle1
            RoutesGridView.Columns[10].Visible = false;  //Cycle2
            RoutesGridView.Columns[11].Visible = false; //Cycle3
            RoutesGridView.Columns[12].Visible = false; //Cycle4
            RoutesGridView.Columns[13].Visible = false; //Cycle5
            RoutesGridView.Columns[14].Visible = false; //Pruning
            RoutesGridView.Columns[15].Visible = false; //Mulching
            RoutesGridView.Columns[16].Visible = false; //Planting
            RoutesGridView.Columns[17].Visible = false; //Uprooting
            RoutesGridView.Columns[18].Visible = true; //Trimming
            RoutesGridView.Columns[19].Visible = false; //Watering
        }

        /// <summary>
        /// This method fromats the Gridview for Planting Routes
        /// </summary>
        protected void P_Routes()
        {
            RoutesGridView.Columns[6].Visible = false; // Area
            RoutesGridView.Columns[7].Visible = false;  //Count
            RoutesGridView.Columns[8].Visible = true;  //Notes
            RoutesGridView.Columns[9].Visible = false;  //Cycle1
            RoutesGridView.Columns[10].Visible = false;  //Cycle2
            RoutesGridView.Columns[11].Visible = false; //Cycle3
            RoutesGridView.Columns[12].Visible = false; //Cycle4
            RoutesGridView.Columns[13].Visible = false; //Cycle5
            RoutesGridView.Columns[14].Visible = false; //Pruning
            RoutesGridView.Columns[15].Visible = false; //Mulching
            RoutesGridView.Columns[16].Visible = true; //Planting
            RoutesGridView.Columns[17].Visible = true; //Uprooting
            RoutesGridView.Columns[18].Visible = false; //Trimming
            RoutesGridView.Columns[19].Visible = false; //Watering
        }

        /// <summary>
        /// This method fromats the Gridview for Watering Routes
        /// </summary>
        protected void W_Routes()
        {
            RoutesGridView.Columns[6].Visible = false; // Area
            RoutesGridView.Columns[7].Visible = true;  //Count
            RoutesGridView.Columns[8].Visible = true;  //Notes
            RoutesGridView.Columns[9].Visible = false;  //Cycle1
            RoutesGridView.Columns[10].Visible = false;  //Cycle2
            RoutesGridView.Columns[11].Visible = false; //Cycle3
            RoutesGridView.Columns[12].Visible = false; //Cycle4
            RoutesGridView.Columns[13].Visible = false; //Cycle5
            RoutesGridView.Columns[14].Visible = false; //Pruning
            RoutesGridView.Columns[15].Visible = false; //Mulching
            RoutesGridView.Columns[16].Visible = false; //Planting
            RoutesGridView.Columns[17].Visible = false; //Uprooting
            RoutesGridView.Columns[18].Visible = false; //Trimming
            RoutesGridView.Columns[19].Visible = true; //Watering
        }

    }
}