using Newtonsoft.Json;

namespace CollectionsAndLinq.BL.Entities;

public record Project(
   [JsonProperty("id")] int Id,
   [JsonProperty("authorId")] int AuthorId,
   [JsonProperty("teamId")] int TeamId,
   [JsonProperty("name")] string Name,
   [JsonProperty("description")] string Description,
   [JsonProperty("createdAt")] DateTime CreatedAt,
   [JsonProperty("deadline")] DateTime Deadline)
{

}
