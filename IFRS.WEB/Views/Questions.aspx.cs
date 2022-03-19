using IFRS.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using static IFRS.DAL.Constants;

namespace IFRS.WEB.Views {
    public partial class Questions : Models.BasePage {

        private Customer customer;
        private List<Question> questions;
        protected void Page_Load(object sender, EventArgs e) {
            SetAccess();

            Initialize();
        }

        private string GetCustomerId() => Request.QueryString["customerId"];

        private void Initialize() {

            if (!string.IsNullOrEmpty(Request.QueryString["customerId"])) {
                string customerId = GetCustomerId();
                var user = DataContext.GetUserById(UserId);

                customer = DataContext.GetCustomerById(customerId);

                this.SetSummeryLayout(customer);

                if(customer != null) {
                    this.AsAtDate = customer.AsAtDate.GetValueOrDefault();
                    this.InitializeQuestions(customer.Id.Trim());

                    

                    this.Instruction = $"PLEASE EVALUATE THE CUSTOMER {customer.Id} - {customer.Name} ({customer.CapitalOSLKR:n})";

                    if (!IsPostBack)
                    {
                        
                        RadTextRemarks.Text = customer.AnswerRemark;
                        RadLabelRemarks.Text = customer.AnswerRemark;
                    }

                    var remarks = DataContext.GetRemarksByCustomerId(customer.Id);
                    var isValidRole = this.UserRole == DEM.UserRole.ENTERING || this.UserRole == DEM.UserRole.VERIFY_4;

                    if (remarks.Any() && isValidRole) {
                        this.GridRemarks.Visible = true;
                        this.GridRemarks.DataSource = remarks;
                    }

                }

            }

        }

        private void SetSummeryLayout(Customer customer) {
            this.Label1SumOfImpairedAmount.Text = $"{customer.TotalImpairmentAmount.GetValueOrDefault():n}";
            this.LabelSumOfPresentValue.Text = $"{customer.TotalPresentValue.GetValueOrDefault():n}";
            var sum = 0M;
            if (customer.TotalPresentValue.GetValueOrDefault() > 0)
            {
                var cashFlows = DataContext.GetCashFlowsByCustomerId(customer.Id);
                sum = cashFlows.Select(i => i.Amount).DefaultIfEmpty().Sum().GetValueOrDefault(0m);
            }
            this.LabelSumOfCashFlows.Text = $"{sum:n}";
        }

        private void InitializeQuestions(string customerId, bool canRebind = false) {
            questions = DataContext.GetQuestionsBy(customerId);
            this.GridQuestions.DataSource = questions.Select(i => new { i.No, i.Text, i.Answer, isReadOnly = true });

            //RadTextRemarks.Visible = false;
            //if (questions.Any(a => a.Answer!=null && a.Answer.Equals("Y")))
            //{
            //    RadTextRemarks.Visible = true;
            //}

            if (canRebind) {
                this.GridQuestions.Rebind();
            }
        }

        private List<Question> GetAllAnsweredForCustomerType(List<Question> qs,Dictionary<int,string> answers, string custype)
        {
            List<Question> allAnswers = new List<Question>();
            var allCustRelatedQuests = qs.Where(a => a.BI_Type.Equals(custype) || a.BI_Type.Equals("A"));
            foreach (var cQ in allCustRelatedQuests)
            {
                string ans = null;
                answers.TryGetValue((int)cQ.No,out ans);

                allAnswers.Add(new Question
                {
                    Answer = ans,
                    BI_Type = cQ.BI_Type,
                    Id = cQ.Id,
                    No = cQ.No,
                    Status = cQ.Status,
                    Text = cQ.Text
                });
            }

            return allAnswers;
        }

