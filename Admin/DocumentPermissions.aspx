<%@ Page Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="DocumentPermissions.aspx.cs" Inherits="dms.Screen.DocumentPermissions" %>


<asp:Content ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript" src="../JS/dragresize.js"></script>

<style type="text/css">

/* Required CSS classes: must be included in all pages using this script */

/* Apply the element you want to drag/resize */
.drsElement {
 position: absolute;
 border: 1px solid #333;
}

/*
 The main mouse handle that moves the whole element.
 You can apply to the same tag as drsElement if you want.
*/
.drsMoveHandle {
 height: 20px;
 background-color: #CCC;
 border-bottom: 1px solid #666;
 cursor: move;
}

/*
 The DragResize object name is automatically applied to all generated
 corner resize handles, as well as one of the individual classes below.
*/
.dragresize {
 position: absolute;
 width: 5px;
 height: 5px;
 font-size: 1px;
 background: #EEE;
 border: 1px solid #333;
}

/*
 Individual corner classes - required for resize support.
 These are based on the object name plus the handle ID.
*/
.dragresize-tl {
 top: -8px;
 left: -8px;
 cursor: nw-resize;
}
.dragresize-tm {
 top: -8px;
 left: 50%;
 margin-left: -4px;
 cursor: n-resize;
}
.dragresize-tr {
 top: -8px;
 right: -8px;
 cursor: ne-resize;
}

.dragresize-ml {
 top: 50%;
 margin-top: -4px;
 left: -8px;
 cursor: w-resize;
}
.dragresize-mr {
 top: 50%;
 margin-top: -4px;
 right: -8px;
 cursor: e-resize;
}

.dragresize-bl {
 bottom: -8px;
 left: -8px;
 cursor: sw-resize;
}
.dragresize-bm {
 bottom: -8px;
 left: 50%;
 margin-left: -4px;
 cursor: s-resize;
}
.dragresize-br {
 bottom: -8px;
 right: -8px;
 cursor: se-resize;
}

</style>

<script type="text/javascript">
//<![CDATA[

    // Using DragResize is simple!
    // You first declare a new DragResize() object, passing its own name and an object
    // whose keys constitute optional parameters/settings:
    var activePanel;

    function activatePanel(panel) {
        activePanel = panel.id;
    }

    var dragresize = new DragResize('dragresize',
 { minWidth: 10, minHeight: 10, minLeft: 20, minTop: 20, maxLeft: 900, maxTop: 800 });

    // Optional settings/properties of the DragResize object are:
    //  enabled: Toggle whether the object is active.
    //  handles[]: An array of drag handles to use (see the .JS file).
    //  minWidth, minHeight: Minimum size to which elements are resized (in pixels).
    //  minLeft, maxLeft, minTop, maxTop: Bounding box (in pixels).

    // Next, you must define two functions, isElement and isHandle. These are passed
    // a given DOM element, and must "return true" if the element in question is a
    // draggable element or draggable handle. Here, I'm checking for the CSS classname
    // of the elements, but you have have any combination of conditions you like:

    dragresize.isElement = function (elm) {
        if (elm.className && elm.className.indexOf('drsElement') > -1) return true;
    };
    dragresize.isHandle = function (elm) {
        if (elm.className && elm.className.indexOf('drsMoveHandle') > -1) return true;
    };

    // You can define optional functions that are called as elements are dragged/resized.
    // Some are passed true if the source event was a resize, or false if it's a drag.
    // The focus/blur events are called as handles are added/removed from an object,
    // and the others are called as users drag, move and release the object's handles.
    // You might use these to examine the properties of the DragResize object to sync
    // other page elements, etc.

    dragresize.ondragfocus = function () { };
    dragresize.ondragstart = function (isResize) { };
    dragresize.ondragmove = function (isResize) {
        document.getElementById("posX").value = document.getElementById(activePanel).style.left;
        document.getElementById("posY").value = document.getElementById(activePanel).style.top;
        document.getElementById("txtW").value = document.getElementById(activePanel).style.width;
        document.getElementById("txtH").value = document.getElementById(activePanel).style.height;

    };
    dragresize.ondragend = function (isResize) { };
    dragresize.ondragblur = function () { };

    // Finally, you must apply() your DragResize object to a DOM node; all children of this
    // node will then be made draggable. Here, I'm applying to the entire document.
    dragresize.apply(document);

