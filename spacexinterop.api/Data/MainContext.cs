using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using spacexinterop.api.Data.Models;

namespace spacexinterop.api.Data;

public class MainContext(DbContextOptions<MainContext> options) : IdentityDbContext<User>(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
#endif
        optionsBuilder.ConfigureWarnings(warnings =>
        {
            warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning);
            warnings.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS);
        });

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}