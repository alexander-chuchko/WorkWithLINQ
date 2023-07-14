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

/*
public class Project
{
    public int ID { get; }
    public int AuthorId { get; }
    public int TeamId { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime CreatedAt { get; }
    public DateTime Deadline { get; }

    public Project(int id, int authorId, int teamId, string name, string description, DateTime createdAt, DateTime deadline)
    {
        ID = id;
        AuthorId = authorId;
        TeamId = teamId;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        Deadline = deadline;
    }
}*/