using IFRS.DAL;
using IFRS.DAL.FileManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IFRS.WEB.Views
{
    public partial class Admin : Models.BasePage
    {
        private List<Question> questions;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // checking loggedin user 
            var isValidRole = this.UserRole == DEM.UserRole.VERIFY_4;
            if (!isValidRole || !this.IsAdmin)
            {
                this.Instruction = $"YOU ARE NOT ALLOWED TO ACCESS THIS VIEW";
                btnClear.Enabled = false;
                btnSubmit.Enabled = false;
                return;
            }
            btnClear.Enabled = true;
            btnSubmit.Enabled = true;

            
            this.Instruction = "ADMINISTRATION";

        }

        

        public void Submit()
        {
            var questionsBIs = this.GridQuestions.Controls.All().OfType<RadRadioButtonList>()
                                            .Select(a => new Question
                                            {
                                                BI_Type = a.SelectedValue,
                                                Id = Convert.ToInt32(a.Attributes["QuestionId"])
                                            }).ToList();

            
            DataContext.UpdateAllMasterQuestionsType(questionsBIs);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            GridQuestions.Rebind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Submit();
                
                this.Notification.Text = "All B/I selections were saved.";
                this.Notification.Title = "Success";
                this.Notification.Show();
            }
            catch (Exception ex)
            {
                
                this.Notification.Text = "Not saved! Please try to select options and submit";
                this.Notification.Show();
            }

            GridQuestions.Rebind();
        }
        
        protected void GridQuestions_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            questions = DataContext.GetAllMasterQuestions(latest:true,DateTime.Today);
            var table = questions.ToDataTable();
            GridQuestions.DataSource = table;
        }
        
        protected void GridQuestions_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editItem = e.Item as GridEditableItem;

            Dictionary<string, string> newValues = new Dictionary<string, string>();

            GridQuestions.MasterTableView.ExtractValuesFromItem(newValues, editItem);

            int qId = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id"]);
            //int qNo = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["No"]);

            string text = newValues["Text"];
            string status = newValues["Status"];
            string biType = newValues["BI_Type"];


            bool saveStatus = DataContext.UpdateMasterQuestion(new Question
            {
                Id = qId,
                Text = text,
                Status = status,
                BI_Type = biType
            });

            if (saveStatus)
            {
                this.Notification.Text = "Question was updated successfully.";
                this.Notification.Title = "Success";
                this.Notification.Show();
            }
            else
            {
                this.Notification.Text = "Not saved! Please try again";
                this.Notification.Show();
            }
            
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            CustomerCsv customerCsv = new CustomerCsv();
            var csvCont = customerCsv.Export();

            var csvBytes = System.Text.Encoding.ASCII.GetBytes(csvCont);

            Response.OutputStream.Write(csvBytes, 0, csvBytes.Length);
            Response.AddHeader("Content-Disposition", "attachment; filename=Customers.csv");
            Response.Flush();
            Response.End();

        }

        

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string strFileName;
            string strFolder;
            strFolder = Server.MapPath("./");
            strFileName = fileUpload.PostedFile.FileName;
            
            if (fileUpload.FileContent.Length >0)
            {
                using (StreamReader stream = new StreamReader(fileUpload.PostedFile.InputStream))
                {
                    string csv = stream.ReadToEnd();

                    CustomerCsv customerCsv = new CustomerCsv();
                    var custList = customerCsv.Import(csv);

                    bool status = DataContext.UpdateNPACustomerStatus(custList);

                    if (status)
                    {
                        this.Notification.Text = "NPA/PA statuses were saved";
                        this.Notification.Title = "Success";
                        this.Notification.Show();
                    }
                    else
                    {
                        this.Notification.Text = "Not saved! Please try again";
                        this.Notification.Show();
                    }
                }
            }
        }
    }
}