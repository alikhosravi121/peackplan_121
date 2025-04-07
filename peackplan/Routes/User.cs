using peackplan.Dtos;
using peackplan.Services;

namespace peackplan.Routes;

public static class User
{
    public static void MapUserRoutes(this IEndpointRouteBuilder app,string tag)
    {
        var route = app.MapGroup("api/v1/user/");
        route.MapPost("create",async (IUserService userService, UserCreateParams dto) =>
        {
            BaseResponse<UserResponse?> result=await userService.CreateUser(dto);
            return Results.Ok(result);
        }).WithTags(tag);
        route.MapGet("Read",async (IUserService userService) =>
        {
            BaseResponse<IEnumerable<UserResponse?>> result=await userService.GetAllUsers();
            return result.ToResult();
        }).WithTags(tag);
        route.MapGet("Read/{id:guid}",async (IUserService userService,Guid id) =>
        {
           BaseResponse<UserResponse?> result=await userService.GetUserById(id);

           return result.ToResult();
        }).WithTags(tag);
        route.MapPost("Update",async (IUserService userService,UserUpdateParams param) =>
        {
            BaseResponse<UserResponse?> result=await userService.UpdateUser(param);
             return result.ToResult();
        }).WithTags(tag);

        route.MapDelete("Delete{id:guid}",async (IUserService userService,Guid id) =>
        {
            await userService.DeleteUser(id); 
            return Results.Ok();
        }).WithTags(tag);


        route.MapGet("getprofile", async (IUserService userService) =>
        {
            BaseResponse<UserResponse?> result = await userService.Profile();
            return result.ToResult();
        }).WithTags(tag);
        ///.RequireAuthorization();
    }
}