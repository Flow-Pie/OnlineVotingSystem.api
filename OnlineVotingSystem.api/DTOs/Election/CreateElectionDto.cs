using System.ComponentModel.DataAnnotations;

namespace OnlineVotingSystem.api.DTOs.Election;

public record CreateElectionDto(
    [StringLength(255)] string Title,
    [StringLength(255)] string Description,
    DateTime StartTime,
    DateTime EndTime
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "End time must be greater than start time.",
                [nameof(EndTime)]
            );
        }
    }
};