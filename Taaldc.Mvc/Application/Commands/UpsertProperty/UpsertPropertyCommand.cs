using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpdateProject;

public class UpsertPropertyCommand : IRequest<string>
{
    [DataMember] public string Id { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public double LandArea { get; set; }
}