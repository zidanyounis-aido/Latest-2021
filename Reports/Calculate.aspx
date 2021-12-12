<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="Calculate.aspx.cs" Inherits="dms.Reports.Calculate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .FixedHeader {
            /*position: absolute;*/
            font-weight: bold;
            height:50px;
            margin-top:-50px;
                font-size: 12px;
        }

       #grdDocuments{
            table-layout:fixed;
            width:100%;
        }
            
        #grdDocuments td {
            max-width:200px;
            word-wrap:break-word;
            white-space: normal;
        }

        #grdDocumentsDiv {
            overflow:auto;
            margin-top:50px;
             
        }
    </style>
    
    <script>
        $(document).ready(function () {
            resizeDiv();
            window.onresize = function () {

                resizeDiv();
            }
            
        });

        function resizeDiv() {
            resizeTbl();
                document.getElementById("grdDocumentsDiv").style.height = String( window.innerHeight - 215) + "px";
            }
        
    </script>
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
      <div id="grdDocumentsDiv">
    <asp:GridView ID="grdDocuments" ClientIDMode="Static" runat="server" AllowPaging="False"
        HeaderStyle-CssClass="FixedHeader" 
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
        GridLines="Vertical" onpageindexchanging="grdDocuments_PageIndexChanging" Width="100%" ShowFooter="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="fldrNameAr" HeaderText="Port Name" />
            <asp:BoundField DataField="sum1" 
                HeaderText="Total Amount" DataFormatString="{0:#,0.00}" />
            <asp:BoundField DataField="sum2" DataFormatString="{0:#,0.00}" HeaderText="Total Received" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
