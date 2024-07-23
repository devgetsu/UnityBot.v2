using UnityBot.Bot.Models.Entities;
using UnityBot.Bot.Models.Enums;
using UnityBot.Bot.Models.ViewModels;

namespace UnityBot.Bot.Services.UserServices
{
    public interface IUserService
    {
        public Task<short> CreateUser(UserModel user, CancellationToken cancellation);
        public Task<IEnumerable<UserModel>> GetAllUsers(CancellationToken cancellationToken);
        public Task<UserModel> GetUser(long chatId, CancellationToken cancellationToken);
        public Task<short> UpdateUserStatus(long userId, UserStatus status, CancellationToken cancellation);
        public Task<short> UpdateUserProperties(long userId, UserViewModel newUser, CancellationToken cancellation);
        public Task<short> UpdateUserOldAds(long userId, long adsId, CancellationToken cancellation);
        public Task<short> UpdateUserShouldDeleteId(long userId, int shouldDeleteId, CancellationToken cancellation);
        public Task<short> AddToListMessages(long userId, string message, CancellationToken cancellation);
        public Task<short> ClearListMessages(long userId, string message, CancellationToken cancellation);

    }
}