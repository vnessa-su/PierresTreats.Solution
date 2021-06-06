using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PierresTreats.Models
{
  public class Order
  {
    public int OrderId { get; set; }

    [Display(Name = "Ordered Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Desired Delivery Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
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