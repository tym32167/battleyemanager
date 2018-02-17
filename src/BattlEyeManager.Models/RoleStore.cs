using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BattlEyeManager.Models
{
    public class RoleModel : IdentityRole<Guid>, IEntity<Guid>
    {
        public override Guid Id { get; set; } = Guid.NewGuid();
    }
    

    public class RoleStore : IRoleStore<RoleModel>
    {
        private readonly IKeyValueStore<RoleModel, Guid> _store;

        public RoleStore(IKeyValueStore<RoleModel, Guid> store)
        {
            _store = store;
        }

        public void Dispose()
        {
        }

        public async Task<IdentityResult> CreateAsync(RoleModel role, CancellationToken cancellationToken)
        {
            await _store.AddAsync(role);
            return IdentityResult.Success;

        }

        public async Task<IdentityResult> UpdateAsync(RoleModel role, CancellationToken cancellationToken)
        {
            await _store.UpdateAsync(role);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(RoleModel role, CancellationToken cancellationToken)
        {
            await _store.DeleteAsync(role.Id);
            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(RoleModel role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(RoleModel role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(RoleModel role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedRoleNameAsync(RoleModel role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(RoleModel role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task<RoleModel> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return _store.FindAsync(Guid.Parse(roleId));
        }

        public async Task<RoleModel> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var res = await _store.FindAsync(r => r.NormalizedName == normalizedRoleName);
            return res.FirstOrDefault();
        }
    }
}