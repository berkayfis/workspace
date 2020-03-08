using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class ProjeOnerileri
    {
        public ProjeOnerileri()
        {
            Istek = new HashSet<Istek>();
            ProjeAl = new HashSet<ProjeAl>();
        }

        public int Id { get; set; }
        public int? DanismanId { get; set; }
        public string Isim { get; set; }
        public int? OturumNo { get; set; }
        public int? KisiSayisi { get; set; }
        public int? GrupSayisi { get; set; }
        public string Kategori { get; set; }
        public string Form1 { get; set; }

        public virtual AkademikPersonel Danisman { get; set; }
        public virtual AlanOturum OturumNoNavigation { get; set; }
        public virtual ICollection<Istek> Istek { get; set; }
        public virtual ICollection<ProjeAl> ProjeAl { get; set; }
    }
}
