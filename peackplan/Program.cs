using Microsoft.EntityFrameworkCore;
using peackplan;
using peackplan.Dtos;
using peackplan.Entities;
using peackplan.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ سرویس‌های Swagger رو اینجا اضافه کن
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddDbContextPool<AppDbContext>(
    o=>o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDataBase")));


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamWorkService, TeamWorkService>(); 
builder.Services.AddScoped<ICompanyService, CompanyService>();

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



 
app.MapPost("teamwork/create",async (ITeamWorkService teamWorkService, TeamWorkEntity teamWorkEntity) =>
{
 TeamWorkEntity result=await teamWorkService.CreateTeamWork(teamWorkEntity);
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
app.Run();