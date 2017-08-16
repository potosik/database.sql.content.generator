using Database.SQL.Content.Generator.Core;
using Database.SQL.Content.Generator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Configuration.Entities
{
    public class FieldDateTime : Field
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        private const int defaultBefore = 31;
        private const int defaultAfter = 31;

        private readonly int before;
        private readonly int after;

        public FieldDateTime(string name, bool nullable = false, int before = defaultBefore, int after = defaultAfter)
            : base(name, nullable)
        {
            this.before = before;
            this.after = after;
        }
        
        public override string GetNewValue()
        {
            if (nullable && Randomizer.Instance.GetBool())
            {
                return null;
            }

            var daysToAdd = Randomizer.Instance.GetInt(before + after);
            var value = DateTime.Now.AddDays(-before).AddDays(daysToAdd);
            return value.ToTableRecordValueString();
        }
    }
}
