using System;

namespace _02_Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- ADIM 2: SINIFLAR VE NESNELER (Classes & Objects) ---\n");

            // 1. Nesne (Object) Oluşturma (Instantiating)
            // BankAccount sınıfından (örnek kalıbından) yeni bir nesne (object) ürettik.
            // C#'ta "new" anahtar kelimesi, C'deki "malloc()" gibi hafızada (Heap bölgesinde) yer ayırır.
            // Fakat free() ile bu belleği manuel silmemize gerek yoktur, arkamızı Çöp Toplayıcı (Garbage Collector) temizler!
            BankAccount account1 = new BankAccount("Ahmet", 1000.0);

            // 2. Property Okuma (get)
            Console.WriteLine($"Hesap Sahibi: {account1.OwnerName}"); // get sayesinde veriyi doğrudan okuyabiliyoruz
            Console.WriteLine($"Başlangıç Bakiyesi: {account1.Balance} TL\n"); // get çalıştı

            // 3. Property Yazma (set)
            // account1.Balance = 5000; // HATA VERİR! Çünkü Balance'ın set'i "private". Dışarıdan değiştirilemez. (Buna Kapsülleme - Encapsulation denir)
            
            account1.OwnerName = "Ahmet Yılmaz"; // OwnerName'in set'i public olduğu için değiştirebildik.
            Console.WriteLine($"Hesap sahibinin adı güncellendi: {account1.OwnerName}\n");

            // 4. Metot Çağrısı (Method Call)
            // C'deki struct'lar aksine, veriyi işleyen fonksiyonlar sınıfın (verinin) kendi içinde yaşar.
            account1.Deposit(500);
            account1.Withdraw(200);
            account1.Withdraw(2000); // İf-else koruması devreye girecek ve yetersiz bakiye uyarısı verecek.

            Console.WriteLine("\n--- Referans Tipi (Reference Type) Mantığı ---");
            // Sınıflar referans tiplerdir. Ram'de bir "adres" tutarlar (pointer mantığı).
            
            BankAccount account2 = account1; // account2, account1'in bellekte gösterdiği aynı adrese (aynı banka hesabına) kilitlendi!
            
            // Eğer account2 üzerinden yatırma işlemi yaparsak, account1 de etkilenir çünkü ikisi de AYNI nesnedir!
            account2.Deposit(100); 
            // *Bu işlem C'deki "BankAccount* account2 = account1;" kod satırı ile tamamen aynı felsefeye sahiptir.*

            Console.WriteLine($"\nAhmet'in son hesabı (account1 referansı üzerinden okuyoruz): {account1.Balance} TL");
            
            Console.WriteLine("\nKapatmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
