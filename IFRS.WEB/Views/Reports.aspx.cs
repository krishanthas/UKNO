using System;
using System.Collections.Generic;
using System.Drawing;
using IFRS.DAL;
using IFRS.WEB.Models;
using Telerik.Web.UI;
using Telerik.Web.UI.ExportInfrastructure;

namespace IFRS.WEB.Views {
    public partial class Reports : Models.BasePage {

        public int SelectedReportIndex { get; set; }

        private List<Question> questions;

        protected void Page_Load(object sender, EventArgs e) {
            Page.Header.DataBind();
            if (!Page.IsPostBack) {


                this.DropDownReportItem.DataSource = new List<string>() { "Customers", "Accounts", "Cash Flows", "Custom" };
                this.DropDownReportItem.DataBind();
                this.DatePickerAsAtDate.SelectedDate = DateTime.Now;
                this.SelectedReportIndex = -1;

                this.GridCustomers.DataSource = new int[] { };
                this.GridAccounts.DataSource = new int[] { };
                this.GridCashFlows.DataSource = new int[] { };
                this.GridAccounts.Visible = false;
                this.GridCustomers.Visible = false;
                this.GridCashFlows.Visible = false;
            }
            else
            {
                var asAtDate = this.DatePickerAsAtDate.SelectedDate.GetValueOrDefault();
                questions = DataContext.GetAllMasterQuestions(latest:false,asAtDate);
            }
        }

        protected void Grid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {
            if (sender is RadGrid grid) {
                var asAtDate = this.DatePickerAsAtDate.SelectedDate.GetValueOrDefault();
                this.AsAtDate = asAtDate;
                switch (grid.FeatureGroupID) {
                    case "GridCustomers": {

                            int startRowIndex = this.GridCustomers.CurrentPageIndex * this.GridCustomers.PageSize;
                            int maximumRows = this.GridCustomers.PageSize;

                            var data = DataContext.GetCustomersReport(this.UserId, this.UserBranchId, this.UserRole, startRowIndex, maximumRows, asAtDate, out var count);
                            grid.VirtualItemCount = count;
                            grid.DataSource = data;
                        }
                        break;
                    case "GridAccounts": {
                            int startRowIndex = this.GridAccounts.CurrentPageIndex * this.GridAccounts.PageSize;
                            int maximumRows = this.GridAccounts.PageSize;

                            var data = DataContext.GetAccountsReport(this.UserId, this.UserBranchId, this.UserRole, startRowIndex, maximumRows, asAtDate, out var count);
                            grid.VirtualItemCount = count;
                            grid.DataSource = data;
                        }
                        break;
                    case "GridCashFlows": {
                            int startRowIndex = this.GridCashFlows.CurrentPageIndex * this.GridCashFlows.PageSize;
                            int maximumRows = this.GridCashFlows.PageSize;

                            var data = DataContext.GetCashFlowsReport(this.UserId, this.UserBranchId, this.UserRole, startRowIndex, maximumRows, asAtDate, out var count);
                            grid.VirtualItemCount = count;
                            grid.DataSource = data;
                        }
                        break;
                    case "Custom": {

                        }
                        break;
                }

            }
        }

