using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class AkademikPersonel
    {
        public AkademikPersonel()
        {
            EskiBasarisizAlinanProje = new HashSet<EskiBasarisizAlinanProje>();
            EskiKabulGorenProjeler = new HashSet<EskiKabulGorenProjeler>();
            OgrenciProjeOnerisi = new HashSet<OgrenciProjeOnerisi>();
            ProjeAl = new HashSet<ProjeAl>();
            ProjeKoordinatoru = new HashSet<ProjeKoordinatoru>();
            ProjeOnerileri = new HashSet<ProjeOnerileri>();
        }

        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Anabilimdali { get; set; }
        public string Unvan { get; set; }
        public string Kisaltma { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }

        public Guid MessageId { get; set; }


        public virtual ICollection<EskiBasarisizAlinanProje> EskiBasarisizAlinanProje { get; set; }
        public virtual ICollection<EskiKabulGorenProjeler> EskiKabulGorenProjeler { get; set; }
        public virtual ICollection<OgrenciProjeOnerisi> OgrenciProjeOnerisi { get; set; }
        public virtual ICollection<ProjeAl> ProjeAl { get; set; }
        public virtual ICollection<ProjeKoordinatoru> ProjeKoordinatoru { get; set; }
        public virtual ICollection<ProjeOnerileri> ProjeOnerileri { get; set; }
    }
}
