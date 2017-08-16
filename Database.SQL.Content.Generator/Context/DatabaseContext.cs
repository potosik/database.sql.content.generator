using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.SQL.Content.Generator.Configuration.Entities;
using Database.SQL.Content.Generator.Core;
using System.Configuration;
using Database.SQL.Content.Generator.Extensions;

namespace Database.SQL.Content.Generator.Context
{
    internal class DatabaseContext : DbContext
    {
        private const string InsertQueryTemplate = "INSERT INTO [dbo].[{0}] ({1}) VALUES({2});";
        private const string SelectExistingIdsQueryTemplate = "SELECT {0} FROM [dbo].[{1}]";
        private const string JoinSeparator = ",";

        private static string connectionString = ConfigurationManager.AppSettings[Constants.AppSettings.ConnectionStringKey];
        private static DatabaseContext _context;

        public static DatabaseContext Instance
        {
            get
            {
                if (_context == null)
                {
                    _context = new DatabaseContext();
                }

                return _context;
            }
        }

        private int _saveCount = 0;

        private DatabaseContext() 
            : base(connectionString)
        {
        }

        public override async Task<int> SaveChangesAsync()
        {
            var result = await base.SaveChangesAsync().ConfigureAwait(false);

            _saveCount++;
            if (_saveCount >= Constants.MaxSaveOperationForContext)
            {
                _saveCount = 0;
                _context.Dispose();
                _context = null;
            }

            return result;
        }

        public async Task<Guid> CreateRecordAsync(string tableName, string identityField, RecordMap recordMap)
        {
            var id = PopulateIdentification(identityField, recordMap);
            var query = CreateInsertQuery(tableName, recordMap);
            await Database.ExecuteSqlCommandAsync(query);

            return id;
        }

        private Guid PopulateIdentification(string identityField, RecordMap recordMap)
        {
            var id = Guid.NewGuid();
            recordMap.Add(identityField, id.ToTableRecordValueString());
            return id;
        }

        private string CreateInsertQuery(string tableName, RecordMap recordMap)
        {
            var keys = String.Join(JoinSeparator, recordMap.Keys);
            var values = String.Join(JoinSeparator, recordMap.Values);
            return String.Format(InsertQueryTemplate, tableName, keys, values);
        }

        public async Task<IList<Guid>> GetExistingIds(string tableName, string identityField)
        {
            var query = CreateSelectIdsQuery(tableName, identityField);
            return await Database.SqlQuery<Guid>(query).ToListAsync();
        }

        private string CreateSelectIdsQuery(string tableName, string identityField)
        {
            return String.Format(SelectExistingIdsQueryTemplate, identityField, tableName);
        }
    }
}
