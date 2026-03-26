using System;

namespace CryptoTrackerApi.Models
{
    // Sistemimize Kayıt Olacak Kullanıcıların SQL Veritabanı Tablosu
    public class User
    {
        public int Id { get; set; }           // EF Core bunun Primary Key (Otomatik Sayı/Eşsiz Kimlik) olduğunu anlar
        public string Username { get; set; }  // Müşterinin seçeceği Kullanıcı Adı
        
        // BÜYÜK KURAL: Gerçek dünyada şifreler veritabanında asla "12345" gibi düz metin (plain text) tutulmaz.
        // SHA-256 veya Bcrypt gibi şifreleme algoritmalarıyla dönüştürülüp karmaşık harf yığınlarına (Hash) çevrilir!
        public string PasswordHash { get; set; } 
        
        public string Role { get; set; }      // Kişinin Yetki Rolü (Örn: "Admin", "User")
    }
}
