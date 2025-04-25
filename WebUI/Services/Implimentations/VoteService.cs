using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc; // For ProblemDetails
using WebUI.DTOs.Vote;

namespace WebUI.Services
{
    public class VoteService : IVoteService
    {
        private readonly HttpClient httpClient;

        public VoteService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<VoteSubmissionResult> SubmitVoteAsync(CreateVoteDto voteDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/votes", voteDto);

                if (response.IsSuccessStatusCode)
                {
                    var vote = await response.Content.ReadFromJsonAsync<VoteSerializedDto>();
                    return new VoteSubmissionResult { IsSuccess = true, Vote = vote };
                }
                else
                {
                    // Read the error response as a string.
                    var errorContent = await response.Content.ReadAsStringAsync();
                    // Extract a cleaner error message.
                    var friendlyError = ExtractUserFriendlyError(errorContent);
                    return new VoteSubmissionResult { IsSuccess = false, ErrorMessage = friendlyError };
                }
            }
            catch (Exception ex)
            {
                // In case of an exception, return only the exception's message.
                return new VoteSubmissionResult { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }

        private string ExtractUserFriendlyError(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
                return "Unknown error occurred.";

            var firstLine = errorMessage.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)[0];
            return firstLine;
        }
    }
}
