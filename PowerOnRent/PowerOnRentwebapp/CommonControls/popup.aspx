<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popup.aspx.cs" Inherits="BrilliantWMS.CommonControls.popup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function closePopup()
        {
            self.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField runat="server" ID="hdnObject" ClientIDMode="Static" />
        </div>
    </form>
</body>
</html>
