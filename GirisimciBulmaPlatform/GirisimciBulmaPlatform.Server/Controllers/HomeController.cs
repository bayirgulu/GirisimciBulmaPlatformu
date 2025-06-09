using GirisimciBulmaPlatform.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace GirisimciBulmaPlatform.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        GirisimPlatformContext contex=new GirisimPlatformContext();
        // POST: HomeController/Create
        [HttpPost("getall")]
        [ValidateAntiForgeryToken]
        public List<Katagori> Ekle ([FromBody]string ad)
        {
            try
            {
                
                return contex.Katagoris.ToList();

            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        public IEnumerable<Katagori> Get()
        {
            try
            {
                return contex.Katagoris.ToArray();
            }
            catch (Exception)
            {

                return null;
            }
        }
        [HttpPost("GetAllKategoriler")]
        public async Task<IActionResult> GetAllKategoriler()
        {
            try
            {
                var kategoriler = await contex.Katagoris.ToListAsync();
                return Ok(kategoriler);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);               
            }
           
        }
        [HttpPost("GetKatogoriFikir")]
        public async Task<IActionResult> GetFikirKatogoriId([FromBody] string id )
        {
            int intValue;
            if (int.TryParse(id, out intValue))
            {
                var kategoriler = await contex.Fikirs.Where(k => k.KatagoriId == intValue).ToListAsync();
              
                return Ok(kategoriler); 
            }
            else
            {
                return BadRequest("gönderilen değer bir sayi değeri olmalı");
            }

        }


		[HttpPost("SorgulaKullanici")]
		public async Task<IActionResult> KullaniciSorgula([FromBody] Kullanici user)
		{
            if (user!=null)
            {
                string kullaiciadi = user.KullaniciAdi.Trim();
                string sifre = user.KullaniciSifre.Trim();
                Kullanici? User = contex.Kullanicis.FirstOrDefault(k => k.KullaniciAdi == kullaiciadi.Trim() && k.KullaniciSifre.Trim() == sifre);
                if (User != null) return Ok(User);
            }
            return NotFound();
		}


        [HttpPost("KullaniciEkle")]
        public async Task<IActionResult> KullaniciEkle([FromBody] Kullanici user)
        {
            try
            {
				if (user != null)
				{
                    var foundedUser = contex.Kullanicis.FirstOrDefault(k => k.KullaniciAdi.ToLower().Trim() == user.KullaniciAdi.ToLower().Trim());
                    if (foundedUser == null)
                    {
                        user.KullaniciAdi = user.KullaniciAdi.Trim();
                        user.KullaniciSifre = user.KullaniciSifre.Trim();
                        user.AdSoyad = user.AdSoyad.Trim();
                        user.MailAdres = user.MailAdres.Trim();
                        if(!string.IsNullOrEmpty(user.Telefon)) user.Telefon = user.Telefon.Trim();
                        contex.Kullanicis.Add(user);
                        contex.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Kullanıcı adı kullanılıyor!");
                    }
				}
				else
				{
					return BadRequest("Kullanıcı bulunamadı!");
				}
			}
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           

        }

        [HttpGet("images/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                // Dosya MIME tipini belirleme
                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);
                contentType = contentType ?? "application/octet-stream"; // Varsayılan tip
                var imageFileStream = System.IO.File.OpenRead(path);
                return File(imageFileStream, contentType);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost("GetFileInfo")]
        public async Task<IActionResult> GetFileInfo([FromBody] List<string> fotoDosyayollari)
        {
            try
            {
                List<FileInfo> fileInfos = fotoDosyayollari.Select(path => new FileInfo(path)).ToList();
                
                var a = JsonSerializer.Serialize<List<FileInfo>>(fileInfos);
                return Ok(fileInfos.ToArray());
            }
            catch (Exception ex)
            {
                var a = ex;
                throw;
            }
            
          
        }
        [HttpPost("UploadImage")]
        public async Task<IActionResult> Post([FromForm]List<IFormFile> files)
        {
            try
            {
                var a = Request.Form.Files;
                string path = "";
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i] == null || files[i].Length == 0)
                        return BadRequest("Dosya yüklenemedi.");

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", files[i].FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await files[i].CopyToAsync(stream);
                    }
                }
                    
                
                return Ok(new { path });
            }
            catch (Exception ex)
            {

              return BadRequest(ex.Message);
            }
           
        }
        [HttpPost("FikirSil")]
        public async Task<IActionResult> FikirSil([FromBody] Fikir fikir)
        {
            try
            {
                if (fikir!=null)
                {
                    var fotos = contex.Fotorafs.Where(k => k.FikirId == fikir.Id);
                    if (fotos!=null)
                    {
                        contex.Fotorafs.RemoveRange(fotos);
                        
                    }
                    contex.Fikirs.Remove(fikir);
                    contex.SaveChanges();
                    return Ok();
                    
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpPost("FikirEkle")]
		public async Task<IActionResult> FikirEkle([FromBody] Fikir fikir)
		{
			try
			{
				if (fikir != null)
                {
                    var idea = contex.Fikirs.FirstOrDefault(k => k.Id == fikir.Id);
                    if (idea != null) // fikir var ise
                    {
                        var fotos = fikir.Fotorafs.ToList();
                        var deletedFotos = contex.Fotorafs
                            .AsNoTracking()
                            .Where(f => f.FikirId == fikir.Id)
                            .ToList()
                            .Where(f => !fotos.Any(foto => foto.Id == f.Id))
                            .ToList();
                        contex.Fotorafs.RemoveRange(
                            deletedFotos
                        );
                        idea.FikirBaslik = fikir.FikirBaslik.Trim();
                        idea.FikirDetay = fikir.FikirDetay.Trim();
                        idea.FikirAciklama = fikir.FikirAciklama.Trim();
                        idea.KatagoriId = fikir.KatagoriId;
                        idea.Fotorafs = fikir.Fotorafs;
                        contex.SaveChanges();
                        return Ok();
                    }
					contex.Fikirs.Add(fikir);
					contex.SaveChanges();
                    return Ok();
				}
				else
				{
					return BadRequest("Hatalı istek");
				}
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}


		}
        [HttpGet("baseklasor")]
        public async Task<IActionResult> klasor()
        {
            var klasor= Directory.GetCurrentDirectory();
            var klasor1= Environment.CurrentDirectory;
            var klasor2= AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directoryInfo = new DirectoryInfo(klasor);
            DirectoryInfo parentDirectory = directoryInfo.Parent;
            var sub = parentDirectory.FullName;
            return Ok(klasor);
        }
        [HttpGet("kullanicifikir/{id}")]
        public async Task<IActionResult> kullanicifikir(int id)
        {
            try
            {
                var katagoris = contex.Katagoris.ToList().Select(k =>
                {
                    k.Fikirs.Clear();
                    return k;
                });
               return Ok(contex.Fikirs
                   .Where(f => f.KullaniciId == id)
                   .OrderByDescending(f => f.Id)
                   .ToList()
                   .Select(f =>
                   {
                       f.Katagori = katagoris.FirstOrDefault(k => k.Id == f.KatagoriId);
                       return f;
                   })); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("kullaniciadi/{id}")]
        public async Task<IActionResult> kullaniciadi(int id)
        {
            var user = contex.Kullanicis.FirstOrDefault(k => k.Id == id);
            if (user!=null)           
                return Ok(user.AdSoyad);
            
            else           
                return NotFound();
            
        }

        [HttpGet("fikirgetir/{id}")]
        public async Task<IActionResult> fikirgetir(int id)
        {
            try
            {
               var fikir = contex.Fikirs.Include(f=>f.Kullanici).FirstOrDefault(k => k.Id == id);
                
                if (fikir!=null)
                {
                    var foto = contex.Fotorafs.Where(k => k.FikirId == id).ToList();
                    fikir.Fotorafs = foto.Select(f =>
                    {
                        f.Fikir = null;
                        return f;
                    }).ToList();
                    if (fikir.Kullanici != null) fikir.Kullanici.Fikirs.Clear();
                    return Ok(fikir);
                }
                else
                {
                    return BadRequest("Fikir Bulunamadi");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
              
            }
        }

        [HttpGet("fikirlerigetir")]
        public async Task<IActionResult> fikirlerigetir()
        {
            try
            {
                var katagoris = contex.Katagoris.ToList().Select(k =>
                {
                    k.Fikirs.Clear();
                    return k;
                });
                var fikirs = contex.Fikirs.OrderByDescending(f => f.Id).ToList();
                return Ok(fikirs.Select(f =>
                {
                    f.Katagori = katagoris.FirstOrDefault(k => k.Id == f.KatagoriId);
                    return f;
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
              
            }
        }

        [HttpPost("KullaniciGuncelle")]
        public async Task<IActionResult> KullaniciGuncelle([FromBody] Kullanici user)
        {
            try
            {
                var kullanici = contex.Kullanicis.FirstOrDefault(k => k.Id == user.Id);
                if (kullanici!=null)
                {
                    kullanici.AdSoyad=user.AdSoyad.Trim();
                    kullanici.KullaniciAdi=user.KullaniciAdi.Trim();
                    kullanici.KullaniciSifre=user.KullaniciSifre.Trim();
                    kullanici.MailAdres=user.MailAdres.Trim();
                    kullanici.Telefon=user.Telefon?.Trim();
                    kullanici.Foto=user.Foto?.Trim();
                                    
                    var a= contex.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
              
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

		[HttpPost("FotoEkle")]
		public async Task<IActionResult> FotoEkle([FromBody] List<Fotoraf> fotorafs)
		{
			try
			{
				if (fotorafs != null)
				{

					contex.Fotorafs.AddRange(fotorafs);

					contex.SaveChanges();
					return Ok();
				}
				else
				{
					return BadRequest("Lütfen fotoğraf seçiniz!");
				}
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}


		}

	}
}
