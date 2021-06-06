namespace PierresTreats.Models
{
  public class OrderTreat
  {
    public int OrderTreatId { get; set; }
    public int OrderId { get; set; }
    public int TreatId { get; set; }
    public int TreatQuantity { get; set; }

    public virtual Order Order { get; set; }
    public virtual Treat Treat { get; set; }
  }
}