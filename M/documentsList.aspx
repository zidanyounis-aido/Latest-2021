<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobile.Master" AutoEventWireup="true" CodeBehind="documentsList.aspx.cs" Inherits="dms.M.documentsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
            height:50px;
            margin-top:-50px;
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

<%--    <script>
        $(document).ready(function () {
            resizeDiv();
            window.onresize = function () {

                resizeDiv();
            }
            
        });

        function resizeDiv() {
            resizeTbl();
                document.getElementById("grdDocumentsDiv").style.height = String( window.innerHeight - 200) + "px";
            }
        
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding:10px">
    <div style="float:left; width:40%">
    <asp:Image ID="fldrIcon" runat="server" ImageAlign="AbsMiddle" 
    ImageUrl="../Images/Icons/folder-documents-icon.png" />
&nbsp;<asp:Label ID="lblFolderName" runat="server" Text="Label"></asp:Label>
        </div>
    <div style="float:right; width:60%">
    
    <asp:TextBox ID="txtSearch" runat="server" placeholder="Search"></asp:TextBox>
&nbsp;<asp:LinkButton ID="lnkSearch" runat="server" CssClass="button" onclick="lnkSearch_Click">
<img border="0" src="../Images/Icons/Search-icon.png" align="absmiddle" />
 </asp:LinkButton>

    </div>
        </div>
    <br style="clear:both; margin-bottom:10px" />
    
    <div style="width:100%; background-color:#5C5C5C; color:White">
    <table>
        <tr>
            <td>
    <asp:HyperLink ID="lnkAddDoc" runat="server"  CssClass="button">
    <img align="absmiddle" alt="" border="0" 
        src="../Images/Icons/Actions-document-new-icon.png" width="16px" />
   <%= (Session["lang"].ToString() == "0") ? "New Doc" : "ملف جديد"%> </asp:HyperLink></td>
                <td style="width:10px"></td>
            <td valign="middle">
                 <%= (Session["lang"].ToString() == "0") ? "Sort" : "ترتيب "%>  </td><td valign="middle">
                <asp:DropDownList ID="drpSortBy" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="drpSortBy_SelectedIndexChanged" Width="75px">
                    <asp:ListItem Selected="True" Value="docID">Serial</asp:ListItem>
                    <asp:ListItem Value="docName">Document Name</asp:ListItem>
                    <asp:ListItem Value="docTypID">Document Type</asp:ListItem>
                    <asp:ListItem Value="addedDate">Added Date</asp:ListItem>
                    <asp:ListItem Value="addedUserID">Added User</asp:ListItem>
                    <asp:ListItem Value="modifyDate">Modify Date</asp:ListItem>
                </asp:DropDownList>
                </td><td valign="middle">
            &nbsp;<asp:RadioButtonList ID="rdoOrderType" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="rdoOrderType_SelectedIndexChanged" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value=" ">A-Z</asp:ListItem>
                    <asp:ListItem Value="desc" Selected="True">Z-A</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    </div>
    <div style="width:100%; background-color:#fff;">
     <%= (Session["lang"].ToString() == "0") ? "Page Number " : "الصفحة"%>
     : <asp:DropDownList ID="drpPager1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPager1_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <asp:GridView ID="grdDocuments" ClientIDMode="Static" runat="server" AllowPaging="False"
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
        GridLines="Vertical" onpageindexchanging="grdDocuments_PageIndexChanging" 
        onselectedindexchanged="grdDocuments_SelectedIndexChanged"  
         onrowdeleting="grdDocuments_RowDeleting">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="docID" HeaderText="Serial" ItemStyle-Width="65px" HeaderStyle-Width="65px" />
            <asp:TemplateField HeaderText="Document Name" >
                <ItemTemplate>
                    <a class="button" style="display: block;" href="../M/documentInfo.aspx?docID=<%#c.encrypt(Eval("docID").ToString()) %>">
                        <%# Eval("docName") %>
                    </a>
                    <div style="font-size:10px;">
                    Type : <%# getDocTypeDesc(c.convertToInt32(Eval("docTypID"))) %>
                    <br />
                    Added in :  <%# Convert.ToDateTime(Eval("addedDate")).ToString("d-MMM-yy") %>
                    <br />
                    By : <%# c.getUserName(c.convertToInt32(Eval("addedUserID"))) %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:CommandField ShowDeleteButton="True"  ItemStyle-Width="25px" HeaderStyle-Width="25px"
            ButtonType="Image" DeleteImageUrl="../Images/Icons/Actions-stop-icon.png"/>
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
    
</asp:Content>
