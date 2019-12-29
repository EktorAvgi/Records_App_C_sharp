using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Records.RecordClasses;
using System.Data.SqlClient;
using System.Configuration;

namespace Records
{
    public partial class frmRecords : Form
    {
        public frmRecords()
        {
            InitializeComponent();
        }

        ContactClass c = new ContactClass();


        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the value from the input fields
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNo = txtContactNumber.Text;
            c.Address = txtAddress.Text;
            c.DongleSN = txtDongle.Text;
            c.RevoSN = txtRevo.Text;
            c.DataLoggerSN = txtDataLogger.Text;
            c.RTDataLoggerSN = txtRTDataLogger.Text;
            c.LicenceType = cmbLicenceType.Text;

            //Insert Data into DB
            bool success = c.Insert(c);
            if (success == true)
            {
                //Successfully inserted
                MessageBox.Show("New Contact Successfully Inserted");
                Clear();
            }
            else
            {
                //Failed to Add contact
                MessageBox.Show("Failed to add New Contact. Try Again.");
            }
            //Load Data on Data Gridviwer
            DataTable dt = c.Select();
            dvgContactList.DataSource = dt;

        }

        private void frmRecords_Load(object sender, EventArgs e)
        {
            //Load Data on Data Gridviwer
            DataTable dt = c.Select();
            dvgContactList.DataSource = dt;
        }

        //Method to Clear fields
        public void Clear()
        {            
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtContactNumber.Text = "";
            txtAddress.Text = "";
            txtDongle.Text = "";
            txtRevo.Text = "";
            txtDataLogger.Text = "";
            txtRTDataLogger.Text = "";
            cmbLicenceType.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the Data from TextBoxes
            c.ContactID = int.Parse(txtContactID.Text); //Convert the string value into Integer
            c.FirstName = txtFirstName.Text;
            c.LastName = txtLastName.Text;
            c.ContactNo = txtContactNumber.Text;
            c.Address = txtAddress.Text;
            c.DongleSN = txtDongle.Text;
            c.RevoSN = txtRevo.Text;
            c.DataLoggerSN = txtDataLogger.Text;
            c.RTDataLoggerSN = txtRTDataLogger.Text;
            c.LicenceType = cmbLicenceType.Text;

            //Update Data in Database
            bool success = c.Update(c);
            if (success == true)
            {
                //Updated Successfully
                MessageBox.Show("Contact has been successfully Updated.");
                //Load Data on Data Gridviwer
                DataTable dt = c.Select();
                dvgContactList.DataSource = dt;
                //Call clear method
                Clear();
            }
            else
            {
                //Failed to Update
                MessageBox.Show("Failed to Update. Try again!");
            }
            
        }

        private void dvgContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get the Data form Data View and load it into textboxes respectively.
            //Identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtContactID.Text = dvgContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dvgContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dvgContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtContactNumber.Text = dvgContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtAddress.Text = dvgContactList.Rows[rowIndex].Cells[4].Value.ToString();
            txtDongle.Text = dvgContactList.Rows[rowIndex].Cells[5].Value.ToString();
            txtRevo.Text = dvgContactList.Rows[rowIndex].Cells[6].Value.ToString();
            txtDataLogger.Text = dvgContactList.Rows[rowIndex].Cells[7].Value.ToString();
            txtRTDataLogger.Text = dvgContactList.Rows[rowIndex].Cells[8].Value.ToString();
            cmbLicenceType.Text = dvgContactList.Rows[rowIndex].Cells[9].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Call clear method
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get Data from the Textbox
            c.ContactID = Convert.ToInt32(txtContactID.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                //successfully deleted
                MessageBox.Show("Contact successfully deleted.");
                //refresh data gridview
                DataTable dt = c.Select();
                dvgContactList.DataSource = dt;
                //Call clear method
                Clear();
            }
            else
            {
                //Failed to delete
                MessageBox.Show("Failed to delete Contact. Try again!");
            }
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the value from text box   
            string keyword = txtSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%"+ keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            dvgContactList.DataSource = dt;
        }
    }
}
