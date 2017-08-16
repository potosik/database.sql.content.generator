using Database.SQL.Content.Generator.Core;
using Database.SQL.Content.Generator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Configuration.Entities
{
    public class FieldString : Field
    {
        private const int defaultMinLength = 3;
        private const int defaultMaxLength = 15;

        private const string lowerChars = "abcdefghijklmnopqrstuvqxyz";
        private const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVQXYZ";
        private const string digitsChars = "0123456789";
        private const string spaceChar = " ";

        private readonly int minLength;
        private readonly int maxLength;
        private readonly bool lower;
        private readonly bool upper;
        private readonly bool digits;
        private readonly bool space;

        public FieldString(string name, bool nullable = false,
            int minLength = defaultMinLength, int maxLength = defaultMaxLength,
            bool lower = true, bool upper = true, bool digits = true, bool space = true)
            : base(name, nullable)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
            this.lower = lower;
            this.upper = upper;
            this.digits = digits;
            this.space = space;
        }
        
        public override string GetNewValue()
        {
            if (nullable && Randomizer.Instance.GetBool())
            {
                return null;
            }

            var chars = GetAvailableChars();
            var length = Randomizer.Instance.GetInt(minLength, maxLength);
            var value = new string(Enumerable.Repeat(chars, length).Select(s => s[Randomizer.Instance.GetInt(s.Length)]).ToArray());
            return value.ToTableRecordValueString();
        }

        private string GetAvailableChars()
        {
            return
                (space ? spaceChar : String.Empty) +
                (lower ? lowerChars : String.Empty) +
                (space ? spaceChar : String.Empty) +
                (upper ? upperChars : String.Empty) +
                (space ? spaceChar : String.Empty) +
                (digits ? digitsChars : String.Empty) +
                (space ? spaceChar : String.Empty);
        }
    }
}
