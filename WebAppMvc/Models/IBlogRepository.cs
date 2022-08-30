using WebAppMvc.Models.Db;

namespace WebAppMvc.Models
{
    public interface IBlogRepository
    {
        Task AddUser(User user);


        Task<User[]> GetUsers();
    }
}
