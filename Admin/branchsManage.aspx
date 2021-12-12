<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SettingsNestedMaster.master" AutoEventWireup="true" CodeBehind="branchsManage.aspx.cs" Inherits="dms.Admin.branchsManage" %>

<%@ Register Src="../Controls/folderTree.ascx" TagName="folderTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1 {
            width: 32px;
            height: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">

    <img align="absmiddle" alt="" class="style1"
        src="../Images/Icons/Company-icon.png" />
    <%= (Session["lang"].ToString() == "0") ? "Manage Branches" : "إدارة الفروع"%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%">
        <tr>
            <td>
                <% if (Session["lang"].ToString() == "0")
                    { %>
                <a href="javascript:changeToAddNew('Add New Branch')">
                    <%}
                    else
                    { %>
                    <a href="javascript:changeToAddNew('اضافة فرع جديد')">
                        <%} %>
                        <img src="../Images/Add-icon.png" border="0" align="middle" alt="Add New" />
                        <%= (Session["lang"].ToString() == "0") ? "Add New Branch" : "إضافة فرع جديد"%>
                    </a>
                    <br />
                    <br />
                    <b>
                        <%= (Session["lang"].ToString() == "0") ? "Added Branches" : "الفروع المضافة"%>
                    </b>
                    <br />
                    <br />
                    <asp:GridView ID="grdBranchs" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" GridLines="None"
                        OnSelectedIndexChanged="grdBranchs_SelectedIndexChanged"
                        ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="branchID" HeaderText="ID" />
                            <asp:BoundField DataField="branchName" HeaderText="Branch Name" />
                            <asp:CommandField SelectText="Edit" ShowSelectButton="True" ButtonType="Image"
                                SelectImageUrl="~/Images/icons/file-edit-icon.png" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5C5C5C" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView></td>
            <td>
                <table style="width: 100%;" border="0" id="tblEditForm">
                    <tr>
                        <td colspan="4"
                            style="font-size: 18px; color: #003366; background-color: #f6b727; height: 30px; padding-left: 10px;">
                            <asp:Label ID="lblFormMode" runat="server" ClientIDMode="Static" Text="Add New Branch"
                                CssClass="formModeTitleCSS"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblBranchID" runat="server" Text="Branch ID"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtBranchID" runat="server" Width="50px" ReadOnly="True"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="lblCompanyID" runat="server" Text="Company"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="drpCompanyID" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblBranchName" runat="server" Text="Branch Name (English)"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtBranchName" runat="server"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtBranchName" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblBranchNameAr" runat="server" Text="Branch Name (Arabic)"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBranchNameAr" runat="server"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtBranchNameAr" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTel1" runat="server" Text="Phone Number 1"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtTel1" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="lblTel2" runat="server" Text="Phone Number 2"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtTel2" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblZipcode" runat="server" Text="Zipcode"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtZipcode" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="lblMainEmail" runat="server" Text="Main Email"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtMainEmail" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ErrorMessage="RegularExpressionValidator" ControlToValidate="txtMainEmail"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="200px"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="lblIsMainBranch" runat="server" Text="Is main branch"></asp:Label></td>
                        <td>
                            <asp:CheckBox ID="chkIsMainBranch" runat="server" />
                        </td>

                    </tr>
                    <tr style="display: none">
                        <td>
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <div style="display: none">
                                <asp:RadioButtonList ID="rdoSaveMethod" ClientIDMode="Static" runat="server"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">
               New Branch</asp:ListItem>
                                    <asp:ListItem Value="1">Exsit Branch</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:LinkButton CssClass="btnSave" ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">
                <img src="../Images/Icons/action-save-icon.png" border="0" align="absmiddle" />
                <%= (Session["lang"].ToString() == "0") ? "Save" : "حفظ"%>
                            </asp:LinkButton>
                            &nbsp;<asp:Label ID="lblRes" runat="server" ForeColor="#CC0000"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <table id="tblDetailsForm" style="display: none" runat="server" clientidmode="Static">
                    <tr>
                        <td colspan="3">
                            <h3>
                                <%= (Session["lang"].ToString() == "0") ? "Allowed Folders" : "المجلدات المسموحة"%>
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%= (Session["lang"].ToString() == "0") ? "Folder" : "مجلد"%>
                
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFolders" runat="server" Visible="false">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnFldrID" ClientIDMode="Static" runat="server" />
                            <asp:TextBox ID="txtFldrID" ClientIDMode="Static" onClick="showFolderDialog()" ReadOnly="true" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            <asp:LinkButton CssClass="btnSave" ID="btnAddFolder" runat="server" OnClick="btnAddFolder_Click"> <img src="../Images/Add-icon.png" border="0" align="middle" alt="Add New" />
                <%= (Session["lang"].ToString() == "0") ? "Add" : "إضافة"%>
                            </asp:LinkButton>


                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">

                            <asp:GridView ID="grdBranchFolders" runat="server" CellPadding="4" ForeColor="#333333"
                                GridLines="None" AutoGenerateColumns="False"
                                OnRowDeleting="grdBranchFolders_RowDeleting">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="fldrID" HeaderText="ID" />
                                    <asp:BoundField DataField="fldrName" HeaderText="Folder Name" />
                                    <asp:CommandField DeleteText="Remove"
                                        ButtonType="Image" DeleteImageUrl="../Images/Icons/Actions-stop-icon.png"
                                        ShowDeleteButton="True" />
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5C5C5C" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>

                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="OutOfDesign">
    <div id="uiDialog" style="background: #ffffff" title="Select a Folder" class="ui-widget-content">
        <uc1:folderTree ID="folderTree1" runat="server" />
    </div>
</asp:Content>
