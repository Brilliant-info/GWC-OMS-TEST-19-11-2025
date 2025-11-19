<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocDownloadWindow.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.DocDownloadWindow" Theme="Blue" %>


<%@ Register Src="../Document/UC_AttachDocument.ascx" TagName="UC_AttachDocument" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" style="background-color: White;" runat="server">

           <asp:ScriptManager ID="ScriptManager12" runat="server" EnablePageMethods="true" />

    <div>
      
         <asp:Panel runat="server" ID="panelconfirmwindow" Visible="false">
             <br />
             <table width="100%">
                 <tr>
                     <td style="width:20%"></td>
                     <td style="width:60%">
                         <center>
                               <table class="tableForm">
                  <tr>
                      <td colspan="2" style="text-align:center">
                             <asp:Label ID="lblmsg" runat="server" ></asp:Label> 
                      </td>  
            </tr>
            <tr>    
                <td style="text-align:right">
                    <br />
                     <input type="button" id="btnCompleted" runat="server" value="Complete"  onclick="Completed()"/>
                </td>                
                <td style="width:50%;text-align:left">
                     <br />
                       <input type="button" id="btncancel" runat="server" value="Cancel"  onclick="CloseWindow()"/>
                </td>
            </tr>
        </table>
                             </center>
                     </td>
                     <td style="width:20%"></td>
                 </tr>
             </table>
          
                </asp:Panel>

          <asp:Panel runat="server" ID="paneldocument" Visible="true">
              <table>
            <tr>
                <td>
                     <div class="divDetailExpand" id="divDocumentDetails" runat="server" clientidmode="Static">
                      <uc3:UC_AttachDocument ID="UC_AttachmentDocument1" runat="server" />
        </div>
                </td>
            </tr>
             <tr>
                <td style="text-align:right">
                        <%--<input type="button" runat="server" value="Close Order" id="btnDocDownload" onclick="CompleteOrder();" />--%>
                    <asp:Button ID="btncloseorder" runat="server" Text="Close Order" OnClick="btncloseorder_Click" />
                    
                </td>
            </tr>
        </table>
            </asp:Panel>

             <asp:HiddenField ID="hndSelectedRec" runat="server" ClientIDMode="Static" />
     <asp:HiddenField ID="hndSelectedRecSku" runat="server" ClientIDMode="Static" />

         <%-- <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
         <asp:HiddenField ID="HiddenField2" runat="server" ClientIDMode="Static" />--%>
    </div>
    </form>

      <script type="text/javascript">
          var OrderNo = hndSelectedRec.value;
          var Skucode = hndSelectedRecSku.value;
          function CompleteOrder() {
              PageMethods.CompleteOrder(OrderNo, Skucode, OnSuccessCompleteOrder, null);
          }
          function OnSuccessCompleteOrder(result) {
              if (result == "No") {
                  showAlert('This Order Already Closed...', 'Error', '#');
              }
              else {
                  window.open('../PowerOnRent/CloseOrder.aspx?OrdNo=' + OrderNo + '&Skucode=' + Skucode + '&ST=24&APL=0', null, 'height=80px, width=595px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
              }
          }

          function AfterCompleteOrder() {
              GridDispatchstatus.reload();
          }

          var myTimer;
          function showAlert(msg, msytype, formpath) {
              var newdiv = document.createElement('div');
              newdiv.id = "divAlerts";
              newdiv.className = "alertBox0";

              var newdiv2 = document.createElement('div');
              msytype = msytype.toString().toLowerCase();
              switch (msytype) {
                  case "info":
                      newdiv2.style.borderTopColor = "green";
                      break;
                  case "error":
                      newdiv2.style.borderTopColor = "red";
                      break;
                  case "p": /*Process*/
                      newdiv2.style.borderTopColor = "navy";
                      break;
                  case "":
                      newdiv2.style.borderTopColor = "orange";
                      break;
              }
              var span1 = document.createElement('span');
              span1.id = 'alertmsg';
              newdiv2.appendChild(span1);
              newdiv.appendChild(newdiv2);
              //var strdiv = "<div id='divAlerts' class='alertBox0'><div><span id='alertmsg'></span></div></div>"
              document.body.appendChild(newdiv);
              document.getElementById('alertmsg').innerHTML = msg;
              document.getElementById('divAlerts').className = "alertBox1";
              myTimer = self.setInterval(function () { msgclock(formpath) }, 3500);
          }

         </script>

      <script type="text/javascript">
          function Completed() {
                var hndSelectedRec= document.getElementById("<%=hndSelectedRec.ClientID %>");
    var hndSelectedRecSku= document.getElementById("<%=hndSelectedRecSku.ClientID %>");
            var OrderId = hndSelectedRec.value;
            var Skucode = hndSelectedRecSku.value;
            document.getElementById("btnCompleted").style.visibility = "hidden";
            PageMethods.CompletedOrder(OrderId, Skucode, OnSuccessCompletedDocUpload, null)
        }
        function OnSuccessCompletedDocUpload(result) {
            if (result == "true") {
                window.opener.GridDispatchstatus.refresh();
                self.close();
            }
        }

            function CloseWindow() {
                window.close();
            }

        
</script>
</body>
</html>
