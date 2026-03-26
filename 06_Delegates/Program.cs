using System;
using System.Collections.Generic;

namespace _06_Delegates
{
    class Program
    {
        // 1. Delege Tanımlama (C'deki typedef yapısıyla Function Pointer tanımlamak gibidir)
        // Bu delege: Geriye "void" dönen ve içine "string" alan herhangi bir fonksiyonun kalıbıdır.
        public delegate void PrintMessageDelegate(string message);

        static void HelloWorld(string msg)
        {
            Console.WriteLine($"HelloWorld Fonksiyonu Çalıştı: {msg}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("--- ADIM 6: DELEGELER VE LAMBDA İFADELERİ (Function Pointers'ın Modern Hali) ---\n");

            // --- 1. Geleneksel Delege Kullanımı ---
            Console.WriteLine("1. Geleneksel Delegate Kullanımı");
            // C'deki "void (*ptr)(char*) = HelloWorld;" mantığının nesne yönelimli aynısıdır.
            // Fonksiyonları parantez açmadan referans olarak değişkene atıyoruz.
            PrintMessageDelegate myPrinter = HelloWorld;
            myPrinter("1. yöntem üzerinden çalışıyorum!"); 

            // --- 2. Action ve Func (C#'ın Hazır Yapıları) ---
            // C#'ta her seferinde yukarıdaki gibi "public delegate..." diye delege TİPİ tanımlamaya gerek kalmasın diye,
            // Geri değer DÖNMEYEN fonksiyonlar için hazır "Action" delege tipi,
            // Geri değer DÖNEN (örneğin int veren) fonksiyonlar için hazır "Func" delege tipi üretilmiştir.
            Console.WriteLine("\n2. Action ve Func Kullanımı");
            
            Action<string> modernPrinter = HelloWorld; // Action, arkada hazır bekleyen bir void delegedir.
            modernPrinter("Hazır Action delegesi çok daha pratik!");

            // --- 3. Anonim Metotlar ve Lambda İfadeleri ---
            // C'de bir fonksiyonu pointer olarak atamak için, o fonksiyonun yukarıda adıyla şanıyla var olması gerekirdi.
            // C#'ta ise (ve modern dillerde), fonksiyonu kullanacağımız yerde "tek satırda isimsiz olarak" yaratabiliriz!
            // Bunu sağlayan yapı "=>" (Gider) (Lambda) operatörüdür.
            Console.WriteLine("\n3. Lambda İfadeleri (Ok Fonksiyonları - =>)");

            // Yukarıda bir "KaresiniAl" fonksiyonu yazmaya hiç gerek kalmadı.
            // "(sayi) => sayi * sayi" demek: "sayi adında parametre al, karesini çarpıp return et" anlamına gelir.
            Func<int, int> squareCalculator = (sayi) => sayi * sayi;
            
            Console.WriteLine($"5'in karesi Lambda ile anında hesaplandı: {squareCalculator(5)}");

            // --- 4. Gerçek Dünyada Lambda Kullanım Senaryosu ---
            Console.WriteLine("\n4. Gerçek Dünya Senaryosu: Listeyi Filtreleme");
            List<int> sayilar = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Örneğin listedeki Çift Sayıları bulmak istiyoruz. 
            // C'de olsaydı; for döngüsü yazıp, içine if(sayi % 2 == 0) kontrolü koyup, yeni bir diziye atmamız gerekirdi.
            // C#'ta Listenin "FindAll" metodu, içine bir filtreleme fonksiyonu bekler. (Tıpkı C'nin qsort fonksiyonu gibi)
            // Biz o fonksiyonu, başka bir yerde tanımlamadan anında içeride yazıyoruz:
            
            List<int> ciftSayilar = sayilar.FindAll(x => x % 2 == 0); // Oku: x parametresini al ve (x % 2 == 0) şartını sağlıyorsa True dön.
            
            Console.WriteLine("Çift Sayılar (Tek satır Lambda koduyla bulundu): " + string.Join(", ", ciftSayilar));

            Console.WriteLine("\nSonraki adım olan LINQ'in temel yapıtaşları bu Lambda'lardır!");
            Console.WriteLine("\nKapatmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
