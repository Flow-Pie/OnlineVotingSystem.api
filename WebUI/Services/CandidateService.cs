using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.api.DTOs.Candidate;

public class CandidatesService : ICandidatesService, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;

    public CandidatesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<IEnumerable<CandidateSerializedDto>> GetSerializedCandidatesAsync()
    {
        var response = await _httpClient.GetAsync("/candidates");
        return await HandleResponseAsync<IEnumerable<CandidateSerializedDto>>(response);
    }

    public async Task<CandidateDetailsDto> GetCandidateDetailsAsync(Guid candidateId)
    {
        var response = await _httpClient.GetAsync($"/candidates/{candidateId}");
        return await HandleResponseAsync<CandidateDetailsDto>(response);
    }

    public async Task<CandidateDetailsDto> CreateCandidateAsync(CreateCandidateDto candidateDto)
    {
        var response = await _httpClient.PostAsJsonAsync("/candidates", candidateDto);
        return await HandleResponseAsync<CandidateDetailsDto>(response);
    }

    public async Task<CandidateDetailsDto> UpdateCandidateAsync(Guid candidateId, UpdateCandidateDto updateDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"/candidates/{candidateId}", updateDto);
        return await HandleResponseAsync<CandidateDetailsDto>(response);
    }

    public async Task DeleteCandidateAsync(Guid candidateId)
    {
        var response = await _httpClient.DeleteAsync($"/candidates/{candidateId}");
        await HandleResponseAsync<object>(response);
    }

    public async Task<IEnumerable<CandidateSerializedDto>> GetCandidatesByPositionAsync(Guid electionPositionId)
    {
        var response = await _httpClient.GetAsync($"/candidates/position/{electionPositionId}");
        return await HandleResponseAsync<IEnumerable<CandidateSerializedDto>>(response);
    }

    public async Task<IEnumerable<CandidateDetailsDto>> GetCandidatesByUserAsync(Guid userId)
    {
        var response = await _httpClient.GetAsync($"/candidates/user/{userId}");
        return await HandleResponseAsync<IEnumerable<CandidateDetailsDto>>(response);
    }

    private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>(_serializerOptions);
        }

        var content = await response.Content.ReadAsStringAsync();
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, _serializerOptions)
            ?? new ProblemDetails
            {
                Title = "API Error",
                Status = (int)response.StatusCode,
                Detail = response.ReasonPhrase
            };

        throw new ApiException(problemDetails);
    }

    public void Dispose() => _httpClient?.Dispose();
}

// Custom Exception for API Errors
public class ApiException : Exception
{
    public ProblemDetails ProblemDetails { get; }

    public ApiException(ProblemDetails problemDetails)
        : base(problemDetails.Detail)
    {
        ProblemDetails = problemDetails;
    }
}