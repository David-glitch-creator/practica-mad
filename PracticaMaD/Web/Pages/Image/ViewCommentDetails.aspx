<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewCommentDetails.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.ViewCommentDetails" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">

        <p>
            <asp:Label ID="lblNoComment" runat="server" meta:resourcekey="lblNoCommentResource1"></asp:Label>
        </p>
        <asp:Image ID="Image" runat="server" meta:resourcekey="ImageResource1" />
        <br />
        <br />
        <asp:HyperLink ID="lnkAuthor" runat="server" meta:resourcekey="lnkAuthorResource1" >[lnkAuthor]</asp:HyperLink>
        <asp:Label ID="lblCommentText" runat="server" meta:resourcekey="lblCommentTextResource1" />
        <br />
        <asp:Label ID="lblPostedDate" runat="server" meta:resourcekey="lblPostedDateResource1" />
        <br />
        <br />
        <asp:Button ID="btnEditComment" runat="server" OnClick="BtnEditComment_Click" meta:resourcekey="btnEditCommentResource1" />
        <br />
        <br />
        <asp:Button ID="btnDeleteComment" runat="server" OnClick="BtnDeleteComment_Click" meta:resourcekey="btnDeleteCommentResource1" />
        <br />
        <br />
        <asp:HyperLink ID="lnkBackToComments" runat="server" meta:resourcekey="lnkBackToCommentsResource1" />

    </form>
</asp:Content>
