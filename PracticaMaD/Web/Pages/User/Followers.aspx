﻿<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="Followers.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.Followers" %>

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
                <asp:Label ID="lblUserNotFound" runat="server" Text="No se encontró usuario"></asp:Label>
                <asp:Label ID="lblNoFollowers" runat="server" Text="Usted no tiene seguidores actualmente"></asp:Label>
            </p>

            <asp:GridView ID="gvFollowers" runat="server" AutoGenerateColumns="false" >
                <Columns>
                    <asp:HyperLinkField DataTextField="LoginName" HeaderText="Login"
                        DataNavigateUrlFields="UserId"
                        DataNavigateUrlFormatString="/Pages/User/ViewUser.aspx?UserId={0}"/>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</asp:Content>
