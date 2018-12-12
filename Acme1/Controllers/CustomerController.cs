using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.SqlClient;
using System.Configuration;
using Acme1.Models;
using System.Data;

namespace Acme1.Controllers
{
    public class CustomerController : Controller
    {

        SqlConnection dbcon = new SqlConnection(
        ConfigurationManager.ConnectionStrings["acmedb"].ConnectionString.ToString());

        // GET: Customer
        [Authorize]
        public ActionResult Update()
        {
            dbcon.Open();
            int custid = (int)Session["custid"];
            Customer cust = Customer.GetCustomerSingle(dbcon, custid, "");
            dbcon.Close();
            return View(cust);
        }

    }
}