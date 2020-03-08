using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class Duyuru
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string Eklenti { get; set; }
        public DateTime? Zaman { get; set; }
        public int? KoordinatorNo { get; set; }

        public virtual ProjeKoordinatoru Koordinator { get; set; }
    }
}
