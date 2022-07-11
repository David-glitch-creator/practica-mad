<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.AddComment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="AddCommentForm" method="post" runat="server">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCommentText" runat="server" Text="Texto del comentario"></asp:Localize>
                    <asp:TextBox ID="txtCommentText" runat="server" Width="100px"
                            Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCommentText" runat="server" ControlToValidate="txtCommentText"
                        Display="Dynamic" Text="Escriba un comentario"></asp:RequiredFieldValidator>
                </span>
            </div>

            <div class="button">
                <asp:Button ID="btnAddComment" runat="server" OnClick="BtnAddComment_Click" Text="Añadir comentario" />
            </div>

        </form>
    </div>
</asp:Content>
