using Newtonsoft.Json;

namespace CollectionsAndLinq.BL.Entities;

public record Task(
    [JsonProperty("id")] int Id,
    [JsonProperty("projectId")] int ProjectId,
    [JsonProperty("performerId")] int PerformerId,
    [JsonProperty("name")] string Name,
    [JsonProperty("description")] string Description,
    [JsonProperty("state")] TaskState State,
    [JsonProperty("createdAt")] DateTime CreatedAt,
    [JsonProperty("finishedAt")] DateTime? FinishedAt)
{

}
