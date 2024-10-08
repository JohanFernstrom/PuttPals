using Microsoft.AspNetCore.Components;
using PuttPals.Data.Models;
using PuttPals.Services;

namespace PuttPals.Components.Pages
{
    public partial class PlayerDetail : ComponentBase
    {
        [Parameter]
        public required string UserName { get; set; }

        public Player Player { get; set; }

        protected override void OnInitialized()
        {
            var players = MockPuttPalsDataService.Players;

            if(players != null && !players.Any())
            {
                Player = players.Single(e => e.Username == UserName);
            }
            else
            {
                Player = new Player
                {
                    Username = "MISSINGNO",
                    ProfilePictureUrl = "WUTDAFUCK",
                    IdentityUserId = "MISSINGUSERID",
                    IdentityUser = new Data.ApplicationUser()
                };
            }
        }
    }
}