//]]>

    function setValues() {
//    try{
        var i = 1;
        var values = "";
        var counter = document.getElementById("hdnCounter").value;
        //alert(counter);
        for (i = 1; i <= parseInt(counter); i++) {
            
            //alert("per" + String(i));
            var obj = "per" + String(i);
            if (document.getElementById(obj) != null) {
                values = values + "per" + String(i) + ":";
                values = values + document.getElementById(obj).style.left.toString() + ",";
                values = values + document.getElementById(obj).style.top.toString() + ",";
                values = values + document.getElementById(obj).style.width.toString() + ",";
                values = values + document.getElementById(obj).style.height.toString() + ";";
                
            }
            else {
                //alert(obj);
            }

        }
        document.getElementById("hdnValues").value = values;
    }
</script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="10" cellspacing="0">
        <tr>
            <td class="inner_header">
             <%= (Session["lang"].ToString() == "0") ? "Template" : "القالب"%>   
                </td>
            <td colspan="3">
               <%= (Session["lang"].ToString() == "0") ? "Document Type" : "نوع المستند"%> 
                 
                        <asp:DropDownList ID="drpDocTypes" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="drpDocTypes_SelectedIndexChanged">
                        </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; 
                    <%= (Session["lang"].ToString() == "0") ? " Image Template :" : "صورة القالب"%>   
                   
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:ImageButton ID="ImageButton3" runat="server" 
                            ImageUrl="~/Images/Icons/Actions-go-up-icon.png" 
                    onclick="ImageButton3_Click" />
            </td>
        </tr>
        <tr>
            <td class="inner_header">
            
            <%= (Session["lang"].ToString() == "0") ? "Group" : "المجموعة"%> 
    
                </td>
            <td>
                        <asp:DropDownList ID="drpGrpID" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="drpGrpID_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
            <td class="inner_header">
            <%= (Session["lang"].ToString() == "0") ? "Blocks" : "المساحات"%> 
            </td>
            <td>
                 <asp:Button ID="Button1" runat="server" Text="Create Panel" 
        onclick="Button1_Click" OnClientClick="setValues();"  />
    &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save Blocks" 
        onclick="btnSave_Click" OnClientClick="setValues();"  />
                &nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
        Text="Load Blocks" 
                    OnClientClick="javascript:document.getElementById('hdnValues').value='';" 
                    Visible="False" />
            </td>
        </tr>
        
   
            
    </table>
    <div style="display:none">
    <br />
    X : <asp:TextBox ID="posX" runat="server"></asp:TextBox>
    Y : <asp:TextBox ID="posY" runat="server"></asp:TextBox><br />
    W : <asp:TextBox ID="txtW" runat="server"></asp:TextBox>
    H : <asp:TextBox ID="txtH" runat="server"></asp:TextBox>
    
    
    <br />
    </div>
    <asp:HiddenField runat="server" ID="hdnCounter" Value="0" />
    <asp:HiddenField runat="server" ID="hdnValues" Value="" />
    <asp:Panel runat="server" ID="pnlMain">
        <asp:Image ID="Image1" runat="server" ImageUrl="../Images/Templates/1.jpg" />

    <%--<asp:panel runat="server" class="drsElement drsMoveHandle" id="per1" onmousedown="activatePanel(this)"
 style="left: 150px; top: 280px; width: 50px; height: 100px;
 background: #DFC; text-align: center">
 
</asp:panel>
<asp:panel runat="server" class="drsElement drsMoveHandle" id="per2" onmousedown="activatePanel(this)"
 style="left: 150px; top: 280px; width: 50px; height: 100px;
 background: #DFC; text-align: center">
 
</asp:panel>--%>
    </asp:Panel>
   </asp:Content>