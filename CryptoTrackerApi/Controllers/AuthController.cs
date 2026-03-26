using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CryptoTrackerApi.Data;
using CryptoTrackerApi.Models;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CryptoTrackerApi.Controllers
{
    // DTO (Data Transfer Object): Kullanıcıdan gelen "Ham şifreyi" yolda havada kapmak (taşıyıcı olmak) için özel kargo sepeti.
    public class UserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    [ApiController] 
    [Route("api/[controller]")] // Adresimiz: http://sitemiz.com/api/Auth olacak
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor (Bağımlılık Enjeksiyonu - Sipariş Defterini API Garsonuna Ver)
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // --- 1. KAYIT OLMA (REGISTER) İŞLEMİ ---
        [HttpPost("register")] // http://.../api/auth/register
        public async Task<IActionResult> Register(UserDto request)
        {
            // Acaba veritabanında aynı kullanıcı isminde daha önceden kayıtlı bir müşteri var mı?
            var userExists = await _context.Users.AnyAsync(u => u.Username == request.Username);
            if (userExists) 
                return BadRequest("Bu kullanıcı adı çoktan alınmış!");

            // -- SİBER GÜVENLİK (HASHING) BAŞLIYOR --
            // Müşterinin yazdığı düz "12345" gibi şifreyi Microsoft'un SHA256 siber algoritmasından geçirip anlamsız karmaşık metne (Hash) döndürüyoruz.
            using var sha256 = SHA256.Create();
            var hashedPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));

            // Gerçek "User" (Müşteri) objesini yukarıdaki şifreli ve GÜVENLİ hâliyle yaratıyoruz:
            var user = new User
            {
                Username = request.Username,
                PasswordHash = hashedPassword, // BÜYÜK KURAL: DÜZ ŞİFREYİ ASLA KAYDETMİYORUZ! Şifresi dönüştürülmüş halini kayda alıyoruz.
                Role = "User" // İleride yetkilendirme yapmak için "Admin" veya "User"
            };

            _context.Users.Add(user);          // C# Veritabanı defterine ekle
            await _context.SaveChangesAsync(); // SQL veritabanına fiziksel olarak KAYDET

            return Ok("Tebrikler, başarıyla kayıt olundu! Artık veritabanında şifreli bir yeriniz var.");
        }

        // --- 2. GİRİŞ YAPMA (LOGIN) İŞLEMİ ---
        [HttpPost("login")] // http://.../api/auth/login
        public async Task<IActionResult> Login(UserDto request)
        {
            // Adamın girdiği 'Username' (Örn: ahmet123) veritabanımızda gerçekten kayıtlı mı?
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            
            if (user == null) 
                return BadRequest("Böyle bir kullanıcı maalesef sistemde bulunamadı.");

            // -- KULLANICI ŞİFRESİ DOĞRULAMA (Verification) --
            // Müşterinin formdan yolladığı düz "12345" şifresini AYNI siber cihazdan (SHA256) geçiriyoruz.
            using var sha256 = SHA256.Create();
            var hashToCheck = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));

            // Şimdi az önce üretilen sonucu, adamın taa ilk kayıt olduğunda veritabanına yazılmış olan o "Anlamsız yazıyla (PasswordHash)" çarpıştırıyoruz. 
            // Biz yazılımcı olarak kullanıcının 12345 şifresini bilemeyiz, ama eğer iki anlamsız metin birbirini tutarsa, girdiği metin asıl şifresidir der ve onu onaylarız!
            if (user.PasswordHash != hashToCheck) 
                return BadRequest("Hatalı şifre girdiniz!");

            // --- (Buraya ileriki adımda JWT Token -Sanal Bilet- oluşturma sistemini ekleyeceğiz) ---
                        // --- JWT (SANAL GÜVENLİK BİLETİ) ÜRETME SİSTEMİ BAŞLIYOR ---
            
            // 1. Şirketimize ait "Çok Gizli" bir imza anahtarı. Biz okuruz ama hacker şifreyi bilemez.
            // Gerçek Kurumsal projelerde bu şifreler farklı bir gizli "Environment Config" dosyasında tutulur, koda asla yazılmaz!
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BenimMuthisKriptoSunucumIcinCokGizliSifreAnahtarim123!"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // Bileti bu gizli silahla Mühürle

            // 2. Biletin (Token) içine basılacak ve internete verilecek bilgiler (Örn: "Bu bilet Username'i tutar, Yetkisi User'dır" vb.)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // 3. Bileti "Kesecek Matbaa" Tanımlanıyor: (Kimlik Bilgilerini, bileti kimlerin kabul edeceğini (Audience), gizli İmzayı birleştirir)
            var token = new JwtSecurityToken(
                issuer: "CryptoTrackerApi",
                audience: "CryptoTrackerMusterileri",
                claims: claims,
                expires: DateTime.Now.AddHours(2), // 2 saat sonra bilet yanar/iptal olur (müşteri tekrar şifresini yollayıp login yapmak zorunda kalır)
                signingCredentials: credentials);

            // C# sınıf objesi olarak üretilen karışık Bileti son bir kez Metne (örn: "eyJhbG...f7Tdfdf") çeviriyor.
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // 4. Müşteriye o meşhur bileti teslim et!
            return Ok(new { Mesaj = "Giriş Mükemmel Şekilde Başarılı!", SizinBiletiniz = tokenString });

        }
    }
}
