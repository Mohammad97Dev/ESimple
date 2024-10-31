using Abp.Application.Services;
using Abp.Dependency;
using ESimple.FileUploads.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.FileUploads
{
    public interface IFileUploadService : IApplicationService
    {
        /// <summary>
        /// Save file to Attachments folder (wwwroot\Attachments\).
        /// </summary>
        /// <param name="file">uploaded file</param>
        /// <returns>AttachmentType and Path of saved file relative to wwwroot folder</returns>
        Task<UploadedFileInfo> SaveAttachmentAsync(IFormFile file);
       
        /// <summary>
        /// Delete Attachment
        /// </summary>
        /// <param name="fileRelativePath"></param>
        void DeleteAttachment(string fileRelativePath);
    }

}
