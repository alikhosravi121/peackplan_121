using Microsoft.EntityFrameworkCore;
using peackplan.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace peackplan.Services;

public interface ITeamWorkService
{
    Task<TeamWorkEntity> CreateTeamWork(TeamWorkEntity teamWork);
    Task<TeamWorkEntity?> UpdateTeamWork(TeamWorkEntity teamWork);
    Task DeleteTeamWork(Guid teamWorkId);
    Task<List<TeamWorkEntity>> GetAllTeamWorks();
}

public class TeamWorkService(AppDbContext dbContext) : ITeamWorkService
{
    public async Task<TeamWorkEntity> CreateTeamWork(TeamWorkEntity teamWork)
    {
        EntityEntry<TeamWorkEntity> entity = dbContext.TeamWorks.Add(teamWork);
        await dbContext.SaveChangesAsync();

        return entity.Entity;
       
    }

    public async Task<TeamWorkEntity?> UpdateTeamWork(TeamWorkEntity param)
    {
        TeamWorkEntity? team=await dbContext.TeamWorks.FindAsync(param.Id);
        if (team == null) return null;
        if(param.Title!=null)team.Title=param.Title;
        if (param.CompanyId!=null)team.CompanyId=param.CompanyId;
        dbContext.TeamWorks.Update(team);
        await dbContext.SaveChangesAsync();
        return team;
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

   

    public async Task<List<TeamWorkEntity>> GetAllTeamWorks()
    {
        List<TeamWorkEntity> list = await dbContext.TeamWorks.ToListAsync();
        return list;
    }
}