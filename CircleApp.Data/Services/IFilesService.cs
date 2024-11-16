using CircleApp.Data.Helpers.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleApp.Data.Services
{
    public interface IFilesService
    {
        Task<string> UploadImageAsync(IFormFile file, ImageFileType imageFileType);
    }
}
