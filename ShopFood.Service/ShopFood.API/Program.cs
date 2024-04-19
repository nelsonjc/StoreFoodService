using Microsoft.Extensions.Configuration;
using ShopFood.API.App_Start;
using ShopFood.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration.AddJsonFile("appsettings.json").Build();

builder.Services.AddControllers();
builder.Services.AddDIConfig();
builder.Services.AddJwtConfig(Configuration);
builder.Services.AddSwaggerConfig();
builder.Services.AddAutoMapperConfig();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwaggerConfig();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
