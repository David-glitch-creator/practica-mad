<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="EditComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.EditComment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="EditCommentForm" method="post" runat="server">

            <p>
                <asp:Label ID="lblNoComment" runat="server" Text="No se encontró el comentario"></asp:Label>
                <asp:Label ID="lblNoPermission" runat="server" Text="Usted no tiene permisos para editar este comentario"></asp:Label>
            </p>

            <asp:Image ID="Image" runat="server" />
            <br />
            <br />

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCommentText" runat="server" Text="Texto del comentario"></asp:Localize>
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtCommentText" runat="server" Width="100px"
                            Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCommentText" runat="server" ControlToValidate="txtCommentText"
                        Display="Dynamic" Text="Escriba un comentario"></asp:RequiredFieldValidator>
                </span>
            </div>

            <div class="button">
                <asp:Button ID="btnEditComment" runat="server" OnClick="BtnEditComment_Click" Text="Editar comentario" />
            </div>

        </form>
    </div>
</asp:Content>
