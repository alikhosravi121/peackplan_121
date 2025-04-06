using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using peackplan.Entities;

namespace peackplan.Services;

public interface ICompanyService
{
    Task<CompanyEntity> CreateCompany(CompanyEntity company);
    Task<CompanyEntity?> UpdateCompany(CompanyEntity company);
    Task DeleteCompany(Guid companyId);
    
    Task<List<CompanyEntity>> GetCompanies();
}

public class CompanyService(AppDbContext dbContext) : ICompanyService
{
    public async Task<CompanyEntity> CreateCompany(CompanyEntity company)
    {
        EntityEntry<CompanyEntity> entity = dbContext.Companies.Add(company);
        await dbContext.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<CompanyEntity?> UpdateCompany(CompanyEntity param)
    {
        CompanyEntity? company=await dbContext.Companies.FindAsync(param.Id);
        if (company == null) return null;
        if(param.Title!=null)company.Title=param.Title; 
        dbContext.Companies.Update(company);
        await dbContext.SaveChangesAsync();
        return company;
    }

    public async Task DeleteCompany(Guid companyId)
    {
        CompanyEntity? company=await dbContext.Companies.FindAsync(companyId);
        if (company != null)
        {
            dbContext.Companies.Remove(company);
            await dbContext.SaveChangesAsync();
        } 
    } 
    public async Task<List<CompanyEntity>> GetCompanies()
    {
        List<CompanyEntity> list = await dbContext.Companies.ToListAsync();
        return list;
    }
}