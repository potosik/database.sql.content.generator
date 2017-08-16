using System;

namespace Database.SQL.Content.Generator.Configuration.Entities
{
    public abstract class Field
    {
        protected readonly bool nullable;

        public string Name { get; private set; }
        
        public Field(string name, bool nullable)
        {
            Name = name;

            this.nullable = nullable;
        }

        public abstract string GetNewValue();
    }
}