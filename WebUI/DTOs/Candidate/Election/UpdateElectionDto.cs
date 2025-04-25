using System.ComponentModel.DataAnnotations;

namespace WebUI.DTOs.Election;

public record UpdateElectionDto(
    [StringLength(255)] string? Title,
    [StringLength(255)] string Description,
    DateTime? StartTime,
    DateTime? EndTime
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