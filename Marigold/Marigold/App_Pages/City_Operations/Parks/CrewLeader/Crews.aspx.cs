using System;
using System.Collections.Generic;

#region Additional Namespaces
using System.Configuration;
using System.Web.UI.WebControls;
using Marigold.Security;
using MarigoldSystem.BLL;
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

                MessageUserControl.TryRun(() =>
                {
                    SecurityController security = new SecurityController();
                    UserId.Text = (security.GetCurrentUserId(Context.User.Identity.GetUserName())).ToString();
                    EmployeeController employee = new EmployeeController();
                    YardID.Text = employee.GetYardID(int.Parse(UserId.Text)).ToString();
                });
            }
            else
            {
                Response.Redirect("~/Security/AccessDenied.aspx");
            }
        }
        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        //This Method sets the DropDownList.
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
                Next.Visible = false;
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
                Next.Visible = false;
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

        //Gets hold of the DataPager to reset it when user changes Truck/Equipment
        protected void SelectUnitDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = FleetCategory.SelectedValue;

            if (selected == "1")
            {
                EmployeeGridView.PageIndex = 0;
                EmployeeGridView.Columns[4].Visible = false;
                EmployeeGridView.Visible = true;
                RefreshDriverList(1);
            

            }
            else if(selected == "2")
            {
                EmployeeGridView.PageIndex = 0;
                //DriverPager.SetPageProperties(0, DriverPager.PageSize, true);
                EmployeeGridView.Visible = true;
                EmployeeGridView.Columns[4].Visible = true;
                RefreshDriverList(2);
            }
        }

        //This Method resets the DataPager
        protected void Drivers_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                //DriverPager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                //RefreshDriverListView();
            });
        }

        //This Method makes the RadioButton fucntional
        protected void SelectedDriver_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedButton = new RadioButton();
            selectedButton = (RadioButton)sender;
            foreach (GridViewRow row in EmployeeGridView.Rows)
            {
                (row.FindControl("SelectedDriver") as RadioButton).Checked = false;
            }
            selectedButton.Checked = true;
            AddMember.Visible = true;
            Next.Visible = true;
        }

        //this Methos refreshes the ListView
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

        protected void EmployeeGridView_PageIndexChanged(object sender, EventArgs e)
        {
          
        }

        protected void EmployeeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int type = int.Parse(FleetCategory.SelectedValue);
            GridView grid = sender as GridView;
            grid.PageIndex = e.NewPageIndex;
            grid.DataBind();
            RefreshDriverList(type);
        }
    }
}