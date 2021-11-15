using Microsoft.AspNet.Identity;
using MVC.Data;
using MVC.Models;
using MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace MVC_APP.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BookService(userId);
            var model = service.GetBooks();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAdd model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBookService();

            if (service.CreateBook(model))
            {
                TempData["SaveResult"] = "Your Book was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Book could not be created.");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateBookService();
            var model = svc.GetBooksById(id);
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateBookService();
            var detail = service.GetBooksById(id);
            var model =
                new BookEdit
                {
                    ID = detail.BookId,
                    Name = detail.Name,
                    Author = detail.Author,
                    Customer = detail.Customer
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,BookEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.ID != id)
            {
                ModelState.AddModelError("","Id does not match.");
                return View(model);
            }
            var service = CreateBookService();
            if(service.UpdateBook(model))
            {
                TempData["SaveResult"] = "Your Book was Updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Book Could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBookService();
            var model = svc.GetBooksById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(int id)
        {
            var service = CreateBookService();
            service.DeleteBook(id);
            TempData["SaveResult"] = "Your book has been deleted.";

            return RedirectToAction("Index");
        }

        private BookService CreateBookService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BookService(userId);
            return service;
        }
    }
}