using System;

namespace _03_Inheritance
{
    // C'deki struct'larda "miras alma" (inheritance) yoktur. 
    // Aynı/benzer alanları paylaşan 5 farklı struct yazacaksanız, ortak alanları hepsine tekrar tekrar yazmanız gerekir.
    // C#'ta (ve diğer OOP dillerinde) ise ortak özellikleri bir "Temel Sınıf" (Base Class) içinde toplarız.

    // 1. Temel Sınıf (Base Class / Parent)
    public class Employee
    {
        public string Name { get; set; }
        public double Salary { get; set; }

        public Employee(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }

        // "virtual" (sanal) anahtar kelimesi C#'a özgüdür.
        // Bu metodu miras alan alt sınıfların, isterlerse bu metodu EZİP (override)
        // kendi ihtiyaçlarına göre yeniden kodlayabilmesine izin verdiğimizi belirtir.
        public virtual void Work()
        {
            Console.WriteLine($"{Name} adlı çalışan normal mesai yapıyor.");
        }
        
        public virtual void CalculateBonus()
        {
            Console.WriteLine($"{Name} için prim: {Salary * 0.10} TL (Maaşın %10'u)");
        }
    }

    // 2. Alt Sınıf (Derived Class / Child)
    // " : Employee " yazarak, Employee'nin içindeki tüm public
    // değişkenleri ve metotları kopyala-yapıştır yapmadan bu sınıfa dahil ettik! (Miras)
    public class Manager : Employee
    {
        public int TeamSize { get; set; }

        // Alt sınıfın yapıcı metodu (constructor), "base(...)" anahtar kelimesi ile 
        // üst sınıftaki constructor'a gereken name ve salary parametrelerini paslar.
        public Manager(string name, double salary, int teamSize) : base(name, salary)
        {
            TeamSize = teamSize; // Kendine ait olan özelliği de burada atar.
        }

        // "override" anahtar kelimesi ile üst sınıftaki (virtual işaretlenmiş) metodu EZİYORUZ.
        // Artık bir yönetici için Work metodu çağrıldığında üstteki değil, buradaki kod çalışacak.
        public override void Work()
        {
            Console.WriteLine($"{Name} adlı yönetici, {TeamSize} kişilik ekibinin toplantısını yönetiyor.");
        }

        public override void CalculateBonus()
        {
            Console.WriteLine($"{Name} (Yönetici) için prim: {Salary * 0.20} TL (Maaşın %20'si)");
        }
    }
}
