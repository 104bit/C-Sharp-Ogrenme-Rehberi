using System;

namespace _03_Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- ADIM 3: KALITIM (Inheritance) VE POLİMORFİZM (Çok Biçimlilik) ---\n");

            // 1. Standart Çalışan Nesnesi
            Employee emp = new Employee("Ahmet", 20000);
            Console.WriteLine("--- Normal Çalışan ---");
            emp.Work();                // Employee sınıfındaki Work çalışır.
            emp.CalculateBonus();      // %10 prim hesaplar.
            Console.WriteLine();

            // 2. Yönetici Nesnesi (Manager sınıfı Employee'den miras aldı)
            Manager mgr = new Manager("Selin", 40000, 5);
            Console.WriteLine("--- Yönetici ---");
            
            // Name ve Salary özelliklerini Manager içinde BİR KEZ BİLE yazmadığımız halde erişebiliyoruz! (Inheritance)
            Console.WriteLine($"Yöneticinin Adı: {mgr.Name}, Ekip Sayısı: {mgr.TeamSize}");
            
            // Override ettiğimiz için, aynı isimli metotlar farklı karakterde çalışır. (Bu sayede kodlar sadeleşir)
            mgr.Work();                // Manager sınıfındaki Work çalışır.
            mgr.CalculateBonus();      // %20 prim hesaplar.
            Console.WriteLine();

            // 3. Gerçek Polimorfizm Gücü!
            // C'deki fonksiyon pointer'ları (function pointers) ile ancak zar zor yapılan işlemi C# çok kolaylaştırır:
            // "Her Yönetici temelde bir Çalışandır". Bu kural sayesinde bir Manager nesnesini 'Employee' tipinde tutabiliriz!
            
            Employee baseRef = new Manager("Can", 35000, 10);
            
            Console.WriteLine("--- Polimorfizm Devrede ---");
            Console.WriteLine("Değişken referans tipi 'Employee' ama nesnenin özü (Heap'teki karşılığı) bir 'Manager'.");
            
            // DİKKAT: C veya daha eski dillerde değişkenin tipi Employee olduğu için düz Employee metodu çalışırdı.
            // Ancak C#'ta metotlar override (virtual) ise, derleyici değişkenin iskeletine değil "özüne" bakar.
            // Özü Manager olduğu için yöneticinin primini verecektir! (C++'daki vtable / virtual method dispatch)
            baseRef.CalculateBonus(); 

            Console.WriteLine("\nKapatmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
