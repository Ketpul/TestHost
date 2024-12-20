using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CaloriesCalculator.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<ProgressEntry> ProgressEntries { get; set; }
        public DbSet<SelectedFood> SelectedFoods { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Food>()
                .HasKey(f => f.Name);

            builder.Entity<SelectedFood>()
                .HasKey(sf => sf.Id);

            builder.Entity<SelectedFood>()
                .HasOne<Food>()
                .WithMany()
                .HasForeignKey(sf => sf.Name)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Food>()
                .HasOne(f => f.Category)
                .WithMany()
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public static async Task SeedData(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            // Създаване на роля "Admin" ако не съществува
            var roleExists = await roleManager.RoleExistsAsync("Admin");
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Проверка дали администраторският потребител съществува
            var user = await userManager.FindByEmailAsync("admin@domain.com");
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "admin@domain.com",
                    Email = "admin@domain.com"
                };

                var result = await userManager.CreateAsync(user, "AdminPassword123!");
            }
        }

    }

}
