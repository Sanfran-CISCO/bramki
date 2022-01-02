using bramkominatorMobile.Models;
using System.Collections.Generic;

namespace bramkominatorMobile.Services
{
    public interface ILogicCircut : IEnumerable<Node>
    {
        void Connect(LogicGateway from, LogicGateway to, int inputNumber);
        bool Disconnect(LogicGateway gate, string direction);
        bool Remove(LogicGateway gate);
        bool IsConnected(LogicGateway gate);
    }
}
