<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddRecipes.aspx.cs" Inherits="Pages_AddRecipes" %>




<%@ Register src="../IngredientList.ascx" tagname="IngredientList" tagprefix="uc2" %>
<%@ Register Src="~/IngredientList.ascx" TagPrefix="uc1" TagName="IngredientList" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        #TextArea1 {
            height: 223px;
            width: 722px;
        }
        #recdesc {
            height: 234px;
            width: 585px;
        }
    </style>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    <br />
    <br />
    Recipe Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
     
    <asp:TextBox ID="rname" runat="server" Width="166px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorRecipie" runat="server" 
        ControlToValidate ="rname"
        ErrorMessage="*Require Name of recipe"
        ForeColor ="Red"></asp:RequiredFieldValidator>
    <br />
    <br />

    Submitted By:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="subby" runat="server" Font-Size="Medium" Font-Bold="true" Width="166px" Enabled ="false"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidatorSubmitted" runat="server" 
        ControlToValidate ="subby"
        ErrorMessage="*Require Name of Submitter"
        ForeColor ="Red"></asp:RequiredFieldValidator>
    <br />
    <br />
    Category:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
   <asp:DropDownList ID="categoryText"  AppendDataBoundItems="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="categoryText_SelectedIndexChanged">
       <asp:ListItem>--Select Your Catagory--</asp:ListItem>
   </asp:DropDownList>
        &nbsp;&nbsp;<asp:Label ID="addCategorylbl" runat="server" Text="Add Category: " Visible="false"></asp:Label>
&nbsp;<asp:TextBox ID="addCategoryText" runat="server" Visible ="false" AutoPostBack="true" OnTextChanged ="categoryTxt_TextChanged"></asp:TextBox>
    <%--<asp:Button ID="AddBtn" runat="server" OnClick="Add_Click" Text="Add" Visible ="false" />--%>
    <asp:Label ID="errorlbl" runat="server" Text="" ForeColor ="Red" Visible ="false"></asp:Label>
    <br />
    <br />

    Cooking Time:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox ID="cookinTime" runat="server" style="margin-left: 0px" Width="166px"></asp:TextBox>

    <asp:RegularExpressionValidator id="RegularExpressionValidatorCk"
                   ControlToValidate="cookinTime"
                   ValidationExpression="\d+"
                   Display="Dynamic"
                   EnableClientScript="true"
                   ErrorMessage="Please enter numbers only"
                   runat="server"/>
    <br />
    <br />

    Number of Servings:
    <asp:TextBox ID="numserv" runat="server" Width="166px"></asp:TextBox>
  
    <asp:RegularExpressionValidator id="RegularExpressionValidator2"
                   ControlToValidate="numserv"
                   ValidationExpression="\d+"
                   Display="Dynamic"
                   EnableClientScript="true"
                   ErrorMessage="Please enter numbers only"
                   runat="server"/>
    <br />
    <br />
    
    Recipe Description: <br />
    &nbsp;
    <asp:TextBox ID="recipedesc" runat="server" Height="193px" Width="527px" TextMode="multiline"></asp:TextBox>  
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDesc" runat="server" 
        ControlToValidate ="recipedesc"
        ErrorMessage="*Require Recipe Description"
        ForeColor ="Red"></asp:RequiredFieldValidator>
    <br />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />

 

    <uc1:IngredientList runat="server" ID="IngredientList" />

 
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

 
    <asp:Button ID="SaveBtn" runat="server" OnClick="Savebtn_Click" Text="Submit Recipie" Width="188px" Height="44px" CssClass="myButton" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:Button ID="CancelBtn" runat="server" Text="Cancel" Width="164px" OnClick="CancelBtn_Click" Height="42px" CssClass="myButton" />
    <br />
    
  
</asp:Content>

