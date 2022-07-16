<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.AddComment" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="AddCommentForm" method="post" runat="server">

            <p>
                <asp:Label ID="lblNoImage" runat="server" meta:resourcekey="lblNoImageResource1"></asp:Label>
            </p>

            <asp:Image ID="Image" runat="server" meta:resourcekey="ImageResource2" />
            <br />
            <br />

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCommentText" runat="server" meta:resourcekey="lclCommentTextResource1"></asp:Localize>
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtCommentText" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtCommentTextResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCommentText" runat="server" ControlToValidate="txtCommentText"
                        Display="Dynamic" meta:resourcekey="rfvCommentTextResource1"></asp:RequiredFieldValidator>
                </span>
            </div>

            <div class="button">
                <asp:Button ID="btnAddComment" runat="server" OnClick="BtnAddComment_Click" meta:resourcekey="btnAddCommentResource1" />
            </div>

            <br />
            <br />
            <asp:HyperLink ID="lnkBackToImage" runat="server" meta:resourcekey="lnkBackToImageResource1" />

        </form>
    </div>
</asp:Content>
