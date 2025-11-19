<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/CRM2.Master" CodeBehind="CancelOrderConfirm.aspx.cs"
     Inherits="PowerOnRentwebapp.PowerOnRent.CancelOrderConfirm"  Theme="Blue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc1" %>
<%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <uc1:UCFormHeader ID="UCFormHeader1" runat="server" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
         <div>
            <br />
            
            
           <%-- <div style="text-align:center">
                <h4>Cancel Order Reason</h4>                
            </div>--%>
            <table class="tableForm">
                <tr>
                    <td> </td>                
                           
                    <td style="text-align:left">
                        <%--Reason :--%>  
                          <asp:Label ID="lblreason" Text="Reason" runat="server"></asp:Label> 
                    </td>

                    <td style="text-align:left">
                         <asp:DropDownList ID="ddlreason" runat="server" Width="200px"></asp:DropDownList>
                    </td>

                    <td style="text-align:center">
                          <input type="button" id="btncancelorder" runat="server" value="Cancel Order"  onclick="CancelOrder()"/>
                    </td>
                        <td style=""></td>
                </tr>
            </table>
                   
        </div>
        </center>

       <asp:HiddenField ID="hdnselectvalue" runat="server" ClientIDMode="Static" />
     <script type="text/javascript">
        
         function CancelOrder()
         {
            var orders = document.getElementById("<%=hdnselectvalue.ClientID %>")
            var hdnselectvalue = orders.value;
            var reasoncode = document.getElementById("<%=ddlreason.ClientID %>").value;
            if (reasoncode != "--Select--")
            {
                PageMethods.WMCancelOrder(hdnselectvalue, reasoncode, OnSuccessCancelOrder, null);
            }
            else
            {
                showAlert('Please select reason!!!', 'Error', '#');
            }
        }

         function OnSuccessCancelOrder(result)
         {
             if (result != 0)
             {
                window.opener.AfterAssignDriver();
                window.close('../PowerOnRent/CancelOrderConfirm.aspx');
             }
            else
            {
                showAlert('Not Applicable', 'Error', '#');
            }
               // self.close();               
        }
        //function OnSuccessCancelOrder(result) {
        //    if (result == 0) {
        //        showAlert("Not Applicable", '', '#');
        //        window.opener.AfterAssignDriver.refresh();
        //        self.close();
        //    } else {
        //        showAlert('Selected Order Is Cancelled!!!', 'Error', '#');
        //        window.opener.AfterAssignDriver.refresh();
        //        self.close();
        //    }
        //}
        
        //var myTimer;
        //function showAlert(msg, msytype, formpath) {
        //    var newdiv = document.createElement('div');
        //    newdiv.id = "divAlerts";
        //    newdiv.className = "alertBox0";

        //    var newdiv2 = document.createElement('div');
        //    msytype = msytype.toString().toLowerCase();
        //    switch (msytype) {
        //        case "info":
        //            newdiv2.style.borderTopColor = "green";
        //            break;
        //        case "error":
        //            newdiv2.style.borderTopColor = "red";
        //            break;
        //        case "p": /*Process*/
        //            newdiv2.style.borderTopColor = "navy";
        //            break;
        //        case "":
        //            newdiv2.style.borderTopColor = "orange";
        //            break;
        //    }
        //    var span1 = document.createElement('span');
        //    span1.id = 'alertmsg';
        //    newdiv2.appendChild(span1);
        //    newdiv.appendChild(newdiv2);
        //    //var strdiv = "<div id='divAlerts' class='alertBox0'><div><span id='alertmsg'></span></div></div>"
        //    document.body.appendChild(newdiv);
        //    document.getElementById('alertmsg').innerHTML = msg;
        //    document.getElementById('divAlerts').className = "alertBox1";
        //    myTimer = self.setInterval(function () { msgclock(formpath) }, 3500);
        //}

    </script>
    </asp:Content>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager12" runat="server" EnablePageMethods="true" />
       
     
    </form>
   
</body>

    

</html>--%>
