using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPVS_API.Data;
using CPVS_API.DTO;
using Microsoft.AspNetCore.SignalR;

namespace CPVS_API.SignalrHub
{
    public class ECHub : Hub
    {

        static HashSet<string> CurrentConnections = new HashSet<string>();
        private readonly static ConnectionMapping<string> _connections =
         new ConnectionMapping<string>();
        static int count;
        static int id;
        static List<int> _idd = new List<int>();
        DataContext _context;
        public ECHub(DataContext context)
        {
            _context = context;
        }

        public async Task Start(string message, string line , int ID)
        {
            _idd.Add(ID);

            //id = ID;
            //Console.ForegroundColor = ConsoleColor.Red;
            //string newMessage = $"#### ### Listen from angular Start: {_idd}";
            //Console.WriteLine(newMessage);

            await Clients.All.SendAsync("Start", message , line , ID);
        }

        public async Task Stop(string message , string line , int ID)
        {
            //count = 0;
            //id = 0;

            _idd.Remove(ID);

            //Console.ForegroundColor = ConsoleColor.Yellow;
            //string newMessage = $"#### ### Listen from angular Stop: {_idd}";
            //Console.WriteLine(newMessage);

            await Clients.All.SendAsync("Stop", message, line , ID);
        }

        public async Task Welcom(string message , string line , string boxCount)
        {
            foreach (var item in _idd)
            {
                if (item != 0)
                {
                    //var count_box = count + 1;
                    var items =  _context.Plans.FirstOrDefault(x => x.Line_Name.ToUpper() == line.ToUpper() && x.ID == item);
                    if (items != null)
                    {
                        items.Amount = items.Amount + int.Parse(boxCount);
                        //count++;
                        try
                        {
                            await _context.SaveChangesAsync();
                            await Clients.All.SendAsync("Welcom", message, line);
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        //public async Task CheckOnline(string user, string message)
        //{
        //    await Clients.All.SendAsync("Online", user, message);
        //}
        //public override async Task OnConnectedAsync()
        //{
        //    var id = Context.ConnectionId;
        //    CurrentConnections.Add(id);
        //    await Clients.All.SendAsync("Online", CurrentConnections.Count);
        //    _connections.Add(id, Context.ConnectionId);
        //    await base.OnConnectedAsync();
        //}

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    var connection = CurrentConnections.FirstOrDefault(x => x == Context.ConnectionId);
        //    if (connection != null)
        //    {
        //        CurrentConnections.Remove(connection);
        //        _connections.Remove(connection, Context.ConnectionId);
        //    }
        //    await Clients.All.SendAsync("Online", CurrentConnections.Count);
        //    await base.OnDisconnectedAsync(exception);
        //}

        //return list of all active connections

        public List<string> GetAllActiveConnections()
        {
            return CurrentConnections.ToList();
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}