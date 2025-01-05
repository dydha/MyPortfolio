using Microsoft.AspNetCore.Components;
using MyPortfolio.Models;
using MyPortfolio.Services;

namespace MyPortfolio.Pages
{
    public partial class ProjectDetail : ComponentBase
    {
        [Parameter] public int Id { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;
        private Project? _project;
        private bool _isLoading;
        protected override async Task OnParametersSetAsync()
        {
            await GetProject();
        }
        private Task GetProject()
        {  
            _isLoading = true;
            _project = ProjectService.Get(Id);
            _isLoading = false;
            return Task.CompletedTask;
        }
        private void GoToProject()
        {
            if (string.IsNullOrEmpty(_project?.DemoUrl)) return;

            _navigationManager.NavigateTo(_project.DemoUrl);
        }
    }
}