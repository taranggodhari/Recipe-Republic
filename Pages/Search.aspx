<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Pages_Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <strong>
    <br />
    Name Of Ingredients: <asp:DropDownList ID="ingridentDropDown" runat="server" AppendDataBoundItems="True" DataTextField ="INGREDIENTNAME" DataValueField="INGREDIENTNAME">
                         <asp:ListItem Text="Any" Selected ="True"/>
                         </asp:DropDownList>
    <br />
    <br />
    Submitted By:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong> <asp:DropDownList ID="submitDropDown" runat="server" AppendDataBoundItems="True" DataTextField ="SUBMITTEDBY" DataValueField="SUBMITTEDBY">
                                                                         <asp:ListItem Text="All" Selected ="True" />
                                                                            </asp:DropDownList>
    
    <strong>
    <br />
    <br />
    Category:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong>  <asp:DropDownList ID="categoryDropDown" runat="server" AppendDataBoundItems="True" DataTextField ="CATEGORY" DataValueField="CATEGORY">
                                                                                                 <asp:ListItem Text="All" Selected ="True" />
                                                                                                 </asp:DropDownList>
     
          
     
    <br />
    <br />
    <asp:Button ID="submit" runat="server" Text="Submit" OnClick="submit_Click" CssClass="myButton"/>
   <br/><h2 align="center" style="margin-left: 40px"> <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </h2>
    <p align="center"> 
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
    </p>
 

    
</asp:Content>




