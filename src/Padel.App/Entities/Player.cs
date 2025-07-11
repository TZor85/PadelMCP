using System.Text.Json.Serialization;

namespace Padel.App.Entities;

public class Player
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    [JsonIgnore]
    public bool Deleted { get; set; }
    [JsonIgnore]
    public DateTimeOffset? DeletedAt { get; set; }
    [JsonIgnore]
    public DateTimeOffset CreatedAt { get; set; }
}
