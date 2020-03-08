using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class Istek
    {
        public int Id { get; set; }
        public int? ProjeId { get; set; }
        public string OgrNo1 { get; set; }
        public string OgrNo2 { get; set; }
        public string Form2 { get; set; }

        public virtual Ogrenci OgrNo1Navigation { get; set; }
        public virtual Ogrenci OgrNo2Navigation { get; set; }
        public virtual ProjeOnerileri Proje { get; set; }
    }
}
