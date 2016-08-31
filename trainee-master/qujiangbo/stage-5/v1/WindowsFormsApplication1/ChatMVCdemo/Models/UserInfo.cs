using System;

namespace ChatMVCdemo.Models
{
    public class UserInfo
    {
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}