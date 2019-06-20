using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CRUDMVCWE.Models;

namespace CRUDMVCWE.Controllers
{
    public class ProductController : Controller
    {
        String connectionString = @"Data Source =(localdb)\ProjectsV13;  Initial Catalog = MvcCRUDDB; Integrated Security= true";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtproduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter dt = new SqlDataAdapter("select * from Product",sqlcon);
                dt.Fill(dtproduct);
            }
            return View(dtproduct);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        public ActionResult Create(ProductModel PM)
        {
            using (SqlConnection  sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                String query = "INSERT INTO Product VALUES (@ProductName,@Price,@Counte)";
                SqlCommand cmd = new SqlCommand(query,sqlcon);
                cmd.Parameters.AddWithValue("@ProductName", PM.ProductName);
                cmd.Parameters.AddWithValue("@Price", PM.Price);
                cmd.Parameters.AddWithValue("@Counte", PM.Counte);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel pr = new ProductModel();
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                String query = "select * from Product where ProductID = @ProductID";
                SqlDataAdapter dta = new SqlDataAdapter(query, cn);
                dta.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                dta.Fill(dt);

            }
            if(dt.Rows.Count == 1)
            {
                pr.ProductID = Convert.ToInt32(dt.Rows[0][0].ToString());
                pr.ProductName = dt.Rows[0][1].ToString();
                pr.Price = dt.Rows[0][2].ToString();
                pr.Counte = Convert.ToInt32(dt.Rows[0][3].ToString());
                return View(pr);
            }
            return RedirectToAction("Index");
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel PM)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                String query = "UPDATE Product set ProductName = @ProductName ,Price =@Price,Counte = @Counte where ProductID=@ProductID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@ProductID", PM.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", PM.ProductName);
                cmd.Parameters.AddWithValue("@Price", PM.Price);
                cmd.Parameters.AddWithValue("@Counte", PM.Counte);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                String query = "delete from Product where ProductID=@ProductID";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@ProductID", id);
            
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

       
    }
}
