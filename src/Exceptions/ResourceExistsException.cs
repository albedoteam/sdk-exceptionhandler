using System;

namespace AlbedoTeam.Sdk.ExceptionHandler.Exceptions
{
    public class ResourceExistsException : Exception
    {
        public ResourceExistsException()
        {
        }

        public ResourceExistsException(string message)
            : base(message)
        {
        }

        public ResourceExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}