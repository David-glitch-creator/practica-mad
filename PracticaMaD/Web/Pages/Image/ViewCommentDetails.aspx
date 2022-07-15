<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewCommentDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.ViewCommentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">

        <p>
            <asp:Label ID="lblNoComment" runat="server" Text="No se encontró el comentario"></asp:Label>
        </p>
        <asp:Image ID="Image" runat="server" />
        <br />
        <br />
        <asp:HyperLink ID="lnkAuthor" runat="server" />
        <asp:Label ID="lblCommentText" runat="server" Text="Texto de comentario" />
        <br />
        <asp:Label ID="lblPostedDate" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnEditComment" runat="server" Text="Editar comentario" OnClick="BtnEditComment_Click" />
        <br />
        <br />
        <asp:Button ID="btnDeleteComment" runat="server" Text="Eliminar comentario" OnClick="BtnDeleteComment_Click" />

    </form>
</asp:Content>
