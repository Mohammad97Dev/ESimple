using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ESimple.EntityFrameworkCore
{
    public static class ESimpleDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ESimpleDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ESimpleDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
