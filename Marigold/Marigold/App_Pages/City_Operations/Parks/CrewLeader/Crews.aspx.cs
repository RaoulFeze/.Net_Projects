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
                    Response.Redirect("~/Account/Login.aspx");
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
                Drivers.DataSource = null;
                Drivers.DataBind();
                Drivers.Visible = false;
                DriverPager.Visible = false;
                MessageUserControl.TryRun(() =>
                {
                    List<Equipment> equipments = fleet.GetEquipments(yardId);
                    SelectUnitDDL.DataSource = equipments;
                    SelectUnitDDL.DataTextField = nameof(Equipment.EquipmentNumber);
                    SelectUnitDDL.DataValueField = nameof(Equipment.EquipmentID);
                    SelectUnitDDL.DataBind();
                    SelectUnitDDL.Items.Insert(0, "Select an Equipment");
                });
            }
            else if (selected == "2")
            {
                Drivers.DataSource = null;
                Drivers.DataBind();
                Drivers.Visible = false;
                DriverPager.Visible = false;
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
                
            }
            else if(selected == "2")
            {
                MessageUserControl.TryRun(() =>
                {
                    DriverPager.SetPageProperties(0, DriverPager.PageSize, true);
                    RefreshDriverListView();
                    Drivers.Visible = true;
                    DriverPager.Visible = true;
                });

            }
        }

        //This Method resets the DataPager
        protected void Drivers_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                DriverPager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                RefreshDriverListView();
            });
        }

        //This Method makes the RadioButton fucntional
        protected void SelectedDriver_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedButton = new RadioButton();
            selectedButton = (RadioButton)sender;
            foreach (ListViewItem listItem in this.Drivers.Items)
            {
                (listItem.FindControl("SelectedDriver") as RadioButton).Checked = false;
            }
            selectedButton.Checked = true;
        }

        //this Methos refreshes the ListView
        protected void RefreshDriverListView()
        {
            int unitId = int.Parse(SelectUnitDDL.SelectedValue);
            int yardId = int.Parse(YardID.Text);
            FleetController fleetManager = new FleetController();
            List<TruckDriver> drivers = fleetManager.GetTruckDrivers(yardId, unitId);
            Drivers.DataSource = drivers;
            Drivers.DataBind();
        }
    }
}