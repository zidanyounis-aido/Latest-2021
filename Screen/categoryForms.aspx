<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="categoryForms.aspx.cs" Inherits="dms.Screen.categoryForms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
    <asp:Repeater ID="rptMainIcons" runat="server">
    <HeaderTemplate>
        <table>
    </HeaderTemplate>
    <ItemTemplate>
    <tr>
    <td align="center" style="width:36px">
    <a class="lnk" target="_parent" href="javascript:parent.showDialog('<%# DataBinder.Eval(Container.DataItem, "formID") %>','<%# DataBinder.Eval(Container.DataItem, "formName") %>','eForm',600,500)">
        <img border="0" id="Image1" style=" width:64px" src="../Images/icons/icon19.png"
        title='<%# DataBinder.Eval(Container.DataItem, "FormName") %>' />
        <br />
            <%# DataBinder.Eval(Container.DataItem, "FormName")%>
    </a>
        </td>
        </tr>
        <tr>
            <td style="height:10px">
            
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
         </table>
    </FooterTemplate>
    </asp:Repeater>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="outForm" runat="server">
</asp:Content>
