<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RecipieDetail.aspx.cs" Inherits="Pages_RecipieDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    Recipie Detail:<br />
    <asp:DetailsView id="recipieDetails" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" OnItemDeleting="grid_RowDeleting"  OnModeChanging ="grid_RowChanging" >
        <EditRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <Fields>
            <asp:CommandField DeleteText="Delete Recipie" ShowDeleteButton="True"  />
            <asp:CommandField ShowEditButton="True" />
        </Fields>
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
         
    </asp:DetailsView>

    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

    <br />

    Ingredient Detail:<br />  
    <asp:GridView ID="ingredientGrid" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
        
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
    </asp:GridView>
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    <br />
    User Comments<br />
    <asp:GridView ID="userComments" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
        
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
    </asp:GridView>
    <br />

  <%--<style type ="text/css">
      .starRating{
           font-size: 0pt;
    width: 30px;
    height: 30px;
    margin: 0px;
    padding: 0px;
    cursor: pointer;
    display: block;
    background-repeat: no-repeat;
      }
      .FilledStars{
          background-image:url("Images/star_fill.png");
          
      }
      .EmptyStars{
          background-image:url("Images/star_empty.png");
          }
      .WantingStars{
          background-image:url("Images/star_wanting.png");
          }
  </style>
    <br />
    Rate:
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Rating ID="Rating1" runat="server"
            OnChanged="OnRatingChanged"
            CurrentRating ="1"
            MaxRating="5"
        StarCssClass ="starRating"
        FilledStarCssClass="FilledStars"
        EmptyStarCssClass="EmptyStars"
        WaitingStarCssClass ="WantingStars"
        AutoPostBack ="true"
            RatingAlign="Horizontal"
        ></asp:Rating>
        
    </ContentTemplate>
         
   </asp:UpdatePanel>
    <br />
    <br />--%>
    Username<br />
    <asp:TextBox ID="commentusername" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp; <asp:Literal ID="ErrorMessage" runat="server" ></asp:Literal>
    <br />
    <br />
    Comments<br />
&nbsp;<asp:TextBox ID="recipiecomment" runat="server" Height="193px" Width="527px" TextMode="multiline"></asp:TextBox>

  
    <br />
    <br />
    <asp:Button ID="subCommentBtn" runat="server" Text="Submit Comment" OnClick="subCommentBtn_Click" CssClass="myButton" />

  
</asp:Content>

