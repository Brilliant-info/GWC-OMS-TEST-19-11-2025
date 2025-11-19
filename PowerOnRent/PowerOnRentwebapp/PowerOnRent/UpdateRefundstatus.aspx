<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM2.Master" AutoEventWireup="true" CodeBehind="UpdateRefundstatus.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.UpdateRefundstatus" Theme="Blue" %>

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

         <br />
        <table class="tableForm">
         
            <tr>
                <td id="tdInvoiceNo" style="text-align:right;">
                    <asp:Label ID="lblInvoiceNo" runat="server" Text="Change Status"></asp:Label> :
                </td>
               
                <td style="text-align:left;">
                  <%-- <asp:DropDownList ID="ddlcngstatus" runat="server" Width="200px">
                          <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>
                        <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>                  
                   </asp:DropDownList>--%>
                  <asp:DropDownList ID="ddlstatus" runat="server" Width="200px" onchange="Changelable(this);"></asp:DropDownList>
                </td>
                <td style="text-align:left;">
                     <asp:Label ID="lblcode" runat="server" Text=""></asp:Label> :
                </td>
                <td style="text-align:left;">
                     <asp:TextBox ID="txtcode" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                </td>
                  <td style="text-align:left;">
                       <input type="button" id="btnSavestatus" runat="server" value="Submit" style="float: right;" onclick="jsSaveStatus()"/>
                      </td>
            </tr>          
        </table>
         <br />
         <br />
        </center>
         <asp:HiddenField ID="hndSelectedOid" runat="server" ClientIDMode="Static" />
   
      <script type="text/javascript">

          function Changelable(ddlstaus) {
              var Status = ddlstaus.options[ddlstaus.selectedIndex].text;
              if (Status == "Pending for Finance activity") {
                  document.getElementById("<%=lblcode.ClientID%>").innerHTML = "ASN No. ";
              }

          }

          function jsSaveStatus()
          {
             
              //var Statustext ="Customer Refunded";
              if (document.getElementById("<%=txtcode.ClientID%>").value != "") {
                  var e = document.getElementById("<%= ddlstatus.ClientID %>");
                  Statustext = e.options[e.selectedIndex].text;
                  var statusID = document.getElementById("<%=ddlstatus.ClientID %>").value;
                  var hndSelectedOid = document.getElementById("<%=hndSelectedOid.ClientID %>");
                  var codevalue = document.getElementById("<%=txtcode.ClientID%>").value;
                  var OrderId = hndSelectedOid.value;
                  PageMethods.WMUpdateRefundstatus(OrderId, statusID, Statustext, codevalue, jsupdaterefundOnSuccess, jsupdaterefundOnFail);
              }
              else {
                   alert("Please add Refund activity code.");
              }
          }

          function jsupdaterefundOnSuccess(result)
          {
              
              if (result == "True") {

                  window.opener.AfterAssignDriver();
                  self.close();
              }
              else {
                  alert("Error Occured");
                  self.close();
              }
                //window.open('../PowerOnRent/Default.aspx', '_self', '');
          }

          function jsupdaterefundOnFail()
          {
             alert("Error Occured");
             self.close();
          }
          // For Popup...
          //$('body').attr('onunload','refreshParent();');

          //function refreshParent()
          //{
          //    window.opener.refreshPage();

          //}


          </script>
</asp:Content>
