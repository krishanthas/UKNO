using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using IFRS.DEM;
using static IFRS.DAL.Constants;

namespace IFRS.DAL {
    public static class DataContext {
        public static class Constants {
            public const string CashFlowsImpairmentStatusInProgress = "In-Progress";

            public const string CashFlowsImpairedNoCashFlows = "Impaired Yes - No Cash Flows";
            public const string CashFlowsImpairedWithCashFlows = "Impaired Yes - With Cash Flows";

            public const string CashFlowsNotImpairedNoCashFlows = "Impaired No - No Cash Flows";
            public const string CashFlowsNotImpairedWithCashFlows = "Impaired No - With Cash Flows";
        }



        public static string GetLastRejectReason(string customerId,string statusCode)
        {
            using (var context = new Entities())
            {

                string remark = context.Remarks.Where(r => r.CustomerId.Equals(customerId) && r.DelFlag.Equals("N"))
                                                .OrderByDescending(a => a.RcReTime)
                                                .FirstOrDefault()?.Remarks ?? "";

                switch (statusCode)
                {
                    case StatusCodes.CORRECTED_TO_BM_BY_ENTERING:
                    case StatusCodes.CORRECTED_TO_AM_BY_ENTERING:
                    case StatusCodes.CORRECTED_TO_AGM_BY_ENTERING:
                    //case StatusCodes.APPROVED_BY_BM:
                    case StatusCodes.CORRECTED_APPROVED_TO_BM_BY_BM:
                    case StatusCodes.CORRECTED_APPROVED_TO_AM_BY_BM:
                    case StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_BM:
                    //case StatusCodes.APPROVED_BY_AM:
                    case StatusCodes.CORRECTED_APPROVED_TO_AM_BY_AM:
                    case StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_AM:
                    //case StatusCodes.APPROVED_BY_AGM:
                    case StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_AGM:
                    //case StatusCodes.APPROVED_BY_FINANCE:
                        remark = string.Empty;break;
                }

                return remark??"";
            }
        }

        public static decimal? GetImpairment(string customerId)
        {
            using (var context = new Entities())
            {

                var accounts = context.Accounts.Where(a => a.CustomerId.Equals(customerId) && a.DelFlag.Equals("N"))
                                        .Sum(a => a.ImpairmentAmount * a.Currency_Rate) ;

                return accounts??0M;
            }
        }

        public static string GetFinalSICR(Customer c)
        {
            var res =
                //(string.IsNullOrEmpty(c.q1) ? false : c.q1.Equals("Y")) ||        // removing Q1 and Q2 to tally with new logic
                //(string.IsNullOrEmpty(c.q2) ? false : c.q2.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q3) ? false : c.q3.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q4) ? false : c.q4.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q5) ? false : c.q5.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q6) ? false : c.q6.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q7) ? false : c.q7.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q8) ? false : c.q8.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q9) ? false : c.q9.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q10) ? false : c.q10.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q11) ? false : c.q11.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q12) ? false : c.q12.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q13) ? false : c.q13.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q14) ? false : c.q14.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q15) ? false : c.q15.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q16) ? false : c.q16.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q17) ? false : c.q17.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q18) ? false : c.q18.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q19) ? false : c.q19.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q20) ? false : c.q20.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q21) ? false : c.q21.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q22) ? false : c.q22.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q23) ? false : c.q23.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q24) ? false : c.q24.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q25) ? false : c.q25.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q26) ? false : c.q26.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q27) ? false : c.q27.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q28) ? false : c.q28.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q29) ? false : c.q29.Equals("Y")) ||
                (string.IsNullOrEmpty(c.q30) ? false : c.q30.Equals("Y"));

            return res ? "Yes" : "No";
                
        }

        public static AccessEntry GetAccessEntry(string statusCode, string impairmentStatus, UserRole userRole)
        {
            var ret = DEM.Constants.AccessRules.GetValueOrDefault(new KeyPair<UserRole, string>(userRole, statusCode), new AccessEntry());

            if (impairmentStatus.Equals("In-Progress"))
            {
                ret = new AccessEntry()
                {
                    Class = "customer-in-progress",
                    Description = impairmentStatus,
                    Enabled = ret.Enabled,
                    State = ret.State,
                    SortOrder = 2
                };
            }

            return ret;
        }

        public static List<CustomerRow> GetCustomers(string userId, string solId,UserRole userRole)
        {
            var customers = GetPendingCustomersByUserId(userId, solId, userRole);
            return customers.Select(c => {
                var entry = GetAccessEntry(c.StatusCode, c.ImpairmentStatus,userRole);

                return new CustomerRow
                {
                    Id = c.Id,
                    Name = c.Name,
                    CapitalOSLKR = c.CapitalOSLKR,
                    ImpairmentAmount = c.TotalImpairmentAmount,
                    CashFlowsAmount = c.TotalCashFlowsAmount,
                    AmortizedAmount = c.TotalAmortizedAmount,
                    PresentValue = c.TotalPresentValue,
                    FinalSICR = GetFinalSICR(c),
                    NPA_PA = c.Npa_Pa??"N/A",
                    MoraStatus = c.Mora_Status??"N/A",
                    Impairment = GetImpairment(c.Id),
                    LastRejectReason = GetLastRejectReason(c.Id,c.StatusCode),
                    StatusCode = c.StatusCode,
                    Status = entry.Description,
                    className = entry.Class,
                    SortOrder = entry.SortOrder,
                    AsAtDate = c.AsAtDate,
                    Remark = c.AnswerRemark
                };
            }).ToList();
        }


        public static bool UpdateRemarks( string customerId, string remarkText)
        {
            using (var context = new Entities())
            {
                var timestamp = DateTime.Now;
                var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(customerId));
                if (customer != null)
                {

                    if (!string.IsNullOrEmpty(remarkText))
                    {
                        customer.AnswerRemark = remarkText;
                    }

                }
                return context.SaveChanges() > 0;
            }
        }

