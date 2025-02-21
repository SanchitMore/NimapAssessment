using Microsoft.Data.SqlClient;

namespace NimapAssessment.Models
{
    public class ProductCRUD
    {
        private IConfiguration configuration;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public ProductCRUD(IConfiguration configuration)
        {
            this.configuration = configuration;
            con=new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> Plist = new List<Product>();
            cmd = new SqlCommand("Select p.*,c.* from Product p join Category c on p.CategoryId=c.CategoryId",con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product pro = new Product();
                    pro.ProductId = Convert.ToInt32(dr["ProductId"]);
                    pro.ProductName = dr["ProductName"].ToString();
                    pro.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    pro.CategoryName = dr["CategoryName"].ToString();
                    Plist.Add(pro);
                }
            }
            con.Close();
            return Plist;
        }

        public Product GetProductById(int id)
        {
            Product pro=new Product();
            cmd = new SqlCommand("Select p.*,c.* from Product p join Category c on p.CategoryId=c.CategoryId where CategoryId=@id",con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    pro.ProductId = Convert.ToInt32(dr["ProductId"]);
                    pro.ProductName = dr["ProductName"].ToString();
                    pro.CategoryName = dr["CategoryName"].ToString();
                }
            }
            con.Close();
            return pro;
        }

        public int AddProduct(Product pro)
        {
            int result = 0;
            string qry = "Insert into Product values(@ProductName,@CategoryId)";
            cmd=new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@ProductName",pro.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", pro.CategoryId);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int UpdateProduct(Product pro)
        {
            int result = 0;
            string qry = "Update Product set ProductName=@ProductName, CategoryId=@CategoryId where ProductId=@ProductId";
            cmd=new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@ProductName", pro.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", pro.CategoryId);
            cmd.Parameters.AddWithValue("@ProductId", pro.ProductId);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int DeleteProduct(int id)
        {
            int result = 0;
            string qry = "Delete from Product where ProductId=@id";
            cmd=new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
