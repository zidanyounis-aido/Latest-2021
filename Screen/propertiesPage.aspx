<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="propertiesPage.aspx.cs" Inherits="Araneas_ERP.screen.propertiesPage" %>
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
        <a class="lnk" target="_parent" href="javascript:parent.showDialog('<%# DataBinder.Eval(Container.DataItem, "CODEN") %>','<%# DataBinder.Eval(Container.DataItem, "NAMEL") %>','<%# DataBinder.Eval(Container.DataItem, "URL") %>')">
        <img  id="Image1" style=" width:32px" src='../Images/icons/<%# DataBinder.Eval(Container.DataItem, "ICON") %>'
        title='<%# DataBinder.Eval(Container.DataItem, "NAMEL") %>' />
        <br />
            <%# DataBinder.Eval(Container.DataItem, "NAMEL") %>
        </a>
        <%--<div onmousedown="activeDialog('<%# DataBinder.Eval(Container.DataItem, "CODEN") %>')" style="position:absolute" id="dialog_<%# DataBinder.Eval(Container.DataItem, "CODEN") %>" class="ui-widget-content" >
        <table cellpadding="3" cellspacing="0" width="100%" 
        style="height:39px;background-image:url(../Images/dialogHeader.jpg); background-repeat:repeat-x">
        <tr>
            <td align="left">
            <h2><%# DataBinder.Eval(Container.DataItem, "NAMEL") %></h2>
            </td>
            <td align="right">
                <img width="24px" src="../Images/Delete-icon.png" onclick="hideDialog('<%# DataBinder.Eval(Container.DataItem, "CODEN") %>')" />
            </td>
        </tr>
        </table>
        
        </div>--%>

        </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
         </table>
    </FooterTemplate>
    </asp:Repeater>
</asp:Content>
