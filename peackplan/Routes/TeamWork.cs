using peackplan.Dtos;
using peackplan.Entities;
using peackplan.Services;

namespace peackplan.Routes;

public static class TeamWork
{
    public static void MapTeamWorkRouts(this IEndpointRouteBuilder app, string tag)
    {
        var route = app.MapGroup("api/v1/teamwork/");
        route.MapPost("create", async (ITeamWorkService teamWorkService, TeamWorkCreateDto param) =>
        {
            BaseResponse<TeamWorkEntity> result = await teamWorkService.CreateTeamWork(param);
            return result.Results;
        }).WithTags(tag);
        route.MapGet("Read",async (ITeamWorkService teamWorkService) =>
        {
            BaseResponse<List<TeamWorkEntity>> result=await teamWorkService.GetAllTeamWorks();
            return result.Results;
            
            
           
        }).WithTags(tag);

        route.MapDelete("Delete{id:guid}",async (ITeamWorkService teamWorkService,Guid id) =>
        {
            await teamWorkService.DeleteTeamWork(id); 
            return Results.Ok();
        }).WithTags(tag);

        route.MapPost("Update",async (ITeamWorkService teamWorkService,TeamWorkEntity param) =>
        {
            BaseResponse<TeamWorkEntity?> result=await teamWorkService.UpdateTeamWork(param);
             return result.Results;
        }).WithTags(tag);
    }
        
}