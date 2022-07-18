<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewImage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.ViewImage" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <hr />
        <p>
            <asp:Label ID="lblNoImage" runat="server" meta:resourcekey="lblNoImageResource1"></asp:Label>
        </p>

        <asp:Image ID="Image" runat="server" meta:resourcekey="ImageResource2" />
        <br />
        <asp:HyperLink ID="lnkAuthor" runat="server" meta:resourcekey="lnkAuthorResource1" />
        <br />
        <asp:Label ID="lblTitle" runat="server" meta:resourcekey="lblTitleResource1"></asp:Label>
        <br />
        <asp:Label ID="lblDescription" runat="server" meta:resourcekey="lblDescriptionResource1"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblCategoryText" runat="server" meta:resourcekey="lblCategoryTextResource1"></asp:Label>
        <asp:Label ID="lblCategoryName" runat="server" meta:resourcekey="lblCategoryNameResource1"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblExifDetails" runat="server" meta:resourcekey="lblExifDetailsResource1"></asp:Label>
        <br />
        <asp:Label ID="lblAperture" runat="server" meta:resourcekey="lblApertureResource1"></asp:Label>
        <br />
        <asp:Label ID="lblExposureTime" runat="server" meta:resourcekey="lblExposureTimeResource1"></asp:Label>
        <br />
        <asp:Label ID="lblIso" runat="server" meta:resourcekey="lblIsoResource1"></asp:Label>
        <br />
        <asp:Label ID="lblWhiteBalance" runat="server" meta:resourcekey="lblWhiteBalanceResource1"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblLikesNumber" runat="server" meta:resourcekey="lblLikesNumberResource1"></asp:Label>
        <asp:Label ID="lblLikesText" runat="server" meta:resourcekey="lblLikesTextResource1"></asp:Label>
        <br />
        <asp:Button ID="btnLikeImage" runat="server" OnClick="BtnLikeImage_Click" meta:resourcekey="btnLikeImageResource1" />
        <asp:Button ID="btnDislikeImage" runat="server" OnClick="BtnDislikeImage_Click" meta:resourcekey="btnDislikeImageResource1" />
        <br />
        <br />
        <asp:HyperLink ID="lnkAddComment" runat="server" meta:resourcekey="lnkAddCommentResource1" />
        <br />
        <asp:HyperLink ID="lnkComments" runat="server" meta:resourcekey="lnkCommentsResource1" />
        <br />
        <br />
        <asp:GridView ID="gvTags" runat="server" AutoGenerateColumns="False"
                HorizontalAlign="Center"
                meta:resourcekey="gvTagsResource1" >
                <Columns>
                    <asp:HyperLinkField DataTextField="TagName"
                        DataNavigateUrlFields="TagId"
                        DataNavigateUrlFormatString="/Pages/Image/SearchImagesByTag.aspx?tagId={0}"
                        meta:resourcekey="HyperLinkFieldResource1"/>
                </Columns>
            </asp:GridView>
        <br />
        <br />
        <asp:HyperLink ID="lnkAddTags" runat="server" meta:resourcekey="lnkAddTagsResource1" />
        <br />
        <br />
        <asp:Button ID="btnDeleteImage" runat="server" OnClick="BtnDeleteImage_Click" meta:resourcekey="btnDeleteImageResource1" />

        <hr />
    </form>
</asp:Content>
