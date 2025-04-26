using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using PokyBack.SignalR.Messages;

namespace PokyBack.SignalR;

public class PokyHub : Hub
{
    private static readonly Dictionary<string, List<string>> HubClients = new();
    private static readonly Lock HubClientsLock = new();

    public async Task SendMessage(WsMessage message)
    {
        await Clients.All.SendAsync("message", message);
        CleanupOnLeaveOrKick(message, Context.ConnectionId);
    }

    private static void CleanupOnLeaveOrKick(WsMessage message, string connectionId)
    {
        switch (message)
        {
            case UserJoinMessage:
            {
                lock (HubClientsLock)
                {
                    if (HubClients.TryGetValue(message.RoomId!, out var roomClients) && roomClients.Contains(message.Uuid!))
                        return;
                
                    if (!string.IsNullOrEmpty(message.RoomId))
                    {
                        if (!HubClients.ContainsKey(message.RoomId))
                            HubClients.Add(message.RoomId, []);
                        
                        HubClients[message.RoomId].Add(connectionId);
                    }
                }

                break;
            }
        }
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
        CleanSpecificConnectionId(Context.ConnectionId);
    }
    
    /// <summary>
    /// Removes the specified connection ID from all rooms it is part of.
    /// If a room becomes empty as a result, it is removed.
    /// </summary>
    /// <param name="connectionId">The unique identifier of the connection to be removed from rooms.</param>
    private static void CleanSpecificConnectionId(string connectionId)
    {
        lock (HubClientsLock)
        {
            var roomsToRemoveFrom =
                (from kvp in HubClients where kvp.Value.Contains(connectionId) select kvp.Key).ToList();
            
            foreach (var roomId in roomsToRemoveFrom)
            {
                if (!HubClients.TryGetValue(roomId, out var clientsInRoom))
                    continue;

                clientsInRoom.Remove(connectionId);

                if (clientsInRoom.Count == 0)
                    HubClients.Remove(roomId);
                
                Console.WriteLine($"Room '{roomId}' removed as it became empty."); // Or use proper logging
            }
        }
    }
}
