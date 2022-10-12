using Microsoft.AspNetCore.SignalR;

namespace SignalR_Test.SignalR.Hubs
{
    public class ChatHub : Hub
    {

        [HubMethodName("NotifyToAll")]
        public async Task SendMessageToAll(string username, string message)
        {
            await this.Clients.All.SendAsync("MessageReceive", username, message + $" ConnectionId: {Context.ConnectionId}");
        }

        [HubMethodName("NotifyToOther")]
        public async Task SendMessageToOther(string username, string message)
        {
            await this.Clients.Others.SendAsync("MessageReceive", username, message);
        }

        [HubMethodName("DirectMessage")]
        public async Task SendMessageToCaller(string username, string message)
        {
            await this.Clients.Caller.SendAsync("MessageReceive", username, message);
        }
        
        [HubMethodName("DoTask")]
        public async Task DoTask(string username, string message)
        {
            await this.Clients.Caller.SendAsync("MessageReceive", username, MessageAppendTime(message));
            await Clients.Caller.SendAsync("MessageReceive", "System", new { Message = MessageAppendTime("Task is processing...") });
            _ = DoWork();
        }

        private async Task DoWork()
        {
            Task.Delay(3000).Wait();
            await Clients.Caller.SendAsync("MessageReceive", "System", MessageAppendTime("Completed."));
        }

        private string MessageAppendTime(string message)
        {
            return message + $" ({DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")})";
        }
    }
}
