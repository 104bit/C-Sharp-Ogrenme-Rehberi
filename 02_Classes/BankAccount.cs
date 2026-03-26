using System;

namespace _02_Classes
{
    // C'deki struct'lara (yapılara) benzer ama C#'ta class'lar referans tiplidir (heap bellekte tutulur) 
    // ve içlerinde sadece veri değil, bu veriyi işleyecek fonksiyonları da (metotları) barındırırlar!
    public class BankAccount
    {
        // 1. Fields (Alanlar)
        // Sınıfın içindeki değişkenlere field denir. Genellikle "private" (gizli) yapılırlar.
        // Private demek; bu değişkene, sadece bu sınıfın kendi içinden erişilebilir demektir.
        private double _balance; 

        // 2. Properties (Özellikler) - C#'in en güçlü yanlarından biri ve mülakatların vazgeçilmezidir!
        // C ve Java gibi dillerde değişkeni korumak için GetBalance() ve SetBalance() metotları yazardık.
        // C#'ta Property kullanarak değişkene erişimi (okuma/yazma) çok şık bir şekilde kontrol ederiz.
        
        // Bu "Auto-implemented property" (Otomatik uygulanmış özellik). 
        // Arka planda gizli bir string field oluşturur ve okuyup yazmamızı sağlar.
        public string OwnerName { get; set; } 

        // Bu ise genişletilmiş property.
        // Bakiye (Balance) dışarıdan okunabilsin (get) ama dışarıdan doğrudan DEĞİŞTİRİLEMESİN (private set)
        public double Balance 
        { 
            get { return _balance; }  // Dışarıdan account.Balance dendiğinde _balance'ı ver
            private set { _balance = value; } // Sadece bu sınıfın içindeki metotlar bakiyeyi değiştirebilsin!
        }

        // 3. Constructor (Yapıcı Metot)
        // Nesne (Object) oluşturulduğu (new dendiği) anda otomatik çalışan ve değişkenleri hazırlayan fonksiyondur.
        // Geri dönüş tipi yoktur ve sınıfın adıyla aynı adı taşır.
        // C'de bir struct oluşturup sonra onu init_struct() fonskiyonuna yollardık, burada otomatiktir.
        public BankAccount(string ownerName, double initialBalance)
        {
            OwnerName = ownerName;
            Balance = initialBalance; // Sınıfın içinde olduğumuz için private set'i kullanabiliyoruz.
        }

        // 4. Methods (Metotlar - Sınıfın Fonksiyonları)
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Yatırılacak miktar 0'dan büyük olmalıdır.");
                return;
            }
            Balance += amount; // private _balance'ı güvenli bir şekilde güncelliyoruz
            Console.WriteLine($"{amount} TL yatırıldı. Yeni bakiye: {Balance} TL");
        }

        public void Withdraw(double amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Yetersiz bakiye! İşlem reddedildi.");
                return;
            }
            Balance -= amount;
            Console.WriteLine($"{amount} TL çekildi. Yeni bakiye: {Balance} TL");
        }
    }
}
