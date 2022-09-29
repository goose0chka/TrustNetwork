using Microsoft.EntityFrameworkCore;
using TrustNetwork.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
    contextOpt => contextOpt.UseNpgsql(builder.Configuration["ConnectionStrings:TrustNetworkDb"],
    npgsqlOpt => npgsqlOpt.MigrationsAssembly(typeof(DataContext).Assembly.FullName))
    );

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
