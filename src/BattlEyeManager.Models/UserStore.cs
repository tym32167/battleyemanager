using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BattlEyeManager.Models
{
    public class UserStore : IUserPasswordStore<UserModel>
    {
        private readonly Dictionary<Guid, UserModel> _byId = new Dictionary<Guid, UserModel>();
        private readonly Dictionary<string, UserModel> _byName = new Dictionary<string, UserModel>();
        private readonly Dictionary<string, UserModel> _byEmail = new Dictionary<string, UserModel>();

        public void Dispose()
        {

        }

        public Task<string> GetUserIdAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(UserModel user, string userName, CancellationToken cancellationToken)
        {
            _byId[user.Id].UserName = userName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(UserModel user, string normalizedName,
            CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(UserModel user, CancellationToken cancellationToken)
        {
            _byId.Add(user.Id, user);
            _byName.Add(user.NormalizedUserName, user);
            _byEmail.Add(user.Email, user);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(UserModel user, CancellationToken cancellationToken)
        {
            if (_byId.ContainsKey(user.Id))
                _byId.Remove(user.Id);

            if (_byName.ContainsKey(user.NormalizedUserName))
                _byName.Remove(user.NormalizedUserName);

            if (_byEmail.ContainsKey(user.Email))
                _byEmail.Remove(user.Email);

            _byId.Add(user.Id, user);
            _byName.Add(user.NormalizedUserName, user);
            _byEmail.Add(user.Email, user);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(UserModel user, CancellationToken cancellationToken)
        {
            if (_byId.ContainsKey(user.Id))
                _byId.Remove(user.Id);

            if (_byName.ContainsKey(user.NormalizedUserName))
                _byName.Remove(user.NormalizedUserName);

            if (_byEmail.ContainsKey(user.Email))
                _byEmail.Remove(user.Email);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<UserModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var gid = Guid.Parse(userId);
            return Task.FromResult(_byId[gid]);
        }

        public Task<UserModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            if (!_byName.ContainsKey(normalizedUserName)) return Task.FromResult<UserModel>(null);
            return Task.FromResult(_byName[normalizedUserName]);
        }

        public Task SetPasswordHashAsync(UserModel user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }
    }
}