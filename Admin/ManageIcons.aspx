<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ManageIcons.aspx.cs" Inherits="dms.Admin.ManageIcons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" runat="server">
        <img align="absmiddle" alt="" class="style1" 
            src="../Images/Icons/Files-Upload-File-icon32.png" />
        <%= (Session["lang"].ToString() == "0") ? "Manage Icons" : "ادارة الايقونات"%>    
                </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderRightBar" runat="server">
    <b> <%= (Session["lang"].ToString() == "0") ? "Added Icons:" : "الايقوانات المضافة"%> </b><br /><br />
    <asp:GridView ID="grdIcons" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" GridLines="None" 
        onselectedindexchanged="grdIcons_SelectedIndexChanged" 
        ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="iconID" HeaderText="ID" />

            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ID="Image2" runat="server" 
                        ImageUrl='<%# "../Images/"+ Session["clientId"].ToString() + "/dbIcons/" + Eval("iconID").ToString() + "-16.png" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="iconDescription" HeaderText="Icon Name" />
            <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
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
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <table>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Image ID="Image1" runat="server" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Icon ID"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtIconID" runat="server" Width="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Icon Description"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtIconDescription" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><%= (Session["lang"].ToString() == "0") ? "Small Image File (16 px)" : "صورة ايقونة صغيرة - 15بكسل"%> </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td><%= (Session["lang"].ToString() == "0") ? "Large Image File (32 px)" : "صورة ايقونة كبيرة - 32بكسل"%></td>
            <td>
                <asp:FileUpload ID="FileUpload2" runat="server" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
           <asp:RadioButtonList ID="rdoSaveMethod" runat="server" 
               RepeatDirection="Horizontal">
               <asp:ListItem Selected="True" Value="0">New Icon</asp:ListItem>
               <asp:ListItem Value="1">Exsit Icon</asp:ListItem>
           </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">
                <img src="../Images/Icons/action-save-icon.png" border="0" align="absmiddle" />
                <%= (Session["lang"].ToString() == "0") ? "Save" : "حفظ"%></asp:LinkButton>
&nbsp;<asp:Label ID="lblRes" runat="server" ForeColor="#CC0000"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
