using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Configuration.Entities
{
    [Serializable]
    public class Table
    {
        public string Name { get; set; }
        public int GenerateCount { get; set; }
        public string IdentityField { get; set; }
        public List<Field> Fields { get; set; }
        public List<Dependency> Dependencies { get; set; }
        
        public Table(string name, string identityField)
            : this(name, identityField, 1)
        {
        }

        public Table(string name, string identityField, int generateCount)
        {
            Name = name;
            GenerateCount = generateCount;
            IdentityField = identityField;
        }
    }
}
