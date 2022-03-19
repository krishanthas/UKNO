<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="IFRS.WEB.Views.Questions" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeMasterContent" runat="server">
    
    <telerik:RadWindowManager RenderMode="Lightweight" ID="windowManager1" runat="server" Style="z-index: 100001"></telerik:RadWindowManager>
    <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Skin='<%#this.Skin%>'>
        <Rows>
            <telerik:LayoutRow RowType="Generic">
                <Columns>
                    <telerik:LayoutColumn Style="text-align: center;">
                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                            <script type="text/javascript">
                                function CheckRadio_DisplayRemarks() {
                                    //var selected = args.get_item().get_text();
                                    var radRemarks = $find("<%=RadTextRemarks.ClientID %>");
                                    var lblSICR = $find("<%=RadTextSICR.ClientID %>");


                                    var yesCounts = 0;

                                    var masterTable = $find("<%=GridQuestions.ClientID %>").get_masterTableView();
                                    var questItems = masterTable.get_dataItems();

                                    for (var i = 0; i < questItems.length; i++) {                   // looping throught all items

                                        rdlist = questItems[i].findControl("RadRadioButtonList");

                                        if (rdlist.get_selectedIndex() == 0) {  //YES
                                            questItems[i].get_element().style.backgroundColor = '#f11';
                                            questItems[i].get_element().style.color = '#fff';

                                            if (i > 1) { yesCounts++; }     // omitting first 2 questions

                                            //enable Q2 based on Q1
                                            if (i == 0) {
                                                questItems[i + 1].findControl("RadRadioButtonList").set_enabled(true);
                                            }
                                        } else {                                //NO
                                            questItems[i].get_element().style.backgroundColor = 'transparent';
                                            questItems[i].get_element().style.color = '#000';

                                            //disable Q2 based on Q1
                                            if (i == 0) {
                                                questItems[i + 1].get_element().style.backgroundColor = 'transparent';
                                                questItems[i + 1].get_element().style.color = '#000';

                                                questItems[i + 1].findControl("RadRadioButtonList").set_enabled(false);
                                                var ss = questItems[i + 1].findControl("RadRadioButtonList").get_items();
                                                ss[0].set_selected(false);
                                                ss[1].set_selected(false);
                                                
                                            }
                                        }
                                                                               

                                    }

                                    //console.info("Yes counts", yesCounts);
                                    if (yesCounts > 0) {
                                        radRemarks.set_visible(true);
                                        lblSICR.set_value("SICR - Yes");
                                    } else {
                                        radRemarks.set_visible(false);
                                        lblSICR.set_value("SICR - No");
                                    }
                                }

                                window.onload = (event) => {
                                    CheckRadio_DisplayRemarks();
                                };

                                function RadRadioItemClicked(sender, args) {
                                    CheckRadio_DisplayRemarks();
                                }
                            </script>
                        </telerik:RadCodeBlock>
                        <telerik:RadGrid ID="GridQuestions" runat="server" OnPreRender="GridQuestions_PreRender"  OnColumnCreated="RadGrid1_ColumnCreated" Skin='<%#this.Skin%>'>
                            <ClientSettings>
                                <%--<Scrolling AllowScroll="true" ScrollHeight="300px" UseStaticHeaders="true" />--%>
                            </ClientSettings>
                            
                            <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed" DataKeyNames="No">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="No" DataField="No" UniqueName="No" HeaderStyle-Width="5%"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Question" DataField="Text" UniqueName="Text" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderStyle-Width="20%">
                                        <ItemTemplate>
                                            <% if (this.UserRole == IFRS.DEM.UserRole.ENTERING) {%>
                                            <telerik:RadRadioButtonList SelectedValue='<%# DataBinder.Eval(Container.DataItem, "Answer") %>'
                                                ID="RadRadioButtonList" runat="server"
                                                CssClass="question-writable"
                                                AutoPostBack="false"
                                                QuestionNo='<%# DataBinder.Eval(Container.DataItem, "No") %>'
                                                Layout="Flow" Columns="2" Direction="Horizontal" HeaderStyle-HorizontalAlign="Center">
                                                <ClientEvents OnItemClicked="RadRadioItemClicked" />
                                                <Items>
                                                    <telerik:ButtonListItem Text="Yes" Value="Y" />
                                                    <telerik:ButtonListItem Text="No" Value="N" />
                                                </Items>
                                            </telerik:RadRadioButtonList>
                                            <% }
                                                else { %>
                                            <label style="margin-bottom: 0px; <%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "Answer")) == "Y")? "color:red;": string.Empty %>"><%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "Answer")) == "Y")? "Yes" : "No" %></label>
                                            <% } %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="Answer" Display="false" DataField="Answer" HeaderText="Answer"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                      </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow RowType="Generic">
                <Columns>
                    <telerik:LayoutColumn style="text-align: center;margin-top: 10px;">
                        <style>
                            .remarks-grid .rgHeader{
                                background-color:red !important;
                                color:white !important;
                                font-weight:bold;
                            }
                            .radTextWidth + input,select,textarea{
                                max-width:unset;
                            }
                        </style>
                        <telerik:RadGrid ID="GridRemarks" runat="server" Skin='<%#this.Skin%>' CssClass="remarks-grid" Visible="false">
                            <MasterTableView AutoGenerateColumns="False" TableLayout="Fixed" DataKeyNames="Id">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Id" DataField="Id" UniqueName="Id" HeaderStyle-Width="5%" Display="false"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Remarks" DataField="Remarks" UniqueName="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Remarked At" DataField="RcReTime" UniqueName="RcReTime">
                                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow RowType="Generic">
                <Columns>
                    <telerik:LayoutColumn Span="6" style="padding: 1% 1%;">
                        <% if (this.UserRole == IFRS.DEM.UserRole.ENTERING)
                            {%>
                        <telerik:RadTextBox runat="server"  Skin='<%#this.Skin%>' ID="RadTextSICR" RenderMode="Classic" Font-Size="Medium" Enabled="false" BorderStyle="None" Font-Bold="true" Width="1000px" ></telerik:RadTextBox>
                        <telerik:RadTextBox CssClass="radTextWidth" Skin='<%#this.Skin%>' RenderMode="Classic" TextMode="MultiLine" Rows="3" Wrap="true" runat="server" ClientIDMode="Static" ID="RadTextRemarks" Width="1000px"  EmptyMessage="Remarks">
                        </telerik:RadTextBox>

                        
                        <% } else {%>
                        <telerik:RadLabel runat="server"  Skin='<%#this.Skin%>' ID="RadLabelRemarks" RenderMode="Classic" Font-Size="Smaller" Width="1000px" ></telerik:RadLabel>
                        <%} %>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="6" style="padding: 1% 1%; text-align:right;">
                        <% if (this.UserRole == IFRS.DEM.UserRole.ENTERING) {%>
                        <script>
                            function ConfirmSubmit(sender, args) {
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

                            function ConfirmClear(sender, args) {
                                var callBackFunction = function (shouldSubmit) {
                                    if (shouldSubmit) {
                                        //initiate the original postback again
                                        sender.click();
                                        if (Telerik.Web.Browser.ff) { //work around a FireFox issue with form submission, see http://www.telerik.com/support/kb/aspnet-ajax/window/details/form-will-not-submit-after-radconfirm-in-firefox
                                            sender.get_element().click();
                                        }
                                    }
                                };

                                var text = "Are you sure you want to clear the page?";
                                radconfirm(text, callBackFunction, 500, 160, null, "Confirm");
                                //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                                args.set_cancel(true);
                            }
                        </script>
                        <telerik:RadButton ID="RadButton2" runat="server" Text="Clear" OnClick="btnClear_Click" SingleClick="true" OnClientClicking="ConfirmClear" SingleClickText="Processing..." UseSubmitBehavior="false" Skin='<%#this.Skin%>' CssClass="button-questions-clear" RenderMode="Lightweight"></telerik:RadButton>
                        <telerik:RadButton ID="RadButton1" runat="server" Text="Submit" OnClick="btnSubmit_Click" SingleClick="true" OnClientClicking="ConfirmSubmit" SingleClickText="Processing..." UseSubmitBehavior="false" Skin='<%#this.Skin%>' CssClass="button-questions-submit" RenderMode="Lightweight"></telerik:RadButton>

                        <% } else  { %>
                        <telerik:RadPageLayout ID="PageLayout12" runat="server" Skin='<%#this.Skin%>'>
                            <Rows>
                                <%--<telerik:LayoutRow WrapperHtmlTag="Div">
                                    <Columns>
                                        <telerik:LayoutColumn Span="4" SpanSm="6" SpanXs="12" Style="float: right;">
                                            <telerik:RadButton ID="ButtonViewAccounts" OnClick="btnViewAccounts_Click" runat="server" UseSubmitBehavior="false" Text="View Accounts"></telerik:RadButton>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>--%>
                                <telerik:LayoutRow WrapperHtmlTag="Div">
                                    <Columns>
                                        <telerik:LayoutColumn Span="4" SpanSm="6" SpanXs="12" Style="float: right;">
                                            <telerik:RadLabel runat="server" ID="RadLabel11" AssociatedControlID="RadTextBox1" Text="Sum Of Impaired Amount"></telerik:RadLabel>
                                            <telerik:RadLabel runat="server" ID="Label1SumOfImpairedAmount" AssociatedControlID="RadTextBox1" Text=""></telerik:RadLabel>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                                <telerik:LayoutRow WrapperHtmlTag="Div">
                                    <Columns>
                                        <telerik:LayoutColumn Span="4" SpanSm="6" SpanXs="12" Style="float: right;">
                                            <telerik:RadLabel runat="server" ID="RadLabel1" AssociatedControlID="RadTextBox1" Text="Sum Of Present Value"></telerik:RadLabel>
                                            <telerik:RadLabel runat="server" ID="LabelSumOfPresentValue" AssociatedControlID="RadTextBox1" Text=""></telerik:RadLabel>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                                <telerik:LayoutRow WrapperHtmlTag="Div">
                                    <Columns>
                                        <telerik:LayoutColumn Span="4" SpanSm="6" SpanXs="12" Style="float: right;">
                                            <telerik:RadLabel runat="server" ID="RadLabel2" AssociatedControlID="RadTextBox1" Text="Sum Of Cash Flows"></telerik:RadLabel>
                                            <telerik:RadLabel runat="server" ID="LabelSumOfCashFlows" AssociatedControlID="RadTextBox1" Text=""></telerik:RadLabel>
                                        </telerik:LayoutColumn>
                                    </Columns>
                                </telerik:LayoutRow>
                            </Rows>
                        </telerik:RadPageLayout>
                        
                        <script>
                            
                            function ConfirmApprove(sender, args) {
                                var callBackFunction = function (shouldSubmit) {
                                    if (shouldSubmit) {
                                        //initiate the original postback again
                                        sender.click();
                                        if (Telerik.Web.Browser.ff) { //work around a FireFox issue with form submission, see http://www.telerik.com/support/kb/aspnet-ajax/window/details/form-will-not-submit-after-radconfirm-in-firefox
                                            sender.get_element().click();
                                        }
                                    }
                                };

                                var text = "Are you sure you want to approve the entry?";
                                radconfirm(text, callBackFunction, 500, 160, null, "Confirm");
                                //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                                args.set_cancel(true);
                            }

                            function ConfirmReject(sender, args) {
                                var callBackFunction = function (arg) {
                                    if (arg) {
                                        //initiate the original postback again
                                        sender.click();
                                        __doPostBack('<%= ButtonReject.UniqueID %>', arg);
                                        if (Telerik.Web.Browser.ff) { //work around a FireFox issue with form submission, see http://www.telerik.com/support/kb/aspnet-ajax/window/details/form-will-not-submit-after-radconfirm-in-firefox
                                            sender.get_element().click();
                                        }
                                    }
                                    else {
                                        alert("Please enter valid rejection reason");
                                    }
                                };

                                var text = "Are you sure you want to reject this entry?? Please enter reason";
                                //radconfirm(text, callBackFunction, 500, 160, null, "Confirm");

                                var reason = radprompt(text, callBackFunction, 350, 230, null, 'Confirm rejection?', null);

                                //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                                args.set_cancel(true);
                            }
                        </script>
                            
                            <%  if (this.UserRole == IFRS.DEM.UserRole.VERIFY_4) {%>
                                <telerik:RadRadioButtonList 
                                    ID="radioFinanceVerify" runat="server"
                                    CssClass="question-writable"
                                    AutoPostBack="false"
                                    Layout="Flow" Columns="1" Direction="Vertical">
                                    <ClientEvents OnItemClicked="RadRadioItemClicked" />
                                    <Items>
                                        <telerik:ButtonListItem Text="No objective Evidence - Complete" Value="1" Selected="true" />
                                        <telerik:ButtonListItem Text="With Objective Evidence - No need of Cash flows" Value="2" />
                                        <telerik:ButtonListItem Text="With Objective Evidence - Need to enter Cash flows" Value="3" />
                                    </Items>
                                </telerik:RadRadioButtonList>
                            <% } %>
                        <telerik:RadButton ID="ButtonReject" runat="server" Text="Reject" OnClick="btnReject_Click" SingleClick="true" OnClientClicking="ConfirmReject" SingleClickText="Processing..." UseSubmitBehavior="false" Skin='<%#this.Skin%>' CssClass="button-questions-reject" RenderMode="Lightweight"></telerik:RadButton>
                        <telerik:RadButton ID="ButtonApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" SingleClick="true" OnClientClicking="ConfirmApprove" SingleClickText="Processing..." UseSubmitBehavior="false" Skin='<%#this.Skin%>' CssClass="button-questions-approve" RenderMode="Lightweight"></telerik:RadButton>
                        <% } %>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</asp:Content>
