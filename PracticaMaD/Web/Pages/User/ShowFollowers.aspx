<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/PracticaMaD.Master"
    CodeBehind="ShowFollowers.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ShowFollowers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation"
    runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
    -
    <asp:HyperLink ID="lnkMenu" runat="server" ForeColor="White"
                        NavigateUrl="~/Pages/MainPage.aspx"
                        Text="<%$ Resources:Common, InkMenu %>" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="AuthenticationForm" method="POST" runat="server">
             <asp:Button ID="btnFindByLogin" runat="server" OnClick="BtnFindByLoginClick" Enabled="false" Text="<%$ Resources:Common, btnFindByLogin %>" />               
                &nbsp;&nbsp;                
            <asp:Button ID="btnAllLogins" runat="server" OnClick="BtnShowAllLoginsClick" Text="<%$ Resources:Common, btnAllLogins %>" />                
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclFindByName" runat="server" meta:resourcekey="lclFindByName" />

                </span>
                <span class="entry">
                        <asp:TextBox ID="txtName" runat="server" Width="100" Columns="16"></asp:TextBox>
                </span>
            </div>          
            <div class="button">
                <asp:Button ID="btnName" runat="server" OnClick="BtnNameClick" meta:resourcekey="btnName" />
            </div>
            <br />
            <hr />
            <br />
            <div id="infoGeneral">
                <asp:Label ID="lblFindNameError" runat="server"
                            Visible="False" meta:resourcekey="lblFindNameError" Font-Size="Medium" Font-Strikeout="False"></asp:Label>
                <div >
                    <asp:Label ID="lblTitleName2" runat="server" Text="Label" BackColor="White" Font-Size="Large" Font-Strikeout="False" Font-Underline="False" Font-Bold="True"></asp:Label>
                </div>
                
                <div >
                    <span class="labelTittle">
                        <asp:Localize ID="lblName" runat="server" meta:resourcekey="lblName" />
                        </span>
                    <asp:Label ID="lblName2" runat="server" Font-Size="Small"></asp:Label>
                </div>

                <div >
                    <span class="labelTittle">
                        <asp:Localize ID="lbllastName" runat="server" meta:resourcekey="lbllastName" />
                        </span>
                    <asp:Label ID="lbllastName2" runat="server" Text="Label" Font-Size="Small"></asp:Label>
                </div>

                <div >
                    <span class="labelTittle">
                        <asp:Localize ID="lblcountry" runat="server" meta:resourcekey="lblcountry" />
                        </span>
                    <asp:Label ID="lblcountry2" runat="server" Text="Label" Font-Size="Small"></asp:Label>
                    <br />
                </div>
                                            
            </div>
            <br />
            <div >
                <asp:Button ID="btnFollow" runat="server" OnClick="BtnFollowAlias" meta:resourcekey="btnFollowAlias" />
                    &nbsp;&nbsp;
                <asp:Button ID="btnUnFollow" runat="server" OnClick="BtnUnFollowAlias" meta:resourcekey="btnUnFollowAlias" />
            </div>
        </form>
    </div>
</asp:Content>