using System;
using System.Collections.Generic;

#region Additional Namespaces
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Marigold.Security;
using MarigoldSystem.BLL;
using MarigoldSystem.Data.DTO_s;
using MarigoldSystem.Data.Entities;
using MarigoldSystem.Data.POCO_s;
using Microsoft.AspNet.Identity;
#endregion

namespace Marigold.App_Pages.City_Operations.Parks.CrewLeader
{
    public partial class Crews : System.Web.UI.Page
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
                    UserId.Text = (security.GetCurrentUserId(Context.User.Identity.GetUserName())).ToString();
                    EmployeeController employee = new EmployeeController();
                    YardID.Text = employee.GetYardID(int.Parse(UserId.Text)).ToString();
                    //DriverID.Text = "0";
                    
                    if (!IsPostBack)
                    {
                        PopulateUnitReport();
                        PopulateRouteStatus();
                    }
                });

            RefreshCurrentCrews();
                
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        /// <summary>
        /// This method populates JobCards
        /// </summary>
        public void PopulateRouteStatus()
        {
            InfoUserControl.TryRun(() =>
            {
                CrewController crewManager = new CrewController();
                List<JobCardStatus> cardStatus = crewManager.Get_JobCardStatus();
                JobCardStatusGridView.DataSource = cardStatus;
                JobCardStatusGridView.DataBind();
                if (cardStatus.Count > 0)
                {
                    JobCardStatusGridView.Visible = true;
                    JobcardTitle.Visible = true;
                }
                else
                {
                    JobCardStatusGridView.Visible = false;
                    JobcardTitle.Visible = false;
                }
            });
        }

        /// <summary>
        /// This method populates the GridView for reporting the mileage
        /// </summary>
        public void PopulateUnitReport()
        {
            int yardId = int.Parse(YardID.Text);
            CrewController crewManager = new CrewController();
            InfoUserControl.TryRun(() =>
            {
                List<UnitReport> report = crewManager.GetUnitReports(yardId);
                if(report.Count == 0)
                {
                    UnitReportHeader.Visible = false;
                    UnitReoprtGV.Visible = false;
                }
                else
                {
                    UnitReportHeader.Visible = true;
                    UnitReoprtGV.Visible = true;
                    report.Sort((x, y) => x.Unit.CompareTo(y.Unit));
                    UnitReoprtGV.DataSource = report;
                    UnitReoprtGV.DataBind();
                }
            });
        }


        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        /// <summary>
        /// This event fires when the user select a unit category (Equipment/truck)
        ///     It resets the EmployeeGirwView to null
        ///     It turns Off all button controls
        ///     It populates the list of Units (Equipment/Truck) in the DrompDownList control
        /// </summary>
        /// <param name="sender">the sender is a RadioButtonList</param>
        /// <param name="e"></param>
        protected void FleetCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int yardId = int.Parse(YardID.Text);
            FleetController fleet = new FleetController();

            string category = (FleetCategory.SelectedItem.Text).Trim();
            if (category == "Equipments")
            {
                EmployeeGridView.DataSource = null;
                EmployeeGridView.DataBind();
                EmployeeGridView.Visible = false;
                CreateCrew.Visible = false;
                Done.Visible = false;
                Cancel.Visible = true;
                CrewID.Text = "";
                MessageUserControl.TryRun(() =>
                {
                    List<Equipment> equipments = fleet.GetEquipments(yardId);
                    SelectUnitDDL.DataSource = equipments;
                    SelectUnitDDL.DataTextField = nameof(Equipment.Description);
                    SelectUnitDDL.DataValueField = nameof(Equipment.EquipmentID);
                    SelectUnitDDL.Visible = true;
                    SelectUnitDDL.DataBind();
                    SelectUnitDDL.Items.Insert(0, "Select an Equipment");
                });
            }
            else if (category == "Trucks")
            {
                EmployeeGridView.DataSource = null;
                EmployeeGridView.DataBind();
                EmployeeGridView.Visible = false;
                CreateCrew.Visible = false;
                Done.Visible = false;
                Cancel.Visible = true;
                CrewID.Text = "";
                MessageUserControl.TryRun(() =>
                {
                    List<Truck> units = fleet.GetTrucks(yardId);
                    SelectUnitDDL.DataSource = units;
                    SelectUnitDDL.DataTextField = nameof(Truck.TruckDescription);
                    SelectUnitDDL.DataValueField = nameof(Truck.TruckID);
                    SelectUnitDDL.Visible = true;
                    SelectUnitDDL.DataBind();
                    SelectUnitDDL.Items.Insert(0, "Select a Truck");
                });
            }
        }

        /// <summary>
        /// This event fires when the user select a unit from the DropDownList control.
        ///     It resets the EmployeeGridView Pager
        ///     It sets Label control for refreshing the EmployeeGridView to "Driver".
        ///     It turns On/Off all the Necessary/Unnecessary columns
        ///     It refreshses the EmployeeGridView
        /// </summary>
        /// <param name="sender">The Sender is a DropDownList control</param>
        /// <param name="e"></param>
        protected void SelectUnitDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SelectUnitDDL.SelectedIndex == 0)
            {
                InfoUserControl.ShowWarning("Please select a Unit before proceeding!");
                Done.Visible = false;
                CreateCrew.Visible = false;
                EmployeeGridView.Visible = false;
                Cancel.Visible = false;
            }
            else
            {
                string category = (FleetCategory.SelectedItem.Text).Trim();
                Refresh.Text = "Driver";
                FleetController fleetManager = new FleetController();
                int unitId = int.Parse(SelectUnitDDL.SelectedValue);
                string unitDesc = SelectUnitDDL.SelectedItem.Text;

                if (fleetManager.FoundUnit(unitId, category))
                {
                    InfoUserControl.ShowWarning(unitDesc + " is already assigned to a different crew. If you want to update the Crew assigned to this unit, " +
                        "Select the crew in the pane below");
                }
                else
                {
                    if (category == "Equipments")
                    {
                        EmployeeGridView.PageIndex = 0;
                        EmployeeGridView.Columns[4].Visible = false;
                        EmployeeGridView.Columns[5].Visible = true;
                        EmployeeGridView.Columns[6].Visible = true;
                        EmployeeGridView.Columns[7].Visible = false;
                        EmployeeGridView.Visible = true;
                        Cancel.Visible = true;
                        RefreshDriverList(1);


                    }
                    else if (category == "Trucks")
                    {
                        EmployeeGridView.PageIndex = 0;
                        EmployeeGridView.Visible = true;
                        EmployeeGridView.Columns[4].Visible = true;
                        EmployeeGridView.Columns[5].Visible = true;
                        EmployeeGridView.Columns[6].Visible = true;
                        EmployeeGridView.Columns[7].Visible = false;
                        Cancel.Visible = true;
                        RefreshDriverList(2);
                    }
                }
            }
           
        }


        /// <summary>
        /// This events fires when the user selects a driver
        ///     It makes RadioButtons mutually exclusive
        ///     It turns On/Off the visibility of the Button controls 
        /// </summary>
        /// <param name="sender">The Sender is a GridView control</param>
        /// <param name="e"></param>
        protected void SelectedDriver_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedButton = new RadioButton();
            selectedButton = (RadioButton)sender;
            foreach (GridViewRow row in EmployeeGridView.Rows)
            {
                (row.FindControl("SelectedDriver") as RadioButton).Checked = false;
            }
            selectedButton.Checked = true;

            string category = (FleetCategory.SelectedItem.Text).Trim();
            if (category == "Equipments")
            {
                CreateCrew.Visible = false;
                Cancel.Visible = true;
            }
            else
            {
                CreateCrew.Visible = true;
                Cancel.Visible = true;
            }
            Done.Visible = true;
        }

        /// <summary>
        /// This Method Populates/Refreshes the list of drivers
        ///     There are two types of drivers: 
        ///         1. Truck  Drivers
        ///         2. Equipment Drivers
        /// </summary>
        /// <param name="type">The type specifies the list of drivers to be returned</param>
        protected void RefreshDriverList(int type)
        {
            InfoUserControl.TryRun(() =>
            {
                int unitId = int.Parse(SelectUnitDDL.SelectedValue);
                int yardId = int.Parse(YardID.Text);
                FleetController fleetManager = new FleetController();
                List<Driver> drivers = fleetManager.GetTruckDrivers(yardId, unitId, type);
                EmployeeGridView.DataSource = drivers;
                EmployeeGridView.DataBind();

            });

        }

        /// <summary>
        /// This event fires when the user pages the GridView
        ///     It sets the Pager index to the next one
        ///     It refreshes the dirvers list (EmployeeGridView)
        /// </summary>
        /// <param name="sender">the Sender is a GridView control</param>
        /// <param name="e"></param>
        protected void EmployeeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (Refresh.Text == "Driver")
            {
                GridView grid = sender as GridView;
                grid.PageIndex = e.NewPageIndex;
                grid.DataBind();
                int type = int.Parse(FleetCategory.SelectedValue);
                RefreshDriverList(type);
            }
            else if (Refresh.Text == "Member")
            {
                GridView grid = sender as GridView;
                grid.PageIndex = e.NewPageIndex;
                grid.DataBind();
                RefreshCrewMember();
            }
        }

        /// <summary>
        /// This event fires when when the user presses on the Create a Crew button
        ///     It retrives the driver ID
        ///     It calls the method that creates a New Crew
        ///     It populates/refreshes all current crews
        /// </summary>
        /// <param name="sender">The sender is a button control</param>
        /// <param name="e"></param>
        protected void CreateCrew_Click(object sender, EventArgs e)
        {
            int driverId = 0;
            string category = (FleetCategory.SelectedItem.Text).Trim();
            //Retrieve the selected DriverID 
            foreach (GridViewRow row in EmployeeGridView.Rows)
            {
                if((row.FindControl("SelectedDriver") as RadioButton).Checked == true)
                {
                  driverId = int.Parse((row.FindControl("EmployeeID") as Label).Text);
                }
            }

            //Checks that a driver is selecetd. then proceed
            if(driverId == 0)
            {
                InfoUserControl.ShowInfo("Please select a driver");
            }
            else
            {
                DriverID.Text = driverId.ToString();
                CrewController crewManager = new CrewController();
                InfoUserControl.TryRun(() =>
                {
                    crewManager.CreateCrew(int.Parse(SelectUnitDDL.SelectedValue), driverId, category); 
                    Refresh.Text = "Member";
                    EmployeeGridView.PageIndex = 0;
                    Cancel.Visible = true;
                    CreateCrew.Visible = false;
                    PopulateUnitReport();
                    RefreshCrewMember();
                    RefreshCurrentCrews();
                    InfoUserControl.ShowInfo("Add Crew Members");
                    List<CurrentCrews> crews = crewManager.GetCurrentCrews(int.Parse(YardID.Text));
                    crews.Sort((x, y) => y.CrewID.CompareTo(x.CrewID));
                    CrewID.Text = crews.Count <= 0 ? "" : (crews[0].CrewID).ToString();
                });

            }
        }

        /// <summary>
        /// This method Populates/refreshes the list all employee for selction
        ///     It turns On/Off all necessary/unnecessary column(s) on the EmployeeGridView
        /// </summary>
        protected void RefreshCrewMember()
        {
            MessageUserControl.TryRun(() =>
            {
                //Retrieve all employees
                EmployeeController EmployeeManager = new EmployeeController();
                List<Driver> employees = EmployeeManager.GetEmployees(int.Parse(YardID.Text));
                employees.Sort((x, y) => x.Name.CompareTo(y.Name));
                EmployeeGridView.DataSource = employees;
                EmployeeGridView.DataBind();
                EmployeeGridView.Columns[4].Visible = false;
                EmployeeGridView.Columns[5].Visible = false;
                EmployeeGridView.Columns[6].Visible = false;
                EmployeeGridView.Columns[7].Visible = true;
                EmployeeGridView.Visible = true;
            });
        }

        /// <summary>
        /// This methods Populates/Refreshes the list of all Current Crews
        ///     It also returns the ID of the last crew created and set on a Label control
        /// </summary>
        protected void RefreshCurrentCrews()
        {
            InfoUserControl.TryRun(() =>
            {
                CrewController crewManager = new CrewController();
                List<CurrentCrews> crews = crewManager.GetCurrentCrews(int.Parse(YardID.Text));
                crews.Sort((x, y) => y.CrewID.CompareTo(x.CrewID));
                AllCurrentCrews.DataSource = crews;
                AllCurrentCrews.DataBind();
            });
        }
        
        /// <summary>
        /// This methood fires when the user press on the Done button
        ///     It tunrs Off all controls used to make up crews
        ///     If the user is creating an Equipment crew, It creates
        ///         the crew only with a driver
        ///     If the user is creating a Truck crew, It cleats all controla
        ///     It populates the list of Routes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Done_Click(object sender, EventArgs e)
        {
            string category = (FleetCategory.SelectedIndex < 0) ? "" : FleetCategory.SelectedItem.Text.Trim();
            switch (category)
            {
                case "Equipments":
                    AssignDriver();
                    break;

                case "Trucks":

                    if(int.Parse(DriverID.Text) == 0)
                    {
                        AssignDriver();
                    }
                    else
                    {
                        CloseCrewPane();
                    }

                    break;

                case "":
                    EmployeeGridView.Visible = false;
                    Done.Visible = false;
                    break;
            }
            SiteMenu.Visible = true;
            JobCardStatusGridView.Visible = false;
            JobcardTitle.Visible = false;
            UnitReoprtGV.Visible = false;
            UnitReportHeader.Visible = false;

        }

        protected void CloseCrewPane()
        {
            RefreshCrewMember();
            RefreshCurrentCrews();
            EmployeeGridView.Visible = false;
            SelectUnitDDL.Visible = false;
            FleetCategory.ClearSelection();
            Done.Visible = false;
            CreateCrew.Visible = false;
            Cancel.Visible = false;
        }

        protected void AssignDriver()
        {
            string category = (FleetCategory.SelectedIndex < 0) ? "" : FleetCategory.SelectedItem.Text.Trim();

            int driverId = 0;
            //Retrieve the selected DriverID 
            foreach (GridViewRow row in EmployeeGridView.Rows)
            {
                if ((row.FindControl("SelectedDriver") as RadioButton).Checked == true)
                {
                    driverId = int.Parse((row.FindControl("EmployeeID") as Label).Text);
                }
            }

            //Checks that a driver is selecetd. then proceed
            if (driverId == 0)
            {
                InfoUserControl.ShowInfo("Please select a driver");
            }
            else
            {
                CrewController crewManager = new CrewController();
                InfoUserControl.TryRun(() =>
                {
                    crewManager.CreateCrew(int.Parse(SelectUnitDDL.SelectedValue), driverId, category);
                    List<CurrentCrews> crews = crewManager.GetCurrentCrews(int.Parse(YardID.Text));
                    crews.Sort((x, y) => y.CrewID.CompareTo(x.CrewID));
                    CrewID.Text = crews.Count <= 0 ? "" : (crews[0].CrewID).ToString();
                    CloseCrewPane();

                });

                PopulateUnitReport();

            }
        }
        /// <summary>
        /// This event add a new member to a given crew 
        ///     It calls the method that adds a new crew member to a crew
        ///     It refreshes the list of current crews
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EmployeeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "AddMember")
            {
                InfoUserControl.TryRun(() =>
                {
                    CrewController crewManager = new CrewController();
                    crewManager.AddCrewMember(int.Parse(CrewID.Text), int.Parse(e.CommandArgument.ToString()));
                    RefreshCurrentCrews();
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void AllCurrentCrews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string caller = e.CommandName;
            switch (caller)
            {
                case "SelectedCrew":
                    CrewID.Text = e.CommandArgument.ToString();
                    string crew = "";
                    SiteMenu.Visible = true;
                    Done.Visible = true;
                    JobCardStatusGridView.Visible = false;
                    JobcardTitle.Visible = false;
                    UnitReoprtGV.Visible = false;
                    UnitReportHeader.Visible = false;
                    InfoUserControl.TryRun(() =>
                    {
                        FleetController fleet = new FleetController();
                         crew = fleet.GetUnitDescription(int.Parse(CrewID.Text));
                    });
                    InfoUserControl.ShowInfo("You are updating crew " + crew + "");
                    RefreshCrewMember();
                    break;

                case "RemoveMember":
                    InfoUserControl.TryRun(() =>
                    {
                        CrewController crewManager = new CrewController();
                        crewManager.RemoveCrewMember(int.Parse(e.CommandArgument.ToString()), int.Parse(CrewID.Text));
                        RefreshCurrentCrews();
                    });
                    break;

                case "DeleteJobCard":
                    InfoUserControl.TryRun(() =>
                    {
                        CrewController crewManager = new CrewController();
                        crewManager.DeleteJobCardCrew(int.Parse(e.CommandArgument.ToString()));
                        RefreshCurrentCrews();
                    });
                    break;

                case "DeleteCrew":
                    MessageUserControl.TryRun(() =>
                    {
                        CrewController crewManager = new CrewController();
                        crewManager.DeleteCrew(int.Parse(e.CommandArgument.ToString()));
                        RefreshCurrentCrews();
                        PopulateUnitReport();
                        PopulateRouteStatus();
                    });
                    break;
            }
        }

        /// <summary>
        /// This event fires when the user click on the Cancel Button
        ///     It turns off all the controls to make up Crews
        ///     It deletes the crew that is currently being formed
        ///     It refreshes the Current Crews List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cancel_Click(object sender, EventArgs e)
        {
            EmployeeGridView.Visible = false;
            SelectUnitDDL.Visible = false;
            FleetCategory.ClearSelection();
            Done.Visible = false;
            Cancel.Visible = false;
            CreateCrew.Visible = false;
            MakeCrew.Visible = true;
            LastCrews.Visible = true;
            FleetCategory.Visible = false;

            //Deletes the crew in formation.
            InfoUserControl.TryRun(() =>
            {
                if (!string.IsNullOrEmpty(CrewID.Text))
                {
                    int crewId = int.Parse(CrewID.Text);
                    CrewController crewManager = new CrewController();
                    crewManager.DeleteCrew(crewId);
                    CrewID.Text = "";
                    RefreshCurrentCrews();
                }
            });
        }

        protected void ARoute_Click(object sender, EventArgs e)
        {
            ResetDataPager("1");
            ARoute.CssClass = "active-tab";
            BRoute.CssClass = "default-tab";
            GRoute.CssClass = "default-tab";
            WRoute.CssClass = "default-tab";
            PRoute.CssClass = "default-tab";
        }

        protected void BRoute_Click(object sender, EventArgs e)
        {
            ResetDataPager("2");
            ARoute.CssClass = "default-tab";
            BRoute.CssClass = "active-tab";
            GRoute.CssClass = "default-tab";
            WRoute.CssClass = "default-tab";
            PRoute.CssClass = "default-tab";
        }

        protected void GRoute_Click(object sender, EventArgs e)
        {
            ResetDataPager("3");
            ARoute.CssClass = "default-tab";
            BRoute.CssClass = "default-tab";
            GRoute.CssClass = "active-tab";
            WRoute.CssClass = "default-tab";
            PRoute.CssClass = "default-tab";
        }

        protected void WRoute_Click(object sender, EventArgs e)
        {
            ResetDataPager("4");
            ARoute.CssClass = "default-tab";
            BRoute.CssClass = "default-tab";
            GRoute.CssClass = "default-tab";
            WRoute.CssClass = "active-tab";
            PRoute.CssClass = "default-tab";
        }

        protected void PRoute_Click(object sender, EventArgs e)
        {
            ResetDataPager("5");
            ARoute.CssClass = "default-tab";
            BRoute.CssClass = "default-tab";
            GRoute.CssClass = "default-tab";
            WRoute.CssClass = "default-tab";
            PRoute.CssClass = "active-tab";
        }

        protected void ResetDataPager(string siteTypeID)
        {
            SiteTypeID.Text = siteTypeID;
            DataPager topPager = (DataPager)RouteListView.FindControl("TopDataPager");
            DataPager bottomPager = (DataPager)RouteListView.FindControl("BottomDataPager");
            topPager.SetPageProperties(0, topPager.PageSize, true);
            bottomPager.SetPageProperties(0, bottomPager.PageSize, true);
            RouteListView.Visible = true;
        }
        /// <summary>
        /// This event fires when the user add a job site to a crew or click on the Finish button
        ///     When adding a job site to a crew, it first verifies that a crew is selected
        ///         Then add the Job site 
        ///     When user click on the Finish button, it turns off the site menu and the Routes
        ///     It turns on the Job Cards GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RouteListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string message = "";
            string command = e.CommandName;

            switch (command)
            {
                case "AddSite":
                    //Get hold of the row that fired the event
                    ListViewDataItem item = RouteListView.Items[e.Item.DisplayIndex];
                    DropDownList list = item.FindControl("SelectTask") as DropDownList;

                    InfoUserControl.TryRun(() =>
                    {
                        if (string.IsNullOrEmpty(CrewID.Text))
                        {
                            InfoUserControl.ShowWarning("You must first create or select a Crew before adding a job site!");
                        }
                        else
                        {
                            int crewId = int.Parse(CrewID.Text);
                            int siteId = int.Parse(e.CommandArgument.ToString());
                            int taskId = int.Parse(list.SelectedValue);

                            CrewController crewManager = new CrewController();
                            message = crewManager.AddJobCard(crewId, siteId,taskId);
                            RefreshCurrentCrews();
                        }
                    });

                    if (!string.IsNullOrEmpty(message))
                    {
                        InfoUserControl.ShowInfo("The following Crew(s) are already assigned to this same Site:  " + message);
                        RefreshCurrentCrews();
                    }
                    break;

                case "Finish":
                    SiteMenu.Visible = false;
                    RouteListView.Visible = false;
                    PopulateRouteStatus();
                    PopulateUnitReport();
                    MakeCrew.Visible = true;
                    LastCrews.Visible = true;
                    FleetCategory.Visible = false;

                    ARoute.CssClass = "default-tab";
                    BRoute.CssClass = "default-tab";
                    GRoute.CssClass = "default-tab";
                    WRoute.CssClass = "default-tab";
                    PRoute.CssClass = "default-tab";
                    break;
            }
            
        }

        /// <summary>
        /// This event fires when the user select a date from the calendar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CloseDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            Calendar calendar = sender as Calendar;
            GridViewRow row = calendar.Parent.Parent as GridViewRow;
            TextBox completedDate = row.FindControl("CompletedDate") as TextBox;
            ImageButton image = row.FindControl("CalendarImage") as ImageButton;

            completedDate.Text = calendar.SelectedDate.ToShortDateString();
            calendar.Visible = false;
            image.Visible = true;
        }

        /// <summary>
        /// This event fires when the calendar is displayed in the GriView
        ///     It disable the days that do not belong to the cuurrent month
        ///         displayed in the calendar and change the backColor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CloseDateCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.DarkGray;
            }
        }

        /// <summary>
        /// This event fires when the user clicks on save unit report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UnitReoprtGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName;
            if(command == "SaveUnitReport")
            {
                int crewId = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer) as GridViewRow; //Gets hold of the GridViewRow
                string KS = ((row.FindControl("KM_Start") as TextBox).Text);
                string KE = ((row.FindControl("KM_End") as TextBox).Text);

                int KmStart = string.IsNullOrEmpty(KS) ? 0 : int.Parse(KS);
                int KmEnd = string.IsNullOrEmpty(KE) ? 0 : int.Parse(KE);
                string comment = (row.FindControl("Comment") as TextBox).Text;

                string crew = (row.FindControl("Unit") as Label).Text;

                InfoUserControl.TryRun(() =>
                {
                    CrewController crewManager = new CrewController();
                    crewManager.UpdateCrew(crewId, KmStart, KmEnd, comment);

                    PopulateUnitReport();
                }, crew + " was updated successfully!");
            }
        }

        /// <summary>
        /// This event fires when the user clicks on the calendar image or the save icon
        ///     It populates the calendar control
        ///     It updates a job card 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void JobCardStatusGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName.ToString();

            switch (command)
            {
                case "ChangeDate":

                    GridViewRow row = ((e.CommandSource as ImageButton).NamingContainer) as GridViewRow;
                    Calendar calendar = row.FindControl("Calendar") as Calendar;
                    calendar.Visible = true;
                    ImageButton image = row.FindControl("CalendarImage") as ImageButton;
                    image.Visible = false;
                    break;

                case "SaveJobCard":
                    GridViewRow gridRow = ((e.CommandSource as LinkButton).NamingContainer) as GridViewRow; //Gets hold of the GridViewRow
                    int jobCardId = int.Parse(e.CommandArgument.ToString());
                    string completedDate = (gridRow.FindControl("CompletedDate") as TextBox).Text;

                    CrewController crewManager = new CrewController();
                    crewManager.UpdateJobCard(jobCardId, completedDate);

                    PopulateRouteStatus();
                    RefreshCurrentCrews();
                    break;
            }
        }

        /// <summary>
        /// This even fires when paging the Unit Reports GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UnitReoprtGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UnitReoprtGV.PageIndex = e.NewPageIndex;
            PopulateUnitReport();
        }

        /// <summary>
        /// this event fires when paging the job card Status GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void JobCardStatusGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            JobCardStatusGridView.PageIndex = e.NewPageIndex;
            PopulateRouteStatus();
        }

        protected void MakeCrew_Click(object sender, EventArgs e)
        {
            FleetCategory.Visible = true;
            MakeCrew.Visible = false;
            LastCrews.Visible = false;
        }
    }
}