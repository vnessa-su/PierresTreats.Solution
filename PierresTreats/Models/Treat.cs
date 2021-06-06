using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PierresTreats.Models
{
  public class Treat
  {
    public int TreatId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
    public decimal Price { get; set; }

    public virtual ICollection<FlavorTreat> FlavorTreatJoinEntities { get; }
    public virtual ApplicationUser User { get; set; }

    public Treat()
    {
      this.FlavorTreatJoinEntities = new HashSet<FlavorTreat>();
    }
  }
}