using System.ComponentModel.DataAnnotations;

namespace WebUI.Entities;

public class Position
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(100)] public required string Name { get; set; }
}