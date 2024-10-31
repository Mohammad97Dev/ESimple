using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ESimple.Authorization
{
    public class ESimpleAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Users_Create, L("UserCreation"));
            context.CreatePermission(PermissionNames.Pages_Users_Update, L("UserUpdating"));
            context.CreatePermission(PermissionNames.Pages_Users_Delete, L("UserDeleting"));
            context.CreatePermission(PermissionNames.Pages_Users_List, L("UserList"));
            context.CreatePermission(PermissionNames.Pages_Users_RolesList, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.Product_Create, L("ProductCreate"));
            context.CreatePermission(PermissionNames.Product_Update, L("ProductUpdate"));
            context.CreatePermission(PermissionNames.Product_Delete, L("ProductDelete"));
            context.CreatePermission(PermissionNames.Product_Get, L("ProductGet"));
            context.CreatePermission(PermissionNames.Product_List, L("ProductList"));

            context.CreatePermission(PermissionNames.Order_Create, L("OrderCreate"));
            context.CreatePermission(PermissionNames.Order_Get, L("OrderGet"));
            context.CreatePermission(PermissionNames.Order_List, L("OrderList"));
            context.CreatePermission(PermissionNames.Order_Delete, L("OrderDelete"));

            context.CreatePermission(PermissionNames.ShoppingCart_FullControl, L("ShoppingCartFullControl"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ESimpleConsts.LocalizationSourceName);
        }
    }
}
