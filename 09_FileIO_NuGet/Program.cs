using System;
using System.IO; // Dosya yazma/okuma için (C'deki stdio.h file fonksiyonları muadili)
using System.Collections.Generic;
using Newtonsoft.Json; // NuGet'ten indirdiğimiz ekstra (harici) kütüphane!

namespace _09_FileIO_NuGet
{
    class User
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- ADIM 9: DOSYA İŞLEMLERİ (I/O) ve NuGET PAKET YÖNETİCİSİ ---\n");

            // --- 1. Temel Dosya Yazma ve Okuma ---
            Console.WriteLine("1. Metin Dosyası İşlemleri:");
            
            string filePath = "notlar.txt";
            
            // C'deki fopen, fprintf, fclose mantığının C#'taki en kısa yolu:
            // Bu kod dosyayı açar, içine yazar ve Ram'de yer kaplamaması için anında kapatır (fclose yapar).
            File.WriteAllText(filePath, "Merhaba! Bu metin C# ile tek satir komutla diske yazildi.\nSatir 2");
            Console.WriteLine($"> '{filePath}' adli dosya C Sharp projesinin yanina kaydedildi.");

            // Dosyayı baştan sona okumak da aynı şekilde tek satırdır:
            string fileContent = File.ReadAllText(filePath);
            Console.WriteLine("\nDisk uzerindeki dosyadan okunan icerik:");
            Console.WriteLine(fileContent);


            // --- 2. JSON İşlemleri ve NuGet Paketleri ---
            Console.WriteLine("\n------------------------------------\n");
            Console.WriteLine("2. JSON Serileştirme (Serialization) ve NuGet Örneği:");
            
            // C veya C++'ta string manipülasyonu yaparak JSON (JavaScript Object Notation) formatı basmak bir kabustur.
            // C#'ta ".NET Ekosisteminin App Store'u" diyebileceğimiz "NuGet" sistemi üzerinden
            // Microsoft'un veya dışarıdan binlerce yazılımcının ürettiği hazır kod kütüphanelerini saniyeler içinde indirebiliriz.
            // Biz bu projeyi kurarken dünyanın en popüler paketi olan "Newtonsoft.Json" paketini kurduk!

            List<User> users = new List<User>
            {
                new User { Name = "Ahmet", Role = "Admin", Age = 25 },
                new User { Name = "Selin", Role = "Editor", Age = 30 }
            };

            // Liste halindeki C# nesnelerimizi alıp, internette her cihazın (Mobil, Web)
            // anlayabileceği standart ve evrensel metin formatına (JSON string) çeviriyoruz:
            string jsonText = JsonConvert.SerializeObject(users, Formatting.Indented);
            
            Console.WriteLine("Nesneler (C# objeleri) başarıyla JSON formatına dönüştürüldü:\n");
            Console.WriteLine(jsonText);

            // Bunu da disk üzerinde veritabanı gibi tutmak için bir dosyaya kaydedelim
            File.WriteAllText("kullanicilar.json", jsonText);
            Console.WriteLine("\n> 'kullanicilar.json' diske kaydedildi! Klasörden gidip açıp bakabilirsiniz.");

            Console.WriteLine("\n===========================");
            Console.WriteLine("Tebrikler! 9 adımlık temel eğitim ve kanıtlama dosya serisini başarıyla tamamladınız.");
            Console.WriteLine("===========================");
            
            Console.WriteLine("Kapatmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
