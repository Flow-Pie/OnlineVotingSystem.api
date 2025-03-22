using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineVotingSystem.api.Data;
using OnlineVotingSystem.api.DTOs;
using OnlineVotingSystem.api.DTOs.User;

namespace OnlineVotingSystem.api.Services;

public class JwtService
{
    private readonly IConfiguration _configuration;
    private readonly OnlineVotingSystemContext _dbContext;

    public JwtService(OnlineVotingSystemContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto?> Authenticate(LoginUserDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Identifier) || string.IsNullOrWhiteSpace(request.Password))
            return null;

        var user = request.Identifier.Contains('@')
            ? await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Identifier)
            : await _dbContext.Users.FirstOrDefaultAsync(x => x.NationalId.ToString() == request.Identifier);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            return null;

        var issuer = _configuration["JwtConfig:Issuer"];
        var audience = _configuration["JwtConfig:Audience"];
        var key = Convert.FromBase64String(_configuration["JwtConfig:Key"]!);
        var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("nationalId", user.NationalId.ToString()),
                new Claim("isAdmin", user.IsAdmin.ToString().ToLower())
            ]),
            Expires = tokenExpiryTimeStamp,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature),
        };

        var userDetails = new UserDetailsDto
        (
            user.Id,
            user.Name,
            user.Email,
            user.NationalId,
            user.IsAdmin,
            user.CreatedAt
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);

        return new LoginResponseDto
        (
            userDetails,
            accessToken,
            (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
        );
    }
}