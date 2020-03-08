﻿using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class EskiBasarisizAlinanProje
    {
        public int Id { get; set; }
        public string Ogrno1 { get; set; }
        public string Ogrno2 { get; set; }
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
    }
}