<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="MyProfile.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.MyProfile" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="lblLoginName" runat="server" meta:resourcekey="lblLoginNameResource1"></asp:Label>
        <br />
        <asp:Label ID="lblFirstName" runat="server" meta:resourcekey="lblFirstNameResource1"></asp:Label>
        <asp:Label ID="lblLastName" runat="server" meta:resourcekey="lblLastNameResource1"></asp:Label>
        <br />
        <asp:HyperLink ID="lnkFollowedUsers" runat="server"
                        meta:resourcekey="lnkFollowedUsers" />
        <br />
        <asp:HyperLink ID="lnkFollowers" runat="server"
                        meta:resourcekey="lnkFollowers" />
        <br />
        <asp:HyperLink ID="lnkFindFolloweds" runat="server" Display="Dynamic"
                        NavigateUrl="~/Pages/User/FindSelect.aspx"
                        meta:resourcekey="lnkFindFolloweds" />
        <br />
        <p>
            <asp:Label ID="lblNoImages" runat="server" meta:resourcekey="lblNoImagesResource1"></asp:Label>
        </p>

        <asp:GridView ID="gvImagesMyProfile" runat="server" AutoGenerateColumns="False"
            HorizontalAlign="Center"
            OnRowDataBound ="grd_RowDataBound"
            meta:resourcekey="gvImagesMyProfileResource1">
            <Columns>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <img src='<%# Eval("ImageFile") %>' style="height:120px;width:120px;" id="ImageControl" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="Title"
                    DataNavigateUrlFields="ImageId"
                    DataNavigateUrlFormatString="/Pages/Image/ViewImage.aspx?ImageId={0}" meta:resourcekey="HyperLinkFieldResource1" />
                <asp:BoundField DataField="AuthorLogin" meta:resourcekey="BoundFieldResource1" />
                <asp:HyperLinkField
                    DataNavigateUrlFields="ImageId" 
                    DataNavigateUrlFormatString="/Pages/Image/AddTags.aspx?ImageId={0}" meta:resourcekey="HyperLinkFieldResource2" />
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
