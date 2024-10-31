using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ESimple.Enums.Enum;

namespace ESimple.FileUploads.Dto
{
    public class UploadedFileInfo
    {
        public AttachmentType  Type { get; set; }
        public string RelativePath { get; set; }
        public string LowResolutionPhotoRelativePath { get; set; }
    }
}
