

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using peackplan.Dtos;
using peackplan.Entities;

namespace peackplan.Services;

public interface IAuthService
{
    Task<LoginResponse?> Login(LoginParam loginParam);
    Task<UserResponse?> Register(UserCreateParams user);
}

public class AuthService(AppDbContext dbContext,IUserService userService) : IAuthService
{
    public async Task<LoginResponse?> Login(LoginParam loginParam)
    {
         UserEntity? e=await dbContext.Users.FirstOrDefaultAsync
             (x=>x.Email == loginParam.Email
                 &&x.Password == loginParam.Password);
             if (e == null)
             {
                 return null;
             }

             string token = CreateToken(e);

             return new LoginResponse
             {
                 Token = token,
             };

    }

    public async Task<UserResponse?> Register(UserCreateParams user)
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

    private string CreateToken(UserEntity userEntity)
    {
        var key = Encoding.UTF8.GetBytes("super_secret_key_1121121121124231231231234564512345"); // حداقل 16 کاراکتر

        var claims = new[]
        {
            new Claim(ClaimTypes.Name,userEntity.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier,userEntity.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, userEntity.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, userEntity.Email),
            new Claim("phone_number", userEntity.PhoneNumber),
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "peackplanpeackplanpeackplanpeackplanpeackplanali1211111,12341234123412341123",
            audience: "peackplanpeackplanpeackplanpeackplanali1211111,2341234123412341234",
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(60),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

