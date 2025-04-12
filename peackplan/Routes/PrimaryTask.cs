using peackplan.Dtos;
using peackplan.Entities;
using peackplan.Services;

namespace peackplan.Routes;

public static class PrimaryTask
{
    public static void MapPrimaryTask(this IEndpointRouteBuilder app, string tag)
    {
        var route = app.MapGroup("api/v1/PrimaryTask/");
        route.MapPost("create",async (IPrimaryTaskService primaryTaskService,PrimaryTaskCreate param) =>
        {
          BaseResponse<PrimaryTaskResponse>? result = await primaryTaskService.CreatePrimaryTask(param);
            return Results.Ok(result);
        }).WithTags(tag);
    }
}