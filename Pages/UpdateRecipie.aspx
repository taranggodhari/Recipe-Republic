<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateRecipie.aspx.cs" Inherits="Pages_UpdateRecipie" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="updaterecipielbl" runat="server" Text="UPDATE YOUR RECIPIE HERE" Font-Bold="true"  BorderStyle="Dashed" BorderWidth="5" BorderColor="Black"></asp:Label>
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

    Submitted By:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="subby" runat="server" Width="166px" Enabled ="false" ReadOnly ="true"></asp:TextBox>
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
        <asp:Label ID="addCategorylbl" runat="server" Text="Add Category: " Visible="false"></asp:Label>
    &nbsp;<asp:TextBox ID="addCategoryText" runat="server" AutoPostBack="true" OnTextChanged="categoryTxt_TextChanged" Visible="false"></asp:TextBox>
    <%--<asp:Button ID="AddBtn" runat="server" OnClick="Add_Click" Text="Add" Visible ="false" />--%>
    <asp:Label ID="errorlbl" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
    <br />
    <br />

    Cooking Time:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox ID="cookinTime" runat="server" style="margin-left: 0px" Width="166px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorck1" runat="server" Display ="Dynamic" 
        ControlToValidate ="recipedesc"
        ErrorMessage="*Require Recipe Description"
        ForeColor ="Red"></asp:RequiredFieldValidator>
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
   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display ="Dynamic" 
        ControlToValidate ="recipedesc"
        ErrorMessage="*Require Recipe Description"
        ForeColor ="Red"></asp:RequiredFieldValidator>
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
    <asp:TextBox ID="recipedesc" runat="server" Height="193px" Width="527px" TextMode ="MultiLine"></asp:TextBox>&nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDesc" runat="server" 
        ControlToValidate ="recipedesc"
        ErrorMessage="*Require Recipe Description"
        ForeColor ="Red"></asp:RequiredFieldValidator>
    <br />
    <br />

<%--Update Ingredient Here--%>
    Edit Ingredient<br />
    <br />

    <asp:GridView ID="grd" runat="server"  AutoGenerateColumns="False" 
        ShowHeaderWhenEmpty="True" BackColor="#DEBA84" BorderColor="#DEBA84"
         BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" OnRowDeleting ="grid_DeletingButtonPressed" >
    <Columns>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Ingredient">
            <ItemTemplate>
                <asp:TextBox ID="txtIngredient" runat="server" Text='<%# Eval("IngredientName") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                ControlToValidate="txtIngredient"
                ErrorMessage="*Require Ingredient Name"
                ForeColor="Red"
                Display="Dynamic"
                SetFocusOnError="true"
                ValidationGroup="AddingIngredient"
                    ></asp:RequiredFieldValidator>
            </ItemTemplate>



<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Quantity">
            <ItemTemplate>
                <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                    runat="server" 
                    ControlToValidate="txtQuantity"
                    ValidationExpression="\d+"
                    ErrorMessage="*Only Numbers are allowed here"
                    ForeColor="Red"
                    Display="Dynamic"
                    ValidationGroup="AddingIngredient"
                    SetFocusOnError="true"
                    ></asp:RegularExpressionValidator>
               
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Unit">
            <ItemTemplate>
                <asp:TextBox ID="txtUnit" runat="server" Text='<%# Eval("Unit") %>'></asp:TextBox>
            </ItemTemplate>
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
        </asp:TemplateField>

        <asp:CommandField ShowDeleteButton="True" />

    </Columns>

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
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <br />
<asp:Button ID="btnAddRow" runat="server" OnClick="btnAddRow_Click" Text="Add Ingredient" ValidationGroup="AddingIngredient" CssClass="myButton" />
<p>
    <asp:Button ID="updateButton" runat="server" OnClick="updateButton_Click" Text="Update Recipie" Height="36px" Width="212px" CssClass="myButton" />
</p>
    <p>
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
</p>
    <p>
        &nbsp;</p>

    <br />
    <br />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />

 
</asp:Content>

