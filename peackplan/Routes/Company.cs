using peackplan.Dtos;
using peackplan.Entities;
using peackplan.Services;

namespace peackplan.Routes;

public static class Company
{
    public static void MapCompanyRoutes(this IEndpointRouteBuilder app, string tag)
    {
        var route = app.MapGroup("api/v1/company/");
        route.MapPost("create", async (ICompanyService companyService,CompanyCreateParam  companyEntity) =>
        {
           BaseResponse<CompanyEntity> result = await companyService.CreateCompany(companyEntity);
           return result.Results;
           
           
        }).WithTags(tag);
        
        route.MapGet("Read",async ( ICompanyService companyService) =>
        {
          BaseResponse<List<CompanyEntity>> result=await companyService.GetCompanies();
            return result.Results;
            
           
            
        }).WithTags(tag);
        
        route.MapDelete("Delete{id:guid}",async (ICompanyService companyService,Guid id) =>
        {
            await companyService.DeleteCompany(id); 
            return Results.Ok();
        }).WithTags(tag);

        route.MapPost("Update",async (ICompanyService companyService,CompanyEntity param) =>
        {
           BaseResponse<CompanyEntity?> result=await companyService.UpdateCompany(param);
            return result.Results;
        }).WithTags(tag);
    }
}