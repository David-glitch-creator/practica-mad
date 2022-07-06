<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="Followed.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Followed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
    - 
    <asp:HyperLink ID="lnkMenu" runat="server"
                        NavigateUrl="~/Pages/MainPage.aspx"
                        Text="<%$ Resources:Common, InkMenu %>" />
    
    - 
    <asp:HyperLink ID="lnkLogout" runat="server"
                        NavigateUrl="~/Pages/User/Logout.aspx"
                        meta:resourcekey="lnkLogout" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div>
            <p>
                <asp:Label ID="lblNoUsersFollowed" runat="server" Text="Usted no sigue a ningún usuario"></asp:Label>
            </p>

            <asp:GridView ID="gvFollowed" runat="server" AutoGenerateColumns="false" >
                <Columns>
                    <asp:HyperLinkField DataTextField="LoginName" HeaderText="Login"
                        DataNavigateUrlFields="UserId"
                        DataNavigateUrlFormatString="/Pages/User/ViewProfile.aspx?UserId={0}"/>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</asp:Content>
