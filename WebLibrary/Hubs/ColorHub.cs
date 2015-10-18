using System.Threading.Tasks;
using Domain.Services;
using Microsoft.AspNet.SignalR;

namespace WebLibrary.Hubs
{
    public class ColorHub : Hub
    {
        private readonly IColorService _colorService;
        public ColorHub(IColorService colorService)
        {
            _colorService = colorService;
        }

        // Client scripts call this method with server.registerVote()
        public void RegisterVote(string hexValue)
        {
            _colorService.RegisterVote(hexValue);
            // Push new vote tallies to all connected clients
            Clients.All.updateVotes(_colorService.GetVotes());
        }

        // Method runs once a client has connected to the hub
        public override Task OnConnected()
        {
            // Call the update votes method on the client who established the connection
            Clients.Caller.updateVotes(_colorService.GetVotes());
            return base.OnConnected();
        }
    }
}