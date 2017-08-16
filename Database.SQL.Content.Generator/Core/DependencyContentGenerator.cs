using Database.SQL.Content.Generator.Configuration.Entities;
using Database.SQL.Content.Generator.Context;
using Database.SQL.Content.Generator.Exceptions;
using Database.SQL.Content.Generator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator.Core
{
    internal class DependencyContentGenerator
    {
        private readonly Dependency dependency;
        private readonly LogWrapper log;

        public DependencyContentGenerator(Dependency dependency, LogWrapper log)
        {
            this.dependency = dependency;
            this.log = log;
        }

        public async Task<string> StartAsync()
        {
            if (!dependency.Required && Randomizer.Instance.GetBool())
            {
                return null;
            }

            return await GetOrCreateRecord();
        }

        private async Task<string> GetOrCreateRecord()
        {
            if (dependency.CanPeekExisting && Randomizer.Instance.GetIntPercentage() <= dependency.PeekingPercentage)
            {
                return await GetRecord();
            }

            return await CreateRecord();
        }

        private async Task<string> GetRecord()
        {
            await log.WriteLineAsync($"Peeking existin record from table {dependency.DependentTable.Name}");
            var existingIds = await DatabaseContext.Instance.GetExistingIds(dependency.DependentTable.Name, dependency.DependentTable.IdentityField);
            if (existingIds.Count == 0)
            {
                await log.WriteLineAsync($"Nothing to peek");
                return await CreateRecord();
            }

            var peekIndex = Randomizer.Instance.GetInt(existingIds.Count - 1);
            return existingIds[peekIndex].ToTableRecordValueString();
        }

        private async Task<string> CreateRecord()
        {
            await log.WriteLineAsync($"Creating new record for table {dependency.DependentTable.Name}");
            var tableContentGenerator = new TableContentGenerator(dependency.DependentTable, log);
            var generatedId = (await tableContentGenerator.StartAsync()).FirstOrDefault();
            if (generatedId == Guid.Empty)
            {
                var message = $"Requested row wasn't generated for table '{dependency.DependentTable.Name}' (identity: '{dependency.DependentTable.IdentityField}').";
                await log.WriteLineAsync(message);
                throw new NotGeneratedException(message);
            }

            return generatedId.ToTableRecordValueString();
        }
    }
}
