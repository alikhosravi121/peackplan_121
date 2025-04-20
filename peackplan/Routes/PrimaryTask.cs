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
        
        route.MapGet("Read",async (IPrimaryTaskService primaryTaskService) =>
        {
            
          //  BaseResponse<List<PrimaryTaskResponse?>> result=await primaryTaskService.GetPrimaryTasks();
            BaseResponse<List<PrimaryTaskResponse?>> result=await primaryTaskService.GetPrimaryTasks();
            return result.ToResult();
        }).WithTags(tag);  
        
        
        
        route.MapGet("ReadById/{id:guid}",async (IPrimaryTaskService primaryTaskService,Guid id) =>
        {
            
          //  BaseResponse<List<PrimaryTaskResponse?>> result=await primaryTaskService.GetPrimaryTasks();
           BaseResponse<PrimaryTaskResponse?> result = await primaryTaskService.GetPrimaryTask(id);
            return result.ToResult();
        }).WithTags(tag);

        route.MapPut("Update",async (IPrimaryTaskService primaryTaskService,PrimaryTaskUpdate param) =>
        { 
          
            BaseResponse<PrimaryTaskResponse?> result = await primaryTaskService.UpdatePrimaryTask(param);
            return result.ToResult();
        }).WithTags(tag);

        route.MapDelete("Delete/{id:guid}",async (IPrimaryTaskService primaryTaskService,Guid id) =>
        {
            await primaryTaskService.DeletePrimaryTask(id);
            return Results.Ok(new BaseResponse<string>(result: null, status: 200, message: "Deleted successfully"));
        }).WithTags(tag);
        
    }
    
    
}