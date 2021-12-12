<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/subPages.Master" AutoEventWireup="true" CodeBehind="manageProfile.aspx.cs" Inherits="dms.Admin.manageProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="changPass" class="mangeProfileDiv">
        
        <table style="margin: 20px;width: 100%;">
            <tr>
                <td><%= (Session["lang"].ToString() == "0") ? "Old Password" : "كلمة السر القديمة"%></td>
                <td>
                    <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtOldPassword" ErrorMessage="Please enter the old Password"
                        ForeColor="#CC0000" ValidationGroup="passG"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><%= (Session["lang"].ToString() == "0") ? "New Password" : "كلمة السر الجديدة"%></td>
                <td>
                    <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtNewPassword" ErrorMessage="Please enter the new Password"
                        ForeColor="#CC0000" ValidationGroup="passG"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><%= (Session["lang"].ToString() == "0") ? "Confirm Password" : "تاكيد كلمة السر"%></td>
                <td>
                    <asp:TextBox ID="txtRePassword" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                        ControlToCompare="txtNewPassword" ControlToValidate="txtRePassword"
                        ErrorMessage="Passwords not matches" ForeColor="#CC0000"
                        ValidationGroup="passG"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align: right;">
                    <br />
                    <asp:LinkButton style="border-radius: 5px; background-color: #000; color: #f68b1e; min-height: 25px; padding: 2px 10px; cursor: pointer; -o-transition: .5s; -ms-transition: .5s; -moz-transition: .5s; -webkit-transition: .5s; transition: .5s;padding-top:10px;padding-bottom:10px" ID="lnkChangePass" runat="server" ValidationGroup="passG"
                        OnClick="lnkChangePass_Click">
                <img src="../Images/Icons/action-save-icon.png" align="absmiddle" border="0" />
                <%= (Session["lang"].ToString() == "0") ? "Save Changes" : "حفظ التغييرات"%></asp:LinkButton></td>
            </tr>

        </table>
    </div>
    
    <asp:Label ID="lblRes" runat="server" Text="" ForeColor="#CC0000" ClientIDMode="Static"></asp:Label>
</asp:Content>

