using Microsoft.EntityFrameworkCore;
using peackplan.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using peackplan.Dtos;

namespace peackplan.Services;

public interface ITeamWorkService
{
    Task<BaseResponse<TeamWorkEntity>> CreateTeamWork(TeamWorkCreateDto param);
    Task<BaseResponse<TeamWorkEntity?>> UpdateTeamWork(TeamWorkEntity teamWork);
    Task DeleteTeamWork(Guid teamWorkId);
    Task<BaseResponse<List<TeamWorkEntity>>> GetAllTeamWorks();
}

public class TeamWorkService(AppDbContext dbContext) : ITeamWorkService
{
    public async Task<BaseResponse<TeamWorkEntity>> CreateTeamWork(TeamWorkCreateDto param)
    {
        List<UserEntity> userList = [];
        foreach (Guid userId in param.Users)
        {
          UserEntity? user=await  dbContext.Users.FindAsync(userId);
          userList.Add(user);
        }
        TeamWorkEntity teamwork = new()
        {
            Id = Guid.NewGuid(),
            Title=param.Title,
            Users = userList
            
        };
        EntityEntry<TeamWorkEntity> entity = dbContext.TeamWorks.Add(teamwork);
        await dbContext.SaveChangesAsync();

        return new BaseResponse<TeamWorkEntity>(result:entity.Entity, status: 200, message: "Success");
    }

    public async Task<BaseResponse<TeamWorkEntity?>> UpdateTeamWork(TeamWorkEntity param)
    {
        TeamWorkEntity? team=await dbContext.TeamWorks.FindAsync(param.Id);
        if (team == null) return new BaseResponse<TeamWorkEntity?>(result: null, status: 404, message: "Not Found");
        if(param.Title!=null)team.Title=param.Title;
        if (param.CompanyId!=null)team.CompanyId=param.CompanyId;
        dbContext.TeamWorks.Update(team);
        await dbContext.SaveChangesAsync();
        return new BaseResponse<TeamWorkEntity?>(result: team, status: 200, message: "Success");
    }

    public async Task DeleteTeamWork(Guid teamWorkId)
    {
        TeamWorkEntity? team=await dbContext.TeamWorks.FindAsync(teamWorkId);
        if (team != null)
        {
            dbContext.TeamWorks.Remove(team);
            await dbContext.SaveChangesAsync();
        } 
    }

   

    public async Task<BaseResponse<List<TeamWorkEntity>>> GetAllTeamWorks()
    {
      //  List<TeamWorkEntity> list = await dbContext.TeamWorks.ToListAsync();
        List<TeamWorkEntity> list = await dbContext.TeamWorks.Include(x=>x.Company).Include(x=>x.Users).ToListAsync();
        
        return new BaseResponse<List<TeamWorkEntity>>(result: list, status: 200, message: "Success");
    }
}