using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class EskiKabulGorenProjeler
    {
        public int Id { get; set; }
        public int? DanismanId { get; set; }
        public string Isim { get; set; }
        public int? OturumNo { get; set; }
        public int? KisiSayisi { get; set; }
        public int? GrupSayisi { get; set; }
        public string Turu { get; set; }
        public string Form1 { get; set; }

        public virtual AkademikPersonel Danisman { get; set; }
        public virtual AlanOturum OturumNoNavigation { get; set; }
    }
}
