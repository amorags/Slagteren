using infrastructure;
using infrastructure.Repositories;
using service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString, sourceBuilder =>
{
    sourceBuilder.EnableParameterLogging();
} );

builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<UserService>();

builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<UserRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyMethod()
        .AllowCredentials();
});

// app.UseSecurityHeaders(); (remove above)

app.MapControllers();

app.Run();
