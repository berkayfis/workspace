using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class OgrenciProjeOnerisi
    {
        public OgrenciProjeOnerisi()
        {
            ProjeAl = new HashSet<ProjeAl>();
        }

        public int Id { get; set; }
        public string Ogrenci1No { get; set; }
        public string Ogrenci2No { get; set; }
        public int? Danismanid { get; set; }
        public string Isim { get; set; }
        public int? OturumNo { get; set; }
        public string Turu { get; set; }
        public string Form2 { get; set; }
        public string Statu { get; set; }

        public virtual AkademikPersonel Danisman { get; set; }
        public virtual Ogrenci Ogrno1Navigation { get; set; }
        public virtual Ogrenci Ogrno2Navigation { get; set; }
        public virtual AlanOturum OturumNoNavigation { get; set; }
        public virtual ICollection<ProjeAl> ProjeAl { get; set; }
    }
}
