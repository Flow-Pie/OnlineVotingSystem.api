using System;

namespace WebUI.DTOs.ElectionPosition;

public record ElectionPositionSerialized(
    Guid Id,
    string Election,
    string Position,
    Guid PositionId
);