using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using ESimple.Authorization;
using ESimple.Authorization.Roles;
using ESimple.Authorization.Users;
using static ESimple.Enums.Enum;
using System.Collections.Generic;
using ESimple.Domains.Products;
using System.Threading.Tasks;

namespace ESimple.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly ESimpleDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(ESimpleDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            // Admin role

            var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }
            var basicUserRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.BasicUser);
            if (basicUserRole == null)
            {
                basicUserRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.BasicUser, StaticRoleNames.Tenants.BasicUser) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }

            // Grant all permissions to admin role

            var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == _tenantId && p.RoleId == adminRole.Id)
                .Select(p => p.Name)
                .ToList();

            var permissions = PermissionFinder
                .GetAllPermissions(new ESimpleAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRole.Id
                    })
                );
                _context.SaveChanges();
            }
            CheckBasicUserRoles(basicUserRole);

            // Admin user

            var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "123qwe");
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;
                adminUser.Type = UserType.Admin;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();
            }

        }
        private void CheckBasicUserRoles(Role basicUserRole)
        {
            var basicUserPermissionInDB = _context
                .Permissions
                .IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == _tenantId && p.RoleId == basicUserRole.Id)
                .Select(x => x.Name)
                .ToList();

            var allBasicUserPermissions = new List<string>
            {
                PermissionNames.Pages_Users,
                PermissionNames.Product_List,
                PermissionNames.Product_Get,
                PermissionNames.Order_Create,
                PermissionNames.Order_Get,
                PermissionNames.Order_List,
                PermissionNames.Order_Delete,
                PermissionNames.ShoppingCart_FullControl,
            };

            GrantPermissionToRole(
                role: basicUserRole,
                alreadyIncludedPermissions: basicUserPermissionInDB,
                actualPermissions: allBasicUserPermissions
            );
        }
        private void GrantPermissionToRole(Role role, List<string> alreadyIncludedPermissions, List<string> actualPermissions, int tenantId = 0)
        {
            var permissionsNotIncluded = PermissionFinder
                .GetAllPermissions(new ESimpleAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) && !alreadyIncludedPermissions.Contains(p.Name) && actualPermissions.Contains(p.Name))
                .ToList();

            if (permissionsNotIncluded.Any())
            {
                _context.Permissions.AddRange(
                    permissionsNotIncluded.Select(permission => new RolePermissionSetting
                    {
                        RoleId = role.Id,
                        IsGranted = true,
                        TenantId = tenantId == 0 ? _tenantId : tenantId,
                        Name = permission.Name,
                    })
                );

                _context.SaveChanges();
            }
        }
    }
}
