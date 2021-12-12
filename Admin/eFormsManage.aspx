<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="eFormsManage.aspx.cs" Inherits="dms.Admin.eFormsManage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #Select1 {
            height: 109px;
        }

        .popBack {
            background-color: Gray;
            opacity: 0.4;
            filter: alpha(opacity=40); /* For IE8 and earlier */
        }

        .style1 {
            width: 32px;
            height: 32px;
        }
    </style>

    <script type="text/javascript">
        function addTextToExpEditor(lst) {
            var dropdown = document.getElementById(lst);
            var myindex = dropdown.selectedIndex;
            var SelValue = dropdown.options[myindex].value;
            document.getElementById("txtExp").value = document.getElementById("txtExp").value + " " + SelValue;
        }

        function showOptions(lst) {
            var dropdown = document.getElementById(lst);
            var myindex = dropdown.selectedIndex;
            var SelValue = dropdown.options[myindex].value;

            document.getElementById("lst2").style.display = "none";
            document.getElementById("lst3").style.display = "none";
            document.getElementById("lst4").style.display = "none";
            document.getElementById("ContentPlaceHolderBody_lst1").style.display = "none";
            document.getElementById(SelValue).style.display = "block";
        }

        function clearExpText() {
            document.getElementById("txtExp").value = "#expr:this = ";
        }

        function getExpText(target) {
            document.getElementById(target).value = document.getElementById("txtExp").value;
            hideScanned('pnlExpr');
        }

        function hideScanned(obj) {
            document.getElementById(obj).style.display = "none";
        }

        function showScanned(obj) {
            var _t = 0;
            var _l = 0;
            //alert(document.body.clientHeight);
            //alert(document.getElementById(obj).clientHeight);
            //_t = (parseFloat(window.innerHeight) / 2) - (parseFloat(document.getElementById(obj).style.height) / 2);
            _t = 100;
            //alert(document.body.clientWidth);
            _l = (parseFloat(document.body.clientWidth) / 2) - (parseFloat(document.getElementById(obj).style.width) / 2);
            document.getElementById(obj).style.position = "absolute";
            //alert(_t);
            document.getElementById(obj).style.top = String(_t) + "px";
            document.getElementById(obj).style.left = String(_l) + "px";
            document.getElementById(obj).style.display = "block";
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">
    <img align="absmiddle" alt="" class="style1"
        src="../Images/Icons/File-icon.png" />
    <%= (Session["lang"].ToString() == "0") ? "Manage eForms" : "ادارة الاشكال الالكترونية"%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightBar" runat="server">
    <% if (Session["lang"].ToString() == "0")
        { %>
    <a href="javascript:changeToAddNew('Add New eForm')">
        <%}
            else
            { %>
        <a href="javascript:changeToAddNew('اضافة نموذج جديد')">
            <%} %>
            <img src="../Images/Add-icon.png" border="0" align="middle" alt="Add New" />
            <%= (Session["lang"].ToString() == "0") ? "Add New eForm" : "إضافة نموذج جديد"%>
        </a>
        <br />
        <br />
        <b><%= (Session["lang"].ToString() == "0") ? "Added eForms:" : " النماذج الالكترونية المضافة"%></b><br />
        <div style="vertical-align: middle">
            <span style="margin-bottom: 15px;"><%= (Session["lang"].ToString() == "0") ? "Category :" : "فئة :"%></span>&nbsp;
    <asp:DropDownList ID="drpCats" runat="server" AutoPostBack="True"
        OnSelectedIndexChanged="drpCats_SelectedIndexChanged">
    </asp:DropDownList>
        </div>
        <br />
        <asp:GridView ID="grdEForms" runat="server" AutoGenerateColumns="False"
            CellPadding="4" GridLines="None"
            OnSelectedIndexChanged="grdTypes_SelectedIndexChanged" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="formID" HeaderText="ID" />
                <asp:BoundField DataField="formName" HeaderText="Name" />
                <asp:CommandField SelectText="Edit" ShowSelectButton="True" ButtonType="Image"
                    SelectImageUrl="../Images/icons/file-edit-icon.png" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5C5C5C" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />

            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
        </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <table id="tblEditForm">
        <tr>
            <td colspan="4"
                style="font-size: 18px; color: #003366; background-color: #f6b727; height: 30px; padding-left: 10px;">
                <asp:Label ID="lblFormMode" runat="server" ClientIDMode="Static" Text="Add New Section"
                    CssClass="formModeTitleCSS"></asp:Label>
            </td>

        </tr>
        <tr style="display: none">
            <td>
                <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFormID" runat="server" Width="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Form Description (English)"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFormName" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtFormName"
                    ErrorMessage="Form Name is Required">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Form Description (Arabic)"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFormNameAr" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="txtFormNameAr"
                    ErrorMessage="Form Name is Required">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <%= (Session["lang"].ToString() == "0") ? "Category" : "فئة :"%></td>
            <td colspan="3">
                <asp:ComboBox ID="cmbCatID" runat="server">
                </asp:ComboBox>
            </td>
        </tr>
        <tr>
            <td style="height: 5px"></td>
            <td colspan="3"></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Default Workflow"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="drpDefaultPathID" runat="server">
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:CheckBox ID="chkActive" runat="server" Text="Active" Checked="True"
                    ClientIDMode="Static" />
            </td>
            <td colspan="2">
                <div style="display: none">
                    <asp:RadioButtonList ID="rdoSaveMethod" runat="server"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">New Type</asp:ListItem>
                        <asp:ListItem Value="1">Exsit Type</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:LinkButton CssClass="btnSave" ID="btnSave" runat="server" OnClick="btnSave_Click"> <img src="../Images/Icons/action-save-icon.png" border="0" align="absmiddle" /><%= (Session["lang"].ToString() == "0") ? "Save" : "حفظ"%></asp:LinkButton>
            </td>
            <td colspan="2">
                <asp:Label ID="lblRes" runat="server" ForeColor="#CC0000"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div id="tblDetailsForm" style="display: none" runat="server" clientidmode="Static">
        <asp:Panel ID="Panel1" runat="server">
            <%= (Session["lang"].ToString() == "0") ? "Fields :" : "حقول :"%>

            <table width="100%" cellspacing="0" cellpadding="3"
                style="border: 1px solid #CCCCCC">
                <tr>
                    <td class="gridHead" width="5%"><%= (Session["lang"].ToString() == "0") ? "ID" : "الرقم"%> </td>
                    <td class="gridHead" width="15%"><%= (Session["lang"].ToString() == "0") ? "Description" : "الوصف"%></td>
                    <td class="gridHead" width="10%"><%= (Session["lang"].ToString() == "0") ? "Type" : "النوع"%></td>
                    <td class="gridHead" width="5%"><%= (Session["lang"].ToString() == "0") ? "Required" : "مطلوب"%></td>
                    <td class="gridHead" width="5%"><%= (Session["lang"].ToString() == "0") ? "Order" : "الترتيب"%></td>
                    <td class="gridHead" width="10%"><%= (Session["lang"].ToString() == "0") ? "Control" : "أداة التحكم"%></td>
                    <td class="gridHead" width="15%"><%= (Session["lang"].ToString() == "0") ? "Default Text(s)" : "النص الافتراضي"%></td>
                    <td class="gridHead" width="15%">&nbsp;</td>
                    <td class="gridHead" width="15%"><%= (Session["lang"].ToString() == "0") ? "Default Value(s)" : "القيم الافتراضية"%></td>
                    <td class="gridHead" width="5%"><%= (Session["lang"].ToString() == "0") ? "Visible" : "مرئي"%></td>
                    <td class="gridHead" width="15%"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAuto" runat="server" Text="Auto"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtMetaDesc" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="drpMetaDataType" runat="server">
                            <asp:ListItem>String</asp:ListItem>
                            <asp:ListItem Value="Int32">Integer</asp:ListItem>
                            <asp:ListItem>Decimal</asp:ListItem>
                            <asp:ListItem Value="DateTime">Date Time</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkrRequired" runat="server" /></td>
                    <td>
                        <asp:TextBox ID="txtOrderSeq" runat="server" Width="50px"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="drpCtrlID" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDefaultTexts" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <img id="ImageButton1" src="../Images/function-icon.png"
                            onclick="showScanned('pnlExpr')" style="cursor: pointer" />

                    </td>
                    <td>
                        <asp:TextBox ID="txtDefaultValues" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:CheckBox ID="chkVisible" runat="server" /></td>
                    <td>
                        <asp:LinkButton ID="lnkAddMeta" runat="server" OnClick="lnkAddMeta_Click">
                    <%= (Session["lang"].ToString() == "0") ? "Add" : "إضافة"%>
                        </asp:LinkButton></td>
                </tr>
            </table>

            <asp:GridView ID="grdMetas" runat="server"
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="4"
                AutoGenerateColumns="False" ShowHeader="False" Width="100%"
                OnRowCancelingEdit="grdMetas_RowCancelingEdit"
                OnRowDataBound="grdMetas_RowDataBound" OnRowDeleting="grdMetas_RowDeleting"
                OnRowEditing="grdMetas_RowEditing" OnRowUpdating="grdMetas_RowUpdating"
                ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="fieldSeq" HeaderText="Seq." ReadOnly="True">
                        <ItemStyle Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="metaDesc" HeaderText="metaDesc">
                        <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="metaDataType">
                        <EditItemTemplate>
                            <asp:DropDownList ID="drpMetaDataType" runat="server"
                                SelectedValue='<%# Eval("metaDataType") %>'>
                                <asp:ListItem>String</asp:ListItem>
                                <asp:ListItem Value="Int32">Integer</asp:ListItem>
                                <asp:ListItem>Decimal</asp:ListItem>
                                <asp:ListItem Value="DateTime">Date Time</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("metaDataType") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="required" HeaderText="required">
                        <ItemStyle Width="5%" />
                    </asp:CheckBoxField>
                    <asp:BoundField DataField="orderSeq" HeaderText="orderSeq">
                        <ItemStyle Width="5%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="ctrlID">
                        <EditItemTemplate>
                            <asp:DropDownList ID="drpCtrlID" runat="server">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnCtrlID" runat="server" Value='<%# Eval("ctrlID") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label10" runat="server"
                                Text='<%# getControlType(Eval("ctrlID").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="defaultTexts" HeaderText="defaultTexts">
                        <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="defaultValues" HeaderText="defaultValues">
                        <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:CheckBoxField DataField="visible" HeaderText="visible">
                        <ItemStyle Width="5%" />
                    </asp:CheckBoxField>
                    <asp:CommandField ShowEditButton="True" ButtonType="Image"
                        EditImageUrl="../Images/icons/file-edit-icon.png">
                        <ItemStyle Width="10%" />
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="../Images/Icons/Actions-stop-icon.png">
                        <ItemStyle Width="5%" />
                    </asp:CommandField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5C5C5C" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </asp:Panel>
    </div>
    <br />
    <div id="pnlExpr" style="background-color: White; width: 550px; display: none; border: 2px solid gray; padding: 15px">
        <h2><%= (Session["lang"].ToString() == "0") ? "Make your expression" : "انشئ الصيغة"%></h2>
        <br />
        <textarea id="txtExp" rows="3" cols="50" style="width: 500px">#expr:this = </textarea>
        <br />
        <br />
        <table width="500" style="border: 2px solid #666666">
            <tr>
                <td valign="top" align="center"
                    style="background-color: #808080; color: #FFFFFF; font-weight: bold"
                    width="50%">
                    <%= (Session["lang"].ToString() == "0") ? "List" : "قائمة"%>  </td>
                <td valign="top" align="center"
                    style="background-color: #808080; color: #FFFFFF; font-weight: bold"
                    width="50%">
                    <%= (Session["lang"].ToString() == "0") ? "Values" : "قيم"%> </td>
            </tr>
            <tr>
                <td valign="top">
                    <select id="lst0" onchange="showOptions('lst0')" name="D1" size="5" style="width: 97%">
                        <option value="ContentPlaceHolderBody_lst1">Metas Values
                        </option>
                        <option value="lst2">Built-in Values</option>
                        <option value="lst3">String Functions</option>
                        <option value="lst4">Intger Functions</option>
                    </select>
                </td>
                <td valign="top">
                    <select id="lst2" onchange="addTextToExpEditor('lst2')" name="D1" size="5" style="display: none; width: 97%">
                        <option value="FolderSeq">Seq. By Folder</option>
                        <option value="DocTypeSeq">Seq. By Document Type
                        </option>
                        <option value="FolderDocTypeSeq">Seq. By Folder and Document 
                        Type</option>
                    </select>
                    <select id="lst3" name="D1" onchange="addTextToExpEditor('lst3')" size="5" style="display: none; width: 97%">
                        <option value="String(value)">Convert To String</option>
                        <option value=" + ">Append (+)</option>
                        <option value="String(value).substr(0,1)">Substring(StartIndex,Count)</option>
                        <option value="String(value).length">Length</option>
                    </select>
                    <select id="lst4" name="D1" onchange="addTextToExpEditor('lst4')" size="5" style="display: none; width: 97%">
                        <option value="parseInt(value)">Convert To Integer</option>
                        <option value="+">+</option>
                        <option value="-">-</option>
                        <option value="*">*</option>
                        <option value="/">/</option>
                    </select>
                    <asp:ListBox onchange="addTextToExpEditor('ContentPlaceHolderBody_lst1')" ID="lst1" runat="server" Rows="5" Width="97%"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td>
                    <a id="A1" style="cursor: pointer" onclick="getExpText('ContentPlaceHolderBody_txtDefaultTexts')"><%= (Session["lang"].ToString() == "0") ? "Add" : "اضافة"%> </a>


                </td>
                <td><a id="lnkCancel" style="cursor: pointer" onclick="hideScanned('pnlExpr')"><%= (Session["lang"].ToString() == "0") ? "Cancel" : "الغاء"%> </a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
