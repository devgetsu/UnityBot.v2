using Microsoft.EntityFrameworkCore;
using UnityBot.Bot.Models.Entities;
using UnityBot.Bot.Models.Enums;
using UnityBot.Bot.Models.ViewModels;
using UnityBot.Bot.Persistanse;

namespace UnityBot.Bot.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UnityDbContext _context;
        private readonly DbSet<UserModel> _users;

        public UserService(UnityDbContext context)
        {
            _context = context;
            _users = _context.Users;
        }

        /*
        
        - - Special codes - -
        
         21 - created
         22 - updated
         23 - deleted         
         24 - existed         
         25 - changed         
         26 - error         
        
        */

        public async Task<short> CreateUser(UserModel user, CancellationToken cancellation)
        {
            var finder = await _users.FirstOrDefaultAsync(x => x.Userid == user.Userid || x.Chatid == user.Chatid);
            if (finder != null)
            {
                return 24;
            }

            await _users.AddAsync(user, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return 21;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _users.ToListAsync(cancellationToken);
        }

        public async Task<UserModel> GetUser(long chatId, CancellationToken cancellationToken)
        {
            return await _users.FirstOrDefaultAsync(x => x.Chatid == chatId || x.Userid == chatId) ?? throw new NotImplementedException();
        }

        public async Task<short> UpdateUserStatus(long userId, UserStatus status, CancellationToken cancellation)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Userid == userId || x.Chatid == userId);
            if (user == null)
            {
                return 24;
            }

            user.Status = status;
            await _context.SaveChangesAsync(cancellation);
            return 22;
        }

        public async Task<short> UpdateUserProperties(long userId, UserViewModel newUser, CancellationToken cancellation)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Userid == userId || x.Chatid == userId);
            if (user == null)
            {
                return 24;
            }

            user.Username = newUser.Username;
            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            await _context.SaveChangesAsync(cancellation);
            return 22;
        }

        public async Task<short> UpdateUserOldAds(long userId, long adsId, CancellationToken cancellation)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Userid == userId || x.Chatid == userId);
            if (user == null)
            {
                return 24;
            }

            user.OldAds.Add(adsId);
            await _context.SaveChangesAsync(cancellation);
            return 22;
        }

        public async Task<short> UpdateUserShouldDeleteId(long userId, int shouldDeleteId, CancellationToken cancellation)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Userid == userId || x.Chatid == userId);
            if (user == null)
            {
                return 24;
            }

            user.ShouldDeleteMessage = shouldDeleteId;
            await _context.SaveChangesAsync(cancellation);
            return 22;
        }

        public async Task<short> AddToListMessages(long userId, string message, CancellationToken cancellation)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Userid == userId || x.Chatid == userId);
            if (user == null)
            {
                return 24;
            }

            user.Messages.Add(message);
            await _context.SaveChangesAsync(cancellation);
            return 22;
        }

        public async Task<short> ClearListMessages(long userId, string message, CancellationToken cancellation)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Userid == userId || x.Chatid == userId);
            if (user == null)
            {
                return 24;
            }

            user.Messages.Clear();
            await _context.SaveChangesAsync(cancellation);
            return 22;
        }
    }
}