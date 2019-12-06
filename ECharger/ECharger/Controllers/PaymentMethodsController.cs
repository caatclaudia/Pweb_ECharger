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
using ECharger.Models.Data_Models;
using Microsoft.AspNet.Identity;

namespace ECharger.Controllers
{
    public class PaymentMethodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentMethods
        [Authorize(Roles = RoleName.AdminOrUser)]
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.User))
            {
                var userID = User.Identity.GetUserId();
                var userPaymentMethods = db.PaymentMethods.Where(m => m.UserCardID == userID);
                return View("UserIndex", userPaymentMethods);
            }
            var paymentMethods = db.PaymentMethods.Include(p => p.UserCard);
            return View(paymentMethods.ToList());
        }

        // GET: PaymentMethods/Details/5
        [Authorize(Roles = RoleName.AdminOrUser)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethod paymentMethod = db.PaymentMethods.Include(p => p.UserCard).Where(m => m.ID == id).SingleOrDefault();
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Create
        [Authorize(Roles = RoleName.AdminOrUser)]
        public ActionResult Create()
        {
            if (User.IsInRole(RoleName.User))
            {
                var userID = User.Identity.GetUserId();
                var userPaymentMethods = db.PaymentMethods.Where(m => m.UserCardID == userID);
                ViewBag.PaymentMethodID = new SelectList(userPaymentMethods, "ID", "Name");

                var paymentMethod = new PaymentMethod { UserCardID = userID };

                return View("UserCreate", paymentMethod);
            }
            
            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email");
            return View();
        }

        // POST: PaymentMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,UserCardID,Value")] PaymentMethod paymentMethod)
        {
            if (NamePaymentMethodRepeat(paymentMethod))
                ModelState.AddModelError("Name", "This name already exists!");

            if (ModelState.IsValid)
            {
                db.PaymentMethods.Add(paymentMethod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (User.IsInRole(RoleName.User))
            {
                paymentMethod.UserCardID = User.Identity.GetUserId();
                return View("UserCreate", paymentMethod);
            }

            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email", paymentMethod.UserCardID);
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Edit/5
        [Authorize(Roles = RoleName.AdminOrUser)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethod paymentMethod = db.PaymentMethods.Find(id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole(RoleName.User))
            {
                paymentMethod.UserCardID = User.Identity.GetUserId();
                return View("UserEdit", paymentMethod);
            }
            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email", paymentMethod.UserCardID);
            return View(paymentMethod);
        }

        // POST: PaymentMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,UserCardID,Value")] PaymentMethod paymentMethod)
        {
            if (User.IsInRole(RoleName.User))
            {
                paymentMethod.UserCardID = User.Identity.GetUserId();
            }

            if (ModelState.IsValid)
            {
                db.Entry(paymentMethod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (User.IsInRole(RoleName.User))
            {
                paymentMethod.UserCardID = User.Identity.GetUserId();
                return View("UserEdit", paymentMethod);
            }

            ViewBag.UserCardID = new SelectList(db.UserCards, "ID", "Email", paymentMethod.UserCardID);
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Delete/5
        [Authorize(Roles = RoleName.AdminOrUser)]
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethod paymentMethod = db.PaymentMethods.Include(p => p.UserCard).Where(m => m.ID == id).SingleOrDefault();
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole(RoleName.User))
            {
                return View("UserDelete", paymentMethod);
            }

            return View(paymentMethod);
        }

        // POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentMethod paymentMethod = db.PaymentMethods.Find(id);
            db.PaymentMethods.Remove(paymentMethod);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: PaymentMethods/Edit/5
        [Authorize(Roles = RoleName.AdminOrUser)]
        public ActionResult Charge(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethod paymentMethod = db.PaymentMethods.Find(id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }

            ChargePaymentMethod chargePaymentMethod = new ChargePaymentMethod() { PaymentMethodID = paymentMethod.ID, PaymentMethod=paymentMethod };

            return View(chargePaymentMethod);
        }

        // POST: PaymentMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Charge([Bind(Include = "PaymentMethodID,ChargingValue")] ChargePaymentMethod chargePaymentMethod)
        {
            PaymentMethod paymentMethod = db.PaymentMethods.Find(chargePaymentMethod.PaymentMethodID);

            if (ModelState.IsValid)
            {
                paymentMethod.Value += chargePaymentMethod.ChargingValue;
                db.Entry(paymentMethod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            chargePaymentMethod.PaymentMethod = paymentMethod;

            return View(chargePaymentMethod);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [NonAction]
        private bool NamePaymentMethodRepeat(PaymentMethod paymentMethod)
        {
            if (db.PaymentMethods.Where(n => n.Name == paymentMethod.Name && n.ID != paymentMethod.ID).FirstOrDefault() == null)
                return false;
            return true;
        }
    }
}
