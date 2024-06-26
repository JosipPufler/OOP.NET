using DataLayer.Models;

namespace DataLayer.Repository
{
    public interface IRepo
    {
        Task<IEnumerable<T>> Get<T>(Categories category);
        Task<IEnumerable<Match>> GetMatchByFifaCode(Categories category, string fifaCode);
    }
}
