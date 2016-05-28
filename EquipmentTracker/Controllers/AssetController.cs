using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquipmentTracker.DAL;
using EquipmentTracker.Models;
using EquipmentTracker.ViewModels;

namespace EquipmentTracker.Controllers
{
    public class AssetController : Controller
    {
        private EquipmentContext db = new EquipmentContext();

        // GET: Asset
        public ActionResult Index(string id, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.AssetSortParm = String.IsNullOrEmpty(sortOrder) ? "asset_desc" : "";
            ViewBag.DescriptionSortParm = sortOrder == "description" ? "description_desc" : "description";

            var viewModel = new AssetIndexData();
            viewModel.Assets = db.Assets
                .Include(i => i.WorkOrders)
                .OrderBy(i => i.AssetID);

            switch (sortOrder)
            {
                case "asset_desc":
                    viewModel.Assets = viewModel.Assets.OrderByDescending(s => s.AssetID);
                    break;
                case "description":
                    viewModel.Assets = viewModel.Assets.OrderBy(s => s.Description);
                    break;
                case "description_desc":
                    viewModel.Assets = viewModel.Assets.OrderByDescending(s => s.Description);
                    break;
                default:
                    viewModel.Assets = viewModel.Assets.OrderBy(s => s.AssetID);
                    break;
            }

            if (!String.IsNullOrEmpty(id))
            {
                ViewBag.AssetID = id;
                var selectedAsset = viewModel.Assets.Where(x => x.AssetID == id).Single();
                db.Entry(selectedAsset).Collection(x => x.WorkOrders).Load();
                foreach(WorkOrder workOrder in selectedAsset.WorkOrders)
                {
                    db.Entry(workOrder).Reference(x => x.Asset).Load();
                }
                
                viewModel.WorkOrders = selectedAsset.WorkOrders;
            }
            
            return View(viewModel);
        }

        // GET: Asset/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: Asset/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Asset/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssetID,Description,SerialNumber")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Assets.Add(asset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asset);
        }

        // GET: Asset/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Asset/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssetID,Description,SerialNumber")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asset);
        }

        // GET: Asset/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Asset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Asset asset = db.Assets.Find(id);
            db.Assets.Remove(asset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
