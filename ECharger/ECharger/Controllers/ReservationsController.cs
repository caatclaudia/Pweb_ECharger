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

namespace ECharger.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.ChargingStation).Include(r => r.PaymentMethod).Include(r => r.UserCard);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.ChargingStationID = new SelectList(db.ChargingStations, "ID", "Name");
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "ID", "Name");
            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StartTime,TotalPrice,ChargingStationID,UserCardID,PaymentMethodID,EndTime")] Reservation reservation)
        {
            reservation.ChargingStation = db.ChargingStations.Where(i => i.ID == reservation.ChargingStationID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                reservation.updateTotalPrice();
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChargingStationID = new SelectList(db.ChargingStations, "ID", "Name", reservation.ChargingStationID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "ID", "Name", reservation.PaymentMethodID);
            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email", reservation.UserCardID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChargingStationID = new SelectList(db.ChargingStations, "ID", "Name", reservation.ChargingStationID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "ID", "Name", reservation.PaymentMethodID);
            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email", reservation.UserCardID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StartTime,TotalPrice,ChargingStationID,UserCardID,PaymentMethodID,EndTime")] Reservation reservation)
        {
            reservation.ChargingStation = db.ChargingStations.Where(i => i.ID == reservation.ChargingStationID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                reservation.updateTotalPrice();
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChargingStationID = new SelectList(db.ChargingStations, "ID", "Name", reservation.ChargingStationID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "ID", "Name", reservation.PaymentMethodID);
            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email", reservation.UserCardID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
