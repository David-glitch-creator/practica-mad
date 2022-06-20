<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.ViewImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <hr />
    <p>
        <asp:Label ID="lblNoImage" runat="server" Text="No se pudo obtener la imagen"></asp:Label>
    </p>

    <asp:Image ID="Image" runat="server" />
    <br />
    <asp:Label ID="lblAuthor" runat="server" Text="Autor"></asp:Label>
    <br />
    <asp:Label ID="lblTitle" runat="server" Text="Título"></asp:Label>
    <br />
    <asp:Label ID="lblDescription" runat="server" Text="Descripción"></asp:Label>

    <hr />
</asp:Content>
