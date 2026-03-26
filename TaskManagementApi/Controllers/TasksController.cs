using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TaskManagementApi.Data;    // Veritabanı (Mutfak) için
using TaskManagementApi.Models;  // TaskItem (Yemek/Sipariş Kalıbı) için

namespace TaskManagementApi.Controllers
{
    // C# Garson Sınıfımız (Dışarıdan gelen API isteklerini dinler)
    [ApiController] 
    [Route("api/[controller]")] // Adresi şu olacak: http://sitemiz.com/api/Tasks
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context; // Sipariş Defteri (Veritabanı köprüsü)

        // ASP.NET Core bu garson (Controller) çağırıldığında otomatik olarak Sipariş Defterini (_context) eline verir.
        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET (Tüm görevleri Listele)
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            // Mutfağa git, Tasks menüsündeki her şeyi listele
            var tasks = await _context.Tasks.ToListAsync();
            
            return Ok(tasks); // 200 Başarılı HTTP mesajıyla, formatı JSON'a çevirerek müşteriye dön!
        }

        // 2. POST (Yeni Görev Ekle)
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItem newTask)
        {
            newTask.CreatedAt = DateTime.Now; // Tarih ve saati biz sunucu tarafında ekliyoruz
            
            _context.Tasks.Add(newTask);       // Deftere yaz
            await _context.SaveChangesAsync(); // Mutfağa yolla (Kalıcı kaydet)

            // 201 Oluşturuldu kuralı ile başarılı diyerek cevabı müşteriye fırlat.
            return CreatedAtAction(nameof(GetAllTasks), new { id = newTask.Id }, newTask);
        }

        // 3. DELETE (Görevi Sil)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            // Veritabanında (Mutfakta) id'ye sahip görevi bul
            var task = await _context.Tasks.FindAsync(id);
            
            if (task == null) 
                return NotFound(); // Bulamazsan 404 (NotFound) dön

            _context.Tasks.Remove(task);       // Defterden karala
            await _context.SaveChangesAsync(); // Mutfağa bunu çöpe at de

            return NoContent(); // İşlem Başarılı ama içi boş mesaj dön (204)
        }
    }
}
