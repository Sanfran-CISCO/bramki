using System;
using System.Collections;

namespace bramkominatorMobile.Models
{
    public class GatewaysEnum : IEnumerator
    {
        public LogicGateway[] gateways;

        int position = -1;

        public GatewaysEnum(LogicGateway[] list)
        {
            gateways = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < gateways.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public LogicGateway Current
        {
            get
            {
                try
                {
                    return gateways[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