        protected void RadGrid1_ColumnCreated(object sender, GridColumnCreatedEventArgs e) {
            e.Column.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            e.Column.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            string customerId = Request.QueryString["customerId"];


            var questionsAndAnswers = this.GridQuestions.Controls.All().OfType<RadRadioButtonList>().ToDictionary(i => Convert.ToInt32(i.Attributes["QuestionNo"]), i => i.SelectedValue);

            var allQAndAofCustomer = GetAllAnsweredForCustomerType(questions, questionsAndAnswers, customer.Customer_Type);

            
            var isEmptyAny = allQAndAofCustomer.Any(i => string.IsNullOrEmpty(i.Answer) || (i.Answer != "Y" && i.Answer != "N"));
            
            var remarksText = RadTextRemarks.Text.Trim();
            

            if (!isEmptyAny) 
            {
                var result = DataContext.UpdateQuestionsAnswersBy(customerId, allQAndAofCustomer, UserId, UserRole,remarksText);

                Response.Redirect($"~/Views/Home.aspx");

            }
            else
            {
                var result = DataContext.UpdateQuestionsAnswersBy(customerId, allQAndAofCustomer, UserId, UserRole, remarksText);
                this.InitializeQuestions(customerId, true);
                this.Notification.Text = "Please fill all the questions!";
                this.Notification.Show();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e) {

            var isCleared = true;
            foreach (GridDataItem item in this.GridQuestions.MasterTableView.Items) {
                var answer = item["Answer"].Text;
                isCleared = answer == null;
                if (!isCleared) {
                    break;
                }
            }

            if (!isCleared) {
                string customerId = GetCustomerId();
                var user = DataContext.GetUserById(UserId);
                if (user != null) {
                    var result = DataContext.CustomerQuestionsClearBy(customerId);
                    if (result) {
                        this.InitializeQuestions(customerId, true);
                    }
                }
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e) {

            bool result = false;
            string radValue = radioFinanceVerify.SelectedValue;

            var customerId = GetCustomerId();

            if (UserRole == DEM.UserRole.VERIFY_4
                    && radValue.Equals(FinanceApproveTypes.WITH_OBJECT_EVI_WITH_CASHFLOW))
            {
                result = DataContext.UpdateApprovalAtVerifyLevel(UserId, UserRole, customerId, cashflowStatus: FinanceApproveTypes.WITH_OBJECT_EVI_WITH_CASHFLOW);
            }
            else
            {
                result = DataContext.UpdateApprovalAtVerifyLevel(UserId, UserRole, customerId, cashflowStatus: "");
            }

            if (result) {
                base.GoToHome();
            }
        }

        protected void btnReject_Click(object sender, EventArgs e) {
            var customerId = GetCustomerId();
            var result = DataContext.UpdateRejectAtVerifyLevel(UserId, UserRole, customerId, string.Empty);
            if (result) {
                base.GoToHome();
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument) {
            if (source == this.ButtonReject) {
                var customerId = GetCustomerId();

                try
                {
                    var result = DataContext.UpdateRejectAtVerifyLevel(UserId, UserRole, customerId, eventArgument);
                    if (result)
                    {
                        base.GoToHome();
                    }
                }
                catch (Exception ex)
                {

                }

            }
            else {
                //call the RaisePostBack event 
                base.RaisePostBackEvent(source, eventArgument);
            }
        }

        protected void radioButtonList1_SelectedIndexChanged(object sender, EventArgs e) {
            //AutoPostBack="true" 
            //OnSelectedIndexChanged = "radioButtonList1_SelectedIndexChanged"
            var s = sender as RadRadioButtonList;
            var value = s.SelectedValue;

            if(s.NamingContainer is GridDataItem gridDataItem) {
                var questionId = gridDataItem.GetDataKeyValue("Id").ToString();
                var customerId = GetCustomerId();

                var result = DataContext.UpdateQuestionAnswerBy(customerId, questionId, value);
            }
        }

        protected void btnViewAccounts_Click(object sender, EventArgs e) {
            string customerId = Request.QueryString["customerId"];
            Response.Redirect($"~/Views/Accounts.aspx?customerId={customerId}");
        }

        private void SetAccess() {

        }

        protected void GridQuestions_PreRender(object sender, EventArgs e)
        {
            if(questions!=null)
                for (int i = 0; i < questions.Count; i++)
                {
                    bool isEnabled = questions[i].BI_Type.Equals(customer.Customer_Type) || questions[i].BI_Type.Equals("A");
                    GridQuestions.Items[i].Enabled = isEnabled;

                    if(i>0)
                        if (questions[i-1].No == 1 && questions[i - 1].Answer!=null && questions[i-1].Answer.Equals("Y"))
                        {
                            GridQuestions.Items[i].Enabled = true;
                        }
                }
            
        }

    }
}