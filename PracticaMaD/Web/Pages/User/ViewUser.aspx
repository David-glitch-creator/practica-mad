<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ViewUser.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ViewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="lblLoginName" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblFirstName" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
        <asp:Label ID="lblLastName" runat="server" Text="Label"></asp:Label>
        <br />
        <%--<asp:HyperLink ID="lnkFollowedUsers" runat="server"
                        NavigateUrl="~/Pages/User/Followed.aspx"
                        meta:resourcekey="lnkFollowedUsers" />
        <br />
        <asp:HyperLink ID="lnkFollowers" runat="server"
                        NavigateUrl="~/Pages/User/Followers.aspx"
                        meta:resourcekey="lnkFollowers" />--%>
        <br />
        <asp:HyperLink ID="lnkFindFolloweds" runat="server" Display="Dynamic"
                        NavigateUrl="~/Pages/User/FindSelect.aspx"
                        meta:resourcekey="lnkFindFolloweds" />
        <br />
        <asp:Button ID="btnFollow" runat="server" Text="Seguir" 
            OnClick="BtnFollow_Click"/>
        
        <asp:Button ID="btnUnfollow" runat="server" Text="Dejar de seguir" 
            OnClick="BtnUnfollow_Click"/>
        <br />
        <p>
            <asp:Label ID="lblNoImages" runat="server" Text="No se encontraron imágenes"></asp:Label>
        </p>

        <asp:GridView ID="gvImagesViewUser" runat="server" AutoGenerateColumns="False" OnRowDataBound ="grd_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Imagen">
                    <ItemTemplate>
                        <img src='<%# Eval("ImageFile") %>' style="height:120px;width:120px;" id="ImageControl" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="Title" HeaderText="Título"
                    DataNavigateUrlFields="ImageId"
                    DataNavigateUrlFormatString="/Pages/Image/ViewImage.aspx?ImageId={0}" />
                <asp:BoundField DataField="AuthorLogin" HeaderText="Autor" />
            </Columns>
        </asp:GridView>

        <br />

        <!-- "Previous" and "Next" links. -->
        <div class="previousNextLinks">
            <span class="previousLink">
                <asp:HyperLink ID="lnkPrevious" Text="Anterior" runat="server" Visible="False"></asp:HyperLink>
            </span>
            <span class="nextLink">
                <asp:HyperLink ID="lnkNext" Text="Siguiente" runat="server" Visible="False"></asp:HyperLink>
            </span>
        </div>

    </form>
</asp:Content>
