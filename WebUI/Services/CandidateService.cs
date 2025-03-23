using System.Net;
using System.Net.Http.Headers;
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

    // Method to set the bearer token
    public void SetBearerToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new ArgumentNullException(nameof(token), "Bearer token cannot be null or empty.");
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<IEnumerable<CandidateSerializedDto>> GetSerializedCandidatesAsync()
    {
        Console.WriteLine("Fetching serialized candidates...");
        var response = await _httpClient.GetAsync("/candidates");
        Console.WriteLine($"Response status: {response.StatusCode}");
        return await HandleResponseAsync<IEnumerable<CandidateSerializedDto>>(response);
    }

    public async Task<CandidateDetailsDto> GetCandidateDetailsAsync(Guid candidateId)
    {
        Console.WriteLine($"Fetching candidate details for ID: {candidateId}...");
        var response = await _httpClient.GetAsync($"/candidates/{candidateId}");
        Console.WriteLine($"Response status: {response.StatusCode}");
        return await HandleResponseAsync<CandidateDetailsDto>(response);
    }

    public async Task<CandidateDetailsDto> CreateCandidateAsync(CreateCandidateDto candidateDto)
    {
        Console.WriteLine("Creating a new candidate...");
        var response = await _httpClient.PostAsJsonAsync("/candidates", candidateDto);
        Console.WriteLine($"Response status: {response.StatusCode}");
        return await HandleResponseAsync<CandidateDetailsDto>(response);
    }

    public async Task<CandidateDetailsDto> UpdateCandidateAsync(Guid candidateId, UpdateCandidateDto updateDto)
    {
        Console.WriteLine($"Updating candidate with ID: {candidateId}...");
        var response = await _httpClient.PutAsJsonAsync($"/candidates/{candidateId}", updateDto);
        Console.WriteLine($"Response status: {response.StatusCode}");
        return await HandleResponseAsync<CandidateDetailsDto>(response);
    }

    public async Task DeleteCandidateAsync(Guid candidateId)
    {
        Console.WriteLine($"Deleting candidate with ID: {candidateId}...");
        var response = await _httpClient.DeleteAsync($"/candidates/{candidateId}");
        Console.WriteLine($"Response status: {response.StatusCode}");
        await HandleResponseAsync<object>(response);
    }

    public async Task<IEnumerable<CandidateSerializedDto>> GetCandidatesByPositionAsync(Guid electionPositionId)
    {
        Console.WriteLine($"Fetching candidates for position ID: {electionPositionId}...");
        var response = await _httpClient.GetAsync($"/candidates/position/{electionPositionId}");
        Console.WriteLine($"Response status: {response.StatusCode}");
        return await HandleResponseAsync<IEnumerable<CandidateSerializedDto>>(response);
    }

    public async Task<IEnumerable<CandidateDetailsDto>> GetCandidatesByUserAsync(Guid userId)
    {
        Console.WriteLine($"Fetching candidates for user ID: {userId}...");
        var response = await _httpClient.GetAsync($"/candidates/user/{userId}");
        Console.WriteLine($"Response status: {response.StatusCode}");
        return await HandleResponseAsync<IEnumerable<CandidateDetailsDto>>(response);
    }

    private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>(_serializerOptions)!;
        }

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"API Error: {content}");

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
