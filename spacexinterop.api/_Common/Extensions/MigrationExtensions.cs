using Microsoft.EntityFrameworkCore;
using spacexinterop.api.Data;

namespace spacexinterop.api._Common.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        using MainContext context = scope.ServiceProvider.GetRequiredService<MainContext>();

        context.Database.Migrate();
    }
}