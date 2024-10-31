using System.Collections.Generic;
using Abp.Configuration;

namespace ESimple.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),


                new SettingDefinition(AppSettingNames.SenderHost, "smtp.gmail.com",scopes: SettingScopes.Application | SettingScopes.All),
                new SettingDefinition(AppSettingNames.SenderPort, "587",scopes: SettingScopes.Application | SettingScopes.All),
                new SettingDefinition(AppSettingNames.SenderEmail, "",scopes: SettingScopes.Application | SettingScopes.All),
                new SettingDefinition(AppSettingNames.SenderPassword, "",scopes: SettingScopes.Application | SettingScopes.All),
                new SettingDefinition(AppSettingNames.SenderEnableSsl, "true", scopes: SettingScopes.Application | SettingScopes.All),
                new SettingDefinition(AppSettingNames.SenderUseDefaultCredentials, "false", scopes: SettingScopes.Application | SettingScopes.All),
                new SettingDefinition(AppSettingNames.Message, "Hello", scopes: SettingScopes.Application | SettingScopes.All),
                new SettingDefinition(AppSettingNames.MessageForResetPassword, "Reset Your Password", scopes: SettingScopes.Application | SettingScopes.All),
                new SettingDefinition(AppSettingNames.ImageSize, "100", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),

            };
        }
    }
}
