<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/PracticaMaD.Master"
    CodeBehind="ShowFollowers.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ShowFollowers" %>

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
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclFindByName" runat="server" meta:resourcekey="lclFindByName" />

                </span>
                <span class="entry">
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
            <br />
            <div id="infoGeneral">
                <asp:Label ID="lblFindNameError" runat="server"
                            Visible="False" meta:resourcekey="lblFindNameError">                        
                        </asp:Label>
                <div >
                    <span class="labelTittle">
                        <asp:Localize ID="lblTitleName" runat="server" meta:resourcekey="lblTitleName" />
                     </span>
                    <asp:Label ID="lblTitleName2" runat="server" Text="Label"></asp:Label>
                </div>
                
                <div >
                    <span class="labelTittle">
                        <asp:Localize ID="lblName" runat="server" meta:resourcekey="lblName" />
                        </span>
                    <asp:Label ID="lblName2" runat="server"></asp:Label>
                </div>

                <div >
                    <span class="labelTittle">
                        <asp:Localize ID="lbllastName" runat="server" meta:resourcekey="lbllastName" />
                        </span>
                    <asp:Label ID="lbllastName2" runat="server" Text="Label"></asp:Label>
                </div>

                <div >
                    <span class="labelTittle">
                        <asp:Localize ID="lblcountry" runat="server" meta:resourcekey="lblcountry" />
                        </span>
                    <asp:Label ID="lblcountry2" runat="server" Text="Label"></asp:Label>
                </div>

            </div>
        </form>
    </div>
</asp:Content>