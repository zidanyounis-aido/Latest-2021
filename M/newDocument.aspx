<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Mobile.master" AutoEventWireup="true" CodeBehind="newDocument.aspx.cs" Inherits="dms.M.newDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

<asp:Literal ID="ltrScripts" runat="server"></asp:Literal>

    <style type="text/css">
        .style1
        {
            width: 24px;
            height: 24px;
        }
        .style2
        {
            width: 32px;
            height: 32px;
        }
    </style>
    <style>
        .optionBlock {
            float:left;
            width:100%;
            height:300px;
            border:1px solid #808080;
            background-color:#ccc;
            border-radius:15px;
            text-align:center;
            padding-top:20px;
        }

        .optionContent {
            width:100%;
            
            margin:0px auto;

        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <img align="absmiddle" alt="" class="style2" 
        src="../Images/Icons/Actions-document-new-icon.png" />
        <asp:Label ID="lblFolderName" runat="server" Text="New Document"></asp:Label>

    <asp:Panel ID="pnlAddNew" runat="server">
    
    <asp:Panel ID="pnlAttach" runat="server">
    <div class="optionContent">
    <div class="optionBlock">
        <img align="absmiddle" alt="" 
                src="../Images/upload.png" />
        <br /><br />
        <%= (Session["lang"].ToString() == "0") ? "Upload File " : "&#1578;&#1581;&#1605;&#1610;&#1604; &#1605;&#1604;&#1601;"%> 
       <br /><br />
            <asp:FileUpload ID="fluFile" runat="server" />
        <br /><br />
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" CssClass="button">
                <img border="0" src="../Images/Icons/Actions-go-up-icon.png" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Upload" : "&#1578;&#1581;&#1605;&#1610;&#1604; "%> </asp:LinkButton>
    </div>
    </div>
   
     <asp:Label ID="lblStep1" runat="server" ForeColor="Red"></asp:Label>
           
    </asp:Panel>

    <asp:HiddenField ID="hdnURL" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="hdnDocPath" runat="server" />
    <asp:HiddenField ID="hdnUserCode" ClientIDMode="Static" runat="server" />

<br />
<asp:Panel runat="server" ID="pnlDocDetails" Visible="false" TabIndex="1">
<div >
<asp:Image ID="imgFile" Width="100%" runat="server" />
</div>
<div>
<div><%= (Session["lang"].ToString() == "0") ? "Document ID:" : "&#1585;&#1602;&#1605; &#1575;&#1604;&#1605;&#1587;&#1578;&#1606;&#1583;: "%>
            <asp:TextBox ID="txtDocID" ReadOnly="true" runat="server" Width="50px" 
                TabIndex="2"></asp:TextBox>
            &nbsp;<asp:HyperLink ID="lnkCheck" runat="server" Visible="False">Check document</asp:HyperLink>
       </div>
   <div><%= (Session["lang"].ToString() == "0") ? "Document Name:" : "&#1593;&#1606;&#1608;&#1575;&#1606; &#1575;&#1604;&#1605;&#1587;&#1578;&#1606;&#1583; :"%>
            <asp:TextBox ID="txtDocName" runat="server" Width="300px" TabIndex="3"></asp:TextBox></td>
   </div>
    <div>
    <%= (Session["lang"].ToString() == "0") ? "Document Type:" : "&#1606;&#1608;&#1593; &#1575;&#1604;&#1605;&#1587;&#1578;&#1606;&#1583; :"%>
            <asp:DropDownList ID="drpDocTypID" runat="server" AutoPostBack="True" 
                onselectedindexchanged="drpDocTypID_SelectedIndexChanged" TabIndex="-48">
            </asp:DropDownList>
       </div>
    <div>
            <%= (Session["lang"].ToString() == "0") ? "Folder :" : "&#1575;&#1604;&#1605;&#1580;&#1604;&#1583; :"%>
            <asp:DropDownList ID="drpFldrID" runat="server" AutoPostBack="True" 
                onselectedindexchanged="drpFldrID_SelectedIndexChanged" TabIndex="-47">
            </asp:DropDownList>
            <asp:HiddenField ID="hdnFolderSeq" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnDocTypeSeq" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnFolderDocTypeSeq" runat="server" 
                ClientIDMode="Static" />
        </div>
    <asp:Table ID="tblDocMetas" runat="server">
    </asp:Table>
    <br />
   <div>
            <%= (Session["lang"].ToString() == "0") ? "Delegate to :" : " &#1578;&#1605;&#1585;&#1610;&#1585; &#1573;&#1604;&#1609; :"%>
        
            <asp:DropDownList ID="drpNextUser" runat="server" TabIndex="10">
            </asp:DropDownList>
    </div>
       <%--<div>
            <%= (Session["lang"].ToString() == "0") ? "The number of days" : "عدد الايام"%> 
            <input type="text" runat="server" id="txtenddateCount" />
    </div>--%>
    <div>
        <%= (Session["lang"].ToString() == "0") ? "Workflow Timeframe :" : " &#1575;&#1604;&#1581;&#1583; &#1575;&#1604;&#1571;&#1602;&#1589;&#1609; &#1604;&#1605;&#1587;&#1575;&#1585; &#1575;&#1604;&#1593;&#1605;&#1604;  "%>
       
            <asp:DropDownList ID="drpTFType" onChange="convertFrame(this)" runat="server" 
                ClientIDMode="Static" TabIndex="11">
                <asp:ListItem Value="m">Minutes</asp:ListItem>
                <asp:ListItem Value="h">Hours</asp:ListItem>
                <asp:ListItem Value="d" Selected="True">Days</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtWfTimeFrame" runat="server" ClientIDMode="Static" Text="1" 
                Width="75" TabIndex="12"></asp:TextBox>
            <input type="hidden" id="tftype" value="d" />
    </div>
    <div>
       
            <asp:CheckBox ID="chkArchiveOnly" runat="server" Text="Archive Only" 
                TabIndex="14" />

        <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click"  CssClass="button"
        onclientclick="araneasFillAutos()" TabIndex="15">
        <img border="0" src="../Images/Icons/action-save-icon.png" align="absmiddle" />
        <%= (Session["lang"].ToString() == "0") ? "Save Document" : "&#1581;&#1601;&#1592; &#1575;&#1604;&#1605;&#1587;&#1578;&#1606;&#1583;"%></asp:LinkButton>
       </div>
    </div>
</asp:Panel>
</asp:Panel>
    <asp:Panel ID="pnlResult" style="text-align:center" runat="server" Visible="false">
        <br />
        <asp:Label ID="lblResultFinal" runat="server" 
            Text="Document has been added sucussefuly...<br/><br/>You will be automaticly redirected to folder documents page" 
            Font-Size="18px"></asp:Label>
        <asp:Literal ID="ltrRedirect" runat="server"></asp:Literal>
    </asp:Panel>
    
</asp:Content>
