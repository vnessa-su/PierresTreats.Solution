using System;
using System.Collections.Generic;

namespace PierresTreats.Models
{
  public class Order
  {
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public virtual ICollection<OrderTreat> OrderTreatJoinEntities { get; }
    public virtual ApplicationUser User { get; set; }

    public Order()
    {
      this.OrderTreatJoinEntities = new HashSet<OrderTreat>();
      this.OrderDate = DateTime.Now;
    }

    public decimal GetOrderTotal()
    {
      decimal orderTotal = 0;
      foreach (OrderTreat entry in this.OrderTreatJoinEntities)
      {
        decimal lineItemPrice = (decimal)entry.TreatQuantity * entry.Treat.Price;
        orderTotal += lineItemPrice;
      }
      return orderTotal;
    }
  }
}