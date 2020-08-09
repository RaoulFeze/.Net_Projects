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

                //InfoUserControl.TryRun(() =>
                //{
                    SecurityController security = new SecurityController();
                    UserId.Text = (security.GetCurrentUserId(Context.User.Identity.GetUserName())).ToString();
                    EmployeeController employee = new EmployeeController();
                    YardID.Text = employee.GetYardID(int.Parse(UserId.Text)).ToString();
                //});

                RefreshCurrentCrews();
                
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
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
                CrewController crewManager = new CrewController();
                InfoUserControl.TryRun(() =>
                {
                    crewManager.CreateCrew(int.Parse(SelectUnitDDL.SelectedValue), driverId, category); 
                    Refresh.Text = "Member";
                    EmployeeGridView.PageIndex = 0;
                    Cancel.Visible = true;
                    CreateCrew.Visible = false;
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
            SiteMenu.Visible = true;

            int driverId = 0;

            switch (category)
            {
                case "Equipments":
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
                            EmployeeGridView.Visible = false;
                            RefreshCurrentCrews();
                            SelectUnitDDL.Visible = false;
                            FleetCategory.ClearSelection();
                            Done.Visible = false;
                            Cancel.Visible = false;
                            List<CurrentCrews> crews = crewManager.GetCurrentCrews(int.Parse(YardID.Text));
                            crews.Sort((x, y) => y.CrewID.CompareTo(x.CrewID));
                            CrewID.Text = crews.Count <= 0 ? "" : (crews[0].CrewID).ToString();

                        });
                       

                    }
                    break;

                case "Trucks":

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
                            //Refresh.Text = "Member";
                            //EmployeeGridView.PageIndex = 0;
                            //Cancel.Visible = true;
                            //CreateCrew.Visible = false;
                            RefreshCrewMember();
                            RefreshCurrentCrews();
                            //InfoUserControl.ShowInfo("Add Crew Members");
                            List<CurrentCrews> crews = crewManager.GetCurrentCrews(int.Parse(YardID.Text));
                            crews.Sort((x, y) => y.CrewID.CompareTo(x.CrewID));
                            CrewID.Text = crews.Count <= 0 ? "" : (crews[0].CrewID).ToString();

                            EmployeeGridView.Visible = false;
                            SelectUnitDDL.Visible = false;
                            FleetCategory.ClearSelection();
                            Done.Visible = false;
                            CreateCrew.Visible = false;
                            Cancel.Visible = false;
                        });
                    }

                    break;

                case "":
                    EmployeeGridView.Visible = false;
                    Done.Visible = false;
                    break;
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
                        crewManager.DeleteJobCard(int.Parse(e.CommandArgument.ToString()));
                        RefreshCurrentCrews();
                    });
                    break;

                case "DeleteCrew":
                    MessageUserControl.TryRun(() =>
                    {
                        CrewController crewManager = new CrewController();
                        crewManager.DeleteCrew(int.Parse(e.CommandArgument.ToString()));
                        RefreshCurrentCrews();
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
            SiteTypeID.Text = "1";
            DataPager topPager = (DataPager)RouteListView.FindControl("TopDataPager");
            DataPager bottomPager = (DataPager)RouteListView.FindControl("BottomDataPager");
            topPager.SetPageProperties(0, topPager.PageSize, true);
            bottomPager.SetPageProperties(0, bottomPager.PageSize, true);
            RouteListView.Visible = true;
        }

        protected void BRoute_Click(object sender, EventArgs e)
        {
            SiteTypeID.Text = "2";
            DataPager topPager = (DataPager)RouteListView.FindControl("TopDataPager");
            DataPager bottomPager = (DataPager)RouteListView.FindControl("BottomDataPager");
            topPager.SetPageProperties(0, topPager.PageSize, true);
            bottomPager.SetPageProperties(0, bottomPager.PageSize, true);
            RouteListView.Visible = true;
        }

        protected void WRoute_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in RouteListView.Items)
            {

            }
        }

        protected void PRoute_Click(object sender, EventArgs e)
        {

        }

        protected void GRoute_Click(object sender, EventArgs e)
        {

        }

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

                            CrewController crewManager = new CrewController();
                            message = crewManager.AddJobCard(int.Parse(CrewID.Text), int.Parse(e.CommandArgument.ToString()), int.Parse(list.SelectedValue));
                            RefreshCurrentCrews();
                        }
                    });

                    if (!string.IsNullOrEmpty(message))
                    {
                        InfoUserControl.ShowInfo("The following Crew(s) are already assigned to this same Site:  " + message);
                    }
                    break;

                case "Finish":
                    SiteMenu.Visible = false;
                    RouteListView.Visible = false;
                    break;
            }
            
        }

    }
}