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
  public class FlavorsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(PierresTreatsContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Flavor> allFlavors = _db.Flavors.ToList();
      return View(allFlavors);
    }

    public ActionResult Create()
    {
      ViewBag.Treats = _db.Treats.ToList();
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor, int[] treatIds)
    {
      foreach (int treatId in treatIds)
      {
        bool entryExists = _db.FlavorTreat
          .Any(entry => entry.FlavorId == flavor.FlavorId && entry.TreatId == treatId);
        if (!entryExists)
        {
          _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = flavor.FlavorId, TreatId = treatId });
        }
      }
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Flavor selectedFlavor = _db.Flavors
        .Include(flavor => flavor.FlavorTreatJoinEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(selectedFlavor);
    }

    public ActionResult Edit(int id)
    {
      Flavor selectedFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(selectedFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = flavor.FlavorId });
    }

    public ActionResult Delete(int id)
    {
      Flavor selectedFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(selectedFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Flavor selectedFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(selectedFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTreat(int id)
    {
      Flavor selectedFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.Treats = _db.Treats.ToList();
      return View(selectedFlavor);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int[] treatIds)
    {
      foreach (int treatId in treatIds)
      {
        bool entryExists = _db.FlavorTreat
          .Any(entry => entry.FlavorId == flavor.FlavorId && entry.TreatId == treatId);
        if (!entryExists)
        {
          _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = flavor.FlavorId, TreatId = treatId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = flavor.FlavorId });
    }

    [HttpPost]
    public ActionResult RemoveTreat(int joinId)
    {
      FlavorTreat joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      int flavorId = joinEntry.FlavorId;
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = flavorId });
    }
  }
}