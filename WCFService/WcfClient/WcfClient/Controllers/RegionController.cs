using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WcfClient.Models;

namespace WcfClient.Controllers
{
    public class RegionController : Controller
    {
        // GET: Region
        public ActionResult Index()
        {
            NW.Northwind northwind = new NW.Northwind();
            DataSet ds = northwind.GetAllRegion();

            List<Models.Region> regions = new List<Models.Region>();
            foreach (DataRow item in ds.Tables[0].Rows) 
            {
                Models.Region reg = new Models.Region();
                reg.RegionID = Convert.ToInt32(item["RegionID"]);
                reg.RegionDescription = item["RegionDescription"].ToString();

                regions.Add(reg);
            }
            return View(regions);
        }

        // GET: Region/Details/5
        public ActionResult Details(int id)
        {
            NW.Northwind northwind = new NW.Northwind();
            DataSet ds = northwind.GetRegionById(id);

            Models.Region reg = new Models.Region();
            reg.RegionID = Convert.ToInt32(ds.Tables[0].Rows[0]["RegionID"]);
            reg.RegionDescription = ds.Tables[0].Rows[0]["RegionDescription"].ToString();
            return View(reg);
        }

        // GET: Region/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Region/Create
        [HttpPost]
        public ActionResult Create(Models.Region obj)
        {
            try
            {
                // TODO: Add insert logic here

                NW.Northwind northwind = new NW.Northwind();

                int result = northwind.InsertRegion(obj.RegionID, obj.RegionDescription);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Region/Edit/5
        public ActionResult Edit(int id)
        {
            NW.Northwind northwind = new NW.Northwind();
            DataSet ds = northwind.GetRegionById(id);

            Models.Region reg = new Models.Region();
            reg.RegionID = Convert.ToInt32(ds.Tables[0].Rows[0]["RegionID"]);
            reg.RegionDescription = ds.Tables[0].Rows[0]["RegionDescription"].ToString();
            return View(reg);
        }

        // POST: Region/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Region obj)
        {
            try
            {
                // TODO: Add update logic here

                NW.Northwind northwind = new NW.Northwind();

                int result = northwind.UpdateRegion(obj.RegionID, obj.RegionDescription);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Region/Delete/5
        public ActionResult Delete(int id)
        {
            NW.Northwind northwind = new NW.Northwind();
            DataSet ds = northwind.GetRegionById(id);

            Models.Region reg = new Models.Region();
            reg.RegionID = Convert.ToInt32(ds.Tables[0].Rows[0]["RegionID"]);
            reg.RegionDescription = ds.Tables[0].Rows[0]["RegionDescription"].ToString();
            return View(reg);
        }

        // POST: Region/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                NW.Northwind northwind = new NW.Northwind();

                int result = northwind.DeleteRegion(id);

                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
