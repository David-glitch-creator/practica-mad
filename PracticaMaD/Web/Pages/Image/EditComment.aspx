<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="EditComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.EditComment" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="EditCommentForm" method="post" runat="server">

            <p>
                <asp:Label ID="lblNoComment" runat="server" Text="No se encontró el comentario" meta:resourcekey="lblNoCommentResource1"></asp:Label>
                <asp:Label ID="lblNoPermission" runat="server" Text="Usted no tiene permisos para editar este comentario" meta:resourcekey="lblNoPermissionResource1"></asp:Label>
            </p>

            <asp:Image ID="Image" runat="server" meta:resourcekey="ImageResource1" />
            <br />
            <br />

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCommentText" runat="server" Text="Texto del comentario" meta:resourcekey="lclCommentTextResource1"></asp:Localize>
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtCommentText" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtCommentTextResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCommentText" runat="server" ControlToValidate="txtCommentText"
                        Display="Dynamic" Text="Escriba un comentario" meta:resourcekey="rfvCommentTextResource1"></asp:RequiredFieldValidator>
                </span>
            </div>

            <div class="button">
                <asp:Button ID="btnEditComment" runat="server" OnClick="BtnEditComment_Click" Text="Editar comentario" meta:resourcekey="btnEditCommentResource1" />
            </div>

            <br />
            <br />
            <asp:HyperLink ID="lnkBackToComments" Text="Volver a comentarios" runat="server" meta:resourcekey="lnkBackToCommentsResource1" />

        </form>
    </div>
</asp:Content>
