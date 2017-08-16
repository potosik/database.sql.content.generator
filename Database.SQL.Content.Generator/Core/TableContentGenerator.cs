using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.SQL.Content.Generator.Configuration.Entities;
using Database.SQL.Content.Generator.Context;

namespace Database.SQL.Content.Generator.Core
{
    internal class TableContentGenerator
    {
        private readonly Table table;
        private readonly LogWrapper log;

        public TableContentGenerator(Table table, LogWrapper log)
        {
            this.table = table;
            this.log = log;
        }

        public async Task<IList<Guid>> StartAsync()
        {
            await log.WriteLineAsync($"Start processing table '{table.Name}'");
            await log.WriteLineAsync($"{table.Name}: generating {table.GenerateCount} entities:");

            var generatedIds = new List<Guid>();
            for (int i = 0; i < table.GenerateCount; i++)
            {
                generatedIds.Add(await ProcessTableRecord());
            }

            return generatedIds;
        }

        private async Task<Guid> ProcessTableRecord()
        {
            var recordMap = new RecordMap();

            if (table.Dependencies.Count > 0)
            {
                foreach (var dependency in table.Dependencies)
                {
                    var dependencyValue = await new DependencyContentGenerator(dependency, log).StartAsync();
                    if (dependencyValue != null)
                    {
                        recordMap.Add(dependency.TargetIdentityFieldName, dependencyValue);
                    }
                }
            }

            return await GenerateRecordAsync(recordMap);
        }

        private async Task<Guid> GenerateRecordAsync(RecordMap recordMap)
        {
            foreach (var field in table.Fields)
            {
                var value = field.GetNewValue();
                if (value != null)
                {
                    recordMap.Add(field.Name, value);
                }

                await log.WriteLineAsync($"{table.Name}: field '{field.Name}' generated value = {value}");
            }

            await log.WriteLineAsync();
            return await DatabaseContext.Instance.CreateRecordAsync(table.Name, table.IdentityField, recordMap);
        }
    }
}
