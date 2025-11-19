<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="UpdateStockInBulk.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.UpdateStockInBulk" Theme="Blue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">


    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
     <div>
            <table>
                <tr>
                    <td> Enter Top Record:
                        <asp:TextBox ID="txttoprecord" runat="server" ></asp:TextBox>
                    </td>
                </tr>
                 
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnudpate" runat="server" Text="Updatestock" OnClick="btnudpate_Click" />
                    </td>
                </tr>
            
            </table>
        </div>
</asp:Content>
