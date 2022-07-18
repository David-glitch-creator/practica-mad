<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AddTags.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.AddTags" meta:resourcekey="PageResource1" %>
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
                <asp:Label ID="lblNoImage" runat="server" meta:resourcekey="lblNoImageResource1"></asp:Label>
                <asp:Label ID="lblNoPermission" runat="server" meta:resourcekey="lblNoPermissionResource1"></asp:Label>
            </p>
            <asp:Image ID="Image" runat="server" meta:resourcekey="ImageResource2" />
            
            <br />
            <br />

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTags" runat="server" meta:resourcekey="lclTagsResource1"></asp:Localize>
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtTags" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtTagsResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTags" runat="server" ControlToValidate="txtTags"
                        Display="Dynamic" meta:resourcekey="rfvTagsResource1"></asp:RequiredFieldValidator>
                </span>
            </div>

            <div class="button">
                <asp:Button ID="btnAddTags" runat="server" OnClick="BtnAddTags_Click" meta:resourcekey="btnAddTagsResource1" />
            </div>

            <div class="button">
                <asp:Button ID="btnRemoveTags" runat="server" OnClick="BtnRemoveTags_Click" meta:resourcekey="btnRemoveTagsResource1" />
            </div>

            <br />

            <asp:GridView ID="gvImageTags" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" meta:resourcekey="gvImageTagsResource1">
                <Columns>
                    <asp:HyperLinkField DataTextField="TagName"
                        DataNavigateUrlFields="TagId"
                        DataNavigateUrlFormatString="/Pages/User/SearchByTag.aspx?TagId={0}" meta:resourcekey="HyperLinkFieldResource1"/>
                </Columns>
            </asp:GridView>

        </form>
    </div>
</asp:Content>
