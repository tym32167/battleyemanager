using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BattlEyeManager.Models
{
    public class UserRole : IEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string RoleName { get; set; }
    }

    public class UserStore : IUserPasswordStore<UserModel>, IUserRoleStore<UserModel>, IQueryableUserStore<UserModel>
    {
        private readonly IKeyValueStore<UserModel, Guid> _store;
        private readonly IKeyValueStore<UserRole, Guid> _userRoleStore;

        public UserStore(IKeyValueStore<UserModel, Guid> store, IKeyValueStore<UserRole, Guid> userRoleStore)
        {
            _store = store;
            _userRoleStore = userRoleStore;
        }

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
            user.UserName = userName;
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

        public async Task<IdentityResult> CreateAsync(UserModel user, CancellationToken cancellationToken)
        {
            await _store.AddAsync(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(UserModel user, CancellationToken cancellationToken)
        {
            await _store.UpdateAsync(user);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(UserModel user, CancellationToken cancellationToken)
        {
            await _store.DeleteAsync(user.Id);

            return IdentityResult.Success;
        }

        public Task<UserModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var gid = Guid.Parse(userId);
            return _store.FindAsync(gid);
        }

        public async Task<UserModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var res = await _store.FindAsync(u => u.NormalizedUserName == normalizedUserName);
            return res.FirstOrDefault();
        }

        public Task SetPasswordHashAsync(UserModel user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            user.Password = null;
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

        public Task AddToRoleAsync(UserModel user, string roleName, CancellationToken cancellationToken)
        {
            return _userRoleStore.AddAsync(new UserRole() { RoleName = roleName, UserId = user.Id });
        }

        public async Task RemoveFromRoleAsync(UserModel user, string roleName, CancellationToken cancellationToken)
        {
            var roles = await _userRoleStore.FindAsync(r => r.UserId == user.Id && r.RoleName == roleName);
            foreach (var role in roles)
            {
                await _userRoleStore.DeleteAsync(role.Id);
            }
        }

        public async Task<IList<string>> GetRolesAsync(UserModel user, CancellationToken cancellationToken)
        {
            var roleIds = await _userRoleStore.FindAsync(r => r.UserId == user.Id);
            return roleIds.Select(x => x.RoleName).ToArray();
        }

        public async Task<bool> IsInRoleAsync(UserModel user, string roleName, CancellationToken cancellationToken)
        {
            var usr = await _userRoleStore.FindAsync(x => x.Id == user.Id && x.RoleName == roleName);
            return usr.Any();
        }

        public async Task<IList<UserModel>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var uroles = await _userRoleStore.FindAsync(x => x.RoleName == roleName);
            var ids = uroles.Select(x => x.UserId).ToArray();
            var res = await _store.FindAsync(x => ids.Contains(x.Id));
            return res.ToArray();
        }

        public IQueryable<UserModel> Users
        {
            get { return _store.Find(x => true); }
        }
    }
}