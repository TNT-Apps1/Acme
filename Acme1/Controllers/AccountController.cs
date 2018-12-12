using Acme1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using System.Data.SqlClient;
using System.Configuration;
using Acme1.Models;
using System.Data;

namespace Acme1.Controllers
{
    public class AccountController : Controller
    {


        SqlConnection dbcon = new SqlConnection(
        ConfigurationManager.ConnectionStrings["acmedb"].ConnectionString.ToString());

        //// GET: Account
        //public ActionResult Login()
        //{
        //   ViewBag.ReturnUrl = Request.QueryString["ReturnUrl"];
        //    return View();
        //}

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.QueryString["returnurl"] == null)
                return RedirectToAction("Index", "Home");
            Loginvm loginvm = new Loginvm();
            ViewBag.message = "";
            return View(loginvm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(Loginvm login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbcon.Open();
                    Customer cust = Customer.GetCustomerSingle(dbcon, 0, login.Username);
                    dbcon.Close();
                    if (cust.CustNumber > 0 && cust.PWD == login.Password)
                    {
                        string ReturnUrl = Request.QueryString["returnurl"].ToString();
                        if (ReturnUrl.Length > 1 && Url.IsLocalUrl(ReturnUrl))
                        {
                            Session["custid"] = cust.CustNumber;
                            FormsAuthentication.SetAuthCookie(login.Username, false);
                            return Redirect(ReturnUrl);
                        }
                        else
                            return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            }
            ViewBag.message = "Credentials are not valid";
            return View(login);
        }


    }
}