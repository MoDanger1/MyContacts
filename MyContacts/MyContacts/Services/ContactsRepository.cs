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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        public bool Delete(int contactId)
        {
            throw new NotImplementedException();
        }
    }
}
