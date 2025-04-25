using System;

using System.ComponentModel.DataAnnotations;

namespace WebUI.DTOs.Position;

public record UpdatePositionDto(
    [StringLength(255)] string Name
);