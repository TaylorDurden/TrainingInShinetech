using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using ChatMVCdemo.Models;

namespace ChatMVCdemo.Hubs
{
    public class ChatHub : Hub
    {
        public static List<UserInfo> OnlineUsers = new List<UserInfo>();
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Connect(string userId, string userName)

        {
            var connectionId = Context.ConnectionId;

            if (OnlineUsers.Count(x => x.ConnectionId == connectionId) == 0)
            {
                if (OnlineUsers.Any(x => x.UserId == userId))
                {
                    var userItems = OnlineUsers.Where(x => x.UserId == userId).ToList();
                    foreach (var item in userItems)
                    {
                        Clients.AllExcept(connectionId).onUserDisconnected(item.ConnectionId, item, userName);
                    }
                    OnlineUsers.RemoveAll(x => x.UserId == userId);
                }

                OnlineUsers.Add(new UserInfo
                {
                    ConnectionId = connectionId,
                    UserId = userId,
                    UserName = userName,
                    LastLoginTime = DateTime.Now
                });
            }

            Clients.All.onConnected(connectionId, userName, OnlineUsers);
        }

        ///// <summary>
        ///// Called when a connection disconnects from this hub gracefully or due to a timeout.
        ///// </summary>
        ///// <param name="stopCalled">
        ///// true, if stop was called on the client closing the connection gracefully;
        ///// false, if the connection has been lost for longer than the
        ///// <see cref="P:Microsoft.AspNet.SignalR.Configuration.IConfigurationManager.DisconnectTimeout" />.
        ///// Timeouts can be caused by clients reconnecting to another SignalR server in scaleout.
        ///// </param>
        ///// <returns>A <see cref="T:System.Threading.Tasks.Task" /></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = OnlineUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            if (user == null) return base.OnDisconnected(stopCalled);

            Clients.All.onUserDisconnected(user.ConnectionId, user.UserName);
            OnlineUsers.Remove(user);

            return base.OnDisconnected(stopCalled);
        }

        public void SendPrivateMessage(string toUserId, string message)
        {
            var fromUserId = Context.ConnectionId;

            var toUser = OnlineUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = OnlineUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if (toUser != null && fromUser != null)
            {
                // send to 
                Clients.Client(toUserId).receivePrivateMessage(fromUserId, fromUser.UserName, message);

                // send to caller user
                //Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
            }
            else
            {
                //表示对方不在线
                Clients.Caller.absentSubscriber();
            }
        }
    }
}