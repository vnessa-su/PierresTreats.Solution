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
  public class OrdersController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrdersController(PierresTreatsContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      List<Order> allUserOrders = _db.Orders.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(allUserOrders);
    }

    public ActionResult Create()
    {
      ViewBag.Treats = _db.Treats.ToList();
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Order order, int[] treatIds)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      order.User = currentUser;
      _db.Orders.Add(order);
      _db.SaveChanges();
      foreach (int treatId in treatIds)
      {
        bool entryExists = _db.OrderTreat
          .Any(entry => entry.OrderId == order.OrderId && entry.TreatId == treatId);
        if (!entryExists)
        {
          _db.OrderTreat.Add(new OrderTreat() { OrderId = order.OrderId, TreatId = treatId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Order selectedOrder = _db.Orders
        .Include(order => order.OrderTreatJoinEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(order => order.OrderId == id);
      return View(selectedOrder);
    }

    public ActionResult Edit(int id)
    {
      Order selectedOrder = _db.Orders.FirstOrDefault(order => order.OrderId == id);
      return View(selectedOrder);
    }

    [HttpPost]
    public ActionResult Edit(Order order)
    {
      _db.Entry(order).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = order.OrderId });
    }

    public ActionResult Delete(int id)
    {
      Order selectedOrder = _db.Orders.FirstOrDefault(order => order.OrderId == id);
      return View(selectedOrder);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Order selectedOrder = _db.Orders.FirstOrDefault(order => order.OrderId == id);
      _db.Orders.Remove(selectedOrder);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTreat(int id)
    {
      Order selectedOrder = _db.Orders.FirstOrDefault(order => order.OrderId == id);
      ViewBag.Treats = _db.Treats.ToList();
      return View(selectedOrder);
    }

    [HttpPost]
    public ActionResult AddTreat(Order order, int[] treatIds)
    {
      foreach (int treatId in treatIds)
      {
        bool entryExists = _db.OrderTreat
          .Any(entry => entry.OrderId == order.OrderId && entry.TreatId == treatId);
        if (!entryExists)
        {
          _db.OrderTreat.Add(new OrderTreat() { OrderId = order.OrderId, TreatId = treatId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = order.OrderId });
    }

    [HttpPost]
    public ActionResult RemoveTreat(int joinId)
    {
      OrderTreat joinEntry = _db.OrderTreat.FirstOrDefault(entry => entry.OrderTreatId == joinId);
      int orderId = joinEntry.OrderId;
      _db.OrderTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = orderId });
    }
  }
}