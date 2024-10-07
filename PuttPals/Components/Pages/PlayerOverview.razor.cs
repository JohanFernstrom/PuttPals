using PuttPals.Services;
using PuttPals.Data.Models;

namespace PuttPals.Components.Pages
{
    public partial class PlayerOverview
    {
        public List<Player> Players { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            Players = MockPuttPalsDataService.Players;
        }
    }
}
