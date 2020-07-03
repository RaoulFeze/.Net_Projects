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
    <%-- --------------------------------------------------------------------------------------------------- --%>
    <div class="info-message container">
        <uc1:InfoUserControl runat="server" ID="InfoUserControl" />
    </div>
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
    <%--<asp:Label ID="DriverID" runat="server" Text="" Visible="true"></asp:Label>--%>
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
                <%--<asp:LinkButton ID="AddCrew" runat="server" Visible="false">ADD CREW</asp:LinkButton>--%>
                <asp:Button ID="AddMember" runat="server" Text="CREW MEMBERS" Visible="false" CssClass="crew-member-button" OnClick="AddMember_Click" />
                &nbsp;
                &nbsp;
                  <asp:Button ID="Done" runat="server" Text="DONE" Visible="false" CssClass="done-control-button" OnClick="Done_Click" />
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
                            <asp:TemplateField HeaderText="License" SortExpression="License" >
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
                                    <asp:RadioButton ID="SelectedDriver" runat="server" AutoPostBack="true" OnCheckedChanged="SelectedDriver_CheckedChanged"/>
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
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                
                <asp:Repeater ID="AllCurrentCrews" runat="server"
                    ItemType="MarigoldSystem.Data.DTO_s.CurrentCrews"
                    OnItemCommand="AllCurrentCrews_ItemCommand">
                    <ItemTemplate>
                        <div class="current-crew-box offset-1">
                            <div class="current-crew-header offset-5">
                                <h4 class="crew-header">
                                    <asp:LinkButton ID="SelectCrew" runat="server" CommandArgument='<%#Item.CrewID %>' CommandName="Seletedcrew">
                                        CREW (<asp:Literal ID="Literal1" runat="server" Text='<%# Container.ItemIndex + 1 %>'/>)
                                    </asp:LinkButton>
                                </h4>
                                <div class="crew-glyphicon-remove">
                                    <asp:LinkButton ID="RemoveCrew" runat="server" CommandArgument='<%# Item.CrewID %>' CommandName="DeleteCrew">
                                    <span aria-hidden="true" class="glyphicon glyphicon-remove" ></span> 
                                    </asp:LinkButton>
                                </div>
                               
                            </div> 
                            <div class="UnitDescription">
                          
                                <%#Item.Description %></div>
                            <div class="col-12">
                                <div class="crew-member-box">
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
                                                    <asp:Label ID="Name" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="RemoveEmployee" runat="server" CommandArgument='<%# Eval("CrewMemberID") %>' CommandName="DeleteMember">
                                                <span aria-hidden="true" class="glyphicon glyphicon-remove" ></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="job-card-box">
                                    <asp:GridView ID="GridView1" runat="server"
                                        AutoGenerateColumns="false"
                                        CssClass="table table-striped table-bordered "
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
                                                    <asp:Label ID="Name" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="RemoveSite" runat="server" CommandArgument='<%# Eval("JobCardID") %>' CommandName="DeleteSite" OnClientClick="return ConfirmRemoveSite()">
                                                <span aria-hidden="true" class="glyphicon glyphicon-remove" ></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
    <%--<asp:ObjectDataSource ID="GetTruckDrivers" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetTruckDrivers" TypeName="MarigoldSystem.BLL.FleetController">
        <SelectParameters>
            <asp:ControlParameter ControlID="YardID" PropertyName="Text" Name="yardId" Type="Int32"></asp:ControlParameter>
            <asp:ControlParameter ControlID="UnitID" PropertyName="Text" Name="unitId" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>--%>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCurrentCrews" TypeName="MarigoldSystem.BLL.CrewController">
        <SelectParameters>
            <asp:ControlParameter ControlID="YardID" PropertyName="Text" Name="yardId" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
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