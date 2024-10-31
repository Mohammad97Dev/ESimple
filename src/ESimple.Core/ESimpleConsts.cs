using ESimple.Debugging;

namespace ESimple
{
    public class ESimpleConsts
    {
        public const string LocalizationSourceName = "ESimple";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;

        public const string AppServerRootAddressKey = "App:ServerRootAddress";


        public const string UploadsFolderName = "Uploads";
        public const string AttachmentsFolderName = "Attachments";
        public const string LowResolutionPhotosFolderName = "LowResolutionPhotos";


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "db99e33a006242fcaa0507dfad61af06";
    }
}
