<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="EventsDocument.aspx.cs" Inherits="dms.Screen.EventsDocument" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('.loading').hide();
        })
        function onButtonClick() {
            $('.loading').hide();
        }
        function fnExcelReport() {
            var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
            var textRange; var j = 0;
            tab = document.getElementById('gvEvents'); // id of table
            for (j = 0; j < tab.rows.length; j++) {
                tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
                //tab_text=tab_text+"</tr>";
            }
            tab_text = tab_text + "</table>";
            tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
            tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
            tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
            {
                txtArea1.document.open("txt/html", "replace");
                txtArea1.document.write(tab_text);
                txtArea1.document.close();
                txtArea1.focus();
                sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
            }
            else                 //other browser not tested on IE 11
                sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

            return (sa);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="white-holder tab-pane active" runat="server" id="divList" style="height: 1000px;">
        <div class="control-side-holder">
            <div class="start-side">
                <a class="btn-main" runat="server" causesvalidation="false" onserverclick="btnAddNewEvent_Click">
                    <div class="btn-main-wrapper">
                        <svg xmlns="http://www.w3.org/2000/svg" width="30.271" height="30.271" viewBox="0 0 30.271 30.271">
                            <path id="Path_6946" data-name="Path 6946" d="M18.271,3.135A10.7,10.7,0,0,0,3.135,18.27,10.7,10.7,0,0,0,18.271,3.135ZM15.332,13.853a1.045,1.045,0,1,1-1.478,1.478L10.7,12.181,7.552,15.332a1.045,1.045,0,0,1-1.478-1.478L9.225,10.7,6.073,7.552A1.045,1.045,0,0,1,7.552,6.074L10.7,9.224l3.151-3.151a1.045,1.045,0,0,1,1.478,1.478L12.181,10.7Z" transform="translate(0 15.136) rotate(-45)" fill="#fff"></path>
                        </svg>
                        <%= (Session["lang"].ToString() == "0") ? "Add New" : "إضافة جديد"%>
                    </div>
                </a>

            </div>

            <div class="end-side">

                <a class="btn-main" runat="server" onserverclick="btnExportEXCEL_ServerClick" onclick="$('.loading').hide();" causesvalidation="false">
                    <div class="btn-main-wrapper">
                        <svg xmlns="http://www.w3.org/2000/svg" width="12.728" height="16.477" viewBox="0 0 12.728 16.477">
                            <g id="surface1" transform="translate(-58.885 0.998)">
                                <path id="Path_7050" data-name="Path 7050" d="M338.5,19.06h2.14l-3.269-3.43v2.306A1.123,1.123,0,0,0,338.5,19.06Z" transform="translate(-269.502 -16.092)" fill="#fff"></path>
                                <path id="Path_7051" data-name="Path 7051" d="M67.167,1.844V-1h-6.7A1.594,1.594,0,0,0,58.885.584V13.9a1.594,1.594,0,0,0,1.587,1.582h9.555A1.594,1.594,0,0,0,71.613,13.9V3.666h-2.62A1.822,1.822,0,0,1,67.167,1.844Zm-4.708,5.68a.35.35,0,0,1,.493.017L64.9,9.634V4.656a.349.349,0,1,1,.7,0V9.638l1.949-2.092a.349.349,0,1,1,.51.475L65.5,10.763a.349.349,0,0,1-.257.109.356.356,0,0,1-.257-.109L62.433,8.021A.365.365,0,0,1,62.459,7.524Zm6.726,5.3a.35.35,0,0,1-.349.349H61.662a.349.349,0,0,1,0-.7h7.179A.346.346,0,0,1,69.185,12.82Z" fill="#fff"></path>
                            </g>
                        </svg>
                        <%= (Session["lang"].ToString() == "0") ? "Export" : "تصدير"%>
                    </div>
                </a>

            </div>
        </div>
        <div runat="server">
            <asp:GridView ID="gvEvents" CssClass="table my-table" ClientIDMode="Static" runat="server" AllowPaging="False"
                AutoGenerateColumns="False" BorderStyle="None"
                OnRowCommand="gvEventsLists_OnRowCommand"
                GridLines="None" OnPageIndexChanging="gvTaskLists_PageIndexChanging" BorderWidth="0">
                <Columns>
                    <asp:TemplateField HeaderText="عنوان الحدث">
                        <HeaderTemplate>
                            <asp:LinkButton runat="server">تاريخ المهمة
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                            </asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--<asp:Button
                                Style="border: 0; background-color: transparent; text-decoration: underline;"
                                ID="btnEdit" runat="server" CommandName="editCommand"
                                CommandArgument='<%# Eval("event_id") %>'
                                Text='' />--%>
                            <%# Eval("title") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="بداية الحدث">
                        <HeaderTemplate>
                            <asp:LinkButton runat="server">تاريخ المهمة
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                            </asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--<asp:Button
                                Style="border: 0; background-color: transparent; text-decoration: underline;"
                                ID="btnEdit" runat="server" CommandName="editCommand"
                                CommandArgument='<%# Eval("event_id") %>'
                                Text='' />--%>
                            <%# Eval("event_start") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="نهاية الحدث">
                        <HeaderTemplate>
                            <asp:LinkButton runat="server">تاريخ المهمة
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                            </asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <%# Eval("event_end") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="انشئت من">
                        <HeaderTemplate>
                            <asp:LinkButton runat="server">تاريخ المهمة
                                <span class="btn-sort-table">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="9.945" height="15.004" viewBox="0 0 9.945 15.004">
                                        <g id="Group_2776" data-name="Group 2776" transform="translate(-1396.213 -413.622)">
                                            <g id="Group_2634" data-name="Group 2634" transform="translate(1396.5 413.909)" opacity="0.6">
                                                <g id="Group_2633" data-name="Group 2633" transform="translate(0 5.048) rotate(-90)">
                                                    <path id="Path_7047" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                                </g>
                                            </g>
                                            <g id="Group_2632" data-name="Group 2632" transform="translate(1405.871 423.291) rotate(90)" opacity="0.597">
                                                <path id="Path_7047-2" data-name="Path 7047" d="M4.943,4.428.615.1a.361.361,0,0,0-.51.51L4.178,4.683.106,8.755a.361.361,0,0,0,.51.51L4.943,4.938A.361.361,0,0,0,4.943,4.428Z" transform="translate(0 0)" fill="#8f9198" stroke="#8f9198" stroke-width="0.5"></path>
                                            </g>
                                        </g>
                                    </svg>
                                </span>
                            </asp:LinkButton>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <%# Eval("CreatedBy") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:BoundField DataField="" HeaderText="" />--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="tr-btn-holder">
                                <asp:LinkButton ID="lnkedit" Style="margin: auto;" runat="server" CommandName="editCommand"
                                    CssClass="tr-edit" CommandArgument='<%# Eval("event_id") %>'>
                                <svg xmlns="http://www.w3.org/2000/svg" width="22.506" height="22.506" viewBox="0 0 22.506 22.506">   <path id="Path_6947" data-name="Path 6947" d="M11.254,0A11.253,11.253,0,1,0,22.507,11.253,11.253,11.253,0,0,0,11.254,0ZM16.6,8.1,15.534,9.167,13.359,6.991l-.825.825,2.176,2.176L9.372,15.328,7.2,13.153l-.825.825,2.176,2.175-.532.532-.01-.01a.421.421,0,0,1-.269.193l-2.029.452a.422.422,0,0,1-.5-.5l.452-2.028a.422.422,0,0,1,.193-.269l-.01-.01,8.588-8.588a.323.323,0,0,1,.456,0L16.6,7.642A.323.323,0,0,1,16.6,8.1Z" transform="translate(-0.001)" fill="#0072ff"></path></svg>
                                </asp:LinkButton>
                                <asp:LinkButton ID="Button3" Style="margin: auto;" runat="server" CommandName="deletethisrow"
                                    CssClass="tr-remove" CommandArgument='<%# Eval("event_id") %>'
                                    OnClientClick='<%# string.Format("return confirm(`{0} {1} ؟`)","هل انت متاكد من حذف",Eval("title"))  %>'>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963" viewBox="0 0 11.963 11.963">
                                    <g id="Group_21" data-name="Group 21" transform="translate(5.981 -3.153) rotate(45)">
                                        <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                        <line id="Line_29" data-name="Line 29" x2="12.918" transform="translate(0 6.459)" fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2"></line>
                                    </g>
                                </svg>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="row" style="text-align: center;">
                <asp:Label ID="lblNoResult" runat="server" Visible="false"></asp:Label>
                <%--                <asp:Label ID="lblNoResult" runat="server" Text="Label" Visible="false"></asp:Label>--%>
            </div>
        </div>
    </div>
    <div id="divDetails" runat="server" visible="false" class="white-holder">
        <div class="max-width-holder">
            <div class="row">
                <div class="col-xs-12">
                    <div class="main-field-holder">
                        <div class="main-title">
                            <asp:Label ID="lblFormMode" runat="server" ClientIDMode="Static" Text="Add New Company"
                                CssClass="formModeTitleCSS"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-xs-6">
                    <div class="main-field-holder">
                        <asp:HiddenField ID="hdnSelectedId" runat="server" />
                        <label class="main-label">عنوان الحدث</label>
                        <asp:TextBox ValidationGroup="btnEventsAdd" ID="txtEventTitleEvents" runat="server" CssClass="main-input"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ControlToValidate="txtEventTitleEvents"
                            ErrorMessage="*" ValidationGroup="btnEventsAdd"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="main-field-holder">
                        <label class="main-label">تاريخ بداية الحدث</label>
                        <div class="main-field-holder main-field-holder-two">
                            <%--<input class="main-input" type="date">--%>
                            <asp:TextBox ValidationGroup="btnEventsAdd" ID="txtDateEvents" runat="server" CssClass="main-input"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                TargetControlID="txtDateEvents" Format="dd/MM/yyyy" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtDateEvents"
                                ErrorMessage="*" ValidationGroup="btnEventsAdd"
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                            <input class="main-input" type="time" runat="server" id="txtTime" required="required">
                        </div>
                    </div>
                    <div class="main-field-holder">
                        <label class="main-label">تاريخ نهاية الحدث</label>
                        <div class="main-field-holder main-field-holder-two">
                            <asp:TextBox ValidationGroup="btnEventsAdd" ID="txtDateToEvents" runat="server" CssClass="main-input"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                TargetControlID="txtDateToEvents" Format="dd/MM/yyyy" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtDateToEvents"
                                ErrorMessage="*" ValidationGroup="btnEventsAdd"
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                            <input class="main-input" type="time" runat="server" id="txtTimeToEvents" required="required">
                        </div>
                    </div>
                </div>

                <div class="col-xs-6">
                    <div class="main-field-holder">
                        <label class="main-label">التفاصيل</label>
                        <asp:TextBox ID="txtEventsDescription" class="main-textarea textarea-lg-three" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>
                </div>

            </div>
        </div>
        <div class="control-side-holder control-side-holder-footer">
            <div class="start-side">
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="btnSave_Click" CssClass="btn-main">
                    <div class="btn-main-wrapper">
                        <svg id="Group_2656" data-name="Group 2656" xmlns="http://www.w3.org/2000/svg" width="14.6" height="14.599" viewBox="0 0 14.6 14.599">
                            <path id="Path_7052" data-name="Path 7052" d="M-437.574,30.081h3.963V26H-438v3.65A.428.428,0,0,0-437.574,30.081Z" transform="translate(441.623 -26.003)" fill="#fff"></path>
                            <path id="Path_7053" data-name="Path 7053" d="M-253.035,29.653V26H-254v4.078h.542A.428.428,0,0,0-253.035,29.653Z" transform="translate(262.872 -26.003)" fill="#fff"></path>
                            <path id="Path_7054" data-name="Path 7054" d="M-438,381.589h6.216V377H-438Z" transform="translate(441.623 -366.989)" fill="#fff"></path>
                            <path id="Path_7055" data-name="Path 7055" d="M-550.4,29.611a.429.429,0,0,0-.095-.229c-.021-.026.2.194-3.251-3.254a.431.431,0,0,0-.3-.125h-.257v3.65a1.285,1.285,0,0,1-1.283,1.283h-5.361a1.285,1.285,0,0,1-1.283-1.283V26h-1.483A1.285,1.285,0,0,0-565,27.286V39.319a1.285,1.285,0,0,0,1.283,1.283h1.483V34.614a1.285,1.285,0,0,1,1.283-1.283h5.361a1.285,1.285,0,0,1,1.283,1.283V40.6h2.623a1.285,1.285,0,0,0,1.283-1.283c0-10.385,0-9.675,0-9.708Z" transform="translate(565.001 -26.003)" fill="#fff"></path>
                            <path id="Path_7056" data-name="Path 7056" d="M-432.214,313h-5.361a.428.428,0,0,0-.428.428v.542h6.216v-.542A.428.428,0,0,0-432.214,313Z" transform="translate(441.623 -304.815)" fill="#fff"></path>
                        </svg>
                        <span id="lblSave1" runat="server"><%= (Session["lang"].ToString() == "0") ? "Save" : "حفظ"%></span>
                    </div>
                </asp:LinkButton>
                &nbsp;<asp:Label ID="lblRes" runat="server" ForeColor="#CC0000"></asp:Label>
                <a class="btn-main btn-white" causesvalidation="false" runat="server" id="btnUndo" onserverclick="btnUndo_ServerClick">
                    <div class="btn-main-wrapper">
                        <svg id="Group_2494" data-name="Group 2494" xmlns="http://www.w3.org/2000/svg" width="22.487" height="22.487" viewBox="0 0 22.487 22.487">
                            <g id="Group_2175" data-name="Group 2175">
                                <circle id="Ellipse_17" data-name="Ellipse 17" cx="11.244" cy="11.244" r="11.244" fill="#f4f4f4"></circle>
                                <g id="Group_2166" data-name="Group 2166" transform="translate(7.496 7.496)">
                                    <line id="Line_28" data-name="Line 28" y2="11.745" transform="translate(8.305) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                    <line id="Line_29" data-name="Line 29" x2="11.745" transform="translate(0) rotate(45)" fill="none" stroke="#8f9198" stroke-linecap="round" stroke-width="2"></line>
                                </g>
                            </g>
                        </svg>
                        <%= (Session["lang"].ToString() == "0") ? "Cancel" : "تراجع"%>
                    </div>
                </a>
            </div>
            <div class="end-side">
                <a class="btn-main" onclick="$('#ContentPlaceHolder1_divDetails').find('input').val('');$('#ContentPlaceHolder1_divDetails').find('textarea').val('');">
                    <div class="btn-main-wrapper">
                        <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg"
                            width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                            <path id="Path_7057" data-name="Path 7057"
                                d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z"
                                transform="translate(-63.122 -124.487)" fill="#fff">
                            </path>
                            <path id="Path_7058" data-name="Path 7058"
                                d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z" transform="translate(-32)"
                                fill="#fff">
                            </path>
                        </svg>
                        مسح
                    </div>
                </a>
            </div>
        </div>
    </div>
    <!-- Modal search task -->
    <div class="modal fade my-modal my-modal-lg" id="search-tasks" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">البحث</h4>
                </div>
                <div class="modal-body modal-body-padding">
                    <div class="row">
                        <div class="col-xs-4">
                            <div class="main-field-holder">
                                <label class="main-label">التاريخ</label>
                                <input class="main-input" type="date">
                            </div>
                        </div>
                        <div class="col-xs-4">
                            <div class="main-field-holder">
                                <label class="main-label">موجهة الي </label>
                                <div class="dropdown-main dropdown">
                                    <div id="drop1" class="btn-dropdown-holder" type="button" data-toggle="dropdown"
                                        aria-haspopup="true" aria-expanded="false">
                                        <span class="dropdown-title">محمد على</span>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="12.204" height="7.118"
                                            viewBox="0 0 12.204 7.118">
                                            <g id="Group_3106" data-name="Group 3106"
                                                transform="translate(11.704 0.556) rotate(90)">
                                                <g id="Group_2125" data-name="Group 2125">
                                                    <path id="Path_6981" data-name="Path 6981"
                                                        d="M5.88,5.268.732.12A.429.429,0,0,0,.126.727L4.97,5.571.126,10.416a.429.429,0,1,0,.607.607L5.88,5.875A.429.429,0,0,0,5.88,5.268Z"
                                                        fill="#8f9198" stroke="#8f9198" stroke-width="1">
                                                    </path>
                                                </g>
                                            </g>
                                        </svg>

                                    </div>
                                    <ul class="dropdown-menu" aria-labelledby="drop1">
                                        <li>محمد على</li>
                                        <li>محمد على</li>
                                        <li>محمد على</li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class="col-xs-4">
                            <div class="main-field-holder">
                                <label class="main-label">الحالة</label>
                                <div class="radio-input-holder">
                                    <input type="radio" id="all-tasks" name="search-status" value="test">
                                    <label for="all-tasks">الكل</label>
                                </div>
                                <div class="radio-input-holder">
                                    <input type="radio" id="list-tasks" name="search-status" value="test">
                                    <label for="list-tasks">القائمة</label>
                                </div>
                                <div class="radio-input-holder">
                                    <input type="radio" id="end-tasks" name="search-status" value="test">
                                    <label for="end-tasks">المنتهي</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn-done-model">نعم</button>
                    <button type="button" class="btn-close-model" data-dismiss="modal">
                        <span class="icon-close">
                            <svg xmlns="http://www.w3.org/2000/svg" width="11.963" height="11.963"
                                viewBox="0 0 11.963 11.963">
                                <g id="Group_21" data-name="Group 21"
                                    transform="translate(5.981 -3.153) rotate(45)">
                                    <line id="Line_28" data-name="Line 28" y2="12.918" transform="translate(6.459)"
                                        fill="none" stroke="#fff" stroke-linecap="round" stroke-width="2">
                                    </line>
                                    <line id="Line_29" data-name="Line 29" x2="12.918"
                                        transform="translate(0 6.459)" fill="none" stroke="#fff"
                                        stroke-linecap="round" stroke-width="2">
                                    </line>
                                </g>
                            </svg>
                        </span>
                        تراجع
                    </button>
                </div>
            </div>
        </div>
    </div>



    <!-- old -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
