<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SubPage.aspx.cs" Inherits="WebTest.SubPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <langbry>1</langbry>
    </div>
    <div>
        <asp:Label ID="lblTest" runat="server"></asp:Label></div>
</asp:Content>