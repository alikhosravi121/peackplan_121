namespace peackplan.Dtos;

public class UserCreateParams
{
     
     
    public required string Fullname { get; set; }
    
    public required string  PhoneNumber { get; set; }
     
    public string  Email { get; set; }
    public required string Password { get; set; } 
    public DateTime? Birthday { get; set; }
    public bool IsMarried { get; set; } = false;

}public class UserUpdateParams
{
     
     public required Guid UserId { get; set; }
    public  string? Fullname { get; set; } 
    public  string?  PhoneNumber { get; set; } 
    public string?  Email { get; set; }
    public  string? Password { get; set; } 
    public DateTime? Birthday { get; set; }
    public bool? IsMarried { get; set; } 

}
public class UserResponse{ 
    public required Guid UserId { get; set; }
public required string Fullname { get; set; } 
public required string  PhoneNumber { get; set; } 
public  string  Email { get; set; }
public required string Password { get; set; } 
public DateTime? Birthday { get; set; }
public bool IsMarried { get; set; } 
public int? Age { get; set; }

}