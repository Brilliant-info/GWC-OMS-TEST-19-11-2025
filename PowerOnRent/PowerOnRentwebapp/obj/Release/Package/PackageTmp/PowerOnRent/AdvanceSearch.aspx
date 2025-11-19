<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvanceSearch.aspx.cs" 
    MasterPageFile="~/MasterPage/CRM2.Master"
    Inherits="PowerOnRentwebapp.PowerOnRent.AdvanceSearch" Theme="Blue" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 

   
     <%--   <asp:ScriptManager ID="ScriptManager12" runat="server" EnablePageMethods="true" />--%>
        <div>

            <table class="tableForm" width="100%">
                <tr>
                    <td style="width: 5%"></td>

                    <td style="width: 13%">
                        <asp:Label ID="lblfdate" runat="server" Text="From Date"></asp:Label>
                    </td>
                    <td style="width: 17%; text-align: left">

                        <%--<asp:TextBox ID="txtfromdate11" runat="server"  Width="200px"></asp:TextBox>--%>
                        <uc1:UC_Date ID="UC_fromDate" runat="server" />
                    </td>


                    <td style="width: 13%">
                        <asp:Label ID="lbltdate" runat="server" Text="To Date"></asp:Label>
                    </td>
                    <td style="width: 17%; text-align: left">

                        <uc1:UC_Date ID="UC_Todate" runat="server" />
                    </td>

                    <td style="width: 13%">
                        <asp:Label ID="lblordcat" runat="server" Text="Order Type"></asp:Label>
                    </td>
                    <td style="width: 17%; text-align: left">
                        <asp:DropDownList ID="ddlordercategory" runat="server" Width="205px" ClientIDMode="Static">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="PreOrder" Value="PreOrder"></asp:ListItem>
                            <asp:ListItem Text="NormalOrder" Value="NormalOrder"></asp:ListItem>
                           
                        </asp:DropDownList>
                    </td>

                    <td style="width: 5%"></td>
                </tr>


                <tr>
                    <td style="width: 5%"></td>

                    <td style="width: 13%">
                        <asp:Label ID="lblordno" runat="server" Text="Order No"></asp:Label>
                    </td>
                    <td style="width: 17%; text-align: left">
                        <asp:TextBox ID="txtorderno" runat="server" Width="200px"></asp:TextBox>
                    </td>


                    <td style="width: 13%">
                        <asp:Label ID="lbllcode" runat="server" Text="Location Code"></asp:Label>
                    </td>
                    <td style="width: 17%; text-align: left">
                        <asp:TextBox ID="txtlocationcode" runat="server" Width="200px"></asp:TextBox>
                    </td>

                    <td style="width: 13%">
                        <asp:Label ID="lblpass" runat="server" Text="Passport/QID"></asp:Label>
                    </td>
                    <td style="width: 17%; text-align: left">
                        <asp:TextBox ID="txtPassqid" runat="server" Width="200px"></asp:TextBox>
                    </td>

                    <td style="width: 5%"></td>
                </tr>


                <tr>
                    <td style="width: 5%"></td>

                    <td style="width: 13%">
                        <asp:Label ID="lblordtype" runat="server" Text="Order Category"></asp:Label>
                    </td>
                    <td style="width: 17%; text-align: left">
                        <asp:DropDownList ID="ddlordertype" runat="server" Width="205px" ClientIDMode="Static">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Prepaid" Value="Prepaid"></asp:ListItem>
                            <asp:ListItem Text="Postpaid" Value="Postpaid"></asp:ListItem>
                            <asp:ListItem Text="SIM Only" Value="SIM Only"></asp:ListItem>
                             <asp:ListItem Text="Fixed Only" Value="Fixed Only"></asp:ListItem>
                            <asp:ListItem Text="FMS Indoor" Value="FMS Indoor"></asp:ListItem>
                             <asp:ListItem Text="FMS Outdoor" Value="FMS Outdoor"></asp:ListItem>
                        </asp:DropDownList>

                    </td>


                    <td style="width: 13%">
                        <asp:Label ID="lblsimseril" runat="server" Text="MSISDN "></asp:Label>
                    </td>
                    <td style="width: 17%; text-align: left">
                        <asp:TextBox ID="txtmssidn" runat="server" Width="200px"></asp:TextBox>
                    </td>


                    <td style="width: 13%">

                              <asp:Label ID="lblemail" runat="server" Text="Email"></asp:Label>

                        <%--  <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
                            <asp:Button ID="btnclose" runat="server" Text="Cancel" OnClick="btnclose_Click" />--%>
                           
                    </td>
                    <td style="width: 17%; text-align: left"">
                          <asp:TextBox ID="txtemail" runat="server" Width="200px"></asp:TextBox>

                    </td>

                    <td style="width: 5%"></td>
                </tr>

             

                   <tr>
                     <td style="width: 5%"></td>

                      <td style="width: 13%">
                                <asp:Label ID="lblpayment" runat="server" Text="Payment Type "></asp:Label>
                      </td>
                     <td style="width: 17%; text-align: left">
                         <asp:DropDownList ID="ddlpaymenttype" runat="server"  Width="205px" ClientIDMode="Static" >
                                  <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                             <asp:ListItem Text="Card" Value="7"></asp:ListItem>
                               <asp:ListItem Text="COD" Value="3"></asp:ListItem>
                         </asp:DropDownList>
                         </td>

                    
                        <td style="width: 13%">
                                <asp:Label ID="lblSemerial" runat="server" Text="SIM Serial No."></asp:Label>
                      </td>
                           <td style="width: 17%; text-align: left">
                                <asp:TextBox ID="txtsemserial" runat="server" Width="200px"></asp:TextBox>
                               </td>


                     <td style="width: 13%"></td>
                     <td style="width: 17%; text-align: left">
                         
                         <input type="button" id="btnsubmitdata" value="Submit" onclick="AdvanceSearchData()" runat="server" />

                        <input type="button" id="btncanceldata" value="Cancel" onclick="CloseWindow()" runat="server" />

                         </td>

                     <td style="width: 5%"></td>
                </tr>

            </table>


            <br />

            <asp:HiddenField ID="hdnFromDate" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnTodate" runat="server" ClientIDMode="Static" />
        </div>
   
