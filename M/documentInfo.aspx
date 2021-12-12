<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobile.master" AutoEventWireup="true" CodeBehind="documentInfo.aspx.cs" Inherits="dms.M.documentInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

    <style type="text/css">
        .cellUnderline {
            border-bottom: 1px dashed #ccc;
        }

        .blueTxt {
            font-weight: bold;
            color: #003366;
            width: 25%;
        }

        .optionDiv {
            padding: 5px 15px;
            border-top: 1px dotted #f3f1f1;
        }
    </style>

    <script type="text/javascript">

        function PrintElem(elem) {
            Popup($(elem).html());
        }

        function Popup(data) {
            var mywindow = window.open('', 'SISCOM_Documents', 'height=400,width=600');
            mywindow.document.write('<html><head><title>SISCOM Documents</title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');

            mywindow.print();
            //mywindow.close();

            //return true;
        }

        function convertFrame(e) {
            var currentValue = parseFloat(document.getElementById("txtWfTimeFrame").value);
            var currentType = document.getElementById("tftype").value;
            var newType = e.value;
            var newValue;
            if (currentType == 'm') {
                switch (newType) {
                    case 'h':
                        newValue = currentValue / 60;
                        break;
                    case 'd':
                        newValue = currentValue / 3600;
                        break;
                }
            }
            if (currentType == 'h') {
                switch (newType) {
                    case 'm':
                        newValue = currentValue * 60;
                        break;
                    case 'd':
                        newValue = currentValue / 60;
                        break;
                }
            }
            if (currentType == 'd') {
                switch (newType) {
                    case 'm':
                        newValue = currentValue * 3600;
                        break;
                    case 'h':
                        newValue = currentValue * 60;
                        break;
                }
            }
            //        alert(newType);
            //        alert(currentValue);
            //        alert(newValue);
            if (newValue % 1 == 0) {
                document.getElementById("txtWfTimeFrame").value = String(newValue);
            }
            else {
                document.getElementById("txtWfTimeFrame").value = newValue.toFixed(2);
            }
            document.getElementById("tftype").value = e.options[e.selectedIndex].value;
        }
    </script>

    <script>
        $(document).ready(function () {
            $("#accordion").accordion({
                heightStyle: "content"
            });
        });

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <img align="absmiddle" alt=""
        src="../Images/Icons/Apps-text-editor-icon.png" /><asp:Label ID="lblFolderName" runat="server" Text="Edit Document"></asp:Label>
    &nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblDocCount" runat="server" Text="1 Attachment(s)"
            Font-Size="12px"></asp:Label>

    <asp:HiddenField ID="hdnURL" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnDocPath" runat="server" />
    <asp:HiddenField ID="hdnUserCode" ClientIDMode="Static" runat="server" />
    <div style="background-color: #fff; padding: 0px 5px">
        <div>
            <%= (Session["lang"].ToString() == "0") ? "Document ID:" : "رقم الملف"%>

            <asp:TextBox ID="txtDocID" ReadOnly="True" runat="server" Width="50px"
                TabIndex="2"></asp:TextBox>
        </div>
        <div>
            <%= (Session["lang"].ToString() == "0") ? "Document Name:" : "اسم الملف"%>
            <asp:TextBox ID="txtDocName" runat="server" Width="100%" TabIndex="3"></asp:TextBox>
        </div>
        <div>
            <%= (Session["lang"].ToString() == "0") ? "Document Type:" : "نوع الملف"%>
            <asp:DropDownList ID="drpDocTypID" runat="server" AutoPostBack="True"
                OnSelectedIndexChanged="drpDocTypID_SelectedIndexChanged" TabIndex="-48">
            </asp:DropDownList>
            <asp:HiddenField ID="hdnDocExt" runat="server" />
            <asp:HiddenField ID="hdnAddedDate" runat="server" />
        </div>
        <div>
            <%= (Session["lang"].ToString() == "0") ? "Folder" : "مجلد"%>
            <asp:DropDownList ID="drpFldrID" runat="server" TabIndex="-47" Enabled="False">
            </asp:DropDownList>
            <asp:HiddenField ID="hdnAddedUserID" runat="server" />
            <asp:HiddenField ID="hdnFolderSeq" runat="server" />
            <asp:HiddenField ID="hdnDocTypeSeq" runat="server" />
            <asp:HiddenField ID="hdnFolderDocTypeSeq" runat="server" />
            <asp:HiddenField ID="hdnOcrContent" runat="server" />
        </div>
        <div>
            <%= (Session["lang"].ToString() == "0") ? "Workflow Timeframe :" : " الحد الأقصى لمسار العمل  "%>
            <asp:HiddenField ID="hdnWfStatus" runat="server" />
            <asp:DropDownList ID="drpTFType" runat="server" ClientIDMode="Static"
                onChange="convertFrame(this)" TabIndex="4">
                <asp:ListItem Value="m">Minutes</asp:ListItem>
                <asp:ListItem Value="h">Hours</asp:ListItem>
                <asp:ListItem Selected="True" Value="d">Days</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtWfTimeFrame" runat="server" ReadOnly="True" Width="75px"
                ClientIDMode="Static" TabIndex="5"></asp:TextBox>
            <input type="hidden" id="tftype" value="d" />
        </div>
        <asp:Table ID="tblDocMetas" runat="server" CellSpacing="0" CellPadding="3" Width="100%">
        </asp:Table>

        <table>
            <tr>
                <td width="100px">
                    <asp:LinkButton ID="LinkButton1" CssClass="button" runat="server" OnClick="LinkButton1_Click"
                        TabIndex="22">
                <img border="0" src="../Images/Icons/action-save-icon.png" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Save" : "حفظ"%></asp:LinkButton>
                </td>
                <td style="width: 50px"></td>
                <td width="100px">
                    <asp:LinkButton ID="LinkButton2" CssClass="button" runat="server" OnClick="LinkButton2_Click"
                        TabIndex="23">
                <img border="0" src="../Images/Icons/System-Folder-icon.png" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Delete" : "حذف"%></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <br />

    <div id="accordion" style="background-color: #fff">
        <h3>Attachments</h3>
        <div>
            <asp:LinkButton ID="lnkPDF" CssClass="button" runat="server" OnClick="lnkPDF_Click"
                TabIndex="21">
