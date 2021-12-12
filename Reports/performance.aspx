<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="performance.aspx.cs" Inherits="dms.Reports.performance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .canvasjs-chart-credit
        {
        display:none !important;
        }

        #chartContainer {
            width:435px !important;
            overflow:hidden;
            height:200px;
        }

        .canvasjs-chart-container {
                text-align: inherit !important;
        }
    </style>

    <script src="../JS/canvasjs-1.6.2/jquery.canvasjs.min.js" type="text/javascript"></script>
    
    <asp:Literal ID="ltrScript" runat="server"></asp:Literal>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="inner_header hide">
        <%= (Session["lang"].ToString() == "0") ? "Choose Date Range" : "حدد الفترة "%> :
        <%= (Session["lang"].ToString() == "0") ? "From" : "من"%>
        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="dateFeild"></asp:TextBox>
        <%= (Session["lang"].ToString() == "0") ? "To" : "إلى"%>
        <asp:TextBox ID="txtDateTo" runat="server" CssClass="dateFeild"></asp:TextBox>
        <asp:Button ID="btnRun" runat="server" Text="تنفيذ" OnClick="btnRun_Click" />
    </div>
    <div class="hide" style="margin-top:10px">
        <img src="../Images/icons/print.png" style="cursor:pointer" onclick="printOut()" />
    </div>
    <div id="chartContainer" style="height: 300px; width: 460px;"></div>
</asp:Content>
