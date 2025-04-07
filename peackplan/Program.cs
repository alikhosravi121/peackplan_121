using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using peackplan;
using peackplan.Dtos;
using peackplan.Entities;
using peackplan.Routes;
using peackplan.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(
 options=>{
options.SerializerOptions.PropertyNamingPolicy=JsonNamingPolicy.CamelCase;
options.SerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles;
options.SerializerOptions.DefaultIgnoreCondition=JsonIgnoreCondition.WhenWritingNull;
options.SerializerOptions.WriteIndented = false;
 });


// ✅ سرویس‌های Swagger رو اینجا اضافه کن
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
 c.UseInlineDefinitionsForEnums();
 c.OrderActionsBy(s => s.RelativePath);

 // ✅ تعریف هدر Authorization
 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
 {
  Description = "Jwt Authorization header.\r\n\r\nمثال: \"Bearer 12345abcdef\"",
  Name = "Authorization",
  In = ParameterLocation.Header,
  Type = SecuritySchemeType.ApiKey,
  Scheme = "Bearer"
 });

 // ✅ الزام به استفاده از هدر Authorization در تمام درخواست‌ها
 c.AddSecurityRequirement(new OpenApiSecurityRequirement
 {
  {
   new OpenApiSecurityScheme
   {
    Reference = new OpenApiReference 
    { 
     Type = ReferenceType.SecurityScheme, 
     Id = "Bearer" 
    },
    Scheme = "Bearer",
    Name = "Bearer",
    In = ParameterLocation.Header,
   },
   Array.Empty<string>()
  }
 });
});

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddDbContextPool<AppDbContext>(
    
    o=>
    {
     o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
     o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDataBase"));
    });


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamWorkService, TeamWorkService>(); 
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(options =>
{
 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
 options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
 options.RequireHttpsMetadata = false;
 options.SaveToken = true;
 options.TokenValidationParameters = new TokenValidationParameters
 {
  RequireSignedTokens = true,
  ValidateIssuerSigningKey = true,
  ValidateIssuer = true,
  ValidateAudience = true,
  RequireExpirationTime = true,
  ClockSkew = TimeSpan.Zero,
  ValidIssuer = "peackplanpeackplanpeackplanpeackplanpeackplan",
  ValidAudience = "peackplanpeackplanpeackplanpeackplanpeackplan",
  IssuerSigningKey = new SymmetricSecurityKey("aliiiiii121,1211211121111211"U8.ToArray()),

 };
});
var app = builder.Build();

 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserRoutes("UserRoutes");
app.MapCompanyRoutes("CompanyRoutes"); 
app.MapTeamWorkRouts("TeamWorkRoutes");
app.MapAuthRouts("AuthRoutes");
app.Run();