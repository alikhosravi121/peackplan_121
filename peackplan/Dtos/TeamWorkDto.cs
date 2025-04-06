namespace peackplan.Dtos;

public class TeamWorkCreateDto
{
        public required string Title { get; set; }
    
    public Guid?  CompanyId { get; set; } 
     
    
    public IEnumerable<Guid>? Users { get; set; }
}