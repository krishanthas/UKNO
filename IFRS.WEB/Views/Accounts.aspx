<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeBehind="Accounts.aspx.cs" Inherits="IFRS.WEB.Views.Accounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeMasterContent" runat="server">
    <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Skin='<%#this.Skin%>'>
        <Rows>
            <telerik:LayoutRow RowType="Generic">
                <Columns>
                    <telerik:LayoutColumn Style="text-align: center;">
                        <telerik:RadGrid
                            ID="GridAccounts"
                            runat="server"
                            OnItemCommand="RadGrid1_ItemCommand"
                            OnItemDataBound="RadGrid1_ItemDataBound"
                            Skin='<%#this.Skin%>'>
                            <ClientSettings EnablePostBackOnRowClick="true">
                                <%--<Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>--%>
                                <Selecting AllowRowSelect="True" />
                                <%--<ClientEvents OnRowClick="RowClick" />--%>
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed" DataKeyNames="AccountNumber">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Account Number" DataField="AccountNumber" UniqueName="AccountNumber" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle CssClass="grid-link" />
                                        <HeaderStyle Width="130px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Customer Id" DataField="CustomerId" UniqueName="CustomerId" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="100px" />
                                    </telerik:GridBoundColumn>
                                    <%-- <telerik:GridNumericColumn HeaderText="Amortized Amount" DataField="AmortizedAmount" UniqueName="AmortizedAmount" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridNumericColumn>--%>
                                    <telerik:GridNumericColumn HeaderText="Amortized Amount" DataField="AmortizedAmount" UniqueName="AmortizedAmount" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle Width="120px" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn HeaderText="Impairment Amount" DataField="ImpairmentAmount" UniqueName="ImpairmentAmount" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle Width="130px" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn HeaderText="Present Value" DataField="PresentValue" UniqueName="PresentValue" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle Width="120px" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn HeaderText="Cash Flows Amount" DataField="CashFlowsAmount" UniqueName="CashFlowsAmount" DataFormatString="{0:n}" DataType="System.Decimal" AllowRounding="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle Width="130px" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridBoundColumn HeaderText="Product" DataField="Product" UniqueName="Product" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="200px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="className" Display="false" DataField="className" HeaderText="className"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow RowType="Generic">
                <Columns>
                    <telerik:LayoutColumn Style="text-align: right; padding: 1% 1%;">
                        <% if (this.UserRole == IFRS.DEM.UserRole.ENTERING) {%>
                        <telerik:RadButton ID="RadButtonSubmit" runat="server" Text="Submit for Verification" OnClick="btnSubmit_Click" SingleClick="true" SingleClickText="Processing..." Skin='<%#this.Skin%>'></telerik:RadButton>
                        <% }
                            else /* if (this.UserRole == IFRS.DEM.UserRole.VERIFY_1) */ { %>
                        <div></div>
                        <% } %>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</asp:Content>

