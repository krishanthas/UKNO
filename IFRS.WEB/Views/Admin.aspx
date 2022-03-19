<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Masters/HomeMasterPage.master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="IFRS.WEB.Views.Admin" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeMasterContent" runat="server">
<style type="text/css">
    .admin-cust{
    border:solid 1px gray;
    margin-bottom:5px;
    padding-top:5px;
    padding-bottom:5px;
}
</style>
    <telerik:RadWindowManager RenderMode="Lightweight" ID="windowManager1" runat="server" Style="z-index: 100001"></telerik:RadWindowManager>
    <telerik:RadPageLayout ID="RadPageLayout1" runat="server" Skin='<%#this.Skin%>'>
        <Rows>
            <telerik:LayoutRow RowType="Generic">
                <Columns>
                    <telerik:LayoutColumn CssClass="admin-cust">
                        <telerik:RadLabel ID="lblCustList" runat="server" Text="Customer list Download" Font-Bold="true" Width="80%"></telerik:RadLabel><br />
                        <telerik:RadLabel ID="RadLabel1" runat="server" Text="Download the csv file, make adjustments to NPA/PA statuses and use the below upload feature to update the system"  Font-Size="Small" Width="38%"></telerik:RadLabel><br />
                        <telerik:RadButton ID="btnDownload" UseSubmitBehavior="false" Style="line-height:12px" runat="server" Text="Download" OnClick="btnDownload_Click" Font-Size="Small"  Skin='<%#this.Skin%>'  RenderMode="Lightweight"></telerik:RadButton>
                        <br /><br />
                        <telerik:RadLabel ID="lblCustUpload" runat="server" Text="Customer list Upload" Font-Size="Small"></telerik:RadLabel>
                        <asp:FileUpload ID="fileUpload" runat="server" Font-Size="Smaller" />
                         <telerik:RadButton ID="btnUpload" runat="server" UseSubmitBehavior="false" Style="line-height:12px" Text="Upload" OnClick="btnUpload_Click" Font-Size="Small"  Skin='<%#this.Skin%>'  RenderMode="Lightweight"><ConfirmSettings ConfirmText="Are you sure you want to continue?" /></telerik:RadButton>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow RowType="Generic">
                <Columns>
                    <telerik:LayoutColumn  CssClass="admin-cust">
                        <telerik:RadLabel ID="RadLabel2" runat="server" Text="Select the Questions for Business or Individual" Font-Bold="true" Width="80%"></telerik:RadLabel><br />
                        <telerik:RadGrid ID="GridQuestions" runat="server"  Skin='<%#this.Skin%>' OnNeedDataSource="GridQuestions_NeedDataSource" OnUpdateCommand="GridQuestions_UpdateCommand">
                            <MasterTableView EditMode="PopUp" AutoGenerateColumns="False" TableLayout="Fixed" DataKeyNames="Id" EditFormSettings-PopUpSettings-KeepInScreenBounds="true">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="No" DataField="No" UniqueName="No" ReadOnly="true" HeaderStyle-Width="5%"><HeaderStyle Width="40" /></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Question" DataField="Text" UniqueName="Text" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn HeaderText="Status" DataField="Status" Visible="true" UniqueName="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"><HeaderStyle Width="80" /></telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn HeaderText="Status" UniqueName="StatusTemp">
                                        <HeaderStyle Width="80" />
                                        <ItemTemplate>
                                            <asp:label runat="server" ID="radTempStatus"  Text='<%# DataBinder.Eval(Container.DataItem, "Status").ToString().Equals("A") ? "Active" : "Disable" %>'  ></asp:label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Type" UniqueName="TemplateColumn" HeaderStyle-Width="20%">
                                        <ItemTemplate>
                                            <telerik:RadRadioButtonList SelectedValue='<%# DataBinder.Eval(Container.DataItem, "BI_Type") %>'
                                                ID="RadRadioButtonList" runat="server"
                                                CssClass="question-writable"
                                                AutoPostBack="false"
                                                QuestionId='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                                                Layout="Flow" Columns="3" Direction="Horizontal" HeaderStyle-HorizontalAlign="Center">
                                                
                                                <Items>
                                                    <telerik:ButtonListItem Text="Business" Value="B" />
                                                    <telerik:ButtonListItem Text="Individual" Value="I" />
                                                    <telerik:ButtonListItem Text="All" Value="A" />
                                                </Items>
                                            </telerik:RadRadioButtonList>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="BI_Type" Display="false" DataField="BI_Type" HeaderText="BI_Type"></telerik:GridBoundColumn>
                                    <telerik:GridEditCommandColumn HeaderText="Edit" UniqueName="EditCommandColumn" ButtonType="FontIconButton" ><HeaderStyle Width="40" /></telerik:GridEditCommandColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <PopUpSettings KeepInScreenBounds="true" />
                                    <FormTemplate>
                                        <table id="Table2" cellspacing="4" cellpadding="10" width="100%" border="0" rules="none" style="border-collapse: collapse; text-align:left;">
                                            <tr class="EditFormHeader">
                                                <td style="vertical-align:top" colspan="2"><b>Edit</b></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top" class="title">Question Text</td>
                                                <td style="vertical-align:top"><asp:TextBox ID="txtQText" TextMode="MultiLine" Columns="5" Width="280px" Rows="4" runat="server" Text='<%# Bind("Text") %>'></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top" class="title">Visibility</td>
                                                <td style="vertical-align:top">
                                                    <telerik:RadRadioButtonList SelectedValue='<%# Bind("Status") %>'
                                                                                        ID="RadRadioButtonList" runat="server"
                                                                                        CssClass="question-writable"
                                                                                        AutoPostBack="false"
                                                                                        Layout="Flow" Columns="2" Direction="Horizontal" HeaderStyle-HorizontalAlign="Center">
                                                        <Items>
                                                            <telerik:ButtonListItem Text="On" Value="A" />
                                                            <telerik:ButtonListItem Text="Off" Value="D" />
                                                        </Items>
                                                    </telerik:RadRadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align:top" class="title">Type</td>
                                                <td style="vertical-align:top">
                                                    <telerik:RadRadioButtonList SelectedValue='<%# Bind("BI_Type") %>'
                                                        ID="RadRadioButtonList2" runat="server"
                                                        CssClass="question-writable"
                                                        AutoPostBack="false"
                                                        Layout="Flow" Columns="3" Direction="Horizontal" HeaderStyle-HorizontalAlign="Center">
                                                        <Items>
                                                            <telerik:ButtonListItem Text="Business" Value="B" />
                                                            <telerik:ButtonListItem Text="Individual" Value="I" />
                                                            <telerik:ButtonListItem Text="All" Value="A" />
                                                        </Items>
                                                    </telerik:RadRadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                        runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                        CommandName="Cancel"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </FormTemplate>
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
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

                                var text = "Are you sure you want to refresh the page?";
                                radconfirm(text, callBackFunction, 500, 160, null, "Confirm");
                                //always prevent the original postback so the RadConfirm can work, it will be initiated again with code in the callback function
                                args.set_cancel(true);
                            }
                        </script>
                        <telerik:RadButton ID="btnClear" runat="server" Text="Refresh" OnClick="btnClear_Click" Style="float: right; margin: 10px 10px;" SingleClick="true" OnClientClicking="ConfirmClear" SingleClickText="Processing..." UseSubmitBehavior="false" Skin='<%#this.Skin%>' CssClass="button-questions-clear" RenderMode="Lightweight"></telerik:RadButton>
                        <telerik:RadButton ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Style="float: right; margin: 10px 10px;" SingleClick="true" OnClientClicking="ConfirmSubmit" SingleClickText="Processing..." UseSubmitBehavior="false" Skin='<%#this.Skin%>' CssClass="button-questions-submit" RenderMode="Lightweight"></telerik:RadButton>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow RowType="Generic">
                <Columns>
                    <telerik:LayoutColumn style="padding: 1% 1%; float:right">
                        
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</asp:Content>
