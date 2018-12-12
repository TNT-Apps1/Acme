using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Acme1.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace Acme1.Controllers
{
    public class ProductController : Controller
    {

        SqlConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["Acmedb"].ConnectionString.ToString());


        // GET: Product for index page list
        public ActionResult Index()
        {
            List<Product> productList;
            try
            {
                dbcon.Open();
                productList = Product.GetProductList(dbcon, "");
                dbcon.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return View(productList);

        }

        public ActionResult Create()
        {
            Product product = new Product();
            product.ImageFile = "nopic.jpg";
            return View(product);
        }


        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase uploadfile)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if(uploadfile!= null && uploadfile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(uploadfile.FileName);
                        var path = Path.Combine(
                        Server.MapPath("~/Content/Images/products"), fileName);
                        uploadfile.SaveAs(path);
                        product.ImageFile = fileName;
                    }

                    dbcon.Open();
                    int intresult = Product.CUDProduct(dbcon, "create", product);
                    dbcon.Close();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (dbcon.State == ConnectionState.Open)
                    {
                        dbcon.Close();
                        ViewBag.errormsg = ex.Message;
                        return View("_Error");
                    }
                
            }
            
            }
            ViewBag.errormsg = "Data validation error in Edit method";
            return View("_Error");
        }


        // GET: Product
        public ActionResult Edit(string id="")
        {
            if (Regex.IsMatch(id, @"^[a-zA-Z0-9]{2,10}$"))
            {
                try
                {
                    dbcon.Open();
                    Product product = Product.GetProductSingle(dbcon, id);
                    dbcon.Close();
                    if (product.ProductID != null)
                    {
                        return View(product);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


            }
            
                ViewBag.errormsg = "Invalid data in the Edit Page";
                return View("_Error");
            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product prod, HttpPostedFileBase uploadfile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadfile != null && uploadfile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(uploadfile.FileName);
                        var path = Path.Combine(
                        Server.MapPath("~/Content/Images/products"), fileName);
                        uploadfile.SaveAs(path);
                        prod.ImageFile = fileName;
                    }
                    //Rest of Update code. Copy and modify the code
                    //from the Post Edit action method in the mvc_db_tutorial.pdf here
                    dbcon.Open();
                    int intresult = Product.CUDProduct(dbcon, "update", prod);
                    dbcon.Close();
                    return RedirectToAction("Index");                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            } //valid data
            ViewBag.errmsg = "Data validation error in Edit method";
            return View("Error");
        }


        // GET: Product
        public ActionResult Delete(string id = "")
        {
            if (Regex.IsMatch(id, @"^[a-zA-Z0-9]{2,10}$"))
            {
                try
                {
                    dbcon.Open();
                    Product product = Product.GetProductSingle(dbcon, id);
                    dbcon.Close();
                    if (product.ProductID != null)
                    {
                        return View(product);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


            }

            ViewBag.errormsg = "Invalid data in the Edit Page";
            return View("_Error");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product prod, HttpPostedFileBase uploadfile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //if (uploadfile != null && uploadfile.ContentLength > 0)
                    //{
                    //    var fileName = Path.GetFileName(uploadfile.FileName);
                    //    var path = Path.Combine(
                    //    Server.MapPath("~/Content/Images/products"), fileName);
                    //    uploadfile.SaveAs(path);
                    //    prod.ImageFile = fileName;
                    //}
                    //Rest of Update code. Copy and modify the code
                    //from the Post Edit action method in the mvc_db_tutorial.pdf here
                    dbcon.Open();
                    int intresult = Product.CUDProduct(dbcon, "delete", prod);
                    dbcon.Close();
                    return RedirectToAction("Index");
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            } //valid data
            ViewBag.errmsg = "Data validation error in Edit method";
            return View("Error");
        }


    }
}