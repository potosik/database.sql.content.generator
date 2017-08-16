using Database.SQL.Content.Generator.Configuration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Configuration
{
    [Serializable]
    public class Configuration
    {
        public List<Table> Tables { get; set; }
    }
}
