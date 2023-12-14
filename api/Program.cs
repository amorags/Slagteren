
using infrastructure;
using infrastructure.DataModels;
using infrastructure.Repositories;

using service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString, sourceBuilder =>
{
    sourceBuilder.EnableParameterLogging();
} );

builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<UserService>();


builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<PasswordHashRepository>();
builder.Services.AddSingleton<AccountService>();


builder.Services.AddSingleton<ProductRepository>();


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


app.UseCors(options =>

{

   options.SetIsOriginAllowed(origin => true)

       .AllowAnyMethod()

       .AllowAnyHeader()

       .AllowCredentials();

});

//app.UseSecurityHeaders(); (remove above)

app.MapControllers();

app.Run();
