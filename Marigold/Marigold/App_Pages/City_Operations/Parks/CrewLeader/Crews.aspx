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
    <asp:Label ID="DriverID" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="SiteTypeID" runat="server" Text="1" Visible="false"></asp:Label>
    <%-- --------------------------------------------------------------------------------------------------- --%>
    <div class="info-message container">
        <uc1:InfoUserControl runat="server" ID="InfoUserControl" />
        
    </div>
    <div class="crew-wrapper">
        <div class="crew-pane">
            <div class="filter-search">
                <div class="fleet-category-selection">
                    <asp:RadioButtonList ID="FleetCategory" runat="server"
                     CssClass="fleet-category"
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
                <asp:Button ID="CreateCrew" runat="server" Text="Add Crew Member" Visible="false" CssClass="btn-crew-member" OnClick="CreateCrew_Click" />
                <div class="ml-auto">
                    <asp:Button ID="Cancel" runat="server" Text="Cancel" CssClass="btn-cancel" Visible="false" OnClick="Cancel_Click" />
                    <asp:Button ID="Done" runat="server" Text="Done" Visible="false" CssClass="btn-done" OnClick="Done_Click" />
                </div>
            </div>
            <div class="selection-pane">
                <asp:GridView ID="EmployeeGridView" runat="server"
                    Visible="false"
                    AutoGenerateColumns="False"
                    OnPageIndexChanging="EmployeeGridView_PageIndexChanging"
                    CssClass="table table-striped table-bordered table-hover select-employee"
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
                        <asp:TemplateField HeaderText="" SortExpression="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" SortExpression="Name">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("Name") %>' ID="Label2"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone" SortExpression="Phone">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("Phone") %>' ID="Label3"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="License" SortExpression="License"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Bind("License") %>' ID="Label4"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trailer" SortExpression="Trailer"  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Checked='<%# Bind("Trailer") %>' Enabled="false" ID="CheckBox1"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Driver" SortExpression=""  ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:RadioButton ID="SelectedDriver" runat="server" AutoPostBack="true" OnCheckedChanged="SelectedDriver_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Add" SortExpression="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="AddCrewMember" runat="server" CommandArgument='<%# Eval("EmployeeID") %>' CommandName="AddMember">
                                        <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="current-crews">
                <asp:Repeater ID="AllCurrentCrews" runat="server"
                    ItemType="MarigoldSystem.Data.DTO_s.CurrentCrews"
                    OnItemCommand="AllCurrentCrews_ItemCommand">
                    <ItemTemplate>
                        <div class="crew-header">
                            <h4>
                                <asp:LinkButton ID="SelectCrew" runat="server" CommandArgument='<%#Item.CrewID %>' CommandName="SelectedCrew" CssClass="crew-linkbutton">
                                        <span title="Click to update this crew">CREW:  <%#Item.Description %></span>
                                </asp:LinkButton>
                            </h4>
                            <div class="crew-glyphicon-remove">
                                <asp:LinkButton ID="RemoveCrew" runat="server" CommandArgument='<%# Item.CrewID %>' CommandName="DeleteCrew" CssClass="crew-linkbutton">
                                    <span aria-hidden="true" class="glyphicon glyphicon-remove" title="Delete all crew members and job cards" ></span> 
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="crew-members">
                            <asp:GridView ID="CrewMemberGridView" runat="server"
                                AutoGenerateColumns="false"
                                CssClass="table table-striped table-bordered "
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
                                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="RemoveEmployee" runat="server" CommandArgument='<%# Eval("CrewMemberID") %>' CommandName="RemoveMember">
                                                <span aria-hidden="true" class="glyphicon glyphicon-trash" title="Remove employee from the crew" ></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="job-cards">
                            <asp:GridView ID="JobCardGridView" runat="server"
                                AutoGenerateColumns="false"
                                CssClass="table table-striped table-bordered container"
                                BorderWidth="1"
                                BackColor="White"
                                GridLines="None"
                                DataSource="<%#Item.CardCrew %>">
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
                                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="RemoveSite" runat="server" CommandArgument='<%# Eval("JobCardCrewID") %>' CommandName="DeleteJobCard" OnClientClick="return ConfirmRemoveSite()">
                                                <span aria-hidden="true" class="glyphicon glyphicon-trash" title="Remove Job Card from the crew"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="jobcard-routes">
            <h3>
                <asp:Label ID="UnitReportHeader" runat="server" Text="Unit Reports"></asp:Label>
            </h3>
            <asp:GridView ID="UnitReoprtGV" runat="server" 
                AutoGenerateColumns="False" 
                OnRowCommand="UnitReoprtGV_RowCommand"
                CssClass="table table-striped table-bordered container close-jobcards"
                AllowPaging="True" 
                PageSize="5"
                OnPageIndexChanging="UnitReoprtGV_PageIndexChanging">
                <Columns>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" SortExpression="Date"  ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Date", "{0:yyyy-MMM-dd}") %>' ID="Date"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit" SortExpression="Unit">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Unit") %>' ID="Unit"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KM Start" SortExpression="KM_Start">
                        <ItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("KM_Start") %>' ID="KM_Start"  CssClass ="resize-textbox"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KM End" SortExpression="KM_End">
                        <ItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("KM_End") %>' ID="KM_End" CssClass ="resize-textbox"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Damages / Comments" SortExpression="Comment">
                        <ItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("Comment") %>' ID="Comment" 
                                TextMode="MultiLine" 
                                CssClass ="resize-textbox-multiline" 
                                PlaceHolder="Drag down the bottom right corner to enlarge the text area"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="Submit" runat="server"  CommandArgument='<%# Eval("CrewID") %>' CommandName="SaveUnitReport">
                                <span aria-hidden="true" class="glyphicon glyphicon-floppy-disk" title="Save"></span>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
 

            <h3>
                <asp:Label ID="JobcardTitle" runat="server" Text="Active Job Cards"></asp:Label>
            </h3>
            <div class="menu-control" id="SiteMenu" runat="server" visible="false">
                <ul class="routes-nav">
                    <li class="nav-tab  " onclick="setActiveTab(this)">
                        <asp:LinkButton ID="ARoute" runat="server" CssClass="tab-menu" OnClick="ARoute_Click">A Routes</asp:LinkButton>
                    </li>
                    <li class="nav-tab" onclick="setActiveTab(this)">
                        <asp:LinkButton ID="BRoute" runat="server" CssClass="tab-menu" OnClick="BRoute_Click">B Routes</asp:LinkButton>
                    </li>
                    <li class="nav-tab" onclick="setActiveTab(this)">
                        <asp:LinkButton ID="GRoute" runat="server" CssClass="tab-menu" OnClick="GRoute_Click">Grass Routes</asp:LinkButton>
                    </li>
                    <li class="nav-tab" onclick="setActiveTab(this)">
                        <asp:LinkButton ID="WRoute" runat="server" CssClass="tab-menu" OnClick="WRoute_Click">Watering Routes</asp:LinkButton>
                    </li>
                    <li class="nav-tab" onclick="setActiveTab(this)">
                        <asp:LinkButton ID="PRoute" runat="server" CssClass="tab-menu" OnClick="PRoute_Click">Planting Routes</asp:LinkButton>
                    </li>
                </ul>
            </div>
             <asp:GridView ID="JobCardStatusGridView" runat="server" 
                    AutoGenerateColumns="False" 
                    OnRowCommand="JobCardStatusGridView_RowCommand"
                    AllowPaging="True"
                    PageSize ="5"
                    CssClass="table table-striped table-bordered container close-jobcards" 
                    OnPageIndexChanging="JobCardStatusGridView_PageIndexChanging">
                 <Columns>
                     <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                             <%# Container.DataItemIndex + 1%>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Assigned Date" SortExpression="AssignedDate"  ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                             <asp:Label runat="server" Text='<%# Bind("AssignedDate", "{0:yyyy-MMM-dd}") %>' ID="Label6"></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Pin" SortExpression="Pin"  ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                             <asp:Label runat="server" Text='<%# Bind("Pin") %>' ID="Label2"></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Community" SortExpression="Community">
                         <ItemTemplate>
                             <asp:Label runat="server" Text='<%# Bind("Community") %>' ID="Label3"></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Description" SortExpression="Description">
                         <ItemTemplate>
                             <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="Label4"></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Address" SortExpression="Address">
                         <ItemTemplate>
                             <asp:Label runat="server" Text='<%# Bind("Address") %>' ID="Label5"></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Task" SortExpression="Task">
                         <ItemTemplate>
                             <asp:Label runat="server" Text='<%# Bind("Task") %>' ID="Label8"></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Completed Date" SortExpression="Completed Date">
                         <ItemTemplate>
                             <asp:TextBox runat="server" Text='<%# Bind("CompletedDate", "{0:yyyy-MMM-dd}") %>' ID="CompletedDate" CssClass ="resize-textbox"></asp:TextBox>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField>
                         <ItemTemplate>
                             <span title="Change the completed date"><asp:ImageButton ID="CalendarImage" runat="server" ImageUrl="~/Images/calendar.png" Commandname="ChangeDate"/></span>
                             <asp:Calendar ID="Calendar" runat="server" 
                                 OnSelectionChanged="CloseDateCalendar_SelectionChanged" 
                                 OnDayRender="CloseDateCalendar_DayRender"
                                 TodayDayStyle-BackColor="ForestGreen"
                                 TodayDayStyle-ForeColor="white"
                                 Visible="false" Height="100px" Width="100px">
                             </asp:Calendar>
                         </ItemTemplate>
                     </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="Save" runat="server"  CommandArgument='<%# Eval("JobCardID") %>' CommandName="SaveJobCard">
                                <span aria-hidden="true" class="glyphicon glyphicon-floppy-disk" title="Save"></span>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
                </asp:GridView>
            <div class="table table-striped table-bordered jobsite-lv">
                    <asp:ListView ID="RouteListView" runat="server"
                       
                        Visible="false"
                        DataSourceID="Routes_Summary"
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
                                    <asp:DropDownList ID="SelectTask" runat="server" DataSourceID="TaskODS" DataTextField="Description" DataValueField="TaskID" ></asp:DropDownList></td>
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
                                    <asp:DropDownList ID="SelectTask" runat="server" DataSourceID="TaskODS" DataTextField="Description" DataValueField="TaskID" ></asp:DropDownList></td>
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
                                        <div class="btn-finish">
                                            <asp:Button ID="Button2" runat="server" Text="Finish" CommandName="Finish"/>
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
                                        <div class="btn-finish">
                                            <asp:Button ID="Finish_Button" runat="server" Text="Finish"  CommandName="Finish"/>
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
        <div class="summary">
            <div class="summaryElements">
              <%--  <div >
                    <asp:Label ID="ActiveCrews" runat="server" Text="Number of Crews: "></asp:Label>
                    <asp:Label ID="NumberCrews" runat="server" Text="3"></asp:Label>&nbsp;&nbsp;
                </div>
                <div>
                    <asp:Label ID="FieldWorkers" runat="server" Text="Total Field Workers: "></asp:Label>
                    <asp:Label ID="TotalFieldWorker" runat="server" Text="20"></asp:Label>&nbsp;&nbsp;
                </div>
                <div>
                    <asp:Label ID="SBM" runat="server" Text="SBM: "></asp:Label>
                    <asp:Label ID="TotalSBM" runat="server" Text="20"></asp:Label>&nbsp;&nbsp;
                </div>
                <div>
                    <asp:Label ID="Planting" runat="server" Text="Planting: "></asp:Label>
                    <asp:Label ID="TotalPlanting" runat="server" Text="20"></asp:Label>&nbsp;&nbsp;
                </div>
                <div>
                    <asp:Label ID="Watering" runat="server" Text="Watering: "></asp:Label>
                    <asp:Label ID="TotalWatering" runat="server" Text="20"></asp:Label>&nbsp;&nbsp;
                </div>
                <div>
                    <asp:Label ID="Mulching" runat="server" Text="Mulcing: "></asp:Label>
                    <asp:Label ID="TotalMulching" runat="server" Text="20"></asp:Label>&nbsp;&nbsp;
                </div>
                <div>
                    <asp:Label ID="Uprooting" runat="server" Text="Uprooting: "></asp:Label>
                    <asp:Label ID="TotalUprooting" runat="server" Text="20"></asp:Label>&nbsp;&nbsp;
                </div>
                <div>
                    <asp:Label ID="Grass" runat="server" Text="Grass: "></asp:Label>
                    <asp:Label ID="TotalGrass" runat="server" Text="20"></asp:Label>
                </div>--%>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="TaskODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetTasks" TypeName="MarigoldSystem.BLL.RouteController">
        <SelectParameters>
            <asp:ControlParameter ControlID="SiteTypeID" PropertyName="Text" Name="siteTypeId" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
     <asp:ObjectDataSource ID="Routes_Summary" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetRouteSummaries" TypeName="MarigoldSystem.BLL.RouteController">
        <SelectParameters>
            <asp:ControlParameter ControlID="YardID" PropertyName="Text" Name="yardId" Type="Int32"></asp:ControlParameter>
            <asp:ControlParameter ControlID="SiteTypeID" PropertyName="Text" Name="siteTypeId" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>