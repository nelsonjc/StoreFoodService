using ShopFood.API.App_Start;
using ShopFood.API.Middlewares;
using Azure.Identity;
using ShopFood.Domain.Variables;

DefaultAzureCredentialOptions options = null;
var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration.AddJsonFile(AppConfig.AppsettingsWithExt).Build();
bool isDevelopment = bool.Parse(Configuration[AppConfig.AppSetting_IsDevelopment]);

builder.Services.AddControllers();
builder.Services.AddDIConfig();
builder.Services.AddJwtConfig(Configuration);
builder.Services.AddSwaggerConfig();
builder.Services.AddAutoMapperConfig();
builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration[AppConfig.AppInsights_ConnectionString]
});

if (!isDevelopment)
{
    options = new()
    {
        ExcludeAzureCliCredential = true,
        ExcludeEnvironmentCredential = true,
        ExcludeAzureDeveloperCliCredential = true,
        ExcludeAzurePowerShellCredential = true,
        ExcludeInteractiveBrowserCredential = true,
        ExcludeSharedTokenCacheCredential = true,
        ExcludeWorkloadIdentityCredential = true,
        ExcludeManagedIdentityCredential = false,
        ExcludeVisualStudioCodeCredential = true,
        ExcludeVisualStudioCredential = true
    };
}

var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable(AppConfig.KeyVaultUri));
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential(options));

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
