using System;
namespace bramkominatorMobile.Exceptions
{
    public class BadElementInputException : Exception
    {
        public BadElementInputException(string msg) : base(msg)
        {
        }
    }
}
