using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Model;

public class TicketAiService
{
    private readonly HttpClient _httpClient;
    public TicketAiService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<TriageReport> AnalyzeTicketAsync(TriageRequest request) {
        try
        {
            // 1. Send the model as a JSON body in a POST request
            var response = await _httpClient.PostAsJsonAsync("ticket/triage", request);

            // 2. Ensure we got a successful status code (200-299)
            response.EnsureSuccessStatusCode();

            // 3. Automatically parse the returned AI response into your TriageReport model
            return await response.Content.ReadFromJsonAsync<TriageReport>();
        }
        catch (Exception ex) {
            throw ex;
        }
    }
}