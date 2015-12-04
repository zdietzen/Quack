using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Quack.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quack.Core.Infrastructure
{
    public class AuthRepository : IDisposable
    {
        private QuackDbContext _dataContext;

        private UserManager<QuackUser> _userManager;

        public AuthRepository()
        {
            _dataContext = new QuackDbContext();
            _userManager = new UserManager<QuackUser>(new UserStore<QuackUser>(_dataContext));
        }

        public Client FindClient(string clientId)
        {
            var client = _dataContext.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var existingToken = _dataContext.RefreshTokens.SingleOrDefault(r => r.Subject == token.Subject &&
                                                                        r.ClientId == token.ClientId);

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _dataContext.RefreshTokens.Add(token);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = _dataContext.RefreshTokens.Find(refreshTokenId);

            if (refreshToken != null)
            {
                return await RemoveRefreshToken(refreshToken);
            }

            return false;
        }
        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _dataContext.RefreshTokens.Remove(refreshToken);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            return await Task.Factory.StartNew(() =>
            {
                var refreshToken = _dataContext.RefreshTokens.Find(refreshTokenId);

                return refreshToken;
            });
        }
    
        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _dataContext.RefreshTokens.ToList();
        }

        public async Task<QuackUser> FindAsync(UserLoginInfo loginInfo)
        {
            QuackUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(QuackUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
            _userManager.Dispose();

        }
    }

}