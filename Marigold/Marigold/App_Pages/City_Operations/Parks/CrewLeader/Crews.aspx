<%@ Page Language="C#" MasterPageFile="~/SiteMasters/CityOperations/Parks/CrewLeader.master" AutoEventWireup="true" CodeBehind="Crews.aspx.cs" Inherits="Marigold.App_Pages.City_Operations.Parks.CrewLeader.Crews" Title="Crews" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <%-- --------------------------------------------------------------------------------------------------- --%>
    <uc1:MessageUserControl runat="server" id="MessageUserControl" />
    <asp:Label ID="YardID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="UserId" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="UnitID" runat="server" Text="1" Visible="false"></asp:Label>
    <%-- --------------------------------------------------------------------------------------------------- --%>
    <div class="crew-content-layout">
        <div class="sites-pane">
            <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="nav-link"  data-toggle="tab" role="tab">A Routes</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="nav-link active" data-toggle="tab" role="tab">B Routes</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="nav-link" data-toggle="tab" role="tab">Watering Routes</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="nav-link" data-toggle="tab" role="tab">Planting Routes</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="LinkButton5" runat="server" CssClass="nav-link" data-toggle="tab" role="tab">Grass Routes</asp:LinkButton>
                    </li>
                </ul>
        </div>
        <div class="selection-column">
            <div class="truck-selection-pane">
                <div class="fleet-category-selection">
                    <asp:RadioButtonList ID="FleetCategory" runat="server"
                        RepeatDirection="Horizontal" 
                        AutoPostBack="true"
                        OnSelectedIndexChanged="FleetCategory_SelectedIndexChanged">
                        <asp:ListItem Value="1">&nbsp;Equipments &nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">&nbsp;Trucks &nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                &nbsp;
                &nbsp;
                <asp:DropDownList ID="SelectUnitDDL" runat="server" Visible="false" OnSelectedIndexChanged="SelectUnitDDL_SelectedIndexChanged" AutoPostBack="true" CssClass="UnitDDL"></asp:DropDownList>
            </div>
            <div class="staff-selection-pane">
                <div class="driverListView">
                <asp:ListView ID="Drivers" runat="server"
                    OnPagePropertiesChanging="Drivers_PagePropertiesChanging">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #E9E9E9; color: black;">
                                        <td> <%# Container.DataItemIndex + 1%> </td>
                            <td>
                                <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("Phone") %>' runat="server" ID="PhoneLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("License") %>' runat="server" ID="LicenseLabel" /></td>
                            <td>
                                <asp:CheckBox Checked='<%# Eval("Trailer") %>' ID="CheckBox1" runat="server" Enabled="false"/></td>
                            <td>
                                <asp:RadioButton ID="SelectedDriver" runat="server" AutoPostBack="true" OnCheckedChanged="SelectedDriver_CheckedChanged" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #FFFFFF; color: black;">
                                        <td> <%# Container.DataItemIndex + 1%> </td>
                            <td>
                                <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("Phone") %>' runat="server" ID="PhoneLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("License") %>' runat="server" ID="LicenseLabel" /></td>
                            <td>
                                <asp:CheckBox Checked='<%# Eval("Trailer") %>' ID="CheckBox1" runat="server" Enabled="false"/></td>
                            <td>
                                <asp:RadioButton ID="SelectedDriver" runat="server" AutoPostBack="true" OnCheckedChanged="SelectedDriver_CheckedChanged" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; font-weight: normal; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                        <tr runat="server" style="background-color: #DCDCDC; color: black;">
                                            <th runat="server" style="width: 5px"></th>
                                            <th runat="server">Name</th>
                                            <th runat="server">Phone</th>
                                            <th runat="server">License</th>
                                            <th runat="server">Trailer</th>
                                            <th runat="server">Driver</th>
                                        </tr>
                                        <tr runat="server" id="itemPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                    </div>
                <div class="pager">
                <asp:DataPager runat="server" ID="DriverPager" PagedControlID="Drivers" PageSize="8"   style="text-align: center; font-family: Verdana, Arial, Helvetica, sans-serif; color: black"> 
                    <Fields>
                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                    </Fields>
                </asp:DataPager>

                </div>
            </div>
            <div class="crew-selection-pane">
                
            </div>
        </div>
        <div class="summary">
        </div>
    </div>
    <asp:ObjectDataSource ID="GetTruckDrivers" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetTruckDrivers" TypeName="MarigoldSystem.BLL.FleetController">
        <SelectParameters>
            <asp:ControlParameter ControlID="YardID" PropertyName="Text" Name="yardId" Type="Int32"></asp:ControlParameter>
            <asp:ControlParameter ControlID="UnitID" PropertyName="Text" Name="unitId" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
