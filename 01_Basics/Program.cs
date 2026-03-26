using System; // C'deki #include <stdio.h> mantığı gibidir ancak kütüphane değil, isim uzayıdır (namespace).

// Geleneksel C programcılarının daha rahat anlaması için "Main" (ana fonksiyon) yapısını kullanıyoruz.
namespace _01_Basics
{
    class Program
    {
        // C'deki "int main()" fonksiyonuna çok benzer.
        // Ancak C#'ta nesne yönelimli olduğu için metotlar (fonksiyonlar) bir sınıf (class) içinde olmalıdır.
        static void Main(string[] args)
        {
            // 1. Ekrana Yazdırma (printf yerine Console.WriteLine)
            Console.WriteLine("C#'a Hoş Geldiniz!"); // satır sonuna \n otomatik eklenir.

            // 2. Değişken Tanımlama (C ile tamamen aynı sentaks)
            int age = 25;
            double pi = 3.14159;
            char grade = 'A';
            bool isStudent = true; // C'de stdbool.h ile gelen bool tipi, C#'ta yerleşiktir (true veya false döner).

            // 3. String (Metin) Veri Tipi
            // C'de char* veya char dizisi (char str[]) ile uğraştığımız metinler için C#'ta "string" kullanırız.
            // Bellek yönetimi, sonlandırıcı NULL karakteri (\0) gibi dertlerimiz yoktur.
            string name = "Ahmet";

            // 4. Ekrana Formatlı Yazdırma (String Interpolation - $ işareti)
            // C'deki printf("%s, %d yaşında", name, age) mantığının modern ve hataya kapalı hali:
            Console.WriteLine($"Benim adım {name}, {age} yaşındayım ve notum {grade}.");

            // 5. Konsoldan Veri Okuma (scanf yerine Console.ReadLine)
            Console.Write("Lütfen favori sayınızı girin: "); 
            
            // C#'ta konsoldan gelen her şey "string" (metin) olarak değerlendirilir.
            string input = Console.ReadLine();

            // Gelen metni tam sayıya (int) çevirmeliyiz (C'deki atoi() fonksiyonuna benzer):
            int favoriteNumber = Convert.ToInt32(input);
            Console.WriteLine($"Sayınızın iki katı: {favoriteNumber * 2}");

            // --- DEĞER VE REFERANS TİPLERİ (Value vs Reference Types) ---
            // C'deki o meşhur pointerlar (*) C#'ta günlük kullanımda yoktur.
            
            // a) Value Types (Değer Tipleri - Stack Bellek): Bellekte yer tutar, doğrudan kopyası çıkarılır.
            int x = 10;
            int y = x; // y, x'in değerini kopyaladı.
            y = 20;
            Console.WriteLine($"x: {x}, y: {y}"); // x hala 10. y'nin değişmesi x'i etkilemez.
            
            // b) Reference Types (Referans Tipleri - Heap Bellek): class, string veya diziler. 
            // Adres tutarlar (pointer mantığı), değiştirdiğinizde nesnenin kendisi değişir.
            // Bellekte kalan verileri temizleme işiniyse biz yapmayız, "Garbage Collector" (Çöp Toplayıcı) halleder! C'den en büyük farklardan biri budur.

            Console.WriteLine("\nUygulamayı kapatmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
