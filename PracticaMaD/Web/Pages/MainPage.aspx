<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="MainPage.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.MainPage" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <form id="form1" runat="server">
        <asp:HyperLink ID="lnkSearchImages" runat="server" NavigateUrl="~/Pages/Image/SearchImages.aspx"
            meta:resourcekey="lnkSearchImagesResource1" />

        <p>
            <asp:Label ID="lblNoImages" runat="server" meta:resourcekey="lblNoImagesResource1"></asp:Label>
        </p>

        <asp:GridView ID="gvImagesMain" runat="server" AutoGenerateColumns="False"
            HorizontalAlign="Center"
            OnRowDataBound ="grd_RowDataBound"
            meta:resourcekey="gvImagesMainResource1">
            <Columns>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <img src='<%# Eval("ImageFile") %>' style="height:120px;width:120px;" id="ImageControl" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="Title"
                    DataNavigateUrlFields="ImageId"
                    DataNavigateUrlFormatString="/Pages/Image/ViewImage.aspx?ImageId={0}" meta:resourcekey="HyperLinkFieldResource1" />
                <asp:HyperLinkField DataTextField="AuthorLogin"
                    DataNavigateUrlFields="AuthorId"
                    DataNavigateUrlFormatString="/Pages/User/ViewUser.aspx?UserId={0}" meta:resourcekey="HyperLinkFieldResource2"/>
            </Columns>
        </asp:GridView>
    <br />
    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" runat="server" Visible="False" meta:resourcekey="lnkPreviousResource1"></asp:HyperLink>
        </span>
        <span class="nextLink">
            <asp:HyperLink ID="lnkNext" runat="server" Visible="False" meta:resourcekey="lnkNextResource1"></asp:HyperLink>
        </span>
    </div>
    </form>
</asp:Content>
