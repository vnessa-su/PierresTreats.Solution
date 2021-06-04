using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PierresTreats.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(PierresTreatsContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Treat> allTreats = _db.Treats.ToList();
      return View(allTreats);
    }

    public ActionResult Create()
    {
      ViewBag.Flavors = _db.Flavors.ToList();
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat, int[] flavorIds)
    {
      foreach (int flavorId in flavorIds)
      {
        bool entryExists = _db.FlavorTreat
          .Any(entry => entry.FlavorId == flavorId && entry.TreatId == treat.TreatId);
        if (!entryExists)
        {
          _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = flavorId, TreatId = treat.TreatId });
        }
      }
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Treat selectedTreat = _db.Treats
        .Include(treat => treat.FlavorTreatJoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      return View(selectedTreat);
    }

    public ActionResult Edit(int id)
    {
      Treat selectedTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(selectedTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = treat.TreatId });
    }

    public ActionResult Delete(int id)
    {
      Treat selectedTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(selectedTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Treat selectedTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(selectedTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddFlavor(int id)
    {
      Treat selectedTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.Flavors = _db.Flavors.ToList();
      return View(selectedTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int[] flavorIds)
    {
      foreach (int flavorId in flavorIds)
      {
        bool entryExists = _db.FlavorTreat
          .Any(entry => entry.FlavorId == flavorId && entry.TreatId == treat.TreatId);
        if (!entryExists)
        {
          _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = flavorId, TreatId = treat.TreatId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = treat.TreatId });
    }

    [HttpPost]
    public ActionResult RemoveFlavor(int joinId)
    {
      FlavorTreat joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      int treatId = joinEntry.TreatId;
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = treatId });
    }
  }
}