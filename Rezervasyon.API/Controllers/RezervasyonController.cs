using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rezervasyon.API.Entities;
using Rezervasyon.API.Requests;
using Rezervasyon.API.Response;

namespace Rezervasyon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervasyonController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public RezervasyonController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Yap([FromBody] RezervasyonYapRequest request)
        {
            var tren = await _dbContext.Trenler
                .Where(x => x.Ad == request.Tren.Ad)
                .Include(x => x.Vagonlar)
                .FirstOrDefaultAsync();

            var islemBasarili = false;
            var rezYapilacakKisi = request.RezervasyonYapilacakKisiSayisi;
            var toplamRezervesyonYapilanKisiSayisi = 0;

            var yerlesimAyrinti = new List<YerlesimAyrinti>();

            foreach (var vagon in tren.Vagonlar)
            {
                var onlineKapasite = vagon.Kapasite - vagon.Kapasite / 100 * 30;

                var kalanKoltuk = onlineKapasite - vagon.DoluKoltukAdet;

                if (kalanKoltuk > 0)
                {
                    var yapilanRezervasyonSayisi = 0;

                    for (int i = 0;
                         i < rezYapilacakKisi - toplamRezervesyonYapilanKisiSayisi &&
                         kalanKoltuk > yapilanRezervasyonSayisi;
                         i++)
                    {
                        vagon.DoluKoltukAdet++;
                        _dbContext.Vagonlar.Update(vagon);
                        await _dbContext.SaveChangesAsync();
                        yapilanRezervasyonSayisi++;
                    }

                    yerlesimAyrinti.Add(new YerlesimAyrinti
                        { VagonAdi = vagon.Ad, KisiSayisi = yapilanRezervasyonSayisi });
                    toplamRezervesyonYapilanKisiSayisi += yapilanRezervasyonSayisi;

                    if (!request.KisilerFarkliVagonlaraYerlestirilebilir)
                    {
                        break;
                    }

                    if (request.RezervasyonYapilacakKisiSayisi == toplamRezervesyonYapilanKisiSayisi)
                    {
                        islemBasarili = true;
                        break;
                    }
                }
            }

            return Ok(new RezervasyonYapResponse()
            {
                RezervasyonYapilabilir = islemBasarili,
                YerlesimAyrinti = yerlesimAyrinti
            });
        }
    }
}