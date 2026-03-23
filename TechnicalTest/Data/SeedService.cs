using TechnicalTest.Models;

namespace TechnicalTest.Data
{
    public static class SeedService
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            if (!context.Roles.Any())
            {
                var role = new Role
                {
                    Name = "Administrador de Recursos",
                    NormalizedName = "ADMINISTRADOR DE RECURSOS"
                };

                await context.Roles.AddAsync(role);
                await context.SaveChangesAsync();
            }

            if (!context.Ministries.Any())
            {
                var ministry = new Ministry
                {
                    Name = "011 Ministerio de Salud",
                    NormalizedName = "011 MINISTERIO DE SALUD"
                };

                await context.Ministries.AddAsync(ministry);
                await context.SaveChangesAsync();
            }
        }
    }
}
