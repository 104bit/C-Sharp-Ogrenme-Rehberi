using System;
using System.Threading.Tasks; // Asenkron (Eşzamanlı olmayan) işlemler için gereken ana kütüphane

namespace _08_Async
{
    class Program
    {
        // Geleneksel C'de ve eski sistemlerde Main metodu her zaman senkrondur (sırayla bloğu kilitler).
        // C#'ta (v7.1 ve sonrası) Main metodunu da "async Task" yaparak asenkron çalışmaya uygun hale getirebiliriz.
        static async Task Main(string[] args)
        {
            Console.WriteLine("--- ADIM 8: ASENKRON PROGRAMLAMA (Async / Await) ---\n");

            // Senaryo: Veritabanından büyük bir veri çekiyoruz veya internetten bir dosya indiriyoruz.
            // Eski usül (Senkron) yapsaydık, internet yavaşsa bu işlem bitene kadar tüm program DONARDI.
            // Düğmelere tıklanamaz, program Windows'ta "Yanıt Vermiyor" (Not Responding) hatası verirdi.

            Console.WriteLine("1. Veritabanı sorgusu başlatılıyor... (Lütfen bekleyin)");
            
            // "await" (bekle) kelimesi, bu uzun işlemin bitmesini burada bekle ama 
            // ARAYÜZÜ VEYA ANA İŞParçacığını (Thread) kilitleme/dondurma demektir!
            // İşlemci o sırada gidip arkaplan işlerini veya ekrandaki animasyonları oynatmaya devam eder kapıda paspas olmaz.
            await DownloadDataAsync();
            
            Console.WriteLine("2. Veritabanı sorgusu tamamlandı, veriler alındı!\n");

            // --- Gerçek Güç: Aynı anda birden fazla işi başlatmak ---
            Console.WriteLine("Aynı anda (Paralel) iki dosya indirme işlemi başlatılıyor...\n");
            
            // Metotların başına "await" YAZMADAN çağırdık. 
            // Bu sayede ikisi de aynı anda (farklı thread'lerde) arka planda indirmeye başlar ve bir alt satıra geçeriz!
            Task task1 = DownloadFileAsync("Video.mp4", 3);
            Task task2 = DownloadFileAsync("Muzik.mp3", 2);

            // Çıktıda göreceksiniz ki, bu yazı indirmeler bitmeden önce ekrana anında basılacak.
            // Çünkü program onları beklerken kilitlenip (donup) kalmadı.
            Console.WriteLine("İndirmeler arka planda devam ederken ben başka kodları anında çalıştırabiliyorum...\n");

            // Ancak programı komple kapatmadan önce bu iki arka plan görevinin de kesin bitmesini istiyorsak:
            await Task.WhenAll(task1, task2); // "İki task de bitene kadar asenkron olarak bekle"

            Console.WriteLine("\nTüm asenkron işlemler bitti!");
            Console.WriteLine("Kapatmak için bir tuşa basın...");
            Console.ReadKey();
        }

        // Asenkron bir fonksiyon/metot yazmak için başına "async" ve tipine "Task" ekleriz. 
        // (Task, C'deki void'in Asenkron ve modern halidir)
        // İsimlendirme standardı gereği bu metotların adının sonuna "Async" kelimesi yazılır.
        static async Task DownloadDataAsync()
        {
            // Task.Delay, C'deki uyutma fonksiyonu olan sleep(2000) veya Sleep() gibidir.
            // ANCAK en büyük farkı: thread'i (çalışan iskeleti) dondurmaz/uyutmaz, sadece görevi 2 saniyeliğine askıya alır işlemci başka işe geçer.
            await Task.Delay(2000); 
        }

        static async Task DownloadFileAsync(string fileName, int delaySeconds)
        {
            Console.WriteLine($">> {fileName} indirilmeye başlandı...");
            await Task.Delay(delaySeconds * 1000); // ms cinsinden asenkron bekle
            Console.WriteLine($"<< {fileName} indirme işlemi bitti.");
        }
    }
}
