<%@ Page MasterPageFile="~/Masters/subPages.master" Language="C#" AutoEventWireup="true" CodeBehind="ShowDocument.aspx.cs" Inherits="dms.Screen.ShowDocument" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
  #draggable { width: 500px; position:absolute; font-weight:bold; color:Black; font-size:26px; z-index:1000 }
  </style>
    <script src="../JS/jQueryRotate.js" type="text/javascript"></script>
    <script>
    

            function PrintElem(elem) {
                Popup($(elem).html());
            }

            function Popup(data) {
                var mywindow = window.open('', 'HudHud', 'height=400,width=600');
                mywindow.document.write('<html><head><title>ZY Documents</title>');
                /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
                mywindow.document.write('</head><body >');
                mywindow.document.write(data);
                mywindow.document.write('</body></html>');

                mywindow.print();
                mywindow.close();

                return true;
            }

        $(window).scroll(function () {
            $("#toolbar").css({ position: 'fixed', top: '0',zIndex:"2000", width:"100%" });
        });

        $(function () {
            $("#draggable").draggable();
        });

        function printPage() {
            $("#toolbar").hide();
            $(".printHide").hide();
            document.getElementById("divWF").style.display = 'block';
//            PrintElem('#divWF');
          

            window.print();

            $("#toolbar").show();
            $(".printHide").show();
              document.getElementById("divWF").style.display = 'none';
            
        }

        var fontSize = 26;
        function enlarage() {
            fontSize += 2;
            document.getElementById("draggable").style.fontSize = String(fontSize) + "px";
        }
        
        function reduction() {
            fontSize -= 2;
            document.getElementById("draggable").style.fontSize = String(fontSize) + "px";
        }

        
        function enlarageImg() {
            var width = $("#Image1").width();
            $("#Image1").animate({ width: String(width + 50) + "px" }, 500);
            //$("#Image1").css("width", width + 50);
        }

        function reductionImg() {
            var width = $("#Image1").width();
            $("#Image1").animate({ width: String(width - 50) + "px" }, 500);
            //$("#Image1").css("width", width - 50);
        }
        var Rvalue = 0
        function rotatePlus() {
            Rvalue += 90;
            $("#Image1").rotate({ animateTo: Rvalue }) 
        }

        function rotateMin() {
            Rvalue -= 90;
            $("#Image1").rotate({ animateTo: Rvalue })
        }

        function checkNode(obj,id) {
            if (obj.checked) {
                document.getElementById("tbl" + id).style.color = "black";
                document.getElementById("tbl" + id).className = "";
            } else {
                document.getElementById("tbl" + id).style.color = "#cccccc";
                document.getElementById("tbl" + id).className = "printHide";
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div id="toolbar" style="background-color:#cfcfcf; padding:10px;">
<a href="javascript:printPage('#divWF')">
               <img src="../Images/Icons/print.png" border="0" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Print " : "طباعة  "%>
               </a>
               |
               <a href="javascript:enlarage()">
               <img src="../Images/Icons/up.png" border="0" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Enlarge Font" : "تكبير الخط"%>
               </a>
               |
               <a href="javascript:reduction()">
               <img src="../Images/Icons/down.png" border="0" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Reduction Font" : "تصغير الخط"%>
               </a>
               |
               <a href="javascript:enlarageImg()" style="display:none">
               <img src="../Images/Icons/doc-resize-icon.png" border="0" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Enlarge Image" : "تكبير الصورة"%>
               
               |</a>
               <a href="javascript:reductionImg()" style="display:none">
               <img src="../Images/Icons/doc-resize-actual-icon.png" border="0" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Reduction Image" : "تصغير الصورة"%>
               
               |</a>
               <a href="javascript:rotatePlus()">
               <img src="../Images/Icons/arrow-rotate-clockwise-icon.png" border="0" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Rotate to Right" : "دوران إلى اليمين"%>
               </a>
               |
               <a href="javascript:rotateMin()">
               <img src="../Images/Icons/arrow-rotate-anticlockwise-icon.png" border="0" align="absmiddle" />
               <%= (Session["lang"].ToString() == "0") ? "Rotate to Left" : "دوران إلى اليسار"%>
               </a>

</div>
        <div id="draggable">
            <asp:Repeater ID="rptWorkflow" runat="server">
                    <HeaderTemplate>
                        <table cellspacing="0" cellpadding="5">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td valign="top">
                                <input checked="checked" type="checkbox" class="printHide" onclick="checkNode(this,'<%# Eval("ID")%>')" />
                            </td>
                            <td valign="top" align="left">
                                <table cellspacing="0" cellpadding="5" id="tbl<%# Eval("ID")%>">
                                    <tr>
                                <td class="cellUnderline">
                                        <%# (c.convertToDateTime(Eval("actionDateTime")) > DateTime.Now)? "-": Convert.ToDateTime(Eval("actionDateTime")).ToString("dd/MM/yyyy hh:mm tt")%>
                                </td>
                            </tr>
                                    <tr>
                                        <td class="cellUnderline">
                                            <%# Eval("fullName")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cellUnderline">
                                            <%= (Session["lang"].ToString() == "0") ? "Action" : "الإجراء"%>   
                            
                                            <%# getWFAction(c.convertToInt32( Eval("actionType")))%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="border-bottom:1px dashed gray">
                                            <%# Eval("userNotes")%>
                                        </td>
                                    </tr>
                                   
                                </table>
                            </td>
                        </tr>
                        
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
        </div>
        <asp:Image ClientIDMode="Static" ID="Image1" Width="1200px" runat="server" />


        <div id="divWF" style="display:none; background-color:White">

                <asp:Repeater ID="rptFullWorkflow" runat="server">
                    <HeaderTemplate>
                        <table cellspacing="0" cellpadding="5" style="width:100%">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                              <span style="font-weight: bold; color: #AE0000">  <%# getCounter() %></span>
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
                            <td>
                                
                            </td>
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

</asp:Content>
    