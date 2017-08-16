using System;

namespace Database.SQL.Content.Generator.Configuration.Entities
{
    [Serializable]
    public class Dependency
    {
        public Table DependentTable { get; set; }
        public bool Required { get; set; }
        public bool CanPeekExisting { get; set; }
        public int PeekingPercentage { get; set; }
        public string TargetIdentityFieldName { get; set; }
    }
}