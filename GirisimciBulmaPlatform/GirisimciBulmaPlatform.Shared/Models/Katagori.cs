using System;
using System.Collections.Generic;

namespace GirisimciBulmaPlatform.Shared.Models
{
    public partial class Katagori
    {
        public Katagori()
        {
            Fikirs = new HashSet<Fikir>();
        }

        public int Id { get; set; }
        public string? KatagoriAdi { get; set; }
        public string? BosAlan { get; set; }

        public virtual ICollection<Fikir> Fikirs { get; set; }
    }
}
