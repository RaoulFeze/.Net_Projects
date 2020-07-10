<%@ Page Language="C#" MasterPageFile="~/SiteMasters/CityOperations/Parks/CrewLeader.master" AutoEventWireup="true" CodeBehind="Crews.aspx.cs" Inherits="Marigold.App_Pages.City_Operations.Parks.CrewLeader.Crews" Title="Crews" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>
<%@ Register Src="~/UserControls/InfoUserControl.ascx" TagPrefix="uc1" TagName="InfoUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <%-- --------------------------------------------------------------------------------------------------- --%>
    <uc1:MessageUserControl runat="server" id="MessageUserControl" />
    <asp:Label ID="YardID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="UserId" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="Refresh" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="CrewID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="SiteTypeID" runat="server" Text="1" Visible="false"></asp:Label>
    <%-- --------------------------------------------------------------------------------------------------- --%>
    <div class="info-message container">
        <uc1:InfoUserControl runat="server" ID="InfoUserControl" />
    </div>
    <div class="crew-content-layout">
        <div class="sites-pane">
            <div class="menu-control">
                <ul class="nav nav-tabs " role="tablist">
                    <li class="nav-item">
                        <asp:LinkButton ID="ARoute" runat="server" CssClass="nav-link tab-menu active" OnClick="ARoute_Click">A Routes</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="BRoute" runat="server" CssClass="nav-link tab-menu" OnClick="BRoute_Click">B Routes</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="WRoute" runat="server" CssClass="nav-link tab-menu" OnClick="WRoute_Click">Watering Routes</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="PRoute" runat="server" CssClass="nav-link tab-menu" OnClick="PRoute_Click">Planting Routes</asp:LinkButton>
                    </li>
                    <li class="nav-item">
                        <asp:LinkButton ID="GRoute" runat="server" CssClass="nav-link tab-menu" OnClick="GRoute_Click">Grass Routes</asp:LinkButton>
                    </li>
                </ul>
            </div>
            
            <div class="route">
                <div class="table table-striped table-bordered Cssgrid container-fluid">
                    <asp:ListView ID="RouteListView" runat="server"
                        Visible="false"
                        DataSourceID="ObjectDataSource1"
                        OnItemCommand="RouteListView_ItemCommand">
                        <AlternatingItemTemplate>
                            <tr>
                                <td>
                                    <%# Container.DataItemIndex + 1%> </td>
                                <td>
                                    <asp:Label Text='<%# Eval("Pin") %>' runat="server" ID="PinLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Community") %>' runat="server" ID="CommunityLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Address") %>' runat="server" ID="AddressLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Area") %>' runat="server" ID="AreaLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Count") %>' runat="server" ID="CountLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("LastDate", "{0:MMM-dd}") %>' runat="server" ID="LastDateLabel" /></td>
                                <td>
                                    <asp:DropDownList ID="SelectTask" runat="server" DataSourceID="TaskODS" DataTextField="Description" DataValueField="TaskID"></asp:DropDownList></td>
                                <td>
                                    <asp:LinkButton ID="AddSite" runat="server" CommandArgument='<%# Eval("SiteID") %>' CommandName="AddSite">
                                        <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                                    </asp:LinkButton>

                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <table runat="server" style="">
                                <tr>
                                    <td>No data was returned.</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <ItemTemplate>
                            <tr style="">
                                <td>
                                    <%# Container.DataItemIndex + 1%> </td>
                                <td>
                                    <asp:Label Text='<%# Eval("Pin") %>' runat="server" ID="PinLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Community") %>' runat="server" ID="CommunityLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Address") %>' runat="server" ID="AddressLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Area") %>' runat="server" ID="AreaLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("Count") %>' runat="server" ID="CountLabel" /></td>
                                <td>
                                    <asp:Label Text='<%# Eval("LastDate", "{0:MMM-dd}") %>' runat="server" ID="LastDateLabel" /></td>
                                <td>
                                    <asp:DropDownList ID="SelectTask" runat="server" DataSourceID="TaskODS" DataTextField="Description" DataValueField="TaskID"></asp:DropDownList></td>
                                <td>
                                    <asp:LinkButton ID="AddSite" runat="server" CommandArgument='<%# Eval("SiteID") %>' CommandName="AddSite">
                                        <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                                    </asp:LinkButton>

                                </td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server" style="text-align: center">
                                        <div class="ml-auto">
                                            <asp:Button ID="Button2" runat="server" Text="Finish" />
                                        </div>
                                        <asp:DataPager runat="server" ID="TopDataPager" PageSize="20">
                                            <Fields>
                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server">
                                        <table runat="server" id="itemPlaceholderContainer" style="" border="0">
                                            <tr runat="server" style="">
                                                <th runat="server"></th>
                                                <th runat="server">Pin</th>
                                                <th runat="server">Community</th>
                                                <th runat="server">Description</th>
                                                <th runat="server">Address</th>
                                                <th runat="server">Area</th>
                                                <th runat="server">Count</th>
                                                <th runat="server">L.D.W</th>
                                                <th runat="server">Task</th>
                                                <th runat="server">Add</th>
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder"></tr>
                                        </table>

                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" style="text-align: center">
                                        <div class="ml-auto">
                                            <asp:Button ID="Button1" runat="server" Text="Finish" />
                                        </div>
                                        <asp:DataPager runat="server" ID="BottomDataPager" PageSize="20">
                                            <Fields>
                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                <asp:NumericPagerField></asp:NumericPagerField>
                                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>

                    </asp:ListView>
                </div>
            </div>
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
                &nbsp;
                &nbsp;
                <asp:Button ID="CreateCrew" runat="server" Text="Create a New Crew" Visible="false" CssClass="crew-member-button" OnClick="CreateCrew_Click" />
                <div class="ml-auto">
                    <asp:Button ID="Cancel" runat="server" Text="Cancel" CssClass="cancel-crew-button" Visible="false" OnClick="Cancel_Click" />
                    <asp:Button ID="Done" runat="server" Text="Done" Visible="false" CssClass="done-control-button" OnClick="Done_Click" />
                </div>
            </div>
            <div class="crew-selection-pane">
                <div class="selectDriver">
                    <asp:GridView ID="EmployeeGridView" runat="server"
                        Visible="false"
                        AutoGenerateColumns="False"
                        OnPageIndexChanging="EmployeeGridView_PageIndexChanging"
                        CssClass="table table-striped table-bordered Cssgrid"
                        BorderColor="Black"
                        BorderWidth="1"
                        AllowPaging="True"
                        OnRowCommand="EmployeeGridView_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="EmployeeID" SortExpression="EmployeeID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("EmployeeID") %>' ID="EmployeeID"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" SortExpression="">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                <ItemTemplate>
                                    <div class="align-cell-left">
                                        <asp:Label runat="server" Text='<%# Bind("Name") %>' ID="Label2"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone" SortExpression="Phone">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Phone") %>' ID="Label3"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="License" SortExpression="License">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("License") %>' ID="Label4"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trailer" SortExpression="Trailer">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" Checked='<%# Bind("Trailer") %>' Enabled="false" ID="CheckBox1"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Driver" SortExpression="">
                                <ItemTemplate>
                                    <asp:RadioButton ID="SelectedDriver" runat="server" AutoPostBack="true" OnCheckedChanged="SelectedDriver_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Add" SortExpression="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="AddCrewMember" runat="server" CommandArgument='<%# Eval("EmployeeID") %>' CommandName="AddMember">
                                        <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="current-crew-pane">
                <asp:Repeater ID="AllCurrentCrews" runat="server"
                    ItemType="MarigoldSystem.Data.DTO_s.CurrentCrews"
                    OnItemCommand="AllCurrentCrews_ItemCommand">
                    <ItemTemplate>
                        <div class="current-crew-box offset-1">
                            <div class="current-crew-header offset-5">
                                <h4 class="crew-header">
                                    <asp:LinkButton ID="SelectCrew" runat="server" CommandArgument='<%#Item.CrewID %>' CommandName="SelectedCrew">
                                        CREW:  <%#Item.Description %>
                                    </asp:LinkButton>
                                </h4>
                                <div class="crew-glyphicon-remove">
                                    <asp:LinkButton ID="RemoveCrew" runat="server" CommandArgument='<%# Item.CrewID %>' CommandName="DeleteCrew">
                                    <span aria-hidden="true" class="glyphicon glyphicon-remove" ></span> 
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="table table-striped table-bordered container-xl">
                                <div class="crew-jobcard">
                                    <div class="crew-member-box ">
                                        <asp:GridView ID="CrewMemberGridView" runat="server"
                                            AutoGenerateColumns="false"
                                            CssClass=""
                                            BorderWidth="0"
                                            BackColor="White"
                                            GridLines="None"
                                            DataSource="<%#Item.Crew %>">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Phone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Phone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="RemoveEmployee" runat="server" CommandArgument='<%# Eval("CrewMemberID") %>' CommandName="RemoveMember">
                                                <span aria-hidden="true" class="glyphicon glyphicon-remove" ></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="job-card-box">
                                        <asp:GridView ID="JobCardGridView" runat="server"
                                            AutoGenerateColumns="false"
                                            CssClass=""
                                            BorderWidth="1"
                                            BackColor="White"
                                            GridLines="None"
                                            DataSource="<%#Item.JobCards %>">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pin">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Name" runat="server" Text='<%# Eval("Pin") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Address" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="RemoveSite" runat="server" CommandArgument='<%# Eval("JobCardID") %>' CommandName="DeleteJobCard" OnClientClick="return ConfirmRemoveSite()">
                                                <span aria-hidden="true" class="glyphicon glyphicon-remove" ></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="summary">
        </div>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetRouteSummaries" TypeName="MarigoldSystem.BLL.RouteController">
        <SelectParameters>
            <asp:ControlParameter ControlID="YardID" PropertyName="Text" Name="yardId" Type="Int32"></asp:ControlParameter>
            <asp:ControlParameter ControlID="SiteTypeID" PropertyName="Text" Name="siteTypeId" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="TaskODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetTasks" TypeName="MarigoldSystem.BLL.RouteController"></asp:ObjectDataSource>
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            xPos = $get('SelectedDriver').scrollLeft;
            yPos = $get('SelectedDriver').scrollTop;
        }

        function EndRequestHandler(sender, args) {
            $get('SelectedDriver').scrollLeft = xPos;
            $get('SelectedDriver').scrollTop = yPos;
        }
    </script>
</asp:Content>