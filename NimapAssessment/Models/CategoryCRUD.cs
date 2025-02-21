using Microsoft.Data.SqlClient;

namespace NimapAssessment.Models
{
    public class CategoryCRUD
    {
        private IConfiguration configuration;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        
        public CategoryCRUD(IConfiguration configuration)
        {
            this.configuration = configuration;
            con=new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        }
        
        public IEnumerable<Category> GetCategories()
        {
            List<Category> Clist = new List<Category>();
            cmd = new SqlCommand("Select * from Category", con);
            con.Open();
            dr=cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    Category cat = new Category();
                    cat.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    cat.CategoryName = dr["CategoryName"].ToString();
                    Clist.Add(cat);
                }
            }
            con.Close();
            return Clist;
        }

        public Category GetCategoryById(int id)
        {
            Category cat = new Category();
            cmd = new SqlCommand("Select * from Category where CategoryId=@id",con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cat.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    cat.CategoryName = dr["CategoryName"].ToString();
                }
            }
            con.Close();
            return cat;
        }

        public int AddCategory(Category cat)
        {
            int result = 0;
            string qry = "Insert into Category values(@CategoryName)";
            cmd = new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
            con.Open();
            result= cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int UpdateCategory(Category cat)
        {
            int result = 0;
            string qry = "Update Category set CategoryName=@CategoryName where CategoryId=@CategoryId";
            cmd = new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
            cmd.Parameters.AddWithValue("@CategoryId", cat.CategoryId);
            con.Open();
            result= cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int DeleteCategory(int id)
        {
            int result = 0;
            string qry = "Delete from Category where CategoryId=@id";
            cmd = new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result= cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
