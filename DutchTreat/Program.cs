var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IMailService, NullMailService>();

builder.Services.AddMvc();

builder.Services.AddRazorPages();

var app = builder.Build();

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
