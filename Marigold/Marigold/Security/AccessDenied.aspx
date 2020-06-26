<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasters/CityOperations/CityOperations.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="Marigold_Application.Security.AccessDenied" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center">
        <div class="container alert alert-danger denied">
            <div class="jumbotron">
                <h1>Access Denied</h1>
            </div>
        </div>
        <asp:Button ID="GoTo" runat="server" Text="Go To Login" CssClass="btn btn-primary" OnClick="GoTo_Click" />
    </div>
</asp:Content>
