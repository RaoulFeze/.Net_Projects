<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfoUserControl.ascx.cs" Inherits="Marigold.UserControls.InfoUserControl" %>
<asp:Panel ID="InfoPanel" runat="server">
    <div class="info-board">
        <div style="font-size:20px; float:left;">
        <asp:Label ID="InfoIcon" runat="server" ></asp:Label>&nbsp;&nbsp;
        </div>
        <asp:Label ID="Message" runat="server"></asp:Label>
    </div>
</asp:Panel>
