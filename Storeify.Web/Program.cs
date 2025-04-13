using Microsoft.Extensions.DependencyInjection;

namespace Storeify.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to AddDependencies class. 

            //ConnectionString
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //Identity

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            // .AddSignInManager<SignInManager<ApplicationUser>>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;
            });
            builder.Services.AddControllersWithViews();

            //AutoMapper
            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
            //ExpressiveAnnotations
            builder.Services.AddExpressiveAnnotations();


            // Register custom services
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IService<Store>, StoreService>();
            builder.Services.AddScoped<IService<Branch>, BranchService>();
            builder.Services.AddScoped<IService<Category>, CategoryService>();
            builder.Services.AddScoped<IService<Inventory>, InventoryService>();
            builder.Services.AddScoped<IService<Product>, ProductService>();
            builder.Services.AddScoped<IService<ApplicationUser>, UserService>();
            builder.Services.AddScoped<IService<IdentityRole>, RoleService>();

            builder.Services.AddLogging();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Seed roles
                await DefaultRoles.SeedAsync(roleManger);

                // Seed the Admin user
                await DefaultUsers.SeedAdminUserAsync(userManger);
            }

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    } 
}
