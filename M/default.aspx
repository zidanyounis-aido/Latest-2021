<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/mobile.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="dms.M._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        <img src="../Images/folders.png" style="vertical-align:middle" />
<%= (Session["lang"].ToString() == "0") ? "Folders" : "المجلدات"%>  
</h2>

    <asp:Repeater ID="rptFolders" runat="server">
        <HeaderTemplate>
            <div class="icons">
        </HeaderTemplate>
        <ItemTemplate>
            <div>
                <a href="../M/documentsList.aspx?fldrID=<%# c.encrypt(Eval("fldrID").ToString()) %>">
                    <img src="../Images/dbIcons/<%# Eval("iconID") %>-32.png" border="0" />
                    <br />
                    <%# (Session["lang"].ToString() == "0") ?  Eval("fldrName") :  Eval("fldrNameAr")%>
                    </a>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    
</asp:Content>
