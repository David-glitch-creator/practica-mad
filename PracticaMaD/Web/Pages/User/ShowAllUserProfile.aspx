<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/PracticaMaD.Master"
    CodeBehind="ShowAllUserProfile.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.User.ShowAllUserProfile"%>

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
    <form id="form1" runat="server">

            <asp:Button ID="btnFindByLogin" runat="server" OnClick="BtnFindByLoginClick" Text="<%$ Resources:Common, btnFindByLogin %>" meta:resourcekey="btnFindByLogin" />               
                &nbsp;&nbsp;                
            <asp:Button ID="btnAllLogins" runat="server" OnClick="BtnShowAllLoginsClick" Enabled="false" Text="<%$ Resources:Common, btnAllLogins %>" />
            
            <br />
            <br />
        
        <asp:GridView ID="gvUsers" runat="server"  onrowcommand="gvUsers_RowCommand" AutoGenerateColumns="False"  OnPageIndexChanging="gvUsersPageIndexChanging" ShowHeaderWhenEmpty ="True"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" SelectedRowStyle-HorizontalAlign="Center" SelectedRowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" PagerStyle-HorizontalAlign="Left" PagerStyle-VerticalAlign="Bottom" HorizontalAlign="Center" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" meta:resourcekey="gvUsersResource1">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />


            <Columns>
             
               <asp:hyperlinkfield   headertext="Alias" datatextfield="loginName" datanavigateurlfields="loginName"
                   datanavigateurlformatstring="~/Pages/User/ShowFollowers.aspx?txtName={0}"/>
                <asp:BoundField DataField="firstName" HeaderText="Nombre" meta:resourcekey="BoundFieldResource" />
                <asp:buttonfield buttontype="Button" 
            commandname="Select"
            headertext="Segir" 
             meta:resourcekey="ButtonFieldResource1" />
            </Columns>
          
              
    </asp:GridView >
    <span class="label">
                    <asp:Localize ID="lblFollow" runat="server" Text ="Siguiendolo" Visible="False" meta:resourcekey="lblFollow" />
                </span>
    </form>
</asp:Content>