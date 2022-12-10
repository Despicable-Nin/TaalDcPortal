using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertProject;

public class UpsertProjectCommand : IRequest<string>
{
    
    [DataMember] 
    public string Name { get; set; }

    [DataMember] 
    public double LandArea { get; set; }
    
    [DataMember]
    public string Id { get; set; }
}