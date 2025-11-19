<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/CRM2.Master" CodeBehind="ChangeStatus.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.ChangeStatus"  Theme="Blue" %>

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
                   <asp:DropDownList ID="ddlcngstatus" runat="server" Width="200px" DataValueField="Value" DataTextField="Text">
                        <%--  <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>
                        <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>--%>
                    
                      
                   </asp:DropDownList>
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
          function jsSaveStatus()
          {
              var cngvalue;
              var e = document.getElementById("<%= ddlcngstatus.ClientID %>");;
              cngvalue = e.options[e.selectedIndex].value;
              var hndSelectedOid = document.getElementById("<%= hndSelectedOid.ClientID %>");          
              var OrderId = hndSelectedOid.value;
              PageMethods.WMChangeStatus(OrderId,cngvalue, jsSaveApprovalOnSuccess, jsSaveApprovalOnFail);
          }

          function jsSaveApprovalOnSuccess(result)
          {
              if(result=="true")
              {
                  window.opener.AfterAssignDriver();
                  self.close();
              }
                showAlert("Status Change successfully", "_self", "");
                window.open('../PowerOnRent/Default.aspx', '_self', '');
          }

          function jsSaveApprovalOnFail()
          {

          self.close();
          }
          // For Popup...
            $('body').attr('onunload','refreshParent();');

          function refreshParent()
          {
              window.opener.refreshPage();

            }
          </script>

   </asp:Content>