using System;

namespace OvaCodeTest.Exceptions
{
    class ServiceException: Exception
    {
        public  string Content { get; }

        public ServiceException()
        {
        }

        public ServiceException(string content) : base(String.Format(content))
        {
            Content = content;
            
        }
    }
}
