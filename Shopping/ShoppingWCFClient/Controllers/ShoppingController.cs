using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingWCFClient.Models;

namespace ShoppingWCFClient.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult Index()
        {
            try
            {
                Sp.Shopping client = new Sp.Shopping();

                // Load categories for GridView (left)
                DataSet dsCategories = client.GetAllCategories();
                List<Category> categories = new List<Category>();
                foreach (DataRow row in dsCategories.Tables["Categories"].Rows)
                {
                    Category cat = new Category
                    {
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        CategoryName = row["CategoryName"].ToString()
                    };
                    categories.Add(cat);
                }

                // Load products for GridView (right)
                DataSet dsProducts = client.GetProducts(null); // All products
                List<Product> products = new List<Product>();
                foreach (DataRow row in dsProducts.Tables["Products"].Rows)
                {
                    Product prod = new Product
                    {
                        ProductID = Convert.ToInt32(row["ProductID"]),
                        ProductName = row["ProductName"].ToString(),
                        Price = Convert.ToDecimal(row["Price"])
                    };
                    products.Add(prod);
                }

                // Pass data to View
                ViewBag.Categories = categories; // For GridView and Search in Category ComboBox
                ViewBag.SearchByOptions = new List<string> { "Band", "Color", "CPU", "RAM", "Video", "Display" }; // Fixed values
                return View(products);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading data: " + ex.Message;
                return View(new List<Product>());
            }
        }

        // GET: Shopping/ByCategory/1
        public ActionResult ByCategory(int id)
        {
            try
            {
                Sp.Shopping client = new Sp.Shopping();

                // Reload categories for GridView (left)
                DataSet dsCategories = client.GetAllCategories();
                List<Category> categories = new List<Category>();
                foreach (DataRow row in dsCategories.Tables["Categories"].Rows)
                {
                    Category cat = new Category
                    {
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        CategoryName = row["CategoryName"].ToString()
                    };
                    categories.Add(cat);
                }

                // Load products for selected category
                DataSet dsProducts = client.GetProducts(id);
                List<Product> products = new List<Product>();
                foreach (DataRow row in dsProducts.Tables["Products"].Rows)
                {
                    Product prod = new Product
                    {
                        ProductID = Convert.ToInt32(row["ProductID"]),
                        ProductName = row["ProductName"].ToString(),
                        Price = Convert.ToDecimal(row["Price"])
                    };
                    products.Add(prod);
                }

                // Pass data to View
                ViewBag.Categories = categories;
                ViewBag.SearchByOptions = new List<string> { "Band", "Color", "CPU", "RAM", "Video", "Display" };
                return View("Index", products);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading category: " + ex.Message;
                return View("Index", new List<Product>());
            }
        }

        // POST: Shopping/Search
        [HttpPost]
        public ActionResult Search(int? categoryId, string property, string value)
        {
            try
            {
                Sp.Shopping client = new Sp.Shopping();

                // Reload categories for GridView (left)
                DataSet dsCategories = client.GetAllCategories();
                List<Category> categories = new List<Category>();
                foreach (DataRow row in dsCategories.Tables["Categories"].Rows)
                {
                    Category cat = new Category
                    {
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        CategoryName = row["CategoryName"].ToString()
                    };
                    categories.Add(cat);
                }

                // Search products
                DataSet dsProducts = client.Search(categoryId, property, value);
                List<Product> products = new List<Product>();
                foreach (DataRow row in dsProducts.Tables["Products"].Rows)
                {
                    Product prod = new Product
                    {
                        ProductID = Convert.ToInt32(row["ProductID"]),
                        ProductName = row["ProductName"].ToString(),
                        Price = Convert.ToDecimal(row["Price"])
                    };
                    products.Add(prod);
                }

                // Pass data to View
                ViewBag.Categories = categories;
                ViewBag.SearchByOptions = new List<string> { "Band", "Color", "CPU", "RAM", "Video", "Display" };
                return View("Index", products);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error searching products: " + ex.Message;
                return View("Index", new List<Product>());
            }
        }

        // GET: Shopping/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shopping/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shopping/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shopping/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shopping/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shopping/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shopping/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
