using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Enums
{
    public class Enum
    {
        public enum UserType : byte
        {
            Admin = 1,
            BasicUser = 2
        }
        public enum AttachmentType : byte
        {
            PDF = 1,
            WORD = 2,
            JPEG = 3,
            PNG = 4,
            JPG = 5,
            MP4 = 6,
            MP3 = 7,
            APK = 8,
        }
    }
}
