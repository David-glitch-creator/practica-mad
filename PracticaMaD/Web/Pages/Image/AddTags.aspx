<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddTags.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.AddTags" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="AddTagsForm" method="post" runat="server">
            <p>
                <asp:Label ID="lblNoImage" runat="server" Text="No se encontró imagen"></asp:Label>
                <asp:Label ID="lblNoPermission" runat="server" Text="Usted no tiene permisos para editar esta imagen"></asp:Label>
            </p>
            <asp:Image ID="Image" runat="server" />
            
            <br />
            <br />

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTags" runat="server" Text="Etiquetas (por favor, sepárelas con una coma seguida de un espacio)"></asp:Localize>
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtTags" runat="server" Width="100px"
                            Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTags" runat="server" ControlToValidate="txtTags"
                        Display="Dynamic" Text="Indique, al menos, una etiqueta"></asp:RequiredFieldValidator>
                </span>
            </div>

            <div class="button">
                <asp:Button ID="btnAddTags" runat="server" OnClick="BtnAddTags_Click" Text="Añadir etiquetas" />
            </div>

        </form>
    </div>
</asp:Content>
