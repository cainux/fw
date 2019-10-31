using System.Threading.Tasks;
using Movies.Core.Entities;

namespace Movies.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(int userId);
    }
}