        //public static User GetUserById(string id) {
        //    using (var context = new Entities()) {

        //        try
        //        {
        //            var user = context.Users.FirstOrDefault(i => i.Id.Equals(id));

        //            return user;
        //        }
        //        catch(Exception ex)
        //        {
        //            return null;
        //        }
        //    }

        //}

        public static User GetUserById(string id)
        {
            using (var context = new Entities())
            {
                try
                {
                    var cmd = context.Database.Connection.CreateCommand();
                    cmd.CommandText = string.Format(@"SELECT * FROM app_ifrs_users where ""Id"" = '{0}'", id);
                    context.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    return (context as IObjectContextAdapter).ObjectContext.Translate<User>(reader).FirstOrDefault();
                    //var user = context.Users.FirstOrDefault(i => i.Id.Equals(id));
                    //return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    context.Database.Connection.Close();
                }
            }
        }

        public static UserRole GetUserRoleById(string userId, string solId) {
            var ret = UserRole.UNAUTHORIZED;
            using (var context = new Entities()) {
                var status = false;
                status = context.Customers.GroupBy(i => i.Verify_4).Any(i => i.Key.Equals(userId));
                if (status) {
                    ret = UserRole.VERIFY_4;
                    goto end;
                }
                status = context.Customers.GroupBy(i => i.Verify_3).Any(i => i.Key.Equals(userId));
                if (status) {
                    ret = UserRole.VERIFY_3;
                    goto end;
                }
                status = context.Customers.GroupBy(i => i.Verify_2).Any(i => i.Key.Equals(userId));
                if (status) {
                    ret = UserRole.VERIFY_2;
                    goto end;
                }
                status = context.Customers.GroupBy(i => i.Verify_1).Any(i => i.Key.Equals(userId));
                if (status) {
                    ret = UserRole.VERIFY_1;
                    goto end;
                }
                status = context.Customers.GroupBy(i => i.RelationshipManager).Any(i => i.Key.Equals(userId));
                if (status) {
                    ret = UserRole.ENTERING;
                    goto end;
                }

                if (int.TryParse(solId, out var solIdInt) && solIdInt <= 200) {
                    ret = UserRole.ENTERING;
                }
                else {
                    ret = UserRole.UNAUTHORIZED;
                }
            end:
                status = true;
            }
            return ret;
        }

        public static List<Customer> GetPendingCustomersByUserId(string userId, string solId, DEM.UserRole userRole) {
            using (var context = new Entities()) {
                var customers = new List<Customer>();
                Expression<Func<Customer, bool>> predicate = null;
                switch (userRole) {
                    case UserRole.ENTERING: {
                            predicate = i => i.BranchCode.Contains(solId) || i.RelationshipManager.Contains(userId);
                            //var isBranchUser = Convert.ToInt32(solId) < 200;
                            //if (isBranchUser) {
                            //    var pattern = $"{solId.Trim().PadLeft(3, '0')}MGR";
                            //    predicate = i => i.BranchCode.Contains(solId);
                            //}
                            //else {
                            //    var pattern = userId;
                            //    predicate = i => i.RelationshipManager.Contains(pattern);
                            //}
                        }
                        break;
                    case UserRole.VERIFY_1: {
                            predicate = i => i.Verify_1.Contains(userId);
                        }
                        break;
                    case UserRole.VERIFY_2: {
                            predicate = i => i.Verify_2.Contains(userId);
                        }
                        break;
                    case UserRole.VERIFY_3: {
                            predicate = i => i.Verify_3.Contains(userId);
                        }
                        break;
                    case UserRole.VERIFY_4: {
                            predicate = i => i.Verify_4.Contains(userId);
                        }
                        break;
                }

                if (predicate != null) {
                    var asATDate = context.Customers.Max(i => i.AsAtDate).GetValueOrDefault().Date;
                    customers = context.Customers
                        .Where(predicate)
                        .Where(i => i.DelFlag.Equals("N") && i.AsAtDate == asATDate)
                        //.OrderByDescending(i => i.StatusCode)
                        .AsEnumerable()
                        .Select(i => {
                            if (string.IsNullOrEmpty(i.StatusCode) || i.StatusCode.Equals("Pending")) {
                                i.StatusCode = "00";
                            }
                            return i;
                        })
                        .ToList();
                }
                //if (asAtDate.HasValue) {
                //    customers = context.Customers.Where(i => i.EntryUserId.Equals(userId) && i.AsAtDate.Equals(asAtDate));
                //}
                //else {
                //    customers = context.Customers.Where(i => i.EntryUserId.Equals(userId));
                //}

                return customers;
            }

        }

        


        public static Account GetAccountById(string accountId) {
            using (var context = new Entities()) {
                var accounts = context.Accounts.FirstOrDefault(i => i.AccountNumber.Equals(accountId));

                return accounts;
            }
        }

