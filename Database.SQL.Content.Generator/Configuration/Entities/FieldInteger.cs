using Database.SQL.Content.Generator.Core;
using Database.SQL.Content.Generator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Configuration.Entities
{
    public class FieldInteger : Field
    {
        private readonly int? min;
        private readonly int? max;

        public FieldInteger(string name, bool nullable = false, int? min = null, int? max = null)
            : base(name, nullable)
        {
            this.min = min;
            this.max = max;
        }
        
        public override string GetNewValue()
        {
            if (nullable && Randomizer.Instance.GetBool())
            {
                return null;
            }

            if (min.HasValue && max.HasValue)
            {
                return Randomizer.Instance.GetInt(min.Value, max.Value).ToString();
            }

            if (!min.HasValue && max.HasValue)
            {
                return Randomizer.Instance.GetInt(max.Value).ToString();
            }

            return Randomizer.Instance.GetInt().ToTableRecordValueString();
        }
    }
}