<script type="text/javascript">
    function AdvanceSearchData() {

         var fdate =  getDateFromUC("<%=UC_fromDate.ClientID %>");
       var tdate = getDateFromUC("<%=UC_Todate.ClientID %>"); 
       
        var hdnFrmdate = window.opener.document.getElementById("hdnFrmdate");
        var hdnTdate = window.opener.document.getElementById("hdnTdate");
        var hdnOrdcategory = window.opener.document.getElementById("hdnOrdcategory");
        var hdnOrderno = window.opener.document.getElementById("hdnOrderno");
        var hdnLocation = window.opener.document.getElementById("hdnLocation");
        var hdnPassportno = window.opener.document.getElementById("hdnPassportno");
        var hdnOrdtype = window.opener.document.getElementById("hdnOrdtype");
        var hdnMisidn = window.opener.document.getElementById("hdnMisidn");
        var hdnemail = window.opener.document.getElementById("hdnemail");
        var hdnpaytype = window.opener.document.getElementById("hdnpaytype");
        var hdnSimserial = window.opener.document.getElementById("hdnSimserial");
      

       hdnFrmdate =  getDateFromUC("<%=UC_fromDate.ClientID %>");
       hdnTdate = getDateFromUC("<%=UC_Todate.ClientID %>");      

            var e = document.getElementById("ddlordercategory");
            hdnOrdcategory = e.options[e.selectedIndex].value;
            // hdnOrdcategory =strUser.v;

            hdnOrderno = document.getElementById("<%= txtorderno.ClientID %>");
            hdnLocation = document.getElementById("<%= txtlocationcode.ClientID %>");
            hdnPassportno = document.getElementById("<%= txtPassqid.ClientID %>");
            hdnSimserial= document.getElementById("<%= txtsemserial.ClientID %>");
            var e1 = document.getElementById("ddlordertype");
            hdnOrdtype = e1.options[e1.selectedIndex].value;

            var e2 = document.getElementById("ddlpaymenttype");
            hdnpaytype = e2.options[e2.selectedIndex].value;

            //hdnOrdtype = Ordtype.value;

        <%--    hdnFrmdate = document.getElementById("<%= UC_fromDate.ClientID %>");--%>

        hdnMisidn = document.getElementById("<%= txtmssidn.ClientID %>");
        hdnemail=document.getElementById("<%= txtemail.ClientID %>");
            var asvalue = "AdvanceSearch";
            window.opener.AfterAdvanceSearchAllValue(asvalue, hdnFrmdate, hdnTdate, hdnOrdcategory, hdnOrderno.value, hdnLocation.value, hdnPassportno.value, hdnOrdtype, hdnMisidn.value, hdnemail.value, hdnpaytype, hdnSimserial.value);
            self.close();
        }

        function CloseWindow() {
            self.close();
        }
</script>
</asp:Content>