using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rota Adresi: http://localhost:port/api/Finance
    
    // İŞTE BÜYÜK SİLAHIMIZ: [Authorize] KİLİDİ!
    // Bu odaya (Finance) sadece ve sadece cebinde geçerli bir JWT Token (Sanal Bilet) olan VIP müşteriler girebilir!
    // Kayıt olmayan veya Token'ı olmayan biri bu adrese istek atarsa C#, aşağıdaki kodları daha hiç okutmadan adamı kapıdan 401 Unauthorized (Yetkisiz Erişim) diyerek kovar!
    [Authorize] 
    
    public class FinanceController : ControllerBase
    {
        private readonly HttpClient _httpClient; // Dışarıya GET/POST atacak aracımız

        // (Bağımlılık Enjeksiyonu): HttpClient aracını (Program.cs'den) enjekte ediyoruz. (new'lemiyoruz)
        public FinanceController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // --- İNTERNETTEN CANLI BİTCOİN FİYATI ÇEKEN C# BOTUMUZ ---
        [HttpGet("crypto")] // Son Adres: http://localhost:port/api/finance/crypto
        public async Task<IActionResult> GetLiveCryptoPrices()
        {
            // Dış borsa olarak hiçbir kısıtlama içermeyen CoinDesk API'sini kullanıyoruz (Canlı Bitcoin güncel Dolar fiyatı çekeceğiz)
            var externalApiUrl = "https://api.coindesk.com/v1/bpi/currentprice.json";

            // C# Botumuz bizim yerimize dış dünyaya (Borsa sunucusuna) gidip GET isteği atıyor (Kilitlenmemesi için await ile işlem bitişini arkaplanda bekliyoruz)
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptoTrackerBot/1.0");
            var response = await _httpClient.GetAsync(externalApiUrl);

            // Eğer karşıki borsa çökmüşse veya cevap vermiyorsa müşterimize siber güvenlikle kendi hata mesajımızı (500) dönüyoruz
            if (!response.IsSuccessStatusCode)
                return StatusCode(500, "Şu an finansal canlı verilere ulaşılamıyor, lütfen daha sonra tekrar deneyin.");

            // Karşı borsadan gelen JSON metnini (Canlı Dolar Fiyatlarını) ham şekilde çekiyoruz
            var jsonString = await response.Content.ReadAsStringAsync();

            // Kendi Müşterimize aldığımız canlı veriyi "Biletin var VIP'sin, buyur canlı kurlar" diyerek direkt önüne sunuyoruz!
            return Content(jsonString, "application/json");
        }
    }
}
