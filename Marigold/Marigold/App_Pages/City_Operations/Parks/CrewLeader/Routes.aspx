<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasters/CityOperations/Parks/CrewLeader.master" AutoEventWireup="true" CodeBehind="Routes.aspx.cs" Inherits="Marigold.App_Pages.City_Operations.Parks.CrewLeader.Routes" %>

<%@ Register Src="~/UserControls/InfoUserControl.ascx" TagPrefix="uc1" TagName="InfoUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:Label ID="YardID" runat="server" Text="1" Visible="false"></asp:Label>
    <asp:Label ID="SiteTypeID" runat="server" Text="1" Visible="false"></asp:Label>
    <asp:Label ID="TaskID" runat="server" Text="1" Visible="false"></asp:Label>
    <asp:Label ID="UserID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="SearchFlag" runat="server" Text="0" Visible="false"></asp:Label>

     <div class="info-message container">
        <uc1:InfoUserControl runat="server" ID="InfoUserControl" />
    </div>
    <div class="route-status">
        <div class="search-area">
            <div class="search-box container">
                <div class="search-label">
                    <asp:Label ID="Label8" runat="server" Text="Search"></asp:Label>
                </div>
                <div class="search-category">
                    <asp:DropDownList ID="SearchCategory" runat="server"></asp:DropDownList>
                </div>
                <div class="search-criteria">
                    <asp:TextBox ID="SearchCriteria" runat="server"></asp:TextBox>
                </div>
                <div class="task-option">
                    <asp:RadioButtonList ID="TaskOption" runat="server"
                        RepeatDirection="Horizontal"
                        CellPadding="5">
                        <asp:ListItem Value="1">&nbsp;SBM</asp:ListItem>
                        <asp:ListItem Value="2">&nbsp;Grass</asp:ListItem>
                        <asp:ListItem Value="3">&nbsp;Watering</asp:ListItem>
                        <asp:ListItem Value="4">&nbsp;Planting</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="season-ddl">
                    <asp:DropDownList ID="SeasonDDL" runat="server"></asp:DropDownList>
                </div>

                <div class="go-clear-search">
                    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ClearSearch" runat="server" Text="Clear" OnClick="ClearSearch_Click" />
                </div>
            </div>
        </div>
        
        <div class="routes-wrapper">
            <div class="routes-menu">

            </div>
           
            <div class="routes">
                <div class="routes-area">
                    <asp:Menu ID="RouteMenu" runat="server"
                        Orientation="Horizontal"
                        StaticMenuItemStyle-CssClass="tab"
                        StaticMenuItemStyle-HorizontalPadding="50px"
                        StaticSelectedStyle-CssClass="selectedTab"
                        StaticSelectedStyle-BackColor="ForestGreen"
                        StaticSelectedStyle-ForeColor="Black"
                        CssClass="tabs"
                        Font-Size="Large"
                        ForeColor="Black" OnMenuItemClick="RouteMenu_MenuItemClick">
                        <Items>
                            <asp:MenuItem Text="A Routes" Selected="true" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="B Routes" Value="1"></asp:MenuItem>
                            <asp:MenuItem Text="Grass Routes" Value="2"></asp:MenuItem>
                            <asp:MenuItem Text="Watering Routes" Value="3"></asp:MenuItem>
                            <asp:MenuItem Text="Planting Route" Value="4"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                    <asp:GridView ID="RoutesGridView" runat="server"
                        AutoGenerateColumns="False"
                        PageSize="20"
                        CssClass="table table-striped table-hover table-bordered"
                        OnPageIndexChanging="RoutesGridView_PageIndexChanging"
                        AllowPaging="True">
                        <Columns>
                            <asp:TemplateField HeaderText="" SortExpression="">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SIteID" SortExpression="SiteID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("SiteID") %>' ID="SiteID"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pin" SortExpression="Pin">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("Pin") %>' ID="Pin"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Community" SortExpression="Community">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Community") %>' ID="Label2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="Label3"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Address") %>' ID="Label4"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Area" SortExpression="Area">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Area") %>' ID="Label5"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Count" SortExpression="Count">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Count") %>' ID="Label6"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Notes") %>' ID="Label7"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cycle1" SortExpression="Cycle1">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Cycle1", "{0:MMM-dd-yyyy}") %>' ID="Cycle1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cycle2" SortExpression="Cycle2">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Cycle2", "{0:MMM-dd-yyyy}") %>' ID="Cycle2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cycle3" SortExpression="Cycle3">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Cycle3", "{0:MMM-dd-yyyy}") %>' ID="Cycle3"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cycle4" SortExpression="Cycle4">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Cycle4", "{0:MMM-dd-yyyy}") %>' ID="Cycle4"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cycle5" SortExpression="Cycle5">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Cycle5", "{0:MMM-dd-yyyy}") %>' ID="Cycle5"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pruning" SortExpression="Pruning">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Pruning", "{0:MMM-dd-yyyy}") %>' ID="Pruning"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mulching" SortExpression="Mulching">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Mulching", "{0:MMM-dd-yyyy}") %>' ID="Mulching"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Planting" SortExpression="Planting">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Planting", "{0:MMM-dd-yyyy}") %>' ID="Planting"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Uprooting" SortExpression="Uprooting">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Uprooting", "{0:MMM-dd-yyyy}") %>' ID="Uprooting"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trimming" SortExpression="Trimming">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Trimming", "{0:MMM-dd-yyyy}") %>' ID="Trimming"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Watering" SortExpression="Watering">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Watering", "{0:MMM-dd-yyyy}") %>' ID="Watering"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings FirstPageText="First" PreviousPageText="Previous" PageButtonCount="5" NextPageText="Next" LastPageText="Last" Position="Top" Mode="NumericFirstLast" />
                        <PagerStyle VerticalAlign="Bottom" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
