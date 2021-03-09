using System;

namespace Map.API.Helpers.SharedResponse
{
    public class SharedAuthorizationException : Exception
    {
        public string[] Messages;
        public SharedAuthorizationException(params string[] message) 
        {
            Messages = message;
        }
    }
    public class SharedAuthenticationException : Exception
    {
        public string[] Messages;
        public SharedAuthenticationException(params string[] message)
        {
            Messages = message;
        }
    }
}
