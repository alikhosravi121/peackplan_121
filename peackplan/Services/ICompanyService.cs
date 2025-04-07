using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using peackplan.Dtos;
using peackplan.Entities;

namespace peackplan.Services;

public interface ICompanyService
{
    Task<BaseResponse<CompanyEntity>> CreateCompany(CompanyCreateParam param);
    Task<BaseResponse<CompanyEntity?>> UpdateCompany(CompanyEntity company);
    Task DeleteCompany(Guid companyId);
    
    Task<BaseResponse<List<CompanyEntity>>> GetCompanies();
}

public class CompanyService(AppDbContext dbContext) : ICompanyService
{
    public async Task<BaseResponse<CompanyEntity>> CreateCompany(CompanyCreateParam param)
    {
        CompanyEntity companyEntity = new()
        {
            Id =  Guid.NewGuid(),
            Title = param.Title,
            
        };
        EntityEntry<CompanyEntity> entity = dbContext.Companies.Add(companyEntity);
        await dbContext.SaveChangesAsync();

       return new BaseResponse<CompanyEntity>(result: companyEntity, status: 200, message: "Success");
    }

    public async Task<BaseResponse<CompanyEntity?>> UpdateCompany(CompanyEntity param)
    {
        CompanyEntity? company=await dbContext.Companies.FindAsync(param.Id);
        if (company == null) return new BaseResponse<CompanyEntity>(result:null, status:404,message:"Company not found");
        if(param.Title!=null)company.Title=param.Title; 
        dbContext.Companies.Update(company);
        await dbContext.SaveChangesAsync();
        return new BaseResponse<CompanyEntity?>(result: company, status: 200, message: "Success");
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
    public async Task<BaseResponse<List<CompanyEntity>>> GetCompanies()
    {
        List<CompanyEntity> list = await dbContext.Companies.Include(x=>x.TeamWorks).ToListAsync(); 
        return new BaseResponse<List<CompanyEntity>>(result: list, status: 200, message: "Success");
    }
}