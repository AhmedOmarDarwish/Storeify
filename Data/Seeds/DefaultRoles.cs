namespace Storeify.Data.Seeds
{
    public static class DefaultRoles 
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Manager));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.InventoryManager));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Cashier));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Customer));
            }
        }
    }
}