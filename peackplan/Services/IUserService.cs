using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using peackplan.Dtos;
using peackplan.Entities;

namespace peackplan.Services;

public interface IUserService
{
    Task<UserResponse> CreateUser(UserCreateParams user);
    Task<IEnumerable<UserResponse>> GetAllUsers();
    Task<UserResponse?> GetUserById(Guid id);
    Task<UserResponse?> UpdateUser(UserUpdateParams param);
    Task DeleteUser(Guid id);
    
}


public class UserService(AppDbContext dbContext ) : IUserService
{
    public async Task<UserResponse> CreateUser(UserCreateParams user)
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
        return new UserResponse
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
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsers()
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
          Age = 7
      }).ToListAsync();
      return list;
    }

    public async Task<UserResponse?> GetUserById(Guid id)
    {
        UserEntity? user=await dbContext.Users.FindAsync(id);
        if (user == null)
        {
            return null;
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
        return response;
    }

    public async Task<UserResponse?> UpdateUser(UserUpdateParams param)
    {
        UserEntity? user=await dbContext.Users.FindAsync(param.UserId);
        if (user == null) return null;
        if(param.Fullname!=null) user.Fullname = param.Fullname;
        if(param.Password!=null)user.Password = param.Password;
        if(param.PhoneNumber!=null)user.PhoneNumber = param.PhoneNumber;
        if(param.Email!=null)user.Email = param.Email;
        if(param.Birthday!=null)user.Birthday = param.Birthday;
        if(param.IsMarried!=null)user.IsMarried = param.IsMarried.Value;
        
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
        return new UserResponse
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
}