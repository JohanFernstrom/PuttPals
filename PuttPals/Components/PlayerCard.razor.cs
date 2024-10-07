using Microsoft.AspNetCore.Components;
using PuttPals.Data.Models;

namespace PuttPals.Components
{
    public partial class PlayerCard
    {
        [Parameter]
        public Player Player { get; set; } = default!;
    }
}