<img border="0" src="../Images/Icons/pdf.png" align="absmiddle" />
<%= (Session["lang"].ToString() == "0") ? "Download PDF" : "تنزيل ملف PDF"%></asp:LinkButton>
            <br />
            <asp:Table ID="tblVersions" runat="server">
            </asp:Table>
            <asp:Label ID="lblRes" runat="server" ForeColor="Red"></asp:Label>
            <asp:HiddenField ID="hdnLastVersion" runat="server" />
            <br />
            <hr />
            <div style="background-color: #ccc">
                <img src="../Images/icons/Open-folder-add-icon.png" align="absmiddle" />
                <asp:Label ID="Label1" runat="server" Text="Add new attachment:"></asp:Label>
                <asp:FileUpload ID="fluFile" runat="server" TabIndex="20" />
                <%= (Session["lang"].ToString() == "0") ? "Attachment Classification" : "تصنيف المرفق"%>
                <asp:DropDownList ID="drpDocGroupID" runat="server"></asp:DropDownList>
                &nbsp;<asp:LinkButton ID="LinkButton3" CssClass="button" runat="server" OnClick="LinkButton3_Click"
                    TabIndex="21">
            <img border="0" src="../Images/Icons/Actions-go-up-icon.png" align="absmiddle" />
            <%= (Session["lang"].ToString() == "0") ? "Upload" : "تحميل"%></asp:LinkButton>


            </div>
        </div>
        <h3>Workflow</h3>
        <div>
            <a href="javascript:PrintElem('#divWF')">
                <img src="../Images/Icons/print.png" border="0" align="absmiddle" />
                <%= (Session["lang"].ToString() == "0") ? "Print Workflow" : "طباعة مسار العمل"%>
            </a>
            <br />
            <div id="divWF">

                <asp:Repeater ID="rptWorkflow" runat="server">
                    <HeaderTemplate>
                        <table cellspacing="0" cellpadding="5" style="width: 100%">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <span style="font-weight: bold; color: #AE0000"><%# getCounter() %></span>
                            </td>
                            <td class="cellUnderline blueTxt">
                                <%= (Session["lang"].ToString() == "0") ? "User" : "المستخدم"%>   
                            </td>
                            <td class="cellUnderline">
                                <%# Eval("fullName")%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="cellUnderline blueTxt">
                                <%= (Session["lang"].ToString() == "0") ? "Receipt date\time" : "تاريخ و وقت الإستلام"%>   
                            </td>
                            <td class="cellUnderline">
                                <%# Convert.ToDateTime( Eval("receiveDate")).ToString("dd/MM/yyyy hh:mm tt")%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="cellUnderline blueTxt">
                                <%= (Session["lang"].ToString() == "0") ? "Action" : "الإجراء"%>   
                            </td>
                            <td class="cellUnderline">
                                <%# getWFAction(c.convertToInt32( Eval("actionType")))%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="cellUnderline blueTxt">
                                <%= (Session["lang"].ToString() == "0") ? "Action date\time" : "تاريخ و وقت الإجراء"%>   
                            </td>
                            <td class="cellUnderline">
                                <%# (c.convertToDateTime(Eval("actionDateTime")) > DateTime.Now) ? "-" : Convert.ToDateTime(Eval("actionDateTime")).ToString("dd/MM/yyyy hh:mm tt")%>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="blueTxt">
                                <%= (Session["lang"].ToString() == "0") ? "Notes" : "ملاحظات"%>   
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <%# Eval("userNotes")%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <h3>Forward</h3>
        <div>
            <table>
               <%-- <tr>
                    <td>
                        <%= (Session["lang"].ToString() == "0") ? "The number of days" : "عدد الايام"%>   
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtenddateCount" />
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <%= (Session["lang"].ToString() == "0") ? "Recipient type" : "نوع المستلم"%>   
                    </td>
                    <td>
                        <asp:DropDownList ID="drpRecipientType" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="drpRecipientType_SelectedIndexChanged">
                            <asp:ListItem Value="1">User</asp:ListItem>
                            <asp:ListItem Value="2">Group</asp:ListItem>
                            <asp:ListItem Value="3">Position</asp:ListItem>
                            <asp:ListItem Value="4">Unit</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%= (Session["lang"].ToString() == "0") ? "Recipient" : "لمستلم"%>   
                    </td>
                    <td style="margin-left: 40px">
                        <asp:DropDownList ID="drpRecipientID" runat="server">
                        </asp:DropDownList>
                        &nbsp;<asp:RadioButtonList ID="rdoSendType" runat="server"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Selected="True">cc</asp:ListItem>
                            <asp:ListItem>bcc</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <%= (Session["lang"].ToString() == "0") ? "Email Subject" : "عنوان الرسالة"%>   </td>
                    <td style="margin-left: 40px">
                        <asp:TextBox ID="txtMailBody" runat="server" Rows="10"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="margin-left: 40px">

                        <asp:LinkButton ID="btnSendEmail" CssClass="button" runat="server" OnClick="btnSendEmail_Click">
                <img border="0" src="../Images/Icons/Actions-go-next-view-icon.png" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Send" : "ارسال"%></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="margin-left: 40px">
                        <asp:Label ID="msglbl" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
