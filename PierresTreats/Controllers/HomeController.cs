using Microsoft.AspNetCore.Mvc;

namespace PierresTreats.Controllers
{
  public class HomeController : Controller
  {
    private readonly FactoryContext _db;

    public HomeController(FactoryContext db)
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