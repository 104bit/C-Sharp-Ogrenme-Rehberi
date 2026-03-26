using System;
using System.Collections.Generic; // List ve Dictionary için bu kütüphaneyi eklememiz gerekir.

namespace _04_Collections
{
    // C'deki fonksiyonları her veri tipi için (int topla, double topla vs.) ayrı ayrı yazmak yerine,
    // C#'ta "Generics" (Jenerikler) yani <T> kullanarak "tip bağımsız ama tip güvenli" yapılar kurarız.
    // C'deki karanlık ve tehlikeli void* (void pointer) mantığının çok daha güvenli ve modern halidir!
    class Box<T>
    {
        private T _content;

        public void Pack(T item)
        {
            _content = item;
            Console.WriteLine($"Kutuya eklendi: {item} (Tipi: {typeof(T).Name})");
        }

        public T Unpack()
        {
            return _content;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- ADIM 4: KOLEKSİYONLAR VE JENERİKLER (Collections & Generics) ---\n");

            // 1. Listeler (List<T>)
            // C'deki diziler (arrays) oluşturulurken bellekte boyutu sabitlenmek zorundadır. (Örn: int arr[10];)
            // C'de dinamik boyut için malloc() ve realloc() ile amelelik yapmak gerekir.
            // C#'ta List<T> arkaplanda boyutu bizim için otomatik ayarlanan dinamik bir dizidir!
            Console.WriteLine("--- 1. List<string> Kullanımı ---");
            
            List<string> cities = new List<string>(); // string tutacak boş bir liste yarattık
            cities.Add("İstanbul"); // realloc derdi yok, kendi büyüyor!
            cities.Add("Ankara");
            cities.Add("İzmir");
            
            // C'deki for ve while döngüleri C#'ta da tıpatıp aynı çalışır ancak 
            // "foreach" döngüsü listeler/koleksiyonlar içerisinde gezinmek için harikadır (okunabilirliği artırır):
            foreach (string city in cities)
            {
                Console.WriteLine($"- {city}");
            }
            
            Console.WriteLine($"\nListe şu an {cities.Count} elemana sahip.\n");


            // 2. Sözlükler (Dictionary<TKey, TValue>)
            // C'de string bir kelimeye karşılık int bir değer tutmak için (Hash Map / Hash Table) yüzlerce satır kod yazmanız gerekirdi.
            // C#'ta bu işlem tek satırdır! Dictionary, Anahtar (Key) ve Değer (Value) ikilileriyle çalışır.
            Console.WriteLine("--- 2. Dictionary<int, string> Kullanımı ---");
            
            Dictionary<int, string> employees = new Dictionary<int, string>();
            // Key: int (id), Value: string (isim)
            employees.Add(101, "Selin");
            employees.Add(102, "Ahmet");
            employees.Add(103, "Ayşe");
            
            // Sözlüklerde belirli bir "Key" üzerinden veri okumak ışık hızındadır (O(1) karmaşıklığı).
            Console.WriteLine($"102 Sicil Numaralı çalışan: {employees[102]}");

            if (employees.ContainsKey(105)) // Böyle bir sicil numarası var mı kontrolü
            {
                Console.WriteLine(employees[105]);
            }
            else
            {
                Console.WriteLine("105 sicil numaralı çalışan bulunamadı.\n");
            }


            // 3. Jenerik (Generic) Sınıf Kullanımı <T>
            Console.WriteLine("--- 3. Jenerik Sınıf (Box<T>) Kullanımı ---");
            
            // En yukarıda yazdığımız "Box" (Kutu) sınıfını önce int (tam sayı) için kullanıyoruz:
            Box<int> intBox = new Box<int>();
            intBox.Pack(1453);

            // Sonra AYNI sınıfı string (metin) için kullanıyoruz. 
            // C'de olsaydı string_Box ve int_Box diye iki farklı struct yazmak zorundaydık!
            Box<string> stringBox = new Box<string>();
            stringBox.Pack("Çok Önemli Evrak");

            Console.WriteLine("\nKapatmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
