using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<string>
{
    public string Id { get; set; }
    public string Name { get; set; }
}