using Microsoft.AspNetCore.Components;
using PuttPals.Data.Models;
using PuttPals.Services;

namespace PuttPals.Components.Pages
{
    public partial class PlayerDetail : ComponentBase
    {
        [Parameter]
        public string UserName { get; set; }

        public Player Player { get; set; } = new Player();

        protected override void OnInitialized()
        {
            Player = MockPuttPalsDataService.Players.Single(e => e.Username == UserName);
        }
    }
}
