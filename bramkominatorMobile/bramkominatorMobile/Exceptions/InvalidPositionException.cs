using System;
namespace bramkominatorMobile.Exceptions
{
    public class InvalidPositionException : Exception
    {
        public InvalidPositionException(string msg) : base(msg)
        {
        }
    }
}
