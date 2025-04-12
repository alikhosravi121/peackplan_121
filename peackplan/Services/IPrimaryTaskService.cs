using Microsoft.EntityFrameworkCore.ChangeTracking;
using peackplan.Dtos;
using peackplan.Entities;
using peackplan.Enums;

namespace peackplan.Services;

public interface IPrimaryTaskService
{
    Task<BaseResponse<PrimaryTaskResponse>> CreatePrimaryTask(PrimaryTaskCreate param);
    Task<BaseResponse<PrimaryTaskEntity>> UpdatePrimaryTask(PrimaryTaskEntity primaryTask);
    Task DeletePrimaryTask(Guid primaryTaskId);
    Task<BaseResponse<List<PrimaryTaskEntity>>> GetPrimaryTasks();
    Task<BaseResponse<PrimaryTaskEntity>> GetPrimaryTask(Guid primaryTaskId);
}

public class PrimaryTaskService(AppDbContext dbContext) : IPrimaryTaskService
{
    public async Task<BaseResponse<PrimaryTaskResponse>> CreatePrimaryTask(PrimaryTaskCreate param)
    {
        UserEntity? user=await dbContext.Users.FindAsync(param.ManagerId);
        if (user == null)return new BaseResponse<PrimaryTaskResponse>(result: null, status: 404, message: "User not found");

        PrimaryTaskEntity primaryTask = new()
        {
            Id = Guid.CreateVersion7(),
            Title = param.Title,
            Description = param.Description,
            Status = param.Status,
            AccessLevel = param.AccessLevel,
            ParentTaskId = param.ParentTaskId,
            AvatarId = param.AvatarId,
            DueDate = param.DueDate,
            Tags = param.Tags,
            ManagerId = param.ManagerId,
        };

        
        EntityEntry<PrimaryTaskEntity> entity = dbContext.PrimaryTasks.Add(primaryTask);
        await dbContext.SaveChangesAsync();
        PrimaryTaskResponse primaryTaskResponse = new()
        {
            Id = primaryTask.Id,
            Title = primaryTask.Title,
            Description = primaryTask.Description,
            Status = primaryTask.Status,
            AccessLevel = primaryTask.AccessLevel,
            ParentTaskId =primaryTask.ParentTaskId,
            AvatarId = primaryTask.AvatarId,
            DueDate = primaryTask.DueDate,
            Tags = primaryTask.Tags,
            NameManager = user.Fullname,
            AvatarManager = user.AvatarId.ToString(),
            ManagerId = user.Id, 
        };
        return new BaseResponse<PrimaryTaskResponse>(result:primaryTaskResponse,status:200,message:"Success");
    }

    public Task<BaseResponse<PrimaryTaskEntity>> UpdatePrimaryTask(PrimaryTaskEntity primaryTask)
    {
        throw new NotImplementedException();
    }

    public Task DeletePrimaryTask(Guid primaryTaskId)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<List<PrimaryTaskEntity>>> GetPrimaryTasks()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<PrimaryTaskEntity>> GetPrimaryTask(Guid primaryTaskId)
    {
        throw new NotImplementedException();
    }
}
