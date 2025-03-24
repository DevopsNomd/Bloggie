using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // seeds Roles (User, Admin, SuperAdmin)

            var adminRoleId = "bd542056-ddd1-4184-8f4d-bf018cc0d088";
            var superAdminRoleId = "5b6c0b8d-a2ff-49a3-878e-bc9b605a313d";
            var userRoleId = "cf88d9bf-01a7-41e6-a25e-aae72330d393";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId

                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId

                }
            };


            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdminUser
            var superAdminId = "a450775e-1e04-46ac-bc6d-c39f9036a401";

            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId

            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                   new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId ,
                    UserId = superAdminId
                },
                      new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }

            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);


        }


    }
}
