<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Redirect.aspx.cs" Inherits="dms.Screen.Redirect" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>شركة اشراق الريادة</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <style>
        /* general style */
        .loader-holder {
            position: absolute;
            left: 0;
            right: 0;
            bottom: 0;
            top: 0;
            z-index: 999999999;
            background: rgb(255 255 255 / 53%);
        }
            .loader-holder img {
                width: 100px;
                position: fixed;
                top: 50%;
                right: 50%;
                transform: translate(50%, -50%);
            }
    </style>
</head>
<body style="overflow: hidden;">
    <div class="loader-holder">
        <img src="/Assets/UIKIT/img/loader.svg" class="loader-img">
    </div>
</body>
</html>
