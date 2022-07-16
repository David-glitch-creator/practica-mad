<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="Followers.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Followers" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <div>
            <p>
                <asp:Label ID="lblUserNotFound" runat="server" meta:resourcekey="lblUserNotFoundResource1"></asp:Label>
                <asp:Label ID="lblNoFollowers" runat="server" meta:resourcekey="lblNoFollowersResource1"></asp:Label>
            </p>

            <asp:GridView ID="gvFollowers" runat="server" AutoGenerateColumns="False" meta:resourcekey="gvFollowersResource1" >
                <Columns>
                    <asp:HyperLinkField DataTextField="LoginName"
                        DataNavigateUrlFields="UserId"
                        DataNavigateUrlFormatString="/Pages/User/ViewUser.aspx?UserId={0}" meta:resourcekey="HyperLinkFieldResource1"/>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</asp:Content>
