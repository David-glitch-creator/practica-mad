<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="MainPage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="form1" runat="server">
        <asp:HyperLink ID="lnkSearchImages" runat="server" NavigateUrl="~/Pages/Image/SearchImages.aspx">
            HyperLink
        </asp:HyperLink>

        <p>
            <asp:Label ID="lblNoImages" runat="server" Text="No se encontraron imágenes"></asp:Label>
        </p>

        <asp:GridView ID="gvImagesMain" runat="server" AutoGenerateColumns="False" OnRowDataBound ="grd_RowDataBound">
            <Columns>
                <%--<asp:ImageField DataImageUrlField="ImageFile" HeaderText="Imagen"></asp:ImageField>--%>
                <asp:TemplateField HeaderText="Imagen">
                    <ItemTemplate>
                        <img src='<%# Eval("ImageFile") %>' style="height:120px;width:120px;" id="ImageControl" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="Title" HeaderText="Titulo" />--%>
                <asp:HyperLinkField DataTextField="Title" HeaderText="Título"
                    DataNavigateUrlFields="ImageId"
                    DataNavigateUrlFormatString="/Pages/Image/ViewImage.aspx?ImageId={0}" />
                <asp:BoundField DataField="AuthorLogin" HeaderText="Autor" />
            </Columns>
        </asp:GridView>
    <br />
    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="Anterior" runat="server" Visible="False"></asp:HyperLink>
        </span>
        <span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="Siguiente" runat="server" Visible="False"></asp:HyperLink>
        </span>
    </div>
    </form>
</asp:Content>
