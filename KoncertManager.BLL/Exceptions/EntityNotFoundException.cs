using System;
using System.Collections.Generic;
using System.Text;

namespace KoncertManager.BLL.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string Message { get; set; }

        public EntityNotFoundException(string message) : base(message)
        {
            Message = message;
        }

        public EntityNotFoundException()
        {
        }
    }
}
