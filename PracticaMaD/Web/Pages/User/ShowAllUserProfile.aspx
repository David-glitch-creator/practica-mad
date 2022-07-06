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
    <asp:GridView ID="gvProds" runat="server"  onrowcommand="gvProds_RowCommand" AutoGenerateColumns="False"  OnPageIndexChanging="gvProdsPageIndexChanging" ShowHeaderWhenEmpty ="True"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" SelectedRowStyle-HorizontalAlign="Center" SelectedRowStyle-VerticalAlign="Middle" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" PagerStyle-HorizontalAlign="Left" PagerStyle-VerticalAlign="Bottom" HorizontalAlign="Center" OnSelectedIndexChanged="gvProds_SelectedIndexChanged" meta:resourcekey="gvProdsResource1">
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
             
                <asp:hyperlinkfield   headertext="Alias" datatextfield="Alias"  
      datanavigateurlformatstring="./ShowFollowers.aspx?txtName={0}" meta:resourcekey="HyperLinkFieldResource1" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" meta:resourcekey="BoundFieldResource2" />
                <asp:buttonfield buttontype="Button" 
            commandname="Select"
            headertext="Segir" 
             meta:resourcekey="ButtonFieldResource1"/>
            </Columns>
          
              
    </asp:GridView >
    
</asp:Content>