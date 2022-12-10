using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.CreateProject;

public class CreateProjectCommand : IRequest<string>
{
    [DataMember] public string Name { get; set; }

    [DataMember] public double LandArea { get; set; }
}