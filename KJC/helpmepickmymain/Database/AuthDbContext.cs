using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace helpmepickmymain.Database
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "33a56c4a-2fc6-4438-bd94-8eb35d517ac5";

            var adminIdentityRole = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            };

            builder.Entity<IdentityRole>().HasData(adminIdentityRole);

            var adminUserId = "0020b04d-9e61-498f-8f2e-33721079ae4d";
            var adminIdentityUser = new IdentityUser
            {
                UserName = "Hi Github",
                Email = "HiGithub@wgu.edu",
                NormalizedEmail = "HiGithub@wgu.edu".ToUpper(),
                NormalizedUserName = "Hi Github".ToUpper(),
                Id = adminUserId,
            };

            adminIdentityUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(adminIdentityUser, "D424task3");

            builder.Entity<IdentityUser>().HasData(adminIdentityUser);

            var adminRole = new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId,
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRole);
            
        }
    }
}
