using MudBlazor;

namespace MyPortfolio.Models
{

    public record Project
    {
        public required int Id { get; init; }
        public required string Title { get; init; }
        public required string Description { get; init; }
        public required (string Name, string Description, string Icon)[] Technologies { get; init; }
        public required (string Name, string Description, string Icon)[] Features { get; init; }
        public required string[] Images { get; init; }
        public string? GitHubUrl { get; init; }
        public string? DemoUrl { get; init; }
        public required string ImageUrl { get; init; }
    }

    
}
