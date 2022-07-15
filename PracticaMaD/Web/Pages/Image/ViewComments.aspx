<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.ViewComments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">

        <p>
            <asp:Label ID="lblNoComments" runat="server" Text="No se encontraron comentarios"></asp:Label>
        </p>
        <asp:Image ID="Image" runat="server" />
        <br />
        <asp:GridView ID="gvViewComments" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:HyperLinkField DataTextField="AuthorLogin" HeaderText="Usuario"
                        DataNavigateUrlFields="AuthorId"
                        DataNavigateUrlFormatString="/Pages/User/ViewUser.aspx?UserId={0}"/>
                <asp:BoundField DataField="CommentText" HeaderText="Comentario" />
                <asp:BoundField DataField="PostedDate" HeaderText="Fecha" />
            </Columns>
        </asp:GridView>
    </form>
</asp:Content>
