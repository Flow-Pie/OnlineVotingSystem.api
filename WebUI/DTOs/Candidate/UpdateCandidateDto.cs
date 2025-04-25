using System.ComponentModel.DataAnnotations;
using System;


namespace WebUI.DTOs.Candidate;

public record UpdateCandidateDto(
    Guid? PositionId, //FIX:  BUG UPDATE DTO TO USE POSITIONID
     [StringLength(100)] string? Party,
    [StringLength(255)] string Bio,
    [StringLength(255)] Uri? PhotoUrl
);