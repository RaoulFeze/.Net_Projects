<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasters/CityOperations/Parks/CrewLeader.master" AutoEventWireup="true" CodeBehind="Routes.aspx.cs" Inherits="Marigold.App_Pages.City_Operations.Parks.CrewLeader.Routes" %>

<%@ Register Src="~/UserControls/InfoUserControl.ascx" TagPrefix="uc1" TagName="InfoUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <uc1:InfoUserControl runat="server" ID="InfoUserControl" />
    <asp:Label ID="YardID" runat="server" Text="1" Visible="false"></asp:Label>
    <asp:Label ID="SiteTypeID" runat="server" Text="1" Visible="false"></asp:Label>
    <asp:Label ID="TaskID" runat="server" Text="1" Visible="false"></asp:Label>
    <asp:Label ID="UserID" runat="server" Text="" Visible="false"></asp:Label>

 

    <div class="route-status">
        <div class="route-status-menu">
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
        </div>
        <div class="routesGridView">
        <asp:GridView ID="RoutesGridView" runat="server" 
            AutoGenerateColumns="False"
            PageSize="20"
            CssClass="table table-striped table-hover table-bordered"
            OnPageIndexChanging="RoutesGridView_PageIndexChanging" 
            AllowPaging="True"
            OnRowEditing="RoutesGridView_RowEditing"
            OnRowUpdating="RoutesGridView_RowUpdating"
            OnRowCancelingEdit="RoutesGridView_RowCancelingEdit">
            <Columns>
                <asp:TemplateField HeaderText="" SortExpression="">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pin" SortExpression="Pin">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Pin") %>' ID="TextBox1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Pin") %>' ID="Label1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Community" SortExpression="Community">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Community") %>' ID="TextBox2"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Community") %>' ID="Label2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Description") %>' ID="TextBox3"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="Label3"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address" SortExpression="Address">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Address") %>' ID="TextBox4"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Address") %>' ID="Label4"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Area" SortExpression="Area">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Area") %>' ID="TextBox5"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Area") %>' ID="Label5"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Count" SortExpression="Count">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Count") %>' ID="TextBox6"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Count") %>' ID="Label6"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Notes" SortExpression="Notes">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Notes") %>' ID="TextBox7"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Notes") %>' ID="Label7"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cycle1" SortExpression="Cycle1">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Cycle1", "{0:MMM-dd-yyyy}") %>' ID="TextBox8"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Cycle1", "{0:MMM-dd-yyyy}") %>' ID="Cycle1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cycle2" SortExpression="Cycle2">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Cycle2", "{0:MMM-dd-yyyy}") %>' ID="TextBox9"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Cycle2", "{0:MMM-dd-yyyy}") %>' ID="Cycle2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cycle3" SortExpression="Cycle3">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Cycle3", "{0:MMM-dd-yyyy}") %>' ID="TextBox10"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Cycle3", "{0:MMM-dd-yyyy}") %>' ID="Cycle3"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cycle4" SortExpression="Cycle4">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Cycle4", "{0:MMM-dd-yyyy}") %>' ID="TextBox11"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Cycle4", "{0:MMM-dd-yyyy}") %>' ID="Cycle4"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cycle5" SortExpression="Cycle5">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Cycle5", "{0:MMM-dd-yyyy}") %>' ID="TextBox12"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Cycle5", "{0:MMM-dd-yyyy}") %>' ID="Cycle5"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pruning" SortExpression="Pruning">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Pruning", "{0:MMM-dd-yyyy}") %>' ID="TextBox13"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Pruning", "{0:MMM-dd-yyyy}") %>' ID="Pruning"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mulching" SortExpression="Mulching">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Mulching", "{0:MMM-dd-yyyy}") %>' ID="TextBox14"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Mulching", "{0:MMM-dd-yyyy}") %>' ID="Mulching"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Planting" SortExpression="Planting">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Planting", "{0:MMM-dd-yyyy}") %>' ID="TextBox15"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Planting", "{0:MMM-dd-yyyy}") %>' ID="Planting"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Uprooting" SortExpression="Uprooting">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Uprooting", "{0:MMM-dd-yyyy}") %>' ID="TextBox16"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Uprooting", "{0:MMM-dd-yyyy}") %>' ID="Uprooting"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Trimming" SortExpression="Trimming">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Trimming", "{0:MMM-dd-yyyy}") %>' ID="TextBox17"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Trimming", "{0:MMM-dd-yyyy}") %>' ID="Trimming"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Watering" SortExpression="Watering">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("Watering", "{0:MMM-dd-yyyy}") %>' ID="TextBox18"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("Watering", "{0:MMM-dd-yyyy}") %>' ID="Watering"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ButtonType="Button"></asp:CommandField>
            </Columns>
                <PagerSettings FirstPageText="First" PreviousPageText="Previous" PageButtonCount="5"  NextPageText="Next" LastPageText="Last" Position="Top" Mode="NumericFirstLast" />
                <PagerStyle VerticalAlign="Bottom" HorizontalAlign="Center" />
        </asp:GridView>
            </div>
    </div>
    <asp:ObjectDataSource ID="RoutesODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Get_AB_Routes" TypeName="MarigoldSystem.BLL.RouteController">
        <SelectParameters>
            <asp:ControlParameter ControlID="YardID" PropertyName="Text" Name="yardId" Type="Int32"></asp:ControlParameter>
            <asp:ControlParameter ControlID="SiteTypeID" PropertyName="Text" Name="siteTypeId" Type="Int32"></asp:ControlParameter>
            <asp:ControlParameter ControlID="SiteTypeID" PropertyName="Text" Name="taskId" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
