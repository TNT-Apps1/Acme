using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Acme1.Models
{
    public class Customer
    {
        [Required, Key]
        public int CustNumber { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PWD { get; set; }
        public static Customer GetCustomerSingle(SqlConnection dbcon, int custid,
        string email)
        {
            Customer obj = new Customer();
            SqlCommand cmd;
            if (custid > 0)
            {
                cmd = new SqlCommand("select * from customers " +
                "where custnumber = @id", dbcon);
                cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = custid;
            }
            else
            {
                cmd = new SqlCommand("select * from customers " +
                "where email = @email", dbcon);
                cmd.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = email;
            }
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                obj.CustNumber = Convert.ToInt32(rdr["Custnumber"].ToString());
                obj.Email = rdr["email"].ToString();
                obj.LastName = rdr["lastname"].ToString();
                obj.FirstName = rdr["firstname"].ToString();
                obj.Address = rdr["address"].ToString();
                obj.City = rdr["city"].ToString();
                obj.State = rdr["state"].ToString();
                obj.ZipCode = rdr["zipcode"].ToString();
                obj.PhoneNumber = rdr["phonenumber"].ToString();
                obj.PWD = rdr["pwd"].ToString();
            }
            rdr.Close();
            return obj;
        }
    }

}