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
    o=>o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDataBase")));


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

 app.MapPost("user/create",async (IUserService userService, UserCreateParams dto) =>
 {
UserResponse result=await userService.CreateUser(dto);
return Results.Ok(result);
 });
app.MapGet("user/Read",async (IUserService userService) =>
 {
IEnumerable<UserResponse> result=await userService.GetAllUsers();
return Results.Ok(result);
 });
app.MapGet("user/Read/{id:guid}",async (IUserService userService,Guid id) =>
 {
UserResponse? result=await userService.GetUserById(id);
if (result == null)
{
 return Results.NotFound();
}
return Results.Ok(result);
 });
app.MapPost("user/Update",async (IUserService userService,UserUpdateParams param) =>
 {
UserResponse? result=await userService.UpdateUser(param);
if (result == null)
{
 return Results.NotFound();
}
return Results.Ok(result);
 });

app.MapDelete("user/Delete{id:guid}",async (IUserService userService,Guid id) =>
 {
await userService.DeleteUser(id); 
return Results.Ok();
 });



 
app.MapPost("teamwork/create",async (ITeamWorkService teamWorkService, TeamWorkCreateDto param) =>
{
 TeamWorkEntity result=await teamWorkService.CreateTeamWork(param);
 return Results.Ok(result);
});
app.MapGet("teamwork/Read",async (ITeamWorkService teamWorkService) =>
{
 IEnumerable<TeamWorkEntity> result=await teamWorkService.GetAllTeamWorks();
 return Results.Ok(result);
});

app.MapDelete("teamwork/Delete{id:guid}",async (ITeamWorkService teamWorkService,Guid id) =>
{
 await teamWorkService.DeleteTeamWork(id); 
 return Results.Ok();
});

app.MapPost("teamwork/Update",async (ITeamWorkService teamWorkService,TeamWorkEntity param) =>
{
 TeamWorkEntity? result=await teamWorkService.UpdateTeamWork(param);
 if (result == null)
 {
  return Results.NotFound();
 }
 return Results.Ok(result);
});

app.MapPost("company/create",async (ICompanyService companyService, CompanyEntity companyEntity) =>
{
 CompanyEntity result=await companyService.CreateCompany(companyEntity);
 return Results.Ok(result);
});
app.MapGet("company/Read",async ( ICompanyService companyService) =>
{
 IEnumerable<CompanyEntity> result=await companyService.GetCompanies();
 return Results.Ok(result);
});
app.MapDelete("company/Delete{id:guid}",async (ICompanyService companyService,Guid id) =>
{
 await companyService.DeleteCompany(id); 
 return Results.Ok();
});

app.MapPost("company/Update",async (ICompanyService companyService,CompanyEntity param) =>
{
 CompanyEntity? result=await companyService.UpdateCompany(param);
 if (result == null)
 {
  return Results.NotFound();
 }
 return Results.Ok(result);
});

app.MapPost("auth/login", async (IAuthService authService, LoginParam param) =>
{
 LoginResponse? result = await authService.Login(param);
 if (result == null)
 {
  return Results.Unauthorized();

 }

 return Results.Ok(result);
});

app.MapPost("auth/register",async (IAuthService authService, UserCreateParams dto) =>
{
 UserResponse result=await authService.Register(dto);
 return Results.Ok(result);
});

app.MapGet("user/getprofile", async (IUserService userService) =>
{
 UserResponse? result = await userService.Profile();
 return result == null ? Results.Unauthorized() : Results.Ok(result);
});
 ///.RequireAuthorization();
app.Run();