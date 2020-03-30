
using Microsoft.AspNetCore.SignalR;
using SignalR.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _dbContext;
        private static ConcurrentDictionary<string, string> CurrentUsers = new ConcurrentDictionary<string, string>();
        private static ConcurrentDictionary<string, ChatRoom> chatRooms = new ConcurrentDictionary<string, ChatRoom>();


        public ChatHub(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task SendMessage(string roomName, string userName, string textMessage)
        {
            if (!CurrentUsers.ContainsKey(Context.ConnectionId))
            {
                CurrentUsers.TryAdd(Context.ConnectionId, userName);
            }
            var message = new ChatMessage
            {
                UserName = userName,
                Message = textMessage,
                TimeStamp = DateTimeOffset.Now
            };

            //save message to datbase
            _dbContext.ChatMessages.Add(message);
            var dbTask = _dbContext.SaveChangesAsync();


            await Clients.Group(roomName.ToLower()).SendAsync("ReceiveMessage", message.UserName, message.TimeStamp, message.Message);

            await dbTask;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "Chat Hub", DateTimeOffset.UtcNow, "Welcome to Chat Hub!");


            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            string userLeftName = "Unknown User";
            CurrentUsers.TryRemove(Context.ConnectionId, out userLeftName);
            await Clients.All.SendAsync("ReceiveMessage", "Chat Hub", DateTimeOffset.UtcNow, $"{userLeftName} left the conversation");

            await base.OnDisconnectedAsync(ex);
        }

        public async Task JoinRoom(string roomName)
        {
            roomName = roomName.ToLower();
            string currentConnectionId = Context.ConnectionId;
            if (!chatRooms.ContainsKey(roomName))
            {
                ChatRoom newRoom = new ChatRoom()
                {
                    RoomName = roomName,
                    ConnectionIds = new List<string>()
                };
                newRoom.ConnectionIds.Add(currentConnectionId);
                chatRooms.TryAdd(roomName, newRoom);
            }
            else
            {
                ChatRoom existingRoom = new ChatRoom();
                chatRooms.TryGetValue(roomName, out existingRoom);
                existingRoom.ConnectionIds.Add(currentConnectionId);
                chatRooms.TryAdd(roomName, existingRoom);
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

            await Clients.Caller.SendAsync("ReceiveMessage", "Chat Hub", DateTimeOffset.UtcNow, $"You joined room: {roomName}!");


        }
        public async Task SendAllMessage(string userName, string textMessage)
        {
            if (!CurrentUsers.ContainsKey(Context.ConnectionId))
            {
                CurrentUsers.TryAdd(Context.ConnectionId, userName);
            }
            var message = new ChatMessage
            {
                UserName = userName,
                Message = textMessage,
                TimeStamp = DateTimeOffset.Now
            };

            //save message to datbase
            _dbContext.ChatMessages.Add(message);
            var dbTask = _dbContext.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", message.UserName, message.TimeStamp, message.Message);

            await dbTask;
        }


    }
}
