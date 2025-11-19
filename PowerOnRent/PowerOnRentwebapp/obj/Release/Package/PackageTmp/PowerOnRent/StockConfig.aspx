<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/CRM.Master" EnableEventValidation="false" CodeBehind="StockConfig.aspx.cs" Theme="Blue"
    Inherits="PowerOnRentwebapp.PowerOnRent.StockConfig" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Territory/UC_Territory.ascx" TagName="UC_Territory" TagPrefix="uc1" %>
<%@ Register Src="../Address/UCAddress.ascx" TagName="UCAddress" TagPrefix="uc7" %>
<%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc2" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc4" %>
<%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc5" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
    <uc5:UCToolbar ID="UCToolbar1" runat="server" />
    <uc4:UCFormHeader ID="UCFormHeader1" runat="server" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">

    <asp:ValidationSummary ID="validationsummary_Department" runat="server" ShowMessageBox="true"
        ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="Save" />

    <center>

        <asp:TabContainer ID="TabContainerUserCreation1" runat="server" ActiveTabIndex="0">
            <asp:TabPanel ID="TabPanelUsersList" runat="server" HeaderText="Database Setting">
                <ContentTemplate>
                    <center>
                        <table class="tableForm" style="width: 100%">
                            <tr>
                                <td style="width: 10%"></td>


                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>
                                </td>


                                <td style="width: 15%">
                                    <asp:DropDownList ID="ddlcompany" runat="server" Width="100%" ClientIDMode="Static"
                                        DataValueField="ID" DataTextField="Name" onchange="GetCompany(this);BindDepartment()">
                                    </asp:DropDownList>
                                </td>


                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lbldept" runat="server" Text="Department"></asp:Label>
                                </td>
                                <td style="width: 17%">
                                    <asp:DropDownList ID="ddldepartment" runat="server" Width="100%" ClientIDMode="Static" DataValueField="ID" DataTextField="Territory"
                                     onchange="GetDepartmentId(this)" >  <%--OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" AutoPostBack="true"--%>
                                    </asp:DropDownList>
                                    <%--onchange="GetDepartment(this)"--%>
                                </td>

                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblwmscode" runat="server" Text="WMS Storecode"></asp:Label>
                                </td>

                                 <td style="width: 18%">
                                    <asp:TextBox ID="txtwmsstorecode" runat="server" Width="100%" MaxLength="50"></asp:TextBox>

                                </td>

                               
                                <td style="width: 10%"></td>
                            </tr>


                            <tr>
                                <td style="width: 10%"></td>

                                 <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lbldatabase" runat="server" Text="Database"></asp:Label>
                                </td>

                                <td style="width: 18%">
                                    <asp:TextBox ID="txtdatabse" runat="server" Width="100%" MaxLength="50"></asp:TextBox>

                                </td>

                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblschema" runat="server" Text="Schema Name"></asp:Label>
                                </td>

                                <td style="width: 15%">
                                    <asp:TextBox ID="txtschema" runat="server" Width="100%" MaxLength="50"></asp:TextBox>
                                </td>

                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblconstrig" runat="server" Text="Connection String"  Visible="false" ></asp:Label>
                                </td>

                                <td style="width: 27%">
                                    <asp:TextBox ID="txtconstring" runat="server" Width="100%" MaxLength="250" Visible="false"></asp:TextBox>
                                </td>

                                <td style="width: 10%"></td>
                            </tr>

                            <tr>
                                  <td style="width: 10%"></td>
                                 <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblactive" runat="server" Text="Active"></asp:Label>
                                </td>

                                <td style="text-align: left">
                                   <obout:OboutRadioButton ID="btnradioyes" runat="server" Checked="True" FolderStyle=""
                                        GroupName="Active" Text="Yes">
                                    </obout:OboutRadioButton>
                                    <obout:OboutRadioButton ID="btnradiono" runat="server" FolderStyle="" GroupName="Active"
                                        Text="No">
                                    </obout:OboutRadioButton>
                                </td>

                                <td  style="width: 18%; text-align: right">
                                    <input type="button" id="btnAdd" runat="server" value="Add" style="cursor: pointer; padding: 4px 14px;" onclick="AddRecord(); return false;" />
                                </td>


                            </tr>

                            <tr>
                                 
                            </tr>
                        </table>
                    </center>
                    <center>
                        <table class="gridFrame" style="width: 100%">
                            <tr>
                                 <td style="text-align: left;">
                                    <%--<table class="gridFrame" style="width: 100%">
                                        <tr>
                                            <td style="text-align: left;">--%>
                                                <asp:Label ID="lblheader" CssClass="headerText" runat="server" Text="Database Setting List"></asp:Label>                                                 
                                           <%-- </td>
                                        </tr>
                                    </table>--%>

                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <obout:Grid ID="gvDepartmentlist" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                        Width="100%" AllowGrouping="True" AllowMultiRecordSelection="false" AutoGenerateColumns="False"
                                        OnRebind="gvDepartmentlist_Rebind" OnSelect="gvDepartmentlist_Select">
                                        <%-- OnSelect="gvUserCreationM_Select" OnRebind="gvUserCreationM_RebindGrid"--%>
                                        <ScrollingSettings ScrollHeight="250" />
                                        <Columns>
                                            <obout:Column DataField="Sequnece" HeaderText="Sr.No." AllowEdit="false" Width="5%" Visible="false"
                                        Align="center" HeaderAlign="center">
                                        <TemplateSettings TemplateId="ItemTempRemove1" />
                                    </obout:Column>

                                            <obout:Column ID="Edit" DataField="ID" HeaderText="Edit" Width="5%" TemplateId="GvTempEdit" Index="0">
                                                <TemplateSettings TemplateId="GvTempEdit" />
                                            </obout:Column>
                                            <obout:Column DataField="SchemaNm" HeaderText="Schema Name" Width="10%" Index="5">
                                            </obout:Column>
                                            <obout:Column DataField="CompanyNm" HeaderText="Company Name" Width="10%" Index="1">
                                            </obout:Column>
                                            <obout:Column DataField="DeptCode" HeaderText="Department Code" Width="10%" Index="2">
                                            </obout:Column>
                                            <obout:Column DataField="WmsStorecode" HeaderText="WMS Storecode" Width="10%" Index="3">
                                            </obout:Column>
                                            <obout:Column DataField="DatabaseName" HeaderText="Datebase Name" Width="13%" Index="4">
                                            </obout:Column>
                                            <obout:Column DataField="ConnectionString" HeaderText="ConnectionString" Width="0%" Index="6" Visible="false">
                                            </obout:Column>
                                            <obout:Column DataField="Active" HeaderText="Active" Width="5%" Index="7">
                                            </obout:Column>
                                             <obout:Column DataField="CompanyId" HeaderText="CompanyId" Width="1%" Index="8" Visible="false">
                                            </obout:Column>
                                             <obout:Column DataField="DeptId" HeaderText="DeptId" Width="1%" Index="9" Visible="false">
                                            </obout:Column>
                                        </Columns>
                                        <Templates>
                                            <obout:GridTemplate ID="ItemTempRemove1">
                                        <Template>
                                            <table>
                                                <tr>
                                                    <td style="width: 20px; text-align: center;">                                                                                                              
                                                    </td>
                                                    <td style="width: 35px; text-align: center;">
                                                    <%-- <%# Convert.ToInt32(Container.PageRecordIndex) + 1 %> 
                                                        <asp:HiddenField ID="Hdnsrid" runat="server" Value="<%# Convert.ToInt32(Container.PageRecordIndex) + 1 %>" ClientIDMode="Static" />--%>

                                                        <asp:Label ID="lblsrid" runat="server" Text=" <%# Convert.ToInt32(Container.PageRecordIndex) + 1 %> "></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </Template>
                                    </obout:GridTemplate>
                                            <obout:GridTemplate ID="GvTempEdit" runat="server">
                                                <Template>
                                                    <asp:ImageButton ID="imgBtnEdit" CausesValidation="false" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png" />
                                                    <%-- <asp:ImageButton ID="imgBtnEdit1" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png"
                                                                    OnClick="imgBtnEdit_OnClick" ToolTip='<%# (Container.Value) %>' CausesValidation="false" />--%>
                                                </Template>
                                            </obout:GridTemplate>
                                        </Templates>
                                    </obout:Grid>
                                </td>
                            </tr>

                        </table>
                    </center>
            

                </ContentTemplate>
            </asp:TabPanel>

        
        </asp:TabContainer>

    </center>
    <asp:HiddenField ID="hdnSelectedCompany" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnEditrecId" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID ="hdndeptid" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hndRoleSate" runat="server" />




    <script type="text/javascript">

        var ddlcompany = document.getElementById("<%=ddlcompany.ClientID %>");
        var ddldeptid = document.getElementById("<%=ddldepartment.ClientID %>");

        function GetCompany(obj) {
            var ddlcompany = document.getElementById("<%=ddlcompany.ClientID %>");
            //var hdnSelectedCompany = document.getElementById('hdnSelectedCompany');
            var hdnSelectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");
            hdnSelectedCompany.value = ddlcompany.value;
        }

        function BindDepartment() {
            var hdnSelectedCompany = document.getElementById('hdnSelectedCompany');
            // PageMethods.PMGetDepartmentList(hdnSelectedCompany.value, onSuccessGetDepartmentList, null)
            var obj1 = new Object();
            obj1.ddlcompanyId = document.getElementById("<%=ddlcompany.ClientID %>").value;
            PageMethods.GetDepartment(obj1, getLoc_onSuccessed);
        }

        function getLoc_onSuccessed(result) {

            ddldeptid.options.length = 0;
            for (var i in result) {
                AddOption(result[i].Name, result[i].Id);
            }
        }

        function AddOption(text, value) {

            var option = document.createElement('option');
            option.value = value;
            option.innerHTML = text;
            ddldeptid.options.add(option);
        }



        function GetDepartmentId(id)
        {
            var departmentId = document.getElementById("<%=ddldepartment.ClientID %>");
            var hdndepartmentId = document.getElementById("<%=hdndeptid.ClientID %>");
            hdndepartmentId.value = departmentId.value;
            gvDepartmentlist.refresh();
        }

        function onSuccessGetDepartmentList(result) {
            ddlDepartment = document.getElementById("<%=ddldepartment.ClientID %>");
            var hdnSelectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");
            ddlDepartment.options.length = 0;
            var option0 = document.createElement("option");

            if (result.length > 0) {

                option0.text = "-Select All-";
                option0.value = "0";

            }
            else {
                if (hdnSelectedCompany.value == "0") {

                   
                    option0.text = "-Select All-";
                    option0.value = "0";
                }
                else {
                    option0.text = "N/A"; option0.value = "0";
                }
            }

            try {
                ddlDepartment.add(option0, null);

            }
            catch (error) {
                ddlDepartment.add(option0);
            }

            for (i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");

                option1.text = result[i].Territory;
                option1.value = result[i].ID;

                try {
                    ddlDepartment.add(option1, null);
                }
                catch (error) { ddlDepartment.add(option1); }
            }
        }

        function AddRecord()
        {

            var txtcom = document.getElementById("<%=ddlcompany.ClientID%>").value;
            var txtDept = document.getElementById("<%=ddldepartment.ClientID%>").value;
            var txtDb = document.getElementById("<%=txtdatabse.ClientID%>").value;
            var constring = "";
            var schemanm = document.getElementById("<%=txtschema.ClientID%>").value;
            var c = document.getElementById("<%=ddlcompany.ClientID%>");
            var redbuttonyes = document.getElementById("<%=btnradioyes.ClientID %>").checked;
           
            var comnm = c.options[c.selectedIndex].text;
            var Active;
            if (redbuttonyes == "true" || redbuttonyes == "1" || redbuttonyes == 1)
            {
                Active = "Yes";
            }
            else
            {
                Active = "No";
            }

              var wmsstorecode = document.getElementById("<%=txtwmsstorecode.ClientID%>").value;
              var d = document.getElementById("<%=ddldepartment.ClientID%>");
              var deptnm = d.options[d.selectedIndex].text;

              var action = "";
              var btnAdd = document.getElementById("<%=btnAdd.ClientID%>").value;   
              if (txtcom == "0") {
                  showAlert("Please Select the Customer ...", "Error", "#");
              }
              else if (txtDept == "0") {
                  showAlert("Please Select the Department ...", "Error", "#");
              }
              else if (txtDb == "") {
                  showAlert("Please Enter the Database Name...", "Error", "#");
              }
              else if (schemanm == "") {
                  showAlert("Please Enter the Database Schema Name...", "Error", "#");
              }
              else if (wmsstorecode == "") {
                  showAlert("Please Enter the WMS Storecode...", "Error", "#");
              }
              else {
                 if (btnAdd == "Add") {
                      action = "Add";
                      PageMethods.InsertRecord(action, txtcom, comnm, txtDept, deptnm, schemanm, txtDb, constring, Active,wmsstorecode, OnSuccessOperation, null);
                  }
                  else {
                      action.value = "Update"
                      PageMethods.InsertRecord(action, txtcom, comnm, txtDept, deptnm, schemanm, txtDb, constring, Active,wmsstorecode, OnSuccessOperation, null);
                  }
              }
          }
          function OnSuccessOperation(result) {
              Clear();
              showAlert("Record saved successfully...", "info", "#");
              gvDepartmentlist.refresh();
          }

          function Clear() {
              document.getElementById("<%=txtdatabse.ClientID %>").value = "";
            document.getElementById("<%=txtschema.ClientID %>").value = "";
            document.getElementById("<%=txtconstring.ClientID %>").value = "";
            document.getElementById("<%=ddlcompany.ClientID%>").value = 0;
              document.getElementById("<%=ddldepartment.ClientID%>").value = 0;
              document.getElementById("<%=btnradioyes.ClientID %>").Cheked;
                document.getElementById("<%=txtwmsstorecode.ClientID %>").value = "";

            return true;
        }

    </script>





</asp:Content>
