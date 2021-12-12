<%@ Page MasterPageFile="~/Masters/subPages.Master" Language="C#" AutoEventWireup="true" CodeBehind="subIcons.aspx.cs" Inherits="Araneas_ERP.screen.subIcons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .my-table tbody tr td a {
            display: inline;
        }

            .my-table tbody tr td a:hover {
                text-decoration: none;
            }

        .select-setting-icon, .select-setting-title {
            width: 200px;
            text-align: center;
        }

        svg:hover path, svg:hover circle, svg:hover rect {
            fill: #007aff;
            stroke: #007aff;
        }

        .white-holder tr td {
            padding-top: 8px !important;
        }
    </style>
    <script>
        $(function () {
            $(".ui-widget-content").hide();
        });

//    function showDialog(CODEN) {
//        $("#dialog_" + CODEN).width(0);
//        $("#dialog_" + CODEN).height(0);
//        //        $("#dialog_" + CODEN).css("top", (($(window).height() - this.outerHeight()) / 2) +
//        //                                                $(window).scrollTop() + "px");
//        //        $("#dialog_" + CODEN).css("left", (($(window).width() - this.outerWidth()) / 2) +
//        //                                                $(window).scrollLeft() + "px");
//        $("#dialog_" + CODEN).show();
//        $("#dialog_" + CODEN).animate({ width: 555, height: 370 }, "slow");
//        $("#dialog_" + CODEN).draggable();
//        $("#dialog_" + CODEN).resizable();
//        activeDialog(CODEN);
//    }

//    function activeDialog(CODEN) {
//        $(".ui-widget-content").zIndex(10);
//        $(".ui-widget-content").css("border:solid 1px #cccccc");

//        $("#dialog_" + CODEN).zIndex(11);
//        $("#dialog_" + CODEN).css("border:solid 1px #000000");
//    }

//    function hideDialog(CODEN) {

//        $("#dialog_" + CODEN).fadeOut();
//    }
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="pages_nav">
        <li><a href="#"><%= (Session["lang"] == "0") ? "Settings" : "إعدادات"%></a></li>
    </ul>
    <asp:DataList ID="dlMainIcons" runat="server" Width="100%" RepeatColumns="5" RepeatDirection="Horizontal" CssClass="white-holder">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <a class="lnk" target="_parent" href="javascript:parent.showDialog('<%# DataBinder.Eval(Container.DataItem, "programID") %>','<%# (Session["lang"].ToString() == "0") ? Eval("programName") : Eval("programNameAr")%>','<%# DataBinder.Eval(Container.DataItem, "programURL") %>',<%# DataBinder.Eval(Container.DataItem, "windowWidth") %>,<%# DataBinder.Eval(Container.DataItem, "windowHeight") %>)">
                <div class="col-xs-3">
                    <div class="select-setting-item-holder">
                        <div class="select-setting-icon">
                            <%# DataBinder.Eval(Container.DataItem, "svg") %>
                        </div>
                        <p class="select-setting-title"><%# (Session["lang"].ToString() == "0") ? Eval("programName") : Eval("programNameAr")%> </p>
                    </div>
                </div>
            </a>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:DataList>
    <div class="icon" style="width: 160px;">
        <%--    <a class="lnk" target="_parent" href="javascript:parent.showDialog('34','التقارير المتاخره','../admin/DocumentLateReports',0,0)">
        <img border="0" id="Image1" src="../Images/icons/icon29.png" title="التقارير المتاخره">
        <br>
            التقارير المتاخره
    </a>--%>
    </div>
</asp:Content>
