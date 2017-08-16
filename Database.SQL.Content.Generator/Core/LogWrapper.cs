using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Core
{
    public class LogWrapper
    {
        private readonly TextWriter @out;

        public LogWrapper(TextWriter @out)
        {
            this.@out = @out;
        }

        public async Task WriteLineAsync()
        {
            if (@out != null)
            {
                await @out.WriteLineAsync();
            }
        }

        public async Task WriteLineAsync(string message)
        {
            if (@out != null)
            {
                await @out.WriteLineAsync(message);
            }
        }
    }
}
