using System;
namespace bramkominatorMobile.Exceptions
{
    public class InvalidGatewayException : Exception
    {
        public InvalidGatewayException(string msg) : base(msg)
        {
        }
    }
}
