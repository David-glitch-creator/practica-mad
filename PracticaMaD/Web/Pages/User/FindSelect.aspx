<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/PracticaMaD.Master"
    CodeBehind="FindSelect.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.FindSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
    -
    <asp:HyperLink ID="lnkMenu" runat="server" ForeColor="White"
                        NavigateUrl="~/Pages/MainPage.aspx"
                        Text="<%$ Resources:Common, InkMenu %>" />
    -
    <asp:HyperLink ID="InkProfile" runat="server" ForeColor="White"
                        NavigateUrl="~/Pages/User/MyProfile.aspx"
                        Text="<%$ Resources:Common, InkProfile %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
        <form id="AuthenticationForm" method="POST" runat="server">
            <asp:Button ID="btnFindByLogin" runat="server" OnClick="BtnFindByLoginClick" Text="<%$ Resources:Common, btnFindByLogin %>" meta:resourcekey="btnFindByLogin"/>               
                &nbsp;&nbsp;                
            <asp:Button ID="btnAllLogins" runat="server" OnClick="BtnShowAllLoginsClick" Text="<%$ Resources:Common, btnAllLogins %>"  meta:resourcekey="btnAllLogins"/>                
        </form>          
</asp:Content>