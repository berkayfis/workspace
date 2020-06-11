using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication14.Models
{
    public partial class FinalSinav
    {
        [Key]
        public int id { get; set; }
        public string projeAdı { get; set; }
        public string projeTipi { get; set; }
        public int akademisyenID1 { get; set; }
        public string akademisyenKisaltma1 { get; set; }
        public int akademisyenID2 { get; set; }
        public string akademisyenKisaltma2 { get; set; }
        public int akademisyenID3 { get; set; }
        public string akademisyenKisaltma3 { get; set; }
        public string ogrNo1 { get; set; }
        public string ogrNo2 { get; set; }
        public string Seans { get; set; }
        public string Sinif { get; set; }
        public DateTime? SinavTarihi { get; set; }
    }
}
