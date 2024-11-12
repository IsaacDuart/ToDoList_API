using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Application.Config;
using ToDoList.Application.DTO;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exception;
using ToDoList.Domain.Interfaces.RepositoryPatternEntities;

namespace ToDoList.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    
    
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(KeyConfig.Secret);
      

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            }),
            Expires = DateTime.UtcNow.AddHours(1),

            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public async Task<AuthenticateDTO> Authenticate(LoginDTO loginDto)
    {
        var loginExist = await _userRepository.GetByEmail(loginDto.Email);

        if (loginExist == null)
        {
            throw new DomainException("User not found");
        }

        var passwordValidation =
            _passwordHasher.VerifyHashedPassword(loginExist, loginExist.Password, loginDto.Password);
        if (passwordValidation == PasswordVerificationResult.Failed)
        {
            throw new DomainException("Invalid credentials");
        }

        return new AuthenticateDTO
        {
            Id = loginExist.Id,
            Email = loginExist.Email,
            Name = loginExist.Name,
            Token = GenerateToken(loginExist)
        };

    }
}