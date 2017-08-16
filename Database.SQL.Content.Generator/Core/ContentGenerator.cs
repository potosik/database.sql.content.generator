using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Core
{
    internal class ContentGenerator
    {
        private readonly Configuration.Configuration configuration;
        private readonly LogWrapper log;
        
        public ContentGenerator(Configuration.Configuration configuration, TextWriter @out)
        {
            this.configuration = configuration;
            this.log = new LogWrapper(@out);
        }

        public async Task StartAsync()
        {
            foreach (var table in configuration.Tables)
            {
                var generator = new TableContentGenerator(table, log);
                await generator.StartAsync();
            }
        }
    }
}
