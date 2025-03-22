using OnlineVotingSystem.api.DTOs.User;
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
            user.NationalId,
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
    
    public static void ApplyUpdates(this User user, UpdateUserDto updateUser)
    {
        if (!string.IsNullOrWhiteSpace(updateUser.Name)) user.Name = updateUser.Name!;
        if (!string.IsNullOrWhiteSpace(updateUser.Email)) user.Email = updateUser.Email!;
        if (!string.IsNullOrWhiteSpace(updateUser.Password)) user.Password = updateUser.Password!;
        
    }
}
