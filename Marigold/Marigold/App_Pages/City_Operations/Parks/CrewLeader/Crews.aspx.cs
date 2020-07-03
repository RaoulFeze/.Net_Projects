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
                });

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

        //This Method sets the Unit DropDownList.
        //  Resets the Employeegridview to null
        //  Turns the visibility of the Buttons controls to false
        protected void FleetCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int yardId = int.Parse(YardID.Text);
            FleetController fleet = new FleetController();

            string selected = FleetCategory.SelectedValue;
            if (selected == "1")
            {
                EmployeeGridView.DataSource = null;
                EmployeeGridView.DataBind();
                EmployeeGridView.Visible = false;
                AddMember.Visible = false;
                Done.Visible = false;
                MessageUserControl.TryRun(() =>
                {
                    List<Equipment> equipments = fleet.GetEquipments(yardId);
                    SelectUnitDDL.DataSource = equipments;
                    SelectUnitDDL.DataTextField = nameof(Equipment.EquipmentNumber);
                    SelectUnitDDL.DataValueField = nameof(Equipment.EquipmentID);
                    SelectUnitDDL.Visible = true;
                    SelectUnitDDL.DataBind();
                    SelectUnitDDL.Items.Insert(0, "Select an Equipment");
                });
            }
            else if (selected == "2")
            {
                EmployeeGridView.DataSource = null;
                EmployeeGridView.DataBind();
                EmployeeGridView.Visible = false;
                AddMember.Visible = false;
                Done.Visible = false;
                MessageUserControl.TryRun(() =>
                {
                    List<Truck> units = fleet.GetUnits(yardId);
                    SelectUnitDDL.DataSource = units;
                    SelectUnitDDL.DataTextField = nameof(Truck.TruckDescription);
                    SelectUnitDDL.DataValueField = nameof(Truck.TruckID);
                    SelectUnitDDL.Visible = true;
                    SelectUnitDDL.DataBind();
                    SelectUnitDDL.Items.Insert(0, "Select a Truck");
                });
            }
        }

        //This Method Displays the drivers of a given unit (Equipment/Truck)
        //  Resets the GridView Pager
        //  Sets the Label control for refreshing the EmployeeGridView to "Driver"
        protected void SelectUnitDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = FleetCategory.SelectedValue;
            Refresh.Text = "Driver";

            if (selected == "1")
            {
                EmployeeGridView.PageIndex = 0;
                EmployeeGridView.Columns[4].Visible = false;
                EmployeeGridView.Columns[5].Visible = true;
                EmployeeGridView.Columns[6].Visible = true;
                EmployeeGridView.Columns[7].Visible = false;
                EmployeeGridView.Visible = true;
                RefreshDriverList(1);
            

            }
            else if(selected == "2")
            {
                EmployeeGridView.PageIndex = 0;
                //DriverPager.SetPageProperties(0, DriverPager.PageSize, true);
                EmployeeGridView.Visible = true;
                EmployeeGridView.Columns[4].Visible = true;
                EmployeeGridView.Columns[5].Visible = true;
                EmployeeGridView.Columns[6].Visible = true;
                EmployeeGridView.Columns[7].Visible = false;
                RefreshDriverList(2);
            }
        }

        //This Method makes the RadioButton fucntional
        //  Sets the Crew Member button control On/Off based on the unit Selected
        protected void SelectedDriver_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedButton = new RadioButton();
            selectedButton = (RadioButton)sender;
            foreach (GridViewRow row in EmployeeGridView.Rows)
            {
                (row.FindControl("SelectedDriver") as RadioButton).Checked = false;
            }
            selectedButton.Checked = true;

            string selected = FleetCategory.SelectedValue;
            if(selected == "1")
            {
                AddMember.Visible = false;
            }
            else
            {
                AddMember.Visible = true;
            }
            Done.Visible = true;
        }

        //this Methods refreshes the Driver List
        protected void RefreshDriverList(int type)
        {
            MessageUserControl.TryRun(() =>
            {
                int unitId = int.Parse(SelectUnitDDL.SelectedValue);
                int yardId = int.Parse(YardID.Text);
                FleetController fleetManager = new FleetController();
                List<Driver> drivers = fleetManager.GetTruckDrivers(yardId, unitId, type);
                EmployeeGridView.DataSource = drivers;
                EmployeeGridView.DataBind();

            });

        }

        //This method refreshes The EmployeeGirview after a paging occured
        //  based on whether the user is selecting the Driver or the Crew members
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
                //foreach (GridViewRow row in EmployeeGridView.Rows)
                //{
                //    var chkBox = row.FindControl("SelectedMember") as CheckBox;
                //    IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;
                //    if (chkBox.Checked)
                //    {
                //        PersistRowIndex(container.DataItemIndex);
                //    }
                //    else
                //    {
                //        RemoveRowIndex(container.DataItemIndex);
                //    }
                //}
                GridView grid = sender as GridView;
                grid.PageIndex = e.NewPageIndex;
                grid.DataBind();
                RefreshCrewMember();
                //RePopulateCheckBoxes();

            }
        }
            #region CheckBox State Persistance
            protected void PersistRowIndex(int index)
            {
                if (!SelectedMembersIndex.Exists(i => i == index))
                {
                    SelectedMembersIndex.Add(index);
                }
            }

            protected void RemoveRowIndex(int index)
            {
                if (SelectedMembersIndex.Exists(i => i == index))
                {
                    SelectedMembersIndex.Remove(index);
                }
            }

            protected List<Int32> SelectedMembersIndex
            {
                get
                { 
                    if(ViewState["SELECTED_MEMBERS_INDEX"] == null)
                    {
                        ViewState["SELECTED_MEMBERS_INDEX"] = new List<Int32>();
                    }
                    return (List<Int32>)ViewState["SELECTED_MEMBERS_INDEX"];
                }
            }

            protected void RePopulateCheckBoxes()
            {
                foreach(GridViewRow row in EmployeeGridView.Rows)
                {
                    var chkBox = row.FindControl("SelectedMember") as CheckBox;
                    IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;

                    if(SelectedMembersIndex != null)
                    {
                        if (SelectedMembersIndex.Exists(i => i == container.DataItemIndex))
                        {
                            chkBox.Checked = true;
                        }
                    }
                }
            }
        #endregion

        //This method creates a new cew and add the Driver as the first crew member
        //  Refreshes the current crews pane
        protected void AddMember_Click(object sender, EventArgs e)
        {
            int driverId = 0;
            //Retrieve the selected DriverID and save in a label control
            foreach (GridViewRow row in EmployeeGridView.Rows)
            {
                if((row.FindControl("SelectedDriver") as RadioButton).Checked == true)
                {
                  driverId = int.Parse((row.FindControl("EmployeeID") as Label).Text);
                }
            }

            CrewController crewManager = new CrewController();
            InfoUserControl.TryRun(() =>
            {
                crewManager.CreateCrew(int.Parse(SelectUnitDDL.SelectedValue), driverId);
                Refresh.Text = "Member";
                EmployeeGridView.PageIndex = 0;
                RefreshCrewMember();
            }, "Successfully added");
            RefreshCurrentCrews();
        }

        //This method populates the list of all employees for crew member selection
        // Turns off all unnecessary column in the EmployeeGridView
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
            });
        }

        //This method populates the currents crews and their job cards
        protected void RefreshCurrentCrews()
        {
            InfoUserControl.TryRun(() =>
            {
                CrewController crewManager = new CrewController();
                List<CurrentCrews> crews = crewManager.GetCurrentCrews(int.Parse(YardID.Text));
                crews.Sort((x, y) => y.CrewID.CompareTo(x.CrewID));
                int lenght = crews.Count;
                CrewID.Text = (crews[0].CrewID).ToString();
                AllCurrentCrews.DataSource = crews;
                AllCurrentCrews.DataBind();
            });
        }
        //This Methods 
        protected void Done_Click(object sender, EventArgs e)
        {
            //int truckId = int.Parse(SelectUnitDDL.SelectedValue);
            //int driverId = int.Parse(DriverID.Text);
            //List<int> memberIds = new List<int>();
            //string test = "";
            //foreach(GridViewRow row in EmployeeGridView.Rows)
            //{
            //    var chkBox = row.FindControl("SelectedMember") as CheckBox;
            //    IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;

            //    if (SelectedMembersIndex != null)
            //    {
            //        if (SelectedMembersIndex.Exists(i => i == container.DataItemIndex))
            //        {
            //            chkBox.Checked = true;
            //            test += ((row.FindControl("EmployeeID") as Label).Text).ToString() + "  ";
            //        }
            //    }





                //if ((row.FindControl("SelectedMember") as CheckBox).Checked == true)
                //{
                //    memberIds.Add(int.Parse((row.FindControl("EmployeeID") as Label).Text));
                //    test += ((row.FindControl("EmployeeID") as Label).Text).ToString() + "  ";
                //}
            //}
            //Label1.Text = test;
        }
     

        protected void EmployeeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Add a Crew Member to the curent Crew
            //Label1.Text = (e.CommandArgument).ToString();
        }

        protected void AllCurrentCrews_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}