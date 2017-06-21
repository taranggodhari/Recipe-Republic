<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IngredientList.ascx.cs" Inherits="IngredientList" %>


    <style type ="text/css">
        .btnAddRow {
	-moz-box-shadow:inset 0px 1px 0px 0px #ffffff;
	-webkit-box-shadow:inset 0px 1px 0px 0px #ffffff;
	box-shadow:inset 0px 1px 0px 0px #ffffff;
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #f9f9f9), color-stop(1, #e9e9e9));
	background:-moz-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
	background:-webkit-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
	background:-o-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
	background:-ms-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
	background:linear-gradient(to bottom, #f9f9f9 5%, #e9e9e9 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#f9f9f9', endColorstr='#e9e9e9',GradientType=0);
	background-color:#f9f9f9;
	-moz-border-radius:6px;
	-webkit-border-radius:6px;
	border-radius:6px;
	border:1px solid #dcdcdc;
	display:inline-block;
	cursor:pointer;
	color:#666666;
	font-family:Arial;
	font-size:15px;
	font-weight:bold;
	padding:6px 24px;
	text-decoration:none;
	text-shadow:0px 1px 0px #ffffff;
}
.btnAddRow:hover {
	background:-webkit-gradient(linear, left top, left bottom, color-stop(0.05, #e9e9e9), color-stop(1, #f9f9f9));
	background:-moz-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
	background:-webkit-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
	background:-o-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
	background:-ms-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
	background:linear-gradient(to bottom, #e9e9e9 5%, #f9f9f9 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#e9e9e9', endColorstr='#f9f9f9',GradientType=0);
	background-color:#e9e9e9;
}
.btnAddRow:active {
	position:relative;
	top:1px;
}
        .auto-style1 {
            height: 65px;
        }
    </style>


<asp:GridView ID="grd" runat="server" DataKeyNames="Ingredient" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" >
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Ingredient">
            <ItemTemplate>
                <asp:TextBox ID="txtIngredient" runat="server" Text='<%# Eval("Ingredient") %>'></asp:TextBox>
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
<asp:Button ID="btnAddRow"  runat="server" OnClick="btnAddRow_Click" Text="Add Ingredient" ValidationGroup="AddingIngredient" CssClass="btnAddRow" />
<p>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</p>
