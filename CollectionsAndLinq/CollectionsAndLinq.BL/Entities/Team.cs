using Newtonsoft.Json;

namespace CollectionsAndLinq.BL.Entities;

public record Team(
    [JsonProperty("id")] int Id,
    [JsonProperty("name")] string Name,
    [JsonProperty("createdAt")] DateTime CreatedAt)
{

}

