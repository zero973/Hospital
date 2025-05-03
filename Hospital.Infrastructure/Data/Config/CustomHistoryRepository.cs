using Hospital.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;
using System.Reflection;
using System.Text;

namespace Hospital.Infrastructure.Data.Config;

#pragma warning disable EF1001
internal class CustomHistoryRepository : NpgsqlHistoryRepository
{

    private const string ExecutionDateColumnName = "execution_date";

    public CustomHistoryRepository(HistoryRepositoryDependencies dependencies) : base(dependencies)
    { }

    protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> builder)
    {
        builder.ToTable(DbConstants.MigrationsHistoryTableName, DbConstants.PublicSchema);

        builder.HasKey(h => h.MigrationId);

        builder.Property(h => h.MigrationId)
            .HasMaxLength(150)
            .HasColumnName("id");

        builder.Property(h => h.ProductVersion)
            .HasColumnName("product_version")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property<DateTime>(ExecutionDateColumnName).IsRequired();
    }

    public override string GetInsertScript(HistoryRow row)
    {
        var stringTypeMapping = Dependencies.TypeMappingSource.GetMapping(typeof(string));

        var file = new FileInfo(Assembly.GetEntryAssembly()!.Location);
        var version = Assembly.GetExecutingAssembly().GetName().Version!.ToString();

        return new StringBuilder().Append("INSERT INTO ")
            .Append(SqlGenerationHelper.DelimitIdentifier(TableName, TableSchema))
            .Append(" (")
            .Append(SqlGenerationHelper.DelimitIdentifier(MigrationIdColumnName))
            .Append(", ")
            .Append(SqlGenerationHelper.DelimitIdentifier(ProductVersionColumnName))
            .Append(", ")
            .Append(SqlGenerationHelper.DelimitIdentifier(ExecutionDateColumnName))
            .AppendLine(")")
            .Append("VALUES (")
            .Append(stringTypeMapping.GenerateSqlLiteral(row.MigrationId))
            .Append(", ")
            .Append(stringTypeMapping.GenerateSqlLiteral(version))
            .Append(", ")
            .Append("now()")
            .Append(')')
            .AppendLine(SqlGenerationHelper.StatementTerminator)
            .ToString();
    }

}