using System;

namespace CatFactsCore.Domain.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string command) : base($"The command {command} is not supported.")
        {
        }
    }
}