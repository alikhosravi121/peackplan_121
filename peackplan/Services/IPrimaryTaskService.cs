using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.OpenApi.Extensions;
using peackplan.Dtos;
using peackplan.Entities;
using peackplan.Enums;

namespace peackplan.Services;

public interface IPrimaryTaskService
{
    Task<BaseResponse<PrimaryTaskResponse>> CreatePrimaryTask(PrimaryTaskCreate param);
    Task<BaseResponse<PrimaryTaskResponse?>> UpdatePrimaryTask(PrimaryTaskUpdate primaryTask);
    Task DeletePrimaryTask(Guid primaryTaskId);
    Task<BaseResponse<List<PrimaryTaskResponse>>> GetPrimaryTasks();
    Task<BaseResponse<PrimaryTaskResponse>> GetPrimaryTask(Guid primaryTaskId);
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
            Status =primaryTask.Status,
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

    public async Task<BaseResponse<PrimaryTaskResponse?>> UpdatePrimaryTask(PrimaryTaskUpdate param)
    {
          PrimaryTaskEntity? task=await dbContext.PrimaryTasks.FindAsync(param.Id);
        if (task == null)return new BaseResponse<PrimaryTaskResponse?>(result: null, status: 404, message: "User not found");
        if(param.Title!=null) task.Title = param.Title;
        if(param.Description!=null)task.Description = param.Description;
     if (param.AccessLevel != null)
    task.AccessLevel = param.AccessLevel.Value;
        if(param.ParentTaskId!=null)task.ParentTaskId = param.ParentTaskId;
        if(param.AvatarId!=null)task.AvatarId = param.AvatarId;
        if(param.DueDate!=null)task.DueDate = param.DueDate;
        if(param.Tags!=null)task.Tags = param.Tags;
        if(param.ManagerId!=null)task.ManagerId = param.ManagerId.Value;
        
        dbContext.PrimaryTasks.Update(task);
        await dbContext.SaveChangesAsync();
        var response= new PrimaryTaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            AccessLevel = task.AccessLevel, 
            ParentTaskId = task.ParentTaskId,
            AvatarId = task.AvatarId,
            DueDate = task.DueDate,
            Tags = task.Tags,
            NameManager = task.Manager != null ? task.Manager.Fullname : "نامشخص",
            AvatarManager = task.Manager != null ? task.Manager.AvatarId.ToString() : "نامشخص",
            ManagerId = task.ManagerId,
        };
        return new BaseResponse<PrimaryTaskResponse?>(result: response, status: 200, message: "Success");
    }

    public async Task DeletePrimaryTask(Guid primaryTaskId)
    {
        PrimaryTaskEntity? task= dbContext.PrimaryTasks.Find(primaryTaskId);
        if (task != null)
        {
            dbContext.PrimaryTasks.Remove(task);
             await dbContext.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Task not found");
        }
    }

    public async Task<BaseResponse<List<PrimaryTaskResponse>>> GetPrimaryTasks()
    {
        var list = await dbContext.PrimaryTasks
            .Include(x => x.Manager) // ← برای دسترسی به اطلاعات کاربر
            .Select(x => new PrimaryTaskResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                AccessLevel = x.AccessLevel, 
                ParentTaskId = x.ParentTaskId,
                AvatarId = x.AvatarId,
                DueDate = x.DueDate,
                Tags = x.Tags,
                ManagerId = x.ManagerId,
                NameManager = x.Manager != null ? x.Manager.Fullname : "نامشخص"
               
            })
            .ToListAsync();

        return new BaseResponse<List<PrimaryTaskResponse>>(result: list, status: 200, message: "Success");
         }

    public async Task<BaseResponse<PrimaryTaskResponse>> GetPrimaryTask(Guid primaryTaskId)
    {
       var task = await dbContext.PrimaryTasks
    .Include(x => x.Manager)
    .Select(x => new PrimaryTaskResponse
    {
        Id = x.Id,
        Title = x.Title,
        Description = x.Description,
        Status = x.Status,
        AccessLevel = x.AccessLevel,
        ParentTaskId = x.ParentTaskId,
        AvatarId = x.AvatarId,
        DueDate = x.DueDate,
        Tags = x.Tags,
        ManagerId = x.ManagerId,
        NameManager = x.Manager != null ? x.Manager.Fullname : "نامشخص"
    })
    .FirstAsync(x => x.Id == primaryTaskId);

        return new BaseResponse<PrimaryTaskResponse>(result: task, status: 200, message: "Success");
    }
}
