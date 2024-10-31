using Abp.Application.Services;
using Abp.Configuration;
using Abp.UI;
using Castle.Core.Logging;
using ESimple.Configuration;
using ESimple.FileUploads.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ESimple.Enums.Enum;

namespace ESimple.FileUploads
{
    public class FileUploadService : ApplicationService, IFileUploadService
    {
        //Can get those constants from configuration
        private static readonly string AttachmentsFolder = Path.Combine(ESimpleConsts.UploadsFolderName, ESimpleConsts.AttachmentsFolderName);
        private static readonly string LowResolutionPhotosFolder = Path.Combine(ESimpleConsts.UploadsFolderName, ESimpleConsts.LowResolutionPhotosFolderName);

        private readonly ISettingManager _settingManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingManager"></param>
        /// <param name="webHostEnvironment"></param>
        public FileUploadService(
            ISettingManager settingManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _settingManager = settingManager;
            _webHostEnvironment = webHostEnvironment;
            Logger = NullLogger.Instance;
        }
        /// <summary>
        /// Save Attachment Async
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<UploadedFileInfo> SaveAttachmentAsync(IFormFile file)
        {
            try
            {
                var fileInfo = new UploadedFileInfo { Type = GetAndCheckFileType(file) };

                var fileName = GenerateUniqueFileName(file);
                var pathToSaveAttacment = GetPathToSaveAttachment(fileName, AttachmentsFolder);

                fileInfo.RelativePath = GetAttachmentRelativePath(fileName, AttachmentsFolder);
                using (var stream = new FileStream(pathToSaveAttacment, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                if (fileInfo.Type == AttachmentType.PNG || fileInfo.Type == AttachmentType.JPG || fileInfo.Type == AttachmentType.JPEG)
                {
                    var pathToSaveLowResolutionPhotos = GetPathToSaveAttachment(fileName, LowResolutionPhotosFolder);
                    // Load the original image from the saved file
                    using (var originalImage = Image.Load(pathToSaveAttacment))
                    {
                        var ImageSize = int.Parse(_settingManager.GetSettingValue(AppSettingNames.ImageSize));
                        // Create and save a version with resolution 200x200
                        originalImage.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new Size(ImageSize),
                            Mode = ResizeMode.Max
                        }));
                        var pathToSaveLowResolutionPhotos200 = GetPathToSaveAttachment(fileName, LowResolutionPhotosFolder);
                        originalImage.Save(pathToSaveLowResolutionPhotos200);
                    }
                    fileInfo.LowResolutionPhotoRelativePath = GetAttachmentRelativePath(fileName, LowResolutionPhotosFolder);
                }

                Logger.Info($"Base Attachment File was saved to ({pathToSaveAttacment}) successfully.");

                return fileInfo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Delete Attachment
        /// </summary>
        /// <param name="fileRelativePath"></param>
        public void DeleteAttachment(string fileRelativePath)
        {
            var pathFile = GetAbsolutePath(fileRelativePath);

            if (!File.Exists(pathFile))
            {
                Logger.Info($"Attachment File ({pathFile}) is not found.");
                return;
            }

            File.Delete(pathFile);

            Logger.Info($"Attachment File ({pathFile}) was deleted successfully.");
        }

        /// <summary>
        /// Get And Check File Type
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private AttachmentType GetAndCheckFileType(IFormFile file)
        {
            foreach (AttachmentType type in Enum.GetValues(typeof(AttachmentType)))
            {
                if (file.ContentType.Contains(type.ToString().ToLower()))
                    return type;
            }

            throw new UserFriendlyException("TheAttachedFileTypeIsNotAcceptable", $"FileName: {file.FileName}");
        }



        private string GetAbsolutePath(string fileRelativePath)
        {
            var basePath = _webHostEnvironment.WebRootPath;
            return Path.Combine(basePath, fileRelativePath);
        }

        private string GetPathToSaveAttachment(string fileName, string folderName)
        {
            var basePath = _webHostEnvironment.WebRootPath;
            return Path.Combine(basePath, folderName, fileName);
        }

        private string GetAttachmentRelativePath(string fileName, string folderName)
        {
            return Path.Combine(folderName, fileName);
        }

        private string GenerateUniqueFileName(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}_{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";
            return fileName;
        }


    }

}
