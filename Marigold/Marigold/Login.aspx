<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Marigold.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="CSS_Files/Login/Login.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
        <p class="text-danger">
            <asp:Literal runat="server" ID="FailureText" />
        </p>
    </asp:PlaceHolder>
    <form id="form1" runat="server">
        <div class="login-box">
            <div class="avatar">
                <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/dispthumb.aspx.jpg" />--%>
                <h1>City of Edmonton</h1>
                <p>Parks and Roads Services</p>
            <div class="form-group">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
                        CssClass="text-danger" ErrorMessage="The Username field is required." />
                <asp:Label runat="server" AssociatedControlID="Username" CssClass="control-label">Username</asp:Label>
                    <asp:TextBox runat="server" ID="Username" CssClass="text-box"/>
            </div>
            <div class="form-group">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                <asp:Label runat="server" AssociatedControlID="Password" CssClass="control-label">Password</asp:Label>
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="text-box"/>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <div class="checkbox">
                        <asp:CheckBox runat="server" ID="RememberMe" />
                        <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="login-button" />
                </div>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
