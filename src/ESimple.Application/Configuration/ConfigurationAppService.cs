using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Runtime.Session;
using ESimple.Authorization;
using ESimple.Configuration.Dto;

namespace ESimple.Configuration
{
    [AbpAuthorize(PermissionNames.Pages_Configuration)]
    public class ConfigurationAppService : ESimpleAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }

        public async Task SetEmailSetting(EmailSettingDto input)
        {


            if (input.SenderEmail != SettingManager.GetSettingValue(AppSettingNames.SenderEmail))
                await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SenderEmail,
                        input.SenderEmail.ToString());

            if (input.SenderPassword != SettingManager.GetSettingValue(AppSettingNames.SenderPassword))
                await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SenderPassword,
                      input.SenderPassword.ToString());

            if (input.SenderHost != SettingManager.GetSettingValue(AppSettingNames.SenderHost))
                await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SenderHost,
                      input.SenderHost.ToString());

            if (input.Message != SettingManager.GetSettingValue(AppSettingNames.Message))
                await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.Message,
                    input.Message.ToString());

            if (input.MessageForResetPassword != SettingManager.GetSettingValue(AppSettingNames.MessageForResetPassword))
                await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.MessageForResetPassword,
                    input.MessageForResetPassword.ToString());

            if (input.SenderEnableSsl != SettingManager.GetSettingValue<bool>(AppSettingNames.SenderEnableSsl))
                await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SenderEnableSsl,
                    input.SenderEnableSsl.ToString());

            if (input.SenderUseDefaultCredentials != SettingManager.GetSettingValue<bool>(AppSettingNames.SenderUseDefaultCredentials))
                await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SenderUseDefaultCredentials,
                    input.SenderUseDefaultCredentials.ToString());

            if (input.SenderPort != SettingManager.GetSettingValue<int>(AppSettingNames.SenderPort))
                await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.SenderPort,
                    input.SenderPort.ToString());
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        public async Task<EmailSettingDto> GetEmailSetting()
        {

            var emailSettingDto = new EmailSettingDto()
            {
                SenderEmail = await SettingManager.GetSettingValueAsync(AppSettingNames.SenderEmail),
                SenderPassword = await SettingManager.GetSettingValueAsync(AppSettingNames.SenderPassword),
                SenderHost = await SettingManager.GetSettingValueAsync(AppSettingNames.SenderHost),
                SenderPort = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.SenderPort),
                SenderEnableSsl = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.SenderEnableSsl),
                SenderUseDefaultCredentials = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.SenderUseDefaultCredentials),
                Message = await SettingManager.GetSettingValueAsync(AppSettingNames.Message),
                MessageForResetPassword = await SettingManager.GetSettingValueAsync(AppSettingNames.MessageForResetPassword)
            };
            return emailSettingDto;
        }
    }
}