        protected void btnFetch_Click(object sender, EventArgs e) {
            RadGrid grid = null;
            switch (this.DropDownReportItem.SelectedIndex) {
                case 0:
                    grid = this.GridCustomers;
                    break;
                case 1:
                    grid = this.GridAccounts;
                    break;
                case 2:
                    grid = this.GridCashFlows;
                    break;
            }

            if (grid != null) {
                grid.Rebind();
                grid.Visible = true;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e) {
            RadGrid grid = null;
            switch (this.DropDownReportItem.SelectedIndex) {
                case 0: {
                        grid = this.GridCustomers;

                        //grid.ClientSettings.Virtualization.ItemsPerView = int.MaxValue;

                        //foreach (GridColumn column in grid.MasterTableView.RenderColumns) {
                        //    grid.MasterTableView.GetColumn(column.UniqueName).HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#00a2ea");
                        //    if (column.UniqueName.StartsWith("q")) {
                        //        var columnData = grid.Items[column.OrderIndex - 1][column.UniqueName].Text;
                        //        if (columnData != null && columnData.Equals("Yes")) {
                        //            grid.MasterTableView.GetColumn(column.UniqueName).ItemStyle.BackColor = Color.Red;
                        //        }

                        //    }
                        //}
                    }
                    break;
                case 1:
                    grid = this.GridAccounts;
                    break;
                case 2:
                    grid = this.GridCashFlows;
                    break;
            }

            if (grid != null) {

                //grid.ClientSettings.Virtualization.ItemsPerView = int.MaxValue;
                //grid.PageSize = grid.MasterTableView.VirtualItemCount;
                grid.MasterTableView.VirtualItemCount = int.MaxValue;
                grid.ExportSettings.IgnorePaging = true;
                grid.ExportSettings.ExportOnlyData = false;
                grid.ExportSettings.FileName = $"IFRS {grid.FeatureGroupID.Replace("Grid", string.Empty)} - {DateTime.Now:yyyy.MM.dd.HH.mm.ss}";
                grid.ExportSettings.OpenInNewWindow = true;
                grid.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
                grid.MasterTableView.ExportToExcel();
            }

            this.RadButtonExport.Enabled = true;
        }

        protected void GridCustomers_ItemCreated(object sender, GridItemEventArgs e) {
            if (e.Item is GridHeaderItem) {
                e.Item.Style["background-color"] = "Blue";
            }
        }

        protected void GridCustomers_ItemDataBound(object sender, GridItemEventArgs e) {
            if (e.Item is GridDataItem) {
                e.Item.Style["background-color"] = "Blue";
            }
        }

        protected void GridCustomers_ExcelExportCellFormatting(object sender, ExcelExportCellFormattingEventArgs e) {
            e.Cell.BackColor = System.Drawing.Color.Black;
        }

        protected void GridCustomers_InfrastructureExporting(object sender, GridInfrastructureExportingEventArgs e) {
            var headerStyle = new ExportStyle {
                ForeColor = Color.White,
                BackColor = System.Drawing.ColorTranslator.FromHtml("#00a2ea")
            };
            ;
            headerStyle.Font.Bold = true;

            var table = e.ExportStructure.Tables[0];
            var colCount = table.Columns.Count;
            var rowCount = table.Rows.Count;
            var yesItemStyle = new ExportStyle() {
                ForeColor = Color.White,
                BackColor = Color.Red
            };

            for (var i = 1; i <= colCount; i++) {
                table.Cells[i, 1].Style = headerStyle;
            }

            var headerText = string.Empty;
            for (var c = 1; c <= colCount; c++) {
                for (var r = 1; r <= rowCount; r++) {
                    if (r == 1) {
                        headerText = table.Cells[c, r].Text.ToString();
                        continue;
                    }
                    if (headerText.StartsWith("q")) {
                        var cellValue = table.Cells[c, r].Text.ToString();
                        switch (cellValue) {
                            case "Y": {
                                    table.Cells[c, r].Style = yesItemStyle;
                                    table.Cells[c, r].Value = "Yes";
                                }
                                break;
                            case "N": {
                                    table.Cells[c, r].Value = "No";
                                }
                                break;
                        }
                    }
                }
            }

        }

        protected void DropDownReportItem_OnItemSelected(object sender, DropDownListEventArgs e) {
            this.SelectedReportIndex = this.DropDownReportItem.SelectedIndex;
            switch (e.Text) {
                case "Customers":
                case "Accounts":
                case "Cash Flows": {
                        this.GridAccounts.Visible = false;
                        this.GridCustomers.Visible = false;
                        this.GridCashFlows.Visible = false;
                        this.DatePickerAsAtDate.Enabled = true;
                        this.RadButtonSubmit.Enabled = true;
                        this.RadButtonExport.Enabled = true;
                    }
                    break;
                case "Custom": {
                        this.GridAccounts.Visible = false;
                        this.GridCustomers.Visible = false;
                        this.GridCashFlows.Visible = false;

                        this.DatePickerAsAtDate.Enabled = false;
                        this.RadButtonSubmit.Enabled = false;
                        this.RadButtonExport.Enabled = false;

                        try {


                            // using (var context = new Entities()) {
                            //     var connection = context.Database.Connection.ConnectionString;
                            //
                            // }



                            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                            ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerCredentials("IFRS", "pabc@123", null);
                            ReportViewer1.ServerReport.ReportServerUrl = new Uri(@"http://192.168.4.46/ReportServer");
                            ReportViewer1.ServerReport.ReportPath = "/SLFRS/Data/PDCAL";
                            
                            
                            
                            // reportViewer.ShowParameterPrompts = false;
                            // reportViewer.ShowPrintButton = true;
                            
                            // Microsoft.Reporting.WebForms.ReportParameter[] reportParameterCollection = new Microsoft.Reporting.WebForms.ReportParameter[1];
                            // reportParameterCollection[0] = new Microsoft.Reporting.WebForms.ReportParameter();
                            // reportParameterCollection[0].Name = "INVOICEID";
                            // reportParameterCollection[0].Values.Add("ABC011223");
                            
                            // reportViewer.ServerReport.SetParameters(reportParameterCollection);
                            ReportViewer1.ServerReport.Refresh();


                        }
                        catch (Exception e11) {

                        }
                    }
                    break;
            }


        }

        protected void GridCustomers_PreRender(object sender, EventArgs e)
        {
            if (questions == null)
                return;

            var mt = GridCustomers.MasterTableView;
            foreach (var q in questions)
            {
                var col = mt.GetColumn("q" + q.Id);
                col.HeaderText = q.Text;
            }

            GridCustomers.Rebind();
        }
    }
}