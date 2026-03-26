using Microsoft.EntityFrameworkCore;
using CryptoTrackerApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. VERİTABANI KÖPRÜSÜNÜ KUR (Önceki işimiz)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=crypto.db"));

// 2. GÜVENLİK (BİLET KONTROL KAPI POLİSLERİ) DOSYAYA EKLENDİ
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,   // Bileti kimin kestiğini kontrol et
            ValidateAudience = true, // Biletin kime kesildiğini kontrol et
            ValidateLifetime = true, // Biletin 2 saati dolmuş mu (tarihi geçmiş mi) kontrol et
            ValidateIssuerSigningKey = true, // BİLET İMZASI SAHTE Mİ DİYE KONTROL ET!
            ValidIssuer = "CryptoTrackerApi", // Matbaa ismimiz
            ValidAudience = "CryptoTrackerMusterileri", // Beklediğimiz müşteri
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BenimMuthisKriptoSunucumIcinCokGizliSifreAnahtarim123!")) // Matbaadaki o çok gizli Sır imzamız!
        };
    });

// 3. DIŞ DÜNYADAN (Örn: Binance vb.) CANLI VERİ ÇEKME ARACIMIZI AÇIYORUZ
builder.Services.AddHttpClient();

// Standart API Ayarları
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 4. POLİSLERİ KAPIYA DİK (Bu Kodların Sırası Çok Önemlidir!)
app.UseAuthentication(); // ÖNCE KİMLİK (BİLET) KONTROLÜ YAP !!
app.UseAuthorization();  // SONRA İÇERİ GİRİŞ / DOLAŞMA YETKİSİ VER !!

app.MapControllers();
app.Run();
