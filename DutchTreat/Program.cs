using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddDbContext<DutchContext>(cfg =>
{
    cfg.UseSqlServer();
});

builder.Services.AddIdentity<StoreUser, IdentityRole>(options => 
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<DutchContext>();

builder.Services.AddAuthentication()
    .AddCookie()
    .AddJwtBearer(jwtOptions =>
    {         
        jwtOptions.TokenValidationParameters = new()
        {
            ValidIssuer = builder.Configuration.GetSection("Tokens:Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("Tokens:Audience").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Tokens:Key").Value))
        };
    });

builder.Services.AddTransient<IMailService, NullMailService>();
builder.Services.AddTransient<DutchSeeder>();
builder.Services.AddScoped<IDutchRepository, DutchRepository>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddRazorPages();

builder.Services.AddMvc();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "/seed")
{
    Console.WriteLine("Seeding database...");
    var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
    using var scope = scopeFactory.CreateScope();
    var seeder = app.Services.GetService<DutchSeeder>();
    seeder.SeedAsync().Wait();
    return;
}

// Configure the HTTP request pipeline.
app.UseStaticFiles();

app.MapRazorPages();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        "Default",
        "/{controller}/{action}/{id?}",
        new { controller = "App", action = "Index" });
});

app.Run();
