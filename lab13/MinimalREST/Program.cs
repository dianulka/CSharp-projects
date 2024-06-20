using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextDb") ?? throw new InvalidOperationException("Connection string 'ContextDb' not found.")));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await DbInitializer.Initialize(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// app.MapGet("/", () => "Hello World!");

// app.Run();
