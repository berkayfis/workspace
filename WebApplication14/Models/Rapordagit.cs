using System;
using System.Collections.Generic;

namespace WebApplication14.Models
{
    public partial class Rapordagit
    {
        public int Id { get; set; }
        public int? Arsgorid { get; set; }
        public int? Projeid { get; set; }

        public virtual AkademikPersonel Arsgor { get; set; }
        public virtual ProjeAl Proje { get; set; }
    }
}
