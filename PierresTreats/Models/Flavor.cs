using System.Collections.Generic;

namespace PierresTreats.Models
{
  public class Flavor
  {
    public int FlavorId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<FlavorTreat> FlavorTreatJoinEntities { get; }
    public virtual ApplicationUser User { get; set; }

    public Flavor()
    {
      this.FlavorTreatJoinEntities = new HashSet<FlavorTreat>();
    }
  }
}