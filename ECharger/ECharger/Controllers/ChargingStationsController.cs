using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECharger;
using ECharger.Models;
using Microsoft.AspNet.Identity;

namespace ECharger.Controllers
{
    public class ChargingStationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ChargingStations
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Company))
            {
                var companyID = User.Identity.GetUserId();
                return View(db.ChargingStations.Where(c => c.CompanyID == companyID));
            }
            return View(db.ChargingStations.ToList());
        }

        // GET: ChargingStations/Map
        public ActionResult Map()
        {
            return View();
        }

        // GET: ChargingStations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChargingStation chargingStation = db.ChargingStations.Find(id);
            if (chargingStation == null)
            {
                return HttpNotFound();
            }
            return View(chargingStation);
        }

        public void setupCompanyIdViewBag()
        {
            var roles = db.Roles.Where(r => r.Name == RoleName.Company);
            if (roles.Any())
            {
                var roleId = roles.First().Id;
                var companyIds = db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId));
                var company = companyIds.First();
                ViewBag.CompanyID = new SelectList(companyIds, "ID", "Email");
            }
        }

        // GET: ChargingStations/Create
        public ActionResult Create()
        {
            setupCompanyIdViewBag();
            return View();
        }

        // POST: ChargingStations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,StreetName,City,Operator,Latitude,Longitude,CompanyID,PricePerMinute")] ChargingStation chargingStation)
        {
            if (User.IsInRole(RoleName.Company))
            {
                chargingStation.CompanyID = User.Identity.GetUserId();
            }

            if (ModelState.IsValid)
            {
                db.ChargingStations.Add(chargingStation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            setupCompanyIdViewBag();
            return View(chargingStation);
        }

        // GET: ChargingStations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChargingStation chargingStation = db.ChargingStations.Find(id);
            if (chargingStation == null)
            {
                return HttpNotFound();
            }
            setupCompanyIdViewBag();
            return View(chargingStation);
        }

        // POST: ChargingStations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,StreetName,City,Operator,Latitude,Longitude,CompanyID,PricePerMinute")] ChargingStation chargingStation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chargingStation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            setupCompanyIdViewBag();
            return View(chargingStation);
        }

        // GET: ChargingStations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChargingStation chargingStation = db.ChargingStations.Find(id);
            if (chargingStation == null)
            {
                return HttpNotFound();
            }
            return View(chargingStation);
        }

        // POST: ChargingStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChargingStation chargingStation = db.ChargingStations.Find(id);
            db.ChargingStations.Remove(chargingStation);
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
