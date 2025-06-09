using System;
using System.Collections.Generic;

namespace GirisimciBulmaPlatform.Shared.Models
{
    public partial class Fotoraf
    {
        public int Id { get; set; }
        public string? FotoAciklama { get; set; }
        public string? FotoDosyaYolu { get; set; }
        public int? FikirId { get; set; }
        public string? BosAlan { get; set; }

        public virtual Fikir? Fikir { get; set; }
    }
}
