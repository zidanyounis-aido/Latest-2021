<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DocumentsMasterFullPage.master" AutoEventWireup="true" CodeBehind="advancedSearch.aspx.cs" Inherits="dms.Screen.advancedSearch"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .new-drop {
            padding: 4px;
            border-radius: 20px;
        }

        .new-main-input {
            width: 100% !important;
            border: 1px solid #cacaca;
            border-radius: 20px;
            padding: 6px;
            outline: none;
            /*height: 35px;*/
        }

        .custom-xs {
            margin-top: 20px;
        }
    </style>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="pageTitle">
    <ul class="pages_nav">
        <li><a href="#">المجلدات</a></li>
        <%--  <li><a href="#">مديرعام</a></li>--%>
        <li><a href="#">
            <%= (Session["lang"].ToString() == "0") ? "Advanced Search" : "بحث متقدم"%>
        </a></li>
    </ul>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <asp:Panel runat="server" ID="pnlDocDetails" Visible="false">
    </asp:Panel>
    <div class="row row-flex">
        <div class="col-xs-12">
            <div class="white-holder">
                <div class="max-width-holder">
                    <div class="col-xs-4">
                        <div class="main-field-holder">
                            <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Document ID:" : "رقم الملف"%></label>
                            <%--<input class="main-input" type="text">--%>
                            <asp:TextBox ID="txtDocID" type="number" CssClass="main-input" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="main-field-holder">
                            <label class="main-label">
                                <%= (Session["lang"].ToString() == "0") ? "Document Name:" : "اسم الملف"%>
                            </label>
                            <asp:TextBox ID="txtDocName" CssClass="main-input" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-xs-4">
                        <div class="main-field-holder">
                            <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Folder" : "مجلد"%></label>
                            <asp:DropDownList ID="drpFldrID" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="drpFldrID_SelectedIndexChanged" CssClass="new-drop">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnFolderSeq" runat="server" />
                            <asp:HiddenField ID="hdnDocTypeSeq" runat="server" />
                            <asp:HiddenField ID="hdnFolderDocTypeSeq" runat="server" />
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="main-field-holder">
                            <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Document Type:" : "نوع الملف"%> </label>
                            <asp:DropDownList ID="drpDocTypID" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="drpDocTypID_SelectedIndexChanged" CssClass="new-drop">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="main-field-holder">
                            <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Status:" : "الحالة"%> </label>
                            <asp:DropDownList ID="dropStatus" runat="server" CssClass="new-drop">
                                <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="in process" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Archived" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Late" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="collapse" id="collapseSearch" style="width: 100%;">
                        <asp:Panel ID="pnlDocMetas" runat="server"></asp:Panel>
                    </div>
                </div>
                <!-- search buttons -->
                <div class="control-side-holder control-side-holder-footer">
                    <div class="start-side">
                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn-main" OnClick="LinkButton3_Click">
                        <div class="btn-main-wrapper">
                            <svg xmlns="http://www.w3.org/2000/svg" width="17.293" height="17.293" viewBox="0 0 17.293 17.293">
                                    <g id="search" transform="translate(0.5 0.5)">
                                        <circle id="Oval" cx="7.149" cy="7.149" r="7.149" fill="none" stroke="#fff" stroke-linecap="round" stroke-linejoin="round" stroke-miterlimit="10" stroke-width="1"></circle>
                                        <path id="Path" d="M3.888,3.888,0,0" transform="translate(12.199 12.199)" fill="none" stroke="#fff" stroke-linecap="round" stroke-linejoin="round" stroke-miterlimit="10" stroke-width="1"></path>
                                    </g>
                                </svg>
                       <%= (Session["lang"].ToString() == "0") ? "Search" : "بحث"%>
                            </div>
                        </asp:LinkButton>
                        <a onclick="window.history.back();" class="btn-main btn-white" >
                            <div class="btn-main-wrapper">
                                <svg id="Group_2494" data-name="Group 2494" xmlns="http://www.w3.org/2000/svg"
                                    width="22.487" height="22.487" viewBox="0 0 22.487 22.487">
                                    <g id="Group_2175" data-name="Group 2175">
                                        <circle id="Ellipse_17" data-name="Ellipse 17" cx="11.244" cy="11.244"
                                            r="11.244" fill="#f4f4f4" />
                                        <g id="Group_2166" data-name="Group 2166"
                                            transform="translate(7.496 7.496)">
                                            <line id="Line_28" data-name="Line 28" y2="11.745"
                                                transform="translate(8.305) rotate(45)" fill="none" stroke="#8f9198"
                                                stroke-linecap="round" stroke-width="2" />
                                            <line id="Line_29" data-name="Line 29" x2="11.745"
                                                transform="translate(0) rotate(45)" fill="none" stroke="#8f9198"
                                                stroke-linecap="round" stroke-width="2" />
                                        </g>
                                    </g>
                                </svg>
                                <%= (Session["lang"].ToString() == "0") ? "Retreat" : "تراجع"%>
                            </div>
                        </a>
                        <div style="display: none">
                            <asp:LinkButton ID="btnExport" runat="server" CssClass="btn-main btn-white" OnClick="btnExport_Click" Visible="false">
                           <div class="btn-main-wrapper" onclick="window.open('/Screen/defaultAr.aspx?CODEN=1&dlgid=2&indexId=0','_self')">
                                <svg id="Group_2494" data-name="Group 2494" xmlns="http://www.w3.org/2000/svg" width="22.487" height="22.487" viewBox="0 0 22.487 22.487">
                                    <g id="Group_2175" data-name="Group 2175">
                                        <circle id="Ellipse_17" data-name="Ellipse 17" cx="11.244" cy="11.244" r="11.244" fill="#f4f4f4"></circle>
                                        <g id="Group_2166" data-name="Group 2166" transform="translate(7.496 7.496)">
                                            <line id="Line_28" data-name="Line 28" y2="11.745" transform="translate(8.305) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                            <line id="Line_29" data-name="Line 29" x2="11.745" transform="translate(0) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                        </g>
                                    </g>
                                </svg>
                                <%= (Session["lang"].ToString() == "0") ? "Retreat" : "تراجع"%>
                            </div>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="more-search-footer">
                <%--      <div class="adv-item">
                    <div class="close-adv-item">
                        <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 15 15">
                            <g id="Group_2736" data-name="Group 2736" transform="translate(0.099 0.237)">
                                <circle id="Ellipse_17" data-name="Ellipse 17" cx="7.5" cy="7.5" r="7.5" transform="translate(-0.099 -0.237)" fill="#0072ff"></circle>
                                <g id="Group_21" data-name="Group 21" transform="translate(7.446 2.221) rotate(45)">
                                    <line id="Line_28" data-name="Line 28" y2="7.248" transform="translate(3.624 0)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="1"></line>
                                    <line id="Line_29" data-name="Line 29" x2="7.248" transform="translate(0 3.624)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="1"></line>
                                </g>
                            </g>
                        </svg>
                    </div>
                    الراتب
                </div>

                <div class="adv-item">
                    <div class="close-adv-item">
                        <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 15 15">
                            <g id="Group_2736" data-name="Group 2736" transform="translate(0.099 0.237)">
                                <circle id="Ellipse_17" data-name="Ellipse 17" cx="7.5" cy="7.5" r="7.5" transform="translate(-0.099 -0.237)" fill="#0072ff"></circle>
                                <g id="Group_21" data-name="Group 21" transform="translate(7.446 2.221) rotate(45)">
                                    <line id="Line_28" data-name="Line 28" y2="7.248" transform="translate(3.624 0)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="1"></line>
                                    <line id="Line_29" data-name="Line 29" x2="7.248" transform="translate(0 3.624)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="1"></line>
                                </g>
                            </g>
                        </svg>
                    </div>
                    اسم الموظف
                </div>

                <div class="adv-item">
                    <div class="close-adv-item">
                        <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" viewBox="0 0 15 15">
                            <g id="Group_2736" data-name="Group 2736" transform="translate(0.099 0.237)">
                                <circle id="Ellipse_17" data-name="Ellipse 17" cx="7.5" cy="7.5" r="7.5" transform="translate(-0.099 -0.237)" fill="#0072ff"></circle>
                                <g id="Group_21" data-name="Group 21" transform="translate(7.446 2.221) rotate(45)">
                                    <line id="Line_28" data-name="Line 28" y2="7.248" transform="translate(3.624 0)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="1"></line>
                                    <line id="Line_29" data-name="Line 29" x2="7.248" transform="translate(0 3.624)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="1"></line>
                                </g>
                            </g>
                        </svg>
                    </div>
                    رقم الموظف
                </div>--%>

                <a class="btn-more-search" data-toggle="collapse" href="#collapseSearch">
                    <svg xmlns="http://www.w3.org/2000/svg" width="9.944" height="9.241" viewBox="0 0 9.944 9.241">
                        <g id="Group_2749" data-name="Group 2749" transform="translate(0.287 -9.095)">
                            <g id="Group_2750" data-name="Group 2750">
                                <g id="Group_2632" data-name="Group 2632" transform="translate(9.371 9.382) rotate(90)">
                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#fff" stroke="#fff" stroke-width="0.5"></path>
                                </g>
                                <g id="Group_2633" data-name="Group 2633" transform="translate(9.371 13) rotate(90)">
                                    <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#fff" stroke="#fff" stroke-width="0.5"></path>
                                </g>
                            </g>
                        </g>
                    </svg>
                    <span class="more-title">إعرض المزيد</span> <span class="less-title">عرض عناصر أقل </span>
                </a>
            </div>
        </div>
    </div>
    <br />
    <div>
        <asp:GridView ID="grdDocuments" CssClass="table my-table" ClientIDMode="Static" runat="server" AllowPaging="False"
            AutoGenerateColumns="False"
            GridLines="Vertical" OnPageIndexChanging="grdDocuments_PageIndexChanging"
            OnSelectedIndexChanged="grdDocuments_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="docID" HeaderText="ID" />
                <asp:BoundField DataField="docName" HeaderText="Document Name" />
                <asp:TemplateField HeaderText="Folder">
                    <ItemTemplate>
                        <asp:Label ID="lblFolderName" runat="server"
                            Text='<%# getFolderName(Eval("fldrID").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Type">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# getDocTypeDesc(c.convertToInt32(Eval("docTypID"))) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
                <asp:BoundField DataField="addedDate" DataFormatString="{0:dd/MM/yyyy}"
                    HeaderText="Added Date" />
                <asp:TemplateField HeaderText="Added By">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"
                            Text='<%# c.getUserName(c.convertToInt32(Eval("addedUserID"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="modifyDate" DataFormatString="{0:dd/MM/yyyy}"
                    HeaderText="Modify Date" />

                <asp:CommandField SelectText="<svg xmlns='http://www.w3.org/2000/svg' width='22.506' height='22.506' viewBox='0 0 22.506 22.506'> <path id='Path_6947' data-name='Path 6947' d='M11.254,0A11.253,11.253,0,1,0,22.507,11.253,11.253,11.253,0,0,0,11.254,0ZM16.6,8.1,15.534,9.167,13.359,6.991l-.825.825,2.176,2.176L9.372,15.328,7.2,13.153l-.825.825,2.176,2.175-.532.532-.01-.01a.421.421,0,0,1-.269.193l-2.029.452a.422.422,0,0,1-.5-.5l.452-2.028a.422.422,0,0,1,.193-.269l-.01-.01,8.588-8.588a.323.323,0,0,1,.456,0L16.6,7.642A.323.323,0,0,1,16.6,8.1Z' transform='translate(-0.001)' fill='#0072ff'></path> </svg>" ShowSelectButton="True" HeaderStyle-Width="25px"
                    ButtonType="Link" ItemStyle-HorizontalAlign="Center" />
                <asp:CommandField ShowDeleteButton="True" Visible="false" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle Font-Bold="True" ForeColor="White" />
            <HeaderStyle Font-Bold="True" ForeColor="White" />
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
    <script>
        $(function () {
            $('.dateFeild ').datepicker({
                format: 'dd/mm/yyyy',
                startDate: '-3d'
            });
        })
    </script>
</asp:Content>
