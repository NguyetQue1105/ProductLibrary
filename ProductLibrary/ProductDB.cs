using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLibrary
{
    public class ProductDB
    {
        string strConnection;
        public ProductDB()
        {
            strConnection = getConnectionString();
        }
        public string getConnectionString()
        {
            string strConnection = "server=SE140090;database=SaleDB;uid=sa;pwd=123456";
            return strConnection;
        }
        public List<Product> GetProducts()
        {
            string SQL = "SELECT * FROM Products";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            List<Product> dtBook = new List<Product>();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int id = reader.GetFieldValue<int>(reader.GetOrdinal("ProductID"));
                            string name = reader.GetFieldValue<string>(reader.GetOrdinal("ProductName"));
                            int quan = reader.GetFieldValue<int>(reader.GetOrdinal("Quantity"));
                            float price = (float)reader.GetFieldValue<decimal>(reader.GetOrdinal("UnitPrice"));
                            dtBook.Add(new Product(id, name, price, quan));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return dtBook;
        }
        public bool AddNewProduct(Product p)
        {
            string SQL = "INSERT Products values(@ID,@Name,@Quantity,@Price)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            bool result;
            cmd.Parameters.AddWithValue("@ID", p.ProductID);
            cmd.Parameters.AddWithValue("@Name", p.ProductName);
            cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
            cmd.Parameters.AddWithValue("@Price", p.UnitPrice);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
        public bool RemoveProduct(Product p)
        {
            string SQL = "DELETE Products WHERE ProductID = @ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", p.ProductID);
            bool result;
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
        public bool UpdateProduct(Product p)
        {
            string SQL = "UPDATE Products set ProductName=@Name, Quantity=@Quantity, " +
                         "UnitPrice=@Price where ProductID=@ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            bool result;
            cmd.Parameters.AddWithValue("@ID", p.ProductID);
            cmd.Parameters.AddWithValue("@Name", p.ProductName);
            cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
            cmd.Parameters.AddWithValue("@Price", p.UnitPrice);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
        public Product FindProduct(int ProductID)
        {
            Product p = new Product();
            string SQL = "SELECT * FROM Products WHERE ProductID=@ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", ProductID);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetFieldValue<int>(reader.GetOrdinal("ProductID"));
                    string name = reader.GetFieldValue<string>(reader.GetOrdinal("ProductName"));
                    int quan = reader.GetFieldValue<int>(reader.GetOrdinal("Quantity"));
                    float price = (float)reader.GetFieldValue<decimal>(reader.GetOrdinal("UnitPrice"));
                    p = new Product(id, name, price, quan);
                }
                else
                {
                    p = null;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return p;
        }

    }
}
