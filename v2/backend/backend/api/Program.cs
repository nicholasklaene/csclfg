using System.Reflection;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using api.Data;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    options.UseNpgsql(connectionString);
});


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Auth/Cognito setup
var region = builder.Configuration.GetValue<string>("AWSCognito:Region");
var poolId = builder.Configuration.GetValue<string>("AWSCognito:PoolId");
var clientId = builder.Configuration.GetValue<string>("AWSCognito:AppClientId");
var credentials = new AnonymousAWSCredentials();
var provider = new AmazonCognitoIdentityProviderClient();
var userPool = new CognitoUserPool(poolId, clientId, provider);
builder.Services.AddSingleton<AnonymousAWSCredentials>(credentials);
builder.Services.AddSingleton<AmazonCognitoIdentityProviderClient>(provider);
builder.Services.AddSingleton<CognitoUserPool>(userPool);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = $"https://cognito-idp.{region}.amazonaws.com/{poolId}",
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidAudience = clientId,
            ValidateAudience = false
        };
        options.MetadataAddress = 
            $"https://cognito-idp.{region}.amazonaws.com/{poolId}/.well-known/openid-configuration";
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
