using System;
using System.IO; // Dosya işlemleri için kütüphane

namespace _05_Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- ADIM 5: HATA ZARAFETİ (Exception Handling) ---\n");

            // C Dilinde Hata Yönetimi:
            // Bir fonksiyon başarısız olduğunda genelde -1, 0, NULL veya özel bir struct dönerdi.
            // Programcı her adımdan sonra "if (sonuc == -1) // hata oldu" diye kontrol etmek zorundaydı.
            // Bu da kodun okunabilirliğini düşürür ve unutulduğunda programın anında çökmesine sebep olurdu.
            
            // C#'ta Hata Yönetimi (Try-Catch-Finally):
            // Hata verme ihtimali olan "riskli" blok "try" içine alınır.
            // Beklenmeyen bir durum oluştuğunda program çökmez, "catch" bloğuna atlar ve ilgili hatayı biz çözeriz.

            Console.WriteLine("Senaryo 1: Sıfıra Bölme Hatası (DivideByZeroException)");
            try
            {
                int a = 10;
                int b = 0;
                int result = a / b; // C'de olsa işletim sistemi bunu tespit edip programı doğrudan "Core dump" veya "Segmentation fault" ile öldürürdü!
                
                // Üst satırda hata patladığı an çalışma durur ve Catch'e Atlar. Bu yüzden bu alt satır ASLA ÇALIŞMAZ.
                Console.WriteLine($"Sonuç: {result}");
            }
            catch (DivideByZeroException ex) // Sistemde tanımlı "Sıfıra Bölme" hatası fırlatılırsa burası çalışır.
            {
                // ex.Message: Sistemin ürettiği hatanın İngilizce açıklamasıdır.
                Console.WriteLine($"HATA YAKALANDI: Bir sayıyı sıfıra bölemezsiniz! (Sistem mesajı: {ex.Message})\n");
            }
            catch (Exception ex) 
            {
                // Exception nesnesi tüm hataların "atasını/babasını" temsil eder.
                // Eğer üstteki catch'lere takılmayan beklenmedik başka bir hata olursa son kale olarak buraya düşer.
                Console.WriteLine($"Bilinmeyen bir hata oluştu: {ex.Message}\n");
            }

            // --- Senaryo 2: Format Hatası ---
            Console.WriteLine("Senaryo 2: Geçersiz Metin Çevirimi (FormatException)");
            try
            {
                string text = "Ahmet123";
                // C'deki atoi() mantığı. Ancak metinde harf var, matematikte Ahmet123 diye bir sayı yok!
                int number = Convert.ToInt32(text); 
            }
            catch (FormatException) // Hata nesnesi 'ex'i ekrana yazdırmayacaksak, ismini yazmamıza gerek yoktur, sadece tipini belirtmek yeterlidir.
            {
                Console.WriteLine("HATA YAKALANDI: Çevrilmeye çalışılan metin sadece sayılardan oluşmuyor!\n");
            }

            // --- Senaryo 3: Finally Bloğu (Garanti Kod) ---
            Console.WriteLine("Senaryo 3: Dosya okuma ve 'finally' bloğu");
            StreamReader reader = null; // Dosya okuyucu nesnesi (C'deki FILE* pointer'ı gibi)
            try
            {
                // Bilerek var olmayan bir dosyayı açmaya çalışalım (FileNotFoundException hatası fırlatacaktır)
                reader = new StreamReader("olmayan_dosya.txt");
                Console.WriteLine(reader.ReadLine());
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"HATA: Dosya bulunamadı! {ex.Message}\n");
            }
            finally
            {
                // "finally" bloğu, try kısmında hata olsa da, olmasa da EN SON KESİN OLARAK ÇALIŞACAK KODDUR.
                // Genellikle ağ/veritabanı bağlantılarını kapatmak (C'deki fclose mantığı) veya belleği iade etmek için kullanılır.
                Console.WriteLine("Finally bloğu çalıştı: Tüm açık ağ / dosya bağlantıları güvenle kapatıldı.\n");
                
                if (reader != null)
                {
                    reader.Close();
                }
            }

            Console.WriteLine("Eğer C'de olsaydık program çoktan ölmüştü.");
            Console.WriteLine("Ancak C#'ta program çökmeden sonuna kadar gelebildi! Başarıyla bitirildi.");
            Console.WriteLine("Kapatmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
