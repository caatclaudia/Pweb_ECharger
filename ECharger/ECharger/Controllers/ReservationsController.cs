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
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.User))
            {
                var userId = User.Identity.GetUserId();
                var userReservations = db.Reservations.Include(r => r.ChargingStation).Include(r => r.PaymentMethod).Where(u => u.UserCardID==userId);
                return View("UserIndex", userReservations.ToList());
            }

            if (User.IsInRole(RoleName.Company))
            {
                var companyId = User.Identity.GetUserId();
                var companyReservations = db.Reservations
                    .Include(r => r.ChargingStation)
                    .Include(r => r.UserCard)
                    .Where(r => r.ChargingStation.CompanyID == companyId);
                return View("CompanyIndex", companyReservations.ToList());
            }

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
            Reservation reservation = db.Reservations.Include(r => r.ChargingStation).Include(r => r.PaymentMethod).Include(r => r.UserCard).Where(r => r.ID == id).SingleOrDefault();
            if (reservation == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole(RoleName.Company))
            {
                return View("CompanyDetails", reservation);
            }

            return View(reservation);
        }

        // GET: Reservations/MapCreate/ChargingStationID
        public ActionResult MapCreate(int? id)
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

            if (User.IsInRole(RoleName.User))
            {
                var userID = User.Identity.GetUserId();
                var userPaymentMethods = db.PaymentMethods.Where(m => m.UserCardID == userID);
                ViewBag.PaymentMethodID = new SelectList(userPaymentMethods, "ID", "Name");

                var userReservation = new Reservation { UserCardID = userID, ChargingStationID = (int)id };

                return View("MapUserCreate", userReservation);
            }

            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "ID", "Name");
            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email");
            var reservation = new Reservation { ChargingStationID = (int)id };
            return View("MapAdminCreate", reservation);
        }

        // POST: Reservations/MapCreate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MapCreate([Bind(Include = "ID,StartTime,TotalPrice,ChargingStationID,UserCardID,PaymentMethodID,EndTime")] Reservation reservation)
        {
            reservation.ChargingStation = db.ChargingStations.Where(i => i.ID == reservation.ChargingStationID).SingleOrDefault();

            if (ModelState.IsValid)
            {
                reservation.updateTotalPrice();
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "ID", "Name", reservation.PaymentMethodID);

            if (User.IsInRole(RoleName.User))
            {
                reservation.UserCardID = User.Identity.GetUserId();
                return View("UserCreate", reservation);
            }

            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email", reservation.UserCardID);
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.ChargingStationID = new SelectList(db.ChargingStations, "ID", "Name");

            if (User.IsInRole(RoleName.User))
            {
                var userID = User.Identity.GetUserId();
                var userPaymentMethods = db.PaymentMethods.Where(m => m.UserCardID==userID);
                ViewBag.PaymentMethodID = new SelectList(userPaymentMethods, "ID", "Name");

                var reservation = new Reservation { UserCardID = userID };

                return View("UserCreate", reservation);
            }

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
            reservation.ChargingStation = db.ChargingStations.Where(i => i.ID == reservation.ChargingStationID).SingleOrDefault();

            if (ModelState.IsValid)
            {
                reservation.updateTotalPrice();
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChargingStationID = new SelectList(db.ChargingStations, "ID", "Name", reservation.ChargingStationID);
            ViewBag.PaymentMethodID = new SelectList(db.PaymentMethods, "ID", "Name", reservation.PaymentMethodID);

            if (User.IsInRole(RoleName.User))
            {
                reservation.UserCardID = User.Identity.GetUserId();
                return View("UserCreate", reservation);
            }

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

            if (User.IsInRole(RoleName.User))
            {
                reservation.UserCardID = User.Identity.GetUserId();
                return View("UserEdit", reservation);
            }

            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email", reservation.UserCardID);
            
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StartTime,TotalPrice,UserCardID,ChargingStationID,PaymentMethodID,EndTime")] Reservation reservation)
        {
            if (User.IsInRole(RoleName.User))
            {
                reservation.UserCardID = User.Identity.GetUserId();
            }

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

            if (User.IsInRole(RoleName.User))
            {
                reservation.UserCardID = User.Identity.GetUserId();
                return View("UserEdit", reservation);
            }

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
            Reservation reservation = db.Reservations.Include(r => r.ChargingStation).Include(r => r.PaymentMethod).Include(r => r.UserCard).Where(u => u.ID == id).SingleOrDefault();
            if (reservation == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole(RoleName.User))
            {
               return View("UserDelete", reservation);
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
