using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class ProjeKoordinatoru
    {
        public ProjeKoordinatoru()
        {
            Duyuru = new HashSet<Duyuru>();
        }

        public int Id { get; set; }
        public int? AkademisyenId { get; set; }

        public virtual AkademikPersonel Akademisyen { get; set; }
        public virtual ICollection<Duyuru> Duyuru { get; set; }
    }
}
