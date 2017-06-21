<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Recipes.aspx.cs" Inherits="Pages_Recipes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
    &nbsp;&nbsp;Recipie List<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecipieConnection %>" SelectCommand="SELECT [RECIPIEID], [RECIPIENAME], [SUBMITTEDBY], [PREPERATIONTIME] FROM [RECIPIEDETAILS]"></asp:SqlDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetRecipe" TypeName="RecipeRepository"></asp:ObjectDataSource>
&nbsp;
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" CellPadding="4" OnSelectedIndexChanged="grid_SelectedIndexChanged" ForeColor="#333333" GridLines="None" >
         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
         <Columns>
            
             <asp:BoundField DataField="RECIPIEID" HeaderText="Recipe No." />
             <asp:BoundField DataField="RECIPIENAME" HeaderText="Recipie Name"  />
            <asp:BoundField DataField="SUBMITTEDBY" HeaderText="Submitted By" />
            <asp:BoundField DataField="PREPERATIONTIME" HeaderText="Preparation time"  />
             <asp:CommandField ShowSelectButton="True" />
        </Columns>

         <EditRowStyle BackColor="#999999" />

        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

    <br />

    <asp:SqlDataSource ID="SqlDataSource3" runat="server"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>

    <asp:DetailsView id="recipieDetails" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" >
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <br />

</asp:Content>

