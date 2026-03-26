using System;
using System.Collections.Generic;
using System.Linq; // LINQ için EN HAYATİ isim uzayıdır. Bunu eklemezseniz Where, Select gibi metotlar gelmez.

namespace _07_LINQ
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- ADIM 7: LINQ (Language Integrated Query) ---\n");

            // LINQ, diziler veya koleksiyonlar (List, Dictionary) üzerinde 
            // sanki bir Veritabanına SQL sorgusu yazar gibi çok basitçe işlem yapmamızı sağlar!
            // C'deki onlarca satırlık for döngüleri, if kontrolleri ve içiçe geçici matrisleri tek satıra düşürür.

            // Örnek bir veri kümesi (Veritabanı Tablosu gibi düşünün) hazırlayalım:
            List<Person> people = new List<Person>
            {
                new Person { Name = "Ahmet", Age = 25, City = "Istanbul" },
                new Person { Name = "Selin", Age = 30, City = "Ankara" },
                new Person { Name = "Can", Age = 22, City = "Istanbul" },
                new Person { Name = "Ayşe", Age = 35, City = "Izmir" },
                new Person { Name = "Mehmet", Age = 28, City = "Ankara" }
            };

            // --- Senaryo 1: Geleneksel C Mantığı (Amelelik) ---
            // Soru: "Istanbul'da yaşayanları bul ve sadece isimlerini ekrana yazdır"
            Console.WriteLine("1. Geleneksel Yol:");
            List<Person> istanbulPeople = new List<Person>();
            foreach (var person in people)
            {
                if (person.City == "Istanbul")
                {
                    istanbulPeople.Add(person);
                }
            }
            foreach (var p in istanbulPeople)
            {
                Console.WriteLine($"- {p.Name}");
            }


            // --- Senaryo 2: LINQ (Lambda İşaretleriyle) ---
            // Aynı işlemi LINQ ile TAŞ çatlasa 1 satırda yaparız!
            Console.WriteLine("\n2. LINQ Method Syntax Yolu (Modern ve Temiz):");
            
            // x => x.City == "Istanbul" bir önceki konudaki (Lambda) isimsiz fonksiyondur.
            // Where : Filtreleme yapar (SQL'deki WHERE)
            // Select: Sadece istediğimiz kolonu (özelliği) seçmeye yarar (SQL'deki SELECT)
            // ToList: Çıkan sonucu yeni bir Liste haline getirir.
            var secilenler = people.Where(p => p.City == "Istanbul")
                                   .Select(p => p.Name)
                                   .ToList();

            foreach (var name in secilenler)
            {
                Console.WriteLine($"- {name}");
            }


            // --- Senaryo 3: Sıralama (OrderBy) ve İlk Elemanı Alma (First) ---
            Console.WriteLine("\n3. LINQ ile Sıralama ve Tek Kayıt Çekme:");
            
            // Soru: Yaşa göre küçükten büyüğe sırala ve en baştakini (en genci) al!
            // C'de bunu yapmak için tüm diziyi Bubble Sort / Quick Sort ile manuel sıralamanız gerekirdi!
            var youngest = people.OrderBy(p => p.Age).First();
            Console.WriteLine($"En Genç Kişi: {youngest.Name} ({youngest.Age} yaşında)");


            // --- Senaryo 4: SQL Tarzı Query Syntax ---
            Console.WriteLine("\n4. LINQ Query Syntax (Tamamen SQL Benzeri):");
            // C# içinde direkt SQL veritabanı sorgusu yazar gibi (from, where, select) kod yazabiliriz!
            
            var ankaralilar = from p in people
                              where p.City == "Ankara" && p.Age >= 25
                              orderby p.Age descending // Yaşa göre büyükten küçüğe (Azalan)
                              select p;

            foreach (var p in ankaralilar)
            {
                Console.WriteLine($"- {p.Name} ({p.Age} yaşında)");
            }

            Console.WriteLine("\nLINQ, C#'ın açık ara en sevilen ve mülakatlarda kesinlikle sorulan özelliğidir!");
            Console.WriteLine("\nKapatmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
