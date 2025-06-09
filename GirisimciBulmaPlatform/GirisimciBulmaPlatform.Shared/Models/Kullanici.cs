using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GirisimciBulmaPlatform.Shared.Models
{
    public partial class Kullanici
    {
        public Kullanici()
        {
            Fikirs = new HashSet<Fikir>();
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Kullanici Adi Gerekli")]
        public string KullaniciAdi { get; set; } = null!;

		[Required(AllowEmptyStrings = false, ErrorMessage = "Şifre Gerekli")]
		public string KullaniciSifre { get; set; } = null!;
        public string? MailAdres { get; set; }
        public string? Telefon { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string? AdSoyad { get; set; }
        public string? Foto { get; set; }

        public virtual ICollection<Fikir> Fikirs { get; set; }
    }
}
