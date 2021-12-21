using System;
using System.Collections;
using System.Collections.Generic;
using bramkominatorMobile.Models;

namespace bramkominatorMobile.Services
{
    public class GatewaysList : IEnumerable<LogicGateway>
    {
        public GatewaysList()
        {
        }

        public IEnumerator<LogicGateway> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

