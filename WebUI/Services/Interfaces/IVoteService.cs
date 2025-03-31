using OnlineVotingSystem.api.DTOs.Vote;

namespace WebUI.Services
{
    //returns a VoteSubmissionResult instead of VoteSerializedDto.
    public class VoteSubmissionResult
    {
        public bool IsSuccess { get; set; }
        public VoteSerializedDto? Vote { get; set; }
        public string? ErrorMessage { get; set; }
    }
    public interface IVoteService
    {
        Task<VoteSubmissionResult> SubmitVoteAsync(CreateVoteDto voteDto);
    }
}
