using MagicOFBulgarian.Data.Domain;
using Microsoft.AspNetCore.Identity;

namespace MagicOFBulgarian.IdentitySeed
{
    public class IdentitySeeder
    {
        private readonly UserManager<CustomerUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PasswordHasher<CustomerUser> _passwordHasher;

        public IdentitySeeder(UserManager<CustomerUser> userManager, RoleManager<IdentityRole> roleManager, PasswordHasher<CustomerUser> passwordHasher)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
        }

        public async Task SeedRolesAndUsers()
        {
            await SeedRoles();
            await SeedUsers();
        }

        public async Task SeedUsers()
        {
            CustomerUser admin = new CustomerUser();
            admin.UserName = "admin";
            admin.Email = "";
            admin.NormalizedUserName = admin.UserName.ToUpper();
            admin.FirstName = "admin";
            admin.LastName = string.Empty;
            admin.PhoneNumber = string.Empty;
            admin.EGN = "";
            admin.PasswordHash = _passwordHasher.HashPassword(admin, "admin");

            await SeedUser(admin, Roles.Role_Admin);

            CustomerUser customer = new CustomerUser();
            customer.UserName = "customer";
            customer.Email = "";
            customer.NormalizedUserName = customer.UserName.ToUpper();
            customer.FirstName = "customer";
            customer.LastName = string.Empty;
            customer.PhoneNumber = string.Empty;
            customer.EGN = "";
            customer.PasswordHash = _passwordHasher.HashPassword(admin, "customer");

            await SeedUser(customer, Roles.Role_Customer);
        }

        public async Task SeedRoles()
        {
            List<string> roles = new List<string>()
            {
                Roles.Role_Admin,
                Roles.Role_Customer
            };

            foreach (var r in roles)
            {
                IdentityRole role = new IdentityRole(r);
                role.NormalizedName = role.Name.ToUpper();
                await SeedRole(role);
            }
        }

        public async Task SeedRole(IdentityRole role)
        {
            if (!_roleManager.Roles.Any(r => r.Name == role.Name))
            {
                await _roleManager.CreateAsync(role);
            }
        }

        public async Task SeedUser(CustomerUser CustomerUser, string role)
        {
            if (!_userManager.Users.Any(u => u.UserName == CustomerUser.UserName))
            {
                await _userManager.CreateAsync(CustomerUser);
                await _userManager.AddToRoleAsync(CustomerUser, role);
            }
        }
    }
}