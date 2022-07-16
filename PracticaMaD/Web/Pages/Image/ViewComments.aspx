<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewComments.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.ViewComments" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">

        <p>
            <asp:Label ID="lblNoComments" runat="server" meta:resourcekey="lblNoCommentsResource1"></asp:Label>
        </p>
        <asp:Image ID="Image" runat="server" meta:resourcekey="ImageResource1" />
        <br />
        <asp:GridView ID="gvViewComments" runat="server" AutoGenerateColumns="False" meta:resourcekey="gvViewCommentsResource1">
            <Columns>
                <asp:HyperLinkField DataTextField="AuthorLogin"
                        DataNavigateUrlFields="AuthorId"
                        DataNavigateUrlFormatString="/Pages/User/ViewUser.aspx?UserId={0}" meta:resourcekey="HyperLinkFieldResource1"/>
                <asp:HyperLinkField DataTextField="CommentText"
                    DataNavigateUrlFields="CommentId"
                    DataNavigateUrlFormatString="/Pages/Image/ViewCommentDetails.aspx?commentId={0}" meta:resourcekey="HyperLinkFieldResource2"/>
                <asp:BoundField DataField="PostedDate" meta:resourcekey="BoundFieldResource1" />
            </Columns>
        </asp:GridView>
        <br />
        <br />
        <asp:HyperLink ID="lnkBackToImage" runat="server" meta:resourcekey="lnkBackToImageResource1" />
    </form>
</asp:Content>
