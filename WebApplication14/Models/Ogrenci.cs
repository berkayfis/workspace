using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication14.Models
{
    public partial class Ogrenci
    {
        public Ogrenci()
        {
            EskiBasarisizAlinanProjeOgrno1Navigation = new HashSet<EskiBasarisizAlinanProje>();
            EskiBasarisizAlinanProjeOgrno2Navigation = new HashSet<EskiBasarisizAlinanProje>();
            IstekOgrNo1Navigation = new HashSet<Istek>();
            IstekOgrNo2Navigation = new HashSet<Istek>();
            OgrenciProjeOnerisiOgrno1Navigation = new HashSet<OgrenciProjeOnerisi>();
            OgrenciProjeOnerisiOgrno2Navigation = new HashSet<OgrenciProjeOnerisi>();
            ProjeAlOgrNo1Navigation = new HashSet<ProjeAl>();
            ProjeAlOgrNo2Navigation = new HashSet<ProjeAl>();
        }

        [Key]
        public string OgrenciNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public virtual ICollection<EskiBasarisizAlinanProje> EskiBasarisizAlinanProjeOgrno1Navigation { get; set; }
        public virtual ICollection<EskiBasarisizAlinanProje> EskiBasarisizAlinanProjeOgrno2Navigation { get; set; }
        public virtual ICollection<Istek> IstekOgrNo1Navigation { get; set; }
        public virtual ICollection<Istek> IstekOgrNo2Navigation { get; set; }
        public virtual ICollection<OgrenciProjeOnerisi> OgrenciProjeOnerisiOgrno1Navigation { get; set; }
        public virtual ICollection<OgrenciProjeOnerisi> OgrenciProjeOnerisiOgrno2Navigation { get; set; }
        public virtual ICollection<ProjeAl> ProjeAlOgrNo1Navigation { get; set; }
        public virtual ICollection<ProjeAl> ProjeAlOgrNo2Navigation { get; set; }
    }
}
