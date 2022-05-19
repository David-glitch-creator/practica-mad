<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="MainPage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="form1" runat="server">
        <asp:GridView ID="gvImagesMain" runat="server" AutoGenerateColumns="False" OnRowDataBound ="grd_RowDataBound">
            <Columns>
                <%--<asp:ImageField DataImageUrlField="ImageFile" HeaderText="Imagen"></asp:ImageField>--%>
                <asp:TemplateField HeaderText="Imagen">
                    <ItemTemplate>
                        <img src='<%# Eval("ImageFile") %>' id="ImageControl" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="Titulo" />
                <asp:BoundField DataField="Author" HeaderText="Autor" />
            </Columns>
        </asp:GridView>
    </form>
</asp:Content>
