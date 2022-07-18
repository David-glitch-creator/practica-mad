<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="SearchImagesByTag.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.SearchImagesByTag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        
        <p>
            <asp:Label ID="lblNoTag" runat="server" Text="No se encontró la etiqueta"></asp:Label>
            <asp:Label ID="lblNoImages" runat="server" Text="No se encontraron imágenes" ></asp:Label>
        </p>

        <asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="False" OnRowDataBound ="grd_RowDataBound" >
            <Columns>
                <asp:TemplateField HeaderText="Imagen">
                    <ItemTemplate>
                        <img src='<%# Eval("ImageFile") %>' style="height:120px;width:120px;" id="ImageControl" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="Title" HeaderText="Título"
                    DataNavigateUrlFields="ImageId"
                    DataNavigateUrlFormatString="/Pages/Image/ViewImage.aspx?ImageId={0}"  />
                <asp:HyperLinkField DataTextField="AuthorLogin" HeaderText="Autor"
                    DataNavigateUrlFields="AuthorId"
                    DataNavigateUrlFormatString="/Pages/User/ViewUser.aspx?UserId={0}" />
            </Columns>
        </asp:GridView>
    <br />
    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="Anterior" runat="server" Visible="False" ></asp:HyperLink>
        </span>
        <span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="Siguiente" runat="server" Visible="False" ></asp:HyperLink>
        </span>
    </div>
    </form>
</asp:Content>
