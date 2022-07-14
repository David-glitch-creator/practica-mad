<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.ViewImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <hr />
        <p>
            <asp:Label ID="lblNoImage" runat="server" Text="No se pudo obtener la imagen"></asp:Label>
        </p>

        <asp:Image ID="Image" runat="server" />
        <br />
        <asp:HyperLink ID="lnkAuthor" runat="server" />
        <br />
        <asp:Label ID="lblTitle" runat="server" Text="Título"></asp:Label>
        <br />
        <asp:Label ID="lblDescription" runat="server" Text="Descripción"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblExifDetails" runat="server" Text="Detalles Exif"></asp:Label>
        <br />
        <asp:Label ID="lblAperture" runat="server" Text="Apertura de Diafragma: "></asp:Label>
        <br />
        <asp:Label ID="lblExposureTime" runat="server" Text="Tiempo de exposición: "></asp:Label>
        <br />
        <asp:Label ID="lblIso" runat="server" Text="ISO: "></asp:Label>
        <br />
        <asp:Label ID="lblWhiteBalance" runat="server" Text="Balance de blancos: "></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblLikesNumber" runat="server" Text="Número de Me gustas"></asp:Label>
        <br />
        <asp:Button ID="btnLikeImage" runat="server" Text="Me gusta" OnClick="BtnLikeImage_Click" />
        <br />
        <br />
        <asp:HyperLink ID="lnkAddComment" Text="Añadir comentario" runat="server"></asp:HyperLink>
        <br />
        <asp:HyperLink ID="lnkComments" Text="Ver comentarios" runat="server"></asp:HyperLink>
        <br />
        <br />
        <asp:Button ID="btnDeleteImage" runat="server" Text="Borrar imagen" OnClick="BtnDeleteImage_Click" />

        <hr />
    </form>
</asp:Content>
