using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using peackplan.Dtos;
using peackplan.Entities;

namespace peackplan.Services;

public interface IUserService
{
    Task <BaseResponse<UserResponse?>> CreateUser(UserCreateParams user);
    Task <BaseResponse<IEnumerable<UserResponse?>>> GetAllUsers();
    Task<BaseResponse<UserResponse?>> GetUserById(Guid id);
    Task<BaseResponse<UserResponse?>> UpdateUser(UserUpdateParams param);
    Task DeleteUser(Guid id);

    Task<BaseResponse<UserResponse?>> Profile();

}


public class UserService(AppDbContext dbContext,IHttpContextAccessor httpContext): IUserService
{
    public async Task<BaseResponse<UserResponse?>> CreateUser(UserCreateParams user)
    {
        UserEntity userEntity = new ()
        {
            Id = Guid.CreateVersion7(),
            Fullname = user.Fullname,
            PhoneNumber =user.PhoneNumber,
            Password = user.Password,
            Email = user.Email,
            Birthday = user.Birthday,
            IsMarried = user.IsMarried,
        };
        EntityEntry<UserEntity> entity= dbContext.Users.Add(userEntity);
       await dbContext.SaveChangesAsync();
        int? age = null;
        if (user.Birthday!=null)
        {
            age =DateTime.UtcNow.Year- user.Birthday.Value.Year;
            
        }
        var response= new UserResponse
        {
            UserId = userEntity.Id,
            Fullname = userEntity.Fullname,
            PhoneNumber = userEntity.PhoneNumber,
            Password = userEntity.Password,
            Email = userEntity.Email,
            Birthday = userEntity.Birthday,
            IsMarried = userEntity.IsMarried,
            Age =   age
        };
        return new BaseResponse<UserResponse?>(result: response, status: 200, message: "Success");
    }

    public async Task <BaseResponse<IEnumerable<UserResponse?>>> GetAllUsers()
    {
      List<UserResponse>list=await dbContext.Users.Select(x=>new UserResponse
      {
          UserId = x.Id,
          Fullname = x.Fullname,
          PhoneNumber = x.PhoneNumber,
          Password = x.Password,
          Email = x.Email,
          Birthday = x.Birthday,
          IsMarried = x.IsMarried,
          Age = 7,
          TeamWorks = x.TeamWorks
      }).ToListAsync();
      
      return new BaseResponse<IEnumerable<UserResponse?>>(result: list, status: 200, message: "Success");
    }

    public async Task<BaseResponse<UserResponse?>> GetUserById(Guid id)
    {
        UserEntity? user=await dbContext.Users.FirstOrDefaultAsync(x=>x.Id==id);
        if (user == null)
        {
            return new BaseResponse<UserResponse?>(result: null, status: 404, message: "User not found");
        }

        UserResponse response = new()
        {
            UserId = user.Id,
            Fullname = user.Fullname,
            PhoneNumber = user.PhoneNumber,
            Password = user.Password,
            Email = user.Email,
            Birthday = user.Birthday,
            IsMarried = user.IsMarried,
            Age = 7,

        };
        return new BaseResponse<UserResponse?>(result: response, status: 200, message: "Success");
    }

    public async Task<BaseResponse<UserResponse?>> UpdateUser(UserUpdateParams param)
    {
        UserEntity? user=await dbContext.Users.FindAsync(param.UserId);
        if (user == null)return new BaseResponse<UserResponse?>(result: null, status: 404, message: "User not found");
        if(param.Fullname!=null) user.Fullname = param.Fullname;
        if(param.Password!=null)user.Password = param.Password;
        if(param.PhoneNumber!=null)user.PhoneNumber = param.PhoneNumber;
        if(param.Email!=null)user.Email = param.Email;
        if(param.Birthday!=null)user.Birthday = param.Birthday;
        if(param.IsMarried!=null)user.IsMarried = param.IsMarried.Value;
        
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
        var response= new UserResponse
        {
            UserId = user.Id,
            Fullname = user.Fullname,
            PhoneNumber = user.PhoneNumber,
            Password = user.Password,
            Email = user.Email,
            Birthday = user.Birthday,
            IsMarried = user.IsMarried,
            Age = 7
        };
        return new BaseResponse<UserResponse?>(result: response, status: 200, message: "Success");
    }

    public async Task DeleteUser(Guid id)
    {
        UserEntity? user=await dbContext.Users.FindAsync(id);
        if (user != null)
        {
            dbContext.Users.Remove(user);
           await dbContext.SaveChangesAsync();
        } 
     
    }

    public async Task<BaseResponse<UserResponse?>>? Profile()
    {
        string? userId=httpContext.HttpContext.User.Identity.Name;
        if (userId!=null)
        {
            return new BaseResponse<UserResponse?>(result: null, status: 404, message: "User not found");
        }
        BaseResponse<UserResponse?> userResponse=await GetUserById(Guid.Parse(userId));
        if (userResponse == null)return new BaseResponse<UserResponse?>(result: null, status: 404, message: "User not found");
       
        return new BaseResponse<UserResponse?>(result: userResponse.Results, status: 200, message: "Success");
    }
}