<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Web_Form.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" >Name : </asp:Label>
            <asp:TextBox runat="server" ID="name"></asp:TextBox>
            <asp:Button runat="server" ID="submitBtn" Text="Submit" OnClick="submitBtn_Click"/>
        </div>
       
        <div>
            <asp:Label runat="server" ID="namePrint"> </asp:Label>

        </div>
    </form>
</body>
 
</html>
