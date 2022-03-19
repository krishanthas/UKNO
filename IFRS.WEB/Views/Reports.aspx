<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="IFRS.WEB.Views.Reports" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %>

<%-- <%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %> --%>

<%-- <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> --%>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeMasterContent" runat="server" style="display: inline-block;">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Skin='<%#this.Skin%>'>
        <Rows>
            <telerik:LayoutRow RowType="Generic">
                <Columns>
                    <telerik:LayoutColumn Span="2" SpanSm="12" SpanXs="12" Style="width: 160px; padding: 0px; text-align: center;">
                        <telerik:RadDropDownList AutoPostBack="true" OnItemSelected="DropDownReportItem_OnItemSelected" RenderMode="Lightweight" ID="DropDownReportItem" Skin="Metro"
                            runat="server"
                            DefaultMessage="Select Report..."
                            Style="width: 100%;">
                        </telerik:RadDropDownList>
                        <br />
                        <telerik:RadLabel runat="server" ID="RadLabel1" AssociatedControlID="RadTextBox1" Text="As At Date"></telerik:RadLabel>
                        <telerik:RadDatePicker ID="DatePickerAsAtDate" runat="server">
                        </telerik:RadDatePicker>
                        <br />
                        <hr />
                        <br />
                        <telerik:RadButton ID="RadButtonSubmit" runat="server" Skin="Metro" Text="Submit" OnClick="btnFetch_Click" SingleClick="true" SingleClickText="Processing..." UseSubmitBehavior="false" CssClass="button-report-get" RenderMode="Lightweight"></telerik:RadButton>
                        <hr />
                        <script>
                            function reEnable() {
                                var btn = $find("<%=RadButtonExport.ClientID%>");
                                btn.enableAfterSingleClick();
                            }
                            function Click_OnClientClicked(sender, eventArgs) {
                                $find('<%=RadButtonExport.ClientID%>').enableAfterSingleClick();
                            }
                        </script>
                        <telerik:RadButton ID="RadButtonExport" OnClientClicked="Click_OnClientClicked" runat="server" Skin="Metro" Text="Export" OnClick="btnExport_Click" SingleClick="true" SingleClickText="Processing..." UseSubmitBehavior="false" CssClass="button-report-export" RenderMode="Lightweight"></telerik:RadButton>
                        <br />
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="10" SpanSm="12" SpanXs="12" Style="padding: 0px;">
                        <telerik:RadScriptBlock ID="radSript1" runat="server">
                            <script type="text/javascript">
                                function GridCreated(sender, eventArgs) {
                                    let id = sender.get_id();
                                    if (id) {
                                        var grid = $find(id);
                                        if (grid) {
                                            var columns = grid.get_masterTableView().get_columns();
                                            for (var i = 0; i < columns.length; i++) {
                                                columns[i].resizeToFit(false, true);
                                            }
                                        }
                                    }
                                }
                            </script>
                        </telerik:RadScriptBlock>
                        <% if (this.SelectedReportIndex == 0) { %>
                        <telerik:RadGrid
                            RenderMode="Lightweight" ID="GridCustomers" runat="server"
                            AllowPaging="True" PageSize="15" AllowCustomPaging="true"
                            OnNeedDataSource="Grid_NeedDataSource"
                            OnInfrastructureExporting="GridCustomers_InfrastructureExporting"
                            VirtualItemCount="10000" 
                            CssClass="report-grid" OnPreRender="GridCustomers_PreRender" >
                            <ExportSettings HideStructureColumns="false" ExportOnlyData="false" IgnorePaging="true" OpenInNewWindow="true" Excel-Format="Xlsx">
                            </ExportSettings>
                            <ClientSettings>
                                <%--<Virtualization EnableVirtualization="false" />--%>
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"></Scrolling>
                                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="As At Date" DataField="AsAtDate" UniqueName="AsAtDate" DataFormatString="{0:yyyy-MM-dd}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Customer Id" DataField="Id" UniqueName="Id"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Branch Id" DataField="BranchId" UniqueName="BranchId"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Customer Name" DataField="Name" UniqueName="Name"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Impairment Status" DataField="ImpairmentStatus" UniqueName="ImpairmentStatus"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="O/S LKR" DataField="CapitalOSLKR" UniqueName="CapitalOSLKR" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Total Amortize Amount" DataField="TotalAmortizedAmount" UniqueName="TotalAmortizedAmount" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Total Cash Flows Amount" DataField="TotalCashFlowsAmount" UniqueName="TotalCashFlowsAmount" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Total Present Value" DataField="TotalPresentValue" UniqueName="TotalPresentValue" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Total Impairment Amount" DataField="TotalImpairmentAmount" UniqueName="TotalImpairmentAmount" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Entry User Id" DataField="EntryUserId" UniqueName="EntryUserId"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Approved Manager Id" DataField="RelationshipManager" UniqueName="RelationshipManager"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn HeaderText="Final SICR" DataField="FinalSICR" UniqueName="FinalSICR" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="NPA/PA" DataField="NPA_PA" UniqueName="NPA_PA" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Moratorium Approved" DataField="MoraStatus" UniqueName="MoraStatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn HeaderText="Impairment" DataField="Impairment" UniqueName="Impairment" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"><HeaderStyle Width="130px" /></telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn HeaderText="Last Reject Reason" DataField="LastRejectReason" UniqueName="LastRejectReason" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"><HeaderStyle Width="350px" /></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Remark" DataField="AnswerRemark" UniqueName="Remark" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"><HeaderStyle Width="350px" /></telerik:GridBoundColumn>

                                    <%--                                    <telerik:GridBoundColumn HeaderText="BranchCode" DataField="BranchCode" UniqueName="BranchCode"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="StatusCode" DataField="StatusCode" UniqueName="StatusCode"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="DelFlag" DataField="DelFlag" UniqueName="DelFlag"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Verify_1" DataField="Verify_1" UniqueName="Verify_1"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Verify_2" DataField="Verify_2" UniqueName="Verify_2"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Verify_3" DataField="Verify_3" UniqueName="Verify_3"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Verify_4" DataField="Verify_4" UniqueName="Verify_4"></telerik:GridBoundColumn>--%>

                                    <telerik:GridBoundColumn HeaderText="q1" DataField="q1" UniqueName="q1"><HeaderStyle Width="80px" ForeColor="Red" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q2" DataField="q2" UniqueName="q2"><HeaderStyle Width="80px" ForeColor="Red" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q3" DataField="q3" UniqueName="q3"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q4" DataField="q4" UniqueName="q4"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q5" DataField="q5" UniqueName="q5"><HeaderStyle Width="80px" BackColor="#99cc00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q6" DataField="q6" UniqueName="q6"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q7" DataField="q7" UniqueName="q7"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q8" DataField="q8" UniqueName="q8"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q9" DataField="q9" UniqueName="q9"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q10" DataField="q10" UniqueName="q10"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q11" DataField="q11" UniqueName="q11"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q12" DataField="q12" UniqueName="q12"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q13" DataField="q13" UniqueName="q13"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q14" DataField="q14" UniqueName="q14"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q15" DataField="q15" UniqueName="q15"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q16" DataField="q16" UniqueName="q16"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q17" DataField="q17" UniqueName="q17"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q18" DataField="q18" UniqueName="q18"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q19" DataField="q19" UniqueName="q19"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q20" DataField="q20" UniqueName="q20"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q21" DataField="q21" UniqueName="q21"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q22" DataField="q22" UniqueName="q22"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q23" DataField="q23" UniqueName="q23"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q24" DataField="q24" UniqueName="q24"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q25" DataField="q25" UniqueName="q25"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q26" DataField="q26" UniqueName="q26"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q27" DataField="q27" UniqueName="q27"><HeaderStyle Width="80px" BackColor="#ffff00" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q28" DataField="q28" UniqueName="q28"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q29" DataField="q29" UniqueName="q29"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="q30" DataField="q30" UniqueName="q30"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn HeaderText="Status History" DataField="StatusHistory" UniqueName="StatusHistory"></telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <% }
                            else if (this.SelectedReportIndex == 1) { %>
                        <telerik:RadGrid
                            RenderMode="Lightweight" ID="GridAccounts" runat="server"
                            AllowSorting="true" ShowStatusBar="true"
                            AllowPaging="True" PageSize="15" AllowCustomPaging="true" 
                            OnNeedDataSource="Grid_NeedDataSource" Skin="Metro" CssClass="report-grid">
                            <ClientSettings>
                                <Virtualization EnableVirtualization="false" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"></Scrolling>
                                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="AccountNumber">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Account Number" DataField="AccountNumber" UniqueName="AccountNumber"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Customer Id" DataField="CustomerId" UniqueName="CustomerId"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="A sAt Date" DataField="AsAtDate" UniqueName="AsAtDate"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Name" DataField="Name" UniqueName="Name"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Product" DataField="Product" UniqueName="Product"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Granted Amount(LKR)" DataField="GrantedAmountLKR" UniqueName="GrantedAmountLKR" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Capital (LKR)" DataField="CapitalLKR" UniqueName="CapitalLKR" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Interest O/S (LKR)" DataField="InterestOSLKR" UniqueName="InterestOSLKR" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Granted Date" DataField="GrantedDate" UniqueName="GrantedDate"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Currency" DataField="Currency" UniqueName="Currency"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Impairment Status" DataField="ImpairmentStatus" UniqueName="ImpairmentStatus"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Impairment Amount" DataField="ImpairmentAmount" UniqueName="ImpairmentAmount" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Present Value" DataField="PresentValue" UniqueName="PresentValue" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Classification" DataField="Classification" UniqueName="Classification"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Period" DataField="Period" UniqueName="Period"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Amortized Amount" DataField="AmortizedAmount" UniqueName="AmortizedAmount" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Interest Rate" DataField="InterestRate" UniqueName="InterestRate" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Applied Interest Rate" DataField="AppliedInterestRate" UniqueName="AppliedInterestRate" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Total Cash Flow Amount" DataField="TotalCashFlowAmount" UniqueName="TotalCashFlowAmount" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn HeaderText="DelFlag" DataField="DelFlag" UniqueName="DelFlag"></telerik:GridBoundColumn>--%>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <% }
                            else if (this.SelectedReportIndex == 2) { %>
                        <telerik:RadGrid
                            RenderMode="Lightweight" ID="GridCashFlows" runat="server"
                            AllowSorting="true" ShowStatusBar="true"
                            AllowPaging="True" PageSize="15" AllowCustomPaging="true" 
                            OnNeedDataSource="Grid_NeedDataSource" Skin="Metro" CssClass="report-grid">
                            <ClientSettings>
                                <Virtualization EnableVirtualization="false" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"></Scrolling>
                                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id">
                                <Columns>
                                    <%--<telerik:GridBoundColumn HeaderText="Id" DataField="Id" UniqueName="Id"></telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn HeaderText="Customer Id" DataField="CustomerId" UniqueName="CustomerId"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Account Number" DataField="AccountNumber" UniqueName="AccountNumber"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Account Name" DataField="AccountName" UniqueName="AccountName"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Interest Rate" DataField="InterestRate" UniqueName="InterestRate" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Date" DataField="Date" UniqueName="Date" DataFormatString="{0:yyyy-MM-dd}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Amount" DataField="Amount" UniqueName="Amount" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Present Value" DataField="PresentValue" UniqueName="PresentValue" DataFormatString="{0:n}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Status" DataField="Status" UniqueName="Status"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Entry User Id" DataField="EntryUserId" UniqueName="EntryUserId"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Entry Time" DataField="EntryTime" UniqueName="EntryTime" DataFormatString="{0:yyyy-MM-dd-HH-mm-ss}"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="A sAt Date" DataField="AsAtDate" UniqueName="AsAtDate" DataFormatString="{0:yyyy-MM-dd}"></telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn HeaderText="DelFlag" DataField="DelFlag" UniqueName="DelFlag"></telerik:GridBoundColumn>--%>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <% }
                            else if (this.SelectedReportIndex == 3) { %>
                            <div style="margin-left: 10px;display:block">
                                <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="400px" runat="server"></rsweb:ReportViewer>
                            </div>
                        <% } %>

                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>




</asp:Content>
