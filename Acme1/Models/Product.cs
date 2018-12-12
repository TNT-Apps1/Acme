using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Acme1.Models
{
    public class Product
    {
        [Required, Key, StringLength(10)]
        //[RegularExpression("^[a-zA-Z0-9_]{2-10}$", ErrorMessage = "Product ID is not valid")]
        public string ProductID { get; set; }
        [Required, Key, StringLength(20)]
        public string Name { get; set; }
        [Required, Key, StringLength(100)]
        public string ShortDescription { get; set; }
        [Required, Key, StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string LongDescription { get; set; }
        [Required, Key, StringLength(10)]
        public string CategoryID { get; set; }
        [Required, Key, StringLength(30)]
        public string ImageFile { get; set; }
        [Required, Range(maximum:999.99, minimum:.01)]
        public decimal UnitPrice { get; set; }
        [Required, Range(maximum: 999, minimum: 0)]
        public int OnHand { get; set; }

        public string imageFileToString
        {
            get
            {
                return this.ImageFile.Replace(".jpg", "");
            }
        }

        public static Product GetProductSingle(SqlConnection dbcon, string id)
        {
            Product obj = new Product();
            string strsql = "select * from Products where ProductID = '" + id + "'";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //put object data property assignment statements here
                if (rdr["ProductID"] != DBNull.Value) obj.ProductID = rdr["ProductID"].ToString();
                if (rdr["Name"] != DBNull.Value) obj.Name = rdr["Name"].ToString();
                if (rdr["ShortDescription"] != DBNull.Value) obj.ShortDescription = rdr["ShortDescription"].ToString();
                if (rdr["LongDescription"] != DBNull.Value) obj.LongDescription = rdr["LongDescription"].ToString();
                if (rdr["CategoryID"] != DBNull.Value) obj.CategoryID = rdr["CategoryID"].ToString();
                if (rdr["ImageFile"] != DBNull.Value) obj.ImageFile = rdr["ImageFile"].ToString();
                if (rdr["UnitPrice"] != DBNull.Value) obj.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"].ToString());
                if (rdr["OnHand"] != DBNull.Value) obj.OnHand = Convert.ToInt32(rdr["OnHand"].ToString());
            }
            rdr.Close();
            return obj;
        }
        public static List<Product> GetProductList(SqlConnection dbcon, string SqlClause)
        {
            List<Product> itemlist = new List<Product>();
            string strsql = "select * from Products " + SqlClause;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Product obj = new Product();
                //put object data property assignment statements here
                if (rdr["ProductID"] != DBNull.Value) obj.ProductID = rdr["ProductID"].ToString();
                if (rdr["Name"] != DBNull.Value) obj.Name = rdr["Name"].ToString();
                if (rdr["ShortDescription"] != DBNull.Value) obj.ShortDescription = rdr["ShortDescription"].ToString();
                if (rdr["LongDescription"] != DBNull.Value) obj.LongDescription = rdr["LongDescription"].ToString();
                if (rdr["CategoryID"] != DBNull.Value) obj.CategoryID = rdr["CategoryID"].ToString();
                if (rdr["ImageFile"] != DBNull.Value) obj.ImageFile = rdr["ImageFile"].ToString();
                if (rdr["UnitPrice"] != DBNull.Value) obj.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"].ToString());
                if (rdr["OnHand"] != DBNull.Value) obj.OnHand = Convert.ToInt32(rdr["OnHand"].ToString());
                itemlist.Add(obj);
            }
            rdr.Close();
            return itemlist;
        }





        public static int CUDProduct(SqlConnection dbcon, string CUDAction, Product obj)
        {
            SqlCommand cmd = new SqlCommand();
            if (CUDAction == "create")
            {
                cmd.CommandText = "insert into Products " +
                "Values (@ProductID,@Name,@ShortDescription,@LongDescription,@CategoryID,@ImageFile,@UnitPrice,@OnHand)";
                //copy parameter assignment statements here
                cmd.Parameters.AddWithValue("@ProductID", SqlDbType.VarChar).Value = obj.ProductID;
                cmd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = obj.Name;
                cmd.Parameters.AddWithValue("@ShortDescription", SqlDbType.VarChar).Value = obj.ShortDescription;
                cmd.Parameters.AddWithValue("@LongDescription", SqlDbType.VarChar).Value = obj.LongDescription;
                cmd.Parameters.AddWithValue("@CategoryID", SqlDbType.VarChar).Value = obj.CategoryID;
                cmd.Parameters.AddWithValue("@ImageFile", SqlDbType.VarChar).Value = obj.ImageFile;
                cmd.Parameters.AddWithValue("@UnitPrice", SqlDbType.Decimal).Value = obj.UnitPrice;
                cmd.Parameters.AddWithValue("@OnHand", SqlDbType.Int).Value = obj.OnHand;
            }
            else if (CUDAction == "update")
            {
                cmd.CommandText = "update Products set Name = @Name,ShortDescription = @ShortDescription,LongDescription = @LongDescription,CategoryID = @CategoryID,ImageFile = @ImageFile,UnitPrice = @UnitPrice,OnHand = @OnHand Where ProductID = @ProductID";
                //copy parameter assignment statements here
                cmd.Parameters.AddWithValue("@ProductID", SqlDbType.VarChar).Value = obj.ProductID;
                cmd.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = obj.Name;
                cmd.Parameters.AddWithValue("@ShortDescription", SqlDbType.VarChar).Value = obj.ShortDescription;
                cmd.Parameters.AddWithValue("@LongDescription", SqlDbType.VarChar).Value = obj.LongDescription;
                cmd.Parameters.AddWithValue("@CategoryID", SqlDbType.VarChar).Value = obj.CategoryID;
                cmd.Parameters.AddWithValue("@ImageFile", SqlDbType.VarChar).Value = obj.ImageFile;
                cmd.Parameters.AddWithValue("@UnitPrice", SqlDbType.Decimal).Value = obj.UnitPrice;
                cmd.Parameters.AddWithValue("@OnHand", SqlDbType.Int).Value = obj.OnHand;
            }
            else if (CUDAction == "delete")
            {
                cmd.CommandText = "delete Products where ProductID = @ProductID";
                cmd.Parameters.AddWithValue("@ProductID", SqlDbType.VarChar).Value = obj.ProductID;
            }
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return intResult;
        }


    }
}