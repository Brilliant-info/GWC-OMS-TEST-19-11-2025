<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DecryptPassword.aspx.cs" Inherits="PowerOnRentwebapp.UserManagement.DecryptPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td> Enter Encrypted Password :
                        <asp:TextBox ID="txtencryptpass" runat="server" Text=""></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td> Decrypted Password :
                        <asp:Label ID="lbldecryptpass" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btndecript" runat="server" Text="Decrypt Password" OnClick="btndecript_Click" />
                    </td>
                </tr>
            
            </table>
                <table>
                <tr>
                    <td> Enter Plain Password :
                        <asp:TextBox ID="txtplainstring" runat="server" Text=""></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td> Encrypted Password :
                        <asp:Label ID="lblencryptpass" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnencrypt" runat="server" Text="Encrypt Password" OnClick="btnencrypt_Click" />
                    </td>
                </tr>
            
            </table>
        </div>
    </form>
</body>
</html>
