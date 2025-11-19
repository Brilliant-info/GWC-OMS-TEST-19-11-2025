<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="CommonReport.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.CommonReport"
    Theme="Blue" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>

<%@ Register Src="UCCommonFilter.ascx" TagName="UCCommonFilter" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <center>
        <div id="divLoading" style="height: 71%; width: 50%; display: none; top: 40; left: 260px; background-color: transparent;"class="modal">
            <center>
                <img src="../App_Themes/Blue/img/ajax-loader.gif" alt="" style="top: 50%; margin-top: 22%" />
                <br />
                <br />
                <b>Please Wait...</b>
            </center>
        </div>
         <div class="divDetailExpandPopUpOff" id="divPopUp">VATPercent
            <center>
                <div class="popupClose" onclick="CloseShowReport()">
                </div>
                <div class="divDetailExpand" id="div1">
                    <iframe runat="server" id="iframePORRpt1" clientidmode="Static" src="#" width="80%"
                        style="border: none; height: 550px;"></iframe>
                </div>
            </center>
        </div>
        <table id="tblRptMenu">
            <tr id="normal" runat="server">
                <td rowspan="2" id="imgadt1" runat="server">
                    <a href="CommonReport.aspx?invoker=imgaudit">
                        <img alt="" src="../POR/Img/PrdRpt.jpg" />
                    </a>
                </td>
                <td style="text-align: left" id="imgadt2" runat="server">
                    <a href="CommonReport.aspx?invoker=imgaudit" style="font-weight: bold;" id="imgaudit"
                        runat="server">
                        <asp:Label ID="lblImageAuditTrails" runat="server" Text="Image Audit Trails"></asp:Label></a>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=sku">
                        <img alt="" src="../CommonControls/HomeSetupImg/ActivityManagement.jpg" /></a>
                </td>
              
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=partrequest" style="font-weight: bold;" id="partrequisition"
                        visible="false" runat="server">
                        <asp:Label ID="lblpartreport" runat="server" Text="Part Requisition Report"></asp:Label>
                    </a><a href="CommonReport.aspx?invoker=sku" style="font-weight: bold;" id="rptsku"
                        runat="server">
                        <asp:Label ID="lblskureport" runat="server" Text="SKU Report"></asp:Label>
                    </a>
                </td>




                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=SkuDetails">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=SkuDetails" style="font-weight: bold;" id="SkuDetails"
                        runat="server">
                        <asp:Label ID="lblSKUDetail" runat="server" Text="SKU Detail Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=BomDetail">
                        <img alt="" src="Img/Report2.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=BomDetail" style="font-weight: bold;" id="BomDetail"
                        runat="server">
                        <asp:Label ID="lblbomdetail" runat="server" Text="BOM Detail Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
           
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=order">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" /></a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=partreceipt" style="font-weight: bold;" id="partreceipt"
                        visible="false" runat="server">
                        <asp:Label ID="lblpartreceipt" runat="server" Text="Part Receipt Report"></asp:Label></a>
                    <a href="CommonReport.aspx?invoker=order" style="font-weight: bold;" id="rptorder"
                        runat="server">
                        <asp:Label ID="lblorderreport" runat="server" Text="Order List Report"></asp:Label></a>                 
                </td>
              

                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=orderdetail">
                        <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=orderdetail" style="font-weight: bold;" id="orderdetail"
                        runat="server">
                        <asp:Label ID="lblorderdetail" runat="server" Text="Order Detail Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=orderlead">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=orderlead" style="font-weight: bold;" id="orderlead"
                        runat="server">
                        <asp:Label ID="lblleadtime" runat="server" Text="Order Lead Time Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
                
                <td rowspan="2" id="usrrpt1" runat="server">
                    <a href="CommonReport.aspx?invoker=user">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" /></a>
                </td>
                <td id="usrrpt2" runat="server">
                    <a href="CommonReport.aspx?invoker=partissue" style="font-weight: bold;" id="partissue"
                        visible="false" runat="server">
                        <asp:Label ID="lblpartissuereport" runat="server" Text="Part Issue Report"></asp:Label></a>
                    <a href="CommonReport.aspx?invoker=user" style="font-weight: bold;" id="rptuser"
                        runat="server">
                        <asp:Label ID="lbluserreport" runat="server" Text="User Report"></asp:Label></a>
                </td>
                    <%---------%>
                <td rowspan ="2" id="odrdlryrpt1" runat="server">
                    <a href="CommonReport.aspx?invoker=orderdelivery">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="odrdlryrpt2" runat="server">
                    <a href ="CommonReport.aspx?invoker=orderdelivery" style="font-weight: bold;" id="rptorderdelivery" runat="server">
                    <asp:Label ID="lblorderdeliveryreport" runat="server" Text="Order Delivery Report"></asp:Label></a>
                </td>
                <td rowspan ="2" id="slarpt1" runat="server">
                    <a href="CommonReport.aspx?invoker=sla">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="slarpt2" runat="server">
                    <a href ="CommonReport.aspx?invoker=sla" style="font-weight: bold;" id="rptsla" runat="server">
                    <asp:Label ID="lblslareport" runat="server" Text="SLA Report"></asp:Label></a>
                </td>

                 <td rowspan ="2" id="tdvstr1" runat="server">
                    <a href="CommonReport.aspx?invoker=totaldeliveryvstotalrequest">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="tdvstr2" runat="server">
                    <a href ="CommonReport.aspx?invoker=totaldeliveryvstotalrequest" style="font-weight: bold;" id="A1" runat="server">
                    <asp:Label ID="lbltdtr" runat="server" Text="Total Delivery vs Total Request"></asp:Label></a>
                </td>
                    <%---------%>
                <%--New Report Added-------%>
                <td rowspan="2" id="tdlocrpt" runat="server">
                    <a href="CommonReport.aspx?invoker=location"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="tdvstr3" runat="server">
                    <a href ="CommonReport.aspx?invoker=location" style="font-weight: bold;" id="A2" runat="server">
                    <asp:Label ID="lblLocationRpt" runat="server" Text="Payment Detail Report"></asp:Label></a>
                </td>
                <%--New Report Added-28------%>
                <td rowspan="2" id="td1" runat="server">
                    <a href="CommonReport.aspx?invoker=deliverylogrpt"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td4" runat="server">
                    <a href ="CommonReport.aspx?invoker=deliverylogrpt" style="font-weight: bold;" id="A5" runat="server">
                    <asp:Label ID="lbldeliverylog" runat="server" Text="Delivery Log Report"></asp:Label></a>
                </td>
                  <%--New Report Added-28------%>
                 <td rowspan="2" id="td40" runat="server">
                    <a href="CommonReport.aspx?invoker=bulkrpt"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td41" runat="server">
                    <a href ="CommonReport.aspx?invoker=bulkrpt" style="font-weight: bold;" id="A29" runat="server">
                    <asp:Label ID="Label17" runat="server" Text="CAF & Delivery Note"></asp:Label></a>
                </td>

                 <%-- change by suraj khopade for RMS--%>
                 <td rowspan="2" id="td33" runat="server">
                    <a href="CommonReport.aspx?invoker=ReturnCollectionReport"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td34" runat="server">
                    <a href ="CommonReport.aspx?invoker=ReturnCollectionReport" style="font-weight: bold;" id="A26" runat="server">
                    <asp:Label ID="Label18" runat="server" Text="Collection Report"></asp:Label></a>
                </td>
                  <%-- change by suraj khopade--%>

                   <%-- change by suraj khopade--%>
                 <td rowspan="2" id="td35" runat="server">
                    <a href="CommonReport.aspx?invoker=ReceiptSummaryReport"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td36" runat="server">
                    <a href ="CommonReport.aspx?invoker=ReceiptSummaryReport" style="font-weight: bold;" id="A27" runat="server">
                    <asp:Label ID="Label19" runat="server" Text="Receipt Summary Report"></asp:Label></a>
                </td>
                  <%-- change by suraj khopade  for RMS--%>

            </tr>

             <tr>
                <td></td>
            </tr>
             <tr id="ecomm" runat="server">
               
                <td rowspan="2" id="tdecom1" runat="server">
                    <a href="CommonReport.aspx?invoker=ecommerce1"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td2" runat="server">
                    <a href ="CommonReport.aspx?invoker=ecommerce1" style="font-weight: bold;" id="A3" runat="server">
                    <asp:Label ID="lblEcommerceRpt1" runat="server" Text="E-Commerce Report1"></asp:Label></a>
                </td>




                 <%--New Report Added-------%>
                 <td rowspan="2" id="tdecom2" runat="server">
                    <a href="CommonReport.aspx?invoker=ecommerce2"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td3" runat="server">
                    <a href ="CommonReport.aspx?invoker=ecommerce2" style="font-weight: bold;" id="A4" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="E-Commerce Report2"></asp:Label></a>
                </td>


                

                 <td rowspan="2" id="tddelivery" runat="server">
                    <a href="CommonReport.aspx?invoker=deliverynote"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td6" runat="server">
                    <a href ="CommonReport.aspx?invoker=deliverynote" style="font-weight: bold;" id="A6" runat="server">
                    <asp:Label ID="lbldeliverynote" runat="server" Text="Delivery Note"></asp:Label></a>
                </td>

                 <%-- change by suraj khopade--%>
                 <td rowspan="2" id="td13" runat="server">
                    <a href="CommonReport.aspx?invoker=normalorderdeliverynote"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td32" runat="server">
                    <a href ="CommonReport.aspx?invoker=normalorderdeliverynote" style="font-weight: bold;" id="A11" runat="server">
                    <asp:Label ID="lblnormalorderdeliveryreport" runat="server" Text="Normal Order Delivery Note"></asp:Label></a>
                </td>
                  <%-- change by suraj khopade--%>

                <%-- change by suraj khopade--%>
                 <td rowspan="2" id="tdtransactionreport" runat="server">
                    <a href="CommonReport.aspx?invoker=Transaction"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td15" runat="server">
                    <a href ="CommonReport.aspx?invoker=Transaction" style="font-weight: bold;" id="A10" runat="server">
                    <asp:Label ID="lbltransactionreport" runat="server" Text="Transaction Report"></asp:Label></a>
                </td>
                  <%-- change by suraj khopade--%>

                 
                 <%-- change by suraj khopade for RMS--%>
                 <td rowspan="2" id="td37" runat="server">
                    <a href="CommonReport.aspx?invoker=ReturnCollectionReport"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td38" runat="server">
                    <a href ="CommonReport.aspx?invoker=ReturnCollectionReport" style="font-weight: bold;" id="A28" runat="server">
                    <asp:Label ID="Label39" runat="server" Text="Collection Report"></asp:Label></a>
                </td>
                  <%-- change by suraj khopade--%>

                   <%-- change by suraj khopade--%>
                 <td rowspan="2" id="td39" runat="server">
                    <a href="CommonReport.aspx?invoker=ReceiptSummaryReport"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td42" runat="server">
                    <a href ="CommonReport.aspx?invoker=ReceiptSummaryReport" style="font-weight: bold;" id="A30" runat="server">
                    <asp:Label ID="Label40" runat="server" Text="Receipt Summary Report"></asp:Label></a>
                </td>
                  <%-- change by suraj khopade  for RMS--%>

                
             <%--    <td rowspan ="2" id="Td13" runat="server">
                    <a href="CommonReport.aspx?invoker=orderdelivery">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="Td15" runat="server">
                    <a href ="CommonReport.aspx?invoker=orderdelivery" style="font-weight: bold;" id="A10" runat="server">
                    <asp:Label ID="lblordderpt" runat="server" Text="Order Delivery Report"></asp:Label></a>
                </td>
                
                  <td rowspan ="2" id="Td32" runat="server">
                    <a href="CommonReport.aspx?invoker=sla">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="Td33" runat="server">
                    <a href ="CommonReport.aspx?invoker=sla" style="font-weight: bold;" id="A11" runat="server">
                    <asp:Label ID="lblslaecomm" runat="server" Text="SLA Report"></asp:Label></a>
                </td>


                  <td rowspan ="2" id="td34" runat="server">
                    <a href="CommonReport.aspx?invoker=totaldeliveryvstotalrequest">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="td35" runat="server">
                    <a href ="CommonReport.aspx?invoker=totaldeliveryvstotalrequest" style="font-weight: bold;" id="A26" runat="server">
                    <asp:Label ID="lbltotvsreq" runat="server" Text="Total Delivery vs Total Request"></asp:Label></a>
                </td>


                 <td rowspan="2" id="td36" runat="server">
                    <a href="CommonReport.aspx?invoker=location"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td37" runat="server">
                    <a href ="CommonReport.aspx?invoker=location" style="font-weight: bold;" id="A27" runat="server">
                    <asp:Label ID="lbllocecomm" runat="server" Text="Payment Detail Report"></asp:Label></a>
                </td>

                 <td rowspan="2" id="td38" runat="server">
                    <a href="CommonReport.aspx?invoker=deliverylogrpt"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td39" runat="server">
                    <a href ="CommonReport.aspx?invoker=deliverylogrpt" style="font-weight: bold;" id="A28" runat="server">
                    <asp:Label ID="lbldeliverylogecom" runat="server" Text="Delivery Log Report"></asp:Label></a>
                </td>--%>

                 

            </tr>
            <tr>
                <td></td>
            </tr>
            <tr id="hmc" runat="server">
                <td rowspan="2" id="td5" runat="server">
                    <a href="CommonReport.aspx?invoker=avgtime"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td7" runat="server">
                    <a href ="CommonReport.aspx?invoker=avgtime" style="font-weight: bold;" id="A7" runat="server">
                    <asp:Label ID="lblavgdeliverytime" runat="server" Text="Delivery List Report"></asp:Label></a>
                </td>
                <td rowspan="2" id="td8" runat="server">
                    <a href="CommonReport.aspx?invoker=noofdelivery"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td9" runat="server">
                    <a href ="CommonReport.aspx?invoker=noofdelivery" style="font-weight: bold;" id="A8" runat="server">
                    <asp:Label ID="lblNoofdelivery" runat="server" Text="Delivery Detail Report"></asp:Label></a>
                </td>
                <td rowspan="2" id="td10" runat="server">
                    <a href="CommonReport.aspx?invoker=driverschedule"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td11" runat="server">
                    <a href ="CommonReport.aspx?invoker=driverschedule" style="font-weight: bold;" id="A9" runat="server">
                    <asp:Label ID="Label2" runat="server" Text="Driver Schedule Report"></asp:Label></a>
                </td>

                  <td colspan="22"></td>
            </tr>

             <tr>
                <td></td>
            </tr>

            <tr id="vfq" runat="server">
                 <%--<td rowspan="2" id="td12" runat="server">
                    <a href="CommonReport.aspx?invoker=qnbnstock"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td13" runat="server">
                    <a href ="CommonReport.aspx?invoker=qnbnstock" style="font-weight: bold;" id="A10" runat="server">
                    <asp:Label ID="lblqnbnstock" runat="server" Text="QNBN Stock Report"></asp:Label></a>
                </td>--%>
               <%-- <td rowspan="2" id="td14" runat="server">
                    <a href="CommonReport.aspx?invoker=qnbnorder"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td15" runat="server">
                    <a href ="CommonReport.aspx?invoker=qnbnorder" style="font-weight: bold;" id="A11" runat="server">
                    <asp:Label ID="lblqnbnorder" runat="server" Text="QNBN Order Report"></asp:Label></a>
                </td>--%>                
                <td rowspan="2" id="Td12" runat="server">
                    <a href="CommonReport.aspx?invoker=imgaudit">
                        <img alt="" src="../POR/Img/PrdRpt.jpg" />
                    </a>
                </td>
                <td style="text-align: left" id="Td14" runat="server">
                    <a href="CommonReport.aspx?invoker=imgaudit" style="font-weight: bold;" id="A12"
                        runat="server">
                        <asp:Label ID="Label3" runat="server" Text="Image Audit Trails"></asp:Label></a>
                </td>
               <%-- <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=sku">
                        <img alt="" src="../CommonControls/HomeSetupImg/ActivityManagement.jpg" /></a>
                </td>--%>
                <%--//old sku report--%>
                <%--<td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=partrequest" style="font-weight: bold;" id="partrequisition"
                        visible="false" runat="server">
                        <asp:Label ID="lblpartreport" runat="server" Text="Part Requisition Report"></asp:Label>
                    </a><a href="CommonReport.aspx?invoker=sku" style="font-weight: bold;" id="rptsku"
                        runat="server">
                        <asp:Label ID="lblskureport" runat="server" Text="SKU Report"></asp:Label>
                    </a>
                </td>--%>

                <td rowspan="2" id="td16" runat="server">
                    <a href="CommonReport.aspx?invoker=qnbnstock"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td17" runat="server">
                    <a href ="CommonReport.aspx?invoker=qnbnstock" style="font-weight: bold;" id="A13" runat="server">
                    <asp:Label ID="Label4" runat="server" Text="QNBN Stock Report"></asp:Label></a>
                </td>


                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=SkuDetails">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=SkuDetails" style="font-weight: bold;" id="A14"
                        runat="server">
                        <asp:Label ID="Label5" runat="server" Text="SKU Detail Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=BomDetail">
                        <img alt="" src="Img/Report2.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=BomDetail" style="font-weight: bold;" id="A15"
                        runat="server">
                        <asp:Label ID="Label6" runat="server" Text="BOM Detail Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
              
               <%-- <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=order">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" /></a>
                </td>--%>
               <%-- <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=partreceipt" style="font-weight: bold;" id="partreceipt"
                        visible="false" runat="server">
                        <asp:Label ID="lblpartreceipt" runat="server" Text="Part Receipt Report"></asp:Label></a>
                    <a href="CommonReport.aspx?invoker=order" style="font-weight: bold;" id="rptorder"
                        runat="server">
                        <asp:Label ID="lblorderreport" runat="server" Text="Order List Report"></asp:Label></a>                 
                </td>--%>
                 <td rowspan="2" id="td18" runat="server">
                    <a href="CommonReport.aspx?invoker=qnbnorder"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td19" runat="server">
                    <a href ="CommonReport.aspx?invoker=qnbnorder" style="font-weight: bold;" id="A16" runat="server">
                    <asp:Label ID="Label7" runat="server" Text="QNBN Order Report"></asp:Label></a>
                </td>

                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=orderdetail">
                        <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=orderdetail" style="font-weight: bold;" id="A17"
                        runat="server">
                        <asp:Label ID="Label8" runat="server" Text="Order Detail Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=orderlead">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=orderlead" style="font-weight: bold;" id="A18"
                        runat="server">
                        <asp:Label ID="Label9" runat="server" Text="Order Lead Time Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
              
                <td rowspan="2" id="Td20" runat="server">
                    <a href="CommonReport.aspx?invoker=user">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" /></a>
                </td>
                <td id="Td21" runat="server">
                    <a href="CommonReport.aspx?invoker=partissue" style="font-weight: bold;" id="A19"
                        visible="false" runat="server">
                        <asp:Label ID="Label10" runat="server" Text="Part Issue Report"></asp:Label></a>
                    <a href="CommonReport.aspx?invoker=user" style="font-weight: bold;" id="A20"
                        runat="server">
                        <asp:Label ID="Label11" runat="server" Text="User Report"></asp:Label></a>
                </td>
                    <%---------%>
                <td rowspan ="2" id="Td22" runat="server">
                    <a href="CommonReport.aspx?invoker=orderdelivery">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="Td23" runat="server">
                    <a href ="CommonReport.aspx?invoker=orderdelivery" style="font-weight: bold;" id="A21" runat="server">
                    <asp:Label ID="Label12" runat="server" Text="Order Delivery Report"></asp:Label></a>
                </td>
                <td rowspan ="2" id="Td24" runat="server">
                    <a href="CommonReport.aspx?invoker=sla">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="Td25" runat="server">
                    <a href ="CommonReport.aspx?invoker=sla" style="font-weight: bold;" id="A22" runat="server">
                    <asp:Label ID="Label13" runat="server" Text="SLA Report"></asp:Label></a>
                </td>

                 <td rowspan ="2" id="td26" runat="server">
                    <a href="CommonReport.aspx?invoker=totaldeliveryvstotalrequest">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="td27" runat="server">
                    <a href ="CommonReport.aspx?invoker=totaldeliveryvstotalrequest" style="font-weight: bold;" id="A23" runat="server">
                    <asp:Label ID="Label14" runat="server" Text="Total Delivery vs Total Request"></asp:Label></a>
                </td>
                    <%---------%>
                <%--New Report Added-------%>
                <td rowspan="2" id="td28" runat="server">
                    <a href="CommonReport.aspx?invoker=location"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td29" runat="server">
                    <a href ="CommonReport.aspx?invoker=location" style="font-weight: bold;" id="A24" runat="server">
                    <asp:Label ID="Label15" runat="server" Text="Payment Detail Report"></asp:Label></a>
                </td>
                <%--New Report Added-28------%>
                <td rowspan="2" id="td30" runat="server">
                    <a href="CommonReport.aspx?invoker=deliverylogrpt"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td31" runat="server">
                    <a href ="CommonReport.aspx?invoker=deliverylogrpt" style="font-weight: bold;" id="A25" runat="server">
                    <asp:Label ID="Label16" runat="server" Text="Delivery Log Report"></asp:Label></a>
                </td>






               
                <%--<td rowspan="2">
                    <a href="CommonReport.aspx?invoker=partconsumption">
                        <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=partconsumption" style="font-weight: bold;" id="partconsumption"
                        runat="server">Part Consumption Report</a>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=monthly">
                        <img alt="" src="../CommonControls/HomeSetupImg/my_reports.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=monthly" style="font-weight: bold;" id="monthly"
                        runat="server">PR-Report Monthly</a>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=weeklylst">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=weeklylst" style="font-weight: bold;" id="weeklylst"
                        runat="server">Weekly Analysis</a>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=consumabletracker">
                        <img alt="" src="../CommonControls/HomeSetupImg/report.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=consumabletracker" style="font-weight: bold;"
                        id="consumabletracker" runat="server">Consumable Tracker</a>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=productdtl">
                        <img alt="" src="../POR/Img/PrdRpt.jpg" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=productdtl" style="font-weight: bold;" id="productdtl"
                        runat="server">Product Report</a>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=transfer">
                        <img alt="" src="Img/Report2.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=transfer" style="font-weight: bold;" id="Transfer"
                        runat="server">Trnasfer Report</a>
                </td>

                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=asset">
                        <img alt="" src="Img/tool5.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=asset" style="font-weight: bold;" id="asset" runat="server">
                        Sitewise Asset & Equipment Report</a>
                </td>--%>
            </tr>

             <tr>
                <td></td>
            </tr>

             <tr id="vftechnical" runat="server">
                <td rowspan="2" id="Td46" runat="server">
                    <a href="CommonReport.aspx?invoker=imgaudit">
                        <img alt="" src="../POR/Img/PrdRpt.jpg" />
                    </a>
                </td>
                <td style="text-align: left" id="Td47" runat="server">
                    <a href="CommonReport.aspx?invoker=imgaudit" style="font-weight: bold;" id="A32"
                        runat="server">
                        <asp:Label ID="Label20" runat="server" Text="Image Audit Trails"></asp:Label></a>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=sku">
                        <img alt="" src="../CommonControls/HomeSetupImg/ActivityManagement.jpg" /></a>
                </td>
              
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=partrequest" style="font-weight: bold;" id="A33"
                        visible="false" runat="server">
                        <asp:Label ID="Label21" runat="server" Text="Part Requisition Report"></asp:Label>
                    </a><a href="CommonReport.aspx?invoker=sku" style="font-weight: bold;" id="A34"
                        runat="server">
                        <asp:Label ID="Label22" runat="server" Text="SKU Report"></asp:Label>
                    </a>
                </td>




                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=SkuDetails">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=SkuDetails" style="font-weight: bold;" id="A35"
                        runat="server">
                        <asp:Label ID="Label23" runat="server" Text="SKU Detail Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=BomDetail">
                        <img alt="" src="Img/Report2.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=BomDetail" style="font-weight: bold;" id="A36"
                        runat="server">
                        <asp:Label ID="Label24" runat="server" Text="BOM Detail Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
           
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=order">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" /></a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=partreceipt" style="font-weight: bold;" id="A37"
                        visible="false" runat="server">
                        <asp:Label ID="Label25" runat="server" Text="Part Receipt Report"></asp:Label></a>
                    <a href="CommonReport.aspx?invoker=order" style="font-weight: bold;" id="A38"
                        runat="server">
                        <asp:Label ID="Label26" runat="server" Text="Order List Report"></asp:Label></a>                 
                </td>
              

                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=orderdetail">
                        <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=orderdetail" style="font-weight: bold;" id="A39"
                        runat="server">
                        <asp:Label ID="Label27" runat="server" Text="Order Detail Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
                <td rowspan="2">
                    <a href="CommonReport.aspx?invoker=orderlead">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" />
                    </a>
                </td>
                <td style="text-align: left">
                    <a href="CommonReport.aspx?invoker=orderlead" style="font-weight: bold;" id="A40"
                        runat="server">
                        <asp:Label ID="Label28" runat="server" Text="Order Lead Time Report"></asp:Label></a>
                    <%--New Added--%>
                </td>
                
                <td rowspan="2" id="Td48" runat="server">
                    <a href="CommonReport.aspx?invoker=user">
                        <img alt="" src="../CommonControls/HomeSetupImg/reports.jpg" /></a>
                </td>
                <td id="Td49" runat="server">
                    <a href="CommonReport.aspx?invoker=partissue" style="font-weight: bold;" id="A41"
                        visible="false" runat="server">
                        <asp:Label ID="Label29" runat="server" Text="Part Issue Report"></asp:Label></a>
                    <a href="CommonReport.aspx?invoker=user" style="font-weight: bold;" id="A42"
                        runat="server">
                        <asp:Label ID="Label30" runat="server" Text="User Report"></asp:Label></a>
                </td>
                    <%---------%>
                <td rowspan ="2" id="Td50" runat="server">
                    <a href="CommonReport.aspx?invoker=orderdelivery">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="Td51" runat="server">
                    <a href ="CommonReport.aspx?invoker=orderdelivery" style="font-weight: bold;" id="A43" runat="server">
                    <asp:Label ID="Label31" runat="server" Text="Order Delivery Report"></asp:Label></a>
                </td>
                <td rowspan ="2" id="Td52" runat="server">
                    <a href="CommonReport.aspx?invoker=sla">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="Td53" runat="server">
                    <a href ="CommonReport.aspx?invoker=sla" style="font-weight: bold;" id="A44" runat="server">
                    <asp:Label ID="Label32" runat="server" Text="SLA Report"></asp:Label></a>
                </td>

                 <td rowspan ="2" id="td54" runat="server">
                    <a href="CommonReport.aspx?invoker=totaldeliveryvstotalrequest">
                         <img alt="" src="Img/Report4.png" />
                    </a>
                </td>
                <td style="text-align: left" id="td55" runat="server">
                    <a href ="CommonReport.aspx?invoker=totaldeliveryvstotalrequest" style="font-weight: bold;" id="A45" runat="server">
                    <asp:Label ID="Label33" runat="server" Text="Total Delivery vs Total Request"></asp:Label></a>
                </td>
                    <%---------%>
                <%--New Report Added-------%>
                <td rowspan="2" id="td56" runat="server">
                    <a href="CommonReport.aspx?invoker=location"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td57" runat="server">
                    <a href ="CommonReport.aspx?invoker=location" style="font-weight: bold;" id="A46" runat="server">
                    <asp:Label ID="Label34" runat="server" Text="Payment Detail Report"></asp:Label></a>
                </td>
                <%--New Report Added-28------%>
                <td rowspan="2" id="td58" runat="server">
                    <a href="CommonReport.aspx?invoker=deliverylogrpt"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td59" runat="server">
                    <a href ="CommonReport.aspx?invoker=deliverylogrpt" style="font-weight: bold;" id="A47" runat="server">
                    <asp:Label ID="Label35" runat="server" Text="Delivery Log Report"></asp:Label></a>
                </td>

                <td rowspan="2" id="td60" runat="server">
                    <a href="CommonReport.aspx?invoker=skutrack"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td61" runat="server">
                    <a href ="CommonReport.aspx?invoker=skutrack" style="font-weight: bold;" id="A48" runat="server">
                    <asp:Label ID="Label36" runat="server" Text="Sku Tracking Report"></asp:Label></a>
                </td>



                <td rowspan="2" id="td62" runat="server">
                    <a href="CommonReport.aspx?invoker=sitetrack"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td63" runat="server">
                    <a href ="CommonReport.aspx?invoker=sitetrack" style="font-weight: bold;" id="A49" runat="server">
                    <asp:Label ID="Label37" runat="server" Text="Site Tracking Report"></asp:Label></a>
                </td>


                <td rowspan="2" id="td64" runat="server">
                    <a href="CommonReport.aspx?invoker=depttrack"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td65" runat="server">
                    <a href ="CommonReport.aspx?invoker=depttrack" style="font-weight: bold;" id="A50" runat="server">
                    <asp:Label ID="Label38" runat="server" Text="Department Tracking Report"></asp:Label></a>
                </td>

              <%-- <td rowspan="2" id="td66" runat="server">
                    <a href="CommonReport.aspx?invoker=vat"><img alt="" src="Img/Report4.png"/>
                    </a>
                </td>
                <td style="text-align: left" id="td67" runat="server">
                    <a href ="CommonReport.aspx?invoker=vat" style="font-weight: bold;" id="A10" runat="server">
                    <asp:Label ID="Label18" runat="server" Text="vat"></asp:Label></a>
                </td>--%>
               
            </tr>
                 
           

        </table>
    </center>
    <center>
        <table>
            <tr style="font-size: large">
                <td>
                    <%--Report Type :--%>
                    <asp:Label ID="lblReportType" runat="server" Text="Report Type"></asp:Label>
                    :
                    <asp:Label ID="lblRptName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:UCCommonFilter ID="UCCommonFilter1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<div id="rptbutton" runat="server" style="padding: 0 0 0 67%;">--%>
                    <div id="rptbutton" runat="server" style="padding: 0 0 0 67%;">
                     <input type="button" runat="server" visible="false" value="Ecommerce Dump Data" id="btnexport" onclick="ExportData();" />
                        <input type="button" runat="server" value="View Report" id="btnViewReport" onclick="selectedProductRec(); jsGetReportData();" />
                        <input type="button" runat="server" value="SKU Transaction Report" id="btnSKUTransaction" onclick="selectedProductRec(); jsGetSKUTransactionReport();" />
                        <input type="button" runat="server" value="User Transaction Report" id="btnusertransaction" onclick="selectedProductRec(); jsGetUserTransactionReport();" />

                         <%--  change by suraj khopade--%>
                         <%-- <input type="button" runat="server" visible="false" value="Export To PDF" id="btnExportToPDF" onclick="jsGetTransactionExportDatatoPDF();" />--%><%--selectedProductRec();--%>
                          <%--<input type="button" runat="server" visible="false" value="Export To EXCEL" id="btnExportToEXCEL" onclick=" jsGetTransactionExportDatatoEXCEL();" />--%><%--selectedProductRec();--%>
                          <%--  change by suraj khopade--%>
                    </div>

                    <%-- <asp:Button ID="btnViewReport" Text="View Report" runat="server" OnClick="btnViewReport_Click"
                        CausesValidation="false" />--%>
                </td>
            </tr>
        </table>

       <%-- <table>
            <tr>
                <td>
                    <obout:Grid ID="GVexport" runat="server" AutoGenerateColumns="true" AllowAddingRecords="False"
                                AllowFiltering="True" AllowGrouping="True" AllowMultiRecordSelection="False"
                                AllowRecordSelection="true" Width="100%" AllowColumnResizing="true" AllowColumnReordering="true"> 
                            </obout:Grid>
                </td>
            </tr>
        </table>--%>
    </center>
    <script type="text/javascript">
        //        jsCheckIssueHistory();
        function onselectA(invoker) {
            var allA = tblRptMenu.getElementsByTagName('a');
            for (var i = 0; i < allA.length; i++) {
                allA[i].className = '';
            }
            invoker.className = "aselected";
        }
        function CloseShowReport() {
            LoadingOff();
            divPopUp.className = "divDetailExpandPopUpOff";
        }
    </script>
    <style type="text/css">
        .popupClose {
            background: url(../App_Themes/Blue/img/icon_close.png) no-repeat;
            height: 32px;
            width: 32px;
            float: right;
            margin-top: -30px;
            margin-right: -25px;
        }

            .popupClose:hover {
                cursor: pointer;
            }

        .divDetailExpandPopUpOff {
            display: none;
        }

        .divDetailExpandPopUpOn {
            border: solid 3px gray;
            width: 65%;
            height: 98%;
            padding: 10px;
            background-color: #FFFFFF;
            margin-top: 50px;
            top: 1%;
            left: 3%;
            position: absolute;
            z-index: 99999;
        }
    </style>
</asp:Content>
