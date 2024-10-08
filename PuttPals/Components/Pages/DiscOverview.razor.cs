using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PuttPals.Data;
using PuttPals.Data.Models;
using System.Text.Json;

namespace PuttPals.Components.Pages
{
    public partial class DiscOverview
    {
        public List<Disc> Discs { get; set; } = new List<Disc>();
        public List<Disc> allDiscs { get; set; } = new List<Disc>();
        public Disc newDisc { get; set; } = new Disc();
        public int? selectedDiscId { get; set; }
        public bool showForm { get; set; } = false;

        [Inject]
        private ApplicationDbContext _context { get; set; } = default!;

        [Inject]
        private UserManager<ApplicationUser> UserManager { get; set; } = default!;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; } = default!;

        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadDiscsAsync();
        }

        private async Task LoadDiscsAsync()
        {
            var authState = await authenticationStateTask;
            var user = await UserManager.GetUserAsync(authState.User);
            if (user != null)
            {
                var player = await _context.Players
                    .Include(p => p.PlayerDiscs)
                        .ThenInclude(pd => pd.Disc)
                    .FirstOrDefaultAsync(p => p.IdentityUserId == user.Id);

                if (player != null)
                {
                    Discs = player.PlayerDiscs.Select(pd => pd.Disc).ToList();
                }

                allDiscs = await _context.Discs.ToListAsync();
            }
        }

        private async void OpenAddDiscModal()
        {
            showForm = false;
            selectedDiscId = null;
            newDisc = new Disc();
            await JSRuntime.InvokeVoidAsync("bootstrapInterop.showModal", "#addDiscModal");
        }

        private void ShowForm()
        {
            showForm = true;
        }

        private async Task AddSelectedDisc()
        {
            var authState = await authenticationStateTask;
            var user = await UserManager.GetUserAsync(authState.User);

            var player = await _context.Players
                .Include(p => p.PlayerDiscs)
                .FirstOrDefaultAsync(p => p.IdentityUserId == user.Id);

            if (player != null && selectedDiscId.HasValue)
            {
                var existingDisc = allDiscs.FirstOrDefault(d => d.Id == selectedDiscId.Value);
                if (existingDisc != null)
                {
                    var playerDisc = player.PlayerDiscs.FirstOrDefault(pd => pd.DiscId == existingDisc.Id);
                    if (playerDisc != null)
                    {
                        // Disc already exists for the player, update the count or handle it as needed
                        playerDisc.Count++;
                        _context.PlayerDiscs.Update(playerDisc);
                    }
                    else
                    {
                        // Disc does not exist for the player, add a new entry
                        player.PlayerDiscs.Add(new PlayerDisc { PlayerId = player.Id, DiscId = existingDisc.Id, Count = 1 });
                    }

                    await _context.SaveChangesAsync();
                    Discs = player.PlayerDiscs.Select(pd => pd.Disc).ToList();
                }
            }

            await JSRuntime.InvokeVoidAsync("bootstrapInterop.hideModal", "#addDiscModal");
        }

        private async Task AddDiscAsync()
        {
            var authState = await authenticationStateTask;
            var user = await UserManager.GetUserAsync(authState.User);
            var player = await _context.Players
                .Include(p => p.PlayerDiscs)
                .FirstOrDefaultAsync(p => p.IdentityUserId == user.Id);

            if (player != null)
            {
                if (string.IsNullOrEmpty(newDisc.ImageUrl))
                {
                    newDisc.ImageUrl = "default-image-url.jpg";
                }

                _context.Discs.Add(newDisc);
                player.PlayerDiscs.Add(new PlayerDisc { PlayerId = player.Id, DiscId = newDisc.Id, Count = 1 });
                await _context.SaveChangesAsync();
                Discs = player.PlayerDiscs.Select(pd => pd.Disc).ToList();
                newDisc = new Disc(); // Reset the form
            }

            await JSRuntime.InvokeVoidAsync("bootstrapInterop.hideModal", "#addDiscModal");
        }

        private async Task IncrementDiscCount(int discId)
        {
            var authState = await authenticationStateTask;
            var user = await UserManager.GetUserAsync(authState.User);

            var player = await _context.Players
                .Include(p => p.PlayerDiscs)
                .FirstOrDefaultAsync(p => p.IdentityUserId == user.Id);

            if (player != null)
            {
                var playerDisc = player.PlayerDiscs.FirstOrDefault(pd => pd.DiscId == discId);
                if (playerDisc != null)
                {
                    playerDisc.Count++;
                    _context.PlayerDiscs.Update(playerDisc);
                    await _context.SaveChangesAsync();
                    Discs = player.PlayerDiscs.Select(pd => pd.Disc).ToList();
                }
            }
        }

        private async Task DecrementDiscCount(int discId)
        {
            var authState = await authenticationStateTask;
            var user = await UserManager.GetUserAsync(authState.User);

            var player = await _context.Players
                .Include(p => p.PlayerDiscs)
                .FirstOrDefaultAsync(p => p.IdentityUserId == user.Id);

            if (player != null)
            {
                var playerDisc = player.PlayerDiscs.FirstOrDefault(pd => pd.DiscId == discId);
                if (playerDisc != null)
                {
                    playerDisc.Count--;
                    if (playerDisc.Count <= 0)
                    {
                        _context.PlayerDiscs.Remove(playerDisc);
                    }
                    else
                    {
                        _context.PlayerDiscs.Update(playerDisc);
                    }
                    await _context.SaveChangesAsync();
                    Discs = player.PlayerDiscs.Select(pd => pd.Disc).ToList();
                }
            }
        }

        private async Task RemoveDisc(int discId)
        {
            var authState = await authenticationStateTask;
            var user = await UserManager.GetUserAsync(authState.User);

            var player = await _context.Players
                .Include(p => p.PlayerDiscs)
                .FirstOrDefaultAsync(p => p.IdentityUserId == user.Id);

            if (player != null)
            {
                var playerDisc = player.PlayerDiscs.FirstOrDefault(pd => pd.DiscId == discId);
                if (playerDisc != null)
                {
                    _context.PlayerDiscs.Remove(playerDisc);
                    await _context.SaveChangesAsync();
                    Discs = player.PlayerDiscs.Select(pd => pd.Disc).ToList();
                }
            }
        }
    }
}
