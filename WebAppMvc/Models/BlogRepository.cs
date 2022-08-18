using Microsoft.EntityFrameworkCore;
using WebAppMvc.Models.Db;

namespace WebAppMvc.Models
{
    public class BlogRepository:IBlogRepository
    {
        private BlogContext _context;

        // Метод-конструктор для инициализации
        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        public async Task<User[]> GetUsers()
        {
           return await _context.Users.ToArrayAsync();
        }
    }
}
