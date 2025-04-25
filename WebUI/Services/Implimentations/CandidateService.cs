using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebUI.DTOs.Candidate;

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

   public async Task<CandidateDetailsDto> CreateCandidateAsync(CreateCandidateDto candidateDto, string token)
    {
        try
        {
            // Validate input first
            if (candidateDto == null)
            {
                throw new ArgumentNullException(nameof(candidateDto), "Candidate data cannot be null");
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "/candidates")
            {
                Content = JsonContent.Create(candidateDto)
            };

            // Set the Authorization header with the Bearer token
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Send the request
            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Response: {response.StatusCode} - {responseContent}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(responseContent, response.StatusCode);
            }

            // Deserialize response
            var candidate = JsonSerializer.Deserialize<CandidateDetailsDto>(responseContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (candidate == null)
            {
                throw new ApiException("Invalid API response format", HttpStatusCode.InternalServerError);
            }

            return candidate;
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException($"Network error: {ex.Message}", HttpStatusCode.ServiceUnavailable);
        }
        catch (JsonException ex)
        {
            throw new ApiException($"Failed to parse API response: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }




    public async Task<CandidateDetailsDto> UpdateCandidateAsync(Guid candidateId, UpdateCandidateDto updateDto, string token)
    {
        Console.WriteLine($"Updating candidate with ID: {candidateId}...");

        // Create the request
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"/candidates/{candidateId}")
        {
            Content = JsonContent.Create(updateDto)
        };

        // Set the Authorization header with the Bearer token
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Send the request
        var response = await _httpClient.SendAsync(request);

        Console.WriteLine($"Response status: {response.StatusCode}");

        // Handle the response
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
        try
        {
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<T>(_serializerOptions);

            if (result == null)
            {
                throw new ApiException(new ProblemDetails
                {
                    Title = "API Error",
                    Status = (int)HttpStatusCode.NoContent,
                    Detail = "The API returned a null response."
                });
            }

            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException(new ProblemDetails
            {
                Title = "Network Error",
                Status = (int)HttpStatusCode.ServiceUnavailable,
                Detail = ex.Message
            });
        }
        catch (JsonException ex)
        {
            throw new ApiException(new ProblemDetails
            {
                Title = "Deserialization Error",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = $"Failed to deserialize the API response: {ex.Message}"
            });
        }
        catch (Exception ex)
        {
            throw new ApiException(new ProblemDetails
            {
                Title = "Unexpected Error",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = $"An unexpected error occurred: {ex.Message}"
            });

        }
        
        
    }
    public void Dispose() => _httpClient?.Dispose();
  
}

public class ApiException : Exception
{
    public ProblemDetails ProblemDetails { get; }
    public HttpStatusCode StatusCode { get; }

     public ApiException(ProblemDetails problemDetails)
        : base(problemDetails.Detail)
    {
        ProblemDetails = problemDetails;
    }
    public ApiException(ProblemDetails problemDetails, HttpStatusCode statusCode)
        : base(problemDetails.Detail)
    {
        ProblemDetails = problemDetails;
        StatusCode = statusCode;
    }
         public ApiException(string? message, HttpStatusCode statusCode) 
        : base(message)
    {
        ProblemDetails = new ProblemDetails
        {
            Detail = message ?? "An error occurred"
        };
        StatusCode = statusCode;
    }
}

