<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeBehind="CashFlowsReadonly.aspx.cs" Inherits="IFRS.WEB.Views.CashFlowsReadonly" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HomeMasterContent" runat="server">
    <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Skin='<%#this.Skin%>'>
        <Rows>
            <telerik:LayoutRow RowType="Container" WrapperHtmlTag="Div">
                <Columns>
                    <telerik:LayoutColumn Span="12" SpanSm="12" SpanXs="12" Style="padding: 0px 0px 0px 1px;">
                        <telerik:RadGrid ID="GridCashFlows" runat="server"  Skin='<%#this.Skin%>'>
                            <ClientSettings>
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed" DataKeyNames="Id">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Date" DataField="Date" UniqueName="Date" DataFormatString="{0:dd-MM-yyyy}">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Forecast Amount" DataField="ForecastAmount" UniqueName="ForecastAmount" DataFormatString="{0:n}">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Present Value" DataField="PresentValue" UniqueName="PresentValue" DataFormatString="{0:n}">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:LayoutColumn>

                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</asp:Content>
