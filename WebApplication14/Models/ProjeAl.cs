using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class ProjeAl
    {
        public int Id { get; set; }
        public int? ProjeNo { get; set; }
        public string OgrNo1 { get; set; }
        public string OgrNo2 { get; set; }
        public string Form2 { get; set; }
        public string ProjeDurumu { get; set; }
        public string KabulDurumu { get; set; }
        public string KurulAciklama { get; set; }
        public int? OgrenciOneriNo { get; set; }
        public string Ararapor1 { get; set; }
        public string Ararapor2 { get; set; }
        public string Finalrapor { get; set; }
        public string Finalkitap { get; set; }
        public string Butunlemerapor { get; set; }
        public string Butunlemekitap { get; set; }
        public int? AsistanId { get; set; }

        public virtual AkademikPersonel Asistan { get; set; }
        public virtual Ogrenci OgrNo1Navigation { get; set; }
        public virtual Ogrenci OgrNo2Navigation { get; set; }
        public virtual OgrenciProjeOnerisi OgrenciOneriNoNavigation { get; set; }
        public virtual ProjeOnerileri ProjeNoNavigation { get; set; }
    }
}
