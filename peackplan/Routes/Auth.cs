using peackplan.Dtos;
using peackplan.Services;

namespace peackplan.Routes;

public static class Auth
{
    public static void MapAuthRouts(this IEndpointRouteBuilder app, string tag)
    {
        var route = app.MapGroup("api/v1/auth/");
       route.MapPost("login", async (IAuthService authService, LoginParam param) =>
        {
            LoginResponse? result = await authService.Login(param);
            if (result == null)
            {
                return Results.Unauthorized();

            }

            return Results.Ok(result);
        }).WithTags(tag);

        route.MapPost("register",async (IAuthService authService, UserCreateParams dto) =>
        {
            UserResponse result=await authService.Register(dto);
            return Results.Ok(result);
        }).WithTags(tag);


    }
}