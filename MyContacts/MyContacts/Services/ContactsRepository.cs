using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MyContacts
{
    class ContactsRepository : IContactsRepository
    {
        private string connectionString = "Data Source=.; Initial Catalog=Contact_DB; Integrated Security= true"; //--------------Connection string
        public DataTable SelectAll()
        {
            string query = "Select * From MyContacts";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public DataTable SelectRow(int contactId)
        {
            string query = "Select * From MyContacts Where ContactID =" + contactId;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }
        public bool Insert(string name, string family, string mobile, int age, string email, string address)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                string query = "Insert into MyContacts (Name, Family, Mobile, Age, Email, Address) values(@Name, @Family, @Mobile, @Age, @Email, @Address)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Family", family);
                command.Parameters.AddWithValue("@Mobile", mobile);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Address", address);
                conn.Open();
                command.ExecuteNonQuery();

                return true;
            }
            catch
            {

                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool Update(int contactId, string name, string family, string mobile, string email, int age, string address)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                string query = "Update MyContacts Set Name=@Name, Family=@Family, Mobile=@Mobile, Age=@Age, Email=@Email, Address=@Address Where ContactID=@ID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@ID", contactId);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Family", family);
                command.Parameters.AddWithValue("@Mobile", mobile);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Address", address);
                conn.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool Delete(int contactId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                string query = "Delete From MyContacts Where ContactID = @ID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@ID", contactId);
                conn.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {

                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable Search(string parameter)
        {
            string query = "Select * From MyContacts Where Name like @parameter or Family like @parameter";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@parameter", "%" + parameter + "%");
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }
    }
}
