<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="myWorkflowDocs.aspx.cs" Inherits="dms.Screen.myWorkflowDocs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .new-drop {
            padding: 4px;
            border-radius: 20px;
        }

        .new-main-input {
            width: 97% !important;
            border: 1px solid #cacaca;
            border-radius: 20px;
            padding: 6px;
            outline: none;
            /*height: 35px;*/
        }

        .margin-top-20 {
            margin-top: 20px;
        }

        .cellTDAr {
            margin-top: 10px;
        }

        .ContentPlaceHolder1_ContentPlaceHolder1_trvFolders_0 {
            margin-right: 5px !important;
        }

        .radio-input-holder label {
        }

        #ContentPlaceHolderBody_lnkNextMulti {
            display: none;
        }

        .toggle {
            --width: 40px;
            --height: calc(var(--width) / 2);
            --border-radius: calc(var(--height) / 2);
            display: inline-block;
            cursor: pointer;
        }

        .toggle__input {
            display: none;
        }

        .toggle__fill {
            position: relative;
            width: var(--width);
            height: var(--height);
            border-radius: var(--border-radius);
            background: #dddddd;
            transition: background 0.2s;
        }

        .toggle__input:checked ~ .toggle__fill {
            background: rgb(0, 114, 255);
        }

            .toggle__input:checked ~ .toggle__fill::after {
                transform: translateX(var(--height));
            }

        .toggle__fill::after {
            content: "";
            position: absolute;
            top: 0;
            left: 0;
            height: var(--height);
            width: var(--height);
            background: #ffffff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.25);
            border-radius: var(--border-radius);
            transition: transform 0.2s;
        }
    </style>

    <script type="text/javascript">

        function PrintElem(elem) {
            Popup($(elem).html());
        }

        function Popup(data) {
            var mywindow = window.open('', 'Imprint_Documents', 'height=400,width=600');
            mywindow.document.write('<html><head><title>Imprint DMS</title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');

            mywindow.print();
            //            mywindow.close();

            //            return true;
        }

        $(document).ready(function () {
            $(".pnlBackGround").hide();
        });
        function callToggalChane(xthis) {
            if ($(xthis).is(":checked")) {
                $("#ContentPlaceHolderBody_lnkNextMulti").show();
                $("#ContentPlaceHolderBody_drpNext").hide();
            }
            else {
                $("#ContentPlaceHolderBody_lnkNextMulti").hide();
                $("#ContentPlaceHolderBody_drpNext").show();
            }
        }
    </script>
    <script type="text/javascript">
        function checkNotes() {
            if ($("#ContentPlaceHolderBody_txtNotes").val() != "") {
                return true;
            }
            else {
                $("#ContentPlaceHolderBody_txtNotes").css("border-color", "red");
                $("#ContentPlaceHolderBody_txtNotes").focus();
                $("#spnStarForNote").show();
                return false;
            }
        }
        var lang =<%= (Session["lang"].ToString() == "0") ? "'en'" : "'ar'"%>;
        function openFileDocument(xthis) {
            //var x = $(".menuDiv")[1];
            //var title = $(x).find(".imgcont").attr('title');
            //addTab('1', 'المجلدات', 'defaultAr', 99);
            var title = 'Folders';
            if (lang == "ar") {
                title = 'المجلدات';
            }
            parent.addTab('1', title, 'documentInfo.aspx?docID=' + $(xthis).attr("data-id") + '', 99);
            //parent
        }
    </script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <%--<link rel="stylesheet" href="/resources/demos/style.css">--%>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        function openThisInbox(xthis) {
            $(xthis).parent().parent().find('input[type="image"]').click()
        }
    </script>
    <link rel="stylesheet" href="https://printjs-4de6.kxcdn.com/print.min.css" />
    <script src="https://printjs-4de6.kxcdn.com/print.min.js"></script>
    <style>
        #ContentPlaceHolderBody_pnlMultiUsers {
            background: #FFFFFF;
            padding: 10px;
            border: solid 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitle" runat="server">
    <ul class="pages_nav">
        <li><a href="#"><%= (Session["lang"] == "0") ? "Inbox" : "البريد"%></a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderRightBar" runat="server">
    <!-- not used now !-->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div class="row row-flex">
        <div class="col-xs-3">
            <div class="list-folders scroll-theme" runat="server" id="divInbox">
                <ul class="main-ul-mail">
                    <asp:ListView ID="dlgrdMyDocs" runat="server" CausesValidation="false" DataKeyNames="ID" AutoPostBack="true" OnSelectedIndexChanging="dlgrdMyDocs_SelectedIndexChanging">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="ButtonSelect" CommandName="Select">
                                <%--<asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CommandArgument='<%# Eval("ID") %>' CausesValidation="false"></asp:LinkButton>--%>
                                <asp:HiddenField ID="hdnDocID" Value='<%# Eval("docID") %>' runat="server" />
                                <asp:HiddenField ID="hdnWFID" Value='<%# Eval("ID") %>' runat="server" />
                                <asp:HiddenField ID="hdnIsDelay" Value='<%# Eval("isDelay") %>' runat="server" />
                                <li class="" runat="server" id="liItem" data-id='<%# Eval("ID")%>' data-docid='<%# Eval("docID")%>' style="color: #000">
                                    <p class="mail-name"><%# Eval("docName")%></p>
                                    <p class="mail-dec"><%# (Session["lang"].ToString() == "0") ? Eval("docTypDesc") :Eval("docTypDescAr")%></p>
                                    <div class="mail-info">
                                        <span class="mail-time"><%# Eval("LeftTime")%></span>
                                        <span class="mail-date"><%# DateTime.Now.Date >Convert.ToDateTime(Eval("receiveDate")).Date ? Session["lang"].ToString() == "0" ? Convert.ToDateTime(Eval("receiveDate")).ToString("ddd dd/MM") : Convert.ToDateTime(Eval("receiveDate")).ToString("ddd dd/MM",new System.Globalization.CultureInfo("ar-AE")) :Convert.ToDateTime(Eval("receiveDate")).ToString("hh:mm tt") %></span>
                                    </div>
                                </li>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:ListView>
                    <asp:GridView ID="grdMyDocs" runat="server" AutoGenerateColumns="False"
                        GridLines="None"
                        OnSelectedIndexChanged="grdMyDocs_SelectedIndexChanged"
                        OnDataBound="grdMyDocs_DataBound" Visible="false">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Title">
                                <ItemTemplate>
                                    <div onclick="openThisInbox(this);">
                                        <div id="div1" style="display: inline-block; width: 79%;">
                                            <span style='color: <%# Eval("Color")%>'><%# (Session["lang"].ToString() == "0") ? Eval("docTypDesc") :Eval("docTypDescAr")%><br />
                                                <b style='color: <%# Eval("Color")%>'><%# Eval("docName")%></b></span>
                                            <br />
                                            <span style="color: #1c94c4;"><%# Eval("LeftTime")%><br />
                                            </span>
                                        </div>
                                        <div id="div2" style="display: inline-block;">
                                            <span style='font-size: 11px; font-weight: bold; color: <%# Eval("Color")%>'>
                                                <%# DateTime.Now.Date >Convert.ToDateTime(Eval("receiveDate")).Date ? Session["lang"].ToString() == "0" ? Convert.ToDateTime(Eval("receiveDate")).ToString("ddd dd/MM") : Convert.ToDateTime(Eval("receiveDate")).ToString("ddd dd/MM",new System.Globalization.CultureInfo("ar-AE")) :Convert.ToDateTime(Eval("receiveDate")).ToString("hh:mm tt") %>
                                            </span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="../Images/Icons/Select-icon.png" ControlStyle-CssClass="hide-Item" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnDocID" Value='<%# Eval("docID") %>' runat="server" />
                                    <asp:HiddenField ID="hdnWFID" Value='<%# Eval("ID") %>' runat="server" />
                                    <asp:HiddenField ID="hdnIsDelay" Value='<%# Eval("isDelay") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle />
                        <FooterStyle />
                        <HeaderStyle />
                        <PagerStyle />
                        <RowStyle />
                        <SelectedRowStyle />
                        <SortedAscendingCellStyle />
                        <SortedAscendingHeaderStyle />
                        <SortedDescendingCellStyle />
                        <SortedDescendingHeaderStyle />
                    </asp:GridView>
                </ul>
            </div>
        </div>
        <div class="col-xs-9">
            <asp:Panel ID="docDetails" runat="server">
                <!-- Nav tabs -->
                <ul class="ul-edit-doc-tabs" role="tablist">
                    <li role="presentation" class="active"><a href="#edit-info" role="tab" data-toggle="tab">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16.706" height="19.234"
                            viewBox="0 0 16.706 19.234">
                            <g id="Group_2681" data-name="Group 2681" transform="translate(-96 -34.728)">
                                <path id="Path_7066" data-name="Path 7066"
                                    d="M110.352,58.884a3,3,0,0,1-2.558-4.554H98.529A2.531,2.531,0,0,0,96,56.859V70.19a2.53,2.53,0,0,0,2.529,2.529H109.1a2.531,2.531,0,0,0,2.529-2.529V58.6A2.981,2.981,0,0,1,110.352,58.884Zm-3.089,11.076h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.678h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.218h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Zm0-3.678h-6.9a.69.69,0,0,1,0-1.379h6.9a.69.69,0,1,1,0,1.379Z"
                                    transform="translate(0 -18.757)" fill="#707070" />
                                <g id="Group_2680" data-name="Group 2680" transform="translate(108.047 34.728)">
                                    <g id="Group_2679" data-name="Group 2679" transform="translate(0 0)">
                                        <path id="Path_7067" data-name="Path 7067"
                                            d="M379.493,35.41a2.33,2.33,0,1,0,.682,1.647A2.314,2.314,0,0,0,379.493,35.41Zm-1.647-.045a.5.5,0,1,1-.5.5A.5.5,0,0,1,377.846,35.365Zm.637,3.185h-1.274v-.273h.273V36.912h-.273v-.273h1v1.638h.273Z"
                                            transform="translate(-375.516 -34.728)" fill="#707070" />
                                    </g>
                                </g>
                            </g>
                        </svg>
                        <%= (Session["lang"].ToString() == "0") ? "Document Info" : "معلومات المستند"%>
                    </a>
                    </li>

                    <li role="presentation"><a href="#edit-Action" role="tab" data-toggle="tab">
                        <svg xmlns="http://www.w3.org/2000/svg" width="17.999" height="19.001"
                            viewBox="0 0 17.999 19.001">
                            <path id="Subtraction_27" data-name="Subtraction 27"
                                d="M-2053-5571h-8a5.006,5.006,0,0,1-5-5v-9a5.006,5.006,0,0,1,5-5h8a5.006,5.006,0,0,1,5,5v9A5.006,5.006,0,0,1-2053-5571Zm-10.1-7.448h0a6.412,6.412,0,0,0,6.126,4.466,6.449,6.449,0,0,0,6.2-4.724h-1.1c-.043.13-.093.26-.146.386a5.4,5.4,0,0,1-.5.915,5.267,5.267,0,0,1-.658.8,5.429,5.429,0,0,1-.8.66,5.426,5.426,0,0,1-.913.5,5.328,5.328,0,0,1-1.009.313,5.425,5.425,0,0,1-1.087.111,5.392,5.392,0,0,1-1.086-.111,5.281,5.281,0,0,1-1.009-.313,5.473,5.473,0,0,1-.915-.5,5.328,5.328,0,0,1-.8-.66,5.3,5.3,0,0,1-.658-.8,5.42,5.42,0,0,1-.5-.915c-.027-.063-.054-.13-.08-.2l1.258-.085-1.958-1.911-1.686,2.156,1.3-.088Zm4.621.749h0a3.118,3.118,0,0,0,.649.268v1.128h1.711v-1.128a3.111,3.111,0,0,0,.651-.268l.8.8,1.211-1.211-.8-.8a3.059,3.059,0,0,0,.268-.651h1.128v-1.71h-1.128a3.048,3.048,0,0,0-.268-.651l.8-.8-1.211-1.211-.8.8a3.1,3.1,0,0,0-.651-.27v-1.128h-1.711v1.128a3.062,3.062,0,0,0-.65.27l-.8-.8-1.211,1.211.8.8a3.12,3.12,0,0,0-.271.651h-1.126v1.71h1.126a3.113,3.113,0,0,0,.271.651l-.8.8,1.211,1.211.8-.8Zm1.505-8.1a5.532,5.532,0,0,1,1.087.109,5.476,5.476,0,0,1,1.009.313,5.353,5.353,0,0,1,.913.5,5.408,5.408,0,0,1,.8.658,5.432,5.432,0,0,1,.658.8,5.328,5.328,0,0,1,.5.915c.026.061.052.127.08.2l-1.259.085,1.96,1.911,1.685-2.156-1.3.087a6.415,6.415,0,0,0-6.128-4.465,6.449,6.449,0,0,0-6.2,4.722h1.1c.05-.144.1-.27.146-.384a5.322,5.322,0,0,1,.5-.915,5.463,5.463,0,0,1,.658-.8,5.315,5.315,0,0,1,.8-.658,5.386,5.386,0,0,1,.915-.5,5.421,5.421,0,0,1,1.009-.313A5.5,5.5,0,0,1-2056.969-5585.8Zm0,7.441a2.051,2.051,0,0,1-.8-.164,2.052,2.052,0,0,1-1.217-1.5,2.05,2.05,0,0,1,.563-1.845,2.03,2.03,0,0,1,1.446-.6,2.054,2.054,0,0,1,1.9,1.254,2.031,2.031,0,0,1,.161.8A2.058,2.058,0,0,1-2056.969-5578.356Z"
                                transform="translate(2066 5590)" fill="#8f9198" />
                        </svg>


                        <%= (Session["lang"].ToString() == "0") ? "Action" : "الاجراء"%>
                    </a>
                    </li>

                    <li role="presentation"><a href="#edit-transfer" role="tab" data-toggle="tab">
                        <svg xmlns="http://www.w3.org/2000/svg" width="17.999" height="19.001"
                            viewBox="0 0 17.999 19.001">
                            <path id="Subtraction_28" data-name="Subtraction 28"
                                d="M-2053-5571h-8a5.006,5.006,0,0,1-5-5v-9a5.006,5.006,0,0,1,5-5h8a5.006,5.006,0,0,1,5,5v9A5.006,5.006,0,0,1-2053-5571Zm-2.72-7.936V-5574h5.759v-4.936Zm-8.28,0V-5574h5.759v-4.936Zm3.291-1.672h7.456v.85h.823v-1.672h-4.14v-.85h-.823v.85h-4.14v1.672h.824v-.849Zm.849-7.43v4.936h5.759v-4.936Zm9.075,13.215h-4.112v-2.468h4.112v2.467Zm-8.28,0h-4.112v-2.468h4.112v2.467Zm4.14-9.1h-4.112v-2.468h4.112v2.467Z"
                                transform="translate(2066 5590)" fill="#8f9198" />
                        </svg>
                        <%= (Session["lang"].ToString() == "0") ? "Workflow History" : "مسار العمل"%>
                    </a>
                    </li>
                </ul>

                <div class="tab-content">

                    <div class="white-holder tab-pane active" id="edit-info">
                        <div class="max-width-holder">
                            <div class="col-xs-4">
                                <div class="main-field-holder">
                                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Document ID:" : "رقم الملف"%> </label>
                                    <asp:TextBox ID="txtDocID" runat="server" ReadOnly="True" CssClass="main-input"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-4">
                                <div class="main-field-holder">
                                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Document Name:" : "اسم الملف"%>   </label>
                                    <asp:TextBox ID="txtDocName" runat="server" CssClass="main-input" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-4">
                                <div class="main-field-holder">
                                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Action end date:" : "الموعد النهائي للاجراء"%>  </label>
                                    <asp:TextBox ID="txtActionEndDate" runat="server" CssClass="main-input" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-4">
                                <div class="main-field-holder">
                                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Document Type:" : "نوع الملف"%>  </label>
                                    <asp:DropDownList ID="drpDocTypID" runat="server" CssClass="new-drop" AutoPostBack="True"
                                        OnSelectedIndexChanged="drpDocTypID_SelectedIndexChanged" Enabled="False"
                                        Font-Size="16px">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnDocExt" runat="server" />
                                    <asp:HiddenField ID="hdnAddedDate" runat="server" />
                                </div>
                            </div>
                            <div class="col-xs-4">
                                <div class="main-field-holder">
                                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Folder" : "مجلد"%>  </label>
                                    <asp:DropDownList ID="drpFldrID" CssClass="new-drop" runat="server" Enabled="False"
                                        Font-Size="16px">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnAddedUserID" runat="server" />
                                    <asp:HiddenField ID="hdnFolderSeq" runat="server" />
                                    <asp:HiddenField ID="hdnDocTypeSeq" runat="server" />
                                    <asp:HiddenField ID="hdnFolderDocTypeSeq" runat="server" />
                                    <asp:HiddenField ID="hdnOcrContent" runat="server" />
                                </div>
                            </div>
                        </div>
                        <asp:Panel runat="server" class="max-width-holder" id="tblDocMetas">
                        </asp:Panel>

                        <div class="files-area files-area-scan mail-file-item-holder">
                            <%--   <asp:Table ID="tblVersions" runat="server">
                        </asp:Table>--%>
                            <asp:ListView ID="tblVersions" runat="server">
                                <ItemTemplate>
                                    <div class="mail-file-item">
                                        <div class="file-item">
                                            <span class="file-format file-format-jpg"><%# Eval("ext") %>
                                            </span>
                                            <div class="file-info">
                                                <span class="file-name"><%# string.Concat(Eval("docName"), "-", Eval("version")+"."+Eval("ext"))%></span>
                                                <span class="added-on"><%= (Session["lang"].ToString() == "0") ? "Added Date" : "تم اضافتة"%> <span class="date"><%# Eval("addedDate") %></span></span>
                                            </div>
                                        </div>

                                        <div class="file-action">
                                            <asp:LinkButton runat="server" ID="lnkDownlod" CommandArgument='<%# string.Concat(Eval("docId"), "-", Eval("version"),"."+Eval("ext") )%>' OnClick="lnkDownlod_Click">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="32" height="33"
                                                viewBox="0 0 32 33">
                                                <g id="Group_2675" data-name="Group 2675" transform="translate(-0.276)">
                                                    <ellipse id="Ellipse_578" data-name="Ellipse 578" cx="16" cy="16.5"
                                                        rx="16" ry="16.5" transform="translate(0.276 0)" fill="#0072ff" />
                                                    <g id="Group_2673" data-name="Group 2673"
                                                        transform="translate(8.024 7.628)">
                                                        <g id="Group_2670" data-name="Group 2670"
                                                            transform="translate(3.712)">
                                                            <g id="Group_2669" data-name="Group 2669"
                                                                transform="translate(0)">
                                                                <path id="Path_7062" data-name="Path 7062"
                                                                    d="M136.434,7.734a.529.529,0,0,0-.483-.311h-2.121V.53A.53.53,0,0,0,133.3,0H131.18a.53.53,0,0,0-.53.53V7.423h-2.121a.53.53,0,0,0-.4.879l3.712,4.242a.529.529,0,0,0,.8,0L136.351,8.3A.529.529,0,0,0,136.434,7.734Z"
                                                                    transform="translate(-127.998)" fill="#fff" />
                                                            </g>
                                                        </g>
                                                        <g id="Group_2672" data-name="Group 2672"
                                                            transform="translate(0 11.665)">
                                                            <g id="Group_2671" data-name="Group 2671"
                                                                transform="translate(0)">
                                                                <path id="Path_7063" data-name="Path 7063"
                                                                    d="M29.786,352v3.182H18.121V352H16v4.242a1.06,1.06,0,0,0,1.06,1.061H30.847a1.06,1.06,0,0,0,1.06-1.061V352Z"
                                                                    transform="translate(-16 -352)" fill="#fff" />
                                                            </g>
                                                        </g>
                                                    </g>
                                                </g>
                                            </svg>
                                            </asp:LinkButton>
                                            <%--<a href="#">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="33" height="33"
                                                viewBox="0 0 33 33">
                                                <g id="Group_2676" data-name="Group 2676" transform="translate(0)">
                                                    <circle id="Ellipse_577" data-name="Ellipse 577" cx="16.5" cy="16.5"
                                                        r="16.5" transform="translate(0 0)" fill="#0072ff" />
                                                    <g id="Group_2674" data-name="Group 2674"
                                                        transform="translate(9.087 9.481)">
                                                        <path id="Path_7057" data-name="Path 7057"
                                                            d="M64,137.831a1.968,1.968,0,0,0,1.966,1.966h7.865a1.968,1.968,0,0,0,1.966-1.966V128H64Z"
                                                            transform="translate(-63.017 -124.068)" fill="#fff" />
                                                        <path id="Path_7058" data-name="Path 7058"
                                                            d="M40.848.983V0H36.915V.983H32V2.949H45.763V.983Z"
                                                            transform="translate(-32)" fill="#fff" />
                                                    </g>
                                                </g>
                                            </svg>
                                        </a>--%>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                            <asp:HiddenField ID="hdnLastVersion" runat="server" />
                        </div>
                    </div>

                    <div class="white-holder tab-pane" id="edit-Action">
                        <div class="max-width-holder">

                            <div class="col-xs-6">
                                <div class="main-field-holder">
                                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Move to" : "تحويل للإجراء إلى"%> </label>
                                    <!-- Rounded switch -->
                                    <div>
                                        <label class="toggle" for="myToggle">
                                            <input class="toggle__input" name="" type="checkbox" id="myToggle" onchange="callToggalChane(this);" />
                                            <div class="toggle__fill"></div>
                                        </label>
                                        <label style="height: 10px; vertical-align: top;"><%= (Session["lang"].ToString() == "0") ? "Multi User" : "عدة مستخدمين"%></label>
                                    </div>
                                    <br />
                                    <%-- <label class="switch">
                                        <input type="checkbox" onchange="callToggalChane(this);" />
                                        <span class="<%= (Session["lang"].ToString() == "0") ? "Multi User" : "عدة مستخدمين"%>"></span>
                                    </label>--%>
                                    <asp:DropDownList ID="drpNext" runat="server" CssClass="new-drop">
                                    </asp:DropDownList>
                                    <asp:HyperLink ID="lnkNextMulti" CssClass="btn-main" runat="server" NavigateUrl="#" Style="cursor: pointer">Multi Users</asp:HyperLink>
                                    <asp:ModalPopupExtender ID="btnChooseCat_ModalPopupExtender" runat="server" CancelControlID="btnCloseWindow"
                                        DynamicServicePath="" PopupControlID="pnlMultiUsers" BackgroundCssClass="modalBackground" Enabled="True" TargetControlID="lnkNextMulti">
                                    </asp:ModalPopupExtender>
                                    <asp:Label ForeColor="Red" ID="lblNextMsg" Visible="False" runat="server"
                                        Text="Label"></asp:Label>
                                </div>

                                <div class="main-field-holder">
                                    <label class="main-label">
                                        <%= (Session["lang"].ToString() == "0") ? "Next node (Regarding to Workflow)" : "التالي (بحسب مسار العمل)"%>
                                    </label>
                                    <asp:Label ID="lblDefaultNext" runat="server" Text="-"></asp:Label>
                                </div>
                                <div class="main-field-holder">
                                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Action" : "الإجراء"%></label>
                                    <asp:DropDownList ID="drpAction" runat="server" CssClass="new-drop">
                                        <asp:ListItem Value="0">No action</asp:ListItem>
                                        <asp:ListItem Value="1">Approve</asp:ListItem>
                                        <asp:ListItem Value="2">Decline</asp:ListItem>
                                        <asp:ListItem Value="3">Approve with conditions</asp:ListItem>
                                        <asp:ListItem Value="4">Share</asp:ListItem>
                                        <asp:ListItem Value="5">Forward</asp:ListItem>
                                        <asp:ListItem Value="6">Seen</asp:ListItem>
                                        <asp:ListItem Value="7">Approve within terms</asp:ListItem>
                                        <asp:ListItem Value="7">Action necessary</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="main-field-holder">
                                    <label class="main-label">
                                        <%= (Session["lang"].ToString() == "0") ? "Action Days" : " ايام الاجراء "%>
                                    </label>
                                    <input type="text" runat="server" id="txtenddateCount" class="main-input" />
                                </div>
                            </div>

                            <div class="col-xs-6">
                                <div class="main-field-holder">
                                    <label class="main-label"><%= (Session["lang"].ToString() == "0") ? "Notes" : "ملاحظات"%> <span id="spnStarForNote" style="display: none; color: red; font-size: 16px;">*</span></label>
                                    <%--<textarea class="main-textarea textarea-lg-three"></textarea>--%>
                                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" CssClass="main-input"
                                        Height="100px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <asp:Label ID="lblRes" runat="server" ForeColor="#CC0000"></asp:Label>
                            </div>
                        </div>

                        <div class="control-side-holder control-side-holder-footer">
                            <div class="start-side">
                                <%-- <a class="btn-main">--%>
                                <asp:LinkButton ID="LinkButton1" CssClass="btn-main" runat="server" OnClick="LinkButton1_Click">
                                <div class="btn-main-wrapper">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="17.133" height="14.277"
                                        viewBox="0 0 17.133 14.277">
                                        <path id="Path_7093" data-name="Path 7093"
                                            d="M-318.14-5.263A.536.536,0,0,0-318.7-5.3L-334.76,3.086a.537.537,0,0,0-.286.515.536.536,0,0,0,.36.466l4.465,1.526,9.509-8.131-7.358,8.865,7.483,2.558a.548.548,0,0,0,.173.029.538.538,0,0,0,.278-.078.538.538,0,0,0,.251-.378l1.963-13.206A.536.536,0,0,0-318.14-5.263Z"
                                            transform="translate(335.048 5.363)" fill="#fff" />
                                    </svg>
                                    <%= (Session["lang"].ToString() == "0") ? "Submit" : "تنفيذ"%>
                                </div>
                                </asp:LinkButton>
                                <%--</a>--%>

                                <asp:LinkButton ID="LinkButton2" CssClass="btn-main btn-white" runat="server" OnClick="lnkUndo_Click" Style="margin-right: 20px;" OnClientClick="return checkNotes();">
                                <div class="btn-main-wrapper">
                                    <svg id="Group_2494" data-name="Group 2494" xmlns="http://www.w3.org/2000/svg"
                                        width="22.487" height="22.487" viewBox="0 0 22.487 22.487">
                                        <g id="Group_2175" data-name="Group 2175">
                                            <circle id="Ellipse_17" data-name="Ellipse 17" cx="11.244" cy="11.244"
                                                r="11.244" fill="#f4f4f4">
                                            </circle>
                                            <g id="Group_2166" data-name="Group 2166"
                                                transform="translate(7.496 7.496)">
                                                <line id="Line_28" data-name="Line 28" y2="11.745"
                                                    transform="translate(8.305) rotate(45)" fill="none"
                                                    stroke="#8f9198" stroke-linecap="round" stroke-width="2">
                                                </line>
                                                <line id="Line_29" data-name="Line 29" x2="11.745"
                                                    transform="translate(0) rotate(45)" fill="none" stroke="#8f9198"
                                                    stroke-linecap="round" stroke-width="2">
                                                </line>
                                            </g>
                                        </g>
                                    </svg>
                                    <%= (Session["lang"].ToString() == "0") ? "Return the document" : "إعادة المستند"%>
                                </div>
                                </asp:LinkButton>
                            </div>

                            <div class="end-side">

                                <asp:LinkButton ID="LinkArchiveNotFinshed" CssClass="btn-main" runat="server" OnClick="LinkArchiveNotFinshed_Click" Style="margin-right: 20px;" OnClientClick="return checkNotes();">
                                <div class="btn-main-wrapper">
                                    <svg id="Group_2657" data-name="Group 2657" xmlns="http://www.w3.org/2000/svg"
                                        width="12.296" height="14.053" viewBox="0 0 12.296 14.053">
                                        <path id="Path_7057" data-name="Path 7057"
                                            d="M64,136.783a1.759,1.759,0,0,0,1.757,1.757h7.026a1.759,1.759,0,0,0,1.757-1.757V128H64Z"
                                            transform="translate(-63.122 -124.487)" fill="#fff">
                                        </path>
                                        <path id="Path_7058" data-name="Path 7058"
                                            d="M39.9.878V0H36.392V.878H32V2.635H44.3V.878Z"
                                            transform="translate(-32)" fill="#fff">
                                        </path>
                                    </svg>
                                    <%= (Session["lang"].ToString() == "0") ? "Archive  not finished" : "ارشفه غير منتهي"%>
                                </div>
                                </asp:LinkButton>

                                <asp:LinkButton ID="linkExport" CssClass="btn-main" runat="server" OnClick="linkExport_Click" Visible="false" Style="margin-right: 20px;">
                                <div class="btn-main-wrapper">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="12.728" height="16.477"
                                            viewBox="0 0 12.728 16.477">
                                            <g id="surface1" transform="translate(-58.885 0.998)">
                                                <path id="Path_7050" data-name="Path 7050"
                                                    d="M338.5,19.06h2.14l-3.269-3.43v2.306A1.123,1.123,0,0,0,338.5,19.06Z"
                                                    transform="translate(-269.502 -16.092)" fill="#fff">
                                                </path>
                                                <path id="Path_7051" data-name="Path 7051"
                                                    d="M67.167,1.844V-1h-6.7A1.594,1.594,0,0,0,58.885.584V13.9a1.594,1.594,0,0,0,1.587,1.582h9.555A1.594,1.594,0,0,0,71.613,13.9V3.666h-2.62A1.822,1.822,0,0,1,67.167,1.844Zm-4.708,5.68a.35.35,0,0,1,.493.017L64.9,9.634V4.656a.349.349,0,1,1,.7,0V9.638l1.949-2.092a.349.349,0,1,1,.51.475L65.5,10.763a.349.349,0,0,1-.257.109.356.356,0,0,1-.257-.109L62.433,8.021A.365.365,0,0,1,62.459,7.524Zm6.726,5.3a.35.35,0,0,1-.349.349H61.662a.349.349,0,0,1,0-.7h7.179A.346.346,0,0,1,69.185,12.82Z"
                                                    fill="#fff">
                                                </path>
                                            </g>
                                        </svg>
                                        تصدير
                                </div>
                                </asp:LinkButton>

                            </div>
                        </div>
                    </div>

                    <div class="white-holder tab-pane" id="edit-transfer">
                        <div class="control-side-holder">
                            <div class="start-side">
                            </div>

                            <div class="end-side">
                                <a onclick="printJS('divWF', 'html')" class="btn-main">
                                    <div class="btn-main-wrapper">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="17.02" height="17.053"
                                            viewBox="0 0 17.02 17.053">
                                            <g id="Group_2658" data-name="Group 2658" transform="translate(-0.5)">
                                                <path id="Path_7059" data-name="Path 7059"
                                                    d="M99.624,3.3V2.5a2.5,2.5,0,0,0-2.5-2.5H91a2.5,2.5,0,0,0-2.5,2.5v.8Z"
                                                    transform="translate(-85.069)" fill="#fff" />
                                                <path id="Path_7060" data-name="Path 7060"
                                                    d="M118.5,319v5.429a1,1,0,0,0,1,1h7.128a1,1,0,0,0,1-1V319Zm5.9,4.263h-2.665a.5.5,0,0,1,0-1H124.4a.5.5,0,1,1,0,1Zm0-2.132h-2.665a.5.5,0,0,1,0-1H124.4a.5.5,0,1,1,0,1Z"
                                                    transform="translate(-114.07 -308.375)" fill="#fff" />
                                                <path id="Path_7061" data-name="Path 7061"
                                                    d="M15.022,129H3a2.5,2.5,0,0,0-2.5,2.5v4a2.5,2.5,0,0,0,2.5,2.5h.433v-2.665h-.3a.5.5,0,1,1,0-1H14.855a.5.5,0,1,1,0,1h-.3v2.665h.466a2.5,2.5,0,0,0,2.5-2.5v-4A2.5,2.5,0,0,0,15.022,129ZM4.73,132.131h-1.6a.5.5,0,1,1,0-1h1.6a.5.5,0,1,1,0,1Z"
                                                    transform="translate(0 -124.703)" fill="#fff" />
                                            </g>
                                        </svg>
                                        <%= (Session["lang"].ToString() == "0") ? "Print Workflow" : "طباعة مسار العمل"%>
                                    </div>
                                </a>
                                <a class="btn-main" runat="server" id="btnExport" onserverclick="btnExport_ServerClick">
                                    <div class="btn-main-wrapper">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="12.728" height="16.477"
                                            viewBox="0 0 12.728 16.477">
                                            <g id="surface1" transform="translate(-58.885 0.998)">
                                                <path id="Path_7050" data-name="Path 7050"
                                                    d="M338.5,19.06h2.14l-3.269-3.43v2.306A1.123,1.123,0,0,0,338.5,19.06Z"
                                                    transform="translate(-269.502 -16.092)" fill="#fff">
                                                </path>
                                                <path id="Path_7051" data-name="Path 7051"
                                                    d="M67.167,1.844V-1h-6.7A1.594,1.594,0,0,0,58.885.584V13.9a1.594,1.594,0,0,0,1.587,1.582h9.555A1.594,1.594,0,0,0,71.613,13.9V3.666h-2.62A1.822,1.822,0,0,1,67.167,1.844Zm-4.708,5.68a.35.35,0,0,1,.493.017L64.9,9.634V4.656a.349.349,0,1,1,.7,0V9.638l1.949-2.092a.349.349,0,1,1,.51.475L65.5,10.763a.349.349,0,0,1-.257.109.356.356,0,0,1-.257-.109L62.433,8.021A.365.365,0,0,1,62.459,7.524Zm6.726,5.3a.35.35,0,0,1-.349.349H61.662a.349.349,0,0,1,0-.7h7.179A.346.346,0,0,1,69.185,12.82Z"
                                                    fill="#fff">
                                                </path>
                                            </g>
                                        </svg>
                                        <%= (Session["lang"].ToString() == "0") ? "Export" : "تصدير"%>
                                    </div>
                                </a>
                            </div>
                        </div>

                        <div runat="server" id="ExportDiv">
                            <ul class="mail-path-holder" id="divWF">
                                <asp:Repeater ID="rptWorkflow" runat="server">
                                    <ItemTemplate>
                                        <li class="mail-path-item">
                                            <div class="line">
                                                <div class="title">
                                                    <%= (Session["lang"].ToString() == "0") ? "User" : "المستخدم"%>
                                                </div>
                                                <div class="result">
                                                    <%# Eval("fullName")%>
                                                </div>
                                            </div>

                                            <div class="line">
                                                <div class="title">
                                                    <%= (Session["lang"].ToString() == "0") ? "Receipt date\time" : "تاريخ و وقت الإستلام"%>
                                                </div>
                                                <div class="result">
                                                    <%# Convert.ToDateTime( Eval("receiveDate")).ToString("dd/MM/yyyy hh:mm tt")%>
                                                </div>
                                            </div>

                                            <div class="line">
                                                <div class="title">
                                                    <%= (Session["lang"].ToString() == "0") ? "Action" : "الإجراء"%>
                                                </div>
                                                <div class="result">
                                                    <%# getWFAction(c.convertToInt32( Eval("actionType")))%>
                                                </div>
                                            </div>

                                            <div class="line">
                                                <div class="title">
                                                    <%= (Session["lang"].ToString() == "0") ? "Action date\time" : "تاريخ و وقت الإجراء"%>
                                                </div>
                                                <div class="result">
                                                    <%# (c.convertToDateTime(Eval("actionDateTime")) > DateTime.Now) ? "-" : Convert.ToDateTime(Eval("actionDateTime")).ToString("dd/MM/yyyy hh:mm tt")%>
                                                </div>
                                            </div>

                                            <div class="line">
                                                <div class="title">
                                                    <%= (Session["lang"].ToString() == "0") ? "Notes" : "ملاحظات"%>
                                                </div>
                                                <div class="result">
                                                    <%# Eval("userNotes")%>
                                                </div>
                                            </div>
                                        </li>
                                    </ItemTemplate>

                                </asp:Repeater>


                            </ul>
                        </div>
                        <div style="display: none">
                            <table>
                                <asp:Repeater ID="rptHideWorkflow" runat="server">
                                    <ItemTemplate>
                                        <table border="1">
                                            <tr>
                                                <td class="title">
                                                    <%= (Session["lang"].ToString() == "0") ? "User" : "المستخدم"%>
                                                </td>
                                                <td class="result">
                                                    <%# Eval("fullName")%>
                                                </td>

                                                <td class="title">
                                                    <%= (Session["lang"].ToString() == "0") ? "Receipt date\time" : "تاريخ و وقت الإستلام"%>
                                                </td>
                                                <td class="result">
                                                    <%# Convert.ToDateTime( Eval("receiveDate")).ToString("dd/MM/yyyy hh:mm tt")%>
                                                </td>

                                                <td class="title">
                                                    <%= (Session["lang"].ToString() == "0") ? "Action" : "الإجراء"%>
                                                </td>
                                                <td class="result">
                                                    <%# getWFAction(c.convertToInt32( Eval("actionType")))%>
                                                </td>

                                                <td class="title">
                                                    <%= (Session["lang"].ToString() == "0") ? "Action date\time" : "تاريخ و وقت الإجراء"%>
                                                </td>
                                                <td class="result">
                                                    <%# (c.convertToDateTime(Eval("actionDateTime")) > DateTime.Now) ? "-" : Convert.ToDateTime(Eval("actionDateTime")).ToString("dd/MM/yyyy hh:mm tt")%>
                                                </td>

                                                <td class="title">
                                                    <%= (Session["lang"].ToString() == "0") ? "Notes" : "ملاحظات"%>
                                                </td>
                                                <td class="result">
                                                    <%# Eval("userNotes")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>





        <asp:Panel ID="pnlEmpty" Visible="false" runat="server" HorizontalAlign="Center" Style="width: 600%">
            <br />
            <br />
            <img src="../Images/Trash-2-Empty-icon.png" />

            <h1 style="color: #336699">
                <%= (Session["lang"].ToString() == "0") ? "There is no Documents pending in your side" : "ليس لديك وثائق معلقة"%>
    
            </h1>
        </asp:Panel>
        <asp:Panel ID="pnlMultiUsers" runat="server" CssClass="pnlBackGround">
            <asp:CheckBoxList ID="chkUsers" runat="server" RepeatColumns="2" CssClass="chkTable">
            </asp:CheckBoxList>
            <br />
            <asp:Button ID="btnCloseWindow" runat="server" Text="Done" Width="100px" />
        </asp:Panel>
    </div>
</asp:Content>
