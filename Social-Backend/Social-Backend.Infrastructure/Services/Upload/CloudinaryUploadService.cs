using Microsoft.AspNetCore.Http;
using Social_Backend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Services.Upload
{
    public class CloudinaryUploadService : IUploadService
    {
        public Task DeleteFile(string filename)
        {
            throw new NotImplementedException();
        }

        public string GetFile(string filename)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadFile(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}