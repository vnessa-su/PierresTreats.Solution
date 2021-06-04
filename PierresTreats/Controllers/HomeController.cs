using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;

namespace PierresTreats.Controllers
{
  public class HomeController : Controller
  {
    private readonly PierresTreatsContext _db;

    public HomeController(PierresTreatsContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.TreatList = _db.Treats.ToList();
      ViewBag.FlavorList = _db.Flavors.ToList();
      return View();
    }

  }
}