using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Records.RecordClasses
{
    class ContactClass
    {
        //getter and setter properties
        //Acts as Data Carrier in our Application
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string DongleSN { get; set; }
        public string RevoSN { get; set; }
        public string DataLoggerSN { get; set; }
        public string LicenceType { get; set; }
        public string RTDataLoggerSN { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Selecting Data from Database
        public DataTable Select()
        {
            //Step1: Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //Step2:Writing SQL Query
                string sql = "SELECT * FROM tbl_contact";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        //Inserting Data into Database
        public bool Insert(ContactClass c)
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            //Step 1: Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Step 2: Create SQL Query to insert Data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, DongleSN, RevoSN, DataLoggerSN, RTDataLoggerSN, LicenceType) VALUES (@FirstName, @LastName, @ContactNo, @Address, @DongleSN, @RevoSN, @DataLoggerSN, @RTDataLoggerSN, @LicenceType)";
                //Creating SQL and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@DongleSN", c.DongleSN);
                cmd.Parameters.AddWithValue("@RevoSN", c.RevoSN);
                cmd.Parameters.AddWithValue("@DataLoggerSN", c.DataLoggerSN);
                cmd.Parameters.AddWithValue("@RTDataLoggerSN", c.RTDataLoggerSN);
                cmd.Parameters.AddWithValue("@LicenceType", c.LicenceType);

                //Connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Method to update data in database from our application
        public bool Update(ContactClass c)
        {
            //Create a default return type and set its default value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL to update data in our database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, DongleSN=@DongleSN, RevoSN=@RevoSN, DataLoggerSN=@DataLoggerSN, RTDataLoggerSN=@RTDataLoggerSN, LicenceType=@LicenceType WHERE ContactID=@ContactID";
                //Creating sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@DongleSN", c.DongleSN);
                cmd.Parameters.AddWithValue("@RevoSN", c.RevoSN);
                cmd.Parameters.AddWithValue("@DataLoggerSN", c.DataLoggerSN);
                cmd.Parameters.AddWithValue("@RTDataLoggerSN", c.RTDataLoggerSN);
                cmd.Parameters.AddWithValue("@LicenceType", c.LicenceType);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);

                //Open Database Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Method to delete data from Database
        public bool Delete(ContactClass c)
        {
            //Create a default return value and set its value to false
            bool isSuccess = false;
            //Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL to delete data
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                //Creating SQL command
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create parameters to add value
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //Open connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                //if the query run successfully then the value of rows is greater than zero else its value is 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
