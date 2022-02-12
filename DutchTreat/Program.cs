﻿using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddDbContext<DutchContext>(cfg =>
{
    cfg.UseSqlServer();
});

builder.Services.AddTransient<IMailService, NullMailService>();
builder.Services.AddTransient<DutchSeeder>();
builder.Services.AddScoped<IDutchRepository, DutchRepository>();

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddJsonOptions(options =>
        // Handle circular references in entities
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddMvc();

builder.Services.AddRazorPages();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "/seed")
{
    Console.WriteLine("Seeding database...");
    var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
    using var scope = scopeFactory.CreateScope();
    var seeder = app.Services.GetService<DutchSeeder>();
    seeder.Seed();
    return;
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}
app.UseStaticFiles();

app.MapRazorPages();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        "Default",
        "/{controller}/{action}/{id?}",
        new { controller = "App", action = "Index" });
});

app.Run();
