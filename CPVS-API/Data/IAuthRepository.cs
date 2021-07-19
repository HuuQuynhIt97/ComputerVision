using System.Threading.Tasks;
using CPVS_API.Models;

namespace CPVS_API.Data
{
    public interface IAuthRepository
    {
        Task<User> Login(string username, string password);
    }
}