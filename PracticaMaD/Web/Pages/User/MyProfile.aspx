<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="MyProfile.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
    - 
    <asp:HyperLink ID="lnkLogout" runat="server"
                        NavigateUrl="~/Pages/User/Logout.aspx"
                        meta:resourcekey="lnkLogout" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="lblLoginName" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblFirstName" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
        <asp:Label ID="lblLastName" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:HyperLink ID="lnkFollowedUsers" runat="server"
                        NavigateUrl="~/Pages/User/Followed.aspx"
                        meta:resourcekey="lnkFollowedUsers" />
        <br />
        <asp:HyperLink ID="lnkFollowers" runat="server"
                        NavigateUrl="~/Pages/User/Followers.aspx"
                        meta:resourcekey="lnkFollowers" />
        <br />
    </form>
</asp:Content>
