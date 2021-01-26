using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Models;
using ZwajApp.API.Models;

namespace Data
{
    //public class DataContext : IDesignTimeDbContextFactory<DbContext>
    //{
    //    public DbSet<Value> values { get; set; }

    //    public DbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
    //        // optionsBuilder.UseSqlite("Data Source=blog.db", b => b.MigrationsAssembly("ZwajApp.API"));
    //        optionsBuilder.UseSqlite("Data Source=ZawjApp.db", b => b.MigrationsAssembly("ZwajApp.API"));

    //        return new DbContext(optionsBuilder.Options);
    //    }
    //}

    public class DataContext : DbContext
    {
        public DbSet<Value> values { get; set; }

         public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}