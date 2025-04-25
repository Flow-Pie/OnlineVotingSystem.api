using System;

namespace WebUI.DTOs.Vote;
 public class VoteSubmissionResult
    {
        public bool IsSuccess { get; set; }
        public VoteSerializedDto? Vote { get; set; }
        public string? ErrorMessage { get; set; }
    }