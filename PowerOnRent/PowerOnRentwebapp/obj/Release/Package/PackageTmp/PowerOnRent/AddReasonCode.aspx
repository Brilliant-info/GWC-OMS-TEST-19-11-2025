<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddReasonCode.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.AddReasonCode" %>

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
            <td>
                Customer:
            </td>
            <td>

            </td>
        </tr>
         <tr>
            <td>
            Rejection <asp:TextBox Id="txtreject" runat="server" ></asp:TextBox>
            </td>
            <td>
           Cancel  <asp:TextBox Id="txtcancel" runat="server" ></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td>
              <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
            </td>
            <td>

            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
