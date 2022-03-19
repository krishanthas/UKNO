using IFRS.DAL;
using IFRS.DEM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IFRS.WEB.Views {
    public partial class Home : Models.BasePage {

        private List<CustomerRow> customers;
        
        protected void Page_Load(object sender, EventArgs e) {
            Initialize();
            SetAccess();
        }

        private void Initialize() {
            var user = DataContext.GetUserById(base.UserId);
            if (user != null) {

                //var customers = DataContext.GetPendingCustomersByUserId(user.Id, user.DepartmentId, this.UserRole);

                if (!IsPostBack)
                {
                    customers = DataContext.GetCustomers(user.Id, user.DepartmentId, this.UserRole);

                    if (customers.Any())
                    {
                        this.AsAtDate = customers.FirstOrDefault().AsAtDate.GetValueOrDefault();
                    }

                    this.GridCustomers.DataSource = customers;
                    var total = customers.Count();
                    var pendingCount = GetPendingCount(customers);

                    this.Instruction = $"YOU ARE HAVING {pendingCount} CUSTOMERS (OUT OF {total}) FOR EVALUATE - CLICK ON CUSTOMER ID TO PROCEED";
                }

            }
        }

        //protected void Page_PreRender(object o, EventArgs e) {
        //    //this.SetGridColumnVisibility();
        //}

        //private void SetGridColumnVisibility() {
        //    switch (this.UserRole) {
        //        case UserRole.VERIFY_1: {
        //                this.GridCustomers.MasterTableView.GetColumn("Impairment").Display = true;
        //            }
        //            break;
        //        default: {
        //                this.GridCustomers.MasterTableView.GetColumn("Impairment").Display = false;
        //            }
        //            break;
        //    }
        //}

        private int GetPendingCount(List<CustomerRow> customers) {

            var pendingCodes = new List<String>() {
                DAL.Constants.StatusCodes.INITIAL,
                DAL.Constants.StatusCodes.REJECTED_BY_BM,
                DAL.Constants.StatusCodes.REJECTED_BY_AM,
                DAL.Constants.StatusCodes.REJECTED_BY_FINANCE,
                DAL.Constants.StatusCodes.REJECTED_BY_AGM,
                DAL.Constants.StatusCodes.REJECTED_BY_SYSTEM
            };

            var codes = DEM.Constants.AccessRules.Where(i => i.Key.Key1 == UserRole).Where(i => i.Value.Description == "Pending").Select(i => i.Key.Key2).ToList();

            return customers.Count(i => codes.Contains(i.StatusCode));
        }

        private string GetStatusDescription(string code) {
            switch (code) {
                case DAL.Constants.StatusCodes.INITIAL:
                    return "Initial";
                case DAL.Constants.StatusCodes.REJECTED_BY_BM:
                    return "Manager Rejected";
                case DAL.Constants.StatusCodes.REJECTED_BY_AM:
                    return "RM/HOD Rejected";
                case DAL.Constants.StatusCodes.REJECTED_BY_AGM:
                    return "SNR DGM Rejected";
                case DAL.Constants.StatusCodes.REJECTED_BY_FINANCE:
                    return "Finance Rejected";
                case DAL.Constants.StatusCodes.REJECTED_BY_SYSTEM:
                    return "System Rejected";
                case DAL.Constants.StatusCodes.ENTERING:
                    return "Entering";
                case DAL.Constants.StatusCodes.COMPLETED_ENTERING:
                    return "Entry level Completed";
                case DAL.Constants.StatusCodes.APPROVED_BY_BM:
                    return "Verified  by Manager";
                case DAL.Constants.StatusCodes.APPROVED_BY_AGM:
                    return "Verified by SNR DGM";
                case DAL.Constants.StatusCodes.APPROVED_BY_AM:
                    return "Verified by RM/HOD";
                case DAL.Constants.StatusCodes.APPROVED_BY_FINANCE:
                    return "Finance Verified";
                case DAL.Constants.StatusCodes.SYSTEM_PROCESSING:
                    return "System Processing";
                default:
                    return "System Processing";
            }
        }

        private string GetClassName(string code) {
            var ret = string.Empty;
            switch (code) {
                case DAL.Constants.StatusCodes.INITIAL:
                case DAL.Constants.StatusCodes.REJECTED_BY_BM:
                case DAL.Constants.StatusCodes.REJECTED_BY_AM:
                case DAL.Constants.StatusCodes.REJECTED_BY_AGM:
                case DAL.Constants.StatusCodes.REJECTED_BY_FINANCE:
                case DAL.Constants.StatusCodes.REJECTED_BY_SYSTEM:
                    ret = Constants.ClassNames.CustomerPending;
                    break;
                case DAL.Constants.StatusCodes.ENTERING:
                    ret = Constants.ClassNames.CustomerInProgress;
                    break;
                case DAL.Constants.StatusCodes.COMPLETED_ENTERING:
                case DAL.Constants.StatusCodes.APPROVED_BY_BM:
                case DAL.Constants.StatusCodes.APPROVED_BY_AGM:
                case DAL.Constants.StatusCodes.APPROVED_BY_AM:
                case DAL.Constants.StatusCodes.APPROVED_BY_FINANCE:
                    ret = Constants.ClassNames.CustomerComplete;
                    break;
                case DAL.Constants.StatusCodes.SYSTEM_PROCESSING:
                    ret = Constants.ClassNames.CustomerInProgress;
                    break;
                default:
                    ret = Constants.ClassNames.CustomerInProgress;
                    break;
            }
            return ret;
        }

        private bool HasAccess2(string code) {
            switch (this.UserRole) {
                case DEM.UserRole.ENTERING: {
                        switch (code) {
                            case DAL.Constants.StatusCodes.INITIAL:
                            case DAL.Constants.StatusCodes.REJECTED_BY_BM:
                            case DAL.Constants.StatusCodes.REJECTED_BY_AM:
                            case DAL.Constants.StatusCodes.REJECTED_BY_AGM:
                            case DAL.Constants.StatusCodes.REJECTED_BY_FINANCE:
                            case DAL.Constants.StatusCodes.REJECTED_BY_SYSTEM:
                            case DAL.Constants.StatusCodes.ENTERING:
                                return true;
                            default:
                                return false;
                        }
                    }
                case DEM.UserRole.VERIFY_1: {
                        switch (code) {
                            case DAL.Constants.StatusCodes.COMPLETED_ENTERING:
                                return true;
                            default:
                                return false;
                        }
                    }
                case DEM.UserRole.VERIFY_2: {
                        switch (code) {
                            case DAL.Constants.StatusCodes.APPROVED_BY_BM:
                                return true;
                            default:
                                return false;
                        }
                    }
                case DEM.UserRole.VERIFY_3: {
                        switch (code) {
                            case DAL.Constants.StatusCodes.APPROVED_BY_AM:
                                return true;
                            default:
                                return false;
                        }
                    }
                case DEM.UserRole.VERIFY_4: {
                        switch (code) {
                            case DAL.Constants.StatusCodes.APPROVED_BY_AGM:
                                return true;
                            default:
                                return false;
                        }
                    }
                default:
                    return false;
            }
            return false;
        }

       


        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e) {
            // OnItemDataBound="RadGrid1_ItemDataBound"
            if (e.Item is GridDataItem dataItem) {
                var className = dataItem["className"].Text;
                
                dataItem["Status"].CssClass = className;
                
            }
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e) {
            switch (e.CommandName) {
                case "RowClick":
                    var dataItem = e.Item as GridDataItem;
                    this.OnRowClick(source, e, dataItem);
                    break;
                default:
                    break;
            }

        }

        protected void OnRowClick(object source, GridCommandEventArgs e, GridDataItem gridDataItem) {
            var status = gridDataItem["StatusCode"].Text;
            var rule = DataContext.GetAccessEntry(status, string.Empty,this.UserRole);
            
            if (rule.Enabled) {
                var customerId = gridDataItem["id"].Text;
                if (status.Equals(DAL.Constants.StatusCodes.APPROVED_BY_FINANCE_CASHFLOWS))
                {
                    Response.Redirect($"~/Views/Accounts.aspx?customerId={customerId}");
                }
                else
                {
                    Response.Redirect($"~/Views/Questions.aspx?customerId={customerId}");
                }
                
            }
        }

        private void SetAccess() {

        }

        protected void GridCustomers_PreRender(object sender, EventArgs e)
        {
            for (int i = 0; i < GridCustomers.Items.Count; i++)
            {
                if (GridCustomers.Items[i].Cells[14].Text.Equals("customer-pending-rejected-for-me"))
                {
                    GridCustomers.Items[i].ForeColor = System.Drawing.Color.White;
                    GridCustomers.Items[i].BackColor = System.Drawing.Color.FromArgb(255, 17, 17);
                }
            }
        }
    }
}