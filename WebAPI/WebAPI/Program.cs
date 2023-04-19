using FCBankBasicHelper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BL.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Abstract;
using WebAPI.Domain;
using System.IdentityModel.Tokens.Jwt;
using Serilog;
using WebAPI.Sinks;
using WebAPI.Token;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authorization;
using BL.Repositories.Interfaceis;
using BL.Repositories;
using BL.Repository;
using BL;
using BL.Hashing;
using BL.MailConfirmation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FcbankBasicContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=FC-PROG-43\\MSSQLSERVER02;Database=FCBankBasic;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<FcbankBasicContext>()
    .AddDefaultTokenProviders();
//builder.Services.AddHostedService<Worker>();
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .WriteTo.CustomSink()
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//builder.Services.AddScoped<UserManager<IdentityUser>>();
//builder.Services.AddTransient<TokenService>();
//builder.Services.AddSingleton<TokenBL>();
builder.Services.AddScoped<UserBL>();
builder.Services.AddScoped<CustomerBl>();
builder.Services.AddScoped<PhoneBl>();
builder.Services.AddScoped<RoleBl>();
builder.Services.AddSingleton<ConfigBl>();

builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PhoneRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<TokenRepository>();

builder.Services.AddTransient<Validation>();
builder.Services.AddTransient<Encryption>();
builder.Services.AddTransient<MailSender>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidIssuer = builder.Configuration["JWT:Issuer"],
//        ValidAudience = builder.Configuration["JWT:Audience"],
//        RequireExpirationTime = true,
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//    };
//    options.Events = new JwtBearerEvents
//    {
//        OnTokenValidated = context =>
//        {
//            // Retrieve the validated token from the context
//            var accessToken = context.SecurityToken as JwtSecurityToken;
//            if (accessToken == null)
//            {
//                context.Fail("Unauthorized");
//            }
//            else
//            {
//                var tokenValidationParameters = options.TokenValidationParameters;
//                try
//                {
//                    var principal = new JwtSecurityTokenHandler().ValidateToken(
//                        accessToken.RawData, tokenValidationParameters, out var securityToken);

//                    context.Principal = principal;
//                    context.Success();
//                }
//                catch (Exception ex)
//                {
//                    if (ex is not SecurityTokenExpiredException)
//                    {
//                        var tokenBL = context.HttpContext.RequestServices.GetRequiredService<TokenBL>();
//                        try
//                        {
//                            var user = tokenBL.GetTokens(context.Principal.FindFirst(ClaimTypes.Name).Value);
//                            if (user is null)
//                                throw new Exception("Revoke failed!"); ;
//                            user.RefreshToken = null;
//                            tokenBL.Save();
//                        }
//                        catch (Exception)
//                        {
//                            throw new Exception("Revoke failed!");
//                        }
//                    }
//                }
//            }
//            return Task.CompletedTask;
//        }
//    };
//});
var app = builder.Build();
//var tokenService = app.Services.GetRequiredService<ITokenService>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
//app.Use(async (context, next) =>
//{
//    try
//    {
//        await next();
//    }
//    catch (Exception ex)
//    {
//        if (ex is SecurityTokenExpiredException)
//        {
//            RefreshTokenRequest tokenApiModel = new RefreshTokenRequest
//            {
//                AccessToken = context.Request.Headers["AccessToken"].FirstOrDefault(),
//                RefreshToken = context.Request.Headers["RefreshToken"].FirstOrDefault()
//            };
//            if (!string.IsNullOrEmpty(tokenApiModel.AccessToken) && !string.IsNullOrEmpty(tokenApiModel.RefreshToken))
//            {
//                var newtokenPair = tokenService.Refresh(tokenApiModel);
//                if (!string.IsNullOrEmpty(newtokenPair.ToString()))
//                {
//                    var httpResponse = context.Response;
//                    httpResponse.Headers["AccessToken"] = newtokenPair.AccessToken;
//                    httpResponse.Headers["RefreshToken"] = newtokenPair.RefreshToken;
//                    httpResponse.Headers["Access-Control-Expose-Headers"] = "AccessToken, RefreshToken";
//                }
//            }
//        }
//    }
//});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
