using Poster.Identity.Services;
using Poster.Identity.Interfaces;
using Poster.Identity.Infrastructure;
using Poster.Data.Infrastructure;
using Poster.Data.Interfaces;
using Poster.Data.Services;
using Serilog;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs.txt")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddJwtTokenValidation();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddSession();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddTransient<IPostRepository, PostRepository>();

builder.Services.AddPosterDbContext();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
