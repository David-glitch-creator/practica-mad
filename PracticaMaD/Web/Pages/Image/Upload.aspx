<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
    CodeBehind="Upload.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Image.Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="UploadForm" method="post" runat="server">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclImageTitle" runat="server" meta:resourcekey="lclImageTitle" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtImageTitle" runat="server" Width="100px"
                            Columns="16" meta:resourcekey="txtImageTitleResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvImageTitle" runat="server" ControlToValidate="txtImageTitle"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvImageTitleResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclImageDescription" runat="server" meta:resourcekey="lclImageDescription" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtImageDescription" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtImageDescriptionResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvImageDescription" runat="server" ControlToValidate="txtImageDescription"
                            Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                            meta:resourcekey="rfvImageDescriptionResource1"></asp:RequiredFieldValidator></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclAperture" runat="server" meta:resourcekey="lclAperture" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtAperture" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtApertureResource1"></asp:TextBox>
                        </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclExposureTime" runat="server" meta:resourcekey="lclExposureTime" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtExposureTime" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtExposureTimeResource1"></asp:TextBox>
                        </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclIso" runat="server" meta:resourcekey="lclIso" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtIso" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtIsoResource1"></asp:TextBox>
                        </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclWhiteBalance" runat="server" meta:resourcekey="lclWhiteBalance" /></span><span
                        class="entry">
                        <asp:TextBox ID="txtWhiteBalance" runat="server" Width="100px" Columns="16"
                            meta:resourcekey="txtWhiteBalanceResource1"></asp:TextBox>
                        </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCategory" runat="server" meta:resourcekey="lclCategory" /></span><span
                        class="entry">
                        <asp:DropDownList ID="comboCategory" runat="server" AutoPostBack="True"
                            Width="100px" meta:resourcekey="comboCategoryResource1"
                            OnSelectedIndexChanged="ComboCategorySelectedIndexChanged">
                        </asp:DropDownList></span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclImageFile" runat="server" meta:resourcekey="lclImageFile" /></span><span
                        class="entry">
                        <asp:FileUpload ID="fuImageFile" accept=".jpg" runat="server" CssClass="form-control"/>
                        </span>
            </div>
            <div class="button">
                <asp:Button ID="btnUpload" runat="server" OnClick="BtnUploadClick" meta:resourcekey="btnUpload" />
            </div>
        </form>
    </div>
</asp:Content>
