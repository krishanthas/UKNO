<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeBehind="CashFlows.aspx.cs" Inherits="IFRS.WEB.Views.CashFlows" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeMasterContent" runat="server">
    <telerik:RadWindowManager RenderMode="Lightweight" ID="windowManager1" runat="server" Style="z-index: 100001"></telerik:RadWindowManager>
    <telerik:RadPageLayout ID="RadPageLayoutHomeHeader" runat="server" Skin='<%#this.Skin%>'>
        <Rows>
            <telerik:LayoutRow RowType="Container" WrapperHtmlTag="Div" CssClass="cashflows-sub-title-bar cashflows-sub-title-bar-1">
                <Columns>
                    <telerik:LayoutColumn Span="4" SpanSm="6" SpanXs="12" CssClass="cashflows-sub-title-bar-item">
                        <telerik:RadLabel runat="server" ID="RadLabel1" AssociatedControlID="RadTextBox1" Text="Account No"></telerik:RadLabel>
                        <telerik:RadLabel runat="server" ID="LabelAccountNumber" AssociatedControlID="RadTextBox1" Text=""></telerik:RadLabel>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="4" SpanSm="6" SpanXs="12" CssClass="cashflows-sub-title-bar-item">
                        <telerik:RadLabel runat="server" ID="RadLabel2" AssociatedControlID="RadTextBox1" Text="Currency"></telerik:RadLabel>
                        <telerik:RadLabel runat="server" ID="LabelAccountCurrency" AssociatedControlID="RadTextBox1" Text=""></telerik:RadLabel>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="4" SpanSm="6" SpanXs="12" CssClass="cashflows-sub-title-bar-item">
                        <telerik:RadLabel runat="server" ID="RadLabel11" AssociatedControlID="RadTextBox1" Text="Customer Id"></telerik:RadLabel>
                        <telerik:RadLabel runat="server" ID="Label1CustomerId" AssociatedControlID="RadTextBox1" Text=""></telerik:RadLabel>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
        <Rows>
            <telerik:LayoutRow RowType="Container" WrapperHtmlTag="Div" CssClass="cashflows-sub-title-bar cashflows-sub-title-bar-2">
                <Columns>
                    <telerik:LayoutColumn Span="4" SpanSm="6" SpanXs="12" CssClass="cashflows-sub-title-bar-item">
                        <telerik:RadLabel runat="server" ID="RadLabel3" AssociatedControlID="RadTextBox1" Text="O/S"></telerik:RadLabel>
                        <telerik:RadLabel runat="server" ID="LabelOS" AssociatedControlID="RadTextBox1" Text=""></telerik:RadLabel>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="4" SpanSm="6" SpanXs="12" CssClass="cashflows-sub-title-bar-item">
                        <telerik:RadLabel runat="server" ID="RadLabel5" AssociatedControlID="RadTextBox1" Text="Amortized Amount"></telerik:RadLabel>
                        <telerik:RadLabel runat="server" ID="LabelAmortizedAmount" AssociatedControlID="RadTextBox1" Text=""></telerik:RadLabel>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="4" SpanSm="6" SpanXs="12" CssClass="cashflows-sub-title-bar-item">
                        <telerik:RadLabel runat="server" ID="RadLabel4" AssociatedControlID="RadTextBox1" Text="Interest Rate"></telerik:RadLabel>
                        <telerik:RadLabel runat="server" ID="LabelInterestRate" AssociatedControlID="RadTextBox1" Text=""></telerik:RadLabel>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
    <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Skin='<%#this.Skin%>'>
        <Rows>
            <telerik:LayoutRow RowType="Container" WrapperHtmlTag="Div" Style="padding: 1px 0px;">
                <Columns>
                    <telerik:LayoutColumn Span="7" SpanSm="10" SpanXs="12" Style="padding: 0px 1px 0px 0px;">
                        <telerik:RadPageLayout ID="ForecastingAreaPageLayout" runat="server" Skin='<%#this.Skin%>' CssClass="forecasting-area">
                            <Rows>
                                <telerik:LayoutRow RowType="Container" WrapperHtmlTag="Div" CssClass="forecasting-area-header">
                                    <Columns>
                                        <telerik:LayoutColumn Span="12" SpanSm="12" SpanXs="12" Style="height: 21px; display: block;">
                                            <telerik:RadLabel runat="server" ID="RadLabel7" AssociatedControlID="RadTextBox1" Text="Forecasting Area" Style="margin-bottom: 0px; display: inline-block;"></telerik:RadLabel>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                                <telerik:LayoutRow RowType="Container" WrapperHtmlTag="Div" CssClass="forecasting-area-body">
                                    <Columns>
                                        <telerik:LayoutColumn Span="12" SpanSm="12" SpanXs="12">
                                            <telerik:RadLabel runat="server" ID="RadLabel9" AssociatedControlID="RadTextBox1" Text="Forecast Date"></telerik:RadLabel>
                                            <telerik:RadDatePicker ID="RadDatePickerForecastDate" runat="server">
                                                <ClientEvents OnDateSelected="RadDatePickerForecastDate_OnDateSelected" />
                                            </telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator ID="RadDatePickerForecastDateRequiredFieldValidator" runat="server" Display="Dynamic" ControlToValidate="RadDatePickerForecastDate" ErrorMessage="The date cannot be empty" Style="color: red; font-size: small;" CssClass="validators" ValidationGroup="validators"></asp:RequiredFieldValidator>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="12" SpanSm="12" SpanXs="12">
                                            <telerik:RadLabel runat="server" ID="RadLabel8" AssociatedControlID="RadTextBox1" Text="Interest Rate(%)"></telerik:RadLabel>
                                            <telerik:RadNumericTextBox ID="NumericTextBoxInterestRate" runat="server" Type="Percent" MinValue="0" MaxValue="100">
                                                <NumberFormat AllowRounding="true" KeepNotRoundedValue="false" DecimalDigits="2" />
                                                <ClientEvents OnValueChanged="NumericTextBoxInterestRate_OnValueChanged" />
                                            </telerik:RadNumericTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="NumericTextBoxInterestRate" ErrorMessage="The interest rate cannot be empty" Style="color: red; font-size: small;" CssClass="validators" ValidationGroup="validators"></asp:RequiredFieldValidator>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="12" SpanSm="12" SpanXs="12">
                                            <telerik:RadLabel runat="server" ID="RadLabel12" AssociatedControlID="RadTextBox1" Text="Cash flow Amount"></telerik:RadLabel>
                                            <telerik:RadNumericTextBox ID="NumericTextBoxCashflowAmount" runat="server">
                                                <NumberFormat AllowRounding="true" KeepNotRoundedValue="false" DecimalDigits="2" />
                                                <ClientEvents OnValueChanged="NumericTextBoxCashflowAmount_OnValueChanged" />
                                            </telerik:RadNumericTextBox>
                                            <telerik:RadTextBox ReadOnly="true" ID="TextBoxCashflowAmountMaxValue" runat="server" Style="width: 200px;font-size: 12px; border: none; color: red;">
                                            </telerik:RadTextBox>
                                            <br />
                                            <%--<telerik:RadLabel runat="server" ID="LabelCashflowAmountMaxValue" Text=""></telerik:RadLabel>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="NumericTextBoxCashflowAmount" ErrorMessage="The amount cannot be empty" Style="color: red; font-size: small;" CssClass="validators" ValidationGroup="validators"></asp:RequiredFieldValidator>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="12" SpanSm="6" SpanXs="12">
                                            <telerik:RadLabel runat="server" ID="RadLabel10" AssociatedControlID="RadTextBox1" Text="Period"></telerik:RadLabel>
                                            <telerik:RadRadioButtonList CssClass="forecasting-area-body-radios" runat="server" ID="RadiosPeriod" AutoPostBack="false" Layout="Flow" Columns="1" Direction="Horizontal" HeaderStyle-HorizontalAlign="Center">
                                                <ClientEvents OnSelectedIndexChanged="RadiosPeriod_OnSelectedIndexChanged" />
                                                <Items>
                                                    <telerik:ButtonListItem Text="1  Month  Forecast" Value="1" Selected="true" />
                                                    <telerik:ButtonListItem Text="3  Months Forecast" Value="3" />
                                                    <telerik:ButtonListItem Text="6  Months Forecast" Value="6" />
                                                    <telerik:ButtonListItem Text="12 Months Forecast" Value="12" />
                                                </Items>
                                            </telerik:RadRadioButtonList>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="12" SpanSm="6" SpanXs="12" Style="text-align: right; padding: 1% 1%;">
                                            <script>
                                                function monthDiff(d1, d2) {
                                                    var months;
                                                    months = (d2.getFullYear() - d1.getFullYear()) * 12;
                                                    months -= d1.getMonth();
                                                    months += d2.getMonth();
                                                    return months <= 0 ? 0 : months;
                                                }

                                                function SetMaxCashFlowValue() {
                                                    var LabelImpairedAmount = $find("<%= LabelImpairedAmount.ClientID %>").get_value();
                                                    var RadDatePickerForecastDate = $find("<%= RadDatePickerForecastDate.ClientID %>").get_selectedDate();
                                                    var RadDatePickerAsAtDate = $find("<%= RadDatePickerAsAtDate.ClientID %>").get_selectedDate();
                                                    var NumericTextBoxInterestRate = $find("<%= NumericTextBoxInterestRate.ClientID %>").get_value();
                                                    var RadiosPeriodElement = $find("<%= RadiosPeriod.ClientID %>");//.get_selectedIndex();
                                                    var LabelCashflowAmountMaxValueElement = $find("<%= TextBoxCashflowAmountMaxValue.ClientID %>");//.get_selectedIndex();

                                                    var RadiosPeriod = 1;

                                                    var NumericTextBoxCashflowAmountElement = $find("<%= NumericTextBoxCashflowAmount.ClientID %>");
                                                    var NumericTextBoxCashflowAmount = NumericTextBoxCashflowAmountElement.get_value();

                                                    var RadiosPeriodElementIndex = RadiosPeriodElement.get_selectedIndex();
                                                    if (RadiosPeriodElementIndex > 0) {
                                                        var items = RadiosPeriodElement.get_items();
                                                        for (var i = 0; i < items.length; i++) {
                                                            if (items[i].get_selected()) {
                                                                RadiosPeriod = parseInt(items[i].get_value() || 1);
                                                            }
                                                        }
                                                    }

                                                    console.log(RadDatePickerForecastDate, NumericTextBoxInterestRate, NumericTextBoxCashflowAmount, RadiosPeriod)

                                                    if (RadDatePickerForecastDate && NumericTextBoxInterestRate && RadiosPeriod) {
                                                        let pv = parseFloat(LabelImpairedAmount);
                                                        let period = RadiosPeriod;

                                                        //let ir = 1 + ((NumericTextBoxInterestRate / 100) / 12);
                                                        //let irPowered = Math.pow(ir, period);
                                                        //let irSum = irPowered * ((1 - irPowered) / (1 - ir));
                                                        //let cfvMax = pv / irSum;
                                                        //console.log('pv ', pv, ' period ', period, ' ir  ', ir, ' irPowered ', irPowered,' cfvMax ', cfvMax )

                                                        let months = monthDiff(RadDatePickerAsAtDate, RadDatePickerForecastDate);
                                                        let irPow = Math.pow(1 + ((NumericTextBoxInterestRate / 100) / 12), months);
                                                        let cfvMax = pv * irPow;
                                                        console.log('pv ', pv, ' irPow ', irPow, ' months ', months, ' cfvMax ', cfvMax);

                                                        //if (NumericTextBoxCashflowAmount > cfvMax) {
                                                        //    NumericTextBoxCashflowAmountElement._maxValue = Number.MAX_SAFE_INTEGER;
                                                        //    NumericTextBoxCashflowAmountElement.set_value(cfvMax);
                                                        //} 
                                                        NumericTextBoxCashflowAmountElement._maxValue = cfvMax;
                                                        //LabelCashflowAmountMaxValueElement.set_value(`Max Amount : ${Number(cfvMax).toLocaleString('en', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`);
                                                        LabelCashflowAmountMaxValueElement.set_value(`Max Amount : ${cfvMax}`);
                                                        LabelCashflowAmountMaxValueElement.set_visible(true);

                                                    }
                                                    else {
                                                        LabelCashflowAmountMaxValueElement.set_visible(false);
                                                    }
                                                }

                                                function RadDatePickerForecastDate_OnDateSelected(sender, e) {
                                                    if (e.get_newDate() != null) {
                                                        console.log("OnDateSelected: " + e.get_newDate().toDateString() + " selected in " + sender.get_id() + "<br />");
                                                    }
                                                    else {
                                                        console.log("OnDateSelected: Date cleared in " + sender.get_id() + "<br />");
                                                    }
                                                    SetMaxCashFlowValue();
                                                }

                                                function NumericTextBoxInterestRate_OnValueChanged(sender, eventArgs) {
                                                    if (eventArgs.get_newValue() == "") {
                                                        eventArgs.set_cancel(true);
                                                    }
                                                    SetMaxCashFlowValue();
                                                }

                                                function NumericTextBoxCashflowAmount_OnValueChanged(sender, eventArgs) {
                                                    if (eventArgs.get_newValue() == "") {
                                                        eventArgs.set_cancel(true);
                                                    }
                                                    SetMaxCashFlowValue();
                                                }

                                                function RadiosPeriod_OnSelectedIndexChanged(sender, args) {
                                                    var oldItem = sender.get_items()[args.get_oldSelectedIndex()];
                                                    var newItem = sender.get_items()[args.get_newSelectedIndex()];
                                                    SetMaxCashFlowValue();
                                                    //console.log("You changed from " + oldItem.get_text() + " to " + newItem.get_text() + " language.");
                                                }

                                                //OnClientClicking = "AddOnClientClicking"
                                                //OnClientClicked = "AddOnClientClicked" 

                                                //function AddOnClientClicking() {
                                                //    SetMaxCashFlowValue();

                                                //}

                                                $(function () {
                                                    console.log('auto-validate-triggered!');
                                                    setTimeout(function () {
                                                        console.log('auto-validate-started!');
                                                        SetMaxCashFlowValue();
                                                    },
                                                        100);
                                                });

                                            </script>
                                            <telerik:RadButton ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_OnClick" UseSubmitBehavior="false" ValidationGroup="validators">
                                            </telerik:RadButton>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Enabled="false" Style="display: none;" Span="12" SpanSm="12" SpanXs="12">
                                            <telerik:RadDatePicker ID="RadDatePickerAsAtDate" runat="server" CssClass="RadDatePickerForecastDate">
                                            </telerik:RadDatePicker>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                            </Rows>
                            <Rows>
                                <telerik:LayoutRow RowType="Container" WrapperHtmlTag="Div" Style="padding: 5px 0px; border: 1px solid #00a2ea">
                                    <Columns>
                                        <telerik:LayoutColumn Span="6" SpanSm="12" SpanXs="12" Style="display: inline-block; padding-right: 0px;">
                                            <telerik:RadLabel Style="display: inline-block; font-size: 14px; min-width: 130px;" runat="server" ID="RadLabel6" AssociatedControlID="RadTextBox1" Text="Impaired Amount"></telerik:RadLabel>
                                            <telerik:RadNumericTextBox Style="padding-top: 0px; margin-top: -5px; display: inline-block; border: none; font-size: 16px; color: blue; font-weight: bold;" ID="LabelImpairedAmount" runat="server" ReadOnly="true" CssClass="labelImpairedAmount">
                                                <NumberFormat AllowRounding="true" KeepNotRoundedValue="false" DecimalDigits="2" />
                                            </telerik:RadNumericTextBox>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="6" SpanSm="12" SpanXs="12" Style="display: inline-block; padding-left: 0px;">
                                            <telerik:RadLabel Style="display: inline-block; font-size: 14px; min-width: 130px;" runat="server" ID="RadLabel13" AssociatedControlID="RadTextBox1" Text="Amortized Amount"></telerik:RadLabel>
                                            <telerik:RadNumericTextBox Style="padding-top: 0px; margin-top: -5px; display: inline-block; border: none; font-size: 16px;" ID="NumericTextBoxAmortizedAmount" runat="server" ReadOnly="true" CssClass="labelImpairedAmount">
                                                <NumberFormat AllowRounding="true" KeepNotRoundedValue="false" DecimalDigits="2" />
                                            </telerik:RadNumericTextBox>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="6" SpanSm="12" SpanXs="12" Style="display: inline-block; padding-right: 0px;">
                                            <telerik:RadLabel Style="display: inline-block; font-size: 14px; min-width: 130px;" runat="server" ID="RadLabel14" AssociatedControlID="RadTextBox1" Text="Cash Flows Amount"></telerik:RadLabel>
                                            <telerik:RadNumericTextBox Style="padding-top: 0px; margin-top: -5px; display: inline-block; border: none; font-size: 16px;" ID="NumericTextBoxCashFlowsAmount" runat="server" ReadOnly="true" CssClass="labelImpairedAmount">
                                                <NumberFormat AllowRounding="true" KeepNotRoundedValue="false" DecimalDigits="2" />
                                            </telerik:RadNumericTextBox>
                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="6" SpanSm="12" SpanXs="12" Style="display: inline-block; padding-left: 0px;">
                                            <telerik:RadLabel Style="display: inline-block; font-size: 14px; min-width: 130px;" runat="server" ID="RadLabel15" AssociatedControlID="RadTextBox1" Text="Present Value"></telerik:RadLabel>
                                            <telerik:RadNumericTextBox Style="padding-top: 0px; margin-top: -5px; display: inline-block; border: none; font-size: 16px;" ID="NumericTextBoxPresentValue" runat="server" ReadOnly="true" CssClass="labelImpairedAmount">
                                                <NumberFormat AllowRounding="true" KeepNotRoundedValue="false" DecimalDigits="2" />
                                            </telerik:RadNumericTextBox>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                            </Rows>
                            <Rows>
                                <telerik:LayoutRow RowType="Container" WrapperHtmlTag="Div" Style="padding: 5px 0px; text-align: center;">
                                    <Columns>
                                        <telerik:LayoutColumn Span="6" SpanSm="6" SpanXs="12" Style="padding: 0px;">
                                            <script>
                                                function RadConfirmNoCashflows(sender, args) {
                                                    var callBackFunction = function (shouldSubmit) {
                                                        if (shouldSubmit) {
                                                            //initiate the original postback again
                                                            sender.click();
                                                            if (Telerik.Web.Browser.ff) { //work around a FireFox issue with form submission, see http://www.telerik.com/support/kb/aspnet-ajax/window/details/form-will-not-submit-after-radconfirm-in-firefox
                                                                sender.get_element().click();
                                                            }
                                                        }
                                                    };

                                                    var text = "Are you sure you want to submit the page?";
                                                    radconfirm(text, callBackFunction, 500, 160, null, "Confirm");
                                                    //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                                                    args.set_cancel(true);
                                                }
                                            </script>
                                            <telerik:RadButton ID="ButtonNoCashflows" RenderMode="Lightweight" runat="server" Text="No Cash flows" OnClick="ButtonNoCashFlows_OnClick" OnClientClicking="RadConfirmNoCashflows" UseSubmitBehavior="false" Skin='<%#this.Skin%>' CssClass="buttonNoCashflows"></telerik:RadButton>

                                        </telerik:LayoutColumn>
                                        <telerik:LayoutColumn Span="6" SpanSm="6" SpanXs="12" Style="padding: 0px;">
                                            <script>
                                                function RadConfirmCompleteAllCashflows(sender, args) {
                                                    var callBackFunction = function (shouldSubmit) {
                                                        if (shouldSubmit) {
                                                            //initiate the original postback again
                                                            sender.click();
                                                            if (Telerik.Web.Browser.ff) { //work around a FireFox issue with form submission, see http://www.telerik.com/support/kb/aspnet-ajax/window/details/form-will-not-submit-after-radconfirm-in-firefox
                                                                sender.get_element().click();
                                                            }
                                                        }
                                                    };

                                                    var text = "Are you sure you want to submit the page?";
                                                    radconfirm(text, callBackFunction, 500, 160, null, "Confirm");
                                                    //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                                                    args.set_cancel(true);
                                                }
                                            </script>
                                            <telerik:RadButton ID="ButtonCompleteAllCashflows" RenderMode="Lightweight" runat="server" Text="Complete All Cash flows" OnClick="ButtonCompleteAllCashFlows_OnClick" OnClientClicking="RadConfirmCompleteAllCashflows" UseSubmitBehavior="false" Skin='<%#this.Skin%>' CssClass="buttonCompleteAllCashflows"></telerik:RadButton>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                            </Rows>
                        </telerik:RadPageLayout>

                    </telerik:LayoutColumn>

                    <telerik:LayoutColumn Span="5" SpanSm="12" SpanXs="12" Style="padding: 0px 0px 0px 1px;">
                        <telerik:RadGrid ID="GridCashFlows" runat="server" OnDeleteCommand="GridCashFlows_DeleteCommand" Skin='<%#this.Skin%>'>
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
                                    <telerik:GridButtonColumn ConfirmText="Delete this?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="FontIconButton" CommandName="Delete">
                                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:LayoutColumn>

                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</asp:Content>
