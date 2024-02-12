using AspNetCore.Authentication.ApiKey;
using BLS.Server.Security;
using BLS.Server.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddTransient<IApiKeyRepository, APIKeyRepository>();

builder.Services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme)

// The below AddApiKeyInHeaderOrQueryParams without type parameter will require options.Events.OnValidateKey delegete to be set.
//.AddApiKeyInHeaderOrQueryParams(options =>

// The below AddApiKeyInHeaderOrQueryParams with type parameter will add the ApiKeyProvider to the dependency container. 
            .AddApiKeyInHeaderOrQueryParams<ApiKeyProvider>(options =>
            {
                options.Realm = "Sample Web API";
                options.KeyName = "X-API-KEY";
            });

builder.Services.AddControllers();

builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddSingleton<IDatabaseService, DatabaseService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()));


var app = builder.Build();

app.MapGet("/", () => "Hello Welcome to the BLS Server!");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

app.Run();

