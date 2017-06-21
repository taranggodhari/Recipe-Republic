<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Setup.aspx.cs" Inherits="Pages_Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     Choose Theme
    <div>
        
     <p>Dark<asp:RadioButton ID="rdbDark" GroupName="ThemeChanger" runat="server" /></p>   
     <p>Light<asp:RadioButton ID="rdbLight" GroupName="ThemeChanger" runat="server" /></p>    
      <asp:Button ID="btnSet" runat="server" OnClick="btnSet_Click" Text="Change Color" CssClass="myButton" />
    </div>
    

</asp:Content>