        public static Customer GetCustomerById(string customerId) {
            using (var context = new Entities()) {
                var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(customerId));

                return customer;
            }
        }

        public static bool UpdateQuestionsSubmit(string userId, string customerId) {
            using (var context = new Entities()) {
                var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(customerId));

                if (customer != null) {
                    var questions = GetQuestionsBy(customerId);
                    var isAnyHasYesAnswered = questions.Any(i => i.Answer.Equals("Y"));

                    if (isAnyHasYesAnswered) {
                        customer.ImpairmentStatus = "PENDING";
                    }
                    else {
                        customer.ImpairmentStatus = "NOT-IMPAIRED";
                    }
                    customer.EntryUserId = userId;
                }

                return context.SaveChanges() > 0;
            }
        }

        public static List<Account> GetAccountsByCustomerId(string customerId) {
            using (var context = new Entities()) {
                var asATDate = context.Accounts.Max(i => i.AsAtDate);
                try
                {
                    var accounts = context.Accounts
                                .Where(i => i.CustomerId.Equals(customerId)
                                && i.AsAtDate == asATDate
                                && i.DelFlag.Equals("N")).ToList();
                    //&& i.AsAtDate.Equals(asATDate)
                    //.Select(i => {
                    //    if (string.IsNullOrEmpty(i.ImpairmentStatus)) {
                    //        i.ImpairmentStatus = "Pending";
                    //    }
                    //    return i;
                    //});
                    return accounts;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static List<Remark> GetRemarksByCustomerId(string customerId) {
            using (var context = new Entities()) {
                var accounts = context.Remarks.Where(i => i.CustomerId.Equals(customerId) && i.DelFlag.Equals("N")).OrderByDescending(t => t.RcReTime);

                return accounts.ToList();
            }
        }


        

        private static IQueryable<CashFlow> GetCashFlowsByAccountId(Entities context, string accountId) {
            var data = from i in context.CashFlows where i.AccountNumber.Equals(accountId) && i.DelFlag.Equals("N") select i;
            return data;
        }

        public static List<CashFlow> GetCashFlowsByAccountId(string accountId) {
            using (var context = new Entities()) {
                var cashFlows = GetCashFlowsByAccountId(context, accountId);

                return cashFlows.ToList();
            }
        }

        public static List<CashFlow> GetCashFlowsByCustomerId(string customerId) {
            using (var context = new Entities()) {

                var data = from i in context.CashFlows where i.CustomerId.Equals(customerId) && !i.DelFlag.Equals("Y") select i;

                //var cashflows = context.CashFlows.Where(i => i.AccountNumber.Equals(accountNumber) && !i.DelFlag.Equals("Y"));

                return data.ToList();
            }
        }

        public static List<Question> GetAllMasterQuestions(bool latest,DateTime asatDate) // if latest=true then the param asatdate will be ignored
        {
            using (var context = new Entities())
            {
                DateTime date = latest ? context.Questions.Max(a => a.AsAtDate).GetValueOrDefault() : asatDate;
                var questions = context.Questions.Where(a=>a.AsAtDate== date).ToList();
                
                return questions;
            }
        }

        public static bool UpdateMasterQuestion(Question updQuestion)
        {
            using (var context = new Entities())
            {
                var question = context.Questions.Where(i => i.Id == updQuestion.Id).FirstOrDefault();       // as at date is not needed
                if (question != null)
                {
                    question.Status = updQuestion.Status;
                    question.Text = updQuestion.Text;
                    question.BI_Type = updQuestion.BI_Type;

                    return context.SaveChanges() > 0;
                }
                return false;
            }
        }

        public static bool UpdateAllMasterQuestionsType(List<Question> updQuestions)
        {
            using (var context = new Entities())
            {
                DateTime asatDate = context.Questions.Max(a => a.AsAtDate).GetValueOrDefault();
                var questions = context.Questions.Where(i => i.Status.Equals("A") && i.AsAtDate == asatDate).ToList();
                foreach (var qs in questions)
                {
                    
                    qs.BI_Type = updQuestions.Where(a => a.Id == qs.Id
                            && !string.IsNullOrEmpty(a.BI_Type) 
                            && (a.BI_Type.Equals("B") || a.BI_Type.Equals("I") || a.BI_Type.Equals("A"))
                        ).FirstOrDefault()?.BI_Type;
                    
                }

                return context.SaveChanges() > 0;
            }
        }

        public static List<Question> GetQuestionsBy(string customerId) {
            using (var context = new Entities()) {
                DateTime asatDate = context.Questions.Max(a => a.AsAtDate).GetValueOrDefault();

                var questions = context.Questions.Where(i => i.Status.Equals("A") && i.AsAtDate == asatDate).ToList();
                
                var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(customerId));
                if (customer != null) {
                    foreach (var question in questions) {
                        var property = customer.GetType().GetProperty($"q{question.No}");
                        if (property != null) {
                            var value = property.GetValue(customer, null);
                            if (value != null) {
                                question.Answer = value.ToString().Trim();
                            }
                        }
                    }
                }

                return questions;
            }
        }

        public static bool CustomerQuestionsClearBy(string customerId) {
            using (var context = new Entities()) {
                var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(customerId));
                var questions = context.Questions.Where(i => i.Status.Equals("A")).ToList();
                if (customer != null) {
                    foreach (var question in questions) {
                        SetQuestionValue(customer, question.No, null);
                    }
                }

                return context.SaveChanges() > 0;
            }
        }

        private static void UpdateAccountNoCashFlows(Account account) {
            //account.ImpairementAmount = "";
            //if (Math.Abs(account.AmortizedAmount.GetValueOrDefault()) > 0m) {
            //    account.ImpairmentStatus = Constants.CashFlowsImpairedNoCashFlows;
            //}
            //else {
            account.ImpairmentStatus = Constants.CashFlowsNotImpairedNoCashFlows;
            //}

            account.ImpairmentAmount = 0; //account.AmortizedAmount;
            account.TotalCashFlowAmount = 0;
            account.AppliedInterestRate = account.InterestRate;
            account.PresentValue = 0;
        }

        private static void UpdateNoCashFlows(IQueryable<CashFlow> flows) {
            foreach (var flow in flows) {
                flow.DelFlag = "Y";
            }
        }

        private static void UpdateNoCashFlowsAccount(Entities context, Account account) {
            UpdateAccountNoCashFlows(account);
            var cashFlows = GetCashFlowsByAccountId(context, account.AccountNumber);
            UpdateNoCashFlows(cashFlows);
        }

        private static void UpdateNoCashFlowsCustomer(Entities context, Customer customer) {
            customer.TotalImpairmentAmount = 0;
            customer.TotalPresentValue = 0;
            customer.TotalCashFlowsAmount = 0;

            var accounts = context.Accounts.Where(i => i.CustomerId.Equals(customer.Id));
            foreach (var account in accounts) {
                UpdateAccountNoCashFlows(account);
            }

            var sumOfAmortizedAmount = accounts.Sum(i => i.AmortizedAmount);
            customer.TotalAmortizedAmount = sumOfAmortizedAmount;

            var flows = context.CashFlows.Where(i => i.CustomerId.Equals(customer.Id));
            UpdateNoCashFlows(flows);
        }

        public static bool UpdateNoCashFlows(string accountId) {
            using (var context = new Entities()) {
                var account = context.Accounts.FirstOrDefault(i => i.AccountNumber.Equals(accountId));
                if (account != null) {
                    UpdateNoCashFlowsAccount(context, account);
                }

                return context.SaveChanges() > 0;
            }
        }

        private static decimal ConvertToDecimal(double value, int decimalPlaces = 4) {
            var valueString = value.ToString(CultureInfo.InvariantCulture);
            var spllited = valueString.Split('.');
            var leftToZero = spllited[0];
            var rightToZero = spllited.Length > 1 ? spllited[1] : "0";
            decimal.TryParse($"{leftToZero}.{rightToZero.Substring(0, Math.Min(decimalPlaces, rightToZero.Length))}", out var ret);
            return ret;
        }

        private static int DifferenceInMonths(DateTime date1, DateTime date2) {
            return ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
        }

        public static bool AddCashFlow(CashFlow flow, int months) {
            using (var context = new Entities()) {
                //var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(flow.CustomerId));
                //if(customer != null) {

                //}

                var account = context.Accounts.FirstOrDefault(i => i.AccountNumber.Equals(flow.AccountNumber));
                if (account != null) {
                    flow.AccountName = account.Name;
                    account.ImpairmentStatus = Constants.CashFlowsImpairmentStatusInProgress;

                    var amortizedAmount = Math.Round(account.AmortizedAmount.GetValueOrDefault(), 2);
                    var sumOfCashFlowsAmount = GetCashFlowsByAccountId(account.AccountNumber).Select(i => i.PresentValue).DefaultIfEmpty(0).Sum();
                    var maxId = context.CashFlows.Select(i => i.Id).DefaultIfEmpty(0).Max();
                    var flows = new List<CashFlow>();
                    var sumOfFlows = sumOfCashFlowsAmount.GetValueOrDefault(0M);


                    for (int i = 0; i < months; i++) {
                        var currentMonth = flow.Date.GetValueOrDefault().AddMonths(i);
                        var presentValue = CalculatePresentValue(flow.Amount.GetValueOrDefault(), flow.InterestRate.GetValueOrDefault(), currentMonth, account.AsAtDate.GetValueOrDefault());

                        sumOfFlows += presentValue;
                        sumOfFlows = Math.Round(sumOfFlows, 2);

                        if (amortizedAmount < sumOfFlows) {
                            throw new Exception($"Cann't exceed amortized amount({amortizedAmount:n})");
                        }

                        var newFlow = new CashFlow() {
                            AccountName = flow.AccountName,
                            AccountNumber = flow.AccountNumber,
                            SolId = account.SolId,
                            Amount = flow.Amount,
                            AsAtDate = account.AsAtDate,
                            CustomerId = flow.CustomerId,
                            Date = currentMonth,
                            DelFlag = "N",
                            EntryTime = DateTime.Now,
                            EntryUserId = flow.EntryUserId,
                            Id = maxId + i + 1,
                            InterestRate = flow.InterestRate,
                            PresentValue = presentValue,
                            Status = flow.AccountName,

                        };
                        flows.Add(newFlow);

                        //context.CashFlows.Add(newFlow);
                    }

                    var updatedFlow = context.CashFlows.AddRange(flows);
                }


                return context.SaveChanges() > 0;
            }

            return true;
        }

        public static decimal CalculatePresentValue(decimal flowAmount, decimal interestRate, DateTime currentMonth, DateTime asAtDate) {
            var period = DifferenceInMonths(currentMonth, asAtDate);
            /* Formula = CF X  1/(1+Rate/12)^month */

            var powered = Math.Pow(Convert.ToDouble(1 + (interestRate / 100) / 12), period);
            var presentValueDouble = Convert.ToDouble(flowAmount) / powered;
            var presentValue = ConvertToDecimal(presentValueDouble, 4);
            return presentValue;
        }

        public static bool UpdateCompleteAllCashFlows(string accountId) {
            using (var context = new Entities()) {
                var account = context.Accounts.FirstOrDefault(i => i.AccountNumber.Equals(accountId));
                if (account != null) {
                    var cashFlows = GetCashFlowsByAccountId(context, account.AccountNumber);
                    var impairmentAmountRemaining = 0M;
                    if (cashFlows.Any()) {
                        var sumPresentValues = cashFlows.Sum(i => i.PresentValue);
                        var sumCashFlowAmount = cashFlows.Sum(i => i.Amount);

                        impairmentAmountRemaining = Math.Round(Math.Abs(account.AmortizedAmount.GetValueOrDefault()) - sumPresentValues.GetValueOrDefault(), 2);

                        account.ImpairmentAmount = impairmentAmountRemaining;
                        account.PresentValue = sumPresentValues;
                        account.TotalCashFlowAmount = sumCashFlowAmount;
                    }
                    else {
                        throw new Exception("Please add one or more cash flows(s)!");
                    }

                    account.ImpairmentStatus = impairmentAmountRemaining.Equals(0M) ? Constants.CashFlowsNotImpairedWithCashFlows : Constants.CashFlowsImpairedWithCashFlows;

                    //account.ImpairementStatus = Constants.CashFlowsImpairementStatusWithCashFlows;
                    account.AppliedInterestRate = account.InterestRate;
                }

                return context.SaveChanges() > 0;
            }
        }

        public static bool DeleteCashFlowById(int flowId) {
            using (var context = new Entities()) {
                var updatedFlow = context.CashFlows.FirstOrDefault(i => i.Id.Equals(flowId));

                if (updatedFlow != null) {
                    updatedFlow.DelFlag = "Y";
                }

                return context.SaveChanges() > 0;
            }
        }

        public static bool UpdateQuestionAnswerBy(string customerId, string questionNo, string answer) {
            using (var context = new Entities()) {
                var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(customerId));
                if (customer != null) {
                    SetQuestionValue(customer, Convert.ToInt32(questionNo), answer);
                }
                return context.SaveChanges() > 0;
            }
        }

        private static void SetQuestionValue(Customer customer, decimal questionNo, string answer) {

            switch (questionNo) {
                case 1:
                    customer.q1 = answer;
                    break;
                case 2:
                    customer.q2 = answer;
                    break;
                case 3:
                    customer.q3 = answer;
                    break;
                case 4:
                    customer.q4 = answer;
                    break;
                case 5:
                    customer.q5 = answer;
                    break;
                case 6:
                    customer.q6 = answer;
                    break;
                case 7:
                    customer.q7 = answer;
                    break;
                case 8:
                    customer.q8 = answer;
                    break;
                case 9:
                    customer.q9 = answer;
                    break;
                case 10:
                    customer.q10 = answer;
                    break;
                case 11:
                    customer.q11 = answer;
                    break;
                case 12:
                    customer.q12 = answer;
                    break;
                case 13:
                    customer.q13 = answer;
                    break;
                case 14:
                    customer.q14 = answer;
                    break;
                case 15:
                    customer.q15 = answer;
                    break;
                case 16:
                    customer.q16 = answer;
                    break;
                case 17:
                    customer.q17 = answer;
                    break;
                case 18:
                    customer.q18 = answer;
                    break;
                case 19:
                    customer.q19 = answer;
                    break;
                case 20:
                    customer.q20 = answer;
                    break;
                case 21:
                    customer.q21 = answer;
                    break;
                case 22:
                    customer.q22 = answer;
                    break;
                case 23:
                    customer.q23 = answer;
                    break;
                case 24:
                    customer.q24 = answer;
                    break;
                case 25:
                    customer.q25 = answer;
                    break;
                case 26:
                    customer.q26 = answer;
                    break;
                case 27:
                    customer.q27 = answer;
                    break;
                case 28:
                    customer.q28 = answer;
                    break;
                case 29:
                    customer.q29 = answer;
                    break;
                case 30:
                    customer.q30 = answer;
                    break;
            }

        }

        public static bool SubmitAllAccountCompleteByCustomerId(string customerId, string userId, UserRole userRole) {
            using (var context = new Entities()) {
                var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(customerId));
                if (customer != null) {
                    var accounts = context.Accounts.Where(i => i.CustomerId.Equals(customer.Id));

                    var hasNotCompletedAny = accounts.Select(i => i.ImpairmentStatus).Distinct().Any(i =>
                        i != Constants.CashFlowsImpairedWithCashFlows &&
                        i != Constants.CashFlowsImpairedNoCashFlows &&
                        i != Constants.CashFlowsNotImpairedNoCashFlows &&
                        i != Constants.CashFlowsNotImpairedWithCashFlows);
                    if (hasNotCompletedAny) {
                        throw new Exception("Please complete all accounts!");
                    }

                    var sumOfPresentValue = accounts.Sum(i => i.PresentValue);
                    var sumOfCashFlows = accounts.Sum(i => i.TotalCashFlowAmount);
                    var sumOfAmortizedAmount = Math.Round(accounts.Sum(i => i.AmortizedAmount).GetValueOrDefault(), 2);
                    var sumOfImpairmentAmount = Math.Round(accounts.Sum(i => i.ImpairmentAmount).GetValueOrDefault(), 2);

                    if (sumOfAmortizedAmount != sumOfImpairmentAmount) {
                        customer.ImpairmentStatus = "Impaired Yes";
                    }
                    else {
                        customer.ImpairmentStatus = "Impaired No";
                    }

                    customer.TotalPresentValue = sumOfPresentValue;
                    customer.TotalImpairmentAmount = sumOfImpairmentAmount;
                    customer.TotalAmortizedAmount = sumOfAmortizedAmount;
                    customer.TotalCashFlowsAmount = sumOfCashFlows;
                    customer.StatusHistory = BuildStatusString(customer.StatusHistory, userId, userRole, "create");

                    customer.StatusCode = IFRS.DAL.Constants.StatusCodes.COMPLETED_ENTERING;
                }

                return context.SaveChanges() > 0;
            }
        }

        private static string BuildStatusString(string current, string userId, UserRole userRole, string action) {
            var ret = current;

            ret = string.IsNullOrEmpty(ret) ? $"{userId}({userRole}):{action.ToUpperInvariant()}" : $"{ret}/{userId}({userRole}):{action.ToUpperInvariant()}";

            return ret;
        }

        private static void UpdateAllQuestionsAnswersAsNo(Entities context, Customer customer, string userId, UserRole userRole) {
            UpdateNoCashFlowsCustomer(context, customer);

            customer.ImpairmentStatus = Constants.CashFlowsNotImpairedNoCashFlows;
            customer.TotalPresentValue = 0;
            customer.TotalImpairmentAmount = 0;
            customer.TotalCashFlowsAmount = 0;
            customer.StatusHistory = BuildStatusString(customer.StatusHistory, userId, userRole, "create");

            customer.StatusCode = IFRS.DAL.Constants.StatusCodes.COMPLETED_ENTERING;
        }

        public static bool UpdateQuestionsAnswersBy(string customerId, List<Question> questionsAndAnswers, string userId, UserRole userRole, string custRemark ) {
            using (var context = new Entities()) {
                var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(customerId));
                if (customer != null) {

                    customer.AnswerRemark = custRemark;

                    var answers = questionsAndAnswers.Where(i => !string.IsNullOrEmpty(i.Answer));
                    foreach (var item in answers) {
                        SetQuestionValue(customer, item.No, item.Answer);
                    }

                    var areAllAnswered = questionsAndAnswers.All(i => !string.IsNullOrEmpty(i.Answer) && (i.Answer.Equals("Y") || i.Answer.Equals("N")));

                    customer.Mora_Status = answers.Where(a => a.No == 1 && (a.Answer?.Equals("Y") ?? false)).FirstOrDefault() != null ? "Y" : "N";
                    customer.ImpairmentStatus = Constants.CashFlowsImpairmentStatusInProgress;
                    
                    

                    if (areAllAnswered) 
                    {
                        customer.ImpairmentStatus = Constants.CashFlowsNotImpairedNoCashFlows;
                        switch (customer.StatusCode)
                        {
                            case StatusCodes.INITIAL:
                            case StatusCodes.ENTERING:
                                customer.StatusCode = StatusCodes.COMPLETED_ENTERING; break;
                            case StatusCodes.REJECTED_BY_BM:
                                customer.StatusCode = StatusCodes.CORRECTED_TO_BM_BY_ENTERING; break;
                            case StatusCodes.REJECTED_BY_AM:
                                customer.StatusCode = StatusCodes.CORRECTED_TO_AM_BY_ENTERING; break;
                            case StatusCodes.REJECTED_BY_AGM:
                                customer.StatusCode = StatusCodes.CORRECTED_TO_AGM_BY_ENTERING; break;
                        }

                        //customer.StatusCode = StatusCodes.COMPLETED_ENTERING;


                        var hasAnyAnswerAsYes = questionsAndAnswers.Any(i => !string.IsNullOrEmpty(i.Answer) && i.Answer.Equals("Y"));
                        if (hasAnyAnswerAsYes) {

                        }
                        else {
                            UpdateAllQuestionsAnswersAsNo(context, customer, userId, userRole);
                        }
                    }
                }

                bool retVal = false;
                try
                {
                    retVal = context.SaveChanges() > 0;
                    return retVal;
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
        }

        public static bool UpdateApprovalAtVerifyLevel(string userId, UserRole userRole, string customerId, string cashflowStatus) {
            using (var context = new Entities()) {
                var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(customerId));
                string prevStatusCode = customer.StatusCode;
                if (customer != null) {
                    switch (userRole) {
                        case UserRole.VERIFY_1: {       // BM APPROVAL
                                switch (prevStatusCode)
                                {
                                    case StatusCodes.COMPLETED_ENTERING:
                                        customer.StatusCode = StatusCodes.APPROVED_BY_BM;break;
                                    case StatusCodes.CORRECTED_TO_BM_BY_ENTERING:
                                        customer.StatusCode = StatusCodes.CORRECTED_APPROVED_TO_BM_BY_BM; break;
                                    case StatusCodes.CORRECTED_TO_AM_BY_ENTERING:
                                        customer.StatusCode = StatusCodes.CORRECTED_APPROVED_TO_AM_BY_BM; break;
                                    case StatusCodes.CORRECTED_TO_AGM_BY_ENTERING:
                                        customer.StatusCode = StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_BM; break;
                                }

                                //customer.StatusCode = DAL.Constants.StatusCodes.APPROVED_BY_BM;
                            }
                            break;
                        case UserRole.VERIFY_2: {       // AM APPROVAL
                                switch (prevStatusCode)
                                {
                                    case StatusCodes.APPROVED_BY_BM:
                                    case StatusCodes.CORRECTED_APPROVED_TO_BM_BY_BM:
                                        customer.StatusCode = StatusCodes.APPROVED_BY_AM;break;
                                    case StatusCodes.CORRECTED_APPROVED_TO_AM_BY_BM:
                                        customer.StatusCode = StatusCodes.CORRECTED_APPROVED_TO_AM_BY_AM;break;
                                    case StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_BM:
                                        customer.StatusCode = StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_AM; break;
                                }
                                //customer.StatusCode = DAL.Constants.StatusCodes.APPROVED_BY_AM;
                            }
                            break;
                        case UserRole.VERIFY_3: {       // AGM APPROVAL
                                switch (prevStatusCode)
                                {
                                    case StatusCodes.APPROVED_BY_AM:
                                    case StatusCodes.CORRECTED_APPROVED_TO_AM_BY_AM:
                                        customer.StatusCode = StatusCodes.APPROVED_BY_AGM;break;
                                    case StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_AM:
                                        customer.StatusCode = StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_AGM;break;
                                }
                                //customer.StatusCode = DAL.Constants.StatusCodes.APPROVED_BY_AGM;
                            }
                            break;
                        case UserRole.VERIFY_4: {       // FIN APPROVAL
                                switch (prevStatusCode)
                                {
                                    case StatusCodes.APPROVED_BY_AGM:
                                    case StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_AGM:
                                        if (cashflowStatus == FinanceApproveTypes.WITH_OBJECT_EVI_WITH_CASHFLOW)
                                        {
                                            customer.StatusCode = StatusCodes.APPROVED_BY_FINANCE_CASHFLOWS;
                                        }
                                        else
                                        {
                                            customer.StatusCode = StatusCodes.APPROVED_BY_FINANCE; 
                                        }
                                        break;
                                }
                                //customer.StatusCode = DAL.Constants.StatusCodes.APPROVED_BY_FINANCE;
                            }
                            break;
                    }
                    customer.StatusHistory = BuildStatusString(customer.StatusHistory, userId, userRole, "approve");
                }
                return context.SaveChanges() > 0;
            }
        }

        public static bool UpdateRejectAtVerifyLevel(string userId, UserRole userRole, string customerId, string reason) {
            using (var context = new Entities()) {
                var timestamp = DateTime.Now;
                var customer = context.Customers.FirstOrDefault(i => i.Id.Equals(customerId));
                string prevStatusCode = customer.StatusCode;

                if (customer != null) {
                    switch (userRole) {
                        case UserRole.VERIFY_1: {       // BM REJECT
                                switch (prevStatusCode)
                                {
                                    case StatusCodes.COMPLETED_ENTERING:
                                    case StatusCodes.CORRECTED_TO_BM_BY_ENTERING:
                                        customer.StatusCode = StatusCodes.REJECTED_BY_BM;break;
                                }
                                //customer.StatusCode = DAL.Constants.StatusCodes.REJECTED_BY_BM;
                            }
                            break;
                        case UserRole.VERIFY_2: {       // AM REJECT
                                switch (prevStatusCode)
                                {
                                    case StatusCodes.APPROVED_BY_BM:
                                    case StatusCodes.CORRECTED_APPROVED_TO_BM_BY_BM:
                                    case StatusCodes.CORRECTED_APPROVED_TO_AM_BY_BM:
                                        customer.StatusCode = StatusCodes.REJECTED_BY_AM;break;
                                }
                                //customer.StatusCode = DAL.Constants.StatusCodes.REJECTED_BY_AM;
                            }
                            break;
                        case UserRole.VERIFY_3: {       // AGM REJECT
                                switch (prevStatusCode)
                                {
                                    case StatusCodes.APPROVED_BY_AM:
                                    case StatusCodes.CORRECTED_APPROVED_TO_AM_BY_AM:
                                    case StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_AM:
                                        customer.StatusCode = StatusCodes.REJECTED_BY_AGM;break;
                                }
                                //customer.StatusCode = DAL.Constants.StatusCodes.REJECTED_BY_AGM;
                            }
                            break;
                        case UserRole.VERIFY_4: {       // FIN REJECT
                                switch (prevStatusCode)
                                {
                                    case StatusCodes.APPROVED_BY_AGM:
                                    case StatusCodes.CORRECTED_APPROVED_TO_AGM_BY_AGM:
                                        customer.StatusCode = StatusCodes.REJECTED_BY_FINANCE;break;
                                }
                                //customer.StatusCode = DAL.Constants.StatusCodes.REJECTED_BY_FINANCE;
                            }
                            break;
                    }
                    customer.StatusHistory = BuildStatusString(customer.StatusHistory, userId, userRole, "reject");

                    if (!string.IsNullOrEmpty(reason)) {
                        var id = context.Remarks.Select(i => i.Id).DefaultIfEmpty().Max() + 1;

                        var remark = new Remark() {
                            Id = id,
                            AsAtDate = customer.AsAtDate,
                            CustomerId = customer.Id,
                            DelFlag = "N",
                            RcReUser = userId,
                            RcReTime = timestamp,
                            LchgUser = userId,
                            LchgTime = timestamp,
                            Remarks = reason
                        };

                        context.Remarks.Add(remark);
                    }

                }
                return context.SaveChanges() > 0;
            }
        }

        public static DataTable GetCustomersReport(string userId, string solId, UserRole userRole, int startRowIndex, int maximumRows, DateTime asAtDate, out int count) {
            count = 100000;
            var dt = new DataTable();

            List<Customer> custList;

            using (var context = new Entities()) {
                var date = asAtDate.Date;
                switch (userRole) {
                    case UserRole.ENTERING: {
                            Expression<Func<Customer, bool>> predicate = i => i.DelFlag == "N" && i.AsAtDate == date && i.BranchId == solId;
                            count = context.Customers.Count(predicate);
                            custList = context.Customers.Where(predicate).DefaultIfEmpty().OrderBy(i => i.Id).Skip(startRowIndex).Take(maximumRows).ToList();
                        }
                        break;
                    default: {
                            Expression<Func<Customer, bool>> predicate = i => i.DelFlag == "N" && i.AsAtDate == date;
                            count = context.Customers.Count(predicate);
                            custList = context.Customers.Where(predicate).OrderBy(i => i.Id).Skip(startRowIndex).Take(maximumRows).ToList();
                        }
                        break;
                }
            }

            List<CustomerReportItem> custReps = new List<CustomerReportItem>();

            try
            {
                foreach (var c in custList)
                {
                    CustomerReportItem cr = new CustomerReportItem {
                        Id = c.Id,
                        Name = c.Name,
                        BranchId = c.BranchId,
                        BranchCode = c.BranchCode,
                        BrancType = c.BrancType,
                        CapitalOSLKR = c.CapitalOSLKR,
                        StatusCode = c.StatusCode,
                        TotalPresentValue = c.TotalPresentValue,
                        TotalAmortizedAmount = c.TotalAmortizedAmount,
                        TotalCashFlowsAmount = c.TotalCashFlowsAmount,
                        TotalImpairmentAmount = c.TotalImpairmentAmount,
                        ImpairmentStatus = c.ImpairmentStatus,
                        EntryUserId = c.EntryUserId,
                        AsAtDate = c.AsAtDate,
                        DelFlag = c.DelFlag,
                        RelationshipManager = c.RelationshipManager,
                        Verify_1 = c.Verify_1,
                        Verify_2 = c.Verify_2,
                        Verify_3 = c.Verify_3,
                        Verify_4 = c.Verify_4,
                        q1 = c.q1??"-",
                        q2 = c.q2??"-",
                        q3 = c.q3??"-",
                        q4 = c.q4??"-",
                        q5 = c.q5??"-",
                        q6 = c.q6??"-",
                        q7 = c.q7??"-",
                        q8 = c.q8??"-",
                        q9 = c.q9 ?? "-",
                        q10 = c.q10??"-",
                        q11 = c.q11??"-",
                        q12 = c.q12??"-",
                        q13 = c.q13??"-",
                        q14 = c.q14??"-",
                        q15 = c.q15??"-",
                        q16 = c.q16??"-",
                        q17 = c.q17??"-",
                        q18 = c.q18??"-",
                        q19 = c.q19??"-",
                        q20 = c.q20??"-",
                        q21 = c.q21??"-",
                        q22 = c.q22??"-",
                        q23 = c.q23??"-",
                        q24 = c.q24??"-",
                        q25 = c.q25??"-",
                        q26 = c.q26??"-",
                        q27 = c.q27??"-",
                        q28 = c.q28??"-",
                        q29 = c.q29??"-",
                        q30 = c.q30 ?? "-",
                        StatusHistory = c.StatusHistory,
                        FreeText_1 = c.FreeText_1,
                        FreeText_2 = c.FreeText_2,
                        FreeText_3 = c.FreeText_3,
                        FreeDate_1 = c.FreeDate_1,
                        FreeDate_2 = c.FreeDate_2,
                        FreeNo_1 = c.FreeNo_1,
                        FreeNo_2 = c.FreeNo_2,
                        RcReUser = c.RcReUser,
                        RcReTime = c.RcReTime,
                        VerifiedUser = c.VerifiedUser,
                        VerifiedTime = c.VerifiedTime,
                        LchgUser = c.LchgUser,
                        LchgTime = c.LchgTime,
                        Npa_Pa = c.Npa_Pa,
                        Mora_Status = c.Mora_Status,
                        Customer_Type = c.Customer_Type,
                        AnswerRemark = c.AnswerRemark,


                        FinalSICR = GetFinalSICR(c),
                        NPA_PA = c.Npa_Pa ?? "N/A",
                        MoraStatus = c.Mora_Status ?? "N/A",
                        Impairment = GetImpairment(c.Id),
                        LastRejectReason = GetLastRejectReason(c.Id,c.StatusCode)
                    };
                    custReps.Add(cr);
                }
            }
            catch (Exception ex)
            {

            }

            dt = custReps.ToDataTable();

            return dt;
        }

        public static DataTable GetAccountsReport(string userId, string solId, UserRole userRole, int startRowIndex, int maximumRows, DateTime asAtDate, out int count) {
            var dt = new DataTable();
            using (var context = new Entities()) {
                var date = asAtDate.Date;
                switch (userRole) {
                    case UserRole.ENTERING: {
                            Expression<Func<Account, bool>> predicate = i => i.DelFlag == "N" && i.AsAtDate == date && i.SolId == solId;
                            count = context.Accounts.Count(predicate);
                            dt = context.Accounts.Where(predicate).DefaultIfEmpty().OrderBy(i => i.AccountNumber).Skip(startRowIndex).Take(maximumRows).ToList().ToDataTable();
                        }
                        break;
                    default: {
                            Expression<Func<Account, bool>> predicate = i => i.DelFlag == "N" && i.AsAtDate == date;
                            count = context.Accounts.Count(predicate);
                            dt = context.Accounts.Where(predicate).OrderBy(i => i.AccountNumber).Skip(startRowIndex).Take(maximumRows).ToList().ToDataTable();
                        }
                        break;
                }
            }
            return dt;
        }

        public static DataTable GetCashFlowsReport(string userId, string solId, UserRole userRole, int startRowIndex, int maximumRows, DateTime asAtDate, out int count) {
            var dt = new DataTable();
            using (var context = new Entities()) {
                var date = asAtDate.Date;
                switch (userRole) {
                    case UserRole.ENTERING: {
                            Expression<Func<CashFlow, bool>> predicate = i => i.DelFlag == "N" && i.AsAtDate == date && i.EntryUserId == userId;
                            count = context.CashFlows.Count(predicate);
                            dt = context.CashFlows.Where(predicate).DefaultIfEmpty().OrderBy(i => i.Id).Skip(startRowIndex).Take(maximumRows).ToList().ToDataTable();
                        }
                        break;
                    default: {
                            Expression<Func<CashFlow, bool>> predicate = i => i.DelFlag == "N" && i.AsAtDate == date;
                            count = context.CashFlows.Count(predicate);
                            dt = context.CashFlows.Where(predicate).OrderBy(i => i.Id).Skip(startRowIndex).Take(maximumRows).ToList().ToDataTable();
                        }
                        break;
                }

            }
            return dt;
        }

        public static DataTable ToDataTable<T>(this IList<T> data) {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties) {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (T item in data) {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties) {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }




        // for Admin


        public static List<Customer> GetAllCustomers()
        {
            using (var context = new Entities())
            {
                return context.Customers.ToList();
            }
        }

        public static bool UpdateNPACustomerStatus(List<Customer> customers)
        {
            IEnumerable<Customer> orCust;
            using (var context = new Entities())
            {
                orCust = context.Customers;


                if (orCust != null)
                {
                    foreach (var cust in customers)
                    {
                        var sel = orCust.Where(a => a.Id == cust.Id).FirstOrDefault();
                        if (sel != null)
                        {
                            sel.Npa_Pa = cust.Npa_Pa;
                        }

                    }
                    return context.SaveChanges() > 0;
                }
            }
            return false;
        }

        //public static IEnumerable<Question> GetAllQuestions()
        //{
        //    using (var context = new Entities())
        //    {
        //        return context.Questions;
        //    }
        //}

    }
}
