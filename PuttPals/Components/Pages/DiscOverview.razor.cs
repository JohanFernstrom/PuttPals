using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PuttPals.Data;
using PuttPals.Data.Models;

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

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private HubConnection? hubConnection;
        private int? discId;
        private int? discCount;

        protected override async Task OnInitializedAsync()
        {
            await LoadDiscsAsync();

            hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/dischub"))
                .Build();

            hubConnection.On<int, int>("UpdateDiscCount", async (discId, discCount) =>
            {
                var foundDisc = Discs.FirstOrDefault(s => s.Id == discId);

                if (foundDisc != null)
                {
                    var foundPlayerDisc = foundDisc.PlayerDiscs.FirstOrDefault(s => s.DiscId == discId);

                    if (foundPlayerDisc != null && discCount > 0)
                    {
                        foundPlayerDisc.Count = discCount;
                        await InvokeAsync(StateHasChanged);
                    }
                }
            });

            hubConnection.On<int>("AddListOfDiscsToPlayer", async (discId) =>
            {
                var existingPlayerDisc = Discs.FirstOrDefault(s => s.Id == discId);

                if (existingPlayerDisc != null)
                {
                    var foundPlayerDisc = existingPlayerDisc.PlayerDiscs.FirstOrDefault(s => s.DiscId == discId);

                    if (foundPlayerDisc != null)
                    {
                        foundPlayerDisc.Count++;
                    }
                }
                else
                {
                    var newPlayerDisc = allDiscs.FirstOrDefault(x => x.Id == discId);

                    if (newPlayerDisc != null)
                    {
                        Discs.Add(newPlayerDisc);
                        var newAddedDisc = Discs.FirstOrDefault(s => s.Id == discId);
                        var player = await GetPlayer();
                        newAddedDisc?.PlayerDiscs.Add(new PlayerDisc() { Count = 1, Disc = newDisc, DiscId = discId, Player = player, PlayerId = player.Id });
                    }
                }

                await InvokeAsync(StateHasChanged);
            });

            hubConnection.On<int>("RemoveDiscFromPlayer", async (discId) =>
            {
                var foundDisc = Discs.FirstOrDefault(s => s.Id == discId);

                if (foundDisc != null)
                {
                    var foundPlayerDisc = foundDisc.PlayerDiscs.FirstOrDefault(s => s.DiscId == discId);
                    if (foundPlayerDisc != null)
                    {
                        foundDisc.PlayerDiscs.Remove(foundPlayerDisc);
                        if (!foundDisc.PlayerDiscs.Any())
                        {
                            Discs.Remove(foundDisc);
                        }
                    }
                }

                Discs = new List<Disc>(Discs);
                await InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();
        }

        private async Task LoadDiscsAsync()
        {
            var player = await GetPlayer();

            if (player != null)
            {
                Discs = player.PlayerDiscs.Select(pd => pd.Disc).ToList();
            }

            allDiscs = await _context.Discs.ToListAsync();
        }

        private async Task<Player> GetPlayer()
        {
            var authState = await authenticationStateTask;
            var user = await UserManager.GetUserAsync(authState.User);

            if (user != null)
            {
                var player = await _context.Players
                    .Include(p => p.PlayerDiscs)
                        .ThenInclude(pd => pd.Disc)
                    .FirstOrDefaultAsync(p => p.IdentityUserId == user.Id);

                return player;
            }

            return null;
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
            var player = await GetPlayer();

            if (player != null && selectedDiscId.HasValue)
            {
                var existingDisc = allDiscs.FirstOrDefault(d => d.Id == selectedDiscId.Value);
                if (existingDisc != null)
                {
                    var playerDisc = player.PlayerDiscs.FirstOrDefault(pd => pd.DiscId == existingDisc.Id);
                    if (playerDisc != null)
                    {
                        _context.PlayerDiscs.Update(playerDisc);
                    }
                    else
                    {
                        // Disc does not exist for the player, add a new entry
                        player.PlayerDiscs.Add(new PlayerDisc { PlayerId = player.Id, DiscId = existingDisc.Id, Count = 1 });
                    }

                    await _context.SaveChangesAsync();
                    Discs = player.PlayerDiscs.Select(pd => pd.Disc).ToList();


                    //DETTA SKA SYNKA KLIENTER
                    await hubConnection.SendAsync("AddDiscsToPlayer", existingDisc.Id);
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

                await hubConnection.SendAsync("UpdateDiscsCount", discId, player.PlayerDiscs.Count);
                StateHasChanged();
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

                    await hubConnection.SendAsync("UpdateDiscsCount", discId, playerDisc.Count);
                    StateHasChanged();
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

                    await hubConnection.SendAsync("UpdateDiscsCount", discId, playerDisc.Count);
                    StateHasChanged();
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

                    await hubConnection.SendAsync("RemoveDiscsFromPlayer", discId);
                    StateHasChanged();
                }
            }
        }
    }
}
