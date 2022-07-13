<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="LikeImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.LikeImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <p>
            <asp:Label ID="lblNoImage" runat="server" Text="No se pudo obtener la imagen"></asp:Label>
        </p>

        <asp:Image ID="Image" runat="server" />

        <br />
        <br />

        <asp:Button ID="btnLikeImage" runat="server" Text="Me gusta" OnClick="BtnLikeImage_Click" />
        <asp:Button ID="btnDislikeImage" runat="server" Text="Ya no me gusta" OnClick="BtnDislikeImage_Click" />
    </form>
</asp:Content>
