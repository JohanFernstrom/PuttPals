using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PuttPals.Data;
using PuttPals.Data.Models;

namespace PuttPals.Components.Pages
{
    public class DiscDetailBase : ComponentBase
    {
        [Parameter]
        public int Identifier { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ApplicationDbContext DbContext { get; set; }

        protected Disc disc = new Disc(); // Initialize to avoid null reference
        protected int discCount; // Property to hold the count of the disc

        protected override async Task OnInitializedAsync()
        {
            var fetchedDisc = await DbContext.Discs
                .Include(d => d.PlayerDiscs)
                    .ThenInclude(pd => pd.Player)
                .FirstOrDefaultAsync(d => d.Id == Identifier);

            if (fetchedDisc != null)
            {
                disc = fetchedDisc;

                // Fetch the count of the disc for the current player
                var playerDisc = fetchedDisc.PlayerDiscs.FirstOrDefault(pd => pd.DiscId == Identifier);
                if (playerDisc != null)
                {
                    discCount = playerDisc.Count;
                }
            }
        }

        protected void GoBack()
        {
            NavigationManager.NavigateTo("/discoverview");
        }
    }
}
