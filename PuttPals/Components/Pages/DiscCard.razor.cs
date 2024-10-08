using Microsoft.AspNetCore.Components;
using PuttPals.Data.Models;

namespace PuttPals.Components.Pages
{
    public partial class DiscCard
    {
        [Parameter]
        public Disc Disc { get; set; } = default!;
    }
}
