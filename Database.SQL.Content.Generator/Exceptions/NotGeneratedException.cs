using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Exceptions
{
    internal class NotGeneratedException : Exception
    {
        public NotGeneratedException(string message) 
            : base(message)
        {
        }
    }
}
