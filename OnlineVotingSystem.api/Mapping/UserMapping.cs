using OnlineVotingSystem.api.DTOs;
using OnlineVotingSystem.api.Entities;

namespace OnlineVotingSystem.api.Mapping;

public static class UserMapping
{
    public static UserDetailsDto ToUserDetailsDto(this User user)
    {
        return new UserDetailsDto
        (
            user.Id,
            user.Name,
            user.Email,
            user.IsAdmin,
            user.CreatedAt
        );
    }

    public static User ToEntity(this CreateUserDto user)
    {
        return new User
        {
            Name = user.Name,
            NationalId = user.NationalId,
            Email = user.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
        };
    }
}