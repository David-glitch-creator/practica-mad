<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/PracticaMaD.Master"
    CodeBehind="FindFollowers.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.FindFollowers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
    - 
    <asp:HyperLink ID="lnkMenu" runat="server" ForeColor="White"
                        NavigateUrl="~/Pages/MainPage.aspx"
                        Text="<%$ Resources:Common, InkMenu %>" />
    -
    <asp:HyperLink ID="InkProfile" runat="server" ForeColor="White"
                        NavigateUrl="~/Pages/User/MyProfile.aspx"
                        Text="<%$ Resources:Common, InkProfile %>" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="AuthenticationForm" method="POST" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclFindByName" runat="server" meta:resourcekey="lclFindByName" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtName" runat="server" Width="100" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server"
                            ControlToValidate="txtName" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                        <asp:Label ID="lblNameError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblNameError">                        
                        </asp:Label>
                    </span>
            </div>          
            <div class="button">
                <asp:Button ID="btnName" runat="server" OnClick="BtnNameClick" meta:resourcekey="btnName" />
            </div>
        </form>
    </div>
</asp:Content>