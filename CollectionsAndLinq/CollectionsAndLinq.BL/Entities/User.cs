using Newtonsoft.Json;

namespace CollectionsAndLinq.BL.Entities;

public record User(
    [JsonProperty("id")] int Id,
    [JsonProperty("teamId")] int? TeamId,
    [JsonProperty("firstName")] string FirstName,
    [JsonProperty("lastName")] string LastName,
    [JsonProperty("email")] string Email,
    [JsonProperty("registeredAt")] DateTime RegisteredAt,
    [JsonProperty("birthDay")] DateTime BirthDay)
{

}
