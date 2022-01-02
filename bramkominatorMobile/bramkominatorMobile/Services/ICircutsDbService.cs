using System.Threading.Tasks;
using System.Collections.Generic;

namespace bramkominatorMobile.Services
{
    public interface ICircutsDbService : IEnumerable<LogicCircut>
    {
        Task Init();
        Task AddCircut(LogicCircut circut);
        Task RemoveCircut(LogicCircut circut);
        Task UpdateCircut(LogicCircut circut);
        Task<LogicCircut> GetCircut(int id);
        Task<IEnumerable<LogicCircut>> GetAllCircuts();
    }
}
