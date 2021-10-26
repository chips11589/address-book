using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    public partial class Schema_1_0 : Migration
    {
        private readonly string _sqlResourcePath;

        public Schema_1_0()
        {
            _sqlResourcePath = $"Infrastructure.Persistence.SQL.SchemaObjects.{GetType().Name}";
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ExecuteSqlFiles(migrationBuilder, "Indexes");
            ExecuteSqlFiles(migrationBuilder, "StoredProcedures");
        }

        private void ExecuteSqlFiles(MigrationBuilder migrationBuilder, string folder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var sqlFiles = assembly.GetManifestResourceNames().
                        Where(file => file.StartsWith($"{_sqlResourcePath}.{folder}") && file.EndsWith(".sql"));

            foreach (var sqlFile in sqlFiles)
            {
                using (Stream stream = assembly.GetManifestResourceStream(sqlFile))
                using (StreamReader reader = new StreamReader(stream))
                {
                    var sqlScript = reader.ReadToEnd()
                        .Replace($"{Environment.NewLine}GO", Environment.NewLine)
                        .Replace("'", "''");
                    migrationBuilder.Sql($"EXEC(N'{sqlScript}')",
                        suppressTransaction: true);
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
