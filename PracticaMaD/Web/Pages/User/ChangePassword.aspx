<%@ Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ChangePassword"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    -
    <asp:Localize ID="lclMenuExplanation" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="ChangePasswordForm" method="post" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclOldPassword" runat="server" meta:resourcekey="lclOldPassword" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server" Width="100px" Columns="16" meta:resourcekey="txtOldPasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvOldPasswordResource1"/>
                        <asp:Label ID="lblOldPasswordError" runat="server" ForeColor="Red" Visible="False"
                            meta:resourcekey="lblOldPasswordError"></asp:Label>
                    </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclNewPassword" runat="server" meta:resourcekey="lclNewPassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtNewPassword" runat="server" Width="100px" Columns="16" meta:resourcekey="txtNewPasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvNewPasswordResource1"/>
                        <asp:CompareValidator ID="cvCreateNewPassword" runat="server" ControlToCompare="txtOldPassword"
                            ControlToValidate="txtNewPassword" Operator="NotEqual" meta:resourcekey="cvCreateNewPassword"></asp:CompareValidator>
                    </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRetypePassword" runat="server" meta:resourcekey="lclRetypePassword" /></span><span
                        class="entry">
                        <asp:TextBox TextMode="Password" ID="txtRetypePassword" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtRetypePasswordResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>" meta:resourcekey="rfvRetypePasswordResource1"/>
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="txtNewPassword"
                            ControlToValidate="txtRetypePassword" meta:resourcekey="cvPasswordCheck"></asp:CompareValidator>
                    </span>
            </div>
            <div class="button">
                <asp:Button ID="btnChangePassword" runat="server" OnClick="BtnChangePasswordClick"
                    meta:resourcekey="btnChangePassword" />
            </div>
        </form>
    </div>
</asp:Content>
