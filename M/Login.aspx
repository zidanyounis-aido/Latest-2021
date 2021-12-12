<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="dms.M.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ZY Documents</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href='http://fonts.googleapis.com/css?family=Ubuntu:500' rel='stylesheet' type='text/css'>
    <link href="../css/mobile_Login.css" rel="stylesheet" />
    <script src="../JS/jquery-1.7.2.min.js"></script>
    <script>
        $('.error-page').hide(0);

        $('.login-button , .no-access').click(function () {
            $('.login').slideUp(500);
            $('.error-page').slideDown(1000);
        });

        $('.try-again').click(function () {
            $('.error-page').hide(0);
            $('.login').slideDown(1000);
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    
<div class="login">
  <div class="login-header">
    <img src="../Images/DMSLogo.png" />
  </div>
  <div class="login-form">
    <h3>Username:</h3>
        <asp:TextBox runat="server" ID="txtUserName" placeholder="Username"/>
      <br>
    <h3>Password:</h3>
        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"  placeholder="Password"/>

    <br>
       <asp:RadioButtonList ID="rdoLang" runat="server" RepeatDirection="Horizontal" 
        Font-Size="12px">
        <asp:ListItem Value="0" Selected="True">English</asp:ListItem>
        <asp:ListItem Value="1">عربي</asp:ListItem>
    </asp:RadioButtonList>
      <br />
      <asp:Button runat="server" ID="submit" class="login-button" Text="Login" 
        onclick="lnkLogin_Click" />
    <br />

<%--    <a class="sign-up">Sign Up!</a>
    <br>
    <h6 class="no-access">Can't access your account?</h6>--%>
      <asp:Label ID="lblResult" runat="server" ForeColor="#CC0000" Font-Size="12px"></asp:Label>
      <br />
  </div>
</div>
    </form>
</body>
</html>
