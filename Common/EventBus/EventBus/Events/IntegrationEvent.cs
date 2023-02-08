using System.Text.Json.Serialization;
using EventBus.Abstractions;

namespace EventBus.Events;

public record IntegrationEvent
{
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    [JsonConstructor]
    public IntegrationEvent(Guid id, DateTime createdDate)
    {
        Id = id;
        CreationDate = createdDate;
    }
    
    [JsonInclude]
    public Guid Id { get; private init; }
    
    [JsonInclude]
    public DateTime CreationDate { get; private init; }
}