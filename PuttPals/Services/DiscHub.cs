using Microsoft.AspNetCore.SignalR;
using PuttPals.Data.Models;

namespace PuttPals.Services
{
    public class DiscHub : Hub
    {
        public async Task UpdateDiscsCount(int discId, int newCount)
        {
            await Clients.All.SendAsync("UpdateDiscCount", discId, newCount);
        }

        public async Task AddDiscsToPlayer(int discId)
        {
            await Clients.All.SendAsync("AddListOfDiscsToPlayer", discId);
        }

        public async Task RemoveDiscsFromPlayer(int discId)
        {
            await Clients.All.SendAsync("RemoveDiscFromPlayer", discId);
        }
    }
}
