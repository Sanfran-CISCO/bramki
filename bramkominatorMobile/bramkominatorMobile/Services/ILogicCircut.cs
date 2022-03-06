using bramkominatorMobile.Models;
using System.Collections.Generic;

namespace bramkominatorMobile.Services
{
    public interface ILogicCircut : IEnumerable<CircutElement>
    {
        void Connect(CircutElement from, CircutElement to, int inputNumber);
        bool Disconnect(CircutElement gate, string direction);
        bool Remove(CircutElement gate);
        bool IsConnected(CircutElement gate);
    }
}
