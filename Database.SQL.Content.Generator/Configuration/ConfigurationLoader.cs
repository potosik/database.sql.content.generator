using Database.SQL.Content.Generator.Configuration.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Database.SQL.Content.Generator.Configuration
{
    internal static class ConfigurationLoader
    {
        public static Configuration Load()
        {
            return new Configuration()
            {
                Tables = new List<Table>()
                {
                    new Table("Products", "ProductId", 3000)
                    {
                        Fields = new List<Field>()
                        {
                            new FieldString("Title", false, 10, 100),
                            new FieldInteger("InventoryPart", false, 1, 99999),
                            new FieldDateTime("Created", false, 100, 0),
                            new FieldDateTime("Modified", false, 100, 0),
                            new FieldString("CreatedBy", false, 10, 30),
                            new FieldString("ModifiedBy", false, 10, 30),
                            new FieldString("Description", false, 10, 30),
                            new FieldInteger("PurchaseYear", true, 2000, 2016),
                            new FieldInteger("PurchaseMonth", true, 1, 12),
                        },
                        Dependencies = new List<Dependency>()
                        {
                            new Dependency()
                            {
                                DependentTable = new Table("Categories", "CategoryId")
                                {
                                    Fields = new List<Field>()
                                    {
                                        new FieldString("Title", false, 10, 100),
                                        new FieldInteger("InventoryPart", false, 1, 99),
                                        new FieldDateTime("Created", false, 100, 0),
                                        new FieldDateTime("Modified", false, 100, 0),
                                        new FieldString("CreatedBy", false, 10, 30),
                                        new FieldString("ModifiedBy", false, 10, 30),
                                    },
                                    Dependencies = new List<Dependency>()
                                    {
                                        new Dependency()
                                        {
                                            DependentTable = new Table("Branches", "BranchId")
                                            {
                                                Fields = new List<Field>()
                                                {
                                                    new FieldString("Title", false, 10, 100),
                                                    new FieldString("InventoryPart", false, 2, 2, false, true, false, false),
                                                    new FieldDateTime("Created", false, 100, 0),
                                                    new FieldDateTime("Modified", false, 100, 0),
                                                    new FieldString("CreatedBy", false, 10, 30),
                                                    new FieldString("ModifiedBy", false, 10, 30),
                                                },
                                                Dependencies = new List<Dependency>(),
                                            },
                                            Required = true,
                                            CanPeekExisting = true,
                                            PeekingPercentage = 90,
                                            TargetIdentityFieldName = "BranchId",
                                        }
                                    },
                                },
                                Required = true,
                                CanPeekExisting = true,
                                PeekingPercentage = 95,
                                TargetIdentityFieldName = "CategoryId",
                            },
                            new Dependency()
                            {
                                DependentTable = new Table("Groups", "GroupId")
                                {
                                    Fields = new List<Field>()
                                    {
                                        new FieldString("Title", false, 10, 100),
                                        new FieldDateTime("Created", false, 100, 0),
                                        new FieldDateTime("Modified", false, 100, 0),
                                        new FieldString("CreatedBy", false, 10, 30),
                                        new FieldString("ModifiedBy", false, 10, 30),

                                    },
                                    Dependencies = new List<Dependency>()
                                    {
                                        new Dependency()
                                        {
                                            DependentTable = new Table("Categories", "CategoryId")
                                            {
                                                Fields = new List<Field>()
                                                {
                                                    new FieldString("Title", false, 10, 100),
                                                    new FieldInteger("InventoryPart", false, 1, 99),
                                                    new FieldDateTime("Created", false, 100, 0),
                                                    new FieldDateTime("Modified", false, 100, 0),
                                                    new FieldString("CreatedBy", false, 10, 30),
                                                    new FieldString("ModifiedBy", false, 10, 30),
                                                },
                                            },
                                            Required = true,
                                            CanPeekExisting = true,
                                            PeekingPercentage = 100,
                                            TargetIdentityFieldName = "CategoryId",
                                        },
                                    },
                                },
                                Required = false,
                                CanPeekExisting = true,
                                PeekingPercentage = 87,
                                TargetIdentityFieldName = "GroupId",
                            }
                        },
                    }
                },
            };
        }
    }
}
