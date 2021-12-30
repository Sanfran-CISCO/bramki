using System;
using System.Threading.Tasks;
using SQLite;
using bramkominatorMobile.Models;
using System.Collections.Generic;

namespace bramkominatorMobile.Services
{
    public interface IGatewaysDbService
    {
        Task Init();
        Task AddGateway(LogicGateway gate);
        Task RemoveGateway(LogicGateway gate);
        Task<IEnumerable<LogicGateway>> GetAllGates();
        Task<IEnumerable<LogicGateway>> GetBasicGates();
        Task<IEnumerable<LogicGateway>> GetCustomGates();
    }
}
