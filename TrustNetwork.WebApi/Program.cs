using Microsoft.EntityFrameworkCore;
using Npgsql;
using TrustNetwork.BL.Services;
using TrustNetwork.DAL;
using TrustNetwork.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connStrBuilder = new NpgsqlConnectionStringBuilder
{
    Host = Environment.GetEnvironmentVariable("TRSUTNETWORK_DB_HOST"),
    Port = int.Parse(Environment.GetEnvironmentVariable("TRSUTNETWORK_DB_PORT")!),
    Username = "postgres",
    Password = Environment.GetEnvironmentVariable("TRUSTNETWORK_DB_PWORD")
};

builder.Services.AddDbContext<DataContext>(
    contextOpt => contextOpt.UseNpgsql(
        connStrBuilder.ConnectionString,
        npgsqlOpt => npgsqlOpt.MigrationsAssembly(typeof(DataContext).Assembly.FullName)
    )
);

builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<MessagingService>();

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var db = scope.ServiceProvider.GetRequiredService<DataContext>())
{
    if (db.Database.GetPendingMigrations().Any())
        db.Database.Migrate();
}

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
