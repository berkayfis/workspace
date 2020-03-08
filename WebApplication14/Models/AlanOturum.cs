using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class AlanOturum
    {
        public AlanOturum()
        {
            EskiBasarisizAlinanProje = new HashSet<EskiBasarisizAlinanProje>();
            EskiKabulGorenProjeler = new HashSet<EskiKabulGorenProjeler>();
            OgrenciProjeOnerisi = new HashSet<OgrenciProjeOnerisi>();
            ProjeOnerileri = new HashSet<ProjeOnerileri>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }

        public virtual ICollection<EskiBasarisizAlinanProje> EskiBasarisizAlinanProje { get; set; }
        public virtual ICollection<EskiKabulGorenProjeler> EskiKabulGorenProjeler { get; set; }
        public virtual ICollection<OgrenciProjeOnerisi> OgrenciProjeOnerisi { get; set; }
        public virtual ICollection<ProjeOnerileri> ProjeOnerileri { get; set; }
    }
}
