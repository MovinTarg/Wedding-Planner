using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wedding_Planner.Models;

namespace Wedding_Planner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingPlannerContext _weddingPlannerContext;
        public HomeController (WeddingPlannerContext context)
        {
            _weddingPlannerContext = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [Route("/dashboard")]
        public IActionResult Dashboard()
        {
            int? ActiveUserId = HttpContext.Session.GetInt32("ActiveUserId");
            if(ActiveUserId == null)
                return RedirectToAction("Index");
            List<Wedding> AllWeddings = _weddingPlannerContext.Wedding
            .OrderBy(q => q.WeddingDate)
            .Include(w => w.Attendees)
            .ThenInclude(g => g.Attendee)
            .ToList();
            ViewBag.AllWeddings = AllWeddings;
            ViewBag.ActiveUserId = (int)HttpContext.Session.GetInt32("ActiveUserId");
            return View("Dashboard");
        }
        [Route("wedding/{WeddingId}")]
        public IActionResult Wedding(int WeddingId)
        {
          Wedding Wedding = _weddingPlannerContext.Wedding
            .Include(w => w.Attendees)
            .ThenInclude(a => a.Attendee)
            .Where(w => w.WeddingId == WeddingId)
            .SingleOrDefault();
          ViewBag.Wedding = Wedding;
          return View("Wedding");
        }
        [Route("/new")]
        public IActionResult New()
        {
            return View("New");
        }
        [HttpPost]
        [Route("/user/register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User EmailCheck = _weddingPlannerContext.User.SingleOrDefault(User => User.Email == model.Email);
                if (EmailCheck == null)
                {
                    User newUser = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = model.Password,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _weddingPlannerContext.Add(newUser);
                    _weddingPlannerContext.SaveChanges();
                    int ActiveUserId = _weddingPlannerContext.User.Last().UserId;
                    HttpContext.Session.SetInt32("ActiveUserId", ActiveUserId);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.RegisterMessages = "Email Taken!";
                }
            }
            return View("Index");
        }
        [Route("/user/login")]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User ActiveUser = _weddingPlannerContext.User.SingleOrDefault(User => User.Email == model.Email);
                if(ActiveUser != null)
                {
                    if(model.Password == ActiveUser.Password)
                    {
                        HttpContext.Session.SetInt32("ActiveUserId", ActiveUser.UserId);
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ViewBag.Messages = "Incorrect Email / Password";
                    }
                }
            }
            return View("Index");
        }
        [Route("/wedding/create")]
        public IActionResult Create(Wedding model)
        {
            if (ModelState.IsValid)
            {
                int? ActiveUserId = HttpContext.Session.GetInt32("ActiveUserId");
                User User = _weddingPlannerContext.User.Where(u => u.UserId == ActiveUserId).SingleOrDefault();
                Wedding newWedding = new Wedding
                {
                    WedderOne = model.WedderOne,
                    WedderTwo = model.WedderTwo,
                    WeddingDate = model.WeddingDate,
                    WeddingAddress = model.WeddingAddress,
                    Planner = User,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                _weddingPlannerContext.Add(newWedding);
                _weddingPlannerContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("New");
        }
        [Route("/wedding/{WeddingId}/rsvp")]
        public IActionResult RSVP(int WeddingId)
        {
            Guest newGuest = new Guest
            {
                UserId = (int)HttpContext.Session.GetInt32("ActiveUserId"),
                WeddingId = WeddingId
            };
            _weddingPlannerContext.Add(newGuest);
            _weddingPlannerContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [Route("/wedding/{WeddingId}/unrsvp")]
        public IActionResult UnRSVP(int WeddingId)
        {
            Guest removeGuest =  _weddingPlannerContext.Guest
                .Where(g => g.WeddingId == WeddingId && g.UserId == (int)HttpContext.Session.GetInt32("ActiveUserId"))
                .SingleOrDefault();
            _weddingPlannerContext.Remove(removeGuest);
            _weddingPlannerContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [Route("/wedding/{WeddingId}/delete")]
        public IActionResult Delete(int WeddingId)
        {
            Wedding deleteWedding = _weddingPlannerContext.Wedding
                .Where(w => w.WeddingId == WeddingId)
                .SingleOrDefault();
            var AttendingGuests = _weddingPlannerContext.Guest.Where(g => g.WeddingId == WeddingId);
            foreach(var guest in AttendingGuests)
            {
                _weddingPlannerContext.Guest.Remove(guest);
            }
            _weddingPlannerContext.Wedding.Remove(deleteWedding);
            _weddingPlannerContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}
