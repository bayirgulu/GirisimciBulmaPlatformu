using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GirisimciBulmaPlatform.Shared.Models
{
    public partial class Fikir
    {
        public Fikir()
        {
            Fotorafs = new HashSet<Fotoraf>();
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Fikir için Başlık Gerekli")]
        public string? FikirBaslik { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Fikir Açıklama Gerekli")]
		public string? FikirAciklama { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Fikirin Detayı Gerekli")]
		public string? FikirDetay { get; set; }
        public string? BosAlan { get; set; }
        public int? KullaniciId { get; set; }
        public int? KatagoriId { get; set; }
        public string? KapakFoto { get; set; }

        public virtual Katagori? Katagori { get; set; }
        public virtual Kullanici? Kullanici { get; set; }
        public virtual ICollection<Fotoraf> Fotorafs { get; set; }
    }
}
