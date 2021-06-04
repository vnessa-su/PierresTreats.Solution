namespace PierresTreats.Models
{
  public class FlavorTreat
  {
    public int FlavorId { get; set; }
    public int TreatId { get; set; }

    public virtual Flavor Flavor { get; set; }
    public virtual Treat Treat { get; set; }
  }
}