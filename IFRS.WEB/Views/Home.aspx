<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="IFRS.WEB.Views.Home" %>

<%@ MasterType TypeName="IFRS.WEB.Views.Masters.HomeMasterPage" %>

<asp:Content ID="HomeMasterContent" ContentPlaceHolderID="HomeMasterContent" runat="server">
    <div>
        <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Skin='<%#this.Skin%>'>
            <Rows>
                <telerik:LayoutRow RowType="Generic">
                    <Columns>
                        <telerik:LayoutColumn Style="text-align: center;">
                            <telerik:RadGrid ID="GridCustomers" runat="server"
                                OnItemCommand="RadGrid1_ItemCommand"
                                OnItemDataBound="RadGrid1_ItemDataBound" OnPreRender="GridCustomers_PreRender"
                                EnableEmbeddedSkins="true"  style="height:70vh;"
                                Skin='<%#this.Skin%>'>
                                <ClientSettings EnablePostBackOnRowClick="true">
                                    <Selecting AllowRowSelect="True" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" EnableColumnClientFreeze="true" FrozenColumnsCount="2" ></Scrolling>
                                </ClientSettings>
                                <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed" DataKeyNames="Id">
                                    <SortExpressions>
                                        <telerik:GridSortExpression FieldName="SortOrder" SortOrder="Ascending" />
                                    </SortExpressions>
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="Customer Id" DataField="Id" UniqueName="Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle CssClass="grid-link" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Customer Name" DataField="Name" UniqueName="Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <HeaderStyle Width="250px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn HeaderText="Capital O/S(LKR)" DataField="CapitalOSLKR" UniqueName="CapitalOSLKR" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle Width="120px" />
                                        </telerik:GridNumericColumn>

                                        <telerik:GridBoundColumn HeaderText="Final SICR" DataField="FinalSICR" UniqueName="FinalSICR" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="NPA/PA" DataField="NPA_PA" UniqueName="NPA_PA" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Moratorium Approved" DataField="MoraStatus" UniqueName="MoraStatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"><HeaderStyle Width="80px" /></telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn HeaderText="Impairment" DataField="Impairment" UniqueName="Impairment" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"><HeaderStyle Width="130px" /></telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn HeaderText="Last Reject Reason" DataField="LastRejectReason" UniqueName="LastRejectReason" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"><HeaderStyle Width="350px" /></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Remark" DataField="Remark" UniqueName="Remark" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"><HeaderStyle Width="350px" /></telerik:GridBoundColumn>

                                        

                                        <%--
                                        <telerik:GridNumericColumn HeaderText="Impairment Amount" DataField="ImpairmentAmount" UniqueName="ImpairmentAmount" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle Width="130px" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn HeaderText="Amortized Amount" DataField="AmortizedAmount" UniqueName="AmortizedAmount" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle Width="120px" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn HeaderText="Present Value" DataField="PresentValue" UniqueName="PresentValue" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle Width="120px" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn HeaderText="Cash Flows Amount" DataField="CashFlowsAmount" UniqueName="CashFlowsAmount" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                            <HeaderStyle Width="130px" />
                                        </telerik:GridNumericColumn>--%>

                                        <telerik:GridBoundColumn HeaderText="Status" DataField="Status" UniqueName="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle Width="140px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="StatusCode" Display="false" DataField="StatusCode" HeaderText="StatusCode"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="SortOrder" Display="false" DataField="SortOrder" HeaderText="SortOrder"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="className" Display="false" DataField="className" HeaderText="className"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:LayoutColumn>
                    </Columns>
                </telerik:LayoutRow>
            </Rows>
        </telerik:RadPageLayout>
    </div>
</asp:Content>
