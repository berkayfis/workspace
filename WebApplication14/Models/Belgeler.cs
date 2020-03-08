using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication14.Models
{
    public partial class Belgeler
    {
        [Key]
        public Guid Id { get; set; }
        public string Isim { get; set; }
        public string ErisimYolu { get; set; }
    }
}
