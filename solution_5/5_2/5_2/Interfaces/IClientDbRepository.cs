using System.Threading.Tasks;

namespace _5_2.Interfaces
{
    public interface IClientDbRepository
    {
        public Task DeleteClient(int idClient);
    }
}
