using Telegram.Bot.Types;
using UnityBot.Bot.Models.Enums;

namespace UnityBot.Bot.Models.Entities
{
    public class UserModel
    {
        public long Id { get; set; }
        public long Userid { get; set; }
        public long Chatid { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public UserStatus Status { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public int ShouldDeleteMessage { get; set; } = 0;
        public List<long> OldAds { get; set; } = new List<long>();
    }
}
