<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="dms.enc.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Encrypted Text : <asp:TextBox ID="TextBox1" runat="server" Width="1006px"></asp:TextBox>
            <br />
            Decrypted Text : <asp:TextBox ID="TextBox2" runat="server" Width="1000px"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Do" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
