<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Web_Form.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="userForm" runat="server">
        <h2>User Information</h2>
        <div style="margin-top : 20px;">
            <label for="name">Name:</label>
            <asp:TextBox ID="name" runat="server"></asp:TextBox>
        </div>
         <div style="margin-top : 10px;">
            <label for="email">Email:</label>
            <asp:TextBox ID="email" runat="server" TextMode="Email"></asp:TextBox>
        </div>
       <div style="margin-top : 10px;">
            <label for="password">Password:</label>
            <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div style="margin-top : 10px;">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="submitBtn_Click" />
        </div>
    </form>
        <div style="margin-top : 20px;">
            <asp:Label runat="server" ID="namePrint"> </asp:Label>
        </div>
        
        <div style="margin-top : 20px;">
            <asp:Label runat="server" ID="emailPrint"> </asp:Label>
        </div>
        
        
</body>
 
</html>
