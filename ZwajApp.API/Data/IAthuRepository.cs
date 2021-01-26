using System.Threading.Tasks;
using ZwajApp.API.Models;

namespace ZwajApp.API.Data
{
    public interface IAthuRepository
    {
        Task<User> Register(User user , string password);
        Task<User> Login(string  userName , string password);
        Task<bool> UserExists(string  userName);
       
    }
